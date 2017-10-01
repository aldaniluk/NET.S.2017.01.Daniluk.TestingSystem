using Domain.Entities;

namespace Domain.Abstract
{
    public interface IPassTestService
    {
        Statistic PassTest(PassTestModel passTest);
    }
}
