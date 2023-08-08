using AssessmentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentAPI.Service.Interface
{
    public interface IColumnInterface
    {
        public Task<Aocolumn> AddColumn(Aocolumn column);
        public IEnumerable<Aotable> GetTableDataByname(string name);
        public IEnumerable<Aocolumn> GetColumnBytype();
        public Aocolumn DeleteColumn(Guid id);
        public Task<Aocolumn> EditColumn(Guid id,Aocolumn aocolumn);
    }
}
