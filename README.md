## Configuration steps
Install the MySQL entity framework package (or any DB framework package that you work with).

Execute in a terminal (replace the connection string with the one suitable for you DB if you are not using MySQL):
```Shell
dotnet user-secrets init
DB_PASSWORD="Your DB password here"
dotnet user-secrets set "ConnectionStrings:ProjectContext" "server=localhost;port=3306;database=orders;uid=root;password=$DB_PASSWORD"
```
Then migrate changes to the database and use the application normally.

## Next steps
The program can be much improved by:
- Converting all functions to async
- Implementing pagination in get methods
- Writing unit tests
But this is out of the scope of this simple demonstration.

____________________________________________________

## Шаги настройки
Установите пакет MySQL entity framework (или любой другой пакет DB framework, с которым вы работаете).

Запустите в терминале (замените строку подключения на подходящую для вас DB, если вы не используете MySQL):
```Shell
dotnet user-secrets init
DB_PASSWORD="Your DB password here"
dotnet user-secrets set "ConnectionStrings:ProjectContext" "server=localhost;port=3306;database=orders;uid=root;password=$DB_PASSWORD"
```
Затем сделайте миграции изменения в базу данных и нормально используйте приложение.

## Следующие шаги
Программа может быть значительно улучшена таким образом:
- Преобразование всех функций в async
- Реализация пагинации в методах get
- Написание модульных тестов
Но это выходит за рамки данной простой демонстрации.