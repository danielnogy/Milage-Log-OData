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
public class SheetRoutesController(ApplicationDbContext dbContext) : ODataController
{
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(dbContext.SheetRoutes.Where(x => !x.IsDeleted));
    }

    public async Task<ActionResult> Post([FromBody] SheetRoute sheetRoutes)
    {
        try
        {
            dbContext.SheetRoutes.Add(sheetRoutes);
            await dbContext.SaveChangesAsync();
            return Created(sheetRoutes);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    public async Task<ActionResult> Put([FromRoute] int key, [FromBody] SheetRoute SheetRoute)
    {
        var sheetRoutes = await dbContext.SheetRoutes.SingleOrDefaultAsync(d => d.Id == key);

        if (sheetRoutes == null)
        {
            return NotFound();
        }

        SheetRoute.Adapt(sheetRoutes);
        dbContext.Update(sheetRoutes);
        await dbContext.SaveChangesAsync();

        return Updated(sheetRoutes);
    }
    public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] Delta<SheetRoute> delta)
    {
        var sheetRoutes = await dbContext.SheetRoutes.SingleOrDefaultAsync(d => d.Id.Equals(key));

        if (sheetRoutes == null)
        {
            return NotFound();
        }
        else if (!sheetRoutes.GetType().Equals(delta.StructuredType))
        {
            return BadRequest();
        }

        delta.Patch(sheetRoutes);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    public async Task<ActionResult> Delete([FromRoute] int key)
    {
        var sheetRoutes = await dbContext.SheetRoutes.SingleOrDefaultAsync(d => d.Id == key);

        if (sheetRoutes != null)
        {
            dbContext.SheetRoutes.Remove(sheetRoutes);
        }

        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
