namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdChange : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TaskItems");
            AlterColumn("dbo.TaskItems", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.TaskItems", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TaskItems");
            AlterColumn("dbo.TaskItems", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.TaskItems", "Id");
        }
    }
}
