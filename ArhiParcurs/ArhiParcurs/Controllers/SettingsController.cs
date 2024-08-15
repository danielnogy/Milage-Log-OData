using ArhiParcurs.Client.Pages.Faz;
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
public class SettingsController(ApplicationDbContext dbContext) : ODataController
{
    [EnableQuery]
    public IActionResult Get()
    {
        
        return Ok(dbContext.Settings.Where(x => !x.IsDeleted));
    }

    public async Task<ActionResult> Post([FromBody] Setting setting)
    {
        try
        {
            
            dbContext.Settings.Add(setting);
            await dbContext.SaveChangesAsync();
            return Created(setting);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    public async Task<ActionResult> Put([FromRoute] int key, [FromBody] Setting Setting)
    {
        var setting = await dbContext.Settings.SingleOrDefaultAsync(d => d.Id == key);

        if (setting == null)
        {
            return NotFound();
        }

        Setting.Adapt(setting);
        dbContext.Settings.Update(setting);
        await dbContext.SaveChangesAsync();

        return Updated(setting);
    }
    public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] Delta<Setting> delta)
    {
        var setting = await dbContext.Settings.SingleOrDefaultAsync(d => d.Id.Equals(key));

        if (setting == null)
        {
            return NotFound();
        }
        else if (!setting.GetType().Equals(delta.StructuredType))
        {
            return BadRequest();
        }

        delta.Patch(setting);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var setting = await dbContext.Settings.SingleOrDefaultAsync(d => d.Id == id);

        if (setting != null)
        {
            setting.IsDeleted = true;
            dbContext.Settings.Update(setting);
        }

        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
