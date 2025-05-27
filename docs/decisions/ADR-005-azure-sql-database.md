# ADR-005: Azure SQL for Database Platform

## Status
Accepted (2025-05-27)

## Context
Our Skill Management System requires a reliable, scalable database solution that can handle complex relationships between people and skills, support efficient querying, and integrate well with our chosen backend technology. We also need to consider cloud deployment, performance, and maintenance requirements.

## Decision
We will use Azure SQL Database as our primary data storage solution, with database schema managed through Entity Framework Core's Code First approach.

## Consequences
- **Positive**:
  - Fully managed SQL database service with high availability
  - Seamless integration with Entity Framework Core Code First development
  - Database schema can be defined and evolved through C# model classes
  - Database migrations can be automatically generated and applied
  - Familiar T-SQL syntax for developers with SQL Server experience
  - Advanced security features including data encryption and firewall rules
  - Automatic scaling options to handle varying workloads
  - Built-in backup and point-in-time restore capabilities
  - Good performance monitoring and optimization tools
  
- **Negative**:
  - Vendor lock-in to the Microsoft/Azure ecosystem
  - Cost implications for high-performance tiers
  - Less flexibility than self-hosted database options
  - Potential latency issues depending on Azure region selection
  
## Alternatives Considered
- **PostgreSQL**: Excellent open-source option with good EF Core support, but less integrated with Azure
- **MongoDB**: Document database that would require different data modeling approach
- **MySQL/MariaDB**: Open-source relational options but with less advanced features than Azure SQL
- **SQLite**: Too lightweight for production use cases
- **Self-hosted SQL Server**: Would require more operational management
- **Database-First Approach**: Using existing database schema rather than Code First, but would limit our agility in schema evolution

## EF Core Code First Implementation
We will implement our database access using EF Core Code First approach:
- C# model classes will define our database schema
- DbContext will configure relationships and constraints
- Migrations will be used to evolve the database schema over time
- Fluent API will be used for complex mapping configurations
- Repository pattern will encapsulate data access logic

## Data Models
The primary data models will include:

- **People**: Storing individual profiles and contact information
- **Skills**: Catalog of available skills with descriptions and categories
- **PersonSkills**: Junction table mapping people to skills with proficiency levels
- **SkillVerifications**: Records of skill verification events

## References
- [Azure SQL Documentation](https://docs.microsoft.com/en-us/azure/azure-sql/)
- [Entity Framework Core with Azure SQL](https://docs.microsoft.com/en-us/azure/azure-sql/database/connect-query-dotnet-core)
- [Azure SQL Performance Best Practices](https://docs.microsoft.com/en-us/azure/azure-sql/database/performance-guidance)
