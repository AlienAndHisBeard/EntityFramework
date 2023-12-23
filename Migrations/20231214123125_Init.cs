using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderedPriceSum = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderEntries_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderEntries_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "Name", "OrderedPriceSum", "Type" },
                values: new object[,]
                {
                    { 1, "659 Bergstrom Falls, South Yolanda, Papua New Guinea", "Nigel Zboncak", 0, "N" },
                    { 2, "09304 Howe Drive, East Kevonland, Australia", "Katelin Torp", 0, "N" },
                    { 3, "00182 Lilly Knoll, Laurystad, Saint Martin", "Gladyce Friesen", 0, "N" },
                    { 4, "11897 Abbott Summit, West Erikaland, Panama", "Earlene Bernier", 0, "N" },
                    { 5, "76462 Zieme Pike, Fadelton, Norfolk Island", "Arch Ritchie", 0, "N" },
                    { 6, "207 Virgil Pine, Hellerstad, Tanzania", "Kayli Metz", 0, "N" },
                    { 7, "080 Lockman Dale, Othoberg, Liechtenstein", "Demond Toy", 0, "N" },
                    { 8, "334 Ritchie Haven, Feestfort, Kazakhstan", "Wilmer Mayert", 0, "N" },
                    { 9, "2709 Jeff Turnpike, North Annetteborough, Guadeloupe", "Brenna Fritsch", 0, "N" },
                    { 10, "435 Darwin Field, South Ebbaborough, New Zealand", "Burnice Johnston", 0, "N" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "IpAddress", "Name", "OrderedPriceSum", "Type" },
                values: new object[,]
                {
                    { 11, "8696 Lonnie Squares, Odaville, Serbia", "11.107.243.243", "Columbus Kuphal", 0, "E" },
                    { 12, "99139 Wade Villages, Aufderharberg, Greenland", "163.105.180.202", "Silas Osinski", 0, "E" },
                    { 13, "542 Goyette Curve, Botsfordview, Senegal", "165.74.181.29", "Dawn Ward", 0, "E" },
                    { 14, "291 Jonathan Village, Port Jesusshire, Azerbaijan", "125.205.89.167", "Brenden Kessler", 0, "E" },
                    { 15, "082 Melba Ports, Rosenbaumside, Liberia", "78.14.212.127", "Columbus Muller", 0, "E" },
                    { 16, "124 Roob Drive, Port Arvid, Tajikistan", "75.11.225.183", "Margie Aufderhar", 0, "E" },
                    { 17, "73623 Lehner Shore, North Ethan, Belize", "178.64.232.224", "Aiden Hessel", 0, "E" },
                    { 18, "9187 Wendy Center, New Forest, Bahamas", "9.112.19.137", "Dillan Rutherford", 0, "E" },
                    { 19, "750 Huel Shoals, Nicolasville, Norfolk Island", "57.192.239.216", "Keely Rowe", 0, "E" },
                    { 20, "303 Leuschke Crossing, Schmittview, United States of America", "2.159.220.183", "Mason Rippin", 0, "E" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "Name", "OrderedPriceSum", "Type" },
                values: new object[,]
                {
                    { 21, "659 Bergstrom Falls, South Yolanda, Papua New Guinea", "Nigel Zboncak", 0, "N" },
                    { 22, "09304 Howe Drive, East Kevonland, Australia", "Katelin Torp", 0, "N" },
                    { 23, "00182 Lilly Knoll, Laurystad, Saint Martin", "Gladyce Friesen", 0, "N" },
                    { 24, "11897 Abbott Summit, West Erikaland, Panama", "Earlene Bernier", 0, "N" },
                    { 25, "76462 Zieme Pike, Fadelton, Norfolk Island", "Arch Ritchie", 0, "N" },
                    { 26, "207 Virgil Pine, Hellerstad, Tanzania", "Kayli Metz", 0, "N" },
                    { 27, "080 Lockman Dale, Othoberg, Liechtenstein", "Demond Toy", 0, "N" },
                    { 28, "334 Ritchie Haven, Feestfort, Kazakhstan", "Wilmer Mayert", 0, "N" },
                    { 29, "2709 Jeff Turnpike, North Annetteborough, Guadeloupe", "Brenna Fritsch", 0, "N" },
                    { 30, "435 Darwin Field, South Ebbaborough, New Zealand", "Burnice Johnston", 0, "N" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "IpAddress", "Name", "OrderedPriceSum", "Type" },
                values: new object[,]
                {
                    { 31, "8696 Lonnie Squares, Odaville, Serbia", "11.107.243.243", "Columbus Kuphal", 0, "E" },
                    { 32, "99139 Wade Villages, Aufderharberg, Greenland", "163.105.180.202", "Silas Osinski", 0, "E" },
                    { 33, "542 Goyette Curve, Botsfordview, Senegal", "165.74.181.29", "Dawn Ward", 0, "E" },
                    { 34, "291 Jonathan Village, Port Jesusshire, Azerbaijan", "125.205.89.167", "Brenden Kessler", 0, "E" },
                    { 35, "082 Melba Ports, Rosenbaumside, Liberia", "78.14.212.127", "Columbus Muller", 0, "E" },
                    { 36, "124 Roob Drive, Port Arvid, Tajikistan", "75.11.225.183", "Margie Aufderhar", 0, "E" },
                    { 37, "73623 Lehner Shore, North Ethan, Belize", "178.64.232.224", "Aiden Hessel", 0, "E" },
                    { 38, "9187 Wendy Center, New Forest, Bahamas", "9.112.19.137", "Dillan Rutherford", 0, "E" },
                    { 39, "750 Huel Shoals, Nicolasville, Norfolk Island", "57.192.239.216", "Keely Rowe", 0, "E" },
                    { 40, "303 Leuschke Crossing, Schmittview, United States of America", "2.159.220.183", "Mason Rippin", 0, "E" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Forward Quality Producer", 150, 1810 },
                    { 2, "Dynamic Paradigm Consultant", 222, 3057 },
                    { 3, "Global Quality Analyst", 514, 2657 },
                    { 4, "Global Usability Representative", 29, 7454 },
                    { 5, "National Infrastructure Representative", 156, 3015 },
                    { 6, "Future Identity Specialist", 998, 222 },
                    { 7, "District Mobility Administrator", 69, 4288 },
                    { 8, "Central Directives Designer", 831, 7002 },
                    { 9, "Future Division Strategist", 213, 5450 },
                    { 10, "Legacy Infrastructure Officer", 583, 9140 },
                    { 11, "National Assurance Representative", 770, 2976 },
                    { 12, "Chief Tactics Analyst", 483, 7749 },
                    { 13, "Forward Configuration Engineer", 90, 7337 },
                    { 14, "Dynamic Marketing Representative", 564, 1748 },
                    { 15, "Future Intranet Designer", 135, 9124 },
                    { 16, "Dynamic Response Technician", 753, 8854 },
                    { 17, "Human Markets Coordinator", 871, 4774 },
                    { 18, "Regional Division Analyst", 597, 3406 },
                    { 19, "Future Optimization Associate", 355, 7728 },
                    { 20, "District Program Planner", 985, 7334 },
                    { 21, "Dynamic Program Administrator", 820, 1095 },
                    { 22, "Chief Configuration Orchestrator", 576, 4885 },
                    { 23, "Principal Branding Assistant", 205, 5468 },
                    { 24, "Human Response Technician", 13, 3927 },
                    { 25, "Global Marketing Planner", 274, 2482 },
                    { 26, "Senior Accounts Designer", 339, 8864 },
                    { 27, "Direct Program Architect", 423, 7437 },
                    { 28, "Dynamic Optimization Officer", 671, 8529 },
                    { 29, "Global Marketing Analyst", 83, 8873 },
                    { 30, "Investor Branding Director", 854, 7670 },
                    { 31, "Corporate Division Analyst", 243, 7980 },
                    { 32, "Future Creative Analyst", 697, 6341 },
                    { 33, "Senior Research Facilitator", 713, 4838 },
                    { 34, "National Mobility Planner", 520, 1268 },
                    { 35, "Human Usability Supervisor", 782, 9926 },
                    { 36, "Internal Configuration Assistant", 498, 9412 },
                    { 37, "Legacy Intranet Developer", 858, 2152 },
                    { 38, "Corporate Solutions Coordinator", 671, 7606 },
                    { 39, "Corporate Accountability Planner", 74, 7051 },
                    { 40, "Dynamic Tactics Designer", 506, 2694 },
                    { 41, "Chief Directives Manager", 239, 3048 },
                    { 42, "Central Marketing Technician", 131, 8897 },
                    { 43, "Internal Accounts Manager", 273, 7333 },
                    { 44, "International Identity Supervisor", 673, 3398 },
                    { 45, "Chief Intranet Strategist", 104, 1731 },
                    { 46, "Chief Accounts Associate", 850, 8513 },
                    { 47, "Internal Optimization Planner", 43, 4693 },
                    { 48, "Central Marketing Assistant", 212, 9570 },
                    { 49, "Principal Intranet Facilitator", 525, 604 },
                    { 50, "Principal Brand Designer", 728, 4636 },
                    { 51, "Dynamic Intranet Agent", 740, 2418 },
                    { 52, "Chief Identity Supervisor", 267, 2740 },
                    { 53, "District Group Representative", 686, 1598 },
                    { 54, "Internal Factors Facilitator", 525, 5015 },
                    { 55, "Forward Applications Engineer", 320, 2589 },
                    { 56, "Corporate Intranet Designer", 536, 9401 },
                    { 57, "Principal Mobility Administrator", 370, 6072 },
                    { 58, "Principal Optimization Architect", 134, 1639 },
                    { 59, "Customer Division Manager", 967, 7293 },
                    { 60, "Direct Identity Representative", 801, 1856 },
                    { 61, "District Accounts Specialist", 971, 2306 },
                    { 62, "National Usability Manager", 503, 2082 },
                    { 63, "Dynamic Factors Supervisor", 178, 9037 },
                    { 64, "Central Interactions Supervisor", 707, 8307 },
                    { 65, "Forward Research Strategist", 657, 176 },
                    { 66, "Human Configuration Specialist", 619, 8889 },
                    { 67, "Corporate Division Planner", 335, 2374 },
                    { 68, "District Creative Technician", 20, 5507 },
                    { 69, "Internal Operations Representative", 736, 9127 },
                    { 70, "Investor Paradigm Consultant", 926, 909 },
                    { 71, "District Security Engineer", 403, 9711 },
                    { 72, "Dynamic Web Associate", 379, 6409 },
                    { 73, "Corporate Division Liaison", 776, 16 },
                    { 74, "International Metrics Officer", 215, 4478 },
                    { 75, "National Mobility Liaison", 660, 5950 },
                    { 76, "Regional Factors Supervisor", 525, 692 },
                    { 77, "Human Web Analyst", 71, 4872 },
                    { 78, "Investor Identity Consultant", 821, 8647 },
                    { 79, "Principal Markets Designer", 496, 7946 },
                    { 80, "Regional Creative Facilitator", 501, 8436 },
                    { 81, "Forward Quality Producer", 720, 4494 },
                    { 82, "Dynamic Paradigm Consultant", 428, 301 },
                    { 83, "Global Quality Analyst", 131, 8425 },
                    { 84, "Global Usability Representative", 733, 2219 },
                    { 85, "National Infrastructure Representative", 823, 892 },
                    { 86, "Future Identity Specialist", 287, 8515 },
                    { 87, "District Mobility Administrator", 616, 3948 },
                    { 88, "Central Directives Designer", 197, 4125 },
                    { 89, "Future Division Strategist", 900, 1105 },
                    { 90, "Legacy Infrastructure Officer", 58, 9491 },
                    { 91, "National Assurance Representative", 768, 8805 },
                    { 92, "Chief Tactics Analyst", 575, 2273 },
                    { 93, "Forward Configuration Engineer", 496, 8004 },
                    { 94, "Dynamic Marketing Representative", 358, 6138 },
                    { 95, "Future Intranet Designer", 364, 7711 },
                    { 96, "Dynamic Response Technician", 394, 6855 },
                    { 97, "Human Markets Coordinator", 337, 6477 },
                    { 98, "Regional Division Analyst", 588, 349 },
                    { 99, "Future Optimization Associate", 308, 1669 },
                    { 100, "District Program Planner", 637, 4126 },
                    { 101, "Dynamic Program Administrator", 272, 6524 },
                    { 102, "Chief Configuration Orchestrator", 361, 5318 },
                    { 103, "Principal Branding Assistant", 577, 9756 },
                    { 104, "Human Response Technician", 217, 985 },
                    { 105, "Global Marketing Planner", 269, 357 },
                    { 106, "Senior Accounts Designer", 35, 1040 },
                    { 107, "Direct Program Architect", 14, 5352 },
                    { 108, "Dynamic Optimization Officer", 947, 24 },
                    { 109, "Global Marketing Analyst", 846, 8997 },
                    { 110, "Investor Branding Director", 128, 1605 },
                    { 111, "Corporate Division Analyst", 491, 8291 },
                    { 112, "Future Creative Analyst", 578, 7171 },
                    { 113, "Senior Research Facilitator", 419, 8420 },
                    { 114, "National Mobility Planner", 315, 4222 },
                    { 115, "Human Usability Supervisor", 184, 4295 },
                    { 116, "Internal Configuration Assistant", 901, 3017 },
                    { 117, "Legacy Intranet Developer", 410, 3924 },
                    { 118, "Corporate Solutions Coordinator", 575, 1946 },
                    { 119, "Corporate Accountability Planner", 145, 1202 },
                    { 120, "Dynamic Tactics Designer", 93, 5611 },
                    { 121, "Chief Directives Manager", 235, 6180 },
                    { 122, "Central Marketing Technician", 634, 5017 },
                    { 123, "Internal Accounts Manager", 848, 6500 },
                    { 124, "International Identity Supervisor", 171, 4180 },
                    { 125, "Chief Intranet Strategist", 908, 5940 },
                    { 126, "Chief Accounts Associate", 906, 8935 },
                    { 127, "Internal Optimization Planner", 784, 4737 },
                    { 128, "Central Marketing Assistant", 501, 5018 },
                    { 129, "Principal Intranet Facilitator", 301, 8252 },
                    { 130, "Principal Brand Designer", 25, 9377 },
                    { 131, "Dynamic Intranet Agent", 569, 2457 },
                    { 132, "Chief Identity Supervisor", 388, 143 },
                    { 133, "District Group Representative", 30, 5666 },
                    { 134, "Internal Factors Facilitator", 980, 2974 },
                    { 135, "Forward Applications Engineer", 574, 7962 },
                    { 136, "Corporate Intranet Designer", 476, 9733 },
                    { 137, "Principal Mobility Administrator", 715, 2577 },
                    { 138, "Principal Optimization Architect", 42, 8677 },
                    { 139, "Customer Division Manager", 497, 4724 },
                    { 140, "Direct Identity Representative", 392, 3190 },
                    { 141, "District Accounts Specialist", 826, 4181 },
                    { 142, "National Usability Manager", 895, 4328 },
                    { 143, "Dynamic Factors Supervisor", 818, 8403 },
                    { 144, "Central Interactions Supervisor", 194, 8723 },
                    { 145, "Forward Research Strategist", 845, 5863 },
                    { 146, "Human Configuration Specialist", 936, 1522 },
                    { 147, "Corporate Division Planner", 933, 7021 },
                    { 148, "District Creative Technician", 24, 7323 },
                    { 149, "Internal Operations Representative", 522, 9524 },
                    { 150, "Investor Paradigm Consultant", 636, 9969 },
                    { 151, "District Security Engineer", 603, 1294 },
                    { 152, "Dynamic Web Associate", 236, 7949 },
                    { 153, "Corporate Division Liaison", 334, 2168 },
                    { 154, "International Metrics Officer", 270, 6285 },
                    { 155, "National Mobility Liaison", 810, 5106 },
                    { 156, "Regional Factors Supervisor", 265, 312 },
                    { 157, "Human Web Analyst", 40, 3368 },
                    { 158, "Investor Identity Consultant", 494, 4580 },
                    { 159, "Principal Markets Designer", 225, 9892 },
                    { 160, "Regional Creative Facilitator", 432, 1755 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ClientId", "Discriminator", "IsCompleted" },
                values: new object[,]
                {
                    { 1, 1, "Order", false },
                    { 2, 2, "Order", false },
                    { 3, 3, "Order", false },
                    { 4, 4, "Order", false },
                    { 5, 5, "Order", false },
                    { 6, 6, "Order", false },
                    { 7, 7, "Order", false },
                    { 8, 8, "Order", false },
                    { 9, 9, "Order", false },
                    { 10, 10, "Order", false }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ClientId", "Discriminator", "IpAddress", "IsCompleted" },
                values: new object[,]
                {
                    { 11, 11, "EOrder", "11.107.243.243", false },
                    { 12, 12, "EOrder", "163.105.180.202", false },
                    { 13, 13, "EOrder", "165.74.181.29", false },
                    { 14, 14, "EOrder", "125.205.89.167", false },
                    { 15, 15, "EOrder", "78.14.212.127", false },
                    { 16, 16, "EOrder", "75.11.225.183", false },
                    { 17, 17, "EOrder", "178.64.232.224", false },
                    { 18, 18, "EOrder", "9.112.19.137", false },
                    { 19, 19, "EOrder", "57.192.239.216", false },
                    { 20, 20, "EOrder", "2.159.220.183", false }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ClientId", "Discriminator", "IsCompleted" },
                values: new object[,]
                {
                    { 21, 21, "Order", false },
                    { 22, 22, "Order", false },
                    { 23, 23, "Order", false },
                    { 24, 24, "Order", false },
                    { 25, 25, "Order", false },
                    { 26, 26, "Order", false },
                    { 27, 27, "Order", false },
                    { 28, 28, "Order", false },
                    { 29, 29, "Order", false },
                    { 30, 30, "Order", false }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ClientId", "Discriminator", "IpAddress", "IsCompleted" },
                values: new object[,]
                {
                    { 31, 31, "EOrder", "11.107.243.243", false },
                    { 32, 32, "EOrder", "163.105.180.202", false },
                    { 33, 33, "EOrder", "165.74.181.29", false },
                    { 34, 34, "EOrder", "125.205.89.167", false },
                    { 35, 35, "EOrder", "78.14.212.127", false },
                    { 36, 36, "EOrder", "75.11.225.183", false },
                    { 37, 37, "EOrder", "178.64.232.224", false },
                    { 38, 38, "EOrder", "9.112.19.137", false },
                    { 39, 39, "EOrder", "57.192.239.216", false },
                    { 40, 40, "EOrder", "2.159.220.183", false }
                });

            migrationBuilder.InsertData(
                table: "OrderEntries",
                columns: new[] { "Id", "Amount", "ItemId", "OrderId" },
                values: new object[,]
                {
                    { 1, 146, 1, 1 },
                    { 2, 888, 2, 1 },
                    { 3, 944, 3, 1 },
                    { 4, 34, 4, 2 },
                    { 5, 248, 5, 2 },
                    { 6, 193, 6, 2 },
                    { 7, 733, 7, 2 },
                    { 8, 71, 8, 3 },
                    { 9, 814, 9, 3 },
                    { 10, 202, 10, 3 },
                    { 11, 489, 11, 3 },
                    { 12, 87, 12, 4 },
                    { 13, 109, 13, 4 },
                    { 14, 932, 14, 4 },
                    { 15, 972, 15, 4 },
                    { 16, 160, 16, 4 },
                    { 17, 361, 17, 5 },
                    { 18, 428, 18, 5 },
                    { 19, 243, 19, 5 },
                    { 20, 68, 20, 5 },
                    { 21, 348, 21, 6 },
                    { 22, 965, 22, 6 },
                    { 23, 355, 23, 6 },
                    { 24, 827, 24, 6 },
                    { 25, 993, 25, 7 },
                    { 26, 76, 26, 7 },
                    { 27, 829, 27, 7 },
                    { 28, 845, 28, 7 },
                    { 29, 265, 29, 8 },
                    { 30, 252, 30, 8 },
                    { 31, 830, 31, 8 },
                    { 32, 627, 32, 8 },
                    { 33, 355, 33, 8 },
                    { 34, 512, 34, 9 },
                    { 35, 313, 35, 9 },
                    { 36, 865, 36, 9 },
                    { 37, 388, 37, 10 },
                    { 38, 320, 38, 10 },
                    { 39, 804, 39, 10 },
                    { 40, 29, 40, 10 },
                    { 41, 403, 41, 10 },
                    { 42, 387, 42, 11 },
                    { 43, 218, 43, 11 },
                    { 44, 962, 44, 11 },
                    { 45, 637, 45, 12 },
                    { 46, 585, 46, 12 },
                    { 47, 334, 47, 12 },
                    { 48, 617, 48, 13 },
                    { 49, 620, 49, 13 },
                    { 50, 615, 50, 13 },
                    { 51, 421, 51, 13 },
                    { 52, 730, 52, 14 },
                    { 53, 972, 53, 14 },
                    { 54, 712, 54, 14 },
                    { 55, 828, 55, 15 },
                    { 56, 274, 56, 15 },
                    { 57, 485, 57, 15 },
                    { 58, 533, 58, 15 },
                    { 59, 445, 59, 16 },
                    { 60, 697, 60, 16 },
                    { 61, 835, 61, 16 },
                    { 62, 525, 62, 16 },
                    { 63, 5, 63, 17 },
                    { 64, 796, 64, 17 },
                    { 65, 967, 65, 17 },
                    { 66, 878, 66, 17 },
                    { 67, 295, 67, 17 },
                    { 68, 366, 68, 18 },
                    { 69, 440, 69, 18 },
                    { 70, 972, 70, 18 },
                    { 71, 62, 71, 18 },
                    { 72, 707, 72, 19 },
                    { 73, 808, 73, 19 },
                    { 74, 949, 74, 19 },
                    { 75, 121, 75, 19 },
                    { 76, 777, 76, 20 },
                    { 77, 725, 77, 20 },
                    { 78, 198, 78, 20 },
                    { 79, 157, 79, 20 },
                    { 80, 379, 80, 20 },
                    { 81, 606, 81, 21 },
                    { 82, 508, 82, 21 },
                    { 83, 909, 83, 21 },
                    { 84, 691, 84, 22 },
                    { 85, 845, 85, 22 },
                    { 86, 85, 86, 22 },
                    { 87, 504, 87, 22 },
                    { 88, 750, 88, 23 },
                    { 89, 425, 89, 23 },
                    { 90, 369, 90, 23 },
                    { 91, 527, 91, 23 },
                    { 92, 606, 92, 24 },
                    { 93, 187, 93, 24 },
                    { 94, 0, 94, 24 },
                    { 95, 707, 95, 24 },
                    { 96, 794, 96, 24 },
                    { 97, 744, 97, 25 },
                    { 98, 340, 98, 25 },
                    { 99, 708, 99, 25 },
                    { 100, 23, 100, 25 },
                    { 101, 486, 101, 26 },
                    { 102, 678, 102, 26 },
                    { 103, 467, 103, 26 },
                    { 104, 637, 104, 26 },
                    { 105, 300, 105, 27 },
                    { 106, 442, 106, 27 },
                    { 107, 355, 107, 27 },
                    { 108, 667, 108, 27 },
                    { 109, 73, 109, 28 },
                    { 110, 269, 110, 28 },
                    { 111, 567, 111, 28 },
                    { 112, 817, 112, 28 },
                    { 113, 759, 113, 28 },
                    { 114, 337, 114, 29 },
                    { 115, 758, 115, 29 },
                    { 116, 447, 116, 29 },
                    { 117, 291, 117, 30 },
                    { 118, 46, 118, 30 },
                    { 119, 688, 119, 30 },
                    { 120, 167, 120, 30 },
                    { 121, 215, 121, 30 },
                    { 122, 202, 122, 31 },
                    { 123, 945, 123, 31 },
                    { 124, 408, 124, 31 },
                    { 125, 458, 125, 32 },
                    { 126, 9, 126, 32 },
                    { 127, 601, 127, 32 },
                    { 128, 967, 128, 33 },
                    { 129, 886, 129, 33 },
                    { 130, 398, 130, 33 },
                    { 131, 919, 131, 33 },
                    { 132, 263, 132, 34 },
                    { 133, 615, 133, 34 },
                    { 134, 368, 134, 34 },
                    { 135, 610, 135, 35 },
                    { 136, 384, 136, 35 },
                    { 137, 472, 137, 35 },
                    { 138, 234, 138, 35 },
                    { 139, 624, 139, 36 },
                    { 140, 338, 140, 36 },
                    { 141, 934, 141, 36 },
                    { 142, 49, 142, 36 },
                    { 143, 141, 143, 37 },
                    { 144, 104, 144, 37 },
                    { 145, 457, 145, 37 },
                    { 146, 817, 146, 37 },
                    { 147, 550, 147, 37 },
                    { 148, 119, 148, 38 },
                    { 149, 862, 149, 38 },
                    { 150, 718, 150, 38 },
                    { 151, 388, 151, 38 },
                    { 152, 796, 152, 39 },
                    { 153, 513, 153, 39 },
                    { 154, 839, 154, 39 },
                    { 155, 156, 155, 39 },
                    { 156, 783, 156, 40 },
                    { 157, 584, 157, 40 },
                    { 158, 137, 158, 40 },
                    { 159, 403, 159, 40 },
                    { 160, 727, 160, 40 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntries_ItemId",
                table: "OrderEntries",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntries_OrderId",
                table: "OrderEntries",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderEntries");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
