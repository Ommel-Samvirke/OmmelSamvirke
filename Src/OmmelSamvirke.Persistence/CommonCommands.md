# Migrate database
Run while in the directory Src/OmmelSamvirke.Persistence to add a Migration.

```bash
dotnet ef migrations add <MigrationName> --project ../OmmelSamvirke.Persistence --startup-project ../OmmelSamvirke.API
```

Update database schema with the new migration.

```bash
dotnet ef database update --project ../OmmelSamvirke.Persistence --startup-project ../OmmelSamvirke.API
```
