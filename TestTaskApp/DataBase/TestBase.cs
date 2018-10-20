using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TestTaskApp.Models;
using Dapper;
using System.Data.SqlClient;

namespace TestTaskApp.DataBase
{
    public class TestBase : ITestBase
    {
        private readonly string _connectionstring;
        public TestBase(IConfiguration config)
        {
            _connectionstring = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<T> Execute<T>(string command, T @object)
        {
            string sqlCommand = command;
            IEnumerable<T> result;

            using (var connection = new SqlConnection(_connectionstring))
            {
                result = connection.Query<T>(command, @object);
            }

            return result;
        }
    }
}
