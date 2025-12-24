# SmartCondo (ESI-ISI TP2 2025/26)

Projeto desenvolvido no âmbito da UC **Integração de Sistemas de Informação (ISI)**.  
O **SmartCondo** é uma solução de interoperabilidade para um *smart environment* aplicado à gestão de condomínios, composta por:
- **API REST** (com Swagger/OpenAPI)
- **SQL Server** (persistência via ADO.NET)
- **Autenticação/Autorização** (JWT + roles)
- **Integração externa** com **OpenWeather**
- **Logging** estruturado (ficheiros JSON)
- **Serviço SOAP (WCF)** com **WSDL**
- **Cliente WinForms** (demonstração end-to-end)

---

## Funcionalidades principais

- Login e emissão de **JWT**
- Operações **CRUD** (ex.: Sensores, Condomínios)
- **Dashboard** por cidade/condomínio (métricas agregadas)
- Consulta de **meteorologia** por cidade + registo em **WeatherLogs**
- **Logs JSON** de requests
- Serviço **SOAP** publicado com contrato **WSDL**
- Cliente **WinForms** com Login/CRUD/Dashboard/Weather

---

## Tecnologias

- C# / .NET
- ASP.NET (API)
- SQL Server + ADO.NET
- Swagger/OpenAPI
- JWT (Auth)
- WCF (SOAP)
- WinForms
- Postman (testes manuais)

---
## Estrutura do repositório



- /EsiTp2.Api # API REST + Swagger + JWT
- /EsiTp2.Data # ADO.NET Repositories + queries
- /EsiTp2.Domain # Entidades/modelos
- /EsiTp2.SoapService # Serviço SOAP (WCF) - Service1.svc
- /EsiTp2.WinFormsClient # Cliente WinForms
- /EsiTp2.Tests # (se aplicável) testes
- /docs # Relatório / evidências / imagens
---

## Pré-requisitos

- Visual Studio 2022 (ou compatível)
- .NET SDK instalado
- SQL Server + SSMS
- Conta OpenWeather (API Key)

---

## Configuração

### 1) Base de dados (SQL Server)
1. Criar a base de dados no SQL Server (nome conforme o teu projeto).
2. Executar os scripts SQL (se existirem no repositório) ou garantir que as tabelas estão criadas:
   - `Condominios`
   - `Sensores`
   - `Alertas`
   - `WeatherLogs`
   - (outras conforme o modelo)

### 2) Configuração da API
Configurar a **connection string** e segredos no ficheiro de settings (ex.: `appsettings.json`, `dbsettings.json`) ou via variáveis de ambiente.

Exemplo (genérico):
- `ConnectionStrings:DefaultConnection`
- `Jwt:Secret`
- `OpenWeather:ApiKey`

---

## Como executar

### 1) API REST
1. Definir o projeto **EsiTp2.Api** como *Startup Project*
2. Executar (F5)
3. Abrir Swagger:
   - `https://localhost:<PORTA>/swagger`

#### Autenticação (Swagger)
1. Fazer login no endpoint `POST /api/auth/login`
2. Copiar o token devolvido
3. Em Swagger → **Authorize** → `Bearer <TOKEN>`

---

### 2) Serviço SOAP (WCF)
1. Definir o projeto **EsiTp2.SoapService** como *Startup Project*
2. Executar (F5)
3. Ver WSDL:
   - `http://localhost:64810/Service1.svc?wsdl`

#### Teste SOAP (Postman)
- URL: `http://localhost:64810/Service1.svc`
- Method: `POST`
- Headers:
  - `Content-Type: text/xml; charset=utf-8`
  - `SOAPAction: http://tempuri.org/IService1/GetData`
- Body (raw/XML): envelope SOAP da operação `GetData`

---

### 3) Cliente WinForms
1. Definir o projeto **EsiTp2.WinFormsClient** como *Startup Project*
2. Executar
3. Fazer login e testar:
   - CRUD de Sensores/Condomínios
   - Dashboard
   - Weather

---

## Endpoints 

> Pode variar conforme implementação.

- `POST /api/auth/login`
- `GET /api/sensores`
- `GET /api/condominios`
- `GET /api/weather/{cidade}`
- `GET /api/dashboard/{cidade}`

---

## Evidências
As evidências usadas no relatório encontram-se em `/docs` (ou na pasta definida), incluindo:
- Swagger (login, weather, 401 sem token)
- Postman (login, SOAP)
- SQL (WeatherLogs)
- Logs JSON
- WinForms (Login, CRUD, Dashboard, Weather)
- WSDL + detalhe das operações

---

## Autor
- **Nome:** António Ferreira Nº 9657
- **UC:** Integração de Sistemas de Informação (ISI)
- **Ano letivo:** 2025/2026

---

## Licença
Uso académico.



