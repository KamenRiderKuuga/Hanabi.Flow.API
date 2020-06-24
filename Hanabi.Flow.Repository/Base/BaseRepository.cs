using Google.Protobuf.WellKnownTypes;
using Hanabi.Flow.Data;
using Hanabi.Flow.IRepository.Base;
using Hanabi.Flow.IRepository.UnitOfWork;
using Hanabi.Flow.Model;
using SQLitePCL;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi.Flow.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly SqlSugarClient _db;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork.GetDbClient();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity model)
        {
            var insert = _db.Insertable(model);
            return await insert.ExecuteReturnIdentityAsync();
        }

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="listEntity">插入的数据集</param>
        /// <returns></returns>
        public async Task<int> Add(List<TEntity> listEntity)
        {
            return await _db.Insertable(listEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity model)
        {
            var delete = _db.Deleteable(model);
            return await delete.ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            return await _db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 批量删除指定ID的数据
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await _db.Deleteable<TEntity>().In(ids).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 查询指定实体类的所有数据
        /// </summary>
        /// <returns>数据集合</returns>
        public async Task<List<TEntity>> Query()
        {
            return await _db.Queryable<TEntity>().ToListAsync();
        }

        /// <summary>
        /// 根据指定条件查询实体类数据集
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>数据集合</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await _db.Queryable<TEntity>()
                           .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere)
                           .ToListAsync();
        }

        /// <summary>
        /// 根据指定条件查询实体类数据集
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns>数据集合</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds = "")
        {
            return await _db.Queryable<TEntity>()
                           .WhereIF(whereExpression != null, whereExpression)
                           .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                           .ToListAsync();
        }

        /// <summary>
        /// 根据指定条件查询实体类数据集
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>数据集合</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await _db.Queryable<TEntity>()
                           .WhereIF(whereExpression != null, whereExpression)
                           .OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc)
                           .ToListAsync();
        }

        /// <summary>
        /// 根据指定条件查询实体类数据集
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns>数据集合</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>()
                           .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere)
                           .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                           .ToListAsync();
        }

        /// <summary>
        /// 根据条件查询前N条数据
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>()
                           .WhereIF(whereExpression != null, whereExpression)
                           .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                           .Take(intTop)
                           .ToListAsync();
        }

        /// <summary>
        /// 根据条件查询前N条数据
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>()
                           .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere)
                           .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                           .Take(intTop)
                           .ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>()
                           .WhereIF(whereExpression != null, whereExpression)
                           .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                           .ToPageListAsync(intPageIndex, intPageSize);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>()
                           .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere)
                           .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                           .ToPageListAsync(intPageIndex, intPageSize);
        }

        /// <summary>
        /// 根据ID查询实体
        /// </summary>
        /// <param name="objId">主键ID</param>
        /// <returns></returns>
        public async Task<TEntity> QueryById(object objId)
        {
            return await _db.Queryable<TEntity>().InSingleAsync(objId);
        }

        /// <summary>
        /// 查询主键ID
        /// </summary>
        /// <param name="objId">主键ID</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns></returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await _db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingleAsync(objId);
        }

        /// <summary>
        /// 根据主键集合查询数据集合
        /// </summary>
        /// <param name="lstIds"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await _db.Queryable<TEntity>().In(lstIds).ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// </summary>
        /// <typeparam name="T">实体一</typeparam>
        /// <typeparam name="T2">实体二</typeparam>
        /// <typeparam name="T3">实体三</typeparam>
        /// <typeparam name="TResult">返回集合</typeparam>
        /// <param name="joinExpression">关联表达式</param>
        /// <param name="selectExpression">字段选取表达式</param>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
        {
            return await _db.Queryable(joinExpression)
                           .WhereIF(selectExpression != null, whereLambda)
                           .Select(selectExpression)
                           .ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">每页数据量</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {
            RefAsync<int> totalCount = 0;

            var list = await _db.Queryable<TEntity>()
                               .WhereIF(whereExpression != null, whereExpression)
                               .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                               .ToPageListAsync(intPageIndex, intPageSize);

            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal())).ObjToInt();
            return new PageModel<TEntity>() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
        }

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <param name="strSql">完整的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>泛型集合</returns>
        public async Task<List<TEntity>> QuerySql(string strSql, SugarParameter[] parameters = null)
        {
            return await _db.Ado.SqlQueryAsync<TEntity>(strSql, parameters);
        }

        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <param name="strSql">完整的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable</returns>
        public async Task<DataTable> QueryTable(string strSql, SugarParameter[] parameters = null)
        {
            return await _db.Ado.GetDataTableAsync(strSql, parameters);
        }

        /// <summary>
        /// 根据实体模型更新数据
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>是否更新成功</returns>
        public async Task<bool> Update(TEntity model)
        {
            return await _db.Updateable<TEntity>().ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 根据条件更新实体模型
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="strWhere">更新条件</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await _db.Updateable<TEntity>().Where(strWhere).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operateAnonymousObjects"></param>
        /// <returns></returns>
        public async Task<bool> Update(object operateAnonymousObjects)
        {
            return await _db.Updateable<TEntity>(operateAnonymousObjects).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 根据指定条件更新指定列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lstColumns"></param>
        /// <param name="lstIgnoreColumns"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            IUpdateable<TEntity> up = _db.Updateable(entity);

            if (lstColumns?.Any() == true)
            {
                up.UpdateColumns(lstColumns.ToArray());
            }

            if (lstIgnoreColumns?.Any() == true)
            {
                up.IgnoreColumns(lstIgnoreColumns.ToArray());
            }

            if (!string.IsNullOrEmpty(strWhere))
            {
                up.Where(strWhere);
            }

            return await up.ExecuteCommandHasChangeAsync();

        }
    }
}
