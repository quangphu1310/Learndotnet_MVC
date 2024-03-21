using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuangPhu.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    listPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "Price", "Price100", "Price50", "Title", "listPrice" },
                values: new object[,]
                {
                    { 1, "Quang Phu", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas consectetur fermentum justo quis tincidunt. Maecenas blandit quis nibh a malesuada. Suspendisse sed lacus massa.", "DH2354", 90.0, 80.0, 85.0, "Fortune of Time", 99.0 },
                    { 2, "Phuc Duc", "Donec semper eleifend odio, sed dictum lectus semper quis. In lacinia eu lectus vitae pharetra. Nulla sollicitudin ipsum quis ultrices vestibulum.", "SJ8923", 30.0, 20.0, 25.0, "Dark Skies", 50.0 },
                    { 3, "Tran Luong", "Nullam hendrerit ut velit sed eleifend. Duis vulputate risus nec velit finibus, porta porta felis lacinia. Ut condimentum non ex sed convallis. ", "HE335345", 40.0, 30.0, 35.0, "Sunset Sunrise", 55.0 },
                    { 4, "Van Lam", "Maecenas sollicitudin, lorem ac porta aliquet, tortor quam cursus sapien, id aliquam nisl diam bibendum nulla. Suspendisse cursus magna ut euismod egestas.", "SF32544", 30.0, 20.0, 25.0, "Maecenas", 49.0 },
                    { 5, "Truc", "Fusce vitae pellentesque felis. Etiam ipsum nisl, mattis sed ligula nec, lobortis finibus justo. Etiam accumsan magna non nisl aliquet rutrum.", "HE345346", 60.0, 50.0, 55.0, "Fusce vitae", 80.0 },
                    { 6, "Pellentesque", " Pellentesque urna ipsum, lacinia nec dignissim vitae, condimentum a sapien. Nam nisl sem, feugiat at varius et, vehicula bibendum leo.", "IH3438767", 50.0, 40.0, 45.0, "Fortune of Time", 70.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
