using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ibdb.Shared.Migrations.Outbox
{
    public partial class OutboxInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "outbox",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Event Id."),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()", comment: "Event timestamp."),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Event name."),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "An Id of an entity which this event related to."),
                    data_version = table.Column<int>(type: "integer", nullable: false, comment: "Event data version."),
                    data = table.Column<string>(type: "text", nullable: false, comment: "Event data.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outbox", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_outbox_timestamp",
                table: "outbox",
                column: "timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "outbox");
        }
    }
}
