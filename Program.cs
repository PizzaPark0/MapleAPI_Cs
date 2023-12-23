using MapleAPI;
using Newtonsoft.Json.Linq;
public class MapleApiViewer
{
    public static void Main()
    {
        StreamReader sr = new StreamReader("config.txt");
        string text = sr.ReadLine().Split(':')[1].Trim();
        sr.Close();
        
        MapleApi m = new MapleApi(text);
        string ocid = m.GetOcid("이름");
        string ogid = m.GetOguilid("길드", "월드");
        Console.WriteLine(ocid);
        Console.WriteLine(ogid);

        JObject cData = m.getCharData(ocid, "2023-12-22", "stat");
        JObject gData = m.getGuildData(ogid, "2023-12-22", "basic");
        JObject uData = m.getUnionData(ocid, "2023-12-22", "union-raider");
        Console.WriteLine(uData);
    }
}