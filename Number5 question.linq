<Query Kind="Statements">
  <Connection>
    <ID>a53bb839-f58f-4640-9442-210b454caccb</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

// Select all orders a picker has done on a particular week (Sunday through Saturday). Group and sorted by picker. Sort the orders by picked date.
// Orders.Max(x=>x.PickedDate).Value.DayOfWeek
DateTime start = new DateTime(2018,1,7) // last date of a picked order, which is a sunday
					 .AddDays(-14); // go two weeks earlier.
DateTime end = start.AddDays(7); // end time

var diff = end - start;
diff.Dump("time between two dates");

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