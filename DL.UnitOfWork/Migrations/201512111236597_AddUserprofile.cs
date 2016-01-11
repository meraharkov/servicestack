namespace DL.UnitOfWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserprofile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Birthday = c.DateTime(nullable: false),
                        IsMale = c.Boolean(nullable: false),
                        Language = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProfiles");
        }
    }
}
