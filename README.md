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

## Getting Started

### Running with Docker (recommended)

1. Make sure Docker and Docker Compose are installed on your system
2. From the root directory, run:
   ```
   docker-compose up -d
   ```
3. Open the application at:
   - Frontend: [http://localhost:3000](http://localhost:3000)
   - API Swagger: [https://localhost:7294/swagger](https://localhost:7294/swagger)

### Running locally (development)

#### Backend API

1. Navigate to the backend directory:
   ```
   cd backend/API
   ```

2. Run the API:
   ```
   dotnet run
   ```

3. Open Swagger UI at: [https://localhost:7294/swagger](https://localhost:7294/swagger)

#### Frontend

1. Navigate to the frontend directory:
   ```
   cd frontend
   ```

2. Install dependencies:
   ```
   npm install
   ```

3. Start the development server:
   ```
   npm start
   ```

4. Open the application at: [http://localhost:3000](http://localhost:3000)
