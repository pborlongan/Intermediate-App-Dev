<Query Kind="Expression">
  <Connection>
    <ID>25dab0a5-613e-4076-857a-f35fe862a5a4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

// Question 4
from order in OrderLists
where order.OrderID == 33
group order by order.Product.Category.Description into ProductCategory
select new
{
	Category = ProductCategory.Key,
	OrderProducts = from item in ProductCategory
					orderby item.Product.Description
					select new
					{
						Product = item.Product.Description,
						Price = item.Price,
						PickedQty = item.QtyPicked,
						Discount = item.Discount
					}
}