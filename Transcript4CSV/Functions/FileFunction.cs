using System;
using System.Text;
using System.Text.Json;

using Transcript4CSV.Model;


namespace Transcript4CSV.Functions;
class FileFunction
{
    public static List<string> ReadFile(string path)     // Read the file
    {
        List<string> list = new List<string>();
        using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8")))
        {
            string? line = "";
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

    public static void WriteJSONFile(List<UtteranceData> datas, string path)
    {
        try 
        {
            // JSON serialization options. indent
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Convert to JSON string
            string jsonString = JsonSerializer.Serialize(datas, options);

            // Write the file in UTF-8
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.Write(jsonString);
            }

            if(StaticParameter.isDebugMode) {
                Console.WriteLine($"JSONデータが{path}に書き込まれました。");
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSONシリアライズエラー: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"ファイル書き込みエラー: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"予期せぬエラーが発生しました: {ex.Message}");
        }
    }
}