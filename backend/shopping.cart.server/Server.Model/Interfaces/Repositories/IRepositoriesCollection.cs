﻿using Server.Model.Interfaces.Repositories;
using Server.Model.Models;

namespace Server.Model.Interfaces.Repositories
{
    public interface IRepositoriesCollection
    {
        #region Entities
        public IRepository<Brands> BrandsRepository { get; }
        public IRepository<Users> UsersRepository { get; }
        public IRepository<ExceptionLogs> ExceptionLogsRepository { get; }
        #endregion
        //public IQueryable<T> CreateExpression1<T>(IQueryable<T> recordSet, string columnName, string value, bool exact = false);
        #region Sp
        public ITestRepository TestRepository { get; }
        #endregion
    }
}