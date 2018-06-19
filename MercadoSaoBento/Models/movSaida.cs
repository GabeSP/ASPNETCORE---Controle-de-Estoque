using MercadoSaoBento.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoSaoBento.Models
{
    public class movSaida
    {
        public int movSaidaID { get; set; }

        [Display(Name = "Data do Movimento")]
        public DateTime dataSaida { get; set; }

        public int Quantidade { get; set; }

        [Display(Name = "Observação")]
        public String obs { get; set; }
        
        public Produto Produto { get; set; }
        public int ProdutoID { get; set; }
        
        public MotivosSaida MotivosSaida { get; set; }
        public int MotivosSaidaID { get; set; }
    }
}
