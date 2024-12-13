using System.Text.RegularExpressions;

using Transcript4CSV.Parameters;

namespace Transcript4CSV.Functions;
class WordFunction
{
    private readonly List<string> changeWordList = new List<string>();
    
    public WordFunction()
    {
        changeWordList = StaticParameter.changeWordList;
    }

    public void AddChangeWordList(string path)
    {
        List<string> list = FileFunction.ReadFile(path);
        changeWordList.AddRange(list);
    }

    public void AddChangeWordList(List<string> list)
    {
        changeWordList.AddRange(list);
    }

    public string Formatting(string text)
    {
        if(text == null) {
            return "";
        }

        string formattingText = text;

        formattingText = AddPunctuationBetweenJapaneseAndJapanese(formattingText);

        char result = formattingText[formattingText.Length - 1];
        if(result == 'す' || result == 'た' || result == 'る')
        {
            formattingText += "。";
        }
        else if(result != '。' && result != '、')
        {
            formattingText += "、";
        }

        formattingText = RemovePeriodBetweenJapaneseAndNumbers(formattingText);

        foreach (string change in changeWordList)
        {
            string[] changeArray = change.Split(',');
            if (text.Contains(changeArray[0]))
            {
                formattingText = formattingText.Replace(changeArray[0], changeArray[1]);
            }
        }

        return RemoveLastPeriod(formattingText);
    }

    private static string RemoveLastPeriod(string text)
    {
        if(text.Length <= 0)
        {
            return text;
        }

        if(text[text.Length - 1] == '、')
        {
            return text.Substring(0, text.Length - 1);
        }
        else
        {
            return text;
        }
    }

    private static string AddPunctuationBetweenJapaneseAndJapanese(string text)
    {
        string str = text;
        Regex reg = new Regex("(?<endChar>[^A-Za-z]) (?<startChar>[^A-Za-z])");
        for (Match m = reg.Match(str); m.Success; m = m.NextMatch())
        {
            if(m.Groups["endChar"].Value == "す" || m.Groups["endChar"].Value == "た" || m.Groups["endChar"].Value == "る")
            {
                str = str.Replace(m.Value, m.Groups["endChar"]  + "。" + m.Groups["startChar"]);
            }
            else
            {
                str = str.Replace(m.Value, m.Groups["endChar"]  + "、" + m.Groups["startChar"]);
            }
        }
        return str;
    }

    private static string RemovePeriodBetweenJapaneseAndNumbers(string text)
    {
        string str = text;
        Regex reg2 = new Regex("(?<endChar>[0-9])、(?<startChar>[^A-Za-z])");
        for (Match m = reg2.Match(str); m.Success; m = m.NextMatch())
        {
            str = str.Replace(m.Value, m.Groups["endChar"]  + "" + m.Groups["startChar"]);
        }

        Regex reg3 = new Regex("(?<endChar>[^A-Za-z])、(?<startChar>[0-9])");
        for (Match m = reg3.Match(str); m.Success; m = m.NextMatch())
        {
            str = str.Replace(m.Value, m.Groups["endChar"]  + "" + m.Groups["startChar"]);
        }
        return str;
    }
}