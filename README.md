# Project Structure

- API
- Application
- Core
- Infrastructure

---
## 🌍 Core

- 🏢 Project: HRLeaveManagement.Application
  - ❔ Application logic: use cases, application services, contract interfaces for communication with external layers.

  - 📁 Contracts
    - ❔ Definitions of interfaces, DTOs, and messages used for communication between different layers of the system or with external systems, ensuring loose coupling and clear contracts between components.

    - 📁 Email
      - ❔ Interface for sending emails.
      - 🗒️ `IEmailSender.cs`
        
    - 📁 Logging
      - ❔ Interface for logging (errors, notifications).
      - 🗒️ `IAppLogger.cs`

    - 📁 Persistence
      - ❔ Repository interfaces defining data access contracts for the application layer.
      - 🗒️ `IGenericRepository.cs`
      - 🗒️ `ILeaveTypeRepository.cs`

  - 📁 Exceptions
    - ❔ Exception classes representing application errors, e.g., validation or missing resource.
    - 🗒️ `BadRequestException.cs`

  - 📁 Features
    - ❔ Groups use cases according to system functionality and divides them into Commands and Queries, implementing request handling logic (CQRS) using MediatR.
      
    - 📁 Commands
      - ❔ Data modifications (Create, Update, Delete).
        
      - 📁 CreateLeaveType
        - 🗒️ `CreateLeaveTypeCommand.cs`
          - ❔ Output data structure.
        - 🗒️ `CreateLeaveTypeCommandHandler.cs`
          - ❔ Request execution logic.
        - 🗒️ `CreateLeaveTypeCommandValidator.cs`
          - ❔ Input data validation.
            
      - 📁 DeleteLeaveType
        
      - 📁 UpdateLeaveType
        
    - 📁 Queries
      - 📁 GetAllLeaveTypes
      - 📁 GetLeaveTypeDetails

  - 📁 MappingProfiles
    - ❔ AutoMapper configurations for mapping between domain entities and DTO objects, used in application commands and queries.
      - 🗒️ `LeaveTypeProfile.cs`

  - 📁 Models
    - ❔ Contains helper classes representing configuration data or message structures.
    -  📁 Email
      -  🗒️ `EmailMessage.cs`
      -  🗒️ `EmailSettings.cs`
        
  -  🗒️ `ApplicationServiceRegistration.cs`
    -  ❔ Registers application components in DI, e.g., AutoMapper, MediatR, validators, and other dependencies from the Application layer.

---
## 🌍 Infrastructure

- 🏢 Project: HRLeaveManagement.Infrastructure
  - ❔ Implements technical details and external dependencies, e.g., sending emails, logging, API integrations, etc.
    
  - 📁 EmailService
    - ❔ Contains the implementation of the email sending service.
      - 🗒️ `EmailSender.cs`
      
  - 📁 Logging
    - ❔ Contains the logging adapter.
      - 🗒️ `LoggerAdapter.cs`
      
  -  🗒️ `InfrastructureServiceRegistration.cs`
    -  ❔ File registering infrastructure services in the DI container.

- 🏢 Project: HRLeaveManagement.Persistence
  - ❔ Responsible for implementing data access (e.g., using Entity Framework) and contains repository classes, database configuration, and any related settings.
  
  - 📁 Configurations
    - ❔ Contains entity configurations used in the data model.
      - 🗒️ `LeaveTypeConfiguration.cs`

  - 📁 DatabaseContext
    - ❔ Contains the database context (DbContext) that connects the application to the database.
      - 🗒️ `HRDatabaseContext.cs`

  - 📁 Repositories
    - ❔ Contains implementations of repositories that serve as the data access layer in the application.
      - 🗒️ `GenericRepository.cs`
      - 🗒️ `LeaveAlocationRepository.cs`
      - 🗒️ `LeaveTypeRepository.cs`
      
  - 🗒️ `PersistenceServiceRegistration.cs`
    - ❔ Registers all data access-related services in the DI container.
