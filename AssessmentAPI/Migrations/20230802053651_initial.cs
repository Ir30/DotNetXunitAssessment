using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssessmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AOTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    History = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Boundary = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Log = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Cache = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Notify = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Identifier = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_AOTable_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AOColumn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DataSize = table.Column<int>(type: "int", nullable: true),
                    DataScale = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Encrypted = table.Column<int>(type: "int", nullable: true),
                    Distortion = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_AOColumn_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_AOColumn_AOTable",
                        column: x => x.TableId,
                        principalTable: "AOTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_AOColumn_Name",
                table: "AOColumn",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "ix_AOColumn_TableId",
                table: "AOColumn",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "ix_AOTable_Name",
                table: "AOTable",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AOColumn");

            migrationBuilder.DropTable(
                name: "AOTable");
        }
    }
}
