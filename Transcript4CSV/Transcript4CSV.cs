using System;
using System.Text;
using System.Text.RegularExpressions;

using Transcript4CSV.Functions;
using Transcript4CSV.Model;

namespace Transcript4CSV;
public class TranscriptProcess
{
    private static string vttFilePath = "";
    private static List<UtteranceData> utteranceDatas = new List<UtteranceData>();
    private static WordFunction wordFunction = new WordFunction();

    public TranscriptProcess(string _vttFilePath)
    {
        vttFilePath = _vttFilePath;
        MakeCSV();
    }

    public void AddChangeWordList(string path)
    {
        wordFunction.AddChangeWordList(path);
    }

    public void WriteCSVFile(string outputPath, bool isHeader=true)
    {
        FileFunction.WriteCSVFile(utteranceDatas, outputPath, isHeader);
    }

    public List<string> GetCSVData(bool isHeader=false)
    {
        CommonFunction.ConvertCSVList(utteranceDatas, isHeader);
    }

    private static void MakeCSV()
    {
        // Read vtt file
        List<string> vttList = FileFunction.ReadFile(vttFilePath);

        // Judge vtt file
        if(!CommonFunction.JudgeVttFile(vttList))
        {
            throw new Exception("The specified vttfile is not valid.");;
        }

        // Judge vtt type
        bool isVtagType = CommonFunction.JudgeVttType(vttList);

        // Make UtteranceDataList
        List<UtteranceData> utteranceDataList = MakeUtteranceDataList(vttList, isVtagType);

        // Sort speaker to List
        utteranceDatas = SortSpeakerToList(utteranceDataList);
    }

    private static List<UtteranceData> MakeUtteranceDataList(List<string> list, bool _isVtagType)
    {
        List<UtteranceData> modelList = new List<UtteranceData>();

        string text = "";
        string lineKey = "";
        if(_isVtagType)
        {
            text = CommonFunction.ListToString(list);
            lineKey = @"(?<start>(\d{1,2}:\d{1,2}:\d{1,2}.\d{1,3})*?)" + " --> " + @"(?<end>(\d{1,2}:\d{1,2}:\d{1,2}.\d{3})*?)" + "<v (?<speaker>.*?)>(?<text>.*?)</v>";
        }
        else
        {
            text = CommonFunction.ConvertNewLineAndListtiString(list);
            text = text.Replace("WEBVTT", "");
            lineKey = @"(?<speaker>.*?)\r\n" + @"(?<start>(\d{1,2}:\d{1,2}:\d{1,2}.\d{1,3})*?)" + " --> " + @"(?<end>(\d{1,2}:\d{1,2}:\d{1,2}.\d{3})*?)\r\n" + @"(?<text>.*?)\r\n";
        }

        Regex reg = new Regex(lineKey, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        for (Match m = reg.Match(text); m.Success; m = m.NextMatch())
        {
            string formatStr = wordFunction.Formatting(m.Groups["text"].Value);
            string[] speaker = m.Groups["speaker"].Value.Split('/');
            modelList.Add(new UtteranceData { Speaker = speaker[0], Text = formatStr, StartDate = m.Groups["start"].Value, EndDate = m.Groups["end"].Value });
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
                if(utterance.Text.Length <= 512)
                {
                    utterance.Text += data.Text;
                    utterance.EndDate = data.EndDate;
                }
                else
                {
                    modelList.Add(utterance);
                    utterance = data;
                }
            }
            else
            {
                modelList.Add(utterance);
                utterance = data;
            }
        }
        modelList.Add(utterance);

        List<UtteranceData> returnList = new List<UtteranceData>();
        foreach(UtteranceData data in modelList)
        {
            if(data.Text != null)
            {
                if(data.Text.Length != 0)
                {
                    returnList.Add(data);
                }
            }
        }

        return returnList;
    }
}
