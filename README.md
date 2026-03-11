# Scraping Aziende 

![C#](https://img.shields.io/badge/language-C%23-blue)
![.NET](https://img.shields.io/badge/framework-.NET-purple)
![Database](https://img.shields.io/badge/database-MySQL-orange)
![Status](https://img.shields.io/badge/status-work_in_progress-yellow)

Dataset builder e scraper per raccogliere informazioni sulle aziende della regione **Marche** dal sito:

🔗 https://www.reportaziende.it/

Il progetto recupera i dati aziendali tramite **endpoint JSON pubblici** e costruisce un dataset utilizzabile per **analisi economiche e territoriali**.

---

# Obiettivo

Creare un **database strutturato delle aziende del territorio marchigiano** per:

- analisi del tessuto imprenditoriale locale
- classificazione delle aziende per territorio
- costruzione di dataset per analisi future

Il progetto nasce come attività di **ricerca personale durante il percorso di studi presso ITS Smart Academy**.

---

# Dataset Attuale

Province attualmente incluse:

- **Fermo (FM)**
- **Macerata (MC)**

Endpoint utilizzati:

https://www.reportaziende.it/assets/json/provinceComuni/mar_mc_elenco.json
https://www.reportaziende.it/assets/json/provinceComuni/mar_fm_elenco.json


---

# Informazioni Raccolte

Per ogni azienda vengono estratti campi come:

| Campo | Descrizione |
|------|-------------|
| Company | Nome azienda |
| Place | Luogo |
| Year | Ultimo anno |
| Revenue | Fatturato |

---

# Stack Tecnologico
- *Framework: .NET (C#)*
-*Database: MariaDB / MySQL*
 ---
Librerie principali:
- *MySqlConnector: Driver ad alte prestazioni per la connessione al database.*
- *Microsoft.Extensions.Configuration: Per la gestione dinamica delle impostazioni tramite file JSON.*
- *System.Text.Json: Per la serializzazione e deserializzazione dei dati.*
- *HttpClient: Per l'integrazione con API esterne.*


# Struttura del Progetto
Scraping-Aziende
│
├── Models
│ └── infoCompany.cs
│
├── Services
│ └── ReportAziendeService.cs
│
├── Database
│ └── schema.sql
│
└── Program.cs

