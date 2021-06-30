namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        Title = c.String(),
                        QuestionGroupId = c.Guid(nullable: false),
                        AnswerTypeId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnswerTypes", t => t.AnswerTypeId, cascadeDelete: true)
                .ForeignKey("dbo.QuestionGroups", t => t.QuestionGroupId, cascadeDelete: true)
                .Index(t => t.QuestionGroupId)
                .Index(t => t.AnswerTypeId);
            
            CreateTable(
                "dbo.QuestionGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AudioGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        Title = c.String(),
                        PatientTypeId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientTypes", t => t.PatientTypeId, cascadeDelete: true)
                .Index(t => t.PatientTypeId);
            
            CreateTable(
                "dbo.PatientTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserQuestionnaires",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Result = c.String(),
                        PatientTypeId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientTypes", t => t.PatientTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PatientTypeId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Password = c.String(maxLength: 150),
                        CellNum = c.String(nullable: false, maxLength: 20),
                        FullName = c.String(nullable: false, maxLength: 250),
                        Code = c.Int(),
                        Email = c.String(maxLength: 256),
                        RoleId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Audios",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        Title = c.String(),
                        AudioGroupId = c.Guid(nullable: false),
                        FileUrl = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioGroups", t => t.AudioGroupId, cascadeDelete: true)
                .Index(t => t.AudioGroupId);
            
            CreateTable(
                "dbo.UserQuestionnaireDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserQuestionnaireId = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                        Answer = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.UserQuestionnaires", t => t.UserQuestionnaireId, cascadeDelete: true)
                .Index(t => t.UserQuestionnaireId)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuestionnaireDetails", "UserQuestionnaireId", "dbo.UserQuestionnaires");
            DropForeignKey("dbo.UserQuestionnaireDetails", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Audios", "AudioGroupId", "dbo.AudioGroups");
            DropForeignKey("dbo.UserQuestionnaires", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserQuestionnaires", "PatientTypeId", "dbo.PatientTypes");
            DropForeignKey("dbo.AudioGroups", "PatientTypeId", "dbo.PatientTypes");
            DropForeignKey("dbo.Questions", "QuestionGroupId", "dbo.QuestionGroups");
            DropForeignKey("dbo.Questions", "AnswerTypeId", "dbo.AnswerTypes");
            DropIndex("dbo.UserQuestionnaireDetails", new[] { "QuestionId" });
            DropIndex("dbo.UserQuestionnaireDetails", new[] { "UserQuestionnaireId" });
            DropIndex("dbo.Audios", new[] { "AudioGroupId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.UserQuestionnaires", new[] { "PatientTypeId" });
            DropIndex("dbo.UserQuestionnaires", new[] { "UserId" });
            DropIndex("dbo.AudioGroups", new[] { "PatientTypeId" });
            DropIndex("dbo.Questions", new[] { "AnswerTypeId" });
            DropIndex("dbo.Questions", new[] { "QuestionGroupId" });
            DropTable("dbo.UserQuestionnaireDetails");
            DropTable("dbo.Audios");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.UserQuestionnaires");
            DropTable("dbo.PatientTypes");
            DropTable("dbo.AudioGroups");
            DropTable("dbo.QuestionGroups");
            DropTable("dbo.Questions");
            DropTable("dbo.AnswerTypes");
        }
    }
}
