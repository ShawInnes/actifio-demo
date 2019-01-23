#!/bin/sh

CONTAINER="actifio-demo_sqlserver_1"
CACHED="$HOME/ISO/WideWorldImporters-Full.bak"
PASSWORD='demo01!password'

docker exec -it $CONTAINER mkdir /var/opt/mssql/backup

# wget -c -O "$CACHED" 'https://github.com/Microsoft/sql-server-samples/releases/download/wide-world-importers-v1.0/WideWorldImporters-Full.bak'

docker cp "$CACHED" $CONTAINER:/var/opt/mssql/backup

docker exec -it $CONTAINER /opt/mssql-tools/bin/sqlcmd -S localhost \
   -U SA -P "$PASSWORD" \
   -Q 'RESTORE FILELISTONLY FROM DISK = "/var/opt/mssql/backup/WideWorldImporters-Full.bak"' \
   | tr -s ' ' | cut -d ' ' -f 1-2


docker exec -it $CONTAINER /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P "$PASSWORD" \
   -Q 'RESTORE DATABASE WideWorldImporters FROM DISK = "/var/opt/mssql/backup/WideWorldImporters-Full.bak" WITH MOVE "WWI_Primary" TO "/var/opt/mssql/data/WideWorldImporters.mdf", MOVE "WWI_UserData" TO "/var/opt/mssql/data/WideWorldImporters_userdata.ndf", MOVE "WWI_Log" TO "/var/opt/mssql/data/WideWorldImporters.ldf", MOVE "WWI_InMemory_Data_1" TO "/var/opt/mssql/data/WideWorldImporters_InMemory_Data_1"'


docker exec -it $CONTAINER /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P "$PASSWORD" \
   -Q "SELECT Name FROM sys.Databases"
