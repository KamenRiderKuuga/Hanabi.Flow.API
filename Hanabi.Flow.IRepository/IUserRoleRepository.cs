using Hanabi.Flow.IRepository.Base;
using Hanabi.Flow.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hanabi.Flow.IRepository
{
    /// <summary>
    /// 继承基类接口创建的接口，若有额外方法可以在此处扩展
    /// </summary>
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
    }
}
