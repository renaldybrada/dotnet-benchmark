# .NET Project (.NET 8.x) with Performance Testing using k6

## 📌 Intro

This project is a .NET-based (version 8) application with performance testing scripts using k6. This guide will help you run the application and conduct performance testing on your computer.
---

## 🛠 Requirements
- [.NET SDK](https://dotnet.microsoft.com/download) , choose version 8 LTS.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [k6](https://k6.io/docs/getting-started/installation/)
- [Docker](https://www.docker.com/get-started) (optional)
- [Node.js](https://nodejs.org/) (optional)

---

## 🚀 Application Preparation

### 1. Clone Repository
```sh
git clone https://github.com/renaldybrada/dotnet-benchmark.git
cd dotnet-benchmark/DotnetBenchmark
```

### 2. Database Initiation
#### a) Set connection string at appsettings.json
Open your favorite IDE, search for this file : `dotnet-benchmar/DotnetBenchmark/appsettings.json`

Then add your connection string pointing to your SQL Server.
For example, this connection string will connect to your SQL Server on your local Windows computer
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=<Your SQL Server Instance>;Database=BenchmarkDotnet;Trusted_Connection=True;TrustServerCertificate=True"
  }
```

#### b) Build and Install Dependencies
```sh
dotnet restore
dotnet build
```

#### c) Update Database
This script will automatically create a database and all tables needed for testing. Run this script on terminal
```sh
dotnet ef database update
```
Check on your SQL server, make sure Database `BenchmarkDotnet` exists after the execution.

#### d) Data Seeding and Running the application
This script will seed your database with data needed for testing. This operation may take a while.
```sh
dotnet run
```
After seeding, the application will running

![image](https://github.com/user-attachments/assets/db7e291c-e252-4cb3-b3fe-3725033d62ca)


---

## 📊 Performance Test With K6

### 1. Requirements
Make sure K6 has been installed, run this script on terminal :
```sh
k6
```
if K6 has been installed, it will show like this :
![image](https://github.com/user-attachments/assets/32e30c70-1c93-403e-828c-30f00a95e57d)

### 2. Setup Configuration
Create config.json from config.example.json

Set `BASE_URL` with URL to your .NET application after running. In my case, my application run at  `http://localhost:5077/api`

```json
{
    "BASE_URL": "http://localhost:5077/api",
    "DEFAULT_RESOURCE": "Product",
    "DEFAULT_METHOD": "sync"
}
```

### 3. Running test
Make sure .NET application has been running. Then try to execute this scripts :
```sh
cd PerformanceTest
k6 run 0_single_test.js
```
This script will execute test for Product list with sync method (default)

---

## 📝 Project Structure
```
/DotnetBenchmark
├── Controllers/                                # Controller : Product & Order
├── Data/                                       # Containing database configuration and seeding script
├── Migrations/                                 # All migrations file to create tables
├── Models/                                     # Model Class : Product, Consumer, Order, OrderItem
├── PerformanceTest/                            # K6 scripts for testing endpoints
│   ├── 0_single_test.js                        # Test script for an endpoint
│   ├── 1_sync_vs_async.js                      # Test script comparing sync and async method
│   ├── 2_sync_vs_sync_no_track.js              # Test script comparing sync and sync no track
│   ├── 3_all_comparison.js                     # Test script all comparison with linier scenario (10 vus, 30s)
│   ├── 4_all_comparison_stages_scenario.js     # Test script all comparison with staged scenario
│   ├── config.json                             # Config file for configuring K6 test
├── README.md               
```

---

## ❓ Need Help
If you have any trouble regarding this project, please kindly open an issue here [GitHub Issues](https://github.com/renaldybrada/dotnet-benchmark/issues)).

