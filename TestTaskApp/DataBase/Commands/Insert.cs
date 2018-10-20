using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskApp.Models;

namespace TestTaskApp.DataBase.Commands
{
    public class Insert : ICommand
    {
        ITestBase _testBase;
        public Insert(ITestBase testBase)
        {
            _testBase = testBase;
        }

        public IEnumerable<T> Execute<T>(string sqlcmd, T list)
        {
            return _testBase.Execute<T>(sqlcmd, list);
        }
    }
}
