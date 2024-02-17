namespace ReadFileShare;

public class Program
{
    public static async Task Main()
    {
        string filePath = @"C:\Users\Matte\OneDrive\Skrivbord\TestData\Test.txt";
        Worker worker = new();

        CancellationTokenSource cts = new();
        Task.Run(() => SetupCancellationListener(cts));
        await worker.Run(filePath, cts, HandleNewLine);
    }

    private static async Task SetupCancellationListener(CancellationTokenSource cancellationTokenSource)
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
    private static void HandleNewLine(object sender, string line)
    {
        Console.WriteLine($"New line: {line}");
    }
}
