using System;

namespace ConsultaResolv.Entidades
{
    public class Error
    {
        public int id { get; set; }
        public string cpf { get; set; }
        public string datanascimento { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public DateTime dataprocessamento { get; set; }

        public Error()
        {
            dataprocessamento = DateTime.Now;
        }
    }
}
