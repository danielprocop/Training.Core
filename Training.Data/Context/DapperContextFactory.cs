using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Training.Data
{
    public class DapperContextFactory : IContextFactory
    {
        private readonly string connectionString;
        public DapperContextFactory(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public IContext Create()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var transaction = connection.BeginTransaction();
            return new DapperContext(transaction);
        }
    }

}
