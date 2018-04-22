using ConsultaResolv.Entidades;

namespace ConsultaResolv.Repositories
{
    class ErrorRepository
    {
        private Context contexto;

        public void Insert(Error error)
        {
            using (contexto = new Context())
            {
                contexto.Erros.Add(error);
                contexto.SaveChanges();
            }
        }
    }
}
