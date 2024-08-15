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
public class PartnersController(ApplicationDbContext dbContext) : ODataController
{
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(dbContext.Partners.Where(x => !x.IsDeleted));
    }

    public async Task<ActionResult> Post([FromBody] Partner partner)
    {
        try
        {
            dbContext.Partners.Add(partner);
            await dbContext.SaveChangesAsync();
            return Created(partner);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    public async Task<ActionResult> Put([FromRoute] int key, [FromBody] Partner Partner)
    {
        var partner = await dbContext.Partners.SingleOrDefaultAsync(d => d.Id == key);

        if (partner == null)
        {
            return NotFound();
        }

        Partner.Adapt(partner);
        dbContext.Update(partner);
        await dbContext.SaveChangesAsync();

        return Updated(partner);
    }

    public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] Delta<Partner> delta)
    {
        var partner =await  dbContext.Partners.SingleOrDefaultAsync(d => d.Id.Equals(key));

        if (partner == null)
        {
            return NotFound();
        }
        else if (!partner.GetType().Equals(delta.StructuredType))
        {
            return BadRequest();
        }

        delta.Patch(partner);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    public async Task<ActionResult> Delete([FromRoute] int key)
    {
        var partner = await dbContext.Partners.SingleOrDefaultAsync(d => d.Id == key);

        if (partner != null)
        {
            partner.IsDeleted = true;
            dbContext.Partners.Update(partner);
        }

        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
