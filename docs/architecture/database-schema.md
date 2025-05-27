# Database Schema

This document outlines the database schema for the Skill Management System.

> Note: This is a placeholder document. The actual database schema will be designed as development progresses.

## Planned Tables

### People
- Table to store information about individuals in the system
- Will include basic profile information
- Will link to skills through a many-to-many relationship

### Skills
- Table to store the catalog of skills available in the system
- Will include skill descriptions, categories, and other metadata

### Person_Skills
- Junction table to map people to their skills
- Will store proficiency levels and verification status
- Will implement the 5-level proficiency scale as defined in [ADR-002](/docs/decisions/features/ADR-002-skill-proficiency-levels.md)

### Verifications
- Table to store verification records
- Will track who verified a person's skill and when
- Will implement the verification process as defined in [ADR-003](/docs/decisions/process/ADR-003-skill-verification-process.md)

## Database Considerations

- Azure SQL will be used as specified in the architecture overview
- Entity Framework will be used for ORM capabilities
- Proper indexing strategies will be implemented
- Consider future performance optimization needs
