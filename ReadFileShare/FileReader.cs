
using System.Diagnostics;

namespace ReadFileShare;

public class FileReader
{
    public event EventHandler<string> NewLineEvent;

    public async Task ReadFromFileWithoutFileShare(string filePath, CancellationToken cancellationToken)
    {
        // Open the file without FileShare.ReadWrite
        using FileStream fs = new(filePath, FileMode.Open, FileAccess.ReadWrite);
        using StreamReader reader = new(fs);

        string? line;
        while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
        {
            line = await reader.ReadLineAsync(CancellationToken.None);
            OnNewLineEvent(line);
        }
    }

    protected virtual void OnNewLineEvent(string line)
    {
        Debug.WriteLine($"New line found: {line}");
        NewLineEvent?.Invoke(this, line);
    }

    public async Task ReadFromFile(string filePath, CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader reader = new(fs))
                {
                    await ReadAndProcessFileContent(reader);

                    // Check for file modification
                    await WaitForFileModification(filePath, cancellationToken);
                }
            }
        }
        catch (TaskCanceledException)
        {
        }
    }

    private async Task ReadAndProcessFileContent(StreamReader reader)
    {
        string line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            OnNewLineEvent(line);
        }
    }

    private async Task WaitForFileModification(string filePath, CancellationToken cancellationToken)
    {
        DateTime lastModified = File.GetLastWriteTime(filePath);
        DateTime currentModified;

        do
        {
            await Task.Delay(1000, CancellationToken.None); // Wait until file is modified
            currentModified = File.GetLastWriteTime(filePath);
        } while (currentModified == lastModified && !cancellationToken.IsCancellationRequested);
    }
}