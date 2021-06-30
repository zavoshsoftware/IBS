namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v07 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSelectedAudios", "WeekNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSelectedAudios", "WeekNo");
        }
    }
}
