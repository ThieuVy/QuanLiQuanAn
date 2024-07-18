﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLiQuanAn.Data;

#nullable disable

namespace QuanLiQuanAn.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240716033714_AddHoaDon")]
    partial class AddHoaDon
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuanLiQuanAn.Models.CCTHD", b =>
                {
                    b.Property<int>("SoHD")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("MaMA")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(1);

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("SoHD", "MaMA");

                    b.HasIndex("MaMA");

                    b.ToTable("CCTHDs");
                });

            modelBuilder.Entity("QuanLiQuanAn.Models.CDatBan", b =>
                {
                    b.Property<string>("TenTK")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Gio")
                        .HasColumnType("time");

                    b.Property<DateTime>("Ngay")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<int>("ViTriBan")
                        .HasColumnType("int");

                    b.HasKey("TenTK");

                    b.ToTable("DatBans");
                });

            modelBuilder.Entity("QuanLiQuanAn.Models.CDatMonAn", b =>
                {
                    b.Property<string>("MaMA")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Gia")
                        .HasColumnType("float");

                    b.Property<string>("TenMA")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Url_anh")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("MaMA");

                    b.ToTable("DatMonAns");
                });

            modelBuilder.Entity("QuanLiQuanAn.Models.CHoaDon", b =>
                {
                    b.Property<int>("SoHD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoHD"));

                    b.Property<string>("MaMA")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("NgHD")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenTK")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SoHD");

                    b.HasIndex("MaMA");

                    b.HasIndex("TenTK");

                    b.ToTable("HoaDons");
                });

            modelBuilder.Entity("QuanLiQuanAn.Models.CTaiKhoan", b =>
                {
                    b.Property<string>("TenTK")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("DiemTL")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HangTV")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoDT")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("TenTK");

                    b.ToTable("TaiKhoans");
                });

            modelBuilder.Entity("QuanLiQuanAn.Models.CCTHD", b =>
                {
                    b.HasOne("QuanLiQuanAn.Models.CDatMonAn", "MonAn")
                        .WithMany("CCTHDs")
                        .HasForeignKey("MaMA")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLiQuanAn.Models.CHoaDon", "HoaDon")
                        .WithMany("CCTHDs")
                        .HasForeignKey("SoHD")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");

                    b.Navigation("MonAn");
                });

            modelBuilder.Entity("QuanLiQuanAn.Models.CDatBan", b =>
                {
                    b.HasOne("QuanLiQuanAn.Models.CTaiKhoan", "TaiKhoan")
                        .WithMany("DatBans")
                        .HasForeignKey("TenTK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("QuanLiQuanAn.Models.CHoaDon", b =>
                {
                    b.HasOne("QuanLiQuanAn.Models.CDatMonAn", "MonAn")
                        .WithMany("HoaDons")
                        .HasForeignKey("MaMA")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLiQuanAn.Models.CTaiKhoan", "TaiKhoan")
                        .WithMany("HoaDons")
                        .HasForeignKey("TenTK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MonAn");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("QuanLiQuanAn.Models.CDatMonAn", b =>
                {
                    b.Navigation("CCTHDs");

                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("QuanLiQuanAn.Models.CHoaDon", b =>
                {
                    b.Navigation("CCTHDs");
                });

            modelBuilder.Entity("QuanLiQuanAn.Models.CTaiKhoan", b =>
                {
                    b.Navigation("DatBans");

                    b.Navigation("HoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}
