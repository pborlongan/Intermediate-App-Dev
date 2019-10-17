<Query Kind="Expression">
  <Connection>
    <ID>b1fa7da7-6733-475c-b548-05cd8c5b4ba0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// show orders for sept 2018
// Orders.Max(x => x.OrderDate)
from sale in Orders
//where sale.OrderDate.Value.Month == 4 && sale.OrderDate.Value.Year == 2018
where sale.OrderDate >= new DateTime(2018, 04, 01) && sale.OrderDate > new DateTime(2018, 05, 01)
select new
{
	Company = sale.Customer.CompanyName,
	DateOrdered = sale.OrderDate,
	TimeToDelivery = sale.RequiredDate - sale.OrderDate // time span
	
}