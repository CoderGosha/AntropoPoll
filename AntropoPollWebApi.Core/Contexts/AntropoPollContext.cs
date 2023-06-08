using AntropoPollWebApi.Core.Models;
using AntropoPollWebApi.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;

namespace AntropoPollWebApi.Core.Contexts
{
    public class AntropoPollContext : DbContext
    {
        public AntropoPollContext(DataBaseProviders dataBaseProviders)
            : base(GetDbContextOptionsBuilder(dataBaseProviders))
        {
            InitDatabase();
            CanThrowDbExceptionsWhenSaveChanges = true;
            var migrationsAssembly = this.GetService<IMigrationsAssembly>();

        }

        /// <summary>
        /// Пустой конструктор с БД необходим для создания миграций на локаьлном ПК
        /// </summary>
        public AntropoPollContext()
            : base(GetDbContextOptionsBuilder(GetSettings()))
        {
            InitDatabase();
            CanThrowDbExceptionsWhenSaveChanges = true;
            var migrationsAssembly = this.GetService<IMigrationsAssembly>();

        }

        private static DataBaseProviders GetSettings()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            var config = builder.Build();

            var serviceProvider = new ServiceCollection()
                .Configure<AntropoPollSettings>(c => config.GetSection("AntropoPollSettings").Bind(c))
                .BuildServiceProvider();
            var opcServerConfig = serviceProvider.GetService<IOptions<AntropoPollSettings>>().Value;
            return opcServerConfig?.AntropoPollProviders;
        }

        protected static DbContextOptions<AntropoPollContext> GetDbContextOptionsBuilder(DataBaseProviders dataBaseProviders)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AntropoPollContext>();

            switch (dataBaseProviders.Provider)
            {
                case "MSSQL":
                    {
                        throw new Exception("MSSQL not supported");
                    }

                case "PostgreSQL":
                    {
                        var options = optionsBuilder
                            .UseNpgsql(dataBaseProviders.ConnectionStrings.PostgreSQL, x =>
                            {
                                x.MigrationsHistoryTable("__MigrationsHistory", "antropopoll");
                                x.MigrationsAssembly("AntropoPollWebApi.Core");
                                x.CommandTimeout(120);
                            })
                            .Options;
                        return options;
                    }

                default:
                    {
                        throw new Exception("MSSQL not supported");
                    }
            }


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("antropopoll");

            modelBuilder.Entity<BaseQuestion>()
                .ToTable("Question")
                .HasDiscriminator<int>("QuestionDiscriminator")
                .HasValue<ClosedQuestion>(1);

            modelBuilder.Entity<BaseQuestion>()
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Schema>()
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Interpretation>()
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Schema>()
                .Property(p => p.StanValue)
                .HasDefaultValue(10);

            modelBuilder.Entity<SystemVariableReport>()
                .Property(p => p.StanValue)
                .HasDefaultValue(0);

            modelBuilder.Entity<SystemVariableReport>()
                .Property(p => p.MaxValue)
                .HasDefaultValue(0);

            modelBuilder.Entity<BaseQuestion>()
                .HasOne(p => p.SchemaVariable)
                .WithMany(t => t.BaseQuestions)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }

        protected bool CanThrowDbExceptionsWhenSaveChanges { get; set; } = false;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Add-Migration InitialCreate
        ///  dotnet ef migrations add InitialCreate --context AntropoPollContext
        ///  dotnet ef database update --context AntropoPollContext
        /// </summary>
        private void InitDatabase()
        {
            var total = this.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);
        }

        public DbSet<BaseQuestion> BaseQuestions { get; set; }

        public DbSet<ClosedQuestion> ClosedQuestion { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Schema> Schemes { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<ResultQuestion> ResultQuestions { get; set; }
        public DbSet<SchemaVariable> SchemaVariables { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<SystemVariableReport> SystemVariableReports { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<Interpretation> Interpretations { get; set; }
        public DbSet<VariableInInterpretation> VariableInInterpretations { get; set; }

        public DbSet<ReportTemplate> ReportTemplates { get; set; }

        public DbSet<ResultInterpretation> ResultInterpretations { get; set; }

        public DbSet<ResultTemplate> ResultTemplates { get; set; }
    }
}
