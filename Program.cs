using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using csharpi.Services;
using System.Threading;

namespace DiscordBot
{
    class Program
    {
        private readonly IConfiguration _config;
        private DiscordSocketClient _client;
        private InteractionService _commands;
        private ulong _testGuildId;

        public static Task Main(string[] args) => new Program().MainAsync();

        public Program()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");

            _config = builder.Build();
            _testGuildId = ulong.Parse(_config["TestGuildId"]);
        }

        public async Task MainAsync()
        {
            using (var services = ConfigureServices())
            {
                var client = services.GetRequiredService<DiscordSocketClient>();
                var commands = services.GetRequiredService<InteractionService>();
                _client = client;
                _commands = commands;

                client.Log += LogAsync;
                commands.Log += LogAsync;
                client.Ready += ReadyAsync;

                await client.LoginAsync(TokenType.Bot, _config["Token"]);
                await client.StartAsync();

                await services.GetRequiredService<CommandHandler>().InitializeAsync();

                // Bucle de espera activa hasta que se cumpla la condición de finalización deseada
                bool exitFlag = false;
                while (!exitFlag)
                {
                    // Aquí puedes realizar cualquier otra lógica que necesites ejecutar en cada iteración del ciclo

                    // Ejemplo: Esperar una entrada del usuario para finalizar el programa
                    Console.WriteLine("Presiona 'q' y Enter para salir...");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "q")
                    {
                        exitFlag = true;
                    }

                    // También puedes agregar una espera breve (por ejemplo, 1 segundo) para evitar un consumo excesivo de recursos del sistema
                    await Task.Delay(1000);
                }
            }
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private async Task ReadyAsync()
        {
            if (IsDebug())
            {
                Console.WriteLine($"In debug mode, adding commands to {_testGuildId}...");
                await _commands.RegisterCommandsToGuildAsync(_testGuildId);
            }
            else
            {
                await _commands.RegisterCommandsGloballyAsync(true);
            }
            Console.WriteLine($"Connected as -> [{_client.CurrentUser}] :)");
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_config)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                .AddSingleton<CommandHandler>()
                .BuildServiceProvider();
        }

        static bool IsDebug()
        {
            #if DEBUG
                return true;
            #else
                return false;
            #endif
        }
    }
}
