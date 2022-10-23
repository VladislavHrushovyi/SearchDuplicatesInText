using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchDuplicatesText.Migrations
{
    public partial class renamecol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfChar",
                table: "ExpFiles",
                newName: "NumberOfPart");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExpFiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfPart",
                table: "ExpFiles",
                newName: "NumberOfChar");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExpFiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
