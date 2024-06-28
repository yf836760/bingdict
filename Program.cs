using HtmlAgilityPack;
using System.Text;
using Trans.Model;

Console.OutputEncoding = Encoding.UTF8; //使console可以正常输出音标
        
string words = string.Empty;
if (args.Length == 0 || !IsValidEnglishWord(String.Join(" ", args).Trim()))
{
    Console.WriteLine("包含非英文字符，或字符数超过100");
    return;
}
string bingDictURL = "https://cn.bing.com/dict/search?q=";
var url = $@"{bingDictURL}{words}";
HttpClient client = new HttpClient();
HttpResponseMessage response = await client.GetAsync(url);
response.EnsureSuccessStatusCode();
string html =await response.Content.ReadAsStringAsync();

await using var db = new WordContext();
try
{
    var word = db.Words
             .Where(b => b.PreTrans == words)
             .FirstOrDefault();
    if (word is not null)
    {
        word.Frequency += 1;
        await db.SaveChangesAsync();

        string difi = word.Transed;
        string difiNewLine = ProcessNewLine($"{difi}");
        Console.WriteLine($"{difiNewLine}");
    } 
    else
    {
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);
        //单词的匹配
        HtmlNode meta = doc.DocumentNode.SelectSingleNode("//head/meta[@name='description']");
        if (meta is null) return;
        string content = meta.GetAttributeValue("content", "词典");
        if (content == "词典" || content.StartsWith("为您提供")) return;
        int posOfComma = content.IndexOf("，");
        string outPut = content.Substring(posOfComma + 1);
        //这里为了处理SnipDo的内容输出而要进行换行处理
        string outputNewLine = ProcessNewLine(outPut);
        Console.WriteLine($"{outputNewLine}");
        //处理完
        db.Words.Add(new Word { PreTrans = words, Transed = outPut, Frequency = 1});
        await db.SaveChangesAsync();
    
    }  
}
catch (Exception ex)
{
    Console.WriteLine("发生错误" + ex.ToString());
    return;
}

static bool IsValidEnglishWord(string word) => word.Length <= 100 && IsEnglish(word);
static string ProcessNewLine(string inputString)
{
    
    string[] searchStrings = ["网络释义", "adj.", "adv.", "un.", "n.", "num.", "v." ];
    string[] ignoreStrings = ["dv."];
    //string searchChar = "]，"
    foreach (string searchString in searchStrings)
    {
        if (inputString.Contains(searchString))
        {
            int index = inputString.IndexOf(searchString);
            
            if (index > 0 && inputString[index - 1] != 'd' && inputString[index - 1] != 'u')
            {
                inputString = inputString.Insert(index, Environment.NewLine);
            }
        }
    }
    return inputString;
}
static bool IsEnglish(string input) => input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-');
