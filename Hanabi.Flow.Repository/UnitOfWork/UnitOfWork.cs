using Hanabi.Flow.Data;
using Hanabi.Flow.IRepository.UnitOfWork;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hanabi.Flow.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _myContext;

        public UnitOfWork(MyContext myContext)
        {
            _myContext = myContext;
        }

        public void BeginTran()
        {
            GetDbClient().BeginTran();
        }

        public void CommitTran()
        {
            GetDbClient().CommitTran();
        }

        public SqlSugarClient GetDbClient() => _myContext.Db;

        public void RollbackTran()
        {
            GetDbClient().RollbackTran();
        }
    }
}
