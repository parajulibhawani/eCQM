using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using eQM;

namespace eQM.Migrations
{
    [DbContext(typeof(MeasureDBContext))]
    partial class MeasureDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eQM.Measure", b =>
                {
                    b.Property<int>("MeasureId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClinicalGuidance");

                    b.Property<int>("MeasureNumber");

                    b.Property<string>("Rationale");

                    b.Property<string>("Title");

                    b.HasKey("MeasureId");

                    b.ToTable("Measures");
                });

            modelBuilder.Entity("eQM.References", b =>
                {
                    b.Property<int>("ReferenceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MeasureId");

                    b.Property<string>("Reference");

                    b.HasKey("ReferenceId");

                    b.HasIndex("MeasureId");

                    b.ToTable("References");
                });

            modelBuilder.Entity("eQM.References", b =>
                {
                    b.HasOne("eQM.Measure", "Measure")
                        .WithMany("References")
                        .HasForeignKey("MeasureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
