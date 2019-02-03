﻿// <auto-generated />

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace StockManagement.Api.DAL.Migrations
{
    [DbContext(typeof(ApiContext))]
    internal class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("StockManagement.Api.Contract.Entities.BatchEntity", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("FruitId");

                b.Property<int>("Quantity");

                b.HasKey("Id");

                b.HasIndex("FruitId");

                b.ToTable("Batches");

                b.HasData(
                    new
                    {
                        Id = 1,
                        FruitId = 2,
                        Quantity = 12
                    },
                    new
                    {
                        Id = 2,
                        FruitId = 3,
                        Quantity = 10
                    },
                    new
                    {
                        Id = 3,
                        FruitId = 2,
                        Quantity = 10
                    },
                    new
                    {
                        Id = 4,
                        FruitId = 1,
                        Quantity = 15
                    });
            });

            modelBuilder.Entity("StockManagement.Api.Contract.Entities.FruitEntity", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Name");

                b.Property<int>("VarietyId");

                b.HasKey("Id");

                b.HasIndex("VarietyId");

                b.ToTable("Fruits");

                b.HasData(
                    new
                    {
                        Id = 3,
                        Name = "Raspberry",
                        VarietyId = 3
                    },
                    new
                    {
                        Id = 2,
                        Name = "Raspberry",
                        VarietyId = 1
                    },
                    new
                    {
                        Id = 1,
                        Name = "Blueberry",
                        VarietyId = 2
                    },
                    new
                    {
                        Id = 4,
                        Name = "Raspberry",
                        VarietyId = 4
                    });
            });

            modelBuilder.Entity("StockManagement.Api.Contract.Entities.UserEntity", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("FirstName");

                b.Property<string>("LastName");

                b.Property<byte[]>("PasswordHash");

                b.Property<byte[]>("PasswordSalt");

                b.Property<string>("Role");

                b.Property<string>("Username");

                b.HasKey("Id");

                b.ToTable("Users");

                b.HasData(
                    new
                    {
                        Id = 1,
                        FirstName = "First",
                        LastName = "First",
                        PasswordHash = new byte[]
                        {
                            73, 39, 54, 240, 62, 133, 6, 110, 215, 178, 142, 161, 206, 60, 30, 255, 63, 214, 151, 1, 126, 229, 78, 121, 239, 155, 103, 245, 104, 38, 50, 120, 146, 90, 43, 168, 250,
                            238, 135, 96, 81, 235, 32, 242, 51, 82, 173, 60, 104, 223, 153, 219, 162, 206, 133, 8, 164, 100, 177, 232, 147, 104, 33, 3
                        },
                        PasswordSalt = new byte[]
                        {
                            23, 84, 56, 48, 114, 153, 49, 105, 119, 68, 98, 190, 40, 117, 12, 10, 54, 229, 109, 50, 144, 224, 24, 115, 91, 61, 85, 177, 149, 221, 60, 196, 127, 120, 215, 226, 130, 228,
                            75, 25, 199, 125, 168, 90, 93, 15, 178, 18, 21, 222, 48, 71, 142, 55, 179, 1, 164, 84, 214, 25, 204, 44, 59, 249, 119, 172, 228, 103, 172, 131, 236, 91, 183, 190, 204, 2,
                            80, 155, 91, 209, 63, 115, 92, 40, 71, 40, 187, 203, 83, 61, 67, 51, 41, 171, 118, 220, 213, 37, 72, 43, 4, 244, 73, 5, 198, 98, 203, 192, 165, 40, 2, 0, 49, 138, 246, 186,
                            49, 104, 77, 216, 202, 83, 172, 177, 162, 199, 62, 69
                        },
                        Role = "User",
                        Username = "FirstUser"
                    },
                    new
                    {
                        Id = 2,
                        FirstName = "Second",
                        LastName = "Second",
                        PasswordHash = new byte[]
                        {
                            50, 90, 184, 163, 83, 173, 183, 112, 229, 49, 188, 166, 196, 155, 44, 176, 19, 228, 242, 106, 191, 107, 189, 240, 195, 53, 111, 230, 169, 143, 160, 148, 83, 208, 118, 144,
                            218, 31, 88, 170, 156, 248, 99, 180, 131, 218, 139, 244, 185, 208, 148, 39, 64, 66, 143, 146, 92, 81, 53, 203, 123, 15, 167, 33
                        },
                        PasswordSalt = new byte[]
                        {
                            215, 63, 122, 139, 86, 89, 109, 162, 160, 20, 95, 65, 253, 76, 140, 185, 213, 192, 19, 151, 179, 29, 177, 183, 57, 92, 119, 135, 115, 64, 88, 114, 12, 94, 118, 184, 121,
                            181, 4, 8, 142, 152, 37, 192, 69, 43, 11, 14, 243, 122, 116, 193, 153, 160, 61, 15, 77, 73, 118, 2, 138, 40, 9, 95, 218, 75, 129, 89, 157, 234, 16, 248, 222, 177, 126, 98,
                            203, 119, 173, 162, 177, 136, 232, 196, 102, 67, 70, 221, 24, 122, 153, 156, 55, 116, 129, 6, 137, 151, 84, 243, 6, 236, 38, 117, 208, 74, 165, 13, 218, 123, 241, 12, 180,
                            2, 102, 221, 240, 227, 118, 145, 93, 5, 40, 244, 204, 56, 255, 20
                        },
                        Role = "User",
                        Username = "SecondUser"
                    },
                    new
                    {
                        Id = 3,
                        FirstName = "Second",
                        LastName = "Second",
                        PasswordHash = new byte[]
                        {
                            116, 32, 225, 238, 68, 47, 149, 58, 45, 216, 104, 15, 129, 131, 184, 193, 74, 209, 106, 185, 120, 77, 149, 172, 154, 161, 106, 240, 154, 100, 14, 161, 91, 195, 221, 81, 88,
                            11, 231, 244, 60, 247, 145, 7, 64, 192, 175, 119, 147, 182, 3, 194, 149, 6, 252, 192, 159, 172, 138, 119, 1, 207, 59, 68
                        },
                        PasswordSalt = new byte[]
                        {
                            150, 148, 119, 161, 20, 128, 98, 60, 226, 68, 234, 145, 145, 237, 106, 150, 11, 118, 46, 93, 195, 6, 9, 255, 249, 21, 194, 80, 232, 232, 9, 171, 70, 64, 6, 211, 15, 47, 35,
                            201, 36, 129, 163, 244, 46, 107, 116, 84, 52, 241, 58, 111, 94, 189, 152, 69, 33, 132, 240, 18, 33, 122, 208, 4, 228, 242, 76, 61, 72, 236, 42, 97, 197, 167, 169, 57, 145,
                            246, 71, 253, 58, 12, 155, 25, 6, 125, 104, 171, 197, 107, 63, 49, 84, 77, 28, 105, 200, 233, 226, 173, 65, 58, 176, 39, 168, 212, 105, 191, 171, 94, 221, 168, 36, 135,
                            179, 113, 192, 156, 42, 206, 63, 88, 113, 32, 181, 6, 42, 246
                        },
                        Role = "Admin",
                        Username = "admin"
                    });
            });

            modelBuilder.Entity("StockManagement.Api.Contract.Entities.VarietyEntity", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Name");

                b.HasKey("Id");

                b.ToTable("Varieties");

                b.HasData(
                    new
                    {
                        Id = 2,
                        Name = "Alba"
                    },
                    new
                    {
                        Id = 3,
                        Name = "Erika"
                    },
                    new
                    {
                        Id = 1,
                        Name = "Amira"
                    },
                    new
                    {
                        Id = 4,
                        Name = "Test"
                    });
            });

            modelBuilder.Entity("StockManagement.Api.Contract.Entities.BatchEntity", b =>
            {
                b.HasOne("StockManagement.Api.Contract.Entities.FruitEntity", "Fruit")
                    .WithMany("Batches")
                    .HasForeignKey("FruitId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("StockManagement.Api.Contract.Entities.FruitEntity", b =>
            {
                b.HasOne("StockManagement.Api.Contract.Entities.VarietyEntity", "Variety")
                    .WithMany("Fruits")
                    .HasForeignKey("VarietyId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}