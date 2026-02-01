namespace Battleships.Services;

public class BackgroundCleanupService(ILogger<BackgroundCleanupService> logger, ISimpleGameService gameService)
    : BackgroundService
{
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5);


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_checkInterval);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            logger.LogInformation("Background Check: Cleaning up stale games...");
            
            gameService.CleanupFinishedGames();
            
            logger.LogInformation("Background Check: Finished removing finished games.");
        }
    }
}