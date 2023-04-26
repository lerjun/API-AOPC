﻿// <auto-generated />
using System;
using AuthSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230131114458_audittrailstatus")]
    partial class audittrailstatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AuthSystem.Models.AuditTrailModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Actions")
                        .HasColumnType("Varchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<string>("Module")
                        .HasColumnType("Varchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tbl_AuditTrailModel");
                });

            modelBuilder.Entity("AuthSystem.Models.BusinessLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("Varchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("Varchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime");

                    b.Property<string>("PostalCode")
                        .HasColumnType("Varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tbl_BusinessLocationModel");
                });

            modelBuilder.Entity("AuthSystem.Models.BusinessModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Cno")
                        .HasColumnType("varchar(MAX)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Gallery")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("varchar(MAX)");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Services")
                        .HasColumnType("varchar(MAX)");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("varchar(MAX)");

                    b.HasKey("Id");

                    b.ToTable("tbl_BusinessModel");
                });

            modelBuilder.Entity("AuthSystem.Models.BusinessTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BusinessTypeName")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Location")
                        .HasColumnType("varchar(MAX)");

                    b.HasKey("Id");

                    b.ToTable("tbl_BusinessTypeModel");
                });

            modelBuilder.Entity("AuthSystem.Models.CorporateModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("CNo")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("CorporateName")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tbl_CorporateModel");
                });

            modelBuilder.Entity("AuthSystem.Models.MembershipModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateEnded")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateUsed")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tbl_MembershipModel");
                });

            modelBuilder.Entity("AuthSystem.Models.PositionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(MAX)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tbl_PositionModel");
                });

            modelBuilder.Entity("AuthSystem.Models.PrivilegeLogsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BusinessId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateRedeemed")
                        .HasColumnType("datetime");

                    b.Property<int?>("PrivilegeId")
                        .HasColumnType("int");

                    b.Property<int?>("PrivilegeOwnerId")
                        .HasColumnType("int");

                    b.Property<string>("RedeemedBy")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<int?>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tbl_PrivilegeLogsModel");
                });

            modelBuilder.Entity("AuthSystem.Models.PrivilegesModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CorporateID")
                        .HasColumnType("int");

                    b.Property<int?>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateExpired")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateIssued")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<int?>("MembershipID")
                        .HasColumnType("int");

                    b.Property<int?>("Size")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tbl_PrivilegesModel");
                });

            modelBuilder.Entity("AuthSystem.Models.StatusModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("tbl_StatusModel");
                });

            modelBuilder.Entity("AuthSystem.Models.UsersModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Active")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Cno")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("CorporateID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EmployeeID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FilePath")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Fname")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Fullname")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Gender")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("JWToken")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Lname")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Mname")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(max)");

                    b.Property<int?>("PositionID")
                        .HasColumnType("int");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("isVIP")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UsersModel");
                });

            modelBuilder.Entity("AuthSystem.Models.VendorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("Cno")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("FeatureImg")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Gallery")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Services")
                        .HasColumnType("varchar(MAX)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<string>("VendorName")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("VrUrl")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("WebsiteUrl")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("location")
                        .HasColumnType("varchar(MAX)");

                    b.HasKey("Id");

                    b.ToTable("tbl_VendorModel");
                });
#pragma warning restore 612, 618
        }
    }
}
