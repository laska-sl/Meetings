using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Meetings.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Meetings",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    IsCanceled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Participants",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "MeetingParticipants",
                table => new
                {
                    MeetingId = table.Column<int>(nullable: false),
                    ParticipantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingParticipants", x => new {x.MeetingId, x.ParticipantId});

                    table.ForeignKey(
                        "FK_MeetingParticipants_Meetings_MeetingId",
                        x => x.MeetingId,
                        "Meetings",
                        "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        "FK_MeetingParticipants_Participants_ParticipantId",
                        x => x.ParticipantId,
                        "Participants",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_MeetingParticipants_ParticipantId",
                "MeetingParticipants",
                "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "MeetingParticipants");

            migrationBuilder.DropTable(
                "Meetings");

            migrationBuilder.DropTable(
                "Participants");
        }
    }
}
