// One Postgres server, one database per bounded context plus a shared "orleans" database
// for cluster membership and reminders. Image tags are pinned on purpose.
const string PostgresTag = "17.11";
const string PgAdminTag = "9.18";

var builder = DistributedApplication.CreateBuilder(args);

// The data volume keeps the password of its first start, so a new random password on every run
// would lock everyone out. persist: true keeps the generated value in the AppHost user secrets.
var postgresPassword = builder.AddParameter(
    "postgres-password",
    new GenerateParameterDefault { MinLength = 24, Special = false },
    secret: true,
    persist: true);

var postgres = builder.AddPostgres("postgres", password: postgresPassword)
    .WithImageTag(PostgresTag)
    .WithDataVolume("gleis-grain-postgres")
    .WithPgAdmin(pgAdmin => pgAdmin.WithImageTag(PgAdminTag));

var orleans = postgres.AddDatabase("orleans");

AddFeature<Projects.GleisGrain_Stops>("stops");
AddFeature<Projects.GleisGrain_Trips>("trips");
AddFeature<Projects.GleisGrain_Alerts>("alerts");
AddFeature<Projects.GleisGrain_Ingestion>("ingestion");

await builder.Build().RunAsync();

void AddFeature<TProject>(string name)
    where TProject : IProjectMetadata, new()
{
    // The database keeps the plain name because it becomes the connection string name the service reads.
    var database = postgres.AddDatabase(name);

    builder.AddProject<TProject>($"{name}-service")
        .WithReference(orleans)
        .WithReference(database)
        .WaitFor(orleans)
        .WaitFor(database);
}
