# PhishGuard SOC 🛡️

PhishGuard is a high-performance, real-time phishing link detection and analytics platform. It leverages heuristic-based analysis to identify potentially malicious URLs and provides a professional Security Operations Center (SOC) dashboard for monitoring and auditing.

## 🚀 Features

- **Link Scanning Engine**: Analyzes URLs using advanced heuristics (keyword matching, domain analysis, IP detection).
- **SOC Dashboard**: Real-time visualization of scan statistics and distribution.
- **Audit Logs**: Persistent history of all analyzed links with status and timestamps.
- **Premium UI**: Modern dark-themed interface designed for security professionals.

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 10.0 MVC
- **ORM**: Entity Framework Core (SQL Server)
- **Frontend**: Bootstrap 5, Chart.js, FontAwesome 6
- **Styling**: Custom CSS with Neon/Cyberpunk aesthetics

## 📂 Project Structure

- `Controllers/LinkController.cs`: Handles scan requests and data retrieval.
- `Services/LinkCheckerService.cs`: Contains the core heuristic logic for phishing detection.
- `Models/Link.cs`: Data model for stored scan results.
- `Views/Home/Index.cshtml`: The main SOC dashboard interface.

## 🚦 Getting Started

1. **Prerequisites**: .NET 10 SDK, SQL Server.
2. **Clone the repo**:
   ```bash
   git clone https://github.com/your-username/PhishGuard.git
