using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure
{
    public class GeladeiraContext : DbContext
    {
        public GeladeiraContext(DbContextOptions<GeladeiraContext> options) : base(options)
        {
        }

        public DbSet<ItemModel> Itens { get; set; }
    }
}