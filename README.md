# UAT.PRapidPay Code challenge

Requirements:

* SQL instance (SQL express/Developer are good)
* NET core SDK 7.0

1. Open solution in Visual Studio
2. Deploy database:
     1. Double click on `UATP.RapidPay.SQL.publish.xml`
     2. Click on "Edit" button to configure connection string
     3. Click on "Load Values" button to load SQL CMD variable default values
     4. Set "1" for "SeedTestData" in case you want to create a 'test' user ('test'/'1234'), 0 otherwise (no test user will be created)
     5. Click on "Publish"
3. In `UATP.RapidPay.API/appsettings.Development.json`, set the connection string for `RapidPay` (where database was deployed). 
	Other properties could be changed, but it is not required for the project to work
4. Having set `UATP.RapidPay.API` as the Startup project, click on Run using `IIS Express`

Since authentication method was upgraded to use Bearer Token (JWT), you must login in `POST Users/login` endpoint first.
* If you set "SeedTestData" to "1", you could use user: `test` and password: `1234` as a default user.
* If not, you should create the user in the database, keeping in mind that password is encrypted using AesManaged algorithm (the encrypt key is in the appsettings file)

Once you have your JWT, you could request the other endpoints. 
