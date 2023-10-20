﻿// <auto-generated />
using System;
using DuHoc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DuHoc.Migrations
{
    [DbContext(typeof(DuHocContext))]
    partial class DuHocContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DuHoc.Models.Appointment", b =>
                {
                    b.Property<int>("Appointment_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Appointment_Id"));

                    b.Property<DateTime>("Appointment_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Create_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Decription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("Appointment_Id");

                    b.HasIndex("user_id");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("DuHoc.Models.ChildComment", b =>
                {
                    b.Property<int>("Comment_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Comment_Id"));

                    b.Property<DateTime>("Comment_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ParentComment_Id")
                        .HasColumnType("int");

                    b.Property<int?>("ParentComment_Id1")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("Comment_Id");

                    b.HasIndex("ParentComment_Id1");

                    b.HasIndex("user_id");

                    b.ToTable("ChildComment");
                });

            modelBuilder.Entity("DuHoc.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Continent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Introduce")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("DuHoc.Models.Course", b =>
                {
                    b.Property<int>("Course_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Course_Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DayPosted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Scholarship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("University_Id")
                        .HasColumnType("int");

                    b.HasKey("Course_Id");

                    b.HasIndex("University_Id");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("DuHoc.Models.NewsPost", b =>
                {
                    b.Property<int>("News_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("News_Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimePosted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPosted")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("News_Id");

                    b.ToTable("NewsPost");
                });

            modelBuilder.Entity("DuHoc.Models.ParentComment", b =>
                {
                    b.Property<int>("ParentComment_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParentComment_Id"));

                    b.Property<DateTime>("Comment_Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NewsPostNews_Id")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("ParentComment_Id");

                    b.HasIndex("NewsPostNews_Id");

                    b.HasIndex("user_id");

                    b.ToTable("ParentComment");
                });

            modelBuilder.Entity("DuHoc.Models.Profile", b =>
                {
                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone_number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("DuHoc.Models.University", b =>
                {
                    b.Property<int>("University_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("University_Id"));

                    b.Property<string>("Decription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Images")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TuitionFee")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("University_Id");

                    b.HasIndex("Id");

                    b.ToTable("University");
                });

            modelBuilder.Entity("DuHoc.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_id"));

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DuHoc.Models.Appointment", b =>
                {
                    b.HasOne("DuHoc.Models.User", "User")
                        .WithMany("Appointment")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DuHoc.Models.ChildComment", b =>
                {
                    b.HasOne("DuHoc.Models.ParentComment", "ParentComment")
                        .WithMany()
                        .HasForeignKey("ParentComment_Id1");

                    b.HasOne("DuHoc.Models.User", "User")
                        .WithMany("ChildComments")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ParentComment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DuHoc.Models.Course", b =>
                {
                    b.HasOne("DuHoc.Models.University", "University")
                        .WithMany("Course")
                        .HasForeignKey("University_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("University");
                });

            modelBuilder.Entity("DuHoc.Models.ParentComment", b =>
                {
                    b.HasOne("DuHoc.Models.NewsPost", null)
                        .WithMany("Comments")
                        .HasForeignKey("NewsPostNews_Id");

                    b.HasOne("DuHoc.Models.User", "User")
                        .WithMany("ParentComments")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DuHoc.Models.Profile", b =>
                {
                    b.HasOne("DuHoc.Models.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("DuHoc.Models.Profile", "user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DuHoc.Models.University", b =>
                {
                    b.HasOne("DuHoc.Models.Country", "Country")
                        .WithMany("University")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("DuHoc.Models.Country", b =>
                {
                    b.Navigation("University");
                });

            modelBuilder.Entity("DuHoc.Models.NewsPost", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("DuHoc.Models.University", b =>
                {
                    b.Navigation("Course");
                });

            modelBuilder.Entity("DuHoc.Models.User", b =>
                {
                    b.Navigation("Appointment");

                    b.Navigation("ChildComments");

                    b.Navigation("ParentComments");

                    b.Navigation("Profile");
                });
#pragma warning restore 612, 618
        }
    }
}
