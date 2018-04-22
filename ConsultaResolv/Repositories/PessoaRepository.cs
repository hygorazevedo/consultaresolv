using ConsultaResolv.Entidades;

namespace ConsultaResolv.Repositories
{
    public class PessoaRepository
    {
        private Context contexto;

        public void Insert(Pessoa pessoa)
        {
            using (contexto = new Context())
            {
                contexto.Pessoas.Add(pessoa);
                contexto.SaveChanges();
            }
        }
    }
}
