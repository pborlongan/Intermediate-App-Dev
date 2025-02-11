<Query Kind="Expression">
  <Connection>
    <ID>dd6fc7dd-8c72-476d-b005-327e10789d86</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

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