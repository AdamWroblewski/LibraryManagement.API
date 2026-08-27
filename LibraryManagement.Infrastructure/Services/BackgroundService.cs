using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Infrastructure.BackgroundServices
{
    public class ExpiredReservationsCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ExpiredReservationsCleanupService> _logger;
        private readonly TimeProvider _timeProvider;

        private static readonly TimeSpan HoldPolicyWindow = TimeSpan.FromHours(72);
        private static readonly TimeSpan CheckInterval = TimeSpan.FromMinutes(15);

        public ExpiredReservationsCleanupService(
            IServiceScopeFactory scopeFactory,
            ILogger<ExpiredReservationsCleanupService> logger,
            TimeProvider? timeProvider = null)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _timeProvider = timeProvider ?? TimeProvider.System;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Expired Reservations Cleanup Service starting with a {Interval}-minute interval.", CheckInterval.TotalMinutes);

            using var timer = new PeriodicTimer(CheckInterval, _timeProvider);

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                await ProcessExpiredReservationsSafeAsync(stoppingToken);
            }
        }

        private async Task ProcessExpiredReservationsSafeAsync(CancellationToken cancellationToken)
        {
            try
            {
                await ProcessExpiredReservationsAsync(cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing expired reservations.");
            }
        }

        private async Task ProcessExpiredReservationsAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;
            var expirationThreshold = utcNow.AddHours(-BookLoan.ReservationHoldPolicyHours);

            var expiredLoans = await context.BookLoans
                .Where(l => l.Status == LoanStatus.Reserved && l.ReservedAt <= expirationThreshold)
                .ToListAsync(cancellationToken);

            if (!expiredLoans.Any())
            {
                return;
            }

            _logger.LogInformation("Found {Count} expired reservations to process.", expiredLoans.Count);

            foreach (var loan in expiredLoans)
            {
                loan.MarkAsExpired();
            }

            await context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully expired {Count} reservations.", expiredLoans.Count);
        }
    }
}