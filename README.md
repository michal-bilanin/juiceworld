# JuiceWorld

### Your trustworthy anabolic steroids online store 💪💪. Achieve your dream physique with minimal effort and questionable consequences! 🔥🔥

![Juice World Mascot](assets/grznar.jpg "Achieve your dream physique with minimal effort and questionable consequences!")

## Team "Cibulus 600mg" Members

* Michal Bilanin
* Mário Hatalčík

## Project assignment

Create an electronic commerce platform for the company named "JuiceWorld" with the purpose of selling anabolic steroids.
The website should offer a user-friendly interface, allowing customers to easily navigate through the available
products. Furthermore, the products should possess additional filtering capabilities based on categories (types of
anabolics, types of usage...) and manufacturers. It is desired that customers can log in to their accounts to view their
order history, while administrators should have the ability to modify various aspects of the products, including their
assigned categories and manufacturers.

Moreover, it is requested that the code be made available for retrieval after the completion of the "Milestone 2" phase,
enabling deployment on our test server. The code should be obtained exclusively from the master branch, ensuring its
reliability and stability. Additionally, we expect the application to be optimized for efficient performance. To achieve
this, we require the implementation of a cache mechanism that refreshes every 10 minutes to enhance response times and
overall system efficiency.

## Use Case Diagram

The use case diagram can be found on the project's wiki
here: [https://gitlab.fi.muni.cz/xbilanin/juiceworld/-/wikis/Use-Case-Diagram](https://gitlab.fi.muni.cz/xbilanin/juiceworld/-/wikis/Use-Case-Diagram)

## Entity Relationship Diagram

The entity relationship diagram can be found on the project's wiki
here: [https://gitlab.fi.muni.cz/xbilanin/juiceworld/-/wikis/Entity-Relationship-Diagram](https://gitlab.fi.muni.cz/xbilanin/juiceworld/-/wikis/Entity-Relationship-Diagram)

## Modules of the System

The system consists of the following modules:

1. **BusinessLayer** - responsible for handling the business logic of the application.
2. **BusinessLayer.Tests** - tests for the business layer.
3. **Commons** - contains shared classes and interfaces used across the system.
4. **DataAccessLayer.EF** - responsible for handling database operations, implemented using Entity Framework.
5. **Infrastructure** - contains classes and interfaces used for applying patterns in an implementation-agnostic way.
6. **PresentationLayer.Mvc** - MVC app providing front-end for the user.
7. **TestUtilities.EF** - utilities and test objects regarding the Entity Framework.
8. **WebApi** - responsible for handling HTTP requests and responses.

## Setup For Development

The development environment could be set up either natively (having .NET installed) on the host machine or in a Dev
Container.
The Dev Container can be launched by running:

```bash
docker compose run juiceworld-dev
```

1. Clone the repository
2. Install the required NuGet packages by running the following command in the project's root directory:

```bash
dotnet restore
```

3. Set up your `.env` file in the project's root directory. You can use the provided `.env.example` file as a template.
4. If you're developing in a Dev Container, both the logging and storage database should start automatically. In case it doesn't, or you're not
   developing in the Dev Container, run the databases containers by executing the following command in the project's root
   directory:

```bash
docker compose up postgres mongodb
```

5. After that, you can select one of the `*-development` launch profiles and run the application:

```bash
dotnet run --project WebApi --launch-profile <launch-profile>
```

### Running Migrations

To run the migrations, it is required to set the `DB_CONNECTION_STRING` environment variable before running the
migrations.
Make sure to also set the correct project and startup project. The full command could look like this:

```bash
DB_CONNECTION_STRING="Host=localhost;Port=5432;Database=juiceworld;Username=postgres;Password=postgres" JWT_SECRET='bAafd@A7d9#@F4*V!LHZs#ebKQrkE6pad2f3kj34c3dXy@' dotnet ef migrations add <message> --project DataAccessLayer.EF --startup-project WebApi
```

Make sure to customise the connection string and the migration message to your needs.

### Updating The Database

The database update follows the same principle as running the migrations. The full command could look like this:

```bash
DB_CONNECTION_STRING="Host=localhost;Port=5432;Database=juiceworld;Username=postgres;Password=postgres" JWT_SECRET='bAafd@A7d9#@F4*V!LHZs#ebKQrkE6pad2f3kj34c3dXy@' dotnet ef database update --project DataAccessLayer.EF --startup-project WebApi
```

### Project demo

The final project will be deployed on the following URL: [https://sypku.fetuje.me](https://sypku.fetuje.me)

Default seeded users:

- Admin: `admin@example.com` / `password`
- User: `user@example.com` / `password`
