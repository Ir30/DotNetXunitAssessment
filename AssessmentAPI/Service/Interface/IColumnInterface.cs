using AssessmentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentAPI.Service.Interface
{
    public interface IColumnInterface
    {
        public Task<Aocolumn> AddColumn(Aocolumn column);
        public Task<IEnumerable<Aotable>> GetTableDataByname(string name);
        public Task<IEnumerable<Aocolumn>> GetColumnBytype();
        public Task<Aocolumn> DeleteColumn(Guid id);
        public Task<Aocolumn> EditColumn(Guid id,Aocolumn aocolumn);
    }
}
