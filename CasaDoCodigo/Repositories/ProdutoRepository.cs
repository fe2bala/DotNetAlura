using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository CategoriaRepository;
        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {
            CategoriaRepository = categoriaRepository;
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet.ToList();
        }
        public IList<Categoria> GetProdutosCategoria(ModelStateDictionary modelState, string pesquisa)
        {
            var categorias = CategoriaRepository.GetCategoria().ToList();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                categorias.ForEach(c =>
                    c.Produtos = dbSet.Where(p => p.CategoriaId ==c.Id &&( p.Nome.ToUpper().Contains(pesquisa.ToUpper()) || c.Nome.ToUpper().Contains(pesquisa.ToUpper()))).ToList()
                );
                categorias = categorias.Where(c => c.Produtos.Count() > 0).ToList();
                if (categorias.Count() <= 0)
                {
                    modelState.AddModelError("Pesquisa_Produto", "Não foram encontrados produtos com o termo pesquisado.");
                }
                return categorias;
            }

            categorias.ForEach(c => c.Produtos = dbSet.Where(p => p.CategoriaId == c.Id).ToList());

            return categorias;
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    var categoria = await CategoriaRepository.AddCategoria(livro.Categoria);
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria.Id));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
