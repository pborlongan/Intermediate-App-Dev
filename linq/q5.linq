<Query Kind="Statements">
  <Connection>
    <ID>dd6fc7dd-8c72-476d-b005-327e10789d86</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

DateTime start = new DateTime(2018,1,7) // last date of a picked order, which is a sunday
					 .AddDays(-14); // go two weeks earlier.
DateTime end = start.AddDays(7); // end time


var result = from sale in Orders
			 where sale.OrderDate >= start && sale.OrderDate < end
			 orderby sale.PickedDate
			 group sale by sale.PickerID into pickedOrders
			 select new
			 {
			 	Picker = //pickedOrders.Key,
						Pickers.Single(x => x.PickerID == pickedOrders.Key),
				Orders = from picked in pickedOrders
						 select new
						 {
						 	Order = picked.OrderID,
							Items = from item in picked.OrderLists
									select new
									{
										Item = item.Product.Description,
										Ordered = item.QtyOrdered,
										Picked = item.QtyPicked
									}
						 }
			 };
	result.Dump();