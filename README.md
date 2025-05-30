# Project Structure

- API
- Application
- Core
- Infrastructure

---
## 🌍 Core

- 🏢 Projekt: HRLeaveManagement.Application
  - ❔ Logika aplikacyjna: use cases, serwisy aplikacyjne, interfejsy kontraktowe do komunikacji z warstwami zewnętrznymi.

  - 📁 Contracts
    - ❔ Definicje interfejsów, DTOs i komunikatów, które służą do komunikacji między różnymi warstwami systemu lub z systemami zewnętrznymi, zapewniając luźne powiązanie i czytelne kontrakty pomiędzy komponentami.

    - 📁 Email
      - ❔ Interfejs do wysyłania maili.
      - 🗒️ `IEmailSender.cs`
        
    - 📁 Logging
      - ❔ Interfejs do logowania (błędów, notyfikacji).
      - 🗒️ `IAppLogger.cs`

    - 📁 Persistence
      - ❔ Interfejsy repozytoriów definiujące kontrakty dostępu do danych dla warstwy aplikacji.
      - 🗒️ `IGenericRepository.cs`
      - 🗒️ `ILeaveTypeRepository.cs`

  - 📁 Exceptions
    - ❔ Klasy wyjątków reprezentujące błędy aplikacyjne, np. walidacji czy braku zasobu.
    - 🗒️ `BadRequestException.cs`

  - 📁 Features
    - ❔ Grupuje przypadki użycia (use cases) według funkcjonalności systemu i dzieli je na Commands i Queries, implementując logikę obsługi żądań (CQRS) przy użyciu MediatR.
      
    - 📁 Commands
      - ❔ Modyfikacje danych (Create, Update, Delete).
        
      - 📁 CreateLeaveType
        - 🗒️ `CreateLeaveTypeCommand.cs`
          - ❔ Struktura danych wyjściowych.
        - 🗒️ `CreateLeaveTypeCommandHandler.cs`
          - ❔ Logika wykonania żądania.
        - 🗒️ `CreateLeaveTypeCommandValidator.cs`
          - ❔ Walidacja danych wejściowych.
            
      - 📁 DeleteLeaveType
        
      - 📁 UpdateLeaveType
        
    - 📁 Queries
      - 📁 GetAllLeaveTypes
      - 📁 GetLeaveTypeDetails

  - 📁 MappingProfiles
    - ❔ Konfiguracje AutoMappera do mapowania między encjami domenowymi a obiektami DTO, wykorzystywane w komendach i zapytaniach aplikacji.
      - 🗒️ `LeaveTypeProfile.cs`

  - 📁 Models
    - ❔ Zawiera klasy pomocnicze reprezentujące dane konfiguracyjne lub struktury komunikatów.
    -  📁 Email
      -  🗒️ `EmailMessage.cs`
      -  🗒️ `EmailSettings.cs`
        
  -  🗒️ `ApplicationServiceRegistration.cs`
    -  ❔ Rejestruje w DI komponenty aplikacyjne, np. AutoMapper, MediatR, validatory i inne zależności z warstwy Application.

---
## 🌍 Infrastructure

- 🏢 Projekt: HRLeaveManagement.Infrastructure
  - ❔ Implementuje techniczne szczegóły i zależności zewnętrzne, np. wysyłkę e-maili, logowanie, integracje z API itp.
    
  - 📁 EmailService
    - ❔ Zawiera implementację usługi do wysyłania e-maili.
      - 🗒️ `EmailSender.cs`
      
  - 📁 Logging
    - ❔ Zawiera adapter do logowania.
      - 🗒️ `LoggerAdapter.cs`
      
  -  🗒️ `InfrastructureServiceRegistration.cs`
    -  ❔ Plik rejestrujący usługi infrastruktury w kontenerze DI.

- 🏢 Projekt: HRLeaveManagement.Persistence
  - ❔ Odpowiada za implementację dostępu do danych (np. za pomocą Entity Framework) i zawiera klasy repozytoriów, konfigurację bazy danych oraz wszelkie powiązane ustawienia.
  
  - 📁 Configurations
    - ❔ Zawiera konfiguracje encji, które są stosowane w modelu danych.
      - 🗒️ `LeaveTypeConfiguration.cs`

  - 📁 DatabaseContext
    - ❔ Zawiera kontekst bazy danych (DbContext), który łączy aplikację z bazą danych.
      - 🗒️ `HRDatabaseContext.cs`

  - 📁 Repositories
    - ❔ Zawiera implementacje repozytoriów, które służą jako warstwa dostępu do danych w aplikacji.
      - 🗒️ `GenericRepository.cs`
      - 🗒️ `LeaveAlocationRepository.cs`
      - 🗒️ `LeaveTypeRepository.cs`
      
  - 🗒️ `PersistenceServiceRegistration.cs`
    - ❔ Rejestruje wszystkie usługi związane z dostępem do danych w kontenerze DI.
