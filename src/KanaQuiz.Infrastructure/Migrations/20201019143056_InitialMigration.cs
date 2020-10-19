using Microsoft.EntityFrameworkCore.Migrations;

namespace KanaQuiz.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kanas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Value = table.Column<string>(maxLength: 255, nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Romanji = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Kanas",
                columns: new[] { "Id", "Romanji", "Type", "Value" },
                values: new object[,]
                {
                    { 1L, "a", 0, "あ" },
                    { 2L, "i", 0, "い" },
                    { 3L, "u", 0, "う" },
                    { 4L, "e", 0, "え" },
                    { 5L, "o", 0, "お" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kanas");
        }
    }
}
