<Query Kind="Expression">
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