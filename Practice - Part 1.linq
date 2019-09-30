<Query Kind="Expression">
  <Connection>
    <ID>a974ae53-c80a-4f65-9912-ec8d7f09a4ab</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// Practice questions - do each one in a separate LinqPad query.
/*

A) List all the customer company names for those with more than 5 orders.
B) Get a list of all the region names
C) Get a list of all the territory names
D) List all the regions and the number of territories in each region
E) List all the region and territory names in a "flat" list
F) List all the region and territory names as an "object graph"
   - use a nested query
G) List all the product names that contain the word "chef" in the name.
H) List all the discontinued products, specifying the product name and unit price.
I) List the company names of all Suppliers in North America (Canada, USA, Mexico)

*/

// A) List all the customer company names for those with more than 5 orders
from person in Customers
where person.Orders.Count() > 5
select new{
	Company = person.CompanyName
}

// B) Get a list of all the region names
from region in Regions
select new{
	Region_Name = region.RegionDescription
}

// C) Get a list of all the territory names
from territory in Territories
select new{
	Territory_Name = territory.TerritoryDescription
}

// D) List all the regions and the number of territories in each region
from region in Regions
select new{
	Region_Name = region.RegionDescription,
	Territory_Count = region.Territories.Count()
}

// E) List all the region and territory names in a "flat" list
// do the blue one and not the green???????? idk why it doesnt work the other way
from territory in Territories
select new{
	Region = territory.Region.RegionDescription,
	Territory = territory.TerritoryDescription
}

// F) List all the region and territory names as an "object graph"
//   - use a nested query
from region in Regions
select new{
	Region = region.RegionDescription,
	Territory = from territories in region.Territories
				select territories.TerritoryDescription
}


// G) List all the product names that contain the word "chef" in the name.
from product in Products
where product.ProductName.Contains("Chef")
select product.ProductName

// H) List all the discontinued products, specifying the product name and unit price.
from product in Products
where product.Discontinued == true
select new
{
	Product_Name = product.ProductName,
	Product_UnitPrice = product.UnitPrice
}

// I) List the company names of all Suppliers in North America (Canada, USA, Mexico)
from company in Suppliers
where company.Address.Country == "Canada" || company.Address.Country == "USA" || company.Address.Country == "Mexico"
select new{
	Company_Name = company.CompanyName,
	Company_Country = company.Address.Country
}