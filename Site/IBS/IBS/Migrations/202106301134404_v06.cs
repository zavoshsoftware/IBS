namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v06 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserSelectedAudios", "WeekNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserSelectedAudios", "WeekNo", c => c.Guid(nullable: false));
        }
    }
}
