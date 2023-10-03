using System;
using System.Text;

using Transcript4CSV.Model;


namespace Transcript4CSV.Functions;
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

    public static void WriteCSVFile(List<UtteranceData> datas, string path, bool isHeader=true)
    {
        List<string> list = CommonFunction.ConvertCSVList(datas);
        try
        {
            StreamWriter sr = new StreamWriter(path, false, Encoding.UTF8);
            foreach (string str in list)
            {
                sr.WriteLine(str.Replace("\r\n", ""));
            }
            sr.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}