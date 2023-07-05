using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace csharpi.Services
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;
        private readonly IServiceProvider _services;

        public CommandHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider services)
        {
            _client = client;
            _commands = commands;
            _services = services;
        }

        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            _client.InteractionCreated += HandleInteraction;

            _commands.SlashCommandExecuted += SlashCommandExecuted;
            _commands.ContextCommandExecuted += ContextCommandExecuted;
            _commands.ComponentCommandExecuted += ComponentCommandExecuted;
        }

        private Task ComponentCommandExecuted(ComponentCommandInfo commandInfo, IInteractionContext context, IResult result)
        {
            if (!result.IsSuccess)
            {
                // Handle the component command execution result here
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.UnknownCommand:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.BadArgs:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.Exception:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.Unsuccessful:
                        // Handle the specific error case
                        break;
                    default:
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private Task ContextCommandExecuted(ContextCommandInfo commandInfo, IInteractionContext context, IResult result)
        {
            if (!result.IsSuccess)
            {
                // Handle the context command execution result here
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.UnknownCommand:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.BadArgs:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.Exception:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.Unsuccessful:
                        // Handle the specific error case
                        break;
                    default:
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private Task SlashCommandExecuted(SlashCommandInfo commandInfo, IInteractionContext context, IResult result)
        {
            if (!result.IsSuccess)
            {
                // Handle the slash command execution result here
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.UnknownCommand:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.BadArgs:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.Exception:
                        // Handle the specific error case
                        break;
                    case InteractionCommandError.Unsuccessful:
                        // Handle the specific error case
                        break;
                    default:
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private async Task HandleInteraction(SocketInteraction interaction)
        {
            try
            {
                var context = new SocketInteractionContext(_client, interaction);
                await _commands.ExecuteCommandAsync(context, _services);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                if (interaction.Type == InteractionType.ApplicationCommand)
                {
                    await interaction.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
                }
            }
        }
    }
}
