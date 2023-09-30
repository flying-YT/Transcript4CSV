namespace Functions;
class wordFunction
{
    private readonly List<string> changeWordList = new List<string>();
    public WordFunction()
    {
        changeWordList = FMakeChangeWordList();
    }

    public string Formatting(string text)
    {
        string formattingText = text;
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
        list.Add("あえっと。,");
        list.Add("あえっと、,");
        list.Add("あえっと,");
        list.Add(",");
        list.Add(",");
        return list;
    }
}