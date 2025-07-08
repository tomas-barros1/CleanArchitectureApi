using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;

namespace Infra.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }
    }
}
