using AssessmentAPI.Models;
using AssessmentAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssessmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ItableInterface tableInterface;

        public TableController(ItableInterface tableInterface)
        {
            this.tableInterface = tableInterface;
        }



      
        // add a table
        [HttpPost]
        public async Task<IActionResult> AddTable([FromBody] Aotable table)
        {
            try
            {
                if (table != null)
                {
                    var newTable = tableInterface.AddTable(table);
                    if (newTable != null)
                    {
                        return Ok(newTable);
                    }
                    else
                    {
                        return BadRequest("Table Cannot be null");
                    }
                }
                else
                {
                    return BadRequest();
                }
                
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        //Update a table
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTable([FromRoute] Guid id, [FromBody] Aotable table)
        {
            try
            {
                if (table != null)
                {
                    var Newtable = tableInterface.UpdateTable(id, table);
                    if (Newtable != null)
                    {
                        return Ok(Newtable);
                    }
                    else
                    {
                        return NotFound($"Table with id: {id} is not found");
                    }
                }
                else
                {
                    return BadRequest();
                }
               
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //
        [HttpGet("policyOrschedule")]
        public IActionResult GetAllTableByType()
        {
            var result = tableInterface.GetAllTableByType();
            return result != null ? Ok(result):NotFound();
        }
    }
}
