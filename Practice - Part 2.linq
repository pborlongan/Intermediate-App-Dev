<Query Kind="Expression">
  <Connection>
    <ID>a974ae53-c80a-4f65-9912-ec8d7f09a4ab</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// Practice questions - do each one in a separate LinqPad query.

// A) Group employees by region and show the results in this format:
/* ----------------------------------------------
 * | REGION        | EMPLOYEES                  |
 * ----------------------------------------------
 * | Eastern       | ------------------------   |
 * |               | | Nancy Devalio        |   |
 * |               | | Fred Flintstone      |   |
 * |               | | Bill Murray          |   |
 * |               | ------------------------   |
 * |---------------|----------------------------|
 * | ...           |                            |
 * 
 */

// B) List all the Customers by Company Name. Include the Customer's company name, contact name, and other contact information in the result.

// C) List all the employees and sort the result in ascending order by last name, then first name. Show the employee's first and last name separately, along with the number of customer orders they have worked on.

// D) List all the employees and sort the result in ascending order by last name, then first name. Show the employee's first and last name separately, along with the number of customer orders they have worked on.

// E) Group all customers by city. Output the city name, aalong with the company name, contact name and title, and the phone number.

// F) List all the Suppliers, by Country

/////////////////////////////////////////////////////////////////////////////////////////


// A) Group employees by region and show the results in the format above
// CHECK ERD!!!!!!!!!!!!!!!!!!!!!
// Start with Region 
from place in Regions
select new{
	Region = place.RegionDescription,
	Employees = (from area in place.Territories //from region, go to territories.
				 from managedTerritory in area.EmployeeTerritories // from territories, access employee 
				 select managedTerritory.Employee.FirstName + " " + managedTerritory.Employee.LastName).Distinct()
}

// B) List all the Customers sorted by Company Name. Include the Customer's company name, contact name, and other contact information in the result.
from customer in Customers
orderby customer.CompanyName
select new{
	Customer_Company_Name = customer.CompanyName,
	Contact_Information = new
						  {
						  	customer.ContactName,
							customer.ContactTitle,
							customer.ContactEmail
						  }
}


// C) List all the employees and sort the result in ascending order by last name, then first name. Show the employee's first and last name separately, along with the number of customer orders they have worked on.
from employee in Employees
orderby employee.LastName ascending
select new{
	Employee_LastName = employee.LastName,
	Employee_FirstName = employee.FirstName,
	Customer_Orders = (from sales in Orders
					   where sales.SalesRepID == employee.EmployeeID
					   select sales.OrderID).Count()
}

// or
from employee in Employees
orderby employee.LastName, employee.FirstName
select new{
	Employee_LastName = employee.LastName,
	Employee_FirstName = employee.FirstName,
	Customer_Orders = employee.SalesRepOrders.Count()
}

// D) List all the employees and sort the result in descending(?) order by last name, then first name. Show the employee's first and last name separately, along with the number of customer orders they have worked on.
from employee in Employees
orderby employee.LastName descending
select new{
	Employee_LastName = employee.LastName,
	Employee_FirstName = employee.FirstName,
	Customer_Orders = (from sales in Orders
					   where sales.SalesRepID == employee.EmployeeID
					   select sales.OrderID).Count()
}

// E) Group all customers by city. Output the city name, along with the company name, contact name and title, and the phone number.
from buyer in Customers
group buyer by buyer.Address.City into CityVendors
select new{
	City = CityVendors.Key,
	Company = from company in CityVendors
			  select new{
			  	company.CompanyName,
				company.ContactName,
				company.ContactTitle,
				company.Phone
			  }
}

// F) List all the Suppliers, by Country
from vendor in Suppliers
group vendor by vendor.Address.Country
// short answer