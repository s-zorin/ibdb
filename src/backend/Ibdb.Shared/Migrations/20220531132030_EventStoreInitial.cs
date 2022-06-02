using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ibdb.Shared.Migrations
{
    public partial class EventStoreInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "event_store",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Event Id."),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()", comment: "Event timestamp."),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Event name."),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "An Id of an entity which this event related to."),
                    entity_version = table.Column<int>(type: "integer", nullable: false, comment: "A would be entity version if this event was applied to it."),
                    data_version = table.Column<int>(type: "integer", nullable: false, comment: "Event data version."),
                    data = table.Column<string>(type: "text", nullable: false, comment: "Event data.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_store", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_store_entity_version",
                table: "event_store",
                column: "entity_version");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_store");
        }
    }
}
