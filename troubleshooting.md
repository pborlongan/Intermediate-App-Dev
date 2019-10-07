### October 1st
---
- When creating listviews, do not forget to put the datakeynames, otherwise it wouldn't know what defines the entity.
- When creating objectdatasources, don't forget to connect the methods that you use for inserting, deleting, and updating.
- HOT TIP: you can move a class to its own separate file by putting your cursor at the end of the class name and pressing ctrl + .
- DONT FORGET TO ADD [DATAOBJECT]

### October 2nd
---
#### Nested Repeaters rseview
```csharp
<asp:Repeater ID="SupplierSummaryRepeater" runat="server" DataSourceID="SupplierSummaryDataSource" ItemType="WestWindSystem.ReadModels.SupplierSummary">
        <ItemTemplate>
            <div>
                <blockquote> 
                <%# Item.Company %>
                <br />
                <%# Item.Contact %> - <%# Item.Phone %>
                </blockquote>
                
                <asp:Repeater ID="SupplierProductRepeater" runat="server" DataSource="<%# Item.Products %>" ItemType="WestWindSystem.ReadModels.SupplierProduct">
```
- we are able to use Products ```DataSource="<%# Item.Products %>"``` as our DataSource becuse we are inside a repeater where we are using SupplierSummaryDataSource: ```DataSourceID="SupplierSummaryDataSource"```  as our root DataSource.

- as we can see here:
```csharp
<asp:ObjectDataSource 
    ID="SupplierSummaryDataSource" 
    runat="server"
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetSupplierSummaries" TypeName="WestWindSystem.BLL.SupplyChainManagement">
    </asp:ObjectDataSource>
```
- we are using SupplyChainManagement which contains the method that allows us to access both Suppliers and their products. :D

