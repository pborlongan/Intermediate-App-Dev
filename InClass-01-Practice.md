# Assessment 1 Practice

## Setup
1. Restore DB into the system.
2. Create a WebApp Project.
    - name the project "WebApp"
    - name the solution "GroceryListSolution"
3. Create two class libraries.
    - GroceryListSystem
    - GroceryList.Data
4. Add folders associated to the two class libraries.
    - System: DAL and BLL
    - Data: Entities, DTOs, and POCOs.
5. Add Entity Framework to all projects.
6. Add references to the solution.
    - GroceryListSystem REFERENCES TO GroceryList.Data

## Entities
1. Create the entity classes in the Entity folder using reverse engineering.
    - Right click on the entities folder
    - Go to data
    - Select ADO.NET Entity Data Model
    - Change the name to GroceryListContext
    - Select code first from database
    - New connection
    - Server name to "."
    - Select the database you are using
    - Change the connection string name to "GroceryListDB"
    - Check Tables only when asked which database objects are needed
2.  Add ErrorMessage to ```[Required]``` entities.
3. Add the not mapped properties

    ```csharp
        private string _FullName;
        public string FullName
        {
            get{
                return _FullName;
            }
            set{
                _FullName = LastName + ", " + FirstName;
            }
        }
    ```
