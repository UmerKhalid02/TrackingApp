﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrackingApp.Data;

#nullable disable

namespace TrackingApp.Data.Migrations
{
    [DbContext(typeof(EFDataContext))]
    [Migration("20230821200748_AddedEmailInAdminSeeder")]
    partial class AddedEmailInAdminSeeder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("trk")
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TrackingApp.Data.Entities.AuthenticationEntity.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("RoleId");

                    b.ToTable("Role", "trk");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("35dc76b5-8de7-4eb3-a29c-9a05686a6f89"),
                            CreatedAt = new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Admin",
                            IsActive = true,
                            RoleName = "AD"
                        },
                        new
                        {
                            RoleId = new Guid("2bd8f739-13c4-46e2-b0cc-5888851f373a"),
                            CreatedAt = new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "User",
                            IsActive = true,
                            RoleName = "US"
                        });
                });

            modelBuilder.Entity("TrackingApp.Data.Entities.UserEntity.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserID");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User", "trk");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252"),
                            ContactNo = "00000000000",
                            CreatedAt = new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@admin.com",
                            IsActive = true,
                            Password = "$2a$12$SHURSR0Suafcx5bKkUePQO7ka7IQ3wfBQkrH.xtnrRY8mnu9bgMb6",
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("TrackingApp.Data.Entities.UserEntity.UserRole", b =>
                {
                    b.Property<Guid>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserRole", "trk");

                    b.HasData(
                        new
                        {
                            UserRoleId = new Guid("70cdb88c-ca74-48e0-a597-162479301c9e"),
                            CreatedAt = new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            RoleId = new Guid("35dc76b5-8de7-4eb3-a29c-9a05686a6f89"),
                            UserId = new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252")
                        });
                });

            modelBuilder.Entity("TrackingApp.Data.Entities.UserEntity.UserRole", b =>
                {
                    b.HasOne("TrackingApp.Data.Entities.AuthenticationEntity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrackingApp.Data.Entities.UserEntity.User", "User")
                        .WithOne("UserRole")
                        .HasForeignKey("TrackingApp.Data.Entities.UserEntity.UserRole", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TrackingApp.Data.Entities.UserEntity.User", b =>
                {
                    b.Navigation("UserRole")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
