# Skill Management System

A web application for tracking people and their skills with proficiency levels and verification processes.

## Project Structure

This project follows a three-tier architecture with:

- React TypeScript frontend
- C# ASP.NET Core API backend
- Entity Framework Core for data access

## Documentation

The complete documentation for this project is available in the [docs](./docs) directory.

- [Project Overview](./docs/README.md)
- [Architecture](./docs/architecture/overview.md)
- [Initial Design Whiteboard](./docs/architecture/diagrams/initial-whiteboard.md)
- [Architectural Decision Records](./docs/decisions/)
- [Configuration Guide](./docs/configuration.md)
- [Secrets Management](./docs/secrets-management.md)

## Getting Started

### Running with Docker (recommended)

1. Make sure Docker and Docker Compose are installed on your system
2. Configure the application (optional):
   - Copy `.env.sample` to `.env` and modify as needed
   - See [Configuration Documentation](./docs/configuration.md) for details
3. From the root directory, run:
   ```
   docker-compose up -d
   ```
4. Open the application at:
   - Frontend: [http://localhost:3000](http://localhost:3000) (or custom port if configured)
   - API Swagger: [http://localhost:5054/swagger](http://localhost:5054/swagger) (or custom port if configured)

### Running locally (development)

#### Backend API

1. Navigate to the backend directory:
   ```
   cd backend/API
   ```

2. Configure environment variables (optional):
   ```
   export API_URLS=http://localhost:5054;https://localhost:7294
   ```

3. Run the API:
   ```
   dotnet run
   ```

4. Open Swagger UI at: [http://localhost:5054/swagger](http://localhost:5054/swagger) or [https://localhost:7294/swagger](https://localhost:7294/swagger)

#### Frontend

1. Navigate to the frontend directory:
   ```
   cd frontend
   ```

2. Configure environment variables (optional):
   - Create a `.env` file with your configuration or copy `.env.sample`
   - See [Configuration Documentation](./docs/configuration.md)

3. Install dependencies:
   ```
   npm install
   ```

4. Start the development server:
   ```
   npm start
   ```

4. Open the application at: [http://localhost:3000](http://localhost:3000)

## Azure Deployment

The application can be deployed to Azure using the provided Bicep infrastructure as code.

### Quick Deployment

1. Navigate to the deploy directory:
   ```
   cd deploy
   ```

2. Run the deployment script:
   ```
   ./deploy.sh dev skillmgmt-rg eastus
   ```

3. Follow the on-screen instructions to deploy the application code

For more details, see the [deployment documentation](./deploy/README.md).
