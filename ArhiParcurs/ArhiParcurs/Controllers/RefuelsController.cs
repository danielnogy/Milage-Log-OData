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
public class RefuelsController(ApplicationDbContext dbContext) : ODataController
{
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(dbContext.Refuels.Where(x => !x.IsDeleted));
    }

    public async Task<ActionResult> Post([FromBody] Refuel refuels)
    {
        try
        {
            dbContext.Refuels.Add(refuels);
            await dbContext.SaveChangesAsync();
            return Created(refuels);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    public async Task<ActionResult> Put([FromRoute] int key, [FromBody] Refuel Refuel)
    {
        var refuels = await dbContext.Refuels.SingleOrDefaultAsync(d => d.Id == key);

        if (refuels == null)
        {
            return NotFound();
        }

        Refuel.Adapt(refuels);
        dbContext.Update(refuels);
        await dbContext.SaveChangesAsync();

        return Updated(refuels);
    }
    public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] Delta<Refuel> delta)
    {
        var refuels = await dbContext.Refuels.SingleOrDefaultAsync(d => d.Id.Equals(key));

        if (refuels == null)
        {
            return NotFound();
        }
        else if (!refuels.GetType().Equals(delta.StructuredType))
        {
            return BadRequest();
        }

        delta.Patch(refuels);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    public async Task<ActionResult> Delete([FromRoute] int key)
    {
        var refuels = await dbContext.Refuels.SingleOrDefaultAsync(d => d.Id == key);

        if (refuels != null)
        {
            refuels.IsDeleted = true;
            dbContext.Refuels.Update(refuels);
        }

        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
