# Ommel Samvirke | Domain

The Domain layer is the innermost layer of the architecture for the backend of
Ommel Samvirke. The Domain layer contains:
- Models: Core business object such as newsletters, events, albums, etc.
- Interfaces: Definitions for repositories and services.
- Exception: Custom exception that relate to the business domain.
- Enums: Enumerations for business values. Example: Log-levels for log messages. 
- Value Objects: Classes used to enforce validation and encapsulation around 
simple types such as names, phone numbers and email addresses.