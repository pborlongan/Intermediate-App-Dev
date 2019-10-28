<Query Kind="Expression">
  <Connection>
    <ID>dd6fc7dd-8c72-476d-b005-327e10789d86</ID>
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
			orderby ItemDescription.Count() descending, ItemDescription.Key
			select new 
			{
				Description = ItemDescription.Key,
				TimesBought = ItemDescription.Count()
			}
}