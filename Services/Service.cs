using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Domain;
using Infrastructure.Repository;

namespace Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public async Task<T> ObterPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task<T> AdicionarAsync(T entidade)
        {
            return await _repository.AdicionarAsync(entidade);
        }

        public async Task<T> AtualizarAsync(T entidade)
        {
            return await _repository.AtualizarAsync(entidade);
        }

        public async Task RemoverAsync(int id)
        {
            await _repository.RemoverAsync(id);
        }

        public bool ValidarEntidade(T entidade)
        {
            return entidade != null;
        }
    }
}