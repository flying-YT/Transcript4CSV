using System;
using System.Text;

using Transcript4CSV.Model;


namespace Transcript4CSV.Functions;
class CommonFunction
{
    public static bool JudgeVttFile(List<string> list)
    {
        string vttString = "WEBVTT";
        if(list[0].Contains(vttString))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool JudgeVttType(List<string> list)
    {
        StringBuilder sb = new StringBuilder();
        foreach(string data in list)
        {
            sb.Append(data);
        }

        if(sb.ToString().Contains("</v>"))
        {
            return true;
        }
        else
        {
            return false;
        }
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
        foreach(string data in list)
        {
            sb.Append(data + "\r\n");
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