# Order Processing

> Orders are shipped directly from our suppliers to our customers. As such, suppliers log onto our system to see what orders there are for the products that they provide.

## User Interface

Suppliers will be interacting with a page that shows the following information.

![Mockup](./Shipping-Orders.svg)

### Events and Interactions
![Mockup](./ShippingOrdersPlan.svg)
- ![](1.svg) - **Page_Load** event
    - ![](A.svg) - Supplier/Contact names obtained from who the logged-in user is.
    - ![](B.svg) - Load the ListView data
        - **`List<OutstandingOrder>OrderProcessingController.LoadOrders(SupplierID)`**
    - ![](C.svg) - Load the list of shippers
        - **`List<ShipperSelection>OrderProcessingController.ListShippers()`**
- ![](2.svg) - **EditCommand** click event
    - Default EditCommand behaviour of the ListView
    - `<EditItemTemplate>` will display the extended information of the Products List ![](D.svg) and other order information.
- ![](3.svg) - **ShipOrder** click
    - Use a custom command name of "ShipOrder" and handle in the ListView's ItemCommand event.
    - Gather information from the form for the products to be shipped and the shipping information. This is sent to the **``void OrderProcessingController.ShipOrder(int orderId, ShippingDirections shipping, List<ProductShipment> products)``**

The information shown here will be displayed in a **ListView**, using the *SelectedItemTemplate* as the part that shows the details for a given order.

## POCOs/DTOs

The POCOs/DTOs are simply classes that will hold our data when we are performing Queries or issuing commands to the BLL.
## QUERY POCO/DTO
> ShipperSelection
> - ShipperId
> - ShipperName
>
> OutstandingOrder

## Command POCO/DTO
> ShippingDirections
>
> ProductShipment

### Queries
```csharp
public class ShipperSelection
{
    public int ShipperId {get; set;}
    public string Shipper {get; set;}
}
```

### Queries
```csharp
public class OutstandingOrder
{
    public int OrderID {get; set;}
    public string ShipToName {get; set;}
    public DateTime OrderDate {get; set;}
    public DateTime RequiredBy {get; set;}
    public TimeSpan DaysRemaining {get;} //Calculated
    public IEnumerable<OrderProductInformation> OutstandingItems {get; set;}
    public string FullShippingAddress {get; set;}
    public string Comments {get; set;}
}
```

```csharp
public class OrderProductInformation
{
    public int ProductId {get;set;}
    public string ProductName {get;set;}
    public short Qty {get;set;}
    public string QtyPerUnit {get;set;}
    public short Outstanding {get;set;}
    // note: outstanding <= orderdetails.quantity - sum(manifestitems.shipquantity) for that product/order
}
```

### Commands
```csharp
public class ShipperDirections
{
    public int ShipperId {get; set;}
    public string TrackingCode {get; set;}
    public decimal? FreightCharge {get; set;}
}
```

```csharp
public class ProductShipment
{
    public int ShipperId {get; set;}
    public int ShipQuantity {get; set;}
}
```


## BLL Processing

All product shipments are handled by the **`OrderProcessingController`**. It supports the following methods.

- **``List<OutstandingOrder> LoadOrders(int supplierId)``**
    - **Validation:**
        - Make sure the supplier ID exists, otherwise throw exception
        - [Advanced:] *Make sure the logged-in user works fir the identified supplier.*
    - Query for outstanding orders, getting data from the following tables:
        - TODO: List table names
- **``List<ShipperSelection> ListShippers()``**
    - Queries for all the shippers.
- **``void OrderProcessingController.ShipOrder(int orderId, ShippingDirections shipping, List<ProductShipment> products)``**
    - **Validation:**
        - OrderId must be valid
        - Products cannot be an empty list
        - Products identified must be on the order
        - Quantity must be greater than zero and less than or equal to the quantity outstanding
        - Shipper must exist
        - Freight charge must either be null (no charge) or > $0.00
    - Processing (tables/data that must be updated/inserted/deleted)
        - Create new shipment
        - Add all manifest items
        - Check if order is complete; if so, update Order.Shipped