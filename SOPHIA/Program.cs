using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;

namespace SOPHIA
{
    class Program
    {
        private DiscordClient _client;

        static void Main(string[] args) => new Program().RodarBotAsync().GetAwaiter().GetResult();
        {
    
        public async Task RodarBotAsync()
        {
            DiscordConfiguration discordConfig = new DiscordConfiguration
            {
                Token = "Token",
                TokenType = TokenType.Bot, //sempre Bot
                ReconnectIndefinitely = true,
                GatewayCompressionLevel = GatewayCompressionLevel.Stream,
                AutoReconnect = true,
            };
            _client = new DiscordClient(discordConfig);
            _client.Ready += Client_Ready;
            _client.ClientErrored += Client_ClientError;

            string[] prefix = new string[1];
            prefix[0] = "!";

            CommandsNextExtension cnt = _client.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = prefix,
                EnableDms = true, //Mensagem privada
                CaseSensitive = false, //Comandos case sensitive
                EnableDefaultHelp = false,
                EnableMentionPrefix = true, //habilita mencionar o bot
                IgnoreExtraArguments = true //Ignora char a mais após comando [ex: !join = !joinnn =!join9]
            });
            discordConfig.CommandExecuted += Cnt_CommandExecuted;

            await _client.ConnectAsync();
            await Task.Delay(-1); //

        }
        private Task Client_Ready(ReadyEventArgs e)
        {
            e.
            _client.UpdateStatusAsync(new DSharpPlus.Entities.DiscordActivity("!ajuda",DSharpPlus.Entities.ActivityType.Playing),DSharpPlus.Entities.UserStatus.Online)
            return Task.CompletedTask;
        }

        private Task Client_ClientError(ClientErrorEventArgs e)
        {
            
        }


    }
}
