using System.Text.RegularExpressions;

namespace Transcript4CSV.Functions;
class WordFunction
{
    private readonly List<string> changeWordList = new List<string>();
    public WordFunction()
    {
        changeWordList = MakeChangeWordList();
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

    private List<string> MakeChangeWordList()
    {
        List<string> list = new List<string>
        {
            "でしょうか,か",
            "いたします、,する。",
            "いたします。,する。",
            "していましたが、,していたが、",
            "していました。,していた。",
            "できます。,できる。",
            "しました、,した。",
            "しました。,した。",
            "思います。,思う。",
            "します。,する。",
            "承知しました,承知した",
            "承知致しました,承知した",
            "承知いたしました,承知した",
            "大丈夫です,問題ない",
            "問題ありません,問題ない",
            "のところですね,ですね",
            "というところ,ところ",
            "ゆっていい,言っていい",
            "2位のタイミング,任意のタイミング",
            "まあちょっと。,",
            "まあちょっと、,",
            "まあちょっと。,",
            "ちょっとですね。,",
            "ちょっとですね、,",
            "ちょっとだけ。,",
            "ちょっとだけ、,",
            "ちょっとだけ,",
            "ちょっと。,",
            "ちょっと、,",
            "ちょっと,",
            "はいでは。,",
            "はいでは、,",
            "はいで。,",
            "はいで、,",
            "そうそう。,",
            "そうそう、,",
            "あえっと。,",
            "あえっと、,",
            "あえっと,",
            "ええっと？,",
            "ええっと。,",
            "ええっと、,",
            "ええっと,",
            "えっと。,",
            "えっと、,",
            "えっと,",
            "ええと。,",
            "ええと、,",
            "えっ、とー。,",
            "えっ、とー、,",
            "ええと,",
            "ええ？,",
            "ええ！,",
            "ええ。,",
            "ええ、,",
            "ええ,",
            "えー。,",
            "えー、,",
            "えー,",
            "え？,",
            "まあまあ。,",
            "まあまあ、,",
            "まあ。,",
            "まあ、,",
            "どうぞ。,",
            "どうぞ、,",
            "あはい。,",
            "あはい、,",
            "あはい,",
            "あのう。,",
            "あのう、,",
            "ました。,",
            "ました、,",
            "あの。,",
            "あの、,",
            "あれ。,",
            "あれ、,",
            "あぁ。,",
            "あぁ、,",
            "あー。,",
            "あー、,",
            "はい。,",
            "はい、,",
            "うん。,",
            "うん、,",
            "うーん。,",
            "うーん、,",
            "おい。,",
            "おい、,",
            "そう。,",
            "そう、,",
            "する。,",
            "する、,",
            "我、々、,我々",
            "我、々,我々",
            "我 々 ,我々",
            "我 々,我々",
            "色、々、,色々",
            "色、々,色々",
            "色 々 ,色々",
            "色 々,色々",
            "様、々、,様々",
            "様、々,様々",
            "様 々 ,様々",
            "様 々,様々",
            "長、々、,長々",
            "長、々,長々",
            "長 々 ,長々",
            "長 々,長々",
            "は？,",
            "あ。,",
            "あ、,",
            "ね。,",
            "ね、,",
            "、、,、",
            "パワーオートメイト,Power Automate",
            "パワーアップス,Power Apps",
            "アウトルック,Outlook",
            "シーオーツー,CO2",
            "リスツ,Lists",
            "異不分,if文",
            "腫瘍ベンダ,主要ベンダ",
            "内政力強化,内製力強化",
            "内政力,内製力",
            "多過し,押下し",
            "不10分,不十分"
        };
        //list.Add(",");
        return list;
    }
}