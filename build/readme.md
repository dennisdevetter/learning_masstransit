Local dev:

- run "docker compose up" to boot up the postgress database + rabbitmq
- go to dotnet ./LearningMassTransit.DataAccess and run "dotnet ef database update"
- then start the api, it will automatically create the database + schemas