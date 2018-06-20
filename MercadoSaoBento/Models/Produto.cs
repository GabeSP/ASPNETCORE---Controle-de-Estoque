using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoSaoBento.Models
{
    public class Produto
    {
        [Display(Name = "Código")]
        public int ProdutoID { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório", AllowEmptyStrings = false)]
        public String Nome { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatório", AllowEmptyStrings = false),
            Display(Name = "Descrição")]
        public String Descricao { get; set; }

        [Required(ErrorMessage = "O preço do produto é obrigatório", AllowEmptyStrings = false),
            Display(Name = "Preço")]
        public decimal Preco { get; set; }


        [Required(ErrorMessage = "A Quantidade do produto é obrigatório", AllowEmptyStrings = false),
            Display(Name = "Quantidade")]
        public int QtdEstoque { get; set; }

        [Display(Name = "Fornecedor")]
        public int FornecedorID { get; set; }
        public Fornecedor Fornecedor{get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }


    }
}
