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
            CommonFunction.ConsoleWrite(e.Message);
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

            CommonFunction.ConsoleWrite($"JSONデータが{path}に書き込まれました。");
        }
        catch (JsonException ex)
        {
            CommonFunction.ConsoleWrite($"JSONシリアライズエラー: {ex.Message}");
        }
        catch (IOException ex)
        {
            CommonFunction.ConsoleWrite($"ファイル書き込みエラー: {ex.Message}");
        }
        catch (Exception ex)
        {
            CommonFunction.ConsoleWrite($"予期せぬエラーが発生しました: {ex.Message}");
        }
    }
}