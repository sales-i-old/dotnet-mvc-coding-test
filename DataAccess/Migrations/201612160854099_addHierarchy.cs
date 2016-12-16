namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addHierarchy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskItems", "DependentTaskId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskItems", "DependentTaskId");
        }
    }
}
