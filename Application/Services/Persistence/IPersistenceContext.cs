namespace Application.Services.Persistence
{

    public interface IPersistenceContext
    {
        #region Methods

        void Add<TEntity>(TEntity entity);

        IQueryable<TEntity> GetEntities<TEntity>();

        void Remove<TEntity>(TEntity entity);

        Task<int> SaveChangesAsync();

        #endregion
    }

}
