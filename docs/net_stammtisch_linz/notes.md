# .NET Stammtisch Linz

How this repo covers each part of the talk. `[TBD]` = not built yet.
Time: up to 45 min. Discussion matters more than a finished demo, so most `[TBD]` items can stay as slides or talking points.
Contexts: Stops, Trips, Alerts, Ingestion, see the README. Data: Linz AG EFA XML.

## Why actors
- One grain per aggregate: `StopGrain`, `TripGrain`, `AlertGrain`, `SourceGrain`.
- Virtual actor in action: endpoints call `grains.GetGrain<IStopGrain>(id)`, no create or lifecycle code (`Endpoints/GetStop.cs`).
- A stop that nobody asks about costs nothing: [TBD] show activation and deactivation in the dashboard.

## Grains and state
- `IPersistentState<StopState>` with a named storage per context, backed by Postgres.
- Domain class (`Stop`) is plain C#, the grain is a thin wrapper.
- Departure board as state, parsed from the XML: [TBD]
- Write-behind instead of persisting every refresh: [TBD]

## Timers vs reminders
- Reminder service is wired (ADO.NET, shared `orleans` database).
- Reminder every minute per stop keeps the board warm, survives a silo crash: [TBD]
- Timer every 10-15 s while somebody watches the stop, dies with the activation: [TBD]

## Concurrency and hot spots
- Linz Hbf is the hot grain. `[StatelessWorker]` or cached snapshot for reads: [TBD]
- `[ReadOnly]`, `[Reentrant]`, deadlock example (Trip <-> Stop): [TBD]
- Rate limiter grain in Ingestion, so hundreds of reminders do not hammer the API: [TBD]

## Cluster
- One cluster, one silo per context. Membership and reminders in the `orleans` database. Demo query: `SELECT * FROM orleansmembershiptable;` in pgAdmin.
- Orleans SQL schema scripts loaded into Postgres: [TBD] (needed before anything runs)
- Run 3 silos of one context, kill one, the reminder still fires: [TBD]

## Versioning
- `[GenerateSerializer]`, `[Id(n)]`, `[Alias]` on every state, snapshot, id and grain interface.
- `SchemaVersion` field in every state class. Migration in `OnActivateAsync`: [TBD]
- `[Version(2)]` and a rolling v2 (for example a platform field on `StopGrain`): [TBD]

## Kubernetes and load test
- `UseKubernetesHosting()`, probes, grace period: [TBD]
- NBomber load test with a replay of recorded XML (never load test the real API): [TBD]

## Extras
- Aspire AppHost with Postgres, pgAdmin and one service per context. Done.
- OpenTelemetry: Orleans meter and activity sources are exported. A real Stop -> Ingestion trace needs those calls: [TBD]
- Static departure page later: [TBD]
- AI agent that explains a delay from the notices: [TBD]

## Discussion
- Where would grains hurt you? When is a plain cache and a cron job the better answer?
- One cluster for all contexts or one per context?
- Reminders or a job scheduler such as Quartz or Hangfire?
- Grain state is a blob. Accept that or project into your own tables?
- How do you roll out a breaking state change safely?
- Is polling a third party API from many grains polite? Where does the rate limit live?
