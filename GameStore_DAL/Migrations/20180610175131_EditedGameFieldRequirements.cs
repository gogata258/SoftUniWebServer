using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore_DAL.Migrations
{
    public partial class EditedGameFieldRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cover",
                table: "Games",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cover",
                table: "Games",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
