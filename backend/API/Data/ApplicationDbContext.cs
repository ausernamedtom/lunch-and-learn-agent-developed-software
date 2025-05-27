using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<PersonSkill> PersonSkills { get; set; } = null!;
        public DbSet<SkillVerification> SkillVerifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Skills)
                .WithOne(ps => ps.Person)
                .HasForeignKey(ps => ps.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Skill>()
                .HasMany(s => s.PersonSkills)
                .WithOne(ps => ps.Skill)
                .HasForeignKey(ps => ps.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PersonSkill>()
                .HasMany<SkillVerification>()
                .WithOne(sv => sv.PersonSkill)
                .HasForeignKey(sv => sv.PersonSkillId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial data (optional)
            SeedInitialData(modelBuilder);
        }

        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            // Seed Skills
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = "skill-1", Name = "React", Description = "A JavaScript library for building user interfaces", Category = "Frontend" },
                new Skill { Id = "skill-2", Name = "TypeScript", Description = "A strongly typed programming language that builds on JavaScript", Category = "Programming Language" },
                new Skill { Id = "skill-3", Name = "C#", Description = "A modern object-oriented programming language", Category = "Backend" },
                new Skill { Id = "skill-4", Name = "Azure SQL", Description = "Microsoft cloud database solution", Category = "Database" },
                new Skill { Id = "skill-5", Name = "Tailwind CSS", Description = "A utility-first CSS framework", Category = "Frontend" },
                new Skill { Id = "skill-6", Name = "Entity Framework", Description = "An ORM framework for .NET applications", Category = "Backend" },
                new Skill { Id = "skill-7", Name = "REST API Design", Description = "Designing RESTful APIs", Category = "Backend" },
                new Skill { Id = "skill-8", Name = "HATEOAS", Description = "Hypermedia as the Engine of Application State", Category = "API Design" }
            );

            // Seed People
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = "person-1",
                    FirstName = "John",
                    LastName = "Doe",
                    JobTitle = "Senior Frontend Developer",
                    Department = "Engineering",
                    Email = "john.doe@example.com",
                    PhotoUrl = "https://randomuser.me/api/portraits/men/1.jpg"
                },
                new Person
                {
                    Id = "person-2",
                    FirstName = "Jane",
                    LastName = "Smith",
                    JobTitle = "Backend Developer",
                    Department = "Engineering",
                    Email = "jane.smith@example.com",
                    PhotoUrl = "https://randomuser.me/api/portraits/women/2.jpg"
                },
                new Person
                {
                    Id = "person-3",
                    FirstName = "Mike",
                    LastName = "Johnson",
                    JobTitle = "Full Stack Developer",
                    Department = "Engineering",
                    Email = "mike.johnson@example.com",
                    PhotoUrl = "https://randomuser.me/api/portraits/men/3.jpg"
                },
                new Person
                {
                    Id = "person-4",
                    FirstName = "Emily",
                    LastName = "Davis",
                    JobTitle = "API Designer",
                    Department = "Architecture",
                    Email = "emily.davis@example.com",
                    PhotoUrl = "https://randomuser.me/api/portraits/women/4.jpg"
                },
                new Person
                {
                    Id = "person-5",
                    FirstName = "Robert",
                    LastName = "Brown",
                    JobTitle = "Database Administrator",
                    Department = "IT Operations",
                    Email = "robert.brown@example.com",
                    PhotoUrl = "https://randomuser.me/api/portraits/men/5.jpg"
                }
            );

            // Seed PersonSkills
            modelBuilder.Entity<PersonSkill>().HasData(
                // John Doe's skills
                new PersonSkill { Id = "ps-1", PersonId = "person-1", SkillId = "skill-1", ProficiencyLevel = ProficiencyLevel.Expert, YearsOfExperience = 5, IsVerified = true },
                new PersonSkill { Id = "ps-2", PersonId = "person-1", SkillId = "skill-2", ProficiencyLevel = ProficiencyLevel.Advanced, YearsOfExperience = 3, IsVerified = true },
                new PersonSkill { Id = "ps-3", PersonId = "person-1", SkillId = "skill-5", ProficiencyLevel = ProficiencyLevel.Intermediate, YearsOfExperience = 2, IsVerified = false },
                
                // Jane Smith's skills
                new PersonSkill { Id = "ps-4", PersonId = "person-2", SkillId = "skill-3", ProficiencyLevel = ProficiencyLevel.Expert, YearsOfExperience = 6, IsVerified = true },
                new PersonSkill { Id = "ps-5", PersonId = "person-2", SkillId = "skill-6", ProficiencyLevel = ProficiencyLevel.Advanced, YearsOfExperience = 4, IsVerified = true },
                new PersonSkill { Id = "ps-6", PersonId = "person-2", SkillId = "skill-4", ProficiencyLevel = ProficiencyLevel.Intermediate, YearsOfExperience = 2, IsVerified = true },
                
                // Mike Johnson's skills
                new PersonSkill { Id = "ps-7", PersonId = "person-3", SkillId = "skill-1", ProficiencyLevel = ProficiencyLevel.Advanced, YearsOfExperience = 4, IsVerified = true },
                new PersonSkill { Id = "ps-8", PersonId = "person-3", SkillId = "skill-3", ProficiencyLevel = ProficiencyLevel.Intermediate, YearsOfExperience = 2, IsVerified = false },
                new PersonSkill { Id = "ps-9", PersonId = "person-3", SkillId = "skill-4", ProficiencyLevel = ProficiencyLevel.Beginner, YearsOfExperience = 1, IsVerified = false },
                
                // Emily Davis's skills
                new PersonSkill { Id = "ps-10", PersonId = "person-4", SkillId = "skill-7", ProficiencyLevel = ProficiencyLevel.Expert, YearsOfExperience = 7, IsVerified = true },
                new PersonSkill { Id = "ps-11", PersonId = "person-4", SkillId = "skill-8", ProficiencyLevel = ProficiencyLevel.Expert, YearsOfExperience = 5, IsVerified = true },
                new PersonSkill { Id = "ps-12", PersonId = "person-4", SkillId = "skill-3", ProficiencyLevel = ProficiencyLevel.Advanced, YearsOfExperience = 4, IsVerified = true },
                
                // Robert Brown's skills
                new PersonSkill { Id = "ps-13", PersonId = "person-5", SkillId = "skill-4", ProficiencyLevel = ProficiencyLevel.Expert, YearsOfExperience = 8, IsVerified = true },
                new PersonSkill { Id = "ps-14", PersonId = "person-5", SkillId = "skill-6", ProficiencyLevel = ProficiencyLevel.Intermediate, YearsOfExperience = 3, IsVerified = false },
                new PersonSkill { Id = "ps-15", PersonId = "person-5", SkillId = "skill-3", ProficiencyLevel = ProficiencyLevel.Beginner, YearsOfExperience = 1, IsVerified = false }
            );

            // Seed some skill verifications
            modelBuilder.Entity<SkillVerification>().HasData(
                new SkillVerification
                {
                    Id = "verification-1",
                    PersonSkillId = "ps-1",
                    VerificationType = "ManagerVerification",
                    VerifiedBy = "Sarah Wilson",
                    Note = "Verified based on project performance",
                    VerificationDate = new DateTime(2024, 12, 15)
                },
                new SkillVerification
                {
                    Id = "verification-2",
                    PersonSkillId = "ps-4",
                    VerificationType = "CertificationUpload",
                    Note = "Microsoft Certified C# Developer",
                    CertificationUrl = "https://example.com/certifications/jane-smith-csharp",
                    VerificationDate = new DateTime(2024, 10, 5)
                },
                new SkillVerification
                {
                    Id = "verification-3",
                    PersonSkillId = "ps-10",
                    VerificationType = "PeerEndorsement",
                    VerifiedBy = "John Doe",
                    Note = "Collaborated on API design project",
                    VerificationDate = new DateTime(2025, 1, 20)
                }
            );
        }
    }
}
