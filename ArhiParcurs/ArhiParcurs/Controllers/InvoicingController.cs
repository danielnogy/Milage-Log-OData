using ArhiParcurs.Data;
using ArhiParcurs.Shared.Models;
using BoldReports.Web;
using BoldReports.Writer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArhiParcurs.Controllers;

[Route("[controller]")]
public class InvoicingController : ControllerBase
{

    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly ApplicationDbContext dbContext;

    public InvoicingController(IWebHostEnvironment hostingEnvironment, ApplicationDbContext dbContext)
    {
        this._hostingEnvironment = hostingEnvironment;
        this.dbContext = dbContext;
    }

    [HttpPost("exportFAZ")]
    public IActionResult ExportFAZ([FromBody] ReportRequest reportRequest)
    {
        try
        {
            // Here, we have loaded the sales-order-detail sample report from application the folder wwwroot\Resources.
            //D:\Source\ErpNou\Arhimedes\Arhimedes.WebAPI\Resources\facturacutvav1.rdl
            var sheet = dbContext.Sheets.Where(x => x.Id == reportRequest.SheetId).Include(x => x.Vehicle).FirstOrDefault();
            string vehicleNumber = "";
            if (sheet.Vehicle != null)
            {
                vehicleNumber = sheet.Vehicle.Number;
            }
            if (sheet == null)
            {
                return NotFound("Foaia nu a fost gasita");
            }

            string reportName = "FAZ.rdl";
            FileStream inputStream = new FileStream(@$".\Resources\{reportName}", FileMode.Open, FileAccess.Read);
            //FileStream inputStream = new FileStream(@".\Resources\facturacutvav1.rdl", FileMode.Open, FileAccess.Read);
            MemoryStream reportStream = new MemoryStream();
            inputStream.CopyTo(reportStream);
            reportStream.Position = 0;
            inputStream.Close();
            BoldReports.Writer.ReportWriter writer = new BoldReports.Writer.ReportWriter();
            writer.ReportErrorOccurred += Writer_ReportErrorOccurred;
            writer.ExportLayout = ExportLayout.Print;

            var parameters = new List<ReportParameter>()
        {
            new ReportParameter() { Name = "SheetId", Values = new List<string>() { reportRequest.SheetId.ToString()} }
        };

            //foreach (var param in modelRaportare.ParametersValues)
            //{

            //    var parameter = new ReportParameter() { Name = param.Key, Values = new List<string>() { param.Value } };
            //    parameters.Add(parameter);
            //}
            //var parameter = new ReportParameter() { Name="DocumentId",Values=new List<string>() {DocumentId.ToString()},};


            var datasourceCredentials = new List<DataSourceCredentials>() {
            new DataSourceCredentials() { Name = "FAZDataSource", ConnectionString = $@"Server=(localdb)\mssqllocaldb;Database=myDb1;Trusted_Connection=True;",  SecurityType = SecurityType.Integrated }
        };
            //foreach (var item in raport.DataSources)
            //{
            //    var ds = new DataSourceCredentials() { Name = item.Denumire, ConnectionString = $"Data Source={item.Server};Initial Catalog={userContext.DatabaseName};Encrypt=false;TrustServerCertificate=false;", UserId = item.UserId, Password = item.Password, SecurityType = SecurityType.DataBase };
            //    datasourceCredentials.Add(ds);
            //}
            //{
            //    new DataSourceCredentials(){ Name = "DSdocument", ConnectionString = $"Data Source=86.127.174.29,49000;Initial Catalog={userContext.DatabaseName};Encrypt=false;TrustServerCertificate=false;", UserId = "sa", Password = "mogosa.2008", SecurityType = SecurityType.DataBase }
            //};

            string fileName = null;
            WriterFormat format;
            string type = null;

            fileName = $"FAZ_{sheet.Number}_{vehicleNumber}.pdf";
            type = "pdf";
            format = WriterFormat.PDF;

            writer.LoadReport(reportStream);
            writer.SetDataSourceCredentials(datasourceCredentials);
            writer.SetParameters(parameters);
            MemoryStream memoryStream = new MemoryStream();
            writer.Save(memoryStream, format);

            // Download the generated export document to the client side.
            memoryStream.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(memoryStream, "application/" + type);
            fileStreamResult.FileDownloadName = fileName;
            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"");
            Response.ContentType = "application/pdf";
            return fileStreamResult;
        }
        catch (Exception ex)
        {

            return BadRequest(ex.InnerException);
        }
    }

    [HttpPost("exportCentralizare")]
    public IActionResult ExportCentralizare([FromBody] ReportRequest reportRequest)
    {
        try
        {
            // Here, we have loaded the sales-order-detail sample report from application the folder wwwroot\Resources.
            //D:\Source\ErpNou\Arhimedes\Arhimedes.WebAPI\Resources\facturacutvav1.rdl



            string reportName = "CentralizareConsum.rdl";
            FileStream inputStream = new FileStream(@$".\Resources\{reportName}", FileMode.Open, FileAccess.Read);
            //FileStream inputStream = new FileStream(@".\Resources\facturacutvav1.rdl", FileMode.Open, FileAccess.Read);
            MemoryStream reportStream = new MemoryStream();
            inputStream.CopyTo(reportStream);
            reportStream.Position = 0;
            inputStream.Close();
            BoldReports.Writer.ReportWriter writer = new BoldReports.Writer.ReportWriter();
            writer.ReportErrorOccurred += Writer_ReportErrorOccurred;
            writer.ExportLayout = ExportLayout.Print;

            var parameters = new List<ReportParameter>()
            {
                new ReportParameter() { Name = "dindata", Values = new List<string>() { reportRequest.StartDate.ToString("yyyy-MM-dd")} },
                new ReportParameter() { Name = "ladata", Values = new List<string>() { reportRequest.EndDate.ToString("yyyy-MM-dd") } }
            };

            //foreach (var param in modelRaportare.ParametersValues)
            //{

            //    var parameter = new ReportParameter() { Name = param.Key, Values = new List<string>() { param.Value } };
            //    parameters.Add(parameter);
            //}
            //var parameter = new ReportParameter() { Name="DocumentId",Values=new List<string>() {DocumentId.ToString()},};


            var datasourceCredentials = new List<DataSourceCredentials>() {
            new DataSourceCredentials() { Name = "FAZDataSource", ConnectionString = $"Data Source=86.127.174.29,49000;Initial Catalog=ICAS;Encrypt=false;TrustServerCertificate=true;User Id=sa;Password=mogosa.2008", UserId = "sa", Password = "mogosa.2008", SecurityType = SecurityType.DataBase }};
            
            //foreach (var item in raport.DataSources)
            //{
            //    var ds = new DataSourceCredentials() { Name = item.Denumire, ConnectionString = $"Data Source={item.Server};Initial Catalog={userContext.DatabaseName};Encrypt=false;TrustServerCertificate=false;", UserId = item.UserId, Password = item.Password, SecurityType = SecurityType.DataBase };
            //    datasourceCredentials.Add(ds);
            //}
            //{
            //    new DataSourceCredentials(){ Name = "DSdocument", ConnectionString = $"Data Source=86.127.174.29,49000;Initial Catalog={userContext.DatabaseName};Encrypt=false;TrustServerCertificate=false;", UserId = "sa", Password = "mogosa.2008", SecurityType = SecurityType.DataBase }
            //};

            string fileName = null;
            WriterFormat format;
            string type = null;

            fileName = $"Centralizare_{reportRequest.StartDate.ToString("dd-MM-yyyy")}_{reportRequest.EndDate.ToString("dd-MM-yyyy")}.pdf";
            type = "pdf";
            format = WriterFormat.PDF;

            writer.LoadReport(reportStream);
            writer.SetDataSourceCredentials(datasourceCredentials);
            writer.SetParameters(parameters);
            MemoryStream memoryStream = new MemoryStream();
            writer.Save(memoryStream, format);

            // Download the generated export document to the client side.
            memoryStream.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(memoryStream, "application/" + type);
            fileStreamResult.FileDownloadName = fileName;
            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"");
            Response.ContentType = "application/pdf";
            return fileStreamResult;
        }
        catch (Exception ex)
        {

            return BadRequest(ex.InnerException);
        }
    }
    [HttpPost("exportProiect")]
    public IActionResult ExportConsumProiect([FromBody] ReportRequest reportRequest)
    {
        try
        {
            // Here, we have loaded the sales-order-detail sample report from application the folder wwwroot\Resources.
            //D:\Source\ErpNou\Arhimedes\Arhimedes.WebAPI\Resources\facturacutvav1.rdl



            string reportName = "ConsumProiecte.rdl";
            FileStream inputStream = new FileStream(@$".\Resources\{reportName}", FileMode.Open, FileAccess.Read);
            //FileStream inputStream = new FileStream(@".\Resources\facturacutvav1.rdl", FileMode.Open, FileAccess.Read);
            MemoryStream reportStream = new MemoryStream();
            inputStream.CopyTo(reportStream);
            reportStream.Position = 0;
            inputStream.Close();
            BoldReports.Writer.ReportWriter writer = new BoldReports.Writer.ReportWriter();
            writer.ReportErrorOccurred += Writer_ReportErrorOccurred;
            writer.ExportLayout = ExportLayout.Print;

            var parameters = new List<ReportParameter>()
            {
                new ReportParameter() { Name = "dindata", Values = new List<string>() { reportRequest.StartDate.ToString("yyyy-MM-dd")} },
                new ReportParameter() { Name = "ladata", Values = new List<string>() { reportRequest.EndDate.ToString("yyyy-MM-dd") } }
            };

            //foreach (var param in modelRaportare.ParametersValues)
            //{

            //    var parameter = new ReportParameter() { Name = param.Key, Values = new List<string>() { param.Value } };
            //    parameters.Add(parameter);
            //}
            //var parameter = new ReportParameter() { Name="DocumentId",Values=new List<string>() {DocumentId.ToString()},};


            var datasourceCredentials = new List<DataSourceCredentials>() {
            new DataSourceCredentials() { Name = "FAZDataSource", ConnectionString = $"Data Source=86.127.174.29,49000;Initial Catalog=ICAS;Encrypt=false;TrustServerCertificate=true;User Id=sa;Password=mogosa.2008", UserId = "sa", Password = "mogosa.2008", SecurityType = SecurityType.DataBase }};
            
            //foreach (var item in raport.DataSources)
            //{
            //    var ds = new DataSourceCredentials() { Name = item.Denumire, ConnectionString = $"Data Source={item.Server};Initial Catalog={userContext.DatabaseName};Encrypt=false;TrustServerCertificate=false;", UserId = item.UserId, Password = item.Password, SecurityType = SecurityType.DataBase };
            //    datasourceCredentials.Add(ds);
            //}
            //{
            //    new DataSourceCredentials(){ Name = "DSdocument", ConnectionString = $"Data Source=86.127.174.29,49000;Initial Catalog={userContext.DatabaseName};Encrypt=false;TrustServerCertificate=false;", UserId = "sa", Password = "mogosa.2008", SecurityType = SecurityType.DataBase }
            //};

            string fileName = null;
            WriterFormat format;
            string type = null;

            fileName = $"ConsumProiecte_{reportRequest.StartDate.ToString("dd-MM-yyyy")}_{reportRequest.EndDate.ToString("dd-MM-yyyy")}.pdf";
            type = "pdf";
            format = WriterFormat.PDF;

            writer.LoadReport(reportStream);
            writer.SetDataSourceCredentials(datasourceCredentials);
            writer.SetParameters(parameters);
            MemoryStream memoryStream = new MemoryStream();
            writer.Save(memoryStream, format);

            // Download the generated export document to the client side.
            memoryStream.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(memoryStream, "application/" + type);
            fileStreamResult.FileDownloadName = fileName;
            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"");
            Response.ContentType = "application/pdf";
            return fileStreamResult;
        }
        catch (Exception ex)
        {

            return BadRequest(ex.InnerException);
        }
    }

    //[HttpGet("export")]
    //public IActionResult Export(string WriterFormat)
    //{
    //    // Here, we have loaded the sales-order-detail sample report from application the folder wwwroot\Resources.
    //    //D:\Source\ErpNou\Arhimedes\Arhimedes.WebAPI\Resources\facturacutvav1.rdl
    //    var raport = usersDbContext.Reports.Where(x=>x.Id == 3).FirstOrDefault();
    //    if (raport == null)
    //    {
    //        return NotFound("Report not found.");
    //    }
    //    var sanitizedFileName = raport.Denumire.Replace(' ', '_');
    //    var rootPath = Directory.GetCurrentDirectory();
    //    var resourcesFolderPath = Path.Combine(rootPath, "Resources");
    //    string reportName = $"{sanitizedFileName}.rdl";
    //    var filePath = Path.Combine(resourcesFolderPath, reportName);
    //    var fileContent = raport.XML;
    //    System.IO.File.WriteAllText(filePath, fileContent);

    //    FileStream inputStream = new FileStream(@$".\Resources\{reportName}", FileMode.Open, FileAccess.Read);
    //    //FileStream inputStream = new FileStream(@".\Resources\facturacutvav1.rdl", FileMode.Open, FileAccess.Read);
    //    MemoryStream reportStream = new MemoryStream();
    //    inputStream.CopyTo(reportStream);
    //    reportStream.Position = 0;
    //    inputStream.Close();
    //    BoldReports.Writer.ReportWriter writer = new BoldReports.Writer.ReportWriter();
    //    writer.ReportErrorOccurred += Writer_ReportErrorOccurred;

    //    var parameter = new ReportParameter() { Name="DocumentId",Values=new List<string>() {DocumentId.ToString()},};
    //    var parameters = new List<ReportParameter>
    //    {
    //        parameter
    //    };


    //    var datasourceCredentials = new List<DataSourceCredentials>(){
    //        new DataSourceCredentials(){ Name = "DSdocument", ConnectionString = $"Data Source=86.127.174.29,49000;Initial Catalog={userContext.DatabaseName};Encrypt=false;TrustServerCertificate=false;", UserId = "sa", Password = "mogosa.2008", SecurityType = SecurityType.DataBase }
    //    };

    //    string fileName = null;
    //    WriterFormat format;
    //    string type = null;

    //    if (WriterFormat == "PDF")
    //    {
    //        fileName = "Factura.pdf";
    //        type = "pdf";
    //        format = WriterFormat.PDF;
    //    }
    //    else if (WriterFormat == "Word")
    //    {
    //        fileName = "Factura.doc";
    //        type = "doc";
    //        format = WriterFormat.Word;
    //    }
    //    else if (WriterFormat == "CSV")
    //    {
    //        fileName = "Factura.csv";
    //        type = "csv";
    //        format = WriterFormat.CSV;
    //    }
    //    else
    //    {
    //        fileName = "Factura.xls";
    //        type = "xls";
    //        format = WriterFormat.Excel;
    //    }


    //        writer.LoadReport(reportStream);
    //        writer.SetDataSourceCredentials(datasourceCredentials);
    //        writer.SetParameters(parameters);
    //        MemoryStream memoryStream = new MemoryStream();
    //        writer.Save(memoryStream, format);

    //        // Download the generated export document to the client side.
    //        memoryStream.Position = 0;
    //        FileStreamResult fileStreamResult = new FileStreamResult(memoryStream, "application/" + type);
    //        fileStreamResult.FileDownloadName = fileName;
    //        return fileStreamResult;


    //}

    private void Writer_ReportErrorOccurred(object sender, ReportErrorOccurredEventArgs e)
    {
        WriteLogs(string.Format("Class Name: {0} \n Method Name: {1} \n Error Message: {2}", e.ClassName, e.MethodName, e.Message));
    }
    internal void WriteLogs(string errorMessage)
    {
        string filePath = Path.Combine(@"D:\Source\ArhiParcurs\ArhiParcurs\ArhiParcurs\Resources\", "ErrorDetails.txt");
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.AutoFlush = true;
            writer.WriteLine(errorMessage);
        }
    }
}
