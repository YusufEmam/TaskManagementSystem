using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Data
{
    public static class DbInitializer
    {
        //Seed Roles
        static string InstructorRoleId = "78008aac-8b78-49d0-9466-0daf616a8f54";
        static string TraineeRoleId = "d2ea87dc-3d18-419c-8f3f-1bcf1aacedeb";
        public static void SeedRoles(ModelBuilder modelBuilder)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=InstructorRoleId,
                    ConcurrencyStamp=InstructorRoleId,
                    Name="Instructor",
                    NormalizedName="Instructor".ToUpper(),
                },
                new IdentityRole
                {
                    Id=TraineeRoleId,
                    ConcurrencyStamp=TraineeRoleId,
                    Name="Trainee",
                    NormalizedName="Trainee".ToUpper(),
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

        //Seed Instructors
        public static void SeedInstructors(ModelBuilder modelBuilder)
        {
            var instructor1AccountId = "8d319a16-abef-497a-ba9e-6db56d4f5240";
            var instructor2AccountId = "c5eed8d5-1d13-4c55-8890-568d84b886c0";
            var instructor3AccountId = "a14d2bcd-313a-42f9-af3c-7fcca268079b";

            //Seed Accounts
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = instructor1AccountId,
                    UserName = "instructor1@gmail.com",
                    NormalizedUserName = "INSTRUCTOR1@GMAIL.COM",
                    Email = "instructor1@gmail.com",
                    NormalizedEmail = "INSTRUCTOR1@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEFDkPT05C2fB5++8Z5p4Do6sEU00R3a3054jBrMjN48Ce7tt0QNtLW2FDCB/IOMlzw==", //Inst@123
                    SecurityStamp = "RHXAFCSNL6T4FPUOPEUT4Z36UJU2P3ZJ",
                    ConcurrencyStamp = "68ed8451-a127-4151-83ab-945fd6832f0a",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new Account
                {
                    Id = instructor2AccountId,
                    UserName = "instructor2@gmail.com",
                    NormalizedUserName = "INSTRUCTOR2@GMAIL.COM",
                    Email = "instructor2@gmail.com",
                    NormalizedEmail = "INSTRUCTOR2@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAED4ykt2Zv+17YHJPsVda9kfG8dxpYeXVLOmF+O+kFTl2WUVimRKAgNW/XsF6eMvzcw==", //Inst@123
                    SecurityStamp = "HCS6343ZS4TFLC2EKWQKNYURWUW2VVK5",
                    ConcurrencyStamp = "151f5b02-a78d-48df-b899-e6ed00daf8b9",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new Account
                {
                    Id = instructor3AccountId,
                    UserName = "instructor3@gmail.com",
                    NormalizedUserName = "INSTRUCTOR3@GMAIL.COM",
                    Email = "instructor3@gmail.com",
                    NormalizedEmail = "INSTRUCTOR3@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEOjAcchy2vaXJt7Tm8Ch+GCo4REthxBvwm2qv0X40c5rWRS7s/XceSLbkN9gXYZbCQ==", //Inst@123
                    SecurityStamp = "SCCJQAWMIKGSHXY4RHB44NF3K7PQFKXI",
                    ConcurrencyStamp = "1d3a1175-26b5-475a-a175-2cdcbb7b95b4",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            );

            // Assign Role to Instructors
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = instructor1AccountId, RoleId = InstructorRoleId },
                new IdentityUserRole<string> { UserId = instructor2AccountId, RoleId = InstructorRoleId },
                new IdentityUserRole<string> { UserId = instructor3AccountId, RoleId = InstructorRoleId }
            );

            // Seed Instructors
            modelBuilder.Entity<Instructor>().HasData(
                new Instructor
                {
                    Id = 1,
                    Name = "Instructor 1",
                    IsDeleted = false,
                    AccountId = instructor1AccountId,
                },
                new Instructor
                {
                    Id = 2,
                    Name = "Instructor 2",
                    IsDeleted = false,
                    AccountId = instructor2AccountId,
                },
                new Instructor
                {
                    Id = 3,
                    Name = "Instructor 3",
                    IsDeleted = false,
                    AccountId = instructor3AccountId,
                }
            );
        }

        //Seed Trainees
        public static void SeedTrainees(ModelBuilder modelBuilder)
        {
            var trainee1AccountId = "25845815-f006-4cb8-9bb9-9b9a3b6583b5";
            var trainee2AccountId = "860c7039-f168-4451-93f8-7bf4401ad338";
            var trainee3AccountId = "b95e4901-d167-4411-8eeb-5f4fc381800f";

            // Seed Accounts
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = trainee1AccountId,
                    UserName = "trainee1@gmail.com",
                    NormalizedUserName = "TRAINEE1@GMAIL.COM",
                    Email = "trainee1@gmail.com",
                    NormalizedEmail = "TRAINEE1@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEDr1ZNDBPBddcwTxPXf/4BCRhD/2PDka4plWqeGbw7AIIUpxMODeMvmyuqDtqFiWGA==", //Train@123
                    SecurityStamp = "DHX2PQMGGNYKVEGZOU6E2EMDSP7RPSFN",
                    ConcurrencyStamp = "a9030550-06c9-4213-9f24-19697aaba6a0",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new Account
                {
                    Id = trainee2AccountId,
                    UserName = "trainee2@gmail.com",
                    NormalizedUserName = "TRAINEE2@GMAIL.COM",
                    Email = "trainee2@gmail.com",
                    NormalizedEmail = "TRAINEE2@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEGPvpfSX/4uPJSkzzjvWMIgQl3pn+RTs6N+ReLuP9UCi+QqsirR59gpIN6E3UGl6SA==", //Train@123
                    SecurityStamp = "ZF7PM3DFAVY3MJEP5YG56ACEERIDLIO3",
                    ConcurrencyStamp = "b1a18c83-b043-40c7-a3bb-2a2c263a4137",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new Account
                {
                    Id = trainee3AccountId,
                    UserName = "trainee3@gmail.com",
                    NormalizedUserName = "TRAINEE3@GMAIL.COM",
                    Email = "trainee3@gmail.com",
                    NormalizedEmail = "TRAINEE3@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEPRV7JInwdlfew9SMSDAEZq81kZ6BQcmJX5bM6hyX7t3FUHZgfJZOuP1Sz3oLGc32A==", //Train@123
                    SecurityStamp = "UNQGWJVRKDAICZFWNHJ73PLNUZ6P7QA4",
                    ConcurrencyStamp = "350c7103-79a5-427a-8dbb-e93e7719a018",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            );

            // Assign Role to Trainees
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = trainee1AccountId, RoleId = TraineeRoleId },
                new IdentityUserRole<string> { UserId = trainee2AccountId, RoleId = TraineeRoleId },
                new IdentityUserRole<string> { UserId = trainee3AccountId, RoleId = TraineeRoleId }
            );

            // Seed Trainees
            modelBuilder.Entity<Trainee>().HasData(
                new Trainee
                {
                    Id = 1,
                    Name = "Trainee 1",
                    IsDeleted = false,
                    AccountId = trainee1AccountId,
                },
                new Trainee
                {
                    Id = 2,
                    Name = "Trainee 2",
                    IsDeleted = false,
                    AccountId = trainee2AccountId,
                },
                new Trainee
                {
                    Id = 3,
                    Name = "Trainee 3",
                    IsDeleted = false,
                    AccountId = trainee3AccountId,
                }
            );
        }
    }
}
