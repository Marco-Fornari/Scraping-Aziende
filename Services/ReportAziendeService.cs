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
            "https://www.reportaziende.it/assets/json/provinceComuni/mar_ap_elenco.json",
            "https://www.reportaziende.it/assets/json/provinceComuni/mar_fm_elenco.json",
            "https://www.reportaziende.it/assets/json/provinceComuni/mar_mc_elenco.json",
            "https://www.reportaziende.it/assets/json/provinceComuni/mar_pu_elenco.json",
            "https://www.reportaziende.it/assets/json/provinceComuni/mar_an_elenco.json"



        };

        var aziende = new List<infoCompany>();

        foreach (var url in urls)
        {
            var province = url.Split("mar_")[1].Substring(0, 2).ToUpper();
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var rawData = JsonSerializer.Deserialize<List<JsonElement>>(json);

            foreach (var item in rawData!)
            {
                aziende.Add(new infoCompany
                {
                    VatNumber = item.GetProperty("vat_number").GetString() ?? "",
                    FiscalCode = item.GetProperty("fiscal_code").GetString() ?? "",

                    Company = item.GetProperty("name").GetString() ?? "",
                   
                    Place = item.GetProperty("comune").GetString() ?? "",
                    Province = province,

                    AtecoCode = item.GetProperty("aziendeAnagraficaCodiceAteco").GetString() ?? "",
                    AtecoDescription = item.GetProperty("aziendeAnagraficaDescrizioneAteco").GetString() ?? "",

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
                  "INSERT INTO totCompanies (vatNumber, fiscalCode, company, place, province, atecoCode, atecoDescription,  year, revenue) VALUES (@vat, @fiscal, @company, @place, @province, @atecoCode, @atecoDesc, @year, @revenue)",
                conn);

            cmd.Parameters.AddWithValue("@vat", azienda.VatNumber);
            cmd.Parameters.AddWithValue("@fiscal", azienda.FiscalCode);
            cmd.Parameters.AddWithValue("@company", azienda.Company);
            cmd.Parameters.AddWithValue("@place", azienda.Place);
            cmd.Parameters.AddWithValue("@province", azienda.Province);
            cmd.Parameters.AddWithValue("@atecoCode", azienda.AtecoCode);
            cmd.Parameters.AddWithValue("@atecoDesc", azienda.AtecoDescription);
            cmd.Parameters.AddWithValue("@year", azienda.Year);
            cmd.Parameters.AddWithValue("@revenue", azienda.Revenue);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}