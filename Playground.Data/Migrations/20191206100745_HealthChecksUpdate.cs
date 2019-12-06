using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Playground.Data.Migrations
{
    public partial class HealthChecksUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthCheckConfiguration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Retries = table.Column<int>(nullable: false),
                    SleepInMillsBetweenRetry = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCheckConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealtCheck",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Alias = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    HealthCheckConfigurationId = table.Column<Guid>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    ValidResponses = table.Column<string>(nullable: true),
                    Headers = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealtCheck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealtCheck_HealthCheckConfiguration_HealthCheckConfigurationId",
                        column: x => x.HealthCheckConfigurationId,
                        principalTable: "HealthCheckConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealtCheck_HealthCheckConfigurationId",
                table: "HealtCheck",
                column: "HealthCheckConfigurationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealtCheck");

            migrationBuilder.DropTable(
                name: "HealthCheckConfiguration");
        }
    }
}
