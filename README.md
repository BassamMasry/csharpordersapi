## Configuration steps
Install the MySQL entity framework package (or any DB framework package that you work with).
Execute in a terminal (replace the connection string with the one suitable for you DB if you are not using MySQL)
```Shell
dotnet user-secrets init
DB_PASSWORD="Your DB password here"
dotnet user-secrets set "ConnectionStrings:ProjectContext" "server=localhost;port=3306;database=orders;uid=root;password=$DB_PASSWORD"
```
Then migrate changes to the database and use the application normally.