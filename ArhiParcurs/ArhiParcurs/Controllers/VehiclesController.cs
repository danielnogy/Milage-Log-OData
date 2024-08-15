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
public class VehiclesController(ApplicationDbContext dbContext) : ODataController
{
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(dbContext.Vehicles.Where(x=>x.IsDeleted==false));
    }

    public async Task<ActionResult> Post([FromBody] Vehicle vehicle)
    {
        try
        {
            dbContext.Vehicles.Add(vehicle);
            await dbContext.SaveChangesAsync();
            return Created(vehicle);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Vehicle Vehicle)
    {
        var vehicle =await dbContext.Vehicles.SingleOrDefaultAsync(d => d.Id == id);

        if (vehicle == null)
        {
            return NotFound();
        }

        Vehicle.Adapt(vehicle);
        dbContext.Update(vehicle);
        await dbContext.SaveChangesAsync();

        return Updated(vehicle);
    }
    public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] Delta<Vehicle> delta)
    {
        var vehicle = await dbContext.Vehicles.SingleOrDefaultAsync(d => d.Id.Equals(key));

        if (vehicle == null)
        {
            return NotFound();
        }
        else if (!vehicle.GetType().Equals(delta.StructuredType))
        {
            return BadRequest();
        }

        delta.Patch(vehicle);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var vehicle = await dbContext.Vehicles.SingleOrDefaultAsync(d => d.Id == id);

        if (vehicle != null)
        {
            vehicle.IsDeleted = true;
            dbContext.Vehicles.Update(vehicle);
        }

        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
