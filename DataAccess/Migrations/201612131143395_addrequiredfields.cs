namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequiredfields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskItems", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.TaskItems", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskItems", "Description", c => c.String());
            AlterColumn("dbo.TaskItems", "Title", c => c.String());
        }
    }
}
