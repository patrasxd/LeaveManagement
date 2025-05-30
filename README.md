# Project Structure

This is a Clean Architecture solution for HR Leave Management system, organized into the following main areas:

- API: RESTful API endpoints for client communication
- Application: Business logic and use cases
- Core: Domain entities and business rules
- Infrastructure: External concerns and technical implementations
- UI: Blazor WebAssembly frontend
- Tests: Unit and integration tests

---
## 🌍 Core

- 🏢 Project: HRLeaveManagement.Domain
  - ❔ Contains domain entities, value objects, and business rules that represent the core business concepts.

  - 📁 Common
    - ❔ Base classes and shared domain entities that provide common functionality across the domain.
    - 🗒️ `BaseDomainEntity.cs`
      - ❔ Abstract base class providing common properties like Id, DateCreated, and LastModifiedDate.

  - 🗒️ `LeaveType.cs`
    - ❔ Domain entity representing different types of leave (e.g., vacation, sick leave) with properties like name, default days.
  - 🗒️ `LeaveRequest.cs`
    - ❔ Domain entity for employee leave requests, containing request details, dates, approval status.
  - 🗒️ `LeaveAllocation.cs`
    - ❔ Domain entity managing how leave days are allocated to employees for specific leave types.

- 🏢 Project: HRLeaveManagement.Application
  - ❔ Contains all application business logic and use cases. Implements CQRS pattern using MediatR for clean separation of commands and queries.

  - 📁 Contracts
    - ❔ Interface definitions establishing boundaries between layers and defining how components interact.

    - 📁 Email
      - ❔ Email service abstraction for sending notifications.
      - 🗒️ `IEmailSender.cs`
        - ❔ Interface defining methods for sending emails in the application.
        
    - 📁 Logging
      - ❔ Logging abstraction for consistent logging across the application.
      - 🗒️ `IAppLogger.cs`
        - ❔ Interface for application-wide logging with structured logging support.

    - 📁 Persistence
      - ❔ Data access abstractions following repository pattern.
      - 🗒️ `IGenericRepository.cs`
        - ❔ Generic repository interface with common CRUD operations.
      - 🗒️ `ILeaveTypeRepository.cs`
        - ❔ Specialized repository for leave type operations.
      - 🗒️ `ILeaveRequestRepository.cs`
        - ❔ Specialized repository for leave request operations.
      - 🗒️ `ILeaveAllocationRepository.cs`
        - ❔ Specialized repository for leave allocation operations.

  - 📁 Exceptions
    - ❔ Custom exception types for specific application error scenarios.
    - 🗒️ `BadRequestException.cs`
      - ❔ Thrown when request validation fails.
    - 🗒️ `NotFoundException.cs`
      - ❔ Thrown when requested resource doesn't exist.
    - 🗒️ `ValidationException.cs`
      - ❔ Thrown when business rule validation fails.

  - 📁 Features
    - ❔ Application features organized by domain concept, implementing CQRS pattern.
      
    - 📁 LeaveTypes
      - 📁 Commands
        - 📁 CreateLeaveType
          - 🗒️ `CreateLeaveTypeCommand.cs`
            - ❔ Command DTO for creating new leave type.
          - 🗒️ `CreateLeaveTypeCommandHandler.cs`
            - ❔ Handler implementing creation logic.
          - 🗒️ `CreateLeaveTypeCommandValidator.cs`
            - ❔ Validator ensuring command data meets requirements.
        - 📁 DeleteLeaveType
          - ❔ Command and handler for leave type deletion.
        - 📁 UpdateLeaveType
          - ❔ Command and handler for leave type updates.
      - 📁 Queries
        - 📁 GetAllLeaveTypes
          - ❔ Query and handler for retrieving all leave types.
        - 📁 GetLeaveTypeDetails
          - ❔ Query and handler for detailed leave type information.

    - 📁 LeaveRequests
      - 📁 Commands
        - ❔ Create, update, cancel, and approve/reject leave requests.
      - 📁 Queries
        - ❔ Retrieve leave requests with various filters.

    - 📁 LeaveAllocations
      - 📁 Commands
        - ❔ Create and update leave allocations for employees.
      - 📁 Queries
        - ❔ Retrieve allocation information and balances.

  - 📁 MappingProfiles
    - ❔ AutoMapper configurations for object-to-object mapping.
    - 🗒️ `LeaveTypeProfile.cs`
      - ❔ Mapping configuration for leave type entities and DTOs.
    - 🗒️ `LeaveRequestProfile.cs`
      - ❔ Mapping configuration for leave request entities and DTOs.
    - 🗒️ `LeaveAllocationProfile.cs`
      - ❔ Mapping configuration for leave allocation entities and DTOs.

  - 📁 Models
    - ❔ DTOs and value objects used across the application.
    - 📁 Email
      - 🗒️ `EmailMessage.cs`
        - ❔ Model representing email message structure.
      - 🗒️ `EmailSettings.cs`
        - ❔ Configuration model for email service settings.
    - 📁 Identity
      - ❔ Models related to authentication and authorization.
    - 📁 DTOs
      - ❔ Data transfer objects for API communication.
        
  - 🗒️ `ApplicationServiceRegistration.cs`
    - ❔ Configures application layer services in DI container including AutoMapper, MediatR, and validators.

