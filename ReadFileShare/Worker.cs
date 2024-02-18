
namespace ReadFileShare;

public class Worker
{
    public bool Running { get; private set; }
    public async Task Run(string filePath, CancellationTokenSource cts, EventHandler<string> eventHandler)
    {
        Running = true;

        try
        {
            var fileWriter = new FileWriter();
            var fileReader = new FileReader();

            fileReader.NewLineEvent += eventHandler;

            // Create cancellation token for the main thread
            CancellationToken cancellationToken = cts.Token;

            // Start the writing and reading tasks concurrently
            Task writingTask = Task.Run(() => fileWriter.FillFileWithData(filePath, cancellationToken));
            Task readingTask = Task.Run(() => fileReader.ReadFromFile(filePath, cancellationToken));

            // Wait for either tasks to complete
            await Task.WhenAll(writingTask, readingTask);

            Running = false;
            // Cancel the remaining task
            cts.Cancel();
        }
        catch (OperationCanceledException ex) 
        { 
            Running = false;
        }

        Console.WriteLine("Main thread exiting...");
    }
}
