﻿using System;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;
using MidoriBot.Events;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MidoriBot
{
    public class Midori
    {
        public static DiscordSocketClient MidoriClient;
        public static string Version = "1.0";
        public static DiscordSocketConfig MidoriSocketConfig = new DiscordSocketConfig
        {
            DownloadUsersOnGuildAvailable = true,
            LogLevel = LogSeverity.Info,
            MessageCacheSize = 10
        };
        public static CommandService MidoriCommands;
        public static Dictionary<string, object> MidoriConfig;
        public static Dictionary<string, object> MidoriCredentials;
        public static CommandServiceConfig MidoriCommandsConfig = new CommandServiceConfig
        {
            DefaultRunMode = RunMode.Sync
        };
        public static MidoriHandler CommandHandler;

        public static string GetDescription()
        {
            return (Midori.MidoriClient.GetApplicationInfoAsync().GetAwaiter().GetResult()).Description;
        }

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Attempting to hand over control to async...");
                Midori.Start().GetAwaiter().GetResult();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Failed to complete handover.");
                Console.WriteLine($"Cannot connect to Discord! D:");
                Console.WriteLine($"Exception details:");
                Console.WriteLine(e);
            }
        }



        public static async Task Start()
        {
            MidoriClient = new DiscordSocketClient(MidoriSocketConfig);
            MidoriCommands = new CommandService(MidoriCommandsConfig);
            CommandHandler = new MidoriHandler();
            Console.WriteLine("Handover success.");
            Console.WriteLine("Created client, command service and command handler.");
            try
            {
                // Import JSON
                StreamReader RawOpen = File.OpenText(@"./midori_config.json");
                StreamReader RawCredentialsOpen = File.OpenText(@"./credentials.json");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("You dumb shit ! I can't find midori_config.json. Follow this guide and try again: https://github.com/lofdat/Midori/blob/master/README.md");
                Environment.FailFast("cannot find midori_config.json");
                await Task.Delay(-1);
            }
            StreamReader Raw = File.OpenText(@"./midori_config.json");
            StreamReader RawCredentials = File.OpenText(@"./credentials.json");
            JsonTextReader TextReader = new JsonTextReader(Raw);
            JsonTextReader CredTextReader = new JsonTextReader(RawCredentials);
            JObject MidoriJConfig = (JObject)JToken.ReadFrom(TextReader);
            JObject MidoriCred = (JObject)JToken.ReadFrom(CredTextReader);
            MidoriConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(MidoriJConfig.ToString());
            MidoriCredentials = JsonConvert.DeserializeObject<Dictionary<string, object>>(MidoriCred.ToString());

            // Setup dependencies
            IDependencyMap MidoriDeps = new DependencyMap();
            MidoriDeps.Add(MidoriClient);
            MidoriDeps.Add(MidoriCommands);
            MidoriDeps.Add(MidoriJConfig);
            Console.WriteLine("Organized dependency library.");

            // Events handler
            MidoriEvents MidoriEvents = new MidoriEvents();
            MidoriEvents.Install();
            Console.WriteLine("Installed event handler.");

            // Login and connect
            await MidoriClient.LoginAsync(TokenType.Bot, (string)MidoriCredentials["Connection_Token"]);
            Console.WriteLine("Sent login information to Discord.");
            await MidoriClient.ConnectAsync();
            Console.WriteLine("Connected.");
            await MidoriClient.DownloadAllUsersAsync();

            // Install command handling
            await CommandHandler.Setup(MidoriDeps);
            Console.WriteLine("Installed commands handler.");

            // Keep the bot running
            await Task.Delay(-1);
        }
    }
}
