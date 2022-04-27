public class eData : DefDirectoryInfo
{
    internal static readonly string[] measuring = {
        "bytes",
        "KB",
        "MB",
        "GB",
    };
}
public class Entity : eData
{
    public long size = 0;
    public string name = "*", text = "";
}
public class Algo : eData
{
    const int DEFAULTDIST = 0;
    public Algo(bool loud, string path, string output, bool humanread)
    {
        SearchManager(loud, path, output, humanread);
    }
    public static Entity Measure(Entity entity)
    {
        Entity temp = entity;
        float db = temp.size;
        int i = 0;
        while (db >= 1024)
        {
            db /= 1024;
            i++;
        }
        if (i > 0)
            temp.name = entity.name + "(" + String.Format("{0:f2}", db) + measuring[i] + ")\n";
        if (i == 0)
            temp.name = entity.name + "( " + String.Format($"{db}") + measuring[i] + ")\n";
        return temp;
    }


    public void SaveData(string path, string s)
    {
        using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            byte[] array = System.Text.Encoding.Default.GetBytes(s);
            stream.Write(array, 0, array.Length);
            Console.WriteLine($"#Success!, your file is in: {path}");
        }
    }
    public Entity SearchManager(bool loud, string path, string output, bool humanread)
    {
        Entity entity;
        if (humanread == false)
            entity = RecursionSearch(path, DEFAULTDIST);
        else
            entity = RecursionSearch(path, DEFAULTDIST, humanread);

        if (loud == true)
            Console.WriteLine(entity.text);

        SaveData(output, entity.text);

        return entity;
    }
    private Entity RecursionSearch(string path, int counter)
    {
        Entity entity = new Entity();
        Entity temp = new Entity();
        DirectoryInfo di = new DirectoryInfo(path);
        FileInfo[] fiArr = di.GetFiles();
        DirectoryInfo[] diArr = di.GetDirectories();
        if (counter == 0)
            entity.name += "<";
        entity.name += di.Name;
        for (int i = 0; i < counter; i++)
            entity.name = "-" + entity.name;
        if (counter == 0)
            entity.name += ">";
        string temptext = "";

        foreach (FileInfo f in fiArr)
        {
            for (int i = 0; i <= counter + 1; i++)
                entity.text += "-";
            entity.text += f.Name + " (" + f.Length + $" bytes)\n";
            entity.size += f.Length;
        }
        temptext = entity.text;
        entity.text = "";
        foreach (DirectoryInfo d in diArr)
        {

            temp = RecursionSearch(d.FullName, counter + 1);
            entity.text += temp.text;
            entity.size += temp.size;
        }
        entity.text = entity.name + "(" + entity.size + $" bytes) " + "\n" + entity.text + temptext;
        return entity;
    }
    private Entity RecursionSearch(string path, int counter, bool humanread)
    {
        Entity entity = new Entity();
        Entity temp = new Entity();
        DirectoryInfo di = new DirectoryInfo(path);
        FileInfo[] fiArr = di.GetFiles();
        DirectoryInfo[] diArr = di.GetDirectories();
        if (counter == 0)
            entity.name += "<";
        entity.name += di.Name;
        for (int i = 0; i < counter; i++)
            entity.name = "-" + entity.name;
        if (counter == 0)
            entity.name += ">";
        string temptext = "";
        foreach (FileInfo f in fiArr)
        {
            Entity fileData = new Entity();
            fileData.name = "-";
            for (int i = 0; i < counter; i++)
                fileData.name += "-";
            fileData.name += f.Name;
            fileData.size = f.Length;
            fileData = Measure(fileData);
            entity.text += fileData.name;
            entity.size += fileData.size;
        }
        temptext = entity.text;
        entity.text = "";
        foreach (DirectoryInfo d in diArr)
        {
            temp = RecursionSearch(d.FullName, counter + 1);
            temp = Measure(temp);
            entity.text += temp.text;
            entity.size += temp.size;
        }

        entity = Measure(entity);
        entity.text = entity.name + entity.text + temptext;
        return entity;
    }

}
