using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Interfaces;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.Shared.ExtensionMethods;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace DGE.Service.Shared.BaseService
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Vars / Props

        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        protected readonly ILogger<BaseService<TEntity>> _logger;

        #endregion

        #region Constructor(s)
        public BaseService(IBaseRepository<TEntity> baseRepository, IStringLocalizer<AppLocalization> stringLocalizer, ILogger<BaseService<TEntity>> logger)
        {
            _baseRepository = baseRepository;
            _stringLocalizer = stringLocalizer;
            _logger = logger;
        }

        #endregion

        #region Actions
        public async virtual Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _baseRepository
                            .GetTableNoTracking()
                            .ToListAsync(cancellationToken);
        }
        public async virtual Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _baseRepository.GetByIdAsync(id, cancellationToken);
        }
        public async virtual Task<TEntity?> GetByIdNoTrackingAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        {
            return await _baseRepository.GetByIdNoTrackingAsync(id, cancellationToken, includes);
        }
        public async Task<string> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await _baseRepository.AddAsync(entity, cancellationToken);
                return _stringLocalizer[AppLocalizationKeys.Success];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add {Entity}", typeof(TEntity).Name);
                return _stringLocalizer[AppLocalizationKeys.AddFailed];
            }
        }
        public async Task<string> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                await _baseRepository.AddRangeAsync(entities, cancellationToken);
                return _stringLocalizer[AppLocalizationKeys.Success];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add a range of {Entity}", typeof(TEntity).Name);
                return _stringLocalizer[AppLocalizationKeys.AddFailed];
            }
        }
        public async Task<string> EditAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await _baseRepository.UpdateAsync(entity, cancellationToken);
                return _stringLocalizer[AppLocalizationKeys.Success];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update {Entity}", typeof(TEntity).Name);
                return _stringLocalizer[AppLocalizationKeys.UpdateFailed];
            }
        }
        public async Task<string> EditRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                await _baseRepository.UpdateRangeAsync(entities, cancellationToken);
                return _stringLocalizer[AppLocalizationKeys.Success];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update a range of {Entity}", typeof(TEntity).Name);
                return _stringLocalizer[AppLocalizationKeys.UpdateFailed];
            }
        }
        public async Task<string> HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await _baseRepository.DeleteAsync(entity, cancellationToken);
                return _stringLocalizer[AppLocalizationKeys.Success];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete {Entity}", typeof(TEntity).Name);
                return _stringLocalizer[AppLocalizationKeys.DeletedFailed];
            }
        }
        public async Task<string> HardDeleteWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            try
            {
                await _baseRepository.ExecuteDeleteAsync(predicate, cancellationToken);
                return _stringLocalizer[AppLocalizationKeys.Success];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to bulk delete {Entity}", typeof(TEntity).Name);
                return _stringLocalizer[AppLocalizationKeys.DeletedFailed];
            }
        }
        public async Task<string> SoftDeleteAndActivationAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _baseRepository.GetByIdAsync(id, cancellationToken);
            if (entity == null)
            {
                return _stringLocalizer[AppLocalizationKeys.NotFound];
            }
            entity.IsDeleted = !entity.IsDeleted;
            return await EditAsync(entity, cancellationToken);
        }
        public async virtual Task<PaginatedList<TEntity>> GetPaginatedListAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var queryable = _baseRepository
                            .GetTableNoTracking()
                            .AsQueryable();

            return await queryable.ToPaginatedListAsync(pageNumber, pageSize, cancellationToken);
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _baseRepository.BeginTransaction();
        }
        #endregion
    }
}
