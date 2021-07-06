namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSelectedAudios", "User_Id", "dbo.Users");
            DropIndex("dbo.UserSelectedAudios", new[] { "User_Id" });
            DropColumn("dbo.UserSelectedAudios", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserSelectedAudios", "User_Id", c => c.Guid());
            CreateIndex("dbo.UserSelectedAudios", "User_Id");
            AddForeignKey("dbo.UserSelectedAudios", "User_Id", "dbo.Users", "Id");
        }
    }
}
