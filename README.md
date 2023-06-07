# Ommel Samvirke | Backend Repository

This repository contains the backend code for Ommel Samvirke.
The backend code will be used by both web and mobile frontends.

The architecture of this repository follow the [Clean Architecture](https://medium.com/dotnet-hub/clean-architecture-with-dotnet-and-dotnet-core-aspnetcore-overview-introduction-getting-started-ec922e53bb97), 
also known as the Onion Architecture. In the Clean Architecture, the solution is
split into multiple layers:
- API
- Infrastructure & Persistence
- Application
- Domain

The main rule of the Clean Architecture is that dependencies must only flow inwards.
For example, the Application layer can depend on the Domain layer, but the Domain
layer cannot depend on the Application layer.

## Functionality
The backend aims to provide much functionality through the top layer of the
architecture, which is a REST API. To improve the navigability of the repository,
the [feature folder](https://medium.com/c2-group/simplifying-projects-with-a-folder-by-feature-structure-3a13cff2d28c) 
pattern is used _within_ each layer to separate and detangle logic for different
feature sets.

The feature sets provided by this solution are:
- Newsletter management
- Reservations for _Beboerhuset_
- Event management
- Forum services
- Logging
- Alerts
- Analytics
- Search
- CMS services
- Photo upload and management
- Album management
