using ArhiParcurs.Client.Services;
using ArhiParcurs.Shared;
using ArhiParcurs.Shared.Models;
using ArhiParcurs.Shared.Models.Enums;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System.Net.Http.Json;
using System.Web;
using Action = Syncfusion.Blazor.Grids.Action;

namespace ArhiParcurs.Client.Pages.Faz.Sheets;
public partial class SheetsForm : ComponentBase
{
    [Parameter] public int SheetId { get; set; }
    [Parameter] public Sheet SheetModel { get; set; } = new Sheet();
    [Parameter] public EventCallback SubmitForm { get; set; }
    [Parameter] public string HeaderText { get; set; }
    [Inject] ISheetService SheetService { get; set; }
    [Inject] HttpClient httpClient { get; set; }
    [Inject] IVehicleService VehicleService { get; set; }
    [Inject] SfDialogService DialogService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    private bool IsVehicleSelected = false;
    public int FormItemColumnSpan = 2;
    public int FormGroupColumn3 = 3;
    public int FormGroupColumn4 = 4;
    public int FormGroupColumn = 2;
    public int FormColumn = 2;
    private decimal FuelTVA;
    private decimal IncreasePercent = 0;
    private Vehicle SelectedVehicle { get; set; } = new();
    private Setting CurrentSetting { get; set; } = new();   
    private async Task<decimal> GetInitialFuel()
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<InitialFUelResponse>($"api/sheets/calculateInitialFuel(vehicleId={SelectedVehicle.Id},sheetId={SheetId})");
            return response.value;
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        return 0;
    }
    private void SetIsVehicleSelected()
    {
        if (SelectedVehicle != null && SelectedVehicle.Id != 0)
        {
            IsVehicleSelected = true;
        }
    }

    private async Task GetSelectedVehicle()
    {
        try
        {
            SelectedVehicle = (await VehicleService.GetVehicles("?$filter=id eq " + SheetModel.VehicleId)).FirstOrDefault();
            if (SheetModel.InitialFuel == 0)
                SheetModel.InitialFuel = await GetInitialFuel();
            SetIsVehicleSelected();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        //populate left fuel based on other sheets


    }
    protected override async Task OnInitializedAsync()
    {
        SheetModel.Number = (await httpClient.GetFromJsonAsync<GetNumberResponse>($"api/sheets/getLastNumber()")).value;
        CurrentSetting = (await httpClient.GetFromJsonAsync<ODataResponse<Setting>>("api/settings")).Value.FirstOrDefault();
    }
    protected override async Task OnParametersSetAsync()
    {
        await GetSelectedVehicle();
    }
    public void OnActionComplete(ActionEventArgs<SheetRoute> args)
    {
        if (args.RequestType.Equals(Action.Add) || args.RequestType.Equals(Action.BeginEdit))
        {
            //based on Add or Edit action disable the PreventRender 
            args.PreventRender = false;

        }
        if (args.RequestType.Equals(Action.Save) || args.RequestType.Equals(Action.Delete))
        {

            CalculateDistance();
            CalculateFuel();

            StateHasChanged();
        }
    }

    public void OnActionCompleteRefuels(ActionEventArgs<Refuel> args)
    {
        if (args.RequestType.Equals(Action.Add) || args.RequestType.Equals(Action.BeginEdit))
        {
            //based on Add or Edit action disable the PreventRender 
            args.PreventRender = false;

        }
        if (args.RequestType.Equals(Action.Save) || args.RequestType.Equals(Action.Delete))
        {

            CalculateDistance();
            CalculateFuel();

            StateHasChanged();
        }
    }
    private void IncreaseChanged(ChangeEventArgs<int?,ConsumptionIncrease> args)
    {
        IncreasePercent= args.ItemData != null ? args.ItemData.Percent : 0;
        CalculateFuel();
    }
    private void CalculateFuel()
    {
        SheetModel.Consumed = (SheetModel.DrivenKilometers / 100) * SelectedVehicle.FuelConsumption;
        SheetModel.Consumed += SheetModel.Consumed * (IncreasePercent/100);
        SheetModel.Refuels  = (SheetModel.RefuelsList.Sum(x => x.Quantity));
        SheetModel.FuelLeft = (SheetModel.InitialFuel - SheetModel.Consumed+SheetModel.Refuels);
    }
    private void CalculateDistance()
    {
        SheetModel.DrivenKilometers = 0;
        foreach (var item in SheetModel.SheetRoutes)
        {
            SheetModel.DrivenKilometers += item.TotalEquivalentDistance * (item.RoundTrip ? 2 : 1);
        }
    }
    private void ValueChangeHandler(Syncfusion.Blazor.Inputs.ChangeEventArgs<decimal> args, ref SheetRoute data)
    {

        data.TotalEquivalentDistance = data.Distance1 * (decimal)0.9
            + data.Distance2
            + data.Distance3 * (decimal)1.1
            + data.Distance4 * (decimal)1.2
            + data.Distance5 * (decimal)1.4
            + data.Distance6 * (decimal)1.6;

        data.TotalDistance = data.Distance1 + data.Distance2 + data.Distance3 + data.Distance4 + data.Distance5 + data.Distance6;
        StateHasChanged();
    }


    private void ValueChangeHandlerRefuels(Syncfusion.Blazor.Inputs.ChangeEventArgs<decimal> args, ref Refuel data)
    {
        data.PriceWithTva = (data.FuelPrice * data.Quantity) + ((FuelTVA / 100) * (data.FuelPrice * data.Quantity));
        data.PriceWithoutTva = (data.FuelPrice * data.Quantity);
        StateHasChanged();
    }

    public async Task ValidSubmitHandler()
    {
        
        //SheetModel.DateBegin = new DateTime(SheetModel.DateBegin.Year,SheetModel.DateBegin.Month,SheetModel.DateBegin.Day,SheetModel.DateBegin.Hour,SheetModel.DateBegin.Minute,SheetModel.DateBegin.Second);
        //SheetModel.DateEnd = new DateTime(SheetModel.DateEnd.Year,SheetModel.DateEnd.Month,SheetModel.DateEnd.Day,SheetModel.DateEnd.Hour,SheetModel.DateEnd.Minute,SheetModel.DateEnd.Second);
        //foreach (var item in SheetModel.SheetRoutes)
        //{
        //    item.StartDate = new DateTime(item.StartDate.Year, item.StartDate.Month, item.StartDate.Day, item.StartDate.Hour, item.StartDate.Minute, item.StartDate.Second);
        //    item.EndDate = new DateTime(item.EndDate.Year, item.EndDate.Month, item.EndDate.Day, item.EndDate.Hour, item.EndDate.Minute, item.EndDate.Second);

        //}
        await SubmitForm.InvokeAsync();
        var x = await DialogService.ConfirmAsync("Doriti recalcularea combustibilului pe foile de parcurs mai recente?", "Atentie!");
        if (x)
        {
            //apeleaza procedura recalcul folosind numarul foii
            // Convert DateTime to ISO 8601 string format
            var dateBeginString = SheetModel.DateBegin.ToString("yyyy-MM-ddTHH:mm:ss");

            // URL encode the date string
            var encodedDateBegin = HttpUtility.UrlEncode(dateBeginString);
            var response = await httpClient.GetFromJsonAsync<RecalculateResponse>($"api/sheets/recalculate(vehicleId={SelectedVehicle.Id},date={encodedDateBegin})");
        }
    }
    public void LayoutChangedhandler(string activeBreakpoint)
    {
        if (activeBreakpoint == "Small")
        {
            FormGroupColumn = FormColumn = FormGroupColumn3 = FormGroupColumn4 = 1;
        }
        else if (activeBreakpoint == "Medium")
        {
            FormGroupColumn4 = 2;
            FormGroupColumn3 = 2;
            FormColumn = 2;
            FormItemColumnSpan = 1;
        }
        else
        {
            FormGroupColumn4 = 4;
            FormGroupColumn3 = 3;
            FormColumn = 2;
            FormGroupColumn = 2;
            FormItemColumnSpan = 1;
        }
    }
    private string activeBreakpoint { get; set; } = "Large";
    public class InitialFUelResponse
    {
        public decimal value { get; set; }
    }
    public class GetNumberResponse
    {
        public int value { get; set; }
    }
    public class RecalculateResponse
    {
        public bool value { get; set; }
    }
}