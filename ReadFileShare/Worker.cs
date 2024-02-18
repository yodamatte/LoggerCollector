
namespace ReadFileShare;

public class Worker
{
    public bool Running { get; private set; }
    public async Task Run(string filePath, CancellationTokenSource cts, EventHandler<string> eventHandler)
    {
        Running = true;

        var fileReader = new FileReader();

        fileReader.NewLineEvent += eventHandler;

        // Create cancellation token for the main thread
        CancellationToken cancellationToken = cts.Token;

        try
        {
            // Start the writing and reading tasks concurrently
            //Task writingTask = Task.Run(() => fileWriter.FillFileWithData(filePath, cancellationToken));
            await Task.Run(() => fileReader.ReadFromFile(filePath, cancellationToken));
        }
        catch (TaskCanceledException)
        {
            // Handle cancellation if needed
        }
        finally
        {
            Running = false;
        }
    }
}
