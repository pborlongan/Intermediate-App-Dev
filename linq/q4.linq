<Query Kind="Expression">
  <Connection>
    <ID>dd6fc7dd-8c72-476d-b005-327e10789d86</ID>
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
						Discount = item.Discount,
						SubTotal = ((double)item.Product.Price * item.QtyPicked) - ((double)item.Discount),
						Tax = item.Product.Taxable == true ? (((double)item.Product.Price * item.QtyPicked) - ((double)item.Discount)) * 0.05 : 0,
						ExtendedPrice = item.Product.Taxable == true ? (((double)item.Product.Price * item.QtyPicked) - ((double)item.Discount)) * 1.05 : ((double)item.Product.Price * item.QtyPicked) - ((double)item.Discount)
					}
}