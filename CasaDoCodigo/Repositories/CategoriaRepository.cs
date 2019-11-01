using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria> AddCategoria(string Nome);
    }
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public async Task<Categoria> AddCategoria(string nome)
        {
            var categoria = contexto.Set<Categoria>().SingleOrDefault(p => p.Nome == nome);

            if (categoria != null)
            {
                return categoria;
            }
            categoria = new Categoria(nome);
            dbSet.Add(categoria);
            await contexto.SaveChangesAsync();

            return categoria;

        }
    }
}
