using ScraperAziende.Models;
using MySqlConnector;
using System.Text.Json;

namespace ScraperAziende.Services;

public class ReportAziendeService
{
    private readonly string _connectionString;
    private readonly HttpClient _client;

    public ReportAziendeService(string connectionString)
    {
        _connectionString = connectionString;
        _client = new HttpClient();
    }

    public async Task<List<infoCompany>> GetAziendeAsync()
    {
        var urls = new List<string>
        {
            "https://www.reportaziende.it/assets/json/provinceComuni/mar_mc_elenco.json",
            "https://www.reportaziende.it/assets/json/provinceComuni/mar_fm_elenco.json"
        };

        var aziende = new List<infoCompany>();

        foreach (var url in urls)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var rawData = JsonSerializer.Deserialize<List<JsonElement>>(json);

            foreach (var item in rawData!)
            {
                aziende.Add(new infoCompany
                {
                    Company = item.GetProperty("name").GetString() ?? "",
                    Place = item.GetProperty("comune").GetString() ?? "",
                    Year = int.Parse(item.GetProperty("LatestAnno").GetString() ?? "0"),
                    Revenue = decimal.Parse(item.GetProperty("LatestValore").GetString() ?? "0", System.Globalization.CultureInfo.InvariantCulture)
                });
            }
        }

        return aziende;
    }

    public async Task SaveToDatabaseAsync(List<infoCompany> aziende)
    {
        await using var conn = new MySqlConnection(_connectionString);
        await conn.OpenAsync();

        foreach (var azienda in aziende)
        {
            var cmd = new MySqlCommand(
                "INSERT INTO totCompanies (company, place, year, revenue) VALUES (@a, @l, @an, @f)",
                conn);

            cmd.Parameters.AddWithValue("@a", azienda.Company);
            cmd.Parameters.AddWithValue("@l", azienda.Place);
            cmd.Parameters.AddWithValue("@an", azienda.Year);
            cmd.Parameters.AddWithValue("@f", azienda.Revenue);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}