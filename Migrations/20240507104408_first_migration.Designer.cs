﻿// <auto-generated />
using System;
using JWTAuthenticationWithMiddlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JWTAuthenticationWithMiddlewares.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240507104408_first_migration")]
    partial class first_migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("JWTAuthenticationWithMiddlewares.Models.LoginDetailModel", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("user_id")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("user_id")
                        .IsUnique();

                    b.ToTable("Login_Detail");
                });

            modelBuilder.Entity("JWTAuthenticationWithMiddlewares.Models.StoryModel", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("user_id")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.ToTable("Story");
                });

            modelBuilder.Entity("JWTAuthenticationWithMiddlewares.Models.UserModel", b =>
                {
                    b.Property<long>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("user_id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("JWTAuthenticationWithMiddlewares.Models.LoginDetailModel", b =>
                {
                    b.HasOne("JWTAuthenticationWithMiddlewares.Models.UserModel", "User")
                        .WithOne("Login_Detail")
                        .HasForeignKey("JWTAuthenticationWithMiddlewares.Models.LoginDetailModel", "user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("JWTAuthenticationWithMiddlewares.Models.StoryModel", b =>
                {
                    b.HasOne("JWTAuthenticationWithMiddlewares.Models.UserModel", "User")
                        .WithMany("Stories")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("JWTAuthenticationWithMiddlewares.Models.UserModel", b =>
                {
                    b.Navigation("Login_Detail")
                        .IsRequired();

                    b.Navigation("Stories");
                });
#pragma warning restore 612, 618
        }
    }
}