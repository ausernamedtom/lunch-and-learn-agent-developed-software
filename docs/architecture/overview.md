# System Architecture Overview

The Skill Management System follows a three-tier architecture with clear separation between data storage, business logic, and presentation layers.

## High-Level Architecture

The system consists of the following components:

> **Note:** All major architectural decisions are documented in [Architectural Decision Records (ADRs)](/docs/decisions/README.md).

### Technology Stack

#### Database
- **Azure SQL**: Relational database for storing user profiles and skills data

#### Backend
- **C# API**: REST API built with C#
- **EF (Entity Framework)**: ORM for database operations
- **Hateoas**: Hypermedia as the Engine of Application State - REST API design principle for improved navigation and discoverability

#### Frontend
- **TypeScript**: Programming language for type-safe JavaScript development
- **React**: JavaScript library for building user interfaces
- **Tailwind**: Utility-first CSS framework for rapid UI development
- **Rich Snippet**: Structured data implementation for improved SEO

## System Intent

This application aims to create a skill management system where users can:
1. Browse a list of people and quickly identify their primary skills
2. Examine detailed skill profiles for individual team members
3. Search for specific skills and discover all team members who possess them

The three-tier architecture ensures clear separation between data storage, business logic, and presentation layers.
