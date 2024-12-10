using System;
using System.Text;

using Transcript4CSV.Model;


namespace Transcript4CSV.Functions;
class CommonFunction
{
    public static bool CheckExtensionVtt(string fileName)
    {
        Console.WriteLine(Path.GetExtension(fileName));
        return Path.GetExtension(fileName) == ".vtt";
    }

    public static bool JudgeVttFile(List<string> list)
    {
        string vttString = "WEBVTT";
        return list[0].Contains(vttString);
    }

    public static bool JudgeVttType(List<string> list)
    {
        StringBuilder sb = new StringBuilder();
        foreach(string data in list)
        {
            sb.Append(data);
        }
        return sb.ToString().Contains("</v>");
    }

    public static string ListToString(List<string> list)
    {
        StringBuilder sb = new StringBuilder();
        foreach(string data in list)
        {
            sb.Append(data);
        }
        return sb.ToString();
    }

    public static string ConvertNewLineAndListString(List<string> list)
    {
        StringBuilder sb = new StringBuilder();
        if(JudgeVttType(list))
        {
            foreach(string data in list)
            {
                sb.Append(data + "\r\n");
            }
        }
        else
        {
            int count = 0;
            foreach(string data in list)
            {
                if(count == 0)
                {
                    sb.Append(data + "\r\n");
                    if(data.Contains("-->"))
                    {
                        count = 1;
                    }
                }
                else if(count == 1)
                {
                    sb.Append(data);
                    count++;
                }
                else if(count >= 2)
                {
                    if(data == "")
                    {
                        sb.Append("\r\n");
                        count = 0;
                    }
                    else
                    {
                        sb.Append(" " + data);
                        count++;
                    }
                }
            }
        }

        return sb.ToString();
    }

    public static List<string> ConvertCSVList(List<UtteranceData> datas, bool isHeader=true)
    {
        List<string> list = new List<string>();

        if(isHeader)
        {
            list.Add(string.Format("{0},{1},{2},{3}", "Speaker", "Text", "StartTime", "EndTime"));
        }

        foreach(UtteranceData data in datas)
        {
            list.Add(string.Format("{0},{1},{2},{3}", data.Speaker, data.Text, data.StartDate, data.EndDate));
        }

        return list;
    }
}