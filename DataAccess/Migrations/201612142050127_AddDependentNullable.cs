namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDependentNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskItems", "DependentTaskId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskItems", "DependentTaskId", c => c.Guid(nullable: false));
        }
    }
}
