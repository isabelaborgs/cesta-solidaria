using Microsoft.EntityFrameworkCore;

namespace app.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<OfertaDoacao> OfertasDoacao { get; set; }
        public DbSet<ItemOferta> ItensOferta { get; set; }
        public DbSet<ItemSolicitacao> ItensSolicitacao { get; set; }
        public DbSet<OpcaoAgendamentoOferta> OpcoesAgendamentoOferta { get; set; }
        public DbSet<SolicitacaoDoacao> SolicitacoesDoacao { get; set; }
        public DbSet<OpcaoAgendamentoSolicit> OpcoesAgendamentoSolicit { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoDeUsuario")
                .HasValue<UsuarioDoadorPf>("UsuarioDoadorPf")
                .HasValue<UsuarioDoadorPj>("UsuarioDoadorPj")
                .HasValue<UsuarioOng>("UsuarioOng");

            modelBuilder.Entity<UsuarioDoadorPj>()
                .Property(b => b.Cnpj)
                .HasColumnName("Cnpj");
            modelBuilder.Entity<UsuarioOng>()
                .Property(b => b.Cnpj)
                .HasColumnName("Cnpj");

            modelBuilder.Entity<UsuarioDoadorPj>()
                .Property(b => b.RazaoSocial)
                .HasColumnName("RazaoSocial");
            modelBuilder.Entity<UsuarioOng>()
                .Property(b => b.RazaoSocial)
                .HasColumnName("RazaoSocial");

            modelBuilder.Entity<ItemOferta>()
                .HasOne(rd => rd.Oferta)
                .WithMany(r => r.ItensOferta)
                .HasForeignKey(d => d.IdOferta);

            modelBuilder.Entity<OpcaoAgendamentoOferta>()
                .HasOne(rd => rd.Oferta)
                .WithMany(r => r.OpcoesAgendamento)
                .HasForeignKey(d => d.IdOferta);
                        
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<app.Models.UsuarioDoadorPf> UsuarioDoadorPf { get; set; } = default!;
        public DbSet<app.Models.UsuarioDoadorPj> UsuarioDoadorPj { get; set; } = default!;
        public DbSet<app.Models.UsuarioOng> UsuarioOng { get; set; } = default!;
        public object OfertaDoacao { get; internal set; }
    }
}
