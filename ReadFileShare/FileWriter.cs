
namespace ReadFileShare;
public class FileWriter
{
    public async Task FillFileWithData(string filePath, CancellationToken cancellationToken)
    {
        // Continuously write data to the file until cancellation is requested
        using StreamWriter outputFile = new(filePath);
        int i = 0;

        while (!cancellationToken.IsCancellationRequested)
        {
            await outputFile.WriteLineAsync($"Test{i++}");
            await outputFile.FlushAsync(); // Flush the buffer to ensure data is written immediately

            // Introduce a delay to simulate writing process
            await Task.Delay(100);
        }
    }
}
