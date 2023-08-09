using AssessmentAPI.Models;
using AssessmentAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssessmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        public IColumnInterface ColumnInterface { get; }

        public ColumnController(IColumnInterface columnInterface)
        {
            ColumnInterface = columnInterface;
        }

        //Add a column for the AOTable record created in the above  API

        [HttpPost]
        public async Task<IActionResult> AddColumn([FromBody] Aocolumn column)
        {
            try
            {
                if (column != null) 
                {
                    var result =await ColumnInterface.AddColumn(column);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest();
                    }

                }else { return BadRequest(); }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Edit a record

        [HttpPut("{id}")]
        public async Task<IActionResult> EditColumn([FromRoute] Guid id, [FromBody] Aocolumn column)
        {
            try
            {
                if (column != null)
                {
                    var Column = await ColumnInterface.EditColumn(id, column);
                    if (Column != null)
                    {

                        return Ok(Column);
                    }
                    else { return NotFound($"Column with id: {id} is not found"); }
                }
                else
                {
                    return BadRequest();
                }
                
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Delete a record
        [HttpDelete("{id}")]
        public ActionResult DeleteColumn(Guid id)
        {
            try
            {
                var column = ColumnInterface.DeleteColumn(id);
                if (column != null)
                {   
                    return Ok("success");
                }
                return NotFound("not found");
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        //Get all records with DataType "decimal" and "datetime"
        [HttpGet("decimalOrdatetime")]
        public IActionResult GetColumnBytype()
        {
            try
            {
                var Records = ColumnInterface.GetColumnBytype();
                if (Records != null)
                {
                    return Ok(Records);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Get all records from AOColumn based on Table Name
        [HttpGet("{tableName}")]
        public IActionResult GetTableDataByname(string tableName)
        {
            try
            {
                if (string.IsNullOrEmpty(tableName))
                {
                    return BadRequest("The table name cannot be empty.");
                }

                var tableInfo = ColumnInterface.GetTableDataByname(tableName);

                if (tableInfo == null)
                {
                    return NotFound($"The table '{tableName}' does not exist in AOTable.");
                }

                return Ok(tableInfo);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
