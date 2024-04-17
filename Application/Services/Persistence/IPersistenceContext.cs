namespace Application.Services.Persistence
{

    public interface IPersistenceContext
    {
        #region Methods

        void Add<TEntity>(TEntity entity);

        IQueryable<TEntity> GetEntities<TEntity>();

        Task<int> SaveChangesAsync();

        #endregion
    }

}
