<Query Kind="Expression">
  <Connection>
    <ID>b1fa7da7-6733-475c-b548-05cd8c5b4ba0</ID>
    <Persist>true</Persist>
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

// B) List all the Customers by Company Name. Include the Customer's company name, contact name, and other contact information in the result.
from customer in Customers
select new{
	Customer_Company_Name = customer.CompanyName,
	Contact_Information = from company in Customers
						  select new
						  {
						  		Contact_Name = company.ContactName,
								Contact_Title = company.ContactTitle,
								Contact_Email = company.ContactEmail
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
