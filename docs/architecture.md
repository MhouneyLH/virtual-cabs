# Architecture

Mermaid source. In Excalidraw use "Insert > Mermaid to Excalidraw" and paste the diagram block.
Dashed arrows are planned, not built yet.

```mermaid
flowchart LR
    client([Browser / API client])
    efa[(Linz AG EFA server<br/>XML over HTTP)]

    client -- "HTTP, Scalar" --> host

    subgraph host [GleisGrain.Host: one process, one Orleans silo]
        direction TB
        subgraph modules [Modules]
            direction LR
            stops[Stops<br/>StopGrain]
            trips[Trips<br/>TripGrain]
            alerts[Alerts<br/>AlertGrain]
            ingestion[Ingestion<br/>SourceGrain<br/>rate limiter + XML parser]
        end
        common[Common: HTTP, Orleans, OpenTelemetry]
    end

    stops -. "via Contracts: reminder 1 min,<br/>timer while watched" .-> ingestion
    alerts -. "via Contracts: reminder" .-> ingestion
    trips -. "via Contracts" .-> stops
    ingestion -. "XML_TRIP_REQUEST2,<br/>departure monitor" .-> efa

    subgraph pg [Postgres, one container]
        orleansdb[(orleans<br/>membership + reminders)]
        d1[(stops)]
        d2[(trips)]
        d3[(alerts)]
        d4[(ingestion)]
    end

    host --> orleansdb
    stops --> d1
    trips --> d2
    alerts --> d3
    ingestion --> d4

    apphost{{Aspire AppHost}} -. starts .-> host
    apphost -. starts .-> pg
    host -. OTLP .-> dashboard[Aspire dashboard]
```

Modules call each other through the `Contracts` projects only. Several replicas of the host form one Orleans cluster of identical silos. Splitting a module into its own deployable later needs only a small host that calls its `Add...Module` and `Map...Endpoints`.
