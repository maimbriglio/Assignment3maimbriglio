using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3maimbriglio.Data.Migrations
{
    public partial class ActorAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Actorid",
                table: "TweetWithScore",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AvgCompoundScore",
                table: "Actor",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TweetWithScore_Actorid",
                table: "TweetWithScore",
                column: "Actorid");

            migrationBuilder.AddForeignKey(
                name: "FK_TweetWithScore_Actor_Actorid",
                table: "TweetWithScore",
                column: "Actorid",
                principalTable: "Actor",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TweetWithScore_Actor_Actorid",
                table: "TweetWithScore");

            migrationBuilder.DropIndex(
                name: "IX_TweetWithScore_Actorid",
                table: "TweetWithScore");

            migrationBuilder.DropColumn(
                name: "Actorid",
                table: "TweetWithScore");

            migrationBuilder.DropColumn(
                name: "AvgCompoundScore",
                table: "Actor");
        }
    }
}
