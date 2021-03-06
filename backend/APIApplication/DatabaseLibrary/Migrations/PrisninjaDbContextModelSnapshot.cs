// <auto-generated />
using DatabaseLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiApplication.Migrations
{
    [DbContext(typeof(PrisninjaDbContext))]
    partial class PrisninjaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DatabaseLibrary.Models.Product", b =>
                {
                    b.Property<long>("EAN")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EAN"), 1L, 1);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Measurement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Organic")
                        .HasColumnType("bit");

                    b.Property<double>("Units")
                        .HasColumnType("float");

                    b.HasKey("EAN");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DatabaseLibrary.Models.ProductStandardName", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("MeasureG")
                        .HasColumnType("bit");

                    b.Property<bool>("MeasureL")
                        .HasColumnType("bit");

                    b.Property<bool>("MeasureStk")
                        .HasColumnType("bit");

                    b.Property<bool>("Organic")
                        .HasColumnType("bit");

                    b.HasKey("Name");

                    b.ToTable("ProductStandardNames");
                });

            modelBuilder.Entity("DatabaseLibrary.Models.ProductStore", b =>
                {
                    b.Property<long>("ProductKey")
                        .HasColumnType("bigint");

                    b.Property<int>("StoreKey")
                        .HasColumnType("int");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.HasKey("ProductKey", "StoreKey");

                    b.HasIndex("StoreKey");

                    b.ToTable("ProductStores");
                });

            modelBuilder.Entity("DatabaseLibrary.Models.Store", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Location_X")
                        .HasColumnType("float");

                    b.Property<double>("Location_Y")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("DatabaseLibrary.Models.ProductStore", b =>
                {
                    b.HasOne("DatabaseLibrary.Models.Product", "Product")
                        .WithMany("ProductStores")
                        .HasForeignKey("ProductKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseLibrary.Models.Store", "Store")
                        .WithMany("ProductStores")
                        .HasForeignKey("StoreKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("DatabaseLibrary.Models.Product", b =>
                {
                    b.Navigation("ProductStores");
                });

            modelBuilder.Entity("DatabaseLibrary.Models.Store", b =>
                {
                    b.Navigation("ProductStores");
                });
#pragma warning restore 612, 618
        }
    }
}
