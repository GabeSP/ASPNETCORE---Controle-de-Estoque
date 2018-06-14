using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoSaoBento.Models
{
    public class Fornecedor
    {
        public int FornecedorID { get; set; }
        public String Nome { get; set; }
        public String CNPJ { get; set; }
        public String Telefone { get; set; }
    }
}
