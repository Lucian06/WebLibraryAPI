using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebLibrary.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Main");

            migrationBuilder.CreateTable(
                name: "Authors",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "character varying(560)", maxLength: 560, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Title = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Cover = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                schema: "Main",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BooksId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalSchema: "Main",
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalSchema: "Main",
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Main",
                table: "Authors",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0e2786ff-fd34-4551-a4ca-27820cbd0420"), null, "Barbara Cartland" },
                    { new Guid("2616bc1b-75ee-414e-a9ee-b132526a57f6"), null, "Danielle Steel" },
                    { new Guid("4808d76e-e4af-4985-8056-cb214a9977bf"), "Dame Agatha Mary Clarissa Christie, was an English writer known for her 66 detective novels and 14 short story collections, particularly those revolving around fictional detectives Hercule Poirot and Miss Marple.", "Agatha Christie" },
                    { new Guid("497da1e8-04e0-4571-a404-d1983c132535"), "William Shakespeare was an English playwright, poet and actor.", "William Shakespeare" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                schema: "Main",
                table: "AuthorBook",
                column: "BooksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook",
                schema: "Main");

            migrationBuilder.DropTable(
                name: "Authors",
                schema: "Main");

            migrationBuilder.DropTable(
                name: "Books",
                schema: "Main");
        }
    }
}
