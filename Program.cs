using Microsoft.Extensions.Configuration;
using ScraperAziende.Services;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

string connectionString = config.GetConnectionString("Base")!;

var service = new ReportAziendeService(connectionString);

var aziende = await service.GetAziendeFermoAsync();

Console.WriteLine($"Trovate {aziende.Count} aziende");

await service.SaveToDatabaseAsync(aziende);

Console.WriteLine("Salvataggio completato.");