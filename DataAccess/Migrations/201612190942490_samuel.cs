namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class samuel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DependentTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        TaskItemId = c.Guid(nullable: false),
                        DependentTaskItemId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskItems", t => t.DependentTaskItemId, cascadeDelete: true)
                .Index(t => t.DependentTaskItemId);
            
            DropColumn("dbo.TaskItems", "DependentTaskId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskItems", "DependentTaskId", c => c.Guid());
            DropForeignKey("dbo.DependentTasks", "DependentTaskItemId", "dbo.TaskItems");
            DropIndex("dbo.DependentTasks", new[] { "DependentTaskItemId" });
            DropTable("dbo.DependentTasks");
        }
    }
}
