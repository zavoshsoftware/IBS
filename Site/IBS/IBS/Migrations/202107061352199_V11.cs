namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSelectedAudios", "UserQuestionnaireId", c => c.Guid());
            CreateIndex("dbo.UserSelectedAudios", "UserQuestionnaireId");
            AddForeignKey("dbo.UserSelectedAudios", "UserQuestionnaireId", "dbo.UserQuestionnaires", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSelectedAudios", "UserQuestionnaireId", "dbo.UserQuestionnaires");
            DropIndex("dbo.UserSelectedAudios", new[] { "UserQuestionnaireId" });
            DropColumn("dbo.UserSelectedAudios", "UserQuestionnaireId");
        }
    }
}
