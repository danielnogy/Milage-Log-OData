using ArhiParcurs.Data;
using ArhiParcurs.Shared.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ArhiParcurs.Controllers;
[Route("odata/[controller]")]
public class ConsumptionIncreasesController(ApplicationDbContext dbContext) : ODataController
{

    [EnableQuery]
    public ActionResult Get()
    {
        return Ok(dbContext.ConsumptionIncreases.Where(x => !x.IsDeleted));
    }

    public async Task<IActionResult> Post([FromBody] ConsumptionIncrease consumptionIncrease)
    {
        try
        {
            dbContext.ConsumptionIncreases.Add(consumptionIncrease);
            await dbContext.SaveChangesAsync();
            return Created(consumptionIncrease);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    public async Task<IActionResult> Put([FromRoute] int key, [FromBody] ConsumptionIncrease ConsumptionIncrease)
    {
        var consumptionIncrease = dbContext.ConsumptionIncreases.SingleOrDefault(d => d.Id == key);

        if (consumptionIncrease == null)
        {
            return NotFound();
        }

        ConsumptionIncrease.Adapt(consumptionIncrease);
        dbContext.Update(consumptionIncrease);
        await dbContext.SaveChangesAsync();

        return Updated(consumptionIncrease);
    }
    public async Task<IActionResult> Patch([FromRoute] int key, [FromBody] Delta<ConsumptionIncrease> delta)
    {
        var consumptionIncrease = dbContext.ConsumptionIncreases.SingleOrDefault(d => d.Id.Equals(key));

        if (consumptionIncrease == null)
        {
            return NotFound();
        }
        else if (!consumptionIncrease.GetType().Equals(delta.StructuredType))
        {
            return BadRequest();
        }

        delta.Patch(consumptionIncrease);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    public async Task<IActionResult> Delete([FromRoute] int key)
    {
        var consumptionIncrease =  dbContext.ConsumptionIncreases.SingleOrDefault(d => d.Id == key);

        if (consumptionIncrease != null)
        {
            consumptionIncrease.IsDeleted = true;
            dbContext.ConsumptionIncreases.Update(consumptionIncrease);
        }

        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
