using System;
using System.Text;
using System.Text.RegularExpressions;

using Transcript4CSV.Model;

namespace Transcript4CSV.Functions;
class TypeCheckFunction
{
    private static List<string> keyList = new List<string>() 
    { 
        @"(?<start>(\d{1,2}:\d{1,2}:\d{1,2}.\d{1,3})*?)" + " --> " + @"(?<end>(\d{1,2}:\d{1,2}:\d{1,2}.\d{3})*?)\r\n" + "<v (?<speaker>.*?)>(?<text>.*?)</v>",
        @"(?<speaker>^[^\r\n]*$)\r\n" + @"(?<start>\d{1,2}:\d{2}(:\d{2})?\.\d{1,3})\s*-->\s*(?<end>\d{1,2}:\d{2}(:\d{2})?\.\d{1,3})\s*\r\n(?<text>.*?)\r\n",
        @"(?<start>\d{1,2}:\d{2}(:\d{2})?\.\d{1,3})\s*-->\s*(?<end>\d{1,2}:\d{2}(:\d{2})?\.\d{1,3})\s*\r\n(?<text>.*?)\r\n",
        @"(?<start>\d{1,2}:\d{2}(:\d{2})?\.\d{1,3})\s*-->\s*(?<end>\d{1,2}:\d{2}(:\d{2})?\.\d{1,3})\s*\n(?<text>.*?)\n"
    };

    public static string GetLineKey(string text)
    {
        string lineKey = "";
        foreach(string str in keyList)
        {
            Regex reg = new Regex(str, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = reg.Match(text); 
            if(m.Success)
            {
                lineKey = str;
                break;
            }
        }
        System.Console.WriteLine("linekey:" + lineKey);
        return lineKey;
    }
}