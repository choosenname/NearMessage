using Newtonsoft.Json;

namespace ConsoleClient;

class Program
{
    static void Main(string[] args)
    {
        Message msg = new();
        string json = JsonConvert.SerializeObject(msg);

        Console.WriteLine(json);

        var des = JsonConvert.DeserializeObject<Message>(json);
        Console.WriteLine(des);
    }
}