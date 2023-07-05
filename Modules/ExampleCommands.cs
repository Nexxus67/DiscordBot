using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpi.Services
{
    public class ExampleCommands : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private CommandHandler _handler;

        public ExampleCommands(CommandHandler handler)
        {
            _handler = handler;
        }

        [SlashCommand("8ball", "Find your answer!")]
        public async Task EightBall(string question)
        {
            var replies = new List<string>
            {
                "Yes",
                "No",
                "Maybe",
                "Hazy..."
            };

            var answer = replies[new Random().Next(replies.Count)];

            await RespondAsync($"You asked: [**{question}**], and your answer is: [**{answer}**]");
        }
    }
}
