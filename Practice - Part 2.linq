<Query Kind="Expression">
  <Connection>
    <ID>a974ae53-c80a-4f65-9912-ec8d7f09a4ab</ID>
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

// A) Group employees by region and show the results in the format above
from employee in Employees
group employee by employee.Address.Region into EmployeeByRegion
select new{
	Region = EmployeeByRegion.Key,
	Employees = from data in EmployeeByRegion
				select new
				{
					First_Name = data.FirstName,
					Last_Name = data.LastName
				}
}

// B) List all the Customers by Company Name. Include the Customer's company name, contact name, and other contact information in the result.