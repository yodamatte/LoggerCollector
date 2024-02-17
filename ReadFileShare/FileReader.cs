
namespace ReadFileShare;
public class FileReader
{
    private readonly EventHandler<string> _newLineEvent;
    public FileReader(EventHandler<string> newLineEvent) 
    {
        _newLineEvent = newLineEvent;
    }

    public async Task ReadFromFileWithoutFileShare(string filePath, CancellationToken cancellationToken)
    {
        // Open the file without FileShare.ReadWrite
        using FileStream fs = new(filePath, FileMode.Open, FileAccess.ReadWrite);
        using StreamReader reader = new(fs);

        string line;
        while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
        {
            line = await reader.ReadLineAsync();
            ProgressResult(line);
        }
    }

    private void ProgressResult(string line)
    {
        _newLineEvent?.Invoke(this, line);
    }

    public async Task ReadFromFile(string filePath, CancellationToken cancellationToken)
    {
        using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using StreamReader reader = new(fs);

        DateTime lastModified = File.GetLastWriteTime(filePath);
        string line = null;

        // Continue reading until cancellation is requested or no changes for 10 seconds
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(1000); // Check for changes every second

            DateTime currentModified = File.GetLastWriteTime(filePath);
            if (currentModified != lastModified)
            {
                // File has been modified, read the new content
                lastModified = currentModified;
                fs.Seek(0, SeekOrigin.Begin); // Move the stream pointer to the beginning
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    ProgressResult(line);
                }
            }
            else
            {
                // No changes for 10 seconds, exit the loop
                await Task.Delay(9000); // Wait for the remaining 9 seconds
                break;
            }
        }
    }
}