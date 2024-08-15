using ArhiParcurs.Data;
using ArhiParcurs.Shared.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace ArhiParcurs.Controllers;

[Route("api/[controller]")]
public class FuelsController(ApplicationDbContext dbContext) : ODataController
{
    [EnableQuery]
    public ActionResult Get()
    {
        return Ok(dbContext.Fuels.Where(x => !x.IsDeleted));
    }

    public async Task<IActionResult> Post([FromBody] Fuel fuel)
    {
        try
        {
            dbContext.Fuels.Add(fuel);
            await dbContext.SaveChangesAsync();
            return Created(fuel);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    public async Task<IActionResult> Put([FromRoute] int key, [FromBody] Fuel Fuel)
    {
        var fuel = await dbContext.Fuels.SingleOrDefaultAsync(d => d.Id == key);

        if (fuel == null)
        {
            return NotFound();
        }

        Fuel.Adapt(fuel);
        dbContext.Update(fuel);
        await dbContext.SaveChangesAsync();

        return Updated(fuel);
    }
    public async Task<IActionResult> Patch([FromRoute] int key, [FromBody] Delta<Fuel> delta)
    {
        var fuel = await dbContext.Fuels.SingleOrDefaultAsync(d => d.Id.Equals(key));

        if (fuel == null)
        {
            return NotFound();
        }
        else if (!fuel.GetType().Equals(delta.StructuredType))
        {
            return BadRequest();
        }

        delta.Patch(fuel);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    public async Task<IActionResult> Delete([FromRoute] int key)
    {
        var fuel = await  dbContext.Fuels.SingleOrDefaultAsync(d => d.Id == key);

        if (fuel != null)
        {
            fuel.IsDeleted = true;
            dbContext.Fuels.Remove(fuel);
        }

         await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
