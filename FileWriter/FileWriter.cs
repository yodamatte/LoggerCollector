
namespace FileWriter;
public class FileWriter
{
    public async Task FillFileWithData(string filePath, CancellationToken cancellationToken)
    {
        // Continuously write data to the file until cancellation is requested
        using StreamWriter outputFile = new(filePath);
        int i = 0;

        while (true)
        {
            string msg = $"Test{i++}";
            Console.WriteLine(msg);
            await outputFile.WriteLineAsync(msg);
            await outputFile.FlushAsync(); // Flush the buffer to ensure data is written immediately

            await Task.Delay(1000);
            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}
