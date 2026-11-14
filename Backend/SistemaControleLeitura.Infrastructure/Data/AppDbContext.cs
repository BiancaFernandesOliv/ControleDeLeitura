using Microsoft.EntityFrameworkCore;
using SistemaControleLeitura.Domain.Entities;

namespace SistemaControleLeitura.Infrastructure.Data;

public class AppDbContext : DbContext {

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; private set; }
    public DbSet<Livro> Livros { get; private set; }
    public DbSet<Leitura> Leituras { get; private set; }
    public DbSet<MetaAnual> MetasAnuais { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity => {
            entity.HasKey(p => p.IdUsuario);
            entity.Property(p => p.Nome).IsRequired().HasMaxLength(120);
            entity.Property(p => p.Email).IsRequired().HasMaxLength(254);
            entity.Property(p => p.SenhaHash).IsRequired().HasMaxLength(256);
            entity.HasIndex(p => p.Email).IsUnique();
        });

        modelBuilder.Entity<Livro>(entity => {
            entity.HasKey(p => p.IdLivro);
            entity.Property(p => p.Titulo).IsRequired().HasMaxLength(120);
            entity.Property(p => p.Autor).IsRequired().HasMaxLength(120);
            entity.Property(p => p.NumeroPaginas).IsRequired();
            entity.HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(p => p.IdUsuario)
            .IsRequired();
        });

        modelBuilder.Entity<Leitura>(entity => {
            entity.HasKey(p => p.IdLeitura);
            entity.Property(p => p.StatusLeitura).IsRequired();
            entity.HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(p => p.IdUsuario)
            .IsRequired();
            entity.HasOne(p => p.Livro)
            .WithMany()
            .HasForeignKey(p => p.IdLivro)
            .IsRequired();
        });

        modelBuilder.Entity<MetaAnual>(entity => {
            entity.HasKey(p => p.IdMeta);
            entity.Property(p => p.Ano).IsRequired();
            entity.Property(p => p.MetaQuantidade).IsRequired();
            entity.HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(p => p.IdUsuario)
            .IsRequired();
            entity.HasIndex(p => new { p.IdUsuario, p.Ano }).IsUnique(); entity.HasIndex(p => new { p.IdUsuario, p.Ano }).IsUnique();
        });
    }
}


