using MercadoSaoBento.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoSaoBento.Models
{
    
    public class movEntrada
    {
        public int movEntradaID { get; set; }

        [Required(ErrorMessage = "Nº do documento inválido.")]
        [Display(Name = "Nº Documento")]
        public int nmrDocumento { get; set; }

        [Display(Name = "Data do Movimento")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime dataEntrada { get; set; }

        public int Quantidade { get; set; }

        [Display(Name = "Observação")]
        public String obs { get; set; }
        public Produto Produto { get; set; }

        public int ProdutoID { get; set; }
    }
}
