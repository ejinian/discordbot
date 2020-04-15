using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace botmememachine
{
    
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;
        Random rand;
        string[] freshestMemes;

        public MyBot()
        {

            rand = new Random();
            freshestMemes = new string[]
            {
                "pictures/block.jpg",
                "pictures/expand.png",
                "pictures/lobby.jpg",
                "pictures/mmememe.jpg",
                "pictures/unknown.png"
            };

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '~';
                x.AllowMentionPrefix = true;
            });
            
            commands = discord.GetService<CommandService>();

            RegisterMemeCommand();

            commands.CreateCommand("urdum").Do(async (e) =>
            {
                await e.Channel.SendMessage("dond use bad words blease");
            });

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MjY0ODg3NzkwNDkwMjIyNjEz.C0nHrw.rtLwwHRkWAmUysO-IVUjfv4Qjd4", TokenType.Bot);
            });
        }

        private void RegisterMemeCommand()
        {
            commands.CreateCommand("meme")
                .Do(async (e) =>
            {
                int randomMemeIndex = rand.Next(freshestMemes.Length);
                string memeToPost = freshestMemes[randomMemeIndex];
                await e.Channel.SendFile(memeToPost);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
