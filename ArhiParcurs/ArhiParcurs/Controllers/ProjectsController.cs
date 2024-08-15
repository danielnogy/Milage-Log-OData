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
public class ProjectsController(ApplicationDbContext dbContext) : ODataController
{
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(dbContext.Projects.Where(x => !x.IsDeleted));
    }

    public async Task<ActionResult> Post([FromBody] Project project)
    {
        try
        {
            dbContext.Projects.Add(project);
            await dbContext.SaveChangesAsync();
            return Created(project);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    public async Task<ActionResult> Put([FromRoute] int key, [FromBody] Project Project)
    {
        var project = await dbContext.Projects.SingleOrDefaultAsync(d => d.Id == key);

        if (project == null)
        {
            return NotFound();
        }

        Project.Adapt(project);
        dbContext.Update(project);
        await dbContext.SaveChangesAsync();

        return Updated(project);
    }

    public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] Delta<Project> delta)
    {
        var project =await  dbContext.Projects.SingleOrDefaultAsync(d => d.Id.Equals(key));

        if (project == null)
        {
            return NotFound();
        }
        else if (!project.GetType().Equals(delta.StructuredType))
        {
            return BadRequest();
        }

        delta.Patch(project);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    public async Task<ActionResult> Delete([FromRoute] int key)
    {
        var project = await dbContext.Projects.SingleOrDefaultAsync(d => d.Id == key);

        if (project != null)
        {
            project.IsDeleted = true;
            dbContext.Projects.Update(project);
        }

        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
