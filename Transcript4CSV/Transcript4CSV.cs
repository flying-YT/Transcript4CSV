using Functions;

namespace Transcript4CSV;
public class Transcript4CSV
{
    public static void MakeCSV(string path)
    {
        // Read vtt file
        List<string> vttList = FileFunction.ReadFile(path);

        // Judge vtt file
        if(!CommonFunction.JudgeVttFile(vttList))
        {
            return false;
        }

        // Judge vtt type
        bool isVtagType = CommonFunction.JudgeVttType(vttList);

        // Make UtteranceDataList
        List<UtteranceData> UtteranceDataList = MakeUtteranceDataList(vttList, isVtagType);
    }

    private static List<UtteranceData> MakeUtteranceDataList(List<string> list, bool _isVtagType)
    {
        List<UtteranceData> modelList = new List<UtteranceData>();
        WordFunction wordFunction = new WordFunction();

        string str = CommonFunction.ListToString(list);
        string lineKey = "";
        if(_isVtagType)
        {
            lineKey = @"(?<start>(\d{1,2}:\d{1,2}:\d{1,2}.\d{1,3})*?)" + " --> " + @"(?<end>(\d{1,2}:\d{1,2}:\d{1,2}.\d{3})*?)\r\n" + "<v (?<speaker>.*?)>(?<text>.*?)</v>";
        }
        else
        {
            lineKey = @"(?<speaker>.*?)\r\n" + @"(?<start>(\d{1,2}:\d{1,2}:\d{1,2}.\d{1,3})*?)" + " --> " + @"(?<end>(\d{1,2}:\d{1,2}:\d{1,2}.\d{3})*?)\r\n" + @"(?<text>.*?)\r\n";
        }
        Regex reg = new Regex(lineKey, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        for (Match m = reg.Match(text); m.Success; m = m.NextMatch())
        {
            string formatStr = wordFunction.Formatting(m.Groups["text"].Value);
            utteranceDatas.Add(new UtteranceData { Speaker = m.Groups["speaker"].Value, Text = formatStr, StartDate = m.Groups["start"].Value, EndDate = m.Groups["end"].Value });
        }

        return modelList;
    }
}
