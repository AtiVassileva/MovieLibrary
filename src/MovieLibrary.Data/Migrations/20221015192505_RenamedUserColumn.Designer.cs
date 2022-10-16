﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieLibrary.Data;

#nullable disable

namespace MovieLibrary.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221015192505_RenamedUserColumn")]
    partial class RenamedUserColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MovieLibrary.Models.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("MovieLibrary.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MovieLibrary.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("Overview")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PremiereDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("UserId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieLibrary.Models.MovieCharacter", b =>
                {
                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MovieId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("MovieCharacters");
                });

            modelBuilder.Entity("MovieLibrary.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("MovieId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MovieLibrary.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCurrentlyLoggedIn")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRegistered")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MovieLibrary.Models.Genre", b =>
                {
                    b.HasOne("MovieLibrary.Models.Movie", null)
                        .WithMany("Genres")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("MovieLibrary.Models.Movie", b =>
                {
                    b.HasOne("MovieLibrary.Models.User", "Creator")
                        .WithMany("AddedMovies")
                        .HasForeignKey("CreatorId");

                    b.HasOne("MovieLibrary.Models.User", null)
                        .WithMany("LikedMovies")
                        .HasForeignKey("UserId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("MovieLibrary.Models.MovieCharacter", b =>
                {
                    b.HasOne("MovieLibrary.Models.Character", "Character")
                        .WithMany("MoviesCharacters")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieLibrary.Models.Movie", "Movie")
                        .WithMany("MoviesCharacters")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieLibrary.Models.Review", b =>
                {
                    b.HasOne("MovieLibrary.Models.User", "Author")
                        .WithMany("Reviews")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieLibrary.Models.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieLibrary.Models.Character", b =>
                {
                    b.Navigation("MoviesCharacters");
                });

            modelBuilder.Entity("MovieLibrary.Models.Movie", b =>
                {
                    b.Navigation("Genres");

                    b.Navigation("MoviesCharacters");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MovieLibrary.Models.User", b =>
                {
                    b.Navigation("AddedMovies");

                    b.Navigation("LikedMovies");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}