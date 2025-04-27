#  Dockerized Dynamic Config System

Full stack dynamic configuration management system with:

-  MSSQL initialized via `init.sql`
-  .NET 8 API (ConfigApi)
-  React + Tailwind config-panel
-  Docker Compose orchestration
-  Independent test app (SERVICE-B)

##  Quick Start

```bash
docker-compose up --build
Frontend: http://localhost:5173
Backend API: http://localhost:5239/api/configurations
MSSQL: localhost:1433 - user: sa - pass: StrongPassword123!

init.sql automatically sets up database and sample data.
