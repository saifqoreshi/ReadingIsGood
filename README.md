# Reading Is Good
An online book store where users can order books which will be delivered to them
with in 10 minutes. 

## WebAPI template
This api uses asp.net core (.net 5) web api template

## Database
The API uses MS SQL Server (Relational Database) to persist user data e.g Customers, Orders, Books etc.
The Database is called BookStore, configured via **Connection String** in appsettings.json file of the project

## RESTful APIs
All the API endpoints are Restful

## Swagger 
The API uses swagger Open API specification

## Authorization 
All the API endpoints are protected with JWT Bearer Token Authorization and Basic Username, Password Authentication

## API Endpoints
1. api/ReadingIsGood/GetAllCustomers
   * Get a list of all customers 
2. /api/ReadingIsGood/CreateCustomer
   * Create a new customer
3. /api/ReadingIsGood/GetCustomerOrders/{customerId}
   * Get All orders for a particular customer
4. /api/ReadingIsGood/GetOrder/{orderId}
   * Get an order with Id orderId
5. /api/ReadingIsGood/CreateOrder
   * Create a new Order
6. /api/ReadingIsGood/GetAllBooks
   * get a list of all the books available in the store
7. /api/ReadingIsGood/authenticate
   * Use this method to generate a token for subsequent requests to API endpoints.

## How to Authenticate?
Please call authenticate "**/api/readingisgoodauthentica**" api with username and password as test1:password1 to get a key
Use authorize button and add the key to login
After login all API endpoints should be accessible.

## Deployed on Azure
 * The API is deployed on Azure. Click [Online API](https://readingisgoodapi.azurewebsites.net/swagger/index.html) to Navigate to the API.
