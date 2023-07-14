#nullable disable

namespace PetShop.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeTypeId", "AnimalTypeId", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "UserId" },
                values: new object[] { new Guid("20ccf9c7-6800-42f2-ab4b-42f2b5885142"), 3, 1, 2, "This is cozy and warm cloth for you dog.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRRBjhNm1ffsW7Von6PLqbcd6Syserv-j6pxw&usqp=CAU", "Dog sweatshirt", 10.00m, new Guid("f9a6a707-3ee6-4ae8-92ee-7d49f67c9242"), null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeTypeId", "AnimalTypeId", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "UserId" },
                values: new object[] { new Guid("33ded947-89a2-4caa-9e86-e949b605bb39"), 2, 4, 1, "This is the best food for you fishes, it is rich in vitams and minerals.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRN5RTYv_uPWx8wEV-D8KFyw6iSQ2wmnP-aPA&usqp=CAU", "Fish food", 5.00m, new Guid("f9a6a707-3ee6-4ae8-92ee-7d49f67c9242"), null });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20ccf9c7-6800-42f2-ab4b-42f2b5885142"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33ded947-89a2-4caa-9e86-e949b605bb39"));

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
