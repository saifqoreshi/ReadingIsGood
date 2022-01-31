# Reading Is Good
An online book store where users can order books which will be delivered to them
with in 10 minutes. 

# WebAPI template
This api uses asp.net core (.net 5) web api template

# Database
The API uses MS SQL Server (Relational Database) to persist user data e.g Customers, Orders, Books etc.
The Database is called BookStore, configured via **Connection String** in appsettings.json file of the project

# RESTful APIs
All the API endpoints are Restful

# Swagger 
The API uses swagger Open API specification

# Authorization 
All the API endpoints are protected with JWT Bearer Token Authorization and Basic Username, Password Authentication

# API Endpoints
* Create Customer (Post), creates a customer with FirstName, LastName, Email, PhoneNumber and a Shipping Address
* GetCustomerOrders (GET), gets all the orders for a given customer (customerId)
* CreateOrder (POST), creates a new order using customerId, and an array of items that correspond to OrderDetails/Books, i.e BookId, Quantity
* GetOrder (GET), List the details of a particular order (OrderId)
