using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

const string apiBaseUrl = "https://localhost:7077";

const string loginEmail = "darlind21@icloud.com";
const string loginPassword = "Pa$$w0rd";


var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
string? jwtToken = null;


Console.WriteLine("Waiting 10 seconds for API to start...\n");
await Task.Delay(10000); // 10 seconds delay for api to start


HubConnection? connection = null;


jwtToken = await LoginAndGetToken();
await ConnectToSignalR(jwtToken);


// Keep app running to listen for notifications
Console.WriteLine("\nPress ENTER to exit...\n");
Console.ReadLine();

await connection!.DisposeAsync();





async Task<string> LoginAndGetToken()
{
    var loginPayload = new
    {
        Email = loginEmail,
        Password = loginPassword
    };

    var response = await httpClient.PostAsync(
        "/api/account/login",
        new StringContent(JsonSerializer.Serialize(loginPayload), Encoding.UTF8, "application/json")
    );

    response.EnsureSuccessStatusCode();

    var content = await response.Content.ReadAsStringAsync();
    var json = JsonDocument.Parse(content);
    var token = json.RootElement.GetProperty("token").GetString();

    Console.WriteLine($"Token: {token}");
    return token!;
}




async Task ConnectToSignalR(string token)
{
    var hubUrl = $"{apiBaseUrl}/hubs/notifications";

    connection = new HubConnectionBuilder()
        .WithUrl(hubUrl, options =>
        {
            options.AccessTokenProvider = () => Task.FromResult(token)!;
        })
        .WithAutomaticReconnect()
        .Build();

    connection.On<object>("ReceiveNotification", notification =>
    {
        Console.WriteLine("\n Notification received:");
        Console.WriteLine(JsonSerializer.Serialize(notification, new JsonSerializerOptions { WriteIndented = true }));
    });

    try
    {
        await connection.StartAsync();
        Console.WriteLine("Connected");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to connect: {ex.Message}");
    }
}