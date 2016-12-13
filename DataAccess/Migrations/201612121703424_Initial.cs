namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskItems",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "(newid())"),
                        Title = c.String(),
                        Description = c.String(),
                        Complete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaskItems");
        }
    }
}
