using System;

namespace ConsultaResolv.Entidades
{
    public class Pessoa
    {
        public int id { get; set; }
        public string cpf { get; set; }
        public string nome { get; set; }
        public string situacao { get; set; }
        public string datanascimento { get; set; }
        public string datainscricao { get; set; }
        public string datahora { get; set; }
        public string comprovante { get; set; }
        public int status { get; set; }
        public string message { get; set; }
        public int fonte { get; set; }
        public DateTime dataprocessamento { get; set; }

        public Pessoa()
        {
            dataprocessamento = DateTime.Now;
        }
    }
}
