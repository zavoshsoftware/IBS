using System.Data.Entity;

namespace Models
{
    public class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AnswerType> AnswerTypes { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public DbSet<AudioGroup> AudioGroups { get; set; }
        public DbSet<PatientType> PatientTypes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionGroup> QuestionGroups { get; set; }
        public DbSet<UserQuestionnaire> UserQuestionnaires { get; set; }
        public DbSet<UserQuestionnaireDetail> UserQuestionnaireDetails { get; set; }
        public DbSet<AnswerItem> AnswerItems { get; set; }
    }
}
