namespace Utility;

public class FileUtility
{
    private const string reportDataDirectory = "/test-data";

    private const int NewFilePerRequest = 1000;

    private static byte[] TestData = GenerateBytes(10 * 1024);

    private static byte[] GenerateBytes(int count)
    {
        var result = new byte[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = (byte)(i % 256);
        }

        return result;
    }

    public static async Task WriteFile()
    {
        SafeCreateDirectory();

        for (var i = 0; i < NewFilePerRequest; i++)
        {
            var fileName = Guid.NewGuid().ToString() + ".xlsx";
            var filePath = Path.Combine(reportDataDirectory, fileName);
            using var fi = new FileStream(filePath, FileMode.Create);
            await fi.WriteAsync(TestData, 0, TestData.Length);
            fi.Close();
        }
    }

    private static void SafeCreateDirectory()
    {
        if (!Directory.Exists(reportDataDirectory))
        {
            Directory.CreateDirectory(reportDataDirectory);
        }
    }

    public static async Task WriteMemoryStream()
    {
        for (var i = 0; i < NewFilePerRequest; i++)
        {
            using var fi = new MemoryStream(TestData.Length);
            await fi.WriteAsync(TestData, 0, TestData.Length);
            fi.Close(); 
        }
    }

    public static void ClearFiles()
    {
        Directory.GetFiles(reportDataDirectory).ToList().ForEach(f => File.Delete(f));
    }
}
