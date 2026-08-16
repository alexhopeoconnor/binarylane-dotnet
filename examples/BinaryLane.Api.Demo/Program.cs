using System.Text.Json;
using BinaryLane.Api.V2;
using BinaryLane.Api.V2.DependencyInjection;
using BinaryLane.Api.V2.Errors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

return await DemoProgram.RunAsync(args);

internal static class DemoProgram
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
    };

    public static async Task<int> RunAsync(string[] args)
    {
        var command = args.FirstOrDefault()?.ToLowerInvariant();
        if (command is null or "--help" or "-h" or "help")
        {
            PrintUsage();
            return 0;
        }

        using var cancellationSource = new CancellationTokenSource();
        Console.CancelKeyPress += (_, eventArgs) =>
        {
            eventArgs.Cancel = true;
            cancellationSource.Cancel();
        };

        var builder = Host.CreateApplicationBuilder(args);
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets(typeof(DemoProgram).Assembly, optional: true)
            .AddEnvironmentVariables();

        var token = builder.Configuration["BinaryLane:ApiToken"]
            ?? Environment.GetEnvironmentVariable("BINARYLANE_API_TOKEN");

        if (string.IsNullOrWhiteSpace(token))
        {
            Console.Error.WriteLine(
                "No API token is configured. Use user secrets or set BINARYLANE_API_TOKEN. "
                + "Run with --help for a setup command.");
            return 2;
        }

        builder.Services.AddBinaryLaneApi(options =>
        {
            options.ApiToken = token;

            var baseUrl = builder.Configuration["BinaryLane:BaseUrl"];
            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                options.BaseUrl = baseUrl;
            }
        });

        using var host = builder.Build();
        var client = host.Services.GetRequiredService<IBinaryLaneClient>();

        try
        {
            switch (command)
            {
                case "account":
                    await WriteJsonAsync(
                        await client.Account.GetAsync(cancellationSource.Token));
                    return 0;

                case "servers":
                    await WriteItemsAsync(
                        client.Servers.ListAllAsync(cancellationToken: cancellationSource.Token));
                    return 0;

                case "server":
                    if (!TryReadServerId(args, out var serverId))
                    {
                        return 2;
                    }

                    await WriteJsonAsync(
                        await client.Servers.GetAsync(serverId, cancellationSource.Token));
                    return 0;

                case "regions":
                    await WriteItemsAsync(
                        client.Regions.ListAllAsync(cancellationToken: cancellationSource.Token));
                    return 0;

                default:
                    Console.Error.WriteLine($"Unknown command: {command}");
                    PrintUsage();
                    return 2;
            }
        }
        catch (OperationCanceledException) when (cancellationSource.IsCancellationRequested)
        {
            Console.Error.WriteLine("Cancelled.");
            return 130;
        }
        catch (BinaryLaneApiException exception)
        {
            // The sample prints the response status and path only.
            Console.Error.WriteLine(
                $"BinaryLane request failed with HTTP {(int)exception.StatusCode} "
                + $"({exception.StatusCode}) at {exception.RequestUri.AbsolutePath}.");
            return 1;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine($"Request failed ({exception.GetType().Name}).");
            return 1;
        }
    }

    private static bool TryReadServerId(string[] args, out long serverId)
    {
        if (args.Length > 1 && long.TryParse(args[1], out serverId) && serverId > 0)
        {
            return true;
        }

        Console.Error.WriteLine("Usage: server <positive-server-id>");
        serverId = default;
        return false;
    }

    private static async Task WriteItemsAsync<T>(IAsyncEnumerable<T> items)
    {
        await foreach (var item in items)
        {
            await WriteJsonAsync(item);
        }
    }

    private static Task WriteJsonAsync<T>(T value)
    {
        Console.WriteLine(JsonSerializer.Serialize(value, JsonOptions));
        return Task.CompletedTask;
    }

    private static void PrintUsage()
    {
        Console.WriteLine(
            """
            BinaryLane.Api demo — read-only operations only

            Setup:
              dotnet user-secrets set "BinaryLane:ApiToken" "your-token" \
                --project examples/BinaryLane.Api.Demo

            Commands:
              account
              servers
              server <positive-server-id>
              regions

            You can also set BINARYLANE_API_TOKEN for one process.
            """);
    }
}
