## Films Land, a webapi running on .NET 6 
---

> This repository contains an example movie API with anonymous read endpoints and protected create, update, and delete endpoints.

---
#### Project Outline

- Technologies Included
    - .NET 6
    - Sqlite
    - NUnit
    - ef core
    - Microsoft jwtbearer
---
- FilmsAPI project
    - Films Sqlite databse for movie data storage
    - Jwt Sqlite database for user credential storage
- Test
    - Unit tests for FilmsAPi movie and jwt logic
    - Data import/initialization for the databases
---
To spin up FilmsAPI:

- For Visual Studio Code:
    - run the filmsAPi project as .NET core `F5`
        ***or***
    - run the project as a docker container from the filmsapi project folder
    `docker build . -t filmsapi`
    `docker run --name filmsapi -p 8080:80 -d filmsapi`
---
To use the API with provided test data:
- Retrieve the jwt token with a valid user in the Jwt database UserInfo table and a POST to the token endpoint `http://localhost:8080/token` *
- Utilize this security token to make any requests to the Create, Update, or Delete endpoints in an *Authorization - Bearer* header

See full API endpoint documentation at http://localhost:8080/swagger *

_ * *localhost port 8080* is the docker container port set by the the `docker run` command above. This can be a different value set by you or by Visual Studio Code