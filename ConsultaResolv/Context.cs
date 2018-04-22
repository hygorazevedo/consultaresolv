using ConsultaResolv.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ConsultaResolv
{
    public class Context : DbContext
    {
        public Context()
            : base("conexao")
        { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Error> Erros { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Context>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            var pessoa = modelBuilder.Entity<Pessoa>();
            pessoa.HasKey(p => p.id).Property(p => p.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            pessoa.ToTable("pessoa");

            var erro = modelBuilder.Entity<Error>();
            erro.HasKey(p => p.id).Property(p => p.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            erro.ToTable("error");

            modelBuilder.Configurations.Add(pessoa);
            modelBuilder.Configurations.Add(erro);
        }
    }
}
