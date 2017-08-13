using Discord;
using Discord.WebSocket;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VentTrent
{
    class Program
    {
        private DiscordSocketClient client;

        static void Main(string[] args)

        => new Program().MainAsync().GetAwaiter().GetResult();


        public async Task MainAsync()
        {
            client = new DiscordSocketClient();

            client.Log += Log;

            client.MessageReceived += MessageReceived;

            string token = "TOKEN"; // Remember to keep this private!
            await client.LoginAsync(TokenType.Bot, token);
            for (int i = 0; i < loadSlurList().Length; i++)
            {
                Console.WriteLine(loadSlurList()[i] + "s");
                Console.WriteLine(loadSlurList()[i]);
            }
            Console.WriteLine(loadSlurList());
            await client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        public static string[] loadSlurList()
        {
            string[] slurList = System.IO.File.ReadAllLines(@"BANNED WORDS TEXT FILE LOCATION");
            return (slurList);
        }
        
        public static string[] loadWatchList()
        {
            string[] watchList = System.IO.File.ReadAllLines(@"WATCHED WORD LIST TEXT FILE LOCATION");
            return (watchList);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private async Task MessageReceived(SocketMessage message)
        {
            string censoredMessage = message.Content;
            if (message.Channel is SocketDMChannel && message.Author.Id != BOT ID)
            {
                for (int i = 0; i < loadSlurList().Length; i++)
                {
                    censoredMessage = Regex.Replace(censoredMessage, loadSlurList()[i], "#########", RegexOptions.IgnoreCase);
                }
                
                for (int i = 0; i < loadWatchList().Length; i++)
                {
                    if (message.Content.ToLower().Contains(loadWatchList()[i]) && (message.Channel is SocketDMChannel))
                    {
                        await message.Channel.SendMessageAsync("ANTI SUICIDE MESSAGE");
                    }
                }

                var eb = new EmbedBuilder()
                {
                    Title = "Anonymous: ",
                    Color = new Color(R, G, B),
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = $"Vent Reznor",
                        IconUrl = "ICON URL"
                    }
                };
                eb.WithDescription(censoredMessage);
                await ((SocketTextChannel)
                client.GetChannel(VENT CHANNEL)).SendMessageAsync("", false, eb);
            }
            
        }



    }
}
