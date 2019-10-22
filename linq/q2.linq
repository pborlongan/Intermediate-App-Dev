<Query Kind="Expression">
  <Connection>
    <ID>25dab0a5-613e-4076-857a-f35fe862a5a4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

// Question 2
from order in Orders
orderby order.Store.Location ascending // gets the store location that has customers
group order by order.Store.Location into StoreLocation // groups them (gets rid of repeating)
select new{
	Location = StoreLocation.Key,
	Client = from customer in StoreLocation // gets a customer per store location
			 group customer by new { customer.Customer.Address, customer.Customer.City, customer.Customer.Province} into CustomerInformation // creates a group for the customers
			 select new{
			 	Address = CustomerInformation.Key.Address,
				City = CustomerInformation.Key.City,
				Province = CustomerInformation.Key.Province
			 }
}