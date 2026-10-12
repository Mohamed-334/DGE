using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace DGE.Infrastructure.Shared.BaseRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Props

        protected readonly AppDbContext _context;

        #endregion

        #region Constructor(s)
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Actions
        public virtual async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }

        // For read-only queries: the entity is not tracked, so it must not be passed to Update/Delete afterwards
        public virtual async Task<TEntity?> GetByIdNoTrackingAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }


        public IQueryable<TEntity> GetTableNoTracking()
        {
            return _context.Set<TEntity>()
                           .AsNoTracking()
                           .Where(x => !x.IsDeleted)
                           .AsQueryable();
        }


        public virtual async Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task DeleteRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Single DELETE ... WHERE statement: rows are not loaded, and the audit interceptor does not see them
        public virtual async Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>()
                                 .Where(predicate)
                                 .ExecuteDeleteAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }



        public IDbContextTransaction BeginTransaction()
        {


            return _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();

        }

        public void RollBack()
        {
            _context.Database.RollbackTransaction();

        }

        public IQueryable<TEntity> GetTableAsTracking()
        {
            return _context.Set<TEntity>()
                           .Where(x => !x.IsDeleted)
                           .AsQueryable();

        }

        public virtual async Task UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