---
## 🌍 Infrastructure

- 🏢 Project: HRLeaveManagement.Infrastructure
  - ❔ Implements infrastructure concerns and external service integrations.
    
  - 📁 EmailService
    - 🗒️ `EmailSender.cs`
      - ❔ Implements email sending using SMTP or other email service providers.
      
  - 📁 Logging
    - 🗒️ `LoggerAdapter.cs`
      - ❔ Implements logging interface using a specific logging provider.
      
  - 🗒️ `InfrastructureServiceRegistration.cs`
    - ❔ Registers infrastructure services in the DI container.

- 🏢 Project: HRLeaveManagement.Persistence
  - ❔ Implements data access layer using Entity Framework Core.
  
  - 📁 Configurations
    - ❔ Entity Framework configuration classes for database schema.
    - 🗒️ `LeaveTypeConfiguration.cs`
      - ❔ Database configuration for leave types.
    - 🗒️ `LeaveRequestConfiguration.cs`
      - ❔ Database configuration for leave requests.
    - 🗒️ `LeaveAllocationConfiguration.cs`
      - ❔ Database configuration for leave allocations.

  - 📁 DatabaseContext
    - 🗒️ `HRDatabaseContext.cs`
      - ❔ Entity Framework DbContext defining database structure and relationships.

  - 📁 Repositories
    - ❔ Implementation of repository interfaces defined in Application layer.
    - 🗒️ `GenericRepository.cs`
      - ❔ Base repository implementation with common CRUD operations.
    - 🗒️ `LeaveAllocationRepository.cs`
      - ❔ Repository implementation for leave allocation operations.
    - 🗒️ `LeaveTypeRepository.cs`
      - ❔ Repository implementation for leave type operations.
    - 🗒️ `LeaveRequestRepository.cs`
      - ❔ Repository implementation for leave request operations.
      
  - 🗒️ `PersistenceServiceRegistration.cs`
    - ❔ Configures database context and repositories in DI container.

- 🏢 Project: HRLeaveManagement.Identity
  - ❔ Handles user authentication, authorization, and identity management.

  - 📁 Configurations
    - 🗒️ `UserConfiguration.cs`
      - ❔ Configuration for ASP.NET Identity user settings.
    - 🗒️ `RoleConfiguration.cs`
      - ❔ Configuration for application roles and permissions.

  - 📁 Models
    - 🗒️ `ApplicationUser.cs`
      - ❔ Custom user class extending IdentityUser with additional properties.

  - 📁 Services
    - 🗒️ `AuthService.cs`
      - ❔ Implements authentication logic including JWT token generation.

  - 🗒️ `IdentityServiceRegistration.cs`
    - ❔ Configures Identity services and JWT authentication.

