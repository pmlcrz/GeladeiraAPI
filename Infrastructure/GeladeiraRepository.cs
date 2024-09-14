using Domain;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GeladeiraRepository
    {
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly GeladeiraContext _contexto;
            private readonly DbSet<T> _dbSet;

            public Repository(GeladeiraContext contexto)
            {
                _contexto = contexto;
                _dbSet = _contexto.Set<T>();
            }

            public async Task<IEnumerable<T>> ObterTodosAsync()
            {
                return await _dbSet.ToListAsync();
            }

            public async Task<T> ObterPorIdAsync(int id)
            {
                return await _dbSet.FindAsync(id);
            }

            public async Task<T> AdicionarAsync(T entidade)
            {
                await _dbSet.AddAsync(entidade);
                await _contexto.SaveChangesAsync();
                return entidade;
            }

            public async Task<T> AtualizarAsync(T entidade)
            {
                _dbSet.Update(entidade);
                await _contexto.SaveChangesAsync();
                return entidade;
            }

            public async Task RemoverAsync(int id)
            {
                var entidade = await ObterPorIdAsync(id);
                if (entidade != null)
                {
                    _dbSet.Remove(entidade);
                    await _contexto.SaveChangesAsync();
                }
            }
        }
    }
}