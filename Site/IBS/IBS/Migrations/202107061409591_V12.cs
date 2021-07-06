namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSelectedAudios", "UserId", "dbo.Users");
            DropIndex("dbo.UserSelectedAudios", new[] { "UserId" });
            RenameColumn(table: "dbo.UserSelectedAudios", name: "UserId", newName: "User_Id");
            AlterColumn("dbo.UserSelectedAudios", "User_Id", c => c.Guid());
            CreateIndex("dbo.UserSelectedAudios", "User_Id");
            AddForeignKey("dbo.UserSelectedAudios", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSelectedAudios", "User_Id", "dbo.Users");
            DropIndex("dbo.UserSelectedAudios", new[] { "User_Id" });
            AlterColumn("dbo.UserSelectedAudios", "User_Id", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.UserSelectedAudios", name: "User_Id", newName: "UserId");
            CreateIndex("dbo.UserSelectedAudios", "UserId");
            AddForeignKey("dbo.UserSelectedAudios", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
