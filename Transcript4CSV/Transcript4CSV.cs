﻿using System;
using System.Text;
using System.Text.RegularExpressions;

using Transcript4CSV.Functions;
using Transcript4CSV.Model;

namespace Transcript4CSV;
public class TranscriptProcess
{
    public static readonly string version = "1.4.0";

    private static string vttFilePath = "";
    private static List<UtteranceData> utteranceDatas = new List<UtteranceData>();
    private static readonly WordFunction wordFunction = new WordFunction();

    public TranscriptProcess(string _vttFilePath)
    {
        vttFilePath = _vttFilePath;

        if(!CommonFunction.CheckExtensionVtt(vttFilePath))
        {
            throw new Exception("Only files with the extension vtt are valid.");;
        }
    }

    public void AddChangeWordList(string path)
    {
        wordFunction.AddChangeWordList(path);
    }

    public void AddChangeWordList(List<string> list)
    {
        wordFunction.AddChangeWordList(list);
    }

    public void WriteCSVFile(string outputPath, bool isHeader=true)
    {
        FileFunction.WriteCSVFile(utteranceDatas, outputPath, isHeader);
    }

    public List<string> GetCSVData(bool isHeader=false)
    {
        return CommonFunction.ConvertCSVList(utteranceDatas, isHeader);
    }

    public void MakeCSVList()
    {
        // Read vtt file
        List<string> vttList = FileFunction.ReadFile(vttFilePath);

        // Judge vtt file
        if(!CommonFunction.JudgeVttFile(vttList))
        {
            throw new Exception("The specified vttfile is not valid.");
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

        string text = CommonFunction.ConvertNewLineAndListString(list);
        text = text.Replace("WEBVTT", "");
        string lineKey = TypeCheckFunction.GetLineKey(text);

        Regex reg = new Regex(lineKey, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        for (Match m = reg.Match(text); m.Success; m = m.NextMatch())
        {
            string formatStr = wordFunction.Formatting(m.Groups["text"].Value);
            //string[] speaker = m.Groups["speaker"].Value.Split('/');
            modelList.Add(new UtteranceData { Speaker = m.Groups["speaker"].Value, Text = formatStr, StartDate = m.Groups["start"].Value, EndDate = m.Groups["end"].Value });
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
                if(utterance.Text == null)
                {
                    utterance.Text = "";
                }

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
                if (data.Text.Length >= 3)
                {
                    returnList.Add(data);
                }
            }
        }

        return returnList;
    }
}
