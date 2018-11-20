using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscaCep.Data.Entidades
{
    [Table("Cep")]
    public class Cep
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Unidade { get; set; }
        public string Ibge { get; set; }
        public string Gia { get; set; }
    }
}
