namespace Functions;
class CommonFunction
{
    public static bool JudgeVttFile(List<string> list)
    {
        readonly string vttString = "WEBVTT";
        if(list[0].Contains(vttString))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool JudgeVttType(List<string> list)
    {
        StringBuilder sb = new StringBuilder();
        foreach(string data in list)
        {
            sb.Append(data);
        }

        if(sb.ToString().Contains("</v>"))
        {
            return true;
        }
        else
        {
            return false;
        }
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
}