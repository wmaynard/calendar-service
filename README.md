# Calendar Service
A service for storing and providing information on events.

# Introduction
This service allows for `events` to be stored in a database to be fetched at any time for easy viewing. The main purpose as of
the time of writing is to allow the `Tower Portal` to have an easy way to display planned and ongoing `events`.

An event is constructed of a `title`, a `description`, a `type`, a `start` time, and an `end` time. This is subject to change in the future
on an as needed basis. The current `types` that exist are as follows: `Banners`, `Calendars`, `Events`, `Quests`, `Sales`, and `Leaderboards`.

# Required Environment Variables
|                  Variable | Description                                                                            |
|--------------------------:|:---------------------------------------------------------------------------------------|
|                  GRAPHITE | Link to hosted _graphite_ for analytics and monitoring.                                |
|                LOGGLY_URL | Link to _Loggly_ to analyze logs in greater detail.                                    |
|              MONGODB_NAME | The _MongoDB_ name which the service connects to.                                      |
|               MONGODB_URI | The connection string for the environment's MongoDB.                                   |
|          RUMBLE_COMPONENT | The name of the service.                                                               |
|         RUMBLE_DEPLOYMENT | Signifies the deployment environment.                                                  |
|                RUMBLE_KEY | Key to validate for each deployment environment.                                       |
|   RUMBLE_TOKEN_VALIDATION | Link to current validation for player tokens.                                          |
| RUMBLE_TOKEN_VERIFICATION | Link to current validation for admin tokens. Will include player tokens in the future. |
|           VERBOSE_LOGGING | Logs in greater detail.                                                                |

# Glossary
|              Term | Description                                                                                                                   |
|------------------:|:------------------------------------------------------------------------------------------------------------------------------|
|             Event | An `event` is any type of special scheduled or ongoing promotion or in-game event.                                            |
|             Title | The `title`, or name, of the event.                                                                                           |
|       Description | A `description` that explains what the event is.                                                                              |
|              Type | A classification of the `type` of event. This can be `Banners`, `Calendars`, `Events`, `Quests`, `Sales`, and `Leaderboards`. |
|             Start | A `Unix timestamp` showing when the `event` begins.                                                                           |
|               End | A `Unix timestamp` showing when the `event` ends.                                                                             |

# Using the Service
All non-health endpoints require a valid token from `player-service`. The admin endpoints require a valid admin token.
Requests to these endpoints should have an `Authorization` header with a `Bearer {token}`, where token is the aforementioned `player-service` token.

All `timestamps` in the service are in the format of a `Unix timestamp`. This is to allow consistency and reduce confusion between time zones.

# Endpoints
All endpoints are reached with the base route `/calendar/`. Any following endpoints listed are appended on to the base route.

**Example**: `GET /calendar/events`

## Top Level
No tokens are required for this endpoint.

| Method | Endpoint  | Description                                                            | Required Parameters | Optional Parameters |
|-------:|:----------|:-----------------------------------------------------------------------|:--------------------|:--------------------|
|    GET | `/health` | **INTERNAL** Health check on the status of the relevant microservices. |                     |                     |

## Events
No tokens are required for this endpoint.

| Method | Endpoint       | Description                                          | Required Parameters | Optional Parameters |
|-------:|:---------------|:-----------------------------------------------------|:--------------------|:--------------------|
|    GET | `/events`      | Fetches all events currently stored in the database. |                     |                     |

## Admin
All non-health endpoints require a valid admin token.

| Method | Endpoint        | Description                                                                             | Required Parameters     | Optional Parameters |
|-------:|:----------------|:----------------------------------------------------------------------------------------|:------------------------|:--------------------|
|   POST | `/admin/events` | Clears out the existing event in the database and restores it with the provided events. | *List*<*Event*>`events` |                     |

### Notes
This endpoint is to be hit by the game server whenever there is a new game server build. The data is parsed from CSVs on that side, then the request is constructed.

**Request Example**
```
{
    "events": [
        {
            "title": "Event 1",
            "description": "Event 1 description",
            "type": 1,
            "start": 1664521200,
            "end": 1664607600
        },
        {
            "title": "Event 2",
            "description": "Event 2 description",
            "type": 2,
            "start": 1664607600,
            "end": 1664694000
        }
    ]
}
```

# Future Updates
- The current `Event` model is subject to change and can be modified when needed.
- A repeating property will be added to the `Event` model to prevent having to have the game server manually calculate start and end times.
- It is possible a bad request may clear out the database and fail to create new events. This will be prevented in the future.

# Troubleshooting
- Any issues should be recorded as a log in _Loggly_.