﻿using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface IUserRepository
    {
      public  User GetUserById(Guid id);
      public  List<User> GetAllUsers();
    }
}
