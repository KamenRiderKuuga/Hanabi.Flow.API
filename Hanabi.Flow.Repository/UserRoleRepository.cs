using Hanabi.Flow.IRepository;
using Hanabi.Flow.IRepository.UnitOfWork;
using Hanabi.Flow.Model.Models;
using Hanabi.Flow.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hanabi.Flow.Repository
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
