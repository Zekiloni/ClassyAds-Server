using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClassyAdsServer.Migrations
{
    /// <inheritdoc />
    public partial class HyGuys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    category_name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    category_description = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.category_id);
                    table.ForeignKey(
                        name: "FK_categories_categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "categories",
                        principalColumn: "category_id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "classified_media_files",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ClassifiedId = table.Column<int>(type: "int", nullable: false),
                    url = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classified_media_files", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    first_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    email_address = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    is_email_verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    street = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    city = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    province = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    postal_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    hashed_password = table.Column<string>(type: "longtext", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    last_login_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "classifieds",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    short_description = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_negotiable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    product_make = table.Column<string>(type: "longtext", nullable: false),
                    product_model = table.Column<string>(type: "longtext", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    is_featured = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_premium = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    expiring_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classifieds", x => x.id);
                    table.ForeignKey(
                        name: "FK_classifieds_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_classifieds_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_categories_ParentCategoryId",
                table: "categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_classifieds_CategoryId",
                table: "classifieds",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_classifieds_UserId",
                table: "classifieds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "classified_media_files");

            migrationBuilder.DropTable(
                name: "classifieds");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
