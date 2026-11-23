using FixIt_Backend.Data.Models.Database;

namespace FixIt_Backend.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FixItContext _dbContext;
        public UnitOfWork(FixItContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task BeginTransaction()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _dbContext.Database.CommitTransactionAsync();

        }

        public async Task RollbackTransaction()
        {
            await _dbContext.Database.RollbackTransactionAsync();

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
