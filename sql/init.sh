#!/bin/bash
sleep 30s

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123 -i /docker-entrypoint-initdb.d/script.sql