using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace Services.Interfaces
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(int id);
        Task<T> AdicionarAsync(T entidade);
        Task<T> AtualizarAsync(T entidade);
        Task RemoverAsync(int id);
        bool ValidarEntidade(T entidade);
    }
}
