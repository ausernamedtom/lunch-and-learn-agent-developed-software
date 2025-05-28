# Environment Configuration

This document describes how to configure the frontend and backend applications using environment variables.

## Frontend Configuration

The frontend React application can be configured using the following environment variables:

| Variable | Description | Default |
|----------|-------------|---------|
| `PORT` | The port on which the frontend server runs | `3000` |
| `REACT_APP_API_URL` | URL of the backend API | `http://localhost:5054/api` |

You can set these variables in a `.env` file in the frontend directory or by setting them in the environment before starting the application.

## Backend Configuration

The backend API can be configured using the following environment variables:

| Variable | Description | Default |
|----------|-------------|---------|
| `API_URLS` | Semicolon-separated list of URLs on which the API should listen | Uses launchSettings.json defaults |
| `ASPNETCORE_ENVIRONMENT` | Environment name (Development, Staging, Production) | `Development` |

Additionally, the following settings can be configured in `appsettings.json` or through environment variables:

| Setting | Description | Default |
|---------|-------------|---------|
| `AllowedOrigins` | Array of origins allowed to access the API via CORS | `["http://localhost:3000", "http://localhost:3001", "http://localhost:3002", "http://localhost:3003"]` |
| `ConnectionStrings:DefaultConnection` | Database connection string | See appsettings.json |

### Example Configuration

#### Frontend (.env file)
```
PORT=3000
REACT_APP_API_URL=http://localhost:5054/api
```

#### Backend (environment variables)
```
API_URLS=http://localhost:5054;https://localhost:7294
ASPNETCORE_ENVIRONMENT=Development
```

## Docker Configuration

When running with Docker Compose, configure the services by setting environment variables in the docker-compose.yml file:

```yaml
services:
  api:
    environment:
      - API_URLS=http://+:80;https://+:443
      - AllowedOrigins__0=http://localhost:3000
      - AllowedOrigins__1=http://frontend:3000

  frontend:
    environment:
      - PORT=3000
      - REACT_APP_API_URL=http://localhost:5054/api
```
