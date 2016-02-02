namespace RoutingAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LINQ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Basics", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Basics", "Date");
        }
    }
}
