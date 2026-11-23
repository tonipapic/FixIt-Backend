namespace FixIt_Backend.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync();
        public Task BeginTransaction();
        public Task CommitTransaction();
        public Task RollbackTransaction();
    }
}
