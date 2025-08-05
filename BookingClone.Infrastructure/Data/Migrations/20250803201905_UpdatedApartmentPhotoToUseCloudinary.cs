using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingClone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedApartmentPhotoToUseCloudinary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "ApartmentPhotos");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Apartments",
                newName: "PricePerDay");

            migrationBuilder.RenameColumn(
                name: "ImageType",
                table: "ApartmentPhotos",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "ApartmentPhotos",
                newName: "PublicId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Owners",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BankAccount",
                table: "Owners",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainPhoto",
                table: "ApartmentPhotos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_BankAccount",
                table: "Owners",
                column: "BankAccount",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_PhoneNumber",
                table: "Owners",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Owners_BankAccount",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_PhoneNumber",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "IsMainPhoto",
                table: "ApartmentPhotos");

            migrationBuilder.RenameColumn(
                name: "PricePerDay",
                table: "Apartments",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ApartmentPhotos",
                newName: "ImageType");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "ApartmentPhotos",
                newName: "ImageName");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BankAccount",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "ApartmentPhotos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
