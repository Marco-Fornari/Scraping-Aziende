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

	public async Task<List<AziendaInfo>> GetAziendeFermoAsync()
	{
		var url = "https://www.reportaziende.it/assets/json/provinceComuni/mar_mc_elenco.json";

		var response = await _client.GetAsync(url);
		var json = await response.Content.ReadAsStringAsync();

		var rawData = JsonSerializer.Deserialize<List<JsonElement>>(json);

		var aziende = new List<AziendaInfo>();

		foreach (var item in rawData!)
		{
			aziende.Add(new AziendaInfo
			{
				Azienda = item.GetProperty("name").GetString() ?? "",
				Localita = item.GetProperty("comune").GetString() ?? "",
				Anno = item.GetProperty("LatestAnno").GetRawText(),
				Fatturato = item.GetProperty("LatestValore").GetRawText()
			});
		}

		return aziende;
	}

	public async Task SaveToDatabaseAsync(List<AziendaInfo> aziende)
	{
		await using var conn = new MySqlConnection(_connectionString);
		await conn.OpenAsync();

		foreach (var azienda in aziende)
		{
			var cmd = new MySqlCommand(
				"INSERT INTO aziende_macerata (azienda, localita, anno, fatturato) VALUES (@a, @l, @an, @f)",
				conn);

			cmd.Parameters.AddWithValue("@a", azienda.Azienda);
			cmd.Parameters.AddWithValue("@l", azienda.Localita);
			cmd.Parameters.AddWithValue("@an", azienda.Anno);
			cmd.Parameters.AddWithValue("@f", azienda.Fatturato);

			await cmd.ExecuteNonQueryAsync();
		}
	}
}