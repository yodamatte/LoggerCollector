
namespace ReadFileShare;

public class Worker
{
    public async Task Run(string filePath)
    {
        // Create an event to notify when a new line is available
        EventHandler<string> newLineEvent = HandleNewLine;

        var fileWriter = new FileWriter();
        var fileReader = new FileReader(newLineEvent);

        // Create cancellation token for the main thread
        CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        // Start the writing and reading tasks concurrently
        Task writingTask = Task.Run(() => fileWriter.FillFileWithData(filePath, cancellationToken));
        Task readingTask = Task.Run(() => fileReader.ReadFromFile(filePath, cancellationToken));

        await SetupCancellationListener(cancellationTokenSource);

        // Wait for either tasks to complete
        await Task.WhenAny(writingTask, readingTask);

        // Cancel the remaining task
        cancellationTokenSource.Cancel();

        Console.WriteLine("Main thread exiting...");
    }

    private void HandleNewLine(object sender, string line)
    {
        Console.WriteLine($"New line: {line}");
    }

    private async Task SetupCancellationListener(CancellationTokenSource cancellationTokenSource)
    {
        // Listen for cancellation shortcut (Escape key)
        Task cancellationListener = Task.Run(() =>
        {
            Console.WriteLine("Press Escape key to cancel...");
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    cancellationTokenSource.Cancel();
                    Console.WriteLine("Cancellation requested.");
                    break;
                }
            }
        });
        // Wait for cancellation listener to complete
        await cancellationListener;
    }
}
