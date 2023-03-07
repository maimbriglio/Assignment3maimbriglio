using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3maimbriglio.Data.Migrations
{
    public partial class addedActorMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Photo",
                table: "Movie",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "photo",
                table: "Actor",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorID = table.Column<int>(type: "int", nullable: true),
                    MovieID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actor_ActorID",
                        column: x => x.ActorID,
                        principalTable: "Actor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_ActorID",
                table: "ActorMovie",
                column: "ActorID");

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MovieID",
                table: "ActorMovie",
                column: "MovieID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Photo",
                table: "Movie",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "photo",
                table: "Actor",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
