using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3maimbriglio.Data.Migrations
{
    public partial class databasefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorTweets");

            migrationBuilder.DropTable(
                name: "TweetWithScore");

            migrationBuilder.DropTable(
                name: "TweetViewModel");

            migrationBuilder.DropColumn(
                name: "AvgCompoundScore",
                table: "Actor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AvgCompoundScore",
                table: "Actor",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TweetViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgCompoundScore = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActorTweets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorID = table.Column<int>(type: "int", nullable: true),
                    TweetID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorTweets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActorTweets_Actor_ActorID",
                        column: x => x.ActorID,
                        principalTable: "Actor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ActorTweets_TweetViewModel_TweetID",
                        column: x => x.TweetID,
                        principalTable: "TweetViewModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TweetWithScore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Actorid = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<double>(type: "float", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TweetViewModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetWithScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetWithScore_Actor_Actorid",
                        column: x => x.Actorid,
                        principalTable: "Actor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TweetWithScore_TweetViewModel_TweetViewModelId",
                        column: x => x.TweetViewModelId,
                        principalTable: "TweetViewModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorTweets_ActorID",
                table: "ActorTweets",
                column: "ActorID");

            migrationBuilder.CreateIndex(
                name: "IX_ActorTweets_TweetID",
                table: "ActorTweets",
                column: "TweetID");

            migrationBuilder.CreateIndex(
                name: "IX_TweetWithScore_Actorid",
                table: "TweetWithScore",
                column: "Actorid");

            migrationBuilder.CreateIndex(
                name: "IX_TweetWithScore_TweetViewModelId",
                table: "TweetWithScore",
                column: "TweetViewModelId");
        }
    }
}
