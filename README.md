# ReserveMe


## Base setup for run (development mode)

1. Pull mysql image

        docker pull mysql

2. Pull rabbitmq image

        docker pull rabbitmq

3. Pull Mongo image

        docker pull mongo

4. Run MySql server in docker
    Default password for mysql in development mode is `strong!password12`

        docker run --name mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD='strong!password12' -d mysql:latest

5. Run MongoDb server in docker

        docker run --name mongo -p 27017:27017 -d mongo:latest

6. Run RabbitMq server in docker

        docker run --name rabbitmq -p 5672:5672 -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=guest -d rabbitmq:latest

7. Run migrations

    1. Install dotnet ef tools

        https://learn.microsoft.com/en-us/ef/core/cli/dotnet
    2. Run Migrations go to `Backend/Components/User/User.Infrastructure/Persistence/Migrations` and run :


           dotnet ef database update -c UserContext -s ../../../User.Api