Dataset builder e scraper per raccogliere informazioni sulle aziende della regione Marche dal sito:

🔗 https://www.reportaziende.it/

Il progetto recupera i dati aziendali tramite endpoint JSON pubblici e costruisce un dataset utilizzabile per analisi economiche, territoriali.

Obiettivo

Creare un database strutturato delle aziende del territorio marchigiano per:

analisi del tessuto imprenditoriale locale.

Il progetto nasce anche come attività di ricerca personale durante il percorso di studi presso ITS Smart Academy.

Dataset Attuale

Province attualmente incluse:

Fermo (FM)

Macerata (MC)

I dati vengono recuperati dagli endpoint JSON:

https://www.reportaziende.it/assets/json/provinceComuni/mar_mc_elenco.json
https://www.reportaziende.it/assets/json/provinceComuni/mar_fm_elenco.json
Informazioni Raccolte

Per ogni azienda vengono estratti campi come:

Company

Place

Year 

Revenue




Stack Tecnologico:

C#

.NET

HttpClient

System.Text.Json

MySQL

Struttura del Progetto:


Scraping-Aziende
│
├── Models
│   └── infoCompany.cs
│
├── Services
│   └── ReportAziendeService.cs
│
├── Database
│   └── schema.sql
│
└── Program.cs
