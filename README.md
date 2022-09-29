# fh-organisation-api
API for adding, editing and deleting Local Authority, Voluntary, Charitable and Faith Organisations, their Users and their Roles.

https://www.scottbrady91.com/identity-server/getting-started-with-identityserver-4

Migrations Commands

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add InitialIdentityServerMigration -c PersistedGrantDbContext --output-dir C:\Projects\FamilyHubs\fh-organisation-api\src\FamilyHubs.IdentityServerHost\Persistence\Data\
dotnet ef migrations add InitialIdentityServerMigration -c ConfigurationDbContext --output-dir C:\Projects\FamilyHubs\fh-organisation-api\src\FamilyHubs.IdentityServerHost\Persistence\Data\
dotnet ef migrations add InitialIdentityServerMigration -c ApplicationDbContext --output-dir C:\Projects\FamilyHubs\fh-organisation-api\src\FamilyHubs.IdentityServerHost\Persistence\Data\

dotnet ef database update -c PersistedGrantDbContext
dotnet ef database update -c ConfigurationDbContext
dotnet ef database update -c ApplicationDbContext
