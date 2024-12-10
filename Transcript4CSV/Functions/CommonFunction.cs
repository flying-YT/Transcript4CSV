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

    // Convert List<string> to string including line breaks
    public static string ConvertNewLineAndListString(List<string> list)
    {
        StringBuilder sb = new StringBuilder();
        // If it includes a V tag, add a line break as is.
        if(JudgeVttType(list))
        {
            foreach(string data in list)
            {
                sb.Append(data + "\r\n");
            }
        }
        // Consider the possibility that the recognized text has line breaks
        else
        {
            bool inCaptionBlock = false;
            foreach(string data in list)
            {
                if (data.Contains("-->"))
                {
                    // Start of caption block
                    sb.Append(data + "\r\n");
                    inCaptionBlock = true;
                }
                else if (string.IsNullOrWhiteSpace(data))
                {
                    // Empty lines are treated as new lines
                    sb.Append("\r\n");
                    inCaptionBlock = false; // End caption block with blank line
                }
                else if (inCaptionBlock)
                {
                    // Text within caption block
                    sb.Append(data);
                }
                else
                {
                    // Line before caption block starts (ex: WEBVTT)
                    sb.Append(data + "\r\n");
                }
            }
        }

        Console.WriteLine(sb.ToString());
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