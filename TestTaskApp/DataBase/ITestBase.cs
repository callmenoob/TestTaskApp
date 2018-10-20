using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskApp.DataBase
{
    public interface ITestBase
    {
        IEnumerable<T> Execute<T>(string command, T @object);
    }
}
