## Entity Framework Review
---
### Restoring a database using .bacpac

1. On SQL Server, right click on databases folder
2. Select 'Import Data-tier Application'
3. Click next -> look for your .bacpac -> change nothing

---

```
    <connectionStrings>
    <add name="WWdb"
         connectionString="Data Source=.;Initial Catalog=WestWind;Integrated Security=true;"
         providerName="System.Data.SqlClient"/>
    </connectionStrings>
```

**Data Source = .** - database server

**Initial Catalog = WestWind** - database name

**Value Type** - depends on the address

```
    public int? SupplierID{ get; set;}
```
- adding a ? at the end of the data type means that it is nullable

```
    public byte[] Picture {get; set;}
```
- image data type in the db

---

The database context class is a "virtual representation" of the database, with each DbSet\<T> property referencing a particular table in the database.

It inherits from the DbContext class.

\<T> is a placeholder for any data type.

```
    Database.SetInitializer<WestWindContext>(null);
```
- This tells EF that it should NOT create any tables in the database on my behalf
- (null == no initializer)

```
    public virtual ICollection<Product> Products { get; set; } =
            new HashSet<Product>();
```
- This is called a Navigation Property
- The virtual keyword allows our entity to support "lazy loading".
- The ICollection\<T> is an interface data type.
- The HashSet\<T> is a class that "implements" the ICollection\<T> interface.

```
        [NotMapped]
        public string FullName
        { get { return FirstName + " " + LastName; } }
```
- NotMapped properties are properties that exist on the Entity, but NOT in the Database