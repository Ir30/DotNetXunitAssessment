using AssessmentAPI.Models;
using AssessmentAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace AssessmentAPI.Service
{
    public class TableRepository : ItableInterface
    {
        private readonly YourDbContext dbContext;

        public TableRepository(YourDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       
        public async Task<Aotable> AddTable(Aotable table)
        {
            if (table != null)
            {
                await dbContext.Aotables.AddAsync(table);
                await dbContext.SaveChangesAsync();
                return table;
            }
            else
            {
                return null;
            }
                
        }

        public IEnumerable<Aotable> GetAllTableByType()
        {
            var Records = dbContext.Aotables
               .Where(r => r.Type == "schedule" || r.Type == "policy")
               .ToList();
            return Records!=null?Records: Enumerable.Empty<Aotable>();
        }

        public async Task<Aotable> UpdateTable(Guid id, Aotable table)
        {
           
                var ExistingTable = await dbContext.Aotables.SingleOrDefaultAsync(option => option.Id == id);
                if (ExistingTable != null)
                {
                    ExistingTable.Id = table.Id;
                    ExistingTable.Name = table.Name;
                    ExistingTable.Type = table.Type;
                    ExistingTable.Description = table.Description;
                    ExistingTable.Comment = table.Comment;
                    ExistingTable.History = table.History;
                    ExistingTable.Boundary = table.Boundary;
                    ExistingTable.Log = table.Log;
                    ExistingTable.Cache = table.Cache;
                    ExistingTable.Notify = table.Notify;
                    ExistingTable.Identifier = table.Identifier;
                    await dbContext.SaveChangesAsync();
                    return table;
                }
                else { return null; }
            
        }
    }
}
