using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestTaskApp.DataBase
{
    public class TestBase : ITestBase
    {
        string _config;
        public TestBase(IConfiguration config)
        {
            _config = config.GetConnectionString("DefaultConnection");
        }

        public void DONothing()
        {

        }
    }
}
