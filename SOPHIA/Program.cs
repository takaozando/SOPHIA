using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TutorialBot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
        
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).BuildServiceProvider();



            string tokenBot = "ODc1MTkwNTUxNjI3MDQyODI2.YRR6tw.s8vtuSPPocXblf7b_L9VITYlB14";

            //assinatura de eventos

            _client.Ready += Client_Ready;
            _client.Log += ClientLog;
            _client.UserJoined += UserJoined;

            
            await Client_Ready();
            await ComandosBot();

            _client.LoginAsync(TokenType.Bot, tokenBot);

            await _client.StartAsync();

            await Task.Delay(-1);
            
        }


        private async Task UserJoined(SocketGuildUser user)
        {
            var usuario = user.Guild;

        }

        private Task ClientLog(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        private async Task Client_Ready()
        {
            await _client.SetActivityAsync(new Game("BIG TESTER", ActivityType.Playing, ActivityProperties.None));
        }
        public async Task ComandosBot()
        {
            _client.MessageReceived += IniciandoComandos;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        }

        public async Task IniciandoComandos(SocketMessage arg)
        {
            var mensagem = arg as SocketUserMessage;
            if (mensagem is null || mensagem.Author.IsBot) return;

            var Context = new SocketCommandContext(_client, mensagem);
            int argPost = 0;
            if(mensagem.HasStringPrefix(".",ref argPost))
            {
                var result = await _commands.ExecuteAsync(Context, argPost, _services);

                if(!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}