# fh-organisation-api
API for adding, editing and deleting Local Authority, Voluntary, Charitable and Faith Organisations, their Users and their Roles.

https://www.scottbrady91.com/identity-server/getting-started-with-identityserver-4

Migrations Commands

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add InitialGrantMigration -c PersistedGrantDbContext --output-dir C:\Projects\FamilyHubs\fh-organisation-api\src\FamilyHubs.IdentityServerHost\Persistence\Data\GrantMigrations
dotnet ef migrations add InitialConfigurationMigration -c ConfigurationDbContext --output-dir C:\Projects\FamilyHubs\fh-organisation-api\src\FamilyHubs.IdentityServerHost\Persistence\Data\ConfigurationMigrations
dotnet ef migrations add InitialIdentityMigration -c ApplicationDbContext --output-dir C:\Projects\FamilyHubs\fh-organisation-api\src\FamilyHubs.IdentityServerHost\Persistence\Data\IdentityMigrations

dotnet ef database update -c PersistedGrantDbContext
dotnet ef database update -c ConfigurationDbContext
dotnet ef database update -c ApplicationDbContext
