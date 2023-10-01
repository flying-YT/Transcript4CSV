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

        // Sort speaker to List
        UtteranceDataList = SortSpeakerToList(UtteranceData);
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

    private static List<UtteranceData> SortSpeakerToList(List<UtteranceData> list)
    {
        List<UtteranceData> modelList = new List<UtteranceData>();

        UtteranceData utterance = new UtteranceData();
        foreach(UtteranceData data in list)
        {
            if(utterance.Speaker == data.Speaker)
            {
                if(utterance.Text <= 512)
                {
                    utterance.Text += data.Text;
                    utterance.EndFate = data.EndDate;
                }
                else
                {
                    modelList.Add(utterance);
                    utterance = data;
                }
            }
            else
            {
                mdeol.Add(utterance);
                utterance = data;
            }
        }

        List<UtteranceData> returnList = new List<UtteranceData>();
        foreach(UtteranceData data in modelList)
        {
            if(data.Text != "")
            {
                returnList.Add(data);
            }
        }

        return returnList;
    }
}
