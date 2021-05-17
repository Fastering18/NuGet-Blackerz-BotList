using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlackerzDotNet
{
    public class BotData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public int Upvotes { get; set; }
        public owner Owner { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string Avatar { get; set; }
        public string Prefix { get; set; }
        public string InviteLink { get; set; }
        private string inviteLink { get { return InviteLink; } set { InviteLink = value; } }
        private string prefix { get { return Prefix; } set { Prefix = value; } }
        private string avatar { get { return Avatar; } set { Avatar = value; } }
        private string name { get { return name; } set { Name = value; } }
        private string id { get { return Id; } set { Id = value; } }
        private string tag { get { return Tag; } set { Tag = value; } }
        private int upvotes { get { return Upvotes; } set { Upvotes = value; } }
        private owner owner { get { return Owner; } set { Owner = value; } }

        public string GetInviteLink()
        {
            return inviteLink == "" ? ("https://discord.com/oauth2/authorize?client_id=" + id + "&permissions=52288&scope=bot") : inviteLink;
        }
    }

    public class owner
    {
        public string Name { get; set; }
        public string Id { get; set; }
        private string name { get { return name; } set { Name = value; } }
        private string id { get { return Id; } set { Id = value; } }
    }

    public class BlackerzBot
    {
        private readonly static string _baseURL = "https://blackerz.herokuapp.com/";
        static HttpClient client = new HttpClient();

        static BlackerzBot()
        {
            client.BaseAddress = new Uri(BlackerzBot._baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static async Task<BotData> GetBotData(int botid)
        {
            BotData botData = default(BotData);
            client.BaseAddress = new Uri(BlackerzBot._baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("api/v1/bots/" + botid.ToString());
            if (response.IsSuccessStatusCode)
            {
                botData = await response.Content.ReadAsAsync<BotData>();
            }
            return botData;
        }
        public static async Task<BotData> GetBotData(string botid)
        {
            BotData botData = default(BotData);
            
            HttpResponseMessage response = await client.GetAsync("api/v1/bots/" + botid);
            if (response.IsSuccessStatusCode)
            {
                botData = await response.Content.ReadAsAsync<BotData>();
            }
            return botData;
        }
        public static string AvatarOf(string id, string avatar, string format="")
        {
            return "https://cdn.discordapp.com/avatars/" + id + "/" + avatar + format;
        }
        public static string AvatarOf(BotData botData, string format = "")
        {
            return "https://cdn.discordapp.com/avatars/" + botData.Id + "/" + botData.Avatar + format;
        }
        public static string GetInviteLink(BotData bot, int permission=52288)
        {
            return "https://discord.com/oauth2/authorize?client_id=" + bot.Id + "&permissions=" + permission.ToString() + "&scope=bot";
        }
        public static string GetInviteLink(string clientId, int permission=52288)
        {
            return "https://discord.com/oauth2/authorize?client_id=" + clientId + "&permissions=" + permission.ToString() + "&scope=bot";
        }
    }
}
