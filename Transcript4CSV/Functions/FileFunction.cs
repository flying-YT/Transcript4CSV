namespace Functions;
class FileFunction
{
    public static List<string> ReadFile(string path)     // Read the file
    {
        List<string> list = new List<string>();
        using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8")))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }
        }
        return list;
    }
}