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
public class BranchesController(ApplicationDbContext dbContext) : ODataController
{
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(dbContext.Branches.Where(x=>!x.IsDeleted));
    }

    public async Task<ActionResult> Post([FromBody] Branch branch)
    {
        try
        {
            dbContext.Branches.Add(branch);
            await dbContext.SaveChangesAsync();
            return Created(branch);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    public async Task<ActionResult> Put([FromRoute] int key, [FromBody] Branch Branch)
    {
        var branch = await dbContext.Branches.SingleOrDefaultAsync(d => d.Id == key);

        if (branch == null)
        {
            return NotFound();
        }

        Branch.Adapt(branch);
        dbContext.Update(branch);
        await dbContext.SaveChangesAsync();

        return Updated(branch);
    }

    public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] Delta<Branch> delta)
    {
        var branch =await  dbContext.Branches.SingleOrDefaultAsync(d => d.Id.Equals(key));

        if (branch == null)
        {
            return NotFound();
        }
        else if (!branch.GetType().Equals(delta.StructuredType))
        {
            return BadRequest();
        }

        delta.Patch(branch);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    public async Task<ActionResult> Delete([FromRoute] int key)
    {
        var branch = await dbContext.Branches.SingleOrDefaultAsync(d => d.Id == key);

        if (branch != null)
        {
            branch.IsDeleted = true;
            dbContext.Branches.Update(branch);
        }

        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
