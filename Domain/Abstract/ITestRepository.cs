using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface ITestRepository : IRepository<Test>
    {
        Test GetById(int id);
        IEnumerable<Test> GetAllReady();
        Test GetByName(string name);
        IEnumerable<Test> SearchByKeyWord(string keyWord);
    }
}
