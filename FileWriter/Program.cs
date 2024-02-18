namespace FileWriter;

public class Program
{
    public static async Task Main()
    {
        string filePath = @"C:\Users\Matte\OneDrive\Skrivbord\TestData\Test.txt";

        CancellationTokenSource cts = new();
        Task.Run(() => SetupCancellationListener(cts));

        try
        {
            await new FileWriter().FillFileWithData(filePath, cts.Token);
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine(ex.Message);
        }
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
}
