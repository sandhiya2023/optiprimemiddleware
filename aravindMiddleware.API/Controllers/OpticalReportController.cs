using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using aravindMiddleware.Portal;
using aravindMiddleware.API.Services;
using aravindMiddleware.Data.DapperClasses;
using aravindMiddleware.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using FastReport;
using FastReport.Export;
using FastReport.Export.Html;
using FastReport.Export.PdfSimple;
using System.Data;


namespace aravindMiddleware.API
{
    [Authorize]
    [ApiController]
    [Route("api/v1/Report")]
    public class OpticalReportController : ControllerBase
    {
        OpticalReportPortal objPortal;
        string CLASS_NAME = "OpticalReportController";
        private readonly IConfiguration Configuration;
        private IUserServices _userService = new UserServices();
        string reportTemplatePath, reportOutputPath, reportSave;

        public OpticalReportController(IConfiguration configuration)
        {
            Configuration = configuration;
            reportTemplatePath = Configuration["KeySettings:ReportTemplatePath"];
            reportOutputPath = Configuration["KeySettings:ReportOutputPath"];
            reportSave = Configuration["KeySettings:ReportSave"];
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("MISSupplierReport")]
        public IActionResult MISSupplierReport([FromBody] MISSupplierReportInput objInput)
        {
            string METHOD_NAME = "MISSupplierReport";
            string ReportHead = string.Empty;
            string ReportFormat = string.Empty;
            try
            {

                //string mime = "application/pdf"; // MIME header with default value
                //string mime = "application/html"; // MIME header with default value
                string mime = "";
                string reportPath = ""; // determine the path to the report
                string outFolder = "";
                string outRptPath = "";
                string filenamepath = "";
                string filetype = "";
                //string filetype = ".pdf";
                //string filetype = ".html";
                var randomGenerator = new Random();
                var random1 = randomGenerator.Next(1000, 1000000000);

                objPortal = new OpticalReportPortal();
                DataSet DSSupplierOrder = objPortal.MISSupplierreport(objInput);
                ReportHead = objInput.ReportHead.ToString().ToUpper();
                ReportFormat = objInput.ReportFormat.ToString().ToUpper();

                //filenamepath = objInput.OrgId + "_" + objInput.LocationId + "_" + objInput.CategoryId + "_" + objInput.SupplierId + "_" + objInput.fromdate + "_" + objInput.todate + "_" + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + "_" + DateTime.Now.Millisecond + filetype;
                filenamepath = random1 + filetype;

                if (ReportHead == "SUPPLIERORDERSUMMARY")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierOrderSummary";
                    DSSupplierOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierOrderSummary.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierOrderSummary_" + filenamepath;

                }
                if (ReportHead == "SUPPLIERORDERITEMWISE")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierOrderItemwise";
                    DSSupplierOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierOrderItemwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierOrderItemwise_" + filenamepath;

                }
                if (ReportHead == "SUPPLIERINWARDSUMMARY")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierInwardSummary";
                    DSSupplierOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierInwardSummary.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierInwardSummary_" + filenamepath;

                }
                if (ReportHead == "SUPPLIERINWARDITEMWISE")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierInwardItemwise";
                    DSSupplierOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierInwardItemwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierInwardItemwise_" + filenamepath;

                }
                if (ReportHead == "SUPPLIERRETURNSUMMARY")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierReturnSummary";
                    DSSupplierOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierReturnSummary.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierReturnSummary_" + filenamepath;

                }
                if (ReportHead == "SUPPLIERRETURNITEMWISE")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierReturnItemwise";
                    DSSupplierOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierReturnItemwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierReturnItemwise_" + filenamepath;

                }

                if (ReportHead == "SUPPLIERINWARDPROMOTIONALSUMMARY")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierInwardSummary";
                    DSSupplierOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierInwardSummary.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierInwardSummary_" + filenamepath;

                }
                if (ReportHead == "SUPPLIERINWARDPROMOTIONALITEMWISE")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierInwardItemwise";
                    DSSupplierOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierInwardItemwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierInwardItemwise_" + filenamepath;

                }
                if (ReportHead == "TRANSFERINSUMMARY")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierTransferInSummary";
                    DSSupplierOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierTransferInSummary.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierTransferInSummary_" + filenamepath;

                }
                if (ReportHead == "TRANSFERINITEMWISE")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierTransferInitemwise";
                    DSSupplierOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierTransferInItemwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierTransferInItemwise_" + filenamepath;

                }
                if (ReportHead == "SUPPLIERORDERCANCEL")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierOrderCancellation";
                    DSSupplierOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierOrderCancellation.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierOrderCancellation_" + filenamepath;

                }
                if (ReportHead == "SUPPLIERCREDITNOTES")
                {
                    DSSupplierOrder.Tables[0].TableName = "RptSupplierCreditNotes";
                    DSSupplierOrder.Tables[1].TableName = "RptSummary";
                    DSSupplierOrder.Tables[2].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/suppliers/" + "RptSupplierCreditNotes.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSupplierCreditNotes_" + filenamepath;

                }

                MemoryStream stream = new MemoryStream();

                String fileName1 = outRptPath;
                if (System.IO.File.Exists(fileName1) == true)
                {
                    System.IO.File.Delete(fileName1);
                }

                Report report = new Report();
                report.Load(reportPath); // Download the report
                report.RegisterData(DSSupplierOrder);
                report.Prepare(); //Prepare the report

                if (ReportFormat == "PDF")
                {
                    //pdf export
                    PDFSimpleExport pdfExport = new PDFSimpleExport();
                    pdfExport.Export(report, outRptPath);
                    report.Dispose();
                }
                if (ReportFormat == "HTML")
                {
                    //html  Format 
                    HTMLExport html = new HTMLExport();
                    html.EmbedPictures = true;
                    html.SinglePage = true;
                    html.SubFolder = false;
                    html.Layers = true;
                    html.Navigator = false;
                    html.SaveStreams = true;
                    html.Export(report, outRptPath);
                }

                String fileName = outRptPath;
                var fileBytes = System.IO.File.ReadAllBytes(fileName);
                System.IO.File.Delete(fileName);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request. MIS Supplier Report - " + ReportHead);
                return File(fileBytes, "application/octet-stream");

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Error while generating - " + ReportHead + "- Error : " + ex.Message);
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - MIS Supplier Report - " + ReportHead + " - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("MISReceiptPrint")]
        public IActionResult MISReceiptPrint([FromBody] MISReceiptInput objInput)
        {
            string METHOD_NAME = "MISReceiptPrint";
            string ReportHead = string.Empty;
            try
            {


                string mime = "application/pdf"; // MIME header with default value                
                //string mime = "application/html"; // MIME header with default value                
                string reportPath = ""; // determine the path to the report
                string outFolder = "";
                string outRptPath = "";
                string filetype = ".pdf";
                //string filetype = ".html";


                objPortal = new OpticalReportPortal();
                DataSet DSSupplierOrder = objPortal.MISReceiptPrint(objInput);
                ReportHead = objInput.ReportHead.ToString().ToUpper();

                if (ReportHead == "SUPPLIERORDERPRINT")
                {
                    DSSupplierOrder.Tables[0].TableName = "PrtSupplierOrderEntry";
                    DSSupplierOrder.Tables[1].TableName = "RptItemDetails";
                    reportPath = reportTemplatePath + objInput.OrgId + "/ReceiptPrint/" + "PrtSupplierOrderEntry.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "PrtSupplierOrderEntry_" + objInput.OrgId + "_" + objInput.LocationId + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + "_" + DateTime.Now.Millisecond + filetype;


                }
                if (ReportHead == "SUPPLIERINWARDPRINT")
                {
                    DSSupplierOrder.Tables[0].TableName = "PrtSupplierInwardEntry";
                    DSSupplierOrder.Tables[1].TableName = "RptItemDetails";
                    reportPath = reportTemplatePath + objInput.OrgId + "/ReceiptPrint/" + "PrtSupplierInwardEntry.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "PrtSupplierInwardEntry_" + objInput.OrgId + "_" + objInput.LocationId + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + filetype;

                }
                if (ReportHead == "SUPPLIERRETURNPRINT")
                {
                    DSSupplierOrder.Tables[0].TableName = "PrtSupplierReturnEntry";
                    DSSupplierOrder.Tables[1].TableName = "RptItemDetails";
                    reportPath = reportTemplatePath + objInput.OrgId + "/ReceiptPrint/" + "PrtSupplierReturnEntry.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "PrtSupplierReturnEntry_" + objInput.OrgId + "_" + objInput.LocationId + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + filetype;

                }

                if (ReportHead == "STOCKTRANSFERPRINT")
                {
                    DSSupplierOrder.Tables[0].TableName = "PrtStoresStockTransferEntry";
                    reportPath = reportTemplatePath + objInput.OrgId + "/ReceiptPrint/" + "PrtStoresStockTransferEntry.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "PrtStoresStockTransferEntry_" + objInput.OrgId + "_" + objInput.LocationId + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + filetype;

                }

                if (ReportHead == "STOCKRETURNPRINT")
                {
                    DSSupplierOrder.Tables[0].TableName = "PrtStoresStockReturnEntry";
                    reportPath = reportTemplatePath + objInput.OrgId + "/ReceiptPrint/" + "PrtStoresStockReturnEntry.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "PrtStoresStockReturnEntry_" + objInput.OrgId + "_" + objInput.LocationId + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + filetype;

                }
                if (ReportHead == "ORDERENTRYPRINT")
                {
                    DSSupplierOrder.Tables[0].TableName = "PrtOrderEntry";
                    reportPath = reportTemplatePath + objInput.OrgId + "/ReceiptPrint/" + "PrtOrderEntry.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "PrtOrderEntry_" + objInput.OrgId + "_" + objInput.LocationId + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + filetype;

                }
                if (ReportHead == "BILLENTRYPRINT")
                {
                    DSSupplierOrder.Tables[0].TableName = "PrtBillEntry";
                    DSSupplierOrder.Tables[1].TableName = "RptItemDetails";
                    reportPath = reportTemplatePath + objInput.OrgId + "/ReceiptPrint/" + "PrtBillEntry.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "PrtBillEntry_" + objInput.OrgId + "_" + objInput.LocationId + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + filetype;


                }

                MemoryStream stream = new MemoryStream();

                String fileName1 = outRptPath;
                if (System.IO.File.Exists(fileName1) == true)
                {
                    System.IO.File.Delete(fileName1);
                }

                Report report = new Report();
                report.Load(reportPath); // Download the report
                report.RegisterData(DSSupplierOrder);
                report.Prepare(); //Prepare the report

                //pdf format 
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.Export(report, outRptPath);
                report.Dispose();

                //html  Format 
                //HTMLExport html = new HTMLExport();
                //html.EmbedPictures = true;
                //html.SinglePage = true;
                //html.SubFolder = false;
                //html.Layers = true;
                //html.Navigator = false;
                //html.SaveStreams = true;
                //html.Export(report, outRptPath);

                String fileName = outRptPath;
                var fileBytes = System.IO.File.ReadAllBytes(fileName);
                System.IO.File.Delete(fileName);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request. MIS receipt print - " + ReportHead);
                return File(fileBytes, "application/octet-stream");


            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Error while generating - " + ReportHead + "- Error : " + ex.Message);
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - MIS receipt print - " + ReportHead + " - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("MISStoresReport")]
        public IActionResult MISStoresReport([FromBody] MISStoresReportInput objInput)
        {
            string METHOD_NAME = "MISStoresReport";
            string ReportHead = string.Empty;
            string ReportFormat = string.Empty;
            try
            {


                //string mime = "application/pdf"; // MIME header with default value
                //string mime = "application/html"; // MIME header with default value
                string mime = "";                               // 
                string reportPath = ""; // determine the path to the report
                string outFolder = "";
                string outRptPath = "";
                string filenamepath = "";
                string filetype = "";
                //string filetype = ".pdf";
                //string filetype = ".html";
                //string filetype = ".csv";
                var randomGenerator = new Random();
                var random1 = randomGenerator.Next(1000, 1000000000);

                objPortal = new OpticalReportPortal();
                DataSet DSStoresOrder = objPortal.MISStoresreport(objInput);
                ReportHead = objInput.ReportHead.ToString().ToUpper();
                ReportFormat = objInput.ReportFormat.ToString().ToUpper();

                //filenamepath = objInput.OrgId + "_" + objInput.LocationId + "_" + objInput.CategoryId + "_" + objInput.SupplierId + "_" + objInput.fromdate + "_" + objInput.todate + "_" + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + "_" + DateTime.Now.Millisecond + filetype;
                filenamepath = random1 + filetype;


                if (ReportHead == "STOCKTRANSFERSUMMARY")
                {
                    DSStoresOrder.Tables[0].TableName = "RptStoresStockTransferSummary";
                    DSStoresOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stores/" + "RptStoresStockTransferSummary.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresStockTransferSummary_" + filenamepath;

                }
                if (ReportHead == "STOCKTRANSFERITEMWISE")
                {
                    DSStoresOrder.Tables[0].TableName = "RptStoresStockTransferItemwise";
                    DSStoresOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stores/" + "RptStoresStockTransferItemwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresStockTransferItemwise_" + filenamepath;

                }
                if (ReportHead == "STOCKRETURNSUMMARY")
                {
                    DSStoresOrder.Tables[0].TableName = "RptStoresStockReturnSummary";
                    DSStoresOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stores/" + "RptStoresStockReturnSummary.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresStockReturnSummary_" + filenamepath;

                }
                if (ReportHead == "STOCKRETURNITEMWISE")
                {
                    DSStoresOrder.Tables[0].TableName = "RptStoresStockReturnItemwise";
                    DSStoresOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stores/" + "RptStoresStockReturnItemwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresStockReturnItemwise_" + filenamepath;

                }
                if (ReportHead == "TRANSFEROUTSUMMARY")
                {
                    DSStoresOrder.Tables[0].TableName = "RptStoresTransferOutSummary";
                    DSStoresOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stores/" + "RptStoresTransferOutSummary.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresTransferOutSummary_" + filenamepath;

                }
                if (ReportHead == "TRANSFEROUTITEMWISE")
                {
                    DSStoresOrder.Tables[0].TableName = "RptStoresTransferOutItemwise";
                    DSStoresOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stores/" + "RptStoresTransferOutItemwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresTransferOutItemwise_" + filenamepath;

                }
                if (ReportHead == "STOCKINTRANSIT")
                {
                    DSStoresOrder.Tables[0].TableName = "RptStoresStocktransit";
                    DSStoresOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stores/" + "RptStoresStocktransit.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresStocktransit_" + filenamepath;

                }
                MemoryStream stream = new MemoryStream();

                String fileName1 = outRptPath;
                if (System.IO.File.Exists(fileName1) == true)
                {
                    System.IO.File.Delete(fileName1);
                }

                Report report = new Report();
                report.Load(reportPath); // Download the report
                report.RegisterData(DSStoresOrder);
                report.Prepare(); //Prepare the report

                if (ReportFormat == "PDF")
                {
                    //pdf export
                    PDFSimpleExport pdfExport = new PDFSimpleExport();
                    pdfExport.Export(report, outRptPath);
                    report.Dispose();
                }
                if (ReportFormat == "HTML")
                {
                    //html  Format 
                    HTMLExport html = new HTMLExport();
                    html.EmbedPictures = true;
                    html.SinglePage = true;
                    html.SubFolder = false;
                    html.Layers = true;
                    html.Navigator = false;
                    html.SaveStreams = true;
                    html.Export(report, outRptPath);

                }
                String fileName = outRptPath;
                var fileBytes = System.IO.File.ReadAllBytes(fileName);
                System.IO.File.Delete(fileName);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request. MIS Stores Report - " + ReportHead);
                return File(fileBytes, "application/octet-stream");
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Error while generating - " + ReportHead + "- Error : " + ex.Message);
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - MIS Stores Report - " + ReportHead + " - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("MISStockReport")]
        public IActionResult MISStockReport([FromBody] MISStockReportInput objInput)
        {
            string METHOD_NAME = "MISStockReport";
            string ReportHead = string.Empty;
            string ReportFormat = string.Empty;
            try
            {

                //string mime = "application/pdf"; // MIME header with default value
                //string mime = "application/html"; // MIME header with default value
                string mime = "";
                string reportPath = ""; // determine the path to the report
                string outFolder = "";
                string outRptPath = "";
                string filenamepath = "";
                string filetype = "";
                //string filetype = ".pdf";
                //string filetype = ".html";
                //string filetype = ".csv";
                var randomGenerator = new Random();
                var random1 = randomGenerator.Next(1000, 1000000000);

                objPortal = new OpticalReportPortal();
                DataSet DSStockOrder = objPortal.MISStockreport(objInput);
                ReportHead = objInput.ReportHead.ToString().ToUpper();
                ReportFormat = objInput.ReportFormat.ToString().ToUpper();

                //filenamepath = objInput.OrgId + "_" + objInput.LocationId + "_" + objInput.CategoryId + "_" + objInput.SupplierId + "_" + objInput.fromdate + "_" + objInput.todate + "_" + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + "_" + DateTime.Now.Millisecond + filetype;
                filenamepath = random1 + filetype;

                if (ReportHead == "STORESSTOCKMOVEMENTREPORT")
                {
                    DSStockOrder.Tables[0].TableName = "RptStoresStockMovement";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptStoresStockMovement.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresStockMovement_" + filenamepath;

                }
                if (ReportHead == "STORESSTOCKMOVEMENTREPORTBATCHWISE")
                {
                    DSStockOrder.Tables[0].TableName = "RptStoresStockMovementBatchwise";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptStoresStockMovementBatchwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresStockMovementBatchwise_" + filenamepath;

                }

                if (ReportHead == "STORESCURRENTSTOCKMOVEMENTREPORT")
                {
                    DSStockOrder.Tables[0].TableName = "RptStoresCurrentStockMovement";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptStoresCurrentStockMovement.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresCurrentStockMovement_" + filenamepath;

                }
                if (ReportHead == "STORESCURRENTSTOCKMOVEMENTREPORTBATCHWISE")
                {
                    DSStockOrder.Tables[0].TableName = "RptStoresCurrentStockMovementBatchwise";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptStoresCurrentStockMovementBatchwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresCurrentStockMovementBatchwise_" + filenamepath;

                }
                if (ReportHead == "STORESCOSTCOMREPORT")
                {
                    DSStockOrder.Tables[0].TableName = "RptStoresStockCostcom";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptStoresStockCostcom.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresStockCostcom_" + filenamepath;

                }
                if (ReportHead == "STORESEXPIRYREPORT")
                {
                    DSStockOrder.Tables[0].TableName = "RptStoresStockExpiry";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptStoresStockExpiry.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresStockExpiry_" + filenamepath;

                }
                if (ReportHead == "SECTIONSTOCKMOVEMENTREPORT")
                {
                    DSStockOrder.Tables[0].TableName = "RptSectionStockMovement";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptSectionStockMovement.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSectionStockMovement_" + filenamepath;

                }
                if (ReportHead == "SECTIONSTOCKMOVEMENTREPORTBATCHWISE")
                {
                    DSStockOrder.Tables[0].TableName = "RptSectionStockMovementBatchwise";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptSectionStockMovementBatchwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSectionStockMovementBatchwise_" + filenamepath;

                }

                if (ReportHead == "SECTIONCURRENTSTOCKMOVEMENTREPORT")
                {
                    DSStockOrder.Tables[0].TableName = "RptSectionCurrentStockMovement";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptSectionCurrentStockMovement.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSectionCurrentStockMovement_" + filenamepath;

                }
                if (ReportHead == "SECTIONCURRENTSTOCKMOVEMENTREPORTBATCHWISE")
                {
                    DSStockOrder.Tables[0].TableName = "RptSectionCurrentStockMovementBatchwise";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptSectionCurrentStockMovementBatchwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSectionCurrentStockMovementBatchwise_" + filenamepath;

                }
                if (ReportHead == "SECTIONCOSTCOMREPORT")
                {
                    DSStockOrder.Tables[0].TableName = "RptSectionStockCostcom";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptSectionStockCostcom.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSectionStockCostcom_" + filenamepath;

                }
                if (ReportHead == "SECTIONEXPIRYREPORT")
                {
                    DSStockOrder.Tables[0].TableName = "RptSectionStockExpiry";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptSectionStockExpiry.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptSectionStockExpiry_" + filenamepath;

                }
                if (ReportHead == "ALLSTOCKMOVEMENTREPORT")
                {
                    DSStockOrder.Tables[0].TableName = "RptStoresAllStockMovement";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptStoresAllStockMovement.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresAllStockMovement_" + filenamepath;

                }
                if (ReportHead == "ALLSTOCKMOVEMENTREPORTBATCHWISE")
                {
                    DSStockOrder.Tables[0].TableName = "RptStoresAllStockMovementBatchwise";
                    DSStockOrder.Tables[1].TableName = "RptHeader";
                    DSStockOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Stock/" + "RptStoresAllStockMovementBatchwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptStoresAllStockMovementBatchwise_" + filenamepath;

                }

                MemoryStream stream = new MemoryStream();

                String fileName1 = outRptPath;
                if (System.IO.File.Exists(fileName1) == true)
                {
                    System.IO.File.Delete(fileName1);
                }

                Report report = new Report();
                report.Load(reportPath); // Download the report
                report.RegisterData(DSStockOrder);
                report.Prepare(); //Prepare the report

                if (ReportFormat == "PDF")
                {
                    //pdf export
                    PDFSimpleExport pdfExport = new PDFSimpleExport();
                    pdfExport.Export(report, outRptPath);
                    report.Dispose();
                }
                if (ReportFormat == "HTML")
                {
                    //html  Format 
                    HTMLExport html = new HTMLExport();
                    html.EmbedPictures = true;
                    html.SinglePage = true;
                    html.SubFolder = false;
                    html.Layers = true;
                    html.Navigator = false;
                    html.SaveStreams = true;
                    html.Export(report, outRptPath);
                }
                String fileName = outRptPath;
                var fileBytes = System.IO.File.ReadAllBytes(fileName);
                System.IO.File.Delete(fileName);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request. MIS Stock Report - " + ReportHead);
                return File(fileBytes, "application/octet-stream");
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Error while generating - " + ReportHead + "- Error : " + ex.Message);
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - MIS Stock Report - " + ReportHead + " - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("MISCustomerReport")]
        public IActionResult MISCustomerReport([FromBody] MISCustomerReportInput objInput)
        {
            string METHOD_NAME = "MISCustomerReport";
            string ReportHead = string.Empty;
            string ReportFormat = string.Empty;
            try
            {

                //string mime = "application/pdf"; // MIME header with default value
                // string mime = "application/html"; // MIME header with default value                
                string mime = "";
                string reportPath = ""; // determine the path to the report
                string outFolder = "";
                string outRptPath = "";
                string filenamepath = "";
                string filetype = "";
                //string filetype = ".pdf";
                //string filetype = ".html";
                var randomGenerator = new Random();
                var random1 = randomGenerator.Next(1000, 1000000000);
                //string filetype = ".csv";

                objPortal = new OpticalReportPortal();
                DataSet DSCustomerOrder = objPortal.MISCustomerreport(objInput);
                ReportHead = objInput.ReportHead.ToString().ToUpper();
                ReportFormat = objInput.ReportFormat.ToString().ToUpper();

                //filenamepath = objInput.OrgId + "_" + objInput.LocationId + "_" + objInput.SectionId + "_" + objInput.PaymentId + "_" + objInput.fromdate + "_" + objInput.todate + "_" + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + "_" + DateTime.Now.Millisecond + filetype;
                filenamepath = random1 + filetype;

                if (ReportHead == "ORDERCOLLECTIONREPORT")
                {
                    DSCustomerOrder.Tables[0].TableName = "RptOpticalOrderCollection";
                    DSCustomerOrder.Tables[1].TableName = "RptHeader";
                    DSCustomerOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Customers/" + "RptOpticalOrderCollection.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptOpticalOrderCollection_" + filenamepath;

                }
                if (ReportHead == "DELIVERYCOLLECTIONREPORT")
                {
                    DSCustomerOrder.Tables[0].TableName = "RptOpticalDeliveryCollection";
                    DSCustomerOrder.Tables[1].TableName = "RptHeader";
                    DSCustomerOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Customers/" + "RptOpticalDeliveryCollection.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptOpticalDeliveryCollection_" + filenamepath;
                }
                if (ReportHead == "DIRECTSALECOLLECTIONREPORT")
                {
                    DSCustomerOrder.Tables[0].TableName = "RptOpticalDirectsaleCollection";
                    DSCustomerOrder.Tables[1].TableName = "RptHeader";
                    DSCustomerOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Customers/" + "RptOpticalDirectsaleCollection.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptOpticalDirectsaleCollection_" + filenamepath;
                }
                if ((ReportHead == "ORDERCANCELLATIONREPORT") || (ReportHead == "BILLCANCELLATIONREPORT"))
                {
                    DSCustomerOrder.Tables[0].TableName = "RptOpticalOrderCancellation";
                    DSCustomerOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Customers/" + "RptOpticalOrderCancellation.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptOpticalOrderCancellation_" + filenamepath;
                }
                if (ReportHead == "ORDERDISCOUNTREPORT")
                {
                    DSCustomerOrder.Tables[0].TableName = "RptOpticalOrderDiscount";
                    DSCustomerOrder.Tables[1].TableName = "RptHeader";
                    DSCustomerOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Customers/" + "RptOpticalOrderDiscount.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptOpticalOrderDiscount_" + filenamepath;
                }
                if (ReportHead == "STOCKDEDUCTIONREPORT")
                {
                    DSCustomerOrder.Tables[0].TableName = "RptOpticalStockDeduction";
                    DSCustomerOrder.Tables[1].TableName = "RptHeader";
                    DSCustomerOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Customers/" + "RptOpticalStockDeduction.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptOpticalStockDeduction_" + filenamepath;
                }
                if (ReportHead == "ORDERDETAILSREPORT")
                {
                    DSCustomerOrder.Tables[0].TableName = "RptOpticalOrderDetails";
                    DSCustomerOrder.Tables[1].TableName = "RptHeader";
                    DSCustomerOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Customers/" + "RptOpticalOrderDetails.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptOpticalOrderDetails_" + filenamepath;
                }
                if (ReportHead == "DIRECTORDERDETAILSREPORT")
                {
                    DSCustomerOrder.Tables[0].TableName = "RptOpticalDirectOrderDetails";
                    DSCustomerOrder.Tables[1].TableName = "RptHeader";
                    DSCustomerOrder.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Customers/" + "RptOpticalDirectOrderDetails.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptOpticalDirectOrderDetails_" + filenamepath;
                }
                if (ReportHead == "DELIVERYDETAILSREPORT")
                {
                    DSCustomerOrder.Tables[0].TableName = "RptOpticalDeliveryDetails";
                    DSCustomerOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Customers/" + "RptOpticalDeliveryDetails.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptOpticalDeliveryDetails_" + filenamepath;
                }
                if (ReportHead == "UNDELIVERYREPORT")
                {
                    DSCustomerOrder.Tables[0].TableName = "RptOpticalUndeliveryDetails";
                    DSCustomerOrder.Tables[1].TableName = "RptHeader";
                    DSCustomerOrder.Tables[2].TableName = "Rptsummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Customers/" + "RptOpticalUndeliveryDetails.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptOpticalUndeliveryDetails_" + filenamepath;
                }
                if (ReportHead == "SALESPERSONREPORT")
                {
                    DSCustomerOrder.Tables[0].TableName = "Rptsummary";
                    DSCustomerOrder.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Customers/" + "RptOpticalSalesperson.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptOpticalSalesperson_" + filenamepath;
                }
                String fileName1 = outRptPath;
                if (System.IO.File.Exists(fileName1) == true)
                {
                    System.IO.File.Delete(fileName1);
                }

                MemoryStream stream = new MemoryStream();

                Report report = new Report();
                report.Load(reportPath); // Download the report
                report.RegisterData(DSCustomerOrder);
                report.Prepare(); //Prepare the report

                if (ReportFormat == "PDF")
                {
                    //pdf export
                    PDFSimpleExport pdfExport = new PDFSimpleExport();
                    pdfExport.Export(report, outRptPath);
                    report.Dispose();
                }
                if (ReportFormat == "HTML")
                {

                    //html  Format 
                    HTMLExport html = new HTMLExport();
                    html.EmbedPictures = true;
                    html.SinglePage = true;
                    html.SubFolder = false;
                    html.Layers = true;
                    html.Navigator = false;
                    html.SaveStreams = true;
                    html.Export(report, outRptPath);

                }
                String fileName = outRptPath;
                var fileBytes = System.IO.File.ReadAllBytes(fileName);
                System.IO.File.Delete(fileName);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request. MIS Customers Report - " + ReportHead);
                return File(fileBytes, "application/octet-stream");
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Error while generating - " + ReportHead + "- Error : " + ex.Message);
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - MIS Customers Report - " + ReportHead + " - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("MISTrackingReport")]
        public IActionResult MISTrackingReport([FromBody] MISTrackingReportInput objInput)
        {
            string METHOD_NAME = "MISTrackingReport";
            string ReportHead = string.Empty;
            string ReportFormat = string.Empty;
            try
            {

                //string mime = "application/pdf"; // MIME header with default value
                //string mime = "application/html"; // MIME header with default value
                string mime = "";                                  // 
                string reportPath = ""; // determine the path to the report
                string outFolder = "";
                string outRptPath = "";
                string filenamepath = "";
                string filetype = "";
                //string filetype = ".pdf";
                //string filetype = ".html";
                var randomGenerator = new Random();
                var random1 = randomGenerator.Next(1000, 1000000000);
                //string filetype = ".csv";

                objPortal = new OpticalReportPortal();
                DataSet DSOrderTracking = objPortal.MISTrackingReport(objInput);
                ReportHead = objInput.ReportHead.ToString().ToUpper();
                ReportFormat = objInput.ReportFormat.ToString().ToUpper();

                //filenamepath = objInput.OrgId + "_" + objInput.LocationId + "_" + objInput.SectionId + "_" + objInput.fromdate + "_" + objInput.todate + "_" + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + "_" + DateTime.Now.Millisecond + filetype;
                filenamepath = random1 + filetype;

                if (ReportHead == "ORDERTRACKINGREPORT")
                {
                    DSOrderTracking.Tables[0].TableName = "RptTracking";
                    DSOrderTracking.Tables[1].TableName = "RptHeader";
                    DSOrderTracking.Tables[2].TableName = "RptSummary";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Tracking/" + "RptTracking.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptTracking_" + filenamepath;

                }
                if (ReportHead == "ORDERTRACKINGONTIMEREPORT")
                {
                    DSOrderTracking.Tables[0].TableName = "RptTrackingOntime";
                    DSOrderTracking.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Tracking/" + "RptTrackingOntime.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptTrackingOntime_" + filenamepath;

                }
                if (ReportHead == "HOURLYREPORT")
                {
                    DSOrderTracking.Tables[0].TableName = "RptHourly";
                    DSOrderTracking.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Tracking/" + "RptHourly.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptHourly_" + filenamepath;

                }

                String fileName1 = outRptPath;
                if (System.IO.File.Exists(fileName1) == true)
                {
                    System.IO.File.Delete(fileName1);
                }

                MemoryStream stream = new MemoryStream();


                Report reportTracking = new Report();
                reportTracking.Load(reportPath); // Download the report
                reportTracking.RegisterData(DSOrderTracking);
                reportTracking.Prepare(); //Prepare the report


                if (ReportFormat == "PDF")
                {
                    //pdf format
                    PDFSimpleExport pdfExport = new PDFSimpleExport();
                    pdfExport.Export(reportTracking, outRptPath);
                    reportTracking.Dispose();
                }
                if (ReportFormat == "HTML")
                {
                    //html  Format 
                    HTMLExport html = new HTMLExport();
                    html.EmbedPictures = true;
                    html.SinglePage = true;
                    html.SubFolder = false;
                    html.Layers = true;
                    html.Navigator = false;
                    html.SaveStreams = true;
                    html.Export(reportTracking, outRptPath);
                }
                String fileName = outRptPath;
                var fileBytes = System.IO.File.ReadAllBytes(fileName);
                System.IO.File.Delete(fileName);

                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request. MIS Tracking Report - " + ReportHead);
                return File(fileBytes, "application/octet-stream");
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Error while generating - " + ReportHead + "- Error : " + ex.Message);
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - MIS Tracking Report - " + ReportHead + " - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("MISAccountReport")]
        public IActionResult MISAccountReport([FromBody] MISAccountReportInput objInput)
        {
            string METHOD_NAME = "MISAccountReport";
            string ReportHead = string.Empty;
            string ReportFormat = string.Empty;
            try
            {


                //string mime = "application/pdf"; // MIME header with default value
                //string mime = "application/html"; // MIME header with default value     
                string mime = "";
                string reportPath = ""; // determine the path to the report
                string outFolder = "";
                string outRptPath = "";
                string filenamepath = "";
                string filetype = "";
                //string filetype = ".pdf";
                //string filetype = ".html";
                var randomGenerator = new Random();
                var random1 = randomGenerator.Next(1000, 1000000000);
                //string filetype = ".csv";

                objPortal = new OpticalReportPortal();
                DataSet DSAccountReport = objPortal.MISAccountReport(objInput);
                ReportHead = objInput.ReportHead.ToString().ToUpper();
                ReportFormat = objInput.ReportFormat.ToString().ToUpper();

                //filenamepath = objInput.OrgId + "_" + objInput.LocationId + "_" + objInput.SectionId + "_" + objInput.fromdate + "_" + objInput.todate + "_" + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_") + "_" + DateTime.Now.Millisecond + filetype;
                filenamepath = random1 + filetype;

                if (ReportHead == "HSNWISEREPORT")
                {
                    DSAccountReport.Tables[0].TableName = "RptHsnwise";
                    DSAccountReport.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Account/" + "RptHsnwise.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptHsnwise_" + filenamepath;

                }
                if (ReportHead == "POSTALREPORT")
                {
                    DSAccountReport.Tables[0].TableName = "RptPostal";
                    DSAccountReport.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Account/" + "RptPostal.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptPostal_" + filenamepath;

                }
                if (ReportHead == "CASHFLOWREPORT")
                {
                    DSAccountReport.Tables[0].TableName = "RptCashFlow";
                    DSAccountReport.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Account/" + "RptCashFlow.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptCashFlow_" + filenamepath;

                }
                if (ReportHead == "AUDITREPORT")
                {
                    DSAccountReport.Tables[0].TableName = "RptAudit";
                    DSAccountReport.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Account/" + "RptAudit.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptAudit_" + filenamepath;

                }
                if (ReportHead == "TALLYSALESSUMMARYREPORT")
                {
                    DSAccountReport.Tables[0].TableName = "RptTallySalesSummary";
                    DSAccountReport.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Account/" + "RptTallySalesSummary.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptTallySalesSummary_" + filenamepath;

                }
                if (ReportHead == "TALLYPURCHASESUMMARYREPORT")
                {
                    DSAccountReport.Tables[0].TableName = "RptTallyPurchaseSummary";
                    DSAccountReport.Tables[1].TableName = "RptHeader";
                    reportPath = reportTemplatePath + objInput.OrgId + "/Account/" + "RptTallyPurchaseSummary.frx"; // determine the path to the report
                    outFolder = reportOutputPath;
                    outRptPath = outFolder + "RptTallyPurchaseSummary_" + filenamepath;

                }
                String fileName1 = outRptPath;
                if (System.IO.File.Exists(fileName1) == true)
                {
                    System.IO.File.Delete(fileName1);
                }

                MemoryStream stream = new MemoryStream();


                Report reportAccount = new Report();
                reportAccount.Load(reportPath); // Download the report
                reportAccount.RegisterData(DSAccountReport);
                reportAccount.Prepare(); //Prepare the report


                if (ReportFormat == "PDF")
                {
                    //pdf format
                    PDFSimpleExport pdfExport = new PDFSimpleExport();
                    pdfExport.Export(reportAccount, outRptPath);
                    reportAccount.Dispose();
                }
                if (ReportFormat == "HTML")
                {
                    //html  Format 
                    HTMLExport html = new HTMLExport();
                    html.EmbedPictures = true;
                    html.SinglePage = true;
                    html.SubFolder = false;
                    html.Layers = true;
                    html.Navigator = false;
                    html.SaveStreams = true;
                    html.Export(reportAccount, outRptPath);
                }

                String fileName = outRptPath;
                var fileBytes = System.IO.File.ReadAllBytes(fileName);
                System.IO.File.Delete(fileName);

                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request. MIS Account Report - " + ReportHead);
                return File(fileBytes, "application/octet-stream");
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Error while generating - " + ReportHead + "- Error : " + ex.Message);
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - MIS Account Report - " + ReportHead + " - " + ex.Message });
            }
        }

    }
}