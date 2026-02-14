# DataImportProj

I’m not sure why, but I decided to create a pet project for importing data using Docker from an old database (artificially created inside a container) into a new one.

Before running and testing my code, make sure you have the following:

1) Development environment — Visual Studio (I’m using v22)

2) Database — MSSQL v20

3) Docker

The project works as follows: first, we’ll need to modify a few things in the files — specifically:

1) Run docker-compose up to create and start the containers (make sure you are in the project folder beforehand).
This will create new containers for the “old” database and a database UI (in this case, I’m using Adminer).

2) Open http://localhost:8080/ and enter the values provided in the file:

Server: legacy_mysql

Username: legacy

Password: legacy

3) After logging in, select the legacydb database, and you will see four tables populated with data.

4) Insert your own email address in the EmailService as the recipient, since the import result will be sent there.

5) Start the project and click Import. If the gods are pleased today, the processes won’t crash.

The result will be visible in the logs and will also be sent to the specified email address.