using System.Collections.Generic;

namespace TestTaskApp.DataBase
{
    public interface ITestBase
    {
        IEnumerable<T> Execute<T>(string command, T @object);
    }
}
