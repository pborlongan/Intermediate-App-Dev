namespace BlogDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Ur;", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "Url");
        }
    }
}
