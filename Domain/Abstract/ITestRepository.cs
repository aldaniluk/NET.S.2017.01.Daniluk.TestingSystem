using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface ITestRepository : IRepository<Test>
    {
        Test GetById(int id);
        Test GetByName(string name);
        IEnumerable<Test> GetAllReady();
        IEnumerable<Test> SearchAllTestsByKeyWord(string keyWord);
        IEnumerable<Test> SearchAllReadyTestsByKeyWord(string keyWord); 
    }
}
