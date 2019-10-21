<Query Kind="Statements">
  <Connection>
    <ID>25dab0a5-613e-4076-857a-f35fe862a5a4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

// Question 1
from product in Products
orderby product.OrderLists.Count() descending, product.Description ascending
select new {
	Product = product.Description,
	TimesPurchased = product.OrderLists.Count()
}

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

// Question 3
from store in Stores
orderby store.City ascending, store.Location ascending
select new{
	City = store.City,
	Location = store.Location,
	Sales = from sale in store.Orders
			where sale.OrderDate.Month == 12 // specific month
			group sale by sale.OrderDate into SalesInformation
			select new{
				Date = SalesInformation.Key,
				NumberOfOrders = SalesInformation.Count(), //counts the grouping inside the group
				ProductSales = SalesInformation.Sum(x => x.SubTotal),
				GST = SalesInformation.Sum(x => x.GST)
			}
}