namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V09 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserQuestionnaires", "PatientTypeId", "dbo.PatientTypes");
            DropIndex("dbo.UserQuestionnaires", new[] { "PatientTypeId" });
            AlterColumn("dbo.UserQuestionnaires", "PatientTypeId", c => c.Guid());
            CreateIndex("dbo.UserQuestionnaires", "PatientTypeId");
            AddForeignKey("dbo.UserQuestionnaires", "PatientTypeId", "dbo.PatientTypes", "Id");
            DropColumn("dbo.UserQuestionnaires", "StartDate");
            DropColumn("dbo.UserQuestionnaires", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserQuestionnaires", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserQuestionnaires", "StartDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.UserQuestionnaires", "PatientTypeId", "dbo.PatientTypes");
            DropIndex("dbo.UserQuestionnaires", new[] { "PatientTypeId" });
            AlterColumn("dbo.UserQuestionnaires", "PatientTypeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.UserQuestionnaires", "PatientTypeId");
            AddForeignKey("dbo.UserQuestionnaires", "PatientTypeId", "dbo.PatientTypes", "Id", cascadeDelete: true);
        }
    }
}
