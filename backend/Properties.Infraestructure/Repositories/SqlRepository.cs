using Microsoft.EntityFrameworkCore;

using Properties.Domain.Interfaces;
using Properties.Infraestructure.Data;



namespace Properties.Infraestructure.Repositories

{

    public class SqlRepository<T, TId> : IRepository<T, TId> where T : class

    {

        private readonly AppDbContext _context;

        private readonly DbSet<T> _dbset;



        public SqlRepository(AppDbContext context)

        {

            _context = context;

            _dbset = _context.Set<T>();

        }



        public async Task<T> AddAsync(T entity)

        {

            await _dbset.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;

        }



        public async Task<IEnumerable<T>> GetAllAsync()

        {

            return await _dbset.ToListAsync();

        }



        public async Task<T?> GetByIdAsync(TId id)

        {

            return await _dbset.FindAsync(id);

        }

    }

}