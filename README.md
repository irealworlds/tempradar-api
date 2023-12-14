# Tempradar Sensor API
[![.NET Build & Test](https://github.com/irealworlds/tempradar-api/actions/workflows/dotnet.yml/badge.svg)](https://github.com/irealworlds/tempradar-api/actions/workflows/dotnet.yml)

The .NET WebApi powering the Tempradar web app, built using ASP.NET 8.

## Running
Using Docker Compose.

0. Install [Docker](https://www.docker.com/) on your machine
1. Clone the repository
2. Copy the `.env.example` file to `.env` and fill in your environment variables
3. Start the image using Docker Compose

```sh
docker compose -f docker-compose.yml -f docker-compose.dev.yml -p tempradar-sensor up --remove-orphans -d --build
```
> **Note:** This command starts the application in _development mode_. If you want to run in production, omit loading `docker-compose.dev.yml`

> **Note:** This command will run the application in _detached mode_.

4. Thee application is now accessible on your machine
    - Port `8000` is open to HTTPS and port `8001` is open to HTTP.
    - You can access Swagger on your local machine at http://localhost:8001/swagger or https://localhost:8000/swagger

## See also
- [irealworlds/tempradar-webapp](https://github.com/irealworlds/tempradar-webapp) - The Tempradar web application, built in Angular.
- [irealworlds/tempradar-sensor-api](https://github.com/irealworlds/tempradar-sensor-api) - An API for receiving data from a Tempradar sensor set and storing that data for later querying by clients.
- [irealworlds/tempradar-sensor](https://github.com/irealworlds/tempradar-sensor) - Code powering Tempradar Arduino sensor sets.

## License
This project is [MIT licensed](LICENSE).
