
public class DefDirectoryInfo
{
    internal static string DEF_PATH = Environment.CurrentDirectory;
    internal static string DEF_NAME = @"\sizes-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
}
class PathCorrector : DefDirectoryInfo
{
    public static string getAbsolutePathFolder(string input)
    {
        string path = System.IO.Path.GetFullPath(input);
        if (Directory.Exists(path))
            return @$"{path}";

        return DEF_PATH;


    }
    public static string getAbsolutePathFile(string input)
    {
        string path = System.IO.Path.GetFullPath(input);
        if (File.Exists(path))
            return @$"{path}";
        if (Directory.Exists(System.IO.Path.GetDirectoryName(path)))
            return $@"{path}";
        return DEF_PATH + DEF_NAME;


    }
}
class Program : DefDirectoryInfo
{
    static void Main(string[] args)
    {
        string[] argsDefault = {
            "-h",
            "-p",
            @$"{DEF_PATH}",
            "-o",
            @$"{DEF_PATH + DEF_NAME}"

        };
        if (args.Length == 0)
        {
            new ConsoleARGS(argsDefault);

        }
        else
            new ConsoleARGS(args);

    }
}

