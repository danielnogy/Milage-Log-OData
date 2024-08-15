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
public class ProvidersController(ApplicationDbContext dbContext) : ODataController
{
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(dbContext.Providers.Where(x => !x.IsDeleted));
    }

    public async Task<ActionResult> Post([FromBody] Provider provider)
    {
        try
        {
            dbContext.Providers.Add(provider);
            await dbContext.SaveChangesAsync();
            return Created(provider);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    public async Task<ActionResult> Put([FromRoute] int key, [FromBody] Provider Provider)
    {
        var provider = await dbContext.Providers.SingleOrDefaultAsync(d => d.Id == key);

        if (provider == null)
        {
            return NotFound();
        }

        Provider.Adapt(provider);
        dbContext.Update(provider);
        await dbContext.SaveChangesAsync();

        return Updated(provider);
    }

    public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] Delta<Provider> delta)
    {
        var provider =await  dbContext.Providers.SingleOrDefaultAsync(d => d.Id.Equals(key));

        if (provider == null)
        {
            return NotFound();
        }
        else if (!provider.GetType().Equals(delta.StructuredType))
        {
            return BadRequest();
        }

        delta.Patch(provider);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    public async Task<ActionResult> Delete([FromRoute] int key)
    {
        var provider = await dbContext.Providers.SingleOrDefaultAsync(d => d.Id == key);

        if (provider != null)
        {
            provider.IsDeleted = true;
            dbContext.Providers.Update(provider);
        }

        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
