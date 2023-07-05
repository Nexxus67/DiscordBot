using Discord.Commands;
using System.Threading.Tasks;

public class Commands : ModuleBase<SocketCommandContext>
{
  [Command("hello")]
public async Task HelloCmd()
    {
        await ReplyAsync("i'm a generic response please don't kill me");
    }
}
