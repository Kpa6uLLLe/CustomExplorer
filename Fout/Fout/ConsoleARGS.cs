

public class Data : DefDirectoryInfo
{
    internal static string[] PossibleArgs = {
            "-q",
            "--quite",
            "-p",
            "--path",
            "-o",
            "--output",
            "-h",
            "--humanread"
        };

}
class Request : Data
{
    public static int param1 = 0, param2 = 0;
}

class BoolRequest : Request
{
    public static bool defaultArg = false;

}
class PathRequest : Request
{
    public static string defaultArg = Environment.CurrentDirectory;


}
class Loud : BoolRequest
{

    public static new int param1 = 0, param2 = 1;
    public bool GetParam(string[] argus)
    {

        if (ConsoleARGS.ToFindAnArgNP(PossibleArgs[param1]) == "" && ConsoleARGS.ToFindAnArgNP(PossibleArgs[param2]) == "")
            return true;
        return false;
    }
}

class FilePath : PathRequest
{
    public static new int param1 = 2, param2 = 3;
    public string GetParam(string[] argus)
    {
        string s1 = ConsoleARGS.ToFindAnArg(PossibleArgs[param1]);
        string s2 = ConsoleARGS.ToFindAnArg(PossibleArgs[param2]);
        if (s1 != "")
            return PathCorrector.getAbsolutePathFolder(s1);
        if (s2 != "")
            return PathCorrector.getAbsolutePathFolder(s2);
        return defaultArg;
    }
}
class Output : PathRequest
{
    public static new int param1 = 4, param2 = 5;
    public string GetParam(string[] argus)
    {
        string s1 = ConsoleARGS.ToFindAnArg(PossibleArgs[param1]);
        string s2 = ConsoleARGS.ToFindAnArg(PossibleArgs[param2]);

        if (s1 != "")
            return PathCorrector.getAbsolutePathFile(s1);
        if (s2 != "")
            return PathCorrector.getAbsolutePathFile(s2);
        return defaultArg + DEF_NAME;
    }
}
class Humanread : BoolRequest
{
    public static new int param1 = 6, param2 = 7;
    public bool GetParam(string[] argus)
    {

        if (ConsoleARGS.ToFindAnArgNP(PossibleArgs[param1]) != "" || ConsoleARGS.ToFindAnArgNP(PossibleArgs[param2]) != "")
            return true;
        return false;
    }
}


public class ConsoleARGS : Data
{
    static string[] args = { };
    Humanread h = new Humanread();
    Output o = new Output();
    FilePath fp = new FilePath();
    Loud l = new Loud();
    string path, output;
    bool loud, humanread;
    public ConsoleARGS(string[] argus)
    {
        args = argus;
        humanread = h.GetParam(args);
        loud = l.GetParam(args);
        output = o.GetParam(args);
        path = fp.GetParam(args);
        Algo algo;
        if (Directory.Exists(path) && (Directory.Exists(System.IO.Path.GetDirectoryName(output)) || File.Exists(output)))
            algo = new Algo(loud, path, output, humanread);
        else
        {
            Console.WriteLine("#Error, retry\n");
        }


    }
    public static string ToFindAnArg(string s)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == s && i + 1 != args.Length)
            {
                return args[i + 1];
            }
        }
        return "";
    }
    public static string ToFindAnArgNP(string s)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == s)
            {
                return args[i];
            }
        }
        return "";
    }
}

