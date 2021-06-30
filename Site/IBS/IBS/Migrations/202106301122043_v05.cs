namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v05 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.UserSelectedAudios", "UserId");
            CreateIndex("dbo.UserSelectedAudios", "AudioId");
            AddForeignKey("dbo.UserSelectedAudios", "AudioId", "dbo.Audios", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserSelectedAudios", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSelectedAudios", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSelectedAudios", "AudioId", "dbo.Audios");
            DropIndex("dbo.UserSelectedAudios", new[] { "AudioId" });
            DropIndex("dbo.UserSelectedAudios", new[] { "UserId" });
        }
    }
}
