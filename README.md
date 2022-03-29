# ef-demo-app
Simple EntityFramework Core example using .NET 6 Minimal APIs

# Description
This is a studying project that shows how to use EF Core and Minimal APIs in .NET 6
This application implements a objective pattern to manage people contact (like email, address...)

#Technologies
In this project you will see how-to configure EF Core and use DBContext to query, insert and delete records. Besides, you can find a sample using global filters and shadow properties;
In API project I decided to use Minimal APIs to test a couple of approaches.

#How to test
You must configure the database connection into the appsettings.json, e.g.:
```
"ConnectionStrings": {
    "Default": "Data Source={YOUR_DATA_SOURCE};Initial Catalog={YOUR_DATABSE_NAME};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  }
```

You also must clone this repository and run the migration command "update-database". Whether you're using Visual Studio, the simplest way is typing Update-Database command on Package Manager Console;
Everything worked? So, you just need to start the application!