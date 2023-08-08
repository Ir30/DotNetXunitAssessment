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
            if(column != null)
            {
                await dbContext.Aocolumns.AddAsync(column);

                await dbContext.SaveChangesAsync();
                return column;
            }
            else
            {
                return null;
            }
        }

        public Aocolumn DeleteColumn(Guid id)
        {
            var column =  dbContext.Aocolumns.Find(id);
            if (column != null)
            {
                dbContext.Aocolumns.Remove(column);
                dbContext.SaveChanges();
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

        public IEnumerable<Aocolumn> GetColumnBytype()
        {
            var Records = dbContext.Aocolumns
               .Where(r => r.DataType == "decimal" || r.DataType == "datetime")
               .ToList();
            if(Records.Count > 0)
            {
                return Records;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Aotable> GetTableDataByname(string name)
        {
            var tableInfo = dbContext.Aotables.Include(t => t.Aocolumns).Where(t => t.Name == name).ToList();
            return tableInfo != null ? tableInfo : Enumerable.Empty<Aotable>();
        }
    }
}
