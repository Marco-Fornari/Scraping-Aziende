# Web Scraping Italian Companies

![C#](https://img.shields.io/badge/language-C%23-blue)
![.NET](https://img.shields.io/badge/framework-.NET-purple)
![Database](https://img.shields.io/badge/database-MySQL-orange)
![Status](https://img.shields.io/badge/status-work_in_progress-yellow)

Dataset builder and scraper designed to collect information about companies located in the **Marche region (Italy)** from the website:

🔗 https://www.reportaziende.it/

The project retrieves company data through **public JSON endpoints** and builds a dataset that can be used for **economic and territorial analysis**.

---

# Objective

The goal of this project is to create a **structured database of companies located in the Marche region** in order to:

- analyze the local business ecosystem
- classify companies by geographical area
- build datasets for future analysis

This project was created as part of **personal research during my studies at ITS Smart Academy**.

---

# Current Dataset

Currently included provinces:

- **Fermo (FM)**
- **Macerata (MC)**

Endpoints used:
https://www.reportaziende.it/assets/json/provinceComuni/mar_mc_elenco.json
https://www.reportaziende.it/assets/json/provinceComuni/mar_fm_elenco.json


---

# Collected Data

For each company, the following fields are extracted:

| Field | Description |
|------|-------------|
| Company | Company name |
| Place | Location |
| Year | Latest available year |
| Revenue | Company revenue |

---

# Technology Stack

### Framework
- **.NET (C#)**

### Database
- **MariaDB / MySQL**

---

## Main Libraries

- **MySqlConnector**  
  High-performance driver used to connect to MySQL/MariaDB databases.

- **Microsoft.Extensions.Configuration**  
  Used to dynamically manage application settings via JSON configuration files (e.g. `appsettings.json`).

- **System.Text.Json**  
  Used for JSON serialization and deserialization.

- **HttpClient**  
  Used to perform HTTP requests and integrate external APIs.

---

# Project Structure
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
