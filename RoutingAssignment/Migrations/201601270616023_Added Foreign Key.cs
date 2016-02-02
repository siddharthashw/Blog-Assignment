namespace RoutingAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Basics", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Basics", "UserId");
            AddForeignKey("dbo.Basics", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Basics", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Basics", "UserName", c => c.String());
            DropForeignKey("dbo.Basics", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Basics", new[] { "UserId" });
            DropColumn("dbo.Basics", "UserId");
        }
    }
}
