# Application Folder Structure

This document outlines the recommended high-level folder structure for implementing the Skill Management System based on our architectural decisions.

## Root Structure

```
skill-management-system/
├── src/                   # Source code
│   ├── api/               # Backend C# API
│   └── client/            # Frontend React application
├── docs/                  # Documentation (already established)
├── tests/                 # Test projects
└── tools/                 # Build scripts, dev tools, etc.
```

## Backend Structure (Clean Architecture)

The backend follows Clean Architecture principles with three main projects:

```
api/
├── Application.API/         # API layer (controllers, middleware, configuration)
├── Application.Core/        # Domain layer (entities, business logic)
└── Application.Infrastructure/ # Infrastructure layer (data access, external services)
```

### Clean Architecture Components

- **API Layer**: Entry point for HTTP requests, HATEOAS implementation
- **Core Layer**: Business entities and logic, independent of external frameworks
- **Infrastructure Layer**: EF Core implementation, database access, external services

### EF Code First Approach

The structure supports Entity Framework Core Code First approach:

- Entity classes defined in `SkillManagement.Core/Entities/`
- DbContext and configurations in `SkillManagement.Infrastructure/Data/`
- Migrations stored in `SkillManagement.Infrastructure/Data/Migrations/`

## Frontend Structure (src/client)

```
client/
├── public/                # Static assets
├── src/
│   ├── assets/            # Images, fonts, etc.
│   ├── components/        # Reusable UI components
│   │   ├── common/        # Generic components
│   │   ├── people/        # People-related components
│   │   └── skills/        # Skills-related components
│   ├── contexts/          # React contexts
│   ├── hooks/             # Custom React hooks
│   ├── layouts/           # Page layout components
│   ├── pages/             # Page components
│   │   ├── Overview/      # Overview screen
│   │   ├── Detail/        # Detail screen
│   │   └── Skill/         # Skill screen
│   ├── services/          # API client services
│   ├── types/             # TypeScript type definitions
│   └── utils/             # Helper functions
├── tailwind.config.js     # Tailwind CSS configuration
└── tsconfig.json          # TypeScript configuration
```

## Test Structure

```
tests/
├── SkillManagement.IntegrationTests/  # Integration tests
└── SkillManagement.E2ETests/          # End-to-end tests with Cypress
```

## Alignment with ADRs

This structure is designed to align with our architectural decisions:

- **ADR-001**: React with TypeScript frontend with appropriate organization for components, hooks, and contexts
- **ADR-002**: Support for 5-level skill proficiency implementation in entities
- **ADR-003**: Support for skill verification process in appropriate entities and services
- **ADR-004**: C# API with clean architecture approach
- **ADR-005**: Azure SQL database with Entity Framework Code First approach

## Benefits of this Structure

1. **Clean Architecture** - Clear separation of concerns with proper layering
2. **Domain-Driven Design** - Core business logic isolated from infrastructure concerns
3. **Testability** - Structure supports comprehensive testing strategies
4. **Scalability** - Features can be added without significant restructuring
5. **Developer Experience** - Intuitive organization reduces onboarding time
6. **Maintainability** - Logical grouping of related code
7. **Support for HATEOAS** - Dedicated middleware for implementing hypermedia links

## Implementation Approach

We recommend starting with:

1. Setting up the basic solution and project structure
2. Implementing core entities and DbContext with initial migrations
3. Adding minimal API controllers with HATEOAS support
4. Creating React component structure for the primary screens
5. Implementing service communication between frontend and backend
