# FundaApp
This application is a .NET Core API that returns the top 10 real estate agents (makelaars) in Amsterdam.  
The base data is retrieved from one of the Funda's partner APIs.

In order to run the application, you need to start MongoDB;  
* Go to FundaApp/MongoDBDocker/  
* Run this command: docker-compose -f mongodb.yml up -d

Application reimports the data from the Funda API to local MongoDB every 60 minutes. This can be configured in "appsetings.json" file.  

During the import process which runs at background, application returns response with instant data in the local database.  
You can see importStatus in the output, either in progress or completed.  

Example requests & responses:  

* Top 10 makelaars has most houses without garden:  
HTTP GET - https://localhost:44361/api/topmakelaars  
```json
{"importStatus":"Data importing is finished","lastUpdated":"2018-10-17T21:27:03.952254+02:00","hasGarden":false,
"top10Makelaars":[{"makelaarId":24067,"makelaarName":"Broersma Makelaardij","objCount":91},
{"makelaarId":24605,"makelaarName":"Hallie & Van Klooster Makelaardij","objCount":76},
{"makelaarId":12285,"makelaarName":"Makelaarsland","objCount":68},
{"makelaarId":24079,"makelaarName":"Makelaardij Van der Linden Amsterdam","objCount":68},
{"makelaarId":24783,"makelaarName":"Hoekstra en van Eck Amsterdam West","objCount":67},
{"makelaarId":24705,"makelaarName":"Eefje Voogd Makelaardij","objCount":65},
{"makelaarId":24648,"makelaarName":"Heeren Makelaars","objCount":62},
{"makelaarId":24594,"makelaarName":"Hoekstra en van Eck Amsterdam Noord","objCount":56},
{"makelaarId":24607,"makelaarName":"Makelaar Amsterdam Kuijs Reinder Kakes","objCount":54},
{"makelaarId":24614,"makelaarName":"Nieuw West Makelaardij B.V.","objCount":46}]}
```
* Top 10 makelaars has most houses with garden:  
HTTP GET - https://localhost:44361/api/topmakelaars/tuin  
```json
{"importStatus":"Data importing is finished","lastUpdated":"2018-10-17T21:27:03.952254+02:00","hasGarden":true,
"top10Makelaars":[{"makelaarId":24594,"makelaarName":"Hoekstra en van Eck Amsterdam Noord","objCount":56},
{"makelaarId":24067,"makelaarName":"Broersma Makelaardij","objCount":91},
{"makelaarId":24783,"makelaarName":"Hoekstra en van Eck Amsterdam West","objCount":67},
{"makelaarId":24079,"makelaarName":"Makelaardij Van der Linden Amsterdam","objCount":68},
{"makelaarId":24614,"makelaarName":"Nieuw West Makelaardij B.V.","objCount":46},
{"makelaarId":12285,"makelaarName":"Makelaarsland","objCount":68},
{"makelaarId":24605,"makelaarName":"Hallie & Van Klooster Makelaardij","objCount":76},
{"makelaarId":24131,"makelaarName":"De Graaf & Groot Makelaars","objCount":36},
{"makelaarId":24648,"makelaarName":"Heeren Makelaars","objCount":62},
{"makelaarId":24705,"makelaarName":"Eefje Voogd Makelaardij","objCount":65}]}
```
