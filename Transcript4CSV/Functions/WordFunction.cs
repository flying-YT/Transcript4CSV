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

        Regex reg = new Regex("(?<endChar>[^A-Za-z]) (?<startChar>[^A-Za-z])");
        for (Match m = reg.Match(formattingText); m.Success; m = m.NextMatch())
        {
            if(m.Groups["endChar"].Value == "す" || m.Groups["endChar"].Value == "た" || m.Groups["endChar"].Value == "る")
            {
                formattingText = formattingText.Replace(m.Value, m.Groups["endChar"]  + "。" + m.Groups["startChar"]);
            }
            else
            {
                formattingText = formattingText.Replace(m.Value, m.Groups["endChar"]  + "、" + m.Groups["startChar"]);
            }
        }
        char result = formattingText[formattingText.Length - 1];
        if(result == 'す' || result == 'た' || result == 'る')
        {
            formattingText += "。";
        }
        else if(result != '。' && result != '、')
        {
            formattingText += "、";
        }

        foreach (string change in changeWordList)
        {
            string[] changeArray = change.Split(',');
            if (text.Contains(changeArray[0]))
            {
                formattingText = formattingText.Replace(changeArray[0], changeArray[1]);
            }
        }
        return formattingText;
    }

    private List<string> MakeChangeWordList()
    {
        List<string> list = new List<string>();
        list.Add("でしょうか,か");
        list.Add("いたします、,する。");
        list.Add("いたします。,する。");
        list.Add("していましたが、,していたが、");
        list.Add("していました。,していた。");
        list.Add("できます。,できる。");
        list.Add("しました、,した。");
        list.Add("しました。,した。");
        list.Add("思います。,思う。");
        list.Add("します。,する。");
        list.Add("承知しました,承知した");
        list.Add("承知致しました,承知した");
        list.Add("承知いたしました,承知した");
        list.Add("大丈夫です,問題ない");
        list.Add("問題ありません,問題ない");
        list.Add("のところですね,ですね");
        list.Add("というところ,ところ");
        list.Add("ゆっていい,言っていい");
        list.Add("2位のタイミング,任意のタイミング");
        list.Add("まあちょっと。,");
        list.Add("まあちょっと、,");
        list.Add("まあちょっと。,");
        list.Add("ちょっとですね。,");
        list.Add("ちょっとですね、,");
        list.Add("ちょっとだけ。,");
        list.Add("ちょっとだけ、,");
        list.Add("ちょっとだけ,");
        list.Add("ちょっと。,");
        list.Add("ちょっと、,");
        list.Add("ちょっと,");
        list.Add("はいでは。,");
        list.Add("はいでは、,");
        list.Add("はいで。,");
        list.Add("はいで、,");
        list.Add("そうそう。,");
        list.Add("そうそう、,");
        list.Add("あえっと。,");
        list.Add("あえっと、,");
        list.Add("あえっと,");
        list.Add("ええっと？,");
        list.Add("ええっと。,");
        list.Add("ええっと、,");
        list.Add("ええっと,");
        list.Add("えっと。,");
        list.Add("えっと、,");
        list.Add("えっと,");
        list.Add("ええと。,");
        list.Add("ええと、,");
        list.Add("えっ、とー。,");
        list.Add("えっ、とー、,");
        list.Add("ええと,");
        list.Add("ええ？,");
        list.Add("ええ！,");
        list.Add("ええ。,");
        list.Add("ええ、,");
        list.Add("ええ,");
        list.Add("えー。,");
        list.Add("えー、,");
        list.Add("えー,");
        list.Add("え？,");
        list.Add("まあまあ。,");
        list.Add("まあまあ、,");
        list.Add("まあ。,");
        list.Add("まあ、,");
        list.Add("どうぞ。,");
        list.Add("どうぞ、,");
        list.Add("あはい。,");
        list.Add("あはい、,");
        list.Add("あはい,");
        list.Add("あのう。,");
        list.Add("あのう、,");
        list.Add("あの。,");
        list.Add("あの、,");
        list.Add("あれ。,");
        list.Add("あれ、,");
        list.Add("あぁ。,");
        list.Add("あぁ、,");
        list.Add("あー。,");
        list.Add("あー、,");
        list.Add("はい。,");
        list.Add("はい、,");
        list.Add("うん。,");
        list.Add("うん、,");
        list.Add("うーん。,");
        list.Add("うーん、,");
        list.Add("おい。,");
        list.Add("おい、,");
        list.Add("そう。,");
        list.Add("そう、,");
        list.Add("あ。,");
        list.Add("あ、,");
        list.Add("ね。,");
        list.Add("ね、,");
        list.Add("パワーオートメイト,Power Automate");
        list.Add("パワーアップス,Power Apps");
        list.Add("リスツ,Lists");
        list.Add("異不分,if文");
        list.Add("多過し,押下し");
        //list.Add(",");
        return list;
    }
}