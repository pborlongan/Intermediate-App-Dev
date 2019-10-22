<Query Kind="Expression">
  <Connection>
    <ID>25dab0a5-613e-4076-857a-f35fe862a5a4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

// Question 6
from customer in Customers
where customer.CustomerID == 1
select new{
	Customer = customer.LastName + ", " + customer.FirstName,
	OrdersCount = customer.Orders.Count(),
	Items = from order in customer.Orders
			from orderList in order.OrderLists
			group orderList by orderList.Product.Description into ItemDescription
			select new 
			{
				Description = ItemDescription.Key,
				TimesBought = ItemDescription.Count()
			}
}