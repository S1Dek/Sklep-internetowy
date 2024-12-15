## Dokumentacja techniczna
Szczegółowa dokumentacja techniczna projektu znajduje się w pliku: [Dokumentacja techniczna.pdf](https://github.com/S1Dek/Sklep-internetowy/blob/project-finished/Dokumentacja%20techniczna.pdf).

## Baza danych
Projekt wykorzystuje bazę danych SQLite, przechowywaną m.in. w pliku Sklep-internetowy.db.

## Uruchomienie
Aby otworzyć projekt, po pobraniu oraz wypakowaniu, należy skorzystać z pliku Sklep-Internetowy.sln
A następnie uruchomić kompilację.

Jeśli przy uruchomieniu wystąpi błąd bazy danych, należy wykonwać migrację bazy danych:
```bash
Add-migration Test
Update-database
```

### Loginy oraz hasła dla utworzonych użytkowników
W celu tesowania aplikacji, można skorzystać z utworzonych już trzech użytkowników:
| Email             | Hasło | Rola |
| :---------------- | :------: | ----: |
| admin@gmail.com |   zaq1@WSX   | administrator |
| moderator@gmail.com  |   zaq1@WSX  | moderator |
| user@gmail.com   |  zaq1@WSX  | użytknownik |
