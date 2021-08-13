using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SOPHIA.Commands
{
    public class PingPong : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]

        public async Task pingPong()
        {
            await ReplyAsync("pong");
        }
    }
}
