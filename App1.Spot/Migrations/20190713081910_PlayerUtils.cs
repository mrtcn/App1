using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App1.Spot.Migrations
{
    public partial class PlayerUtils : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Player",
                newName: "PictureUrl");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Player",
                newName: "Nickname");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Player",
                newName: "LastName");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Spot",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Spot",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Player",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Spot",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageUrl", "LastModifiedDate", "Latitude", "Longitude", "Name", "ShortDescription", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 7, 13, 11, 19, 10, 228, DateTimeKind.Local).AddTicks(7937), "Very Magnificiant Tennis Club with a lot of blabla and blabla", "", null, 40.871516999999997, 29.089058999999999, "Heybeliada Tennis Club", "Greatest Tennis Club of the World", 1 },
                    { 2, new DateTime(2019, 7, 13, 11, 19, 10, 228, DateTimeKind.Local).AddTicks(8444), "Very Magnificiant Tennis Club with a lot of blabla and blabla", "", null, 40.906027000000002, 29.048176000000002, "Kınalıada Tennis Club", "Greatest Tennis Club of the World", 1 },
                    { 3, new DateTime(2019, 7, 13, 11, 19, 10, 228, DateTimeKind.Local).AddTicks(8450), "Very Magnificiant Tennis Club with a lot of blabla and blabla", "", null, 40.880651, 29.061132000000001, "Burgazada Tennis Club", "Greatest Tennis Club of the World", 1 },
                    { 4, new DateTime(2019, 7, 13, 11, 19, 10, 228, DateTimeKind.Local).AddTicks(8453), "Very Magnificiant Tennis Club with a lot of blabla and blabla", "", null, 40.958429000000002, 29.081837, "Suadiye Tennis Club", "Greatest Tennis Club of the World", 1 },
                    { 5, new DateTime(2019, 7, 13, 11, 19, 10, 228, DateTimeKind.Local).AddTicks(8456), "Very Magnificiant Tennis Club with a lot of blabla and blabla", "", null, 40.991616999999998, 29.025068999999998, "Kadıköy Tennis Club", "Greatest Tennis Club of the World", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Spot",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Spot",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Spot",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Spot",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Spot",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Spot");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Spot");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Player",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Player",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Player",
                newName: "ImageUrl");
        }
    }
}
