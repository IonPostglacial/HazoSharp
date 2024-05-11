using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hazo.Migrations
{
    /// <inheritdoc />
    public partial class InitializeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    Doc_Order = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Ref);
                });

            migrationBuilder.CreateTable(
                name: "Lang",
                columns: table => new
                {
                    Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lang", x => x.Ref);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Base_Unit_Ref = table.Column<string>(type: "TEXT", nullable: true),
                    To_Base_Unit_Factor = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Ref);
                    table.ForeignKey(
                        name: "FK_Unit_Unit_Base_Unit_Ref",
                        column: x => x.Base_Unit_Ref,
                        principalTable: "Unit",
                        principalColumn: "Ref");
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Document_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    ISBN = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Document_Ref);
                    table.ForeignKey(
                        name: "FK_Book_Document_Document_Ref",
                        column: x => x.Document_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref");
                });

            migrationBuilder.CreateTable(
                name: "Categorical_Character",
                columns: table => new
                {
                    Document_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorical_Character", x => x.Document_Ref);
                    table.ForeignKey(
                        name: "FK_Categorical_Character_Document_Document_Ref",
                        column: x => x.Document_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref");
                });

            migrationBuilder.CreateTable(
                name: "Descriptor_Visibility_Inapplicable",
                columns: table => new
                {
                    Descriptor_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Inapplicable_Descriptor_Ref = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptor_Visibility_Inapplicable", x => new { x.Descriptor_Ref, x.Inapplicable_Descriptor_Ref });
                    table.ForeignKey(
                        name: "FK_Descriptor_Visibility_Inapplicable_Document_Descriptor_Ref",
                        column: x => x.Descriptor_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Descriptor_Visibility_Inapplicable_Document_Inapplicable_Descriptor_Ref",
                        column: x => x.Inapplicable_Descriptor_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Descriptor_Visibility_Requirement",
                columns: table => new
                {
                    Descriptor_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Required_Descriptor_Ref = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptor_Visibility_Requirement", x => new { x.Descriptor_Ref, x.Required_Descriptor_Ref });
                    table.ForeignKey(
                        name: "FK_Descriptor_Visibility_Requirement_Document_Descriptor_Ref",
                        column: x => x.Descriptor_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Descriptor_Visibility_Requirement_Document_Required_Descriptor_Ref",
                        column: x => x.Required_Descriptor_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Document_Attachment",
                columns: table => new
                {
                    Document_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Attachment_Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document_Attachment", x => new { x.Document_Ref, x.Attachment_Index });
                    table.ForeignKey(
                        name: "FK_Document_Attachment_Document_Document_Ref",
                        column: x => x.Document_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Geographical_Place",
                columns: table => new
                {
                    Document_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    Scale = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geographical_Place", x => x.Document_Ref);
                    table.ForeignKey(
                        name: "FK_Geographical_Place_Document_Document_Ref",
                        column: x => x.Document_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref");
                });

            migrationBuilder.CreateTable(
                name: "Periodic_Character",
                columns: table => new
                {
                    Document_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Periodic_Category_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodic_Character", x => x.Document_Ref);
                    table.ForeignKey(
                        name: "FK_Periodic_Character_Document_Document_Ref",
                        column: x => x.Document_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref");
                    table.ForeignKey(
                        name: "FK_Periodic_Character_Document_Periodic_Category_Ref",
                        column: x => x.Periodic_Category_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Document_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Document_Ref);
                    table.ForeignKey(
                        name: "FK_State_Document_Document_Ref",
                        column: x => x.Document_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref");
                });

            migrationBuilder.CreateTable(
                name: "Taxon",
                columns: table => new
                {
                    Document_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    Meaning = table.Column<string>(type: "TEXT", nullable: true),
                    Herbarium_No = table.Column<string>(type: "TEXT", nullable: true),
                    Herbarium_Picture = table.Column<string>(type: "TEXT", nullable: true),
                    Fasc = table.Column<int>(type: "INTEGER", nullable: true),
                    Page = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxon", x => x.Document_Ref);
                    table.ForeignKey(
                        name: "FK_Taxon_Document_Document_Ref",
                        column: x => x.Document_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref");
                });

            migrationBuilder.CreateTable(
                name: "Document_Translation",
                columns: table => new
                {
                    Document_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Lang_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document_Translation", x => new { x.Document_Ref, x.Lang_Ref });
                    table.ForeignKey(
                        name: "FK_Document_Translation_Document_Document_Ref",
                        column: x => x.Document_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Document_Translation_Lang_Lang_Ref",
                        column: x => x.Lang_Ref,
                        principalTable: "Lang",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measurement_Character",
                columns: table => new
                {
                    Document_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    UnitRef = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurement_Character", x => x.Document_Ref);
                    table.ForeignKey(
                        name: "FK_Measurement_Character_Document_Document_Ref",
                        column: x => x.Document_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref");
                    table.ForeignKey(
                        name: "FK_Measurement_Character_Unit_UnitRef",
                        column: x => x.UnitRef,
                        principalTable: "Unit",
                        principalColumn: "Ref");
                });

            migrationBuilder.CreateTable(
                name: "Geographical_Map",
                columns: table => new
                {
                    Document_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Place_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Map_File = table.Column<string>(type: "TEXT", nullable: false),
                    Map_File_Feature_Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geographical_Map", x => x.Document_Ref);
                    table.ForeignKey(
                        name: "FK_Geographical_Map_Document_Document_Ref",
                        column: x => x.Document_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref");
                    table.ForeignKey(
                        name: "FK_Geographical_Map_Geographical_Place_Place_Ref",
                        column: x => x.Place_Ref,
                        principalTable: "Geographical_Place",
                        principalColumn: "Document_Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Taxon_Book_Info",
                columns: table => new
                {
                    Taxon_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Book_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Fasc = table.Column<int>(type: "INTEGER", nullable: true),
                    Page = table.Column<int>(type: "INTEGER", nullable: true),
                    Details = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxon_Book_Info", x => new { x.Taxon_Ref, x.Book_Ref });
                    table.ForeignKey(
                        name: "FK_Taxon_Book_Info_Book_Book_Ref",
                        column: x => x.Book_Ref,
                        principalTable: "Book",
                        principalColumn: "Document_Ref",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Taxon_Book_Info_Taxon_Taxon_Ref",
                        column: x => x.Taxon_Ref,
                        principalTable: "Taxon",
                        principalColumn: "Document_Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Taxon_Description",
                columns: table => new
                {
                    Taxon_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Description_Ref = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxon_Description", x => new { x.Taxon_Ref, x.Description_Ref });
                    table.ForeignKey(
                        name: "FK_Taxon_Description_Document_Description_Ref",
                        column: x => x.Description_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Taxon_Description_Taxon_Taxon_Ref",
                        column: x => x.Taxon_Ref,
                        principalTable: "Taxon",
                        principalColumn: "Document_Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Taxon_Specimen_Location",
                columns: table => new
                {
                    Taxon_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Specimen_Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Longitude = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxon_Specimen_Location", x => new { x.Taxon_Ref, x.Specimen_Index });
                    table.ForeignKey(
                        name: "FK_Taxon_Specimen_Location_Taxon_Taxon_Ref",
                        column: x => x.Taxon_Ref,
                        principalTable: "Taxon",
                        principalColumn: "Document_Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Taxon_Measurement",
                columns: table => new
                {
                    Taxon_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Character_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Minimum = table.Column<double>(type: "REAL", nullable: true),
                    Maximum = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxon_Measurement", x => new { x.Taxon_Ref, x.Character_Ref });
                    table.ForeignKey(
                        name: "FK_Taxon_Measurement_Measurement_Character_Character_Ref",
                        column: x => x.Character_Ref,
                        principalTable: "Measurement_Character",
                        principalColumn: "Document_Ref",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Taxon_Measurement_Taxon_Taxon_Ref",
                        column: x => x.Taxon_Ref,
                        principalTable: "Taxon",
                        principalColumn: "Document_Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Geographical_Character",
                columns: table => new
                {
                    Document_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Map_Ref = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geographical_Character", x => x.Document_Ref);
                    table.ForeignKey(
                        name: "FK_Geographical_Character_Document_Document_Ref",
                        column: x => x.Document_Ref,
                        principalTable: "Document",
                        principalColumn: "Ref");
                    table.ForeignKey(
                        name: "FK_Geographical_Character_Geographical_Map_Map_Ref",
                        column: x => x.Map_Ref,
                        principalTable: "Geographical_Map",
                        principalColumn: "Document_Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Document",
                columns: new[] { "Ref", "Details", "Name", "Doc_Order", "Path" },
                values: new object[,]
                {
                    { "_cal", "Gregorian calendar", "Calendar", 0, "" },
                    { "_geo", "All geographical places", "Geographical Places", 1, "" },
                    { "_geo_mada", "The island of Madagascar", "Madagascar", 2, "_geo" }
                });

            migrationBuilder.InsertData(
                table: "Lang",
                columns: new[] { "Ref", "Name" },
                values: new object[,]
                {
                    { "CN", "Chinese" },
                    { "EN", "English" },
                    { "FR", "French" },
                    { "S2", "Name 2" },
                    { "V", "Vernacular" },
                    { "V2", "Vernacular Name 2" }
                });

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "Ref", "Base_Unit_Ref", "To_Base_Unit_Factor" },
                values: new object[,]
                {
                    { "kg", null, 0.0 },
                    { "m", null, 0.0 },
                    { "nbr", null, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Geographical_Place",
                columns: new[] { "Document_Ref", "Latitude", "Longitude", "Scale" },
                values: new object[] { "_geo_mada", 46.518366999999998, -18.546564, 2000 });

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "Ref", "Base_Unit_Ref", "To_Base_Unit_Factor" },
                values: new object[,]
                {
                    { "cm", "m", 100.0 },
                    { "g", "kg", 1000.0 },
                    { "km", "m", 0.001 },
                    { "mm", "m", 1000.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Descriptor_Visibility_Inapplicable_Inapplicable_Descriptor_Ref",
                table: "Descriptor_Visibility_Inapplicable",
                column: "Inapplicable_Descriptor_Ref");

            migrationBuilder.CreateIndex(
                name: "IX_Descriptor_Visibility_Requirement_Required_Descriptor_Ref",
                table: "Descriptor_Visibility_Requirement",
                column: "Required_Descriptor_Ref");

            migrationBuilder.CreateIndex(
                name: "IX_Document_Translation_Lang_Ref",
                table: "Document_Translation",
                column: "Lang_Ref");

            migrationBuilder.CreateIndex(
                name: "IX_Geographical_Character_Map_Ref",
                table: "Geographical_Character",
                column: "Map_Ref");

            migrationBuilder.CreateIndex(
                name: "IX_Geographical_Map_Place_Ref",
                table: "Geographical_Map",
                column: "Place_Ref");

            migrationBuilder.CreateIndex(
                name: "IX_Measurement_Character_UnitRef",
                table: "Measurement_Character",
                column: "UnitRef");

            migrationBuilder.CreateIndex(
                name: "IX_Periodic_Character_Periodic_Category_Ref",
                table: "Periodic_Character",
                column: "Periodic_Category_Ref");

            migrationBuilder.CreateIndex(
                name: "IX_Taxon_Book_Info_Book_Ref",
                table: "Taxon_Book_Info",
                column: "Book_Ref");

            migrationBuilder.CreateIndex(
                name: "IX_Taxon_Description_Description_Ref",
                table: "Taxon_Description",
                column: "Description_Ref");

            migrationBuilder.CreateIndex(
                name: "IX_Taxon_Measurement_Character_Ref",
                table: "Taxon_Measurement",
                column: "Character_Ref");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_Base_Unit_Ref",
                table: "Unit",
                column: "Base_Unit_Ref");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorical_Character");

            migrationBuilder.DropTable(
                name: "Descriptor_Visibility_Inapplicable");

            migrationBuilder.DropTable(
                name: "Descriptor_Visibility_Requirement");

            migrationBuilder.DropTable(
                name: "Document_Attachment");

            migrationBuilder.DropTable(
                name: "Document_Translation");

            migrationBuilder.DropTable(
                name: "Geographical_Character");

            migrationBuilder.DropTable(
                name: "Periodic_Character");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Taxon_Book_Info");

            migrationBuilder.DropTable(
                name: "Taxon_Description");

            migrationBuilder.DropTable(
                name: "Taxon_Measurement");

            migrationBuilder.DropTable(
                name: "Taxon_Specimen_Location");

            migrationBuilder.DropTable(
                name: "Lang");

            migrationBuilder.DropTable(
                name: "Geographical_Map");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Measurement_Character");

            migrationBuilder.DropTable(
                name: "Taxon");

            migrationBuilder.DropTable(
                name: "Geographical_Place");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Document");
        }
    }
}
