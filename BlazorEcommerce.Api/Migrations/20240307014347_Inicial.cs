using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorEcommerce.Api.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IconCSS = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CartId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CartId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, "1" },
                    { 2, "2" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IconCSS", "Name" },
                values: new object[,]
                {
                    { 1, "fas fa-spa", "Beleza" },
                    { 2, "fas fa-couch", "Moveis" },
                    { 3, "fas fa-headphones", "Eletronicos" },
                    { 4, "fas fa-shoe-prints", "Calcados" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Amount", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 100, 1, "Um kit fornecido pela Natura, contendo produtos para cuidados com a pele", "/Imagens/Beleza/Beleza1.png", "Glossier - Beleza Kit", 100m },
                    { 2, 45, 1, "Um kit fornecido pela Curology, contendo produtos para cuidados com a pele", "/Imagens/Beleza/Beleza2.png", "Curology - Kit para Pele", 50m },
                    { 3, 30, 1, "Um kit fornecido pela Glossier, contendo produtos para cuidados com a pele", "/Imagens/Beleza/Beleza3.png", "Óleo de Coco Orgânico", 20m },
                    { 4, 60, 1, "Um kit fornecido pela Curology, contendo produtos para cuidados com a pele", "/Imagens/Beleza/Beleza4.png", "Schwarzkopf - Kit de cuidados com a pele e cabelo", 50m },
                    { 5, 85, 1, "Kit de cuidados com a pele, contendo produtos para cuidados com a pele e cabelos", "/Imagens/Beleza/Beleza5.png", "Kit de cuidados com a pele", 30m },
                    { 6, 120, 3, "Air Pods - fones de ouvido sem fio intra-auriculares", "/Imagens/Eletronicos/eletronico1.png", "Fones de ouvidos", 100m },
                    { 7, 200, 3, "Fones de ouvido dourados na orelha - esses fones de ouvido não são sem fio", "/Imagens/Eletronicos/eletronico2.png", "Fones de ouvido dourados", 40m },
                    { 8, 300, 3, "Fones de ouvido pretos na orelha - esses fones de ouvido não são sem fio", "/Imagens/Eletronicos/eletronico3.png", "Fones de ouvido pretos", 40m },
                    { 9, 20, 3, "Câmera Digital Sennheiser - Câmera digital de alta qualidade fornecida pela Sennheiser - inclui tripé", "/Imagens/Eletronicos/eletronico4.png", "Câmera digital Sennheiser com tripé", 600m },
                    { 10, 15, 3, "Canon Digital Camera - Câmera digital de alta qualidade fornecida pela Canon", "/Imagens/Eletronicos/eletronico5.png", "Câmera Digital Canon", 500m },
                    { 11, 60, 3, "Gameboy - Fornecido por Nintendo", "/Imagens/Eletronicos/tecnologia6.png", "Nintendo Gameboy", 100m },
                    { 12, 212, 2, "Cadeira de escritório em couro preto muito confortável", "/Imagens/Moveis/moveis1.png", "Cadeira de escritório de couro preto", 50m },
                    { 13, 112, 2, "Cadeira de escritório em couro rosa muito confortável", "/Imagens/Moveis/moveis2.png", "Cadeira de escritório de couro rosa", 50m },
                    { 14, 90, 2, "Poltrona muito confortável", "/Imagens/Moveis/moveis3.png", "Espreguiçadeira", 70m },
                    { 15, 95, 2, "Poltrona prateada muito confortável", "/Imagens/Moveis/moveis4.png", "Silver Lounge Chair", 120m },
                    { 16, 100, 2, "Abajur de mesa de porcelana branco e azul", "/Imagens/Moveis/moveis6.png", "Luminária de mesa de porcelana", 15m },
                    { 17, 73, 2, "Abajur de mesa de escritório", "/Imagens/Moveis/moveis7.png", "Office Table Lamp", 20m },
                    { 18, 50, 4, "Tênis Puma confortáveis na maioria dos tamanhos", "/Imagens/Calcados/calcado1.png", "Tênis Puma", 100m },
                    { 19, 60, 4, "Tênis coloridos - disponíveis na maioria dos tamanhos", "/Imagens/Calcados/calcado2.png", "Tênis Colodiros", 150m },
                    { 20, 70, 4, "Tênis Nike azul - disponível na maioria dos tamanhos", "/Imagens/Calcados/calcado3.png", "Tênis Nike Azul", 200m },
                    { 21, 120, 4, "Treinadores Hummel coloridos - disponíveis na maioria dos tamanhos", "/Imagens/Calcados/calcado4.png", "Tênis Hummel Coloridos", 120m },
                    { 22, 100, 4, "Tênis Nike vermelho - disponível na maioria dos tamanhos", "/Imagens/Calcados/calcado5.png", "Tênis Nike Vermelho", 200m },
                    { 23, 150, 4, "Sandálias Birkenstock - disponíveis na maioria dos tamanhos", "/Imagens/Calcados/calcado6.png", "Sandálidas Birkenstock", 50m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CartId",
                table: "Users",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
