# Migrate database
Run while in the directory Src/OmmelSamvirke.Persistence to add a Migration.

```bash
dotnet ef migrations add <MigrationName> --project ../OmmelSamvirke.Persistence --startup-project ../OmmelSamvirke.API
```

Update database schema with the new migration.

```bash
dotnet ef database update --project ../OmmelSamvirke.Persistence --startup-project ../OmmelSamvirke.API
```

Clear database schema and remove all data.
```bash
dotnet ef database drop --project ../OmmelSamvirke.Persistence --startup-project ../OmmelSamvirke.API
```

Rollback migration.

```bash
dotnet ef migrations remove --project ../OmmelSamvirke.Persistence --startup-project ../OmmelSamvirke.API
```