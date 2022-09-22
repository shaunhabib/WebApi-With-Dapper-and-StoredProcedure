# WebApi-With-Dapper-and-StoredProcedure
This is a very simple .Net core Web Api project. In this project I've made two CRUD operation using Dapper. 
  ```
  > One is done by Raw SQL queries and 
  > Another is done by Stored Procedure. 
  ```
  You can clone it and see the proceedings for better understanding. 

## Steps you should know

Step-01: Install These two Nuget package
  ```
   > Dapper
   > MySql.Data
  ```

Step-02: Add a ConnectionString in appsettings.json or appsettings.Development.json file
  ```
  "ConnectionStrings": {
    "DefaultConnection": "server=your server; port=port; database=your-db; user=user name; password=your-password"
  }
  ```
  
Step-04: Add this configuration in your controller for connecting with your DB 
  ```
  private readonly IConfiguration _Config;
  private readonly MySqlConnection _Connection;
  public StudentController(IConfiguration config)
  {
      _Config = config;
      _Connection = new MySqlConnection(_Config.GetConnectionString("DefaultConnection"));
  }
  ```
  
Step-03: Add entity class or ViewModel
  ```
  Property should be same with your fetching data
  ```
  
###### No need to create any Migrations
###### It's kind of code first approch
###### Thats it..very simple
