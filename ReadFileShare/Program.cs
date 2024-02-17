namespace ReadFileShare;

public class Program
{
    public static async Task Main()
    {
        string filePath = @"C:\Users\Matte\OneDrive\Skrivbord\TestData\Test.txt";
        Worker worker = new();


        await worker.Run(filePath);
    }
}
