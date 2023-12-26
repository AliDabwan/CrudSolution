﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(CRUDDbContext))]
    partial class CRUDDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = new Guid("000c76eb-62e9-4465-96d1-2c41fdb64c3b"),
                            Name = "Yemen"
                        },
                        new
                        {
                            Id = new Guid("32da506b-3eba-48a4-bd86-5f93a2e19e3f"),
                            Name = "Palestine"
                        },
                        new
                        {
                            Id = new Guid("df7c89ce-3341-4246-84ae-e01ab7ba476e"),
                            Name = "Iraq"
                        },
                        new
                        {
                            Id = new Guid("15889048-af93-412c-b8f3-22103e943a6d"),
                            Name = "Syria"
                        },
                        new
                        {
                            Id = new Guid("80df255c-efe7-49e5-a7f9-c35d7c701cab"),
                            Name = "Libya"
                        });
                });

            modelBuilder.Entity("Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Gender")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("PN")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchr(9)")
                        .HasDefaultValue("None")
                        .HasColumnName("PassportNumber");

                    b.Property<bool>("ReceiveEmails")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8082ed0c-396d-4162-ad1d-29a13f929824"),
                            CountryId = new Guid("000c76eb-62e9-4465-96d1-2c41fdb64c3b"),
                            DateOfBirth = new DateTime(1981, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mo@email.com",
                            Gender = "Male",
                            Name = "Muhammad Awadallah",
                            ReceiveEmails = true
                        },
                        new
                        {
                            Id = new Guid("06d15bad-52f4-498e-b478-acad847abfaa"),
                            CountryId = new Guid("32da506b-3eba-48a4-bd86-5f93a2e19e3f"),
                            DateOfBirth = new DateTime(1991, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "amany@email.com",
                            Gender = "Female",
                            Name = "Amany Muhammad",
                            ReceiveEmails = true
                        },
                        new
                        {
                            Id = new Guid("d3ea677a-0f5b-41ea-8fef-ea2fc41900fd"),
                            CountryId = new Guid("32da506b-3eba-48a4-bd86-5f93a2e19e3f"),
                            DateOfBirth = new DateTime(1993, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "kha@email.com",
                            Gender = "Male",
                            Name = "Khaled Jaber",
                            ReceiveEmails = false
                        },
                        new
                        {
                            Id = new Guid("89452edb-bf8c-4283-9ba4-8259fd4a7a76"),
                            CountryId = new Guid("df7c89ce-3341-4246-84ae-e01ab7ba476e"),
                            DateOfBirth = new DateTime(1985, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "galal@email.com",
                            Gender = "Male",
                            Name = "Galal Ali",
                            ReceiveEmails = true
                        },
                        new
                        {
                            Id = new Guid("f5bd5979-1dc1-432c-b1f1-db5bccb0e56d"),
                            CountryId = new Guid("df7c89ce-3341-4246-84ae-e01ab7ba476e"),
                            DateOfBirth = new DateTime(1996, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "fatima@email.com",
                            Gender = "Female",
                            Name = "Fatima Ahmad",
                            ReceiveEmails = false
                        },
                        new
                        {
                            Id = new Guid("a795e22d-faed-42f0-b134-f3b89b8683e5"),
                            CountryId = new Guid("15889048-af93-412c-b8f3-22103e943a6d"),
                            DateOfBirth = new DateTime(1993, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "batool@email.com",
                            Gender = "Female",
                            Name = "Batool Ali",
                            ReceiveEmails = false
                        },
                        new
                        {
                            Id = new Guid("3c12d8e8-3c1c-4f57-b6a4-c8caac893d7a"),
                            CountryId = new Guid("80df255c-efe7-49e5-a7f9-c35d7c701cab"),
                            DateOfBirth = new DateTime(1996, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "amir@email.com",
                            Gender = "Male",
                            Name = "Amir Muhammad",
                            ReceiveEmails = true
                        },
                        new
                        {
                            Id = new Guid("7b75097b-bff2-459f-8ea8-63742bbd7afb"),
                            CountryId = new Guid("000c76eb-62e9-4465-96d1-2c41fdb64c3b"),
                            DateOfBirth = new DateTime(1982, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "basel@email.com",
                            Gender = "Male",
                            Name = "Basel Mahmoud",
                            ReceiveEmails = false
                        },
                        new
                        {
                            Id = new Guid("6717c42d-16ec-4f15-80d8-4c7413e250cb"),
                            CountryId = new Guid("80df255c-efe7-49e5-a7f9-c35d7c701cab"),
                            DateOfBirth = new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "saad@email.com",
                            Gender = "Male",
                            Name = "Saad Muhammad",
                            ReceiveEmails = false
                        },
                        new
                        {
                            Id = new Guid("6e789c86-c8a6-4f18-821c-2abdb2e95982"),
                            CountryId = new Guid("000c76eb-62e9-4465-96d1-2c41fdb64c3b"),
                            DateOfBirth = new DateTime(1996, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "fareed@email.com",
                            Gender = "Male",
                            Name = "Fareed Said",
                            ReceiveEmails = false
                        });
                });

            modelBuilder.Entity("Entities.Person", b =>
                {
                    b.HasOne("Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });
#pragma warning restore 612, 618
        }
    }
}
