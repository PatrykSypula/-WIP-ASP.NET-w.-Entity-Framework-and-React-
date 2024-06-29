using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "varchar", nullable: false),
                    Password = table.Column<string>(type: "varchar", nullable: false),
                    Email = table.Column<string>(type: "varchar", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dictionaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DictionaryName = table.Column<string>(type: "varchar", nullable: false),
                    DictionaryLevel = table.Column<string>(type: "varchar", nullable: false),
                    IsDefaultDictionary = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dictionaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GoodAnswers = table.Column<string>(type: "varchar(64)", nullable: false),
                    AllAnswers = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<string>(type: "varchar", nullable: false),
                    DictionaryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionStatistics_Dictionaries_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "Dictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionStatistics_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscribedDictionary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DictionaryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribedDictionary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscribedDictionary_Dictionaries_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "Dictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscribedDictionary_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordPolish = table.Column<string>(type: "varchar", nullable: false),
                    WordTranslated = table.Column<string>(type: "varchar", nullable: false),
                    DictionaryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_Dictionaries_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "Dictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dictionaries_UserId",
                table: "Dictionaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionStatistics_DictionaryId",
                table: "SessionStatistics",
                column: "DictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionStatistics_UserId",
                table: "SessionStatistics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscribedDictionary_DictionaryId",
                table: "SubscribedDictionary",
                column: "DictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscribedDictionary_UserId",
                table: "SubscribedDictionary",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_DictionaryId",
                table: "Words",
                column: "DictionaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionStatistics");

            migrationBuilder.DropTable(
                name: "SubscribedDictionary");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Dictionaries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
