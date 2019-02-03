using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Api.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "Varieties",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Varieties", x => x.Id); });

            migrationBuilder.CreateTable(
                "Fruits",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    VarietyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fruits", x => x.Id);
                    table.ForeignKey(
                        "FK_Fruits_Varieties_VarietyId",
                        x => x.VarietyId,
                        "Varieties",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Batches",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FruitId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                    table.ForeignKey(
                        "FK_Batches_Fruits_FruitId",
                        x => x.FruitId,
                        "Fruits",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "Users",
                new[] {"Id", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Role", "Username"},
                new object[]
                {
                    1, "First", "First",
                    new byte[]
                    {
                        73, 39, 54, 240, 62, 133, 6, 110, 215, 178, 142, 161, 206, 60, 30, 255, 63, 214, 151, 1, 126, 229, 78, 121, 239, 155, 103, 245, 104, 38, 50, 120, 146, 90, 43, 168, 250, 238,
                        135, 96, 81, 235, 32, 242, 51, 82, 173, 60, 104, 223, 153, 219, 162, 206, 133, 8, 164, 100, 177, 232, 147, 104, 33, 3
                    },
                    new byte[]
                    {
                        23, 84, 56, 48, 114, 153, 49, 105, 119, 68, 98, 190, 40, 117, 12, 10, 54, 229, 109, 50, 144, 224, 24, 115, 91, 61, 85, 177, 149, 221, 60, 196, 127, 120, 215, 226, 130, 228, 75,
                        25, 199, 125, 168, 90, 93, 15, 178, 18, 21, 222, 48, 71, 142, 55, 179, 1, 164, 84, 214, 25, 204, 44, 59, 249, 119, 172, 228, 103, 172, 131, 236, 91, 183, 190, 204, 2, 80, 155,
                        91, 209, 63, 115, 92, 40, 71, 40, 187, 203, 83, 61, 67, 51, 41, 171, 118, 220, 213, 37, 72, 43, 4, 244, 73, 5, 198, 98, 203, 192, 165, 40, 2, 0, 49, 138, 246, 186, 49, 104, 77,
                        216, 202, 83, 172, 177, 162, 199, 62, 69
                    },
                    "User", "FirstUser"
                });

            migrationBuilder.InsertData(
                "Users",
                new[] {"Id", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Role", "Username"},
                new object[]
                {
                    2, "Second", "Second",
                    new byte[]
                    {
                        50, 90, 184, 163, 83, 173, 183, 112, 229, 49, 188, 166, 196, 155, 44, 176, 19, 228, 242, 106, 191, 107, 189, 240, 195, 53, 111, 230, 169, 143, 160, 148, 83, 208, 118, 144, 218,
                        31, 88, 170, 156, 248, 99, 180, 131, 218, 139, 244, 185, 208, 148, 39, 64, 66, 143, 146, 92, 81, 53, 203, 123, 15, 167, 33
                    },
                    new byte[]
                    {
                        215, 63, 122, 139, 86, 89, 109, 162, 160, 20, 95, 65, 253, 76, 140, 185, 213, 192, 19, 151, 179, 29, 177, 183, 57, 92, 119, 135, 115, 64, 88, 114, 12, 94, 118, 184, 121, 181,
                        4, 8, 142, 152, 37, 192, 69, 43, 11, 14, 243, 122, 116, 193, 153, 160, 61, 15, 77, 73, 118, 2, 138, 40, 9, 95, 218, 75, 129, 89, 157, 234, 16, 248, 222, 177, 126, 98, 203, 119,
                        173, 162, 177, 136, 232, 196, 102, 67, 70, 221, 24, 122, 153, 156, 55, 116, 129, 6, 137, 151, 84, 243, 6, 236, 38, 117, 208, 74, 165, 13, 218, 123, 241, 12, 180, 2, 102, 221,
                        240, 227, 118, 145, 93, 5, 40, 244, 204, 56, 255, 20
                    },
                    "User", "SecondUser"
                });

            migrationBuilder.InsertData(
                "Users",
                new[] {"Id", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Role", "Username"},
                new object[]
                {
                    3, "Second", "Second",
                    new byte[]
                    {
                        116, 32, 225, 238, 68, 47, 149, 58, 45, 216, 104, 15, 129, 131, 184, 193, 74, 209, 106, 185, 120, 77, 149, 172, 154, 161, 106, 240, 154, 100, 14, 161, 91, 195, 221, 81, 88, 11,
                        231, 244, 60, 247, 145, 7, 64, 192, 175, 119, 147, 182, 3, 194, 149, 6, 252, 192, 159, 172, 138, 119, 1, 207, 59, 68
                    },
                    new byte[]
                    {
                        150, 148, 119, 161, 20, 128, 98, 60, 226, 68, 234, 145, 145, 237, 106, 150, 11, 118, 46, 93, 195, 6, 9, 255, 249, 21, 194, 80, 232, 232, 9, 171, 70, 64, 6, 211, 15, 47, 35,
                        201, 36, 129, 163, 244, 46, 107, 116, 84, 52, 241, 58, 111, 94, 189, 152, 69, 33, 132, 240, 18, 33, 122, 208, 4, 228, 242, 76, 61, 72, 236, 42, 97, 197, 167, 169, 57, 145, 246,
                        71, 253, 58, 12, 155, 25, 6, 125, 104, 171, 197, 107, 63, 49, 84, 77, 28, 105, 200, 233, 226, 173, 65, 58, 176, 39, 168, 212, 105, 191, 171, 94, 221, 168, 36, 135, 179, 113,
                        192, 156, 42, 206, 63, 88, 113, 32, 181, 6, 42, 246
                    },
                    "Admin", "admin"
                });

            migrationBuilder.InsertData(
                "Varieties",
                new[] {"Id", "Name"},
                new object[] {2, "Alba"});

            migrationBuilder.InsertData(
                "Varieties",
                new[] {"Id", "Name"},
                new object[] {3, "Erika"});

            migrationBuilder.InsertData(
                "Varieties",
                new[] {"Id", "Name"},
                new object[] {1, "Amira"});

            migrationBuilder.InsertData(
                "Varieties",
                new[] {"Id", "Name"},
                new object[] {4, "Test"});

            migrationBuilder.InsertData(
                "Fruits",
                new[] {"Id", "Name", "VarietyId"},
                new object[] {1, "Blueberry", 2});

            migrationBuilder.InsertData(
                "Fruits",
                new[] {"Id", "Name", "VarietyId"},
                new object[] {3, "Raspberry", 3});

            migrationBuilder.InsertData(
                "Fruits",
                new[] {"Id", "Name", "VarietyId"},
                new object[] {2, "Raspberry", 1});

            migrationBuilder.InsertData(
                "Fruits",
                new[] {"Id", "Name", "VarietyId"},
                new object[] {4, "Raspberry", 4});

            migrationBuilder.InsertData(
                "Batches",
                new[] {"Id", "FruitId", "Quantity"},
                new object[] {4, 1, 15});

            migrationBuilder.InsertData(
                "Batches",
                new[] {"Id", "FruitId", "Quantity"},
                new object[] {2, 3, 10});

            migrationBuilder.InsertData(
                "Batches",
                new[] {"Id", "FruitId", "Quantity"},
                new object[] {1, 2, 12});

            migrationBuilder.InsertData(
                "Batches",
                new[] {"Id", "FruitId", "Quantity"},
                new object[] {3, 2, 10});

            migrationBuilder.CreateIndex(
                "IX_Batches_FruitId",
                "Batches",
                "FruitId");

            migrationBuilder.CreateIndex(
                "IX_Fruits_VarietyId",
                "Fruits",
                "VarietyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Batches");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "Fruits");

            migrationBuilder.DropTable(
                "Varieties");
        }
    }
}