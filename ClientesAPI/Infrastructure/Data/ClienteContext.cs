using ClientesAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ClientesAPI.Infrastructure.Data
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options) { }
        
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Nome)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Telefone)
                      .HasMaxLength(20);

                entity.OwnsOne(c => c.Endereco, endereco =>
                {
                    endereco.Property<string>("Rua").HasMaxLength(100).HasColumnName("Rua");
                    endereco.Property<string>("Numero").HasMaxLength(10).HasColumnName("Numero");
                    endereco.Property<string>("Cidade").HasMaxLength(50).HasColumnName("Cidade");
                    endereco.Property<string>("Estado").HasMaxLength(50).HasColumnName("Estado");
                    endereco.Property<string>("CEP").HasMaxLength(20).HasColumnName("CEP");
                });

            });

        }
    }


}
