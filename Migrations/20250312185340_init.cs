using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainees_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TraineeTasks",
                columns: table => new
                {
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeTasks", x => new { x.TraineeId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TraineeTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraineeTasks_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78008aac-8b78-49d0-9466-0daf616a8f54", "78008aac-8b78-49d0-9466-0daf616a8f54", "Instructor", "INSTRUCTOR" },
                    { "d2ea87dc-3d18-419c-8f3f-1bcf1aacedeb", "d2ea87dc-3d18-419c-8f3f-1bcf1aacedeb", "Trainee", "TRAINEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "25845815-f006-4cb8-9bb9-9b9a3b6583b5", 0, "a9030550-06c9-4213-9f24-19697aaba6a0", "trainee1@gmail.com", true, true, null, "TRAINEE1@GMAIL.COM", "TRAINEE1@GMAIL.COM", "AQAAAAIAAYagAAAAEDr1ZNDBPBddcwTxPXf/4BCRhD/2PDka4plWqeGbw7AIIUpxMODeMvmyuqDtqFiWGA==", null, false, "DHX2PQMGGNYKVEGZOU6E2EMDSP7RPSFN", false, "trainee1@gmail.com" },
                    { "860c7039-f168-4451-93f8-7bf4401ad338", 0, "b1a18c83-b043-40c7-a3bb-2a2c263a4137", "trainee2@gmail.com", true, true, null, "TRAINEE2@GMAIL.COM", "TRAINEE2@GMAIL.COM", "AQAAAAIAAYagAAAAEGPvpfSX/4uPJSkzzjvWMIgQl3pn+RTs6N+ReLuP9UCi+QqsirR59gpIN6E3UGl6SA==", null, false, "ZF7PM3DFAVY3MJEP5YG56ACEERIDLIO3", false, "trainee2@gmail.com" },
                    { "8d319a16-abef-497a-ba9e-6db56d4f5240", 0, "68ed8451-a127-4151-83ab-945fd6832f0a", "instructor1@gmail.com", true, true, null, "INSTRUCTOR1@GMAIL.COM", "INSTRUCTOR1@GMAIL.COM", "AQAAAAIAAYagAAAAEFDkPT05C2fB5++8Z5p4Do6sEU00R3a3054jBrMjN48Ce7tt0QNtLW2FDCB/IOMlzw==", null, false, "RHXAFCSNL6T4FPUOPEUT4Z36UJU2P3ZJ", false, "instructor1@gmail.com" },
                    { "a14d2bcd-313a-42f9-af3c-7fcca268079b", 0, "1d3a1175-26b5-475a-a175-2cdcbb7b95b4", "instructor3@gmail.com", true, true, null, "INSTRUCTOR3@GMAIL.COM", "INSTRUCTOR3@GMAIL.COM", "AQAAAAIAAYagAAAAEOjAcchy2vaXJt7Tm8Ch+GCo4REthxBvwm2qv0X40c5rWRS7s/XceSLbkN9gXYZbCQ==", null, false, "SCCJQAWMIKGSHXY4RHB44NF3K7PQFKXI", false, "instructor3@gmail.com" },
                    { "b95e4901-d167-4411-8eeb-5f4fc381800f", 0, "350c7103-79a5-427a-8dbb-e93e7719a018", "trainee3@gmail.com", true, true, null, "TRAINEE3@GMAIL.COM", "TRAINEE3@GMAIL.COM", "AQAAAAIAAYagAAAAEPRV7JInwdlfew9SMSDAEZq81kZ6BQcmJX5bM6hyX7t3FUHZgfJZOuP1Sz3oLGc32A==", null, false, "UNQGWJVRKDAICZFWNHJ73PLNUZ6P7QA4", false, "trainee3@gmail.com" },
                    { "c5eed8d5-1d13-4c55-8890-568d84b886c0", 0, "151f5b02-a78d-48df-b899-e6ed00daf8b9", "instructor2@gmail.com", true, true, null, "INSTRUCTOR2@GMAIL.COM", "INSTRUCTOR2@GMAIL.COM", "AQAAAAIAAYagAAAAED4ykt2Zv+17YHJPsVda9kfG8dxpYeXVLOmF+O+kFTl2WUVimRKAgNW/XsF6eMvzcw==", null, false, "HCS6343ZS4TFLC2EKWQKNYURWUW2VVK5", false, "instructor2@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d2ea87dc-3d18-419c-8f3f-1bcf1aacedeb", "25845815-f006-4cb8-9bb9-9b9a3b6583b5" },
                    { "d2ea87dc-3d18-419c-8f3f-1bcf1aacedeb", "860c7039-f168-4451-93f8-7bf4401ad338" },
                    { "78008aac-8b78-49d0-9466-0daf616a8f54", "8d319a16-abef-497a-ba9e-6db56d4f5240" },
                    { "78008aac-8b78-49d0-9466-0daf616a8f54", "a14d2bcd-313a-42f9-af3c-7fcca268079b" },
                    { "d2ea87dc-3d18-419c-8f3f-1bcf1aacedeb", "b95e4901-d167-4411-8eeb-5f4fc381800f" },
                    { "78008aac-8b78-49d0-9466-0daf616a8f54", "c5eed8d5-1d13-4c55-8890-568d84b886c0" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "AccountId", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "8d319a16-abef-497a-ba9e-6db56d4f5240", false, "Instructor 1" },
                    { 2, "c5eed8d5-1d13-4c55-8890-568d84b886c0", false, "Instructor 2" },
                    { 3, "a14d2bcd-313a-42f9-af3c-7fcca268079b", false, "Instructor 3" }
                });

            migrationBuilder.InsertData(
                table: "Trainees",
                columns: new[] { "Id", "AccountId", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "25845815-f006-4cb8-9bb9-9b9a3b6583b5", false, "Trainee 1" },
                    { 2, "860c7039-f168-4451-93f8-7bf4401ad338", false, "Trainee 2" },
                    { 3, "b95e4901-d167-4411-8eeb-5f4fc381800f", false, "Trainee 3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_AccountId",
                table: "Instructors",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_InstructorId",
                table: "Tasks",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_AccountId",
                table: "Trainees",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeTasks_TaskId",
                table: "TraineeTasks",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TraineeTasks");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
