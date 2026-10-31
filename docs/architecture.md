# Architecture

Mermaid source. In Excalidraw use "Insert > Mermaid to Excalidraw" and paste the diagram block.
Dashed arrows are planned, not built yet.

```mermaid
flowchart LR
    client([Browser / API client])
    efa[(Linz AG EFA server<br/>XML over HTTP)]

    client -- "HTTP, Scalar" --> api

    subgraph api [API edge, per context]
        direction TB
        endpoints[Minimal API endpoints]
    end

    subgraph cluster [Orleans cluster: gleis-grain]
        direction TB
        stops[Stops silo<br/>StopGrain]
        trips[Trips silo<br/>TripGrain]
        alerts[Alerts silo<br/>AlertGrain]
        ingestion[Ingestion silo<br/>SourceGrain<br/>rate limiter + XML parser]
    end

    endpoints --> stops & trips & alerts & ingestion

    stops -. "reminder 1 min,<br/>timer while watched" .-> ingestion
    alerts -. reminder .-> ingestion
    trips -.-> stops
    ingestion -. "XML_TRIP_REQUEST2,<br/>departure monitor" .-> efa

    subgraph pg [Postgres, one container]
        orleansdb[(orleans<br/>membership + reminders)]
        d1[(stops)]
        d2[(trips)]
        d3[(alerts)]
        d4[(ingestion)]
    end

    cluster --> orleansdb
    stops --> d1
    trips --> d2
    alerts --> d3
    ingestion --> d4

    apphost{{Aspire AppHost}} -. starts .-> cluster
    apphost -. starts .-> pg
    cluster -. OTLP .-> dashboard[Aspire dashboard]
```

Silos of different contexts join the same cluster and call each other through the `Contracts` projects only.
