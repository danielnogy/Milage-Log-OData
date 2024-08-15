using ArhiParcurs.Client.Pages.Faz.Sheets;
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
public class SheetsController(ApplicationDbContext dbContext) : ODataController
{
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(dbContext.Sheets.Where(x=>x.IsDeleted==false).OrderBy(x=>x.DateBegin));
    }
    [HttpGet("getLastNumber()")]
    public ActionResult CalculateInitialFuel()
    {
        try
        {
            //combustibil ramas in rezervor + alimentari - consum
            var lastNumber = dbContext.Sheets.Where(x=>x.IsDeleted==false)
                .OrderByDescending(x=>x.Number)
                .Select(x=>x.Number)
                .FirstOrDefault();

            return Ok(new { value = lastNumber+1 });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpGet("calculateInitialFuel(vehicleId={vehicleId},sheetId={sheetId})")]
    public ActionResult CalculateInitialFuel(int vehicleId,int sheetId)
    {
        try
        {
            //combustibil ramas in rezervor + alimentari - consum
            decimal lastSheetInitialFuel = 0;
            if (sheetId == 0)
            {
                var vehicleSheets = dbContext.Sheets.Where(x => x.VehicleId == vehicleId && x.IsDeleted == false).OrderByDescending(x => x.DateBegin).Select(x => x.FuelLeft);
                lastSheetInitialFuel = vehicleSheets.FirstOrDefault();
                if(vehicleSheets.Count() == 0)
                {
                    lastSheetInitialFuel = dbContext.Vehicles.Where(x => !x.IsDeleted && x.Id == vehicleId).Select(x => x.InitialFuelTank).FirstOrDefault();
                }
            }
            else
            {
                var currentSheetDate = dbContext.Sheets.Where(x => x.Id == sheetId).Select(x=>x.DateBegin).FirstOrDefault();
                var previouseSheets = dbContext.Sheets.Where(x => x.VehicleId == vehicleId && x.DateBegin < currentSheetDate && x.IsDeleted == false).OrderByDescending(x => x.DateBegin).Select(x => x.FuelLeft);
                lastSheetInitialFuel = previouseSheets.FirstOrDefault();
                if(previouseSheets.Count()<0)
                {
                    lastSheetInitialFuel = dbContext.Vehicles.Where(x => !x.IsDeleted && x.Id == vehicleId).Select(x => x.InitialFuelTank).FirstOrDefault();
                }
            }
            return Ok(new { value = lastSheetInitialFuel });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    private DateTime ReconstructDate(DateTime dateTime)
    {
        DateTime dateTimeWithZeroMilliseconds = new DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            dateTime.Hour,
            dateTime.Minute,
            dateTime.Second,
            0 // Milliseconds set to zero
        );
        return dateTimeWithZeroMilliseconds;
    }
    [HttpGet("recalculate(vehicleId={vehicleId},date={date})")]
    public async Task<ActionResult> Recalculate (int vehicleId,DateTime date)
    {
        try
        {
            date = ReconstructDate(date);
             //combustibil ramas in rezervor + alimentari - consum
            List<Sheet> sheetsToSync;
            if (date == DateTime.MinValue)
            {
                sheetsToSync = dbContext.Sheets.Where(x => x.VehicleId == vehicleId && x.IsDeleted == false).Include(x=>x.SheetRoutes).Include(x=>x.Vehicle).OrderBy(x => x.DateBegin).ToList();
            }
            else
            {
                sheetsToSync = dbContext.Sheets.Where(x => x.VehicleId == vehicleId && x.DateBegin.AddMilliseconds(-x.DateBegin.Millisecond) >= date && x.IsDeleted == false).Include(x => x.SheetRoutes).Include(x => x.Vehicle).OrderBy(x => x.DateBegin).ToList();
            }
            foreach (var sheet in sheetsToSync)
            {
                var currentSheetDate = ReconstructDate(sheet.DateBegin);
                var initialFuel = sheetsToSync.Where(x => x.VehicleId == vehicleId && x.DateBegin.AddMilliseconds(-x.DateBegin.Millisecond) < currentSheetDate && x.IsDeleted == false).OrderByDescending(x => x.DateBegin).Select(x => x.FuelLeft).FirstOrDefault();
                
                sheet.InitialFuel = sheetsToSync.Min(x=>x.DateBegin) == sheet.DateBegin? sheet.InitialFuel : initialFuel;
                sheet.FuelLeft = sheet.InitialFuel+sheet.Refuels - sheet.Consumed;

                dbContext.Update(sheet);
            }
                var changes = await dbContext.SaveChangesAsync();
            return Ok(new { value = true,  changes});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpGet("recalcValidation(vehicleId={vehicleId},date={date})")]
    public async Task<ActionResult> RecalcValidation (int vehicleId, DateTime date)
    {
        try
        {
            var sheetsCount = dbContext.Sheets.Where(x => x.VehicleId == vehicleId && x.DateBegin.AddMilliseconds(-x.DateBegin.Millisecond) > date).Count();
            await dbContext.SaveChangesAsync();
            return Ok(new { value = sheetsCount>0?true:false });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    public async Task<ActionResult> Post([FromBody] Sheet sheets)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                // Log or inspect ModelState to find out why the model binding failed
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(new { Message = "Model validation failed", Errors = errors });
            }
            dbContext.Sheets.Add(sheets);
                await dbContext.SaveChangesAsync();
            return Created(sheets);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }

    

    public async Task<ActionResult> Put([FromRoute] int key, [FromBody] Sheet Sheet)
    {
        var sheets = await dbContext.Sheets.SingleOrDefaultAsync(d => d.Id == key);
        var sheetroutes = dbContext.SheetRoutes.AsNoTracking().Where(sr => sr.SheetId == key).ToList();
        if (sheets == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            // Log or inspect ModelState to find out why the model binding failed
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest(new { Message = "Model validation failed", Errors = errors });
        }

        var removed = sheetroutes
                                          .Where(sr => !Sheet.SheetRoutes
                                                                     .Any(ur => ur.Id == sr.Id))
                                          .ToList();
        
        try
        {
            foreach (var sheet in removed)
            {
                dbContext.SheetRoutes.Remove(sheet);
            }
            Sheet.Adapt(sheets);
            dbContext.Sheets.Update(sheets);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.InnerException);
        }


        return Updated(sheets);
    }
    public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] Delta<Sheet> delta)
    {
        var sheets =await dbContext.Sheets.SingleOrDefaultAsync(d => d.Id.Equals(key));

        if (sheets == null)
        {
            return NotFound();
        }
        else if (!sheets.GetType().Equals(delta.StructuredType))
        {
            return BadRequest();
        }

        delta.Patch(sheets);
        await dbContext.SaveChangesAsync();
        return Ok();
    }

    public async Task<ActionResult> Delete([FromRoute] int key)
    {
        var sheets = await dbContext.Sheets.Include(x=>x.SheetRoutes).SingleOrDefaultAsync(d => d.Id == key);

        if (sheets != null)
        {
            //dbContext.Sheets.Remove(sheets);
            sheets.IsDeleted = true;
            dbContext.Update(sheets);
            foreach (var sheetRoute in sheets.SheetRoutes)
            {
                sheetRoute.IsDeleted = true;
                dbContext.Update(sheetRoute);
            }
        }

        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
