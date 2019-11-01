using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IProdutoRepository
    {
        Task SaveProdutos(List<Livro> livros);
        IList<Produto> GetProdutos();
        IList<Categoria> GetProdutosCategoria(ModelStateDictionary modelState, string pesquisa);
    }
}