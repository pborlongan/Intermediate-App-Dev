<Query Kind="Expression">
  <Connection>
    <ID>b1fa7da7-6733-475c-b548-05cd8c5b4ba0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

/*

    Supplier:
        company name
        contact name
        phone
        Product Summary:
            product name
            category name
            unit price
            quantity/unit


*/

from company in Suppliers
select new
{
	CompanyName = company.CompanyName,
	ContactName = company.ContactName,
	Phone = company.Phone,
	ProductSummary = from product in company.Products
					 from category in category.
	                 select new
					 {
					 	Name = product.ProductName,
						Category = product.CategoryName,
						UnitPrice = product.ProductName
					 }
}