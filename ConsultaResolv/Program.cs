using ConsultaResolv.Entidades;
using ConsultaResolv.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace ConsultaResolv
{
    class Program
    {
        #region Properties
        private static string filePath = @"E:\xampp\htdocs\client_ole.php";
        private static string url = "http://localhost/client_ole.php";
        private static Context contexto;
        private static Error error;
        private static Pessoa pessoa; 
        #endregion

        static void Main(string[] args)
        {
            IEnumerable<Consulta> consultas;
            using (contexto = new Context())
            {
                var itemQuery = contexto.Database.Connection.Query<Consulta>(Resources.query);
                consultas = itemQuery;
            }
            foreach (var consulta in consultas)
            {
                StringBuilder builder = new StringBuilder();
                
                builder.AppendFormat(Resources.consulta, consulta.cpf, consulta.datanascimento);
                string texto = Resources.parte1 + builder.ToString() + Resources.parte2;
                using (var writer = new StreamWriter(filePath, false))
                {
                    writer.Write(texto);
                }

                using (var client = new WebClient())
                {
                    var response = client.DownloadString(url);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(response);
                    XmlNodeList nodeErros = doc.GetElementsByTagName("Error");
                    if (nodeErros[0].HasChildNodes)
                    {
                        error = new Error
                        {
                            code = Convert.ToInt32(nodeErros[0].ChildNodes[0].InnerText),
                            message = nodeErros[0].ChildNodes[1].InnerText
                        };
                        ErrorRepository repository = new ErrorRepository();
                        repository.Insert(error);
                    }
                    else
                    {
                        XmlNodeList nodePessoa = doc.GetElementsByTagName("Pessoa");
                        if (nodePessoa[0].HasChildNodes)
                        {
                            pessoa = new Pessoa
                            {
                                cpf = nodePessoa[0].ChildNodes[0].InnerText,
                                nome = nodePessoa[0].ChildNodes[1].InnerText,
                                situacao = nodePessoa[0].ChildNodes[2].InnerText,
                                datanascimento = nodePessoa[0].ChildNodes[3].InnerText,
                                datainscricao = nodePessoa[0].ChildNodes[4].InnerText,
                                datahora = nodePessoa[0].ChildNodes[5].InnerText,
                                comprovante = nodePessoa[0].ChildNodes[6].InnerText,
                                status = Convert.ToInt32(nodePessoa[0].ChildNodes[7].InnerText),
                                message = nodePessoa[0].ChildNodes[8].InnerText,
                                fonte = Convert.ToInt32(nodePessoa[0].ChildNodes[9].InnerText)
                            };
                            PessoaRepository repository = new PessoaRepository();
                            repository.Insert(pessoa);
                        }
                    }
                }
            }
        }
    }
}
