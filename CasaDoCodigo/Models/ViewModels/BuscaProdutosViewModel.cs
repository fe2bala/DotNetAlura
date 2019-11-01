using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaProdutosViewModel
    {
        public IList<Categoria> Categorias { get; set; }
        public string Pesquisa { get; set; }
        public string PesquisaErro { get; set; }

        public BuscaProdutosViewModel(IList<Categoria> categorias, string pesquisa, string erro)
        {
            Categorias = categorias;
            Pesquisa = pesquisa;
            PesquisaErro = erro;
        }
        public BuscaProdutosViewModel()
        {

        }
            
    }
}
