using AssessmentAPI.Models;
using AssessmentAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AssessmentAPI.Service
{
    public class ColumnRepository : IColumnInterface
    {
        private readonly YourDbContext dbContext;

        public ColumnRepository(YourDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Aocolumn> AddColumn(Aocolumn column)
        {
            
                await dbContext.Aocolumns.AddAsync(column);

                await dbContext.SaveChangesAsync();
                return column;
        }

        public async Task<Aocolumn> DeleteColumn(Guid id)
        {
            var column =await dbContext.Aocolumns.FindAsync(id);
            if (column != null)
            {
                dbContext.Aocolumns.Remove(column);
                await dbContext.SaveChangesAsync();
                return column;
            }
            return null;
        }

        public async Task<Aocolumn> EditColumn(Guid id, Aocolumn column)
        {
            var ColumnDetails = await dbContext.Aocolumns.SingleOrDefaultAsync(option => option.Id == id);
            if (ColumnDetails != null)
            {
                ColumnDetails.Id = column.Id;
                ColumnDetails.TableId = column.TableId;
                ColumnDetails.Name = column.Name;
                ColumnDetails.Description = column.Description;
                ColumnDetails.DataType = column.DataType;
                ColumnDetails.DataSize = column.DataSize;
                ColumnDetails.DataScale = column.DataScale;
                ColumnDetails.Comment = column.Comment;
                ColumnDetails.Encrypted = column.Encrypted;
                ColumnDetails.Distortion = column.Distortion;

                await dbContext.SaveChangesAsync();
                return column;
            }
            else { return null; }
        }

        public async Task<IEnumerable<Aocolumn>> GetColumnBytype()
        {
            var Records =await dbContext.Aocolumns
               .Where(r => r.DataType == "decimal" || r.DataType == "datetime")
               .ToListAsync();
            if(Records.Count > 0)
            {
                return Records;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Aotable>> GetTableDataByname(string name)
        {
            var tableInfo =await dbContext.Aotables.Include(t => t.Aocolumns).Where(t => t.Name == name).ToListAsync();
            return tableInfo != null ? tableInfo : Enumerable.Empty<Aotable>();
        }
    }
}
