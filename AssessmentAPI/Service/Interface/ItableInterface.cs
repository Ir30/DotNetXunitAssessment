using AssessmentAPI.Models;

namespace AssessmentAPI.Service.Interface
{
    public interface ItableInterface
    {
        public Task<Aotable> AddTable(Aotable table);
        public Task<Aotable> UpdateTable(Guid id,Aotable table);
        public IEnumerable<Aotable> GetAllTableByType();
    }
}
