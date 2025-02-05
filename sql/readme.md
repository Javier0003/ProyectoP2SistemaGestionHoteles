ejecutar en el top level de la aplicacion

```
docker-compose up -d

cd sql

docker exec -it hotel_db bash

/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123 -C -N -i /docker-entrypoint-initdb.d/script.sql
```