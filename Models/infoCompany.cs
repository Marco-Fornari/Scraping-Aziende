namespace ScraperAziende.Models;

public class infoCompany
{

    public string VatNumber { get; set; } = "";
    public string FiscalCode { get; set; } = "";

    public string Company  { get; set; } = "";

    public string Place { get; set; } = "";
    public string Province { get; set; } = "";

   
    public string AtecoCode { get; set; } = "";
    public string AtecoDescription { get; set; } = "";

    public int Year { get; set; } 
    public decimal Revenue { get; set; } 
}