---
## 🌍 API

- 🏢 Project: HRLeaveManagement.Api
  - ❔ ASP.NET Core Web API project exposing HTTP endpoints for client applications.

  - 📁 Controllers
    - 🗒️ `LeaveTypesController.cs`
      - ❔ API endpoints for managing leave types.
    - 🗒️ `LeaveRequestsController.cs`
      - ❔ API endpoints for managing leave requests.
    - 🗒️ `LeaveAllocationsController.cs`
      - ❔ API endpoints for managing leave allocations.

  - 📁 Middleware
    - 🗒️ `ExceptionMiddleware.cs`
      - ❔ Global exception handling and error response formatting.

  - 📁 Models
    - 🗒️ `CustomProblemDetails.cs`
      - ❔ Custom error response model following RFC 7807.

  - 🗒️ `Program.cs`
    - ❔ Application startup and configuration.
  - 🗒️ `appsettings.json`
    - ❔ Application configuration including connection strings and JWT settings.

---
## 🌍 UI

- 🏢 Project: HRLeaveManagement.BlazorUI
  - ❔ Blazor WebAssembly frontend providing user interface for the application.

  - 📁 Pages
    - 📁 LeaveTypes
      - ❔ Components for managing leave types (create, edit, list).
    - 📁 LeaveRequests
      - ❔ Components for submitting and managing leave requests.
    - 📁 LeaveAllocations
      - ❔ Components for viewing and managing leave allocations.

  - 📁 Services
    - ❔ Client-side services for API communication.
    - 🗒️ `LeaveTypeService.cs`
      - ❔ Service for leave type operations.
    - 🗒️ `LeaveRequestService.cs`
      - ❔ Service for leave request operations.
    - 🗒️ `LeaveAllocationService.cs`
      - ❔ Service for leave allocation operations.

  - 📁 Contracts
    - ❔ Interface definitions for client services.
    - 🗒️ `ILeaveTypeService.cs`
      - ❔ Contract for leave type operations.
    - 🗒️ `ILeaveRequestService.cs`
      - ❔ Contract for leave request operations.
    - 🗒️ `ILeaveAllocationService.cs`
      - ❔ Contract for leave allocation operations.

  - 📁 Models
    - ❔ Client-side view models and DTOs.
    - 📁 LeaveTypes
      - ❔ Models for leave type operations.
    - 📁 LeaveRequests
      - ❔ Models for leave request operations.
    - 📁 LeaveAllocations
      - ❔ Models for leave allocation operations.

  - 📁 Providers
    - 🗒️ `ApiAuthenticationStateProvider.cs`
      - ❔ Custom authentication state provider for JWT handling.

  - 📁 Handlers
    - 🗒️ `JwtAuthenticationHandler.cs`
      - ❔ Handler for JWT authentication in API requests.

---
## 🌍 Tests

- 🏢 Project: HRLeaveManagement.Application.UnitTests
  - ❔ Unit tests for application layer logic using xUnit.

  - 📁 Features
    - 📁 LeaveTypes
      - ❔ Tests for leave type commands and queries.
    - 📁 LeaveRequests
      - ❔ Tests for leave request commands and queries.
    - 📁 LeaveAllocations
      - ❔ Tests for leave allocation commands and queries.

  - 📁 Mocks
    - ❔ Mock implementations of interfaces for testing.

- 🏢 Project: HRLeaveManagement.Persistence.IntegrationTests
  - ❔ Integration tests for data access layer using test database.

  - 📁 DatabaseContextTests
    - ❔ Tests for Entity Framework configurations and operations.
  - 📁 Repositories
    - ❔ Tests for repository implementations.
