using System.Net;
using System.Web;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MapleAPI
{
    public class MapleApi
    {
        string ApiKey;
        public MapleApi(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        private string RespAsString(ref HttpWebRequest req)
        {
            string responseText;
            using (WebResponse response = req.GetResponse())
            {        
                Stream respStream = response.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                }
            }
            return responseText.ToString();
        }

        private HttpWebRequest SetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("x-nxopen-api-key", this.ApiKey);
            return request;
        }

        public string GetOcid(string nickname)
        {
            string encodeName = HttpUtility.UrlEncode(nickname, Encoding.UTF8);
            string url = $"https://open.api.nexon.com/maplestory/v1/id?character_name={encodeName}";
            HttpWebRequest request = this.SetRequest(url);

            string resp = this.RespAsString(ref request);
            JObject jsonResp = JObject.Parse(resp);
            return jsonResp.GetValue("ocid").ToString();
        }
        public string GetOguilid(string guildname, string worldname)
        {
            string encodeguild = HttpUtility.UrlEncode(guildname, Encoding.UTF8);
            string encodeworld = HttpUtility.UrlEncode(worldname, Encoding.UTF8);
            string url = $"https://open.api.nexon.com/maplestory/v1/guild/id?guild_name={encodeguild}&world_name={encodeworld}";
            HttpWebRequest request = this.SetRequest(url);

            string resp = this.RespAsString(ref request);
            JObject jsonResp = JObject.Parse(resp);
            return jsonResp.GetValue("oguild_id").ToString();
        }
        public JObject getCharData(string ocid, string date, string dataType)
        {
            /*
            [date]
            "YYYY-MM-DD" format string, after 2023-12-21, before a day ago
            
            [dataType]
            basic, popularity, stat, hyer-stat, propensity, ability, item-equiment,
            cashitem-equipment, symbol-equipment, set-effect, beauty-equipment, pet-equipment,
            skill, link-skill, vmatrix, hexametrix, hexametrix-stat, dojang
            */
            string url = $"https://open.api.nexon.com/maplestory/v1/character/{dataType}?ocid={ocid}&date={date}";
            HttpWebRequest request = this.SetRequest(url);
            string resp = this.RespAsString(ref request);
            JObject jsonResp = JObject.Parse(resp);
            return jsonResp;
        }
        public JObject getGuildData(string oguildid, string date, string dataType)
        {
            /*
            [date]
            "YYYY-MM-DD" format string, after 2023-12-21, before a day ago
            
            [dataType]
            basic
            */
            string url = $"https://open.api.nexon.com/maplestory/v1/guild/{dataType}?oguild_id={oguildid}&date={date}";
            HttpWebRequest request = this.SetRequest(url);
            string resp = this.RespAsString(ref request);
            JObject jsonResp = JObject.Parse(resp);
            return jsonResp;
        }
        public JObject getUnionData(string ocid, string date, string dataType)
        {
            /*
            [date]
            "YYYY-MM-DD" format string, after 2023-12-21, before a day ago
            
            [dataType]
            union, union-raider
            */
            string url = $"https://open.api.nexon.com/maplestory/v1/user/{dataType}?ocid={ocid}&date={date}";
            HttpWebRequest request = this.SetRequest(url);
            string resp = this.RespAsString(ref request);
            JObject jsonResp = JObject.Parse(resp);
            return jsonResp;
        }
    }

}