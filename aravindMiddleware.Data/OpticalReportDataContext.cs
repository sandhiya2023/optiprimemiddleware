using aravindMiddleware.Data.DapperClasses;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Linq;


namespace aravindMiddleware.Data
{
    public class OpticalReportDataContext
    {
        #region Constraints
        private readonly string connectionString = CommonConstraints.ConnectionString;
        #endregion  

        public DataSet MISSupplierreport(MISSupplierReportInput objInput)
        {
            DataSet ds = new DataSet();
            string ReportHead = string.Empty;
            try
            {
                ReportHead = objInput.ReportHead.ToString().ToUpper();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "";
                    if (ReportHead == "SUPPLIERORDERSUMMARY") // Suppliers Order Summary 
                    {
                        tmpqry = "RptSupplierSummary";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);
                        cmd.Parameters.AddWithValue("pSupplierOrderStatus", objInput.SupplierOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "SUPPLIERORDERITEMWISE") // Suppliers Order Summary 
                    {
                        tmpqry = "RptSupplierItemwise";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);
                        cmd.Parameters.AddWithValue("pSupplierOrderStatus", objInput.SupplierOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "SUPPLIERINWARDSUMMARY") // Suppliers Order Summary 
                    {
                        tmpqry = "RptSupplierSummary";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);
                        cmd.Parameters.AddWithValue("pSupplierOrderStatus", objInput.SupplierOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }
                    if (ReportHead == "SUPPLIERINWARDITEMWISE") // Suppliers Order Summary 
                    {
                        tmpqry = "RptSupplierInwardItemwise";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);
                        cmd.Parameters.AddWithValue("pSupplierOrderStatus", objInput.SupplierOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "SUPPLIERINWARDPROMOTIONALSUMMARY") // Suppliers Order Summary 
                    {
                        tmpqry = "RptSupplierSummary";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);
                        cmd.Parameters.AddWithValue("pSupplierOrderStatus", objInput.SupplierOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }
                    if (ReportHead == "SUPPLIERINWARDPROMOTIONALITEMWISE") // Suppliers Order Summary 
                    {
                        tmpqry = "RptSupplierInwardItemwise";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);
                        cmd.Parameters.AddWithValue("pSupplierOrderStatus", objInput.SupplierOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }
                    if (ReportHead == "SUPPLIERRETURNSUMMARY") // Suppliers Order Summary 
                    {
                        tmpqry = "RptSupplierSummary";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);
                        cmd.Parameters.AddWithValue("pSupplierOrderStatus", objInput.SupplierOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }
                    if (ReportHead == "SUPPLIERRETURNITEMWISE") // Suppliers Order Summary 
                    {
                        tmpqry = "RptSupplierInwardItemwise";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);
                        cmd.Parameters.AddWithValue("pSupplierOrderStatus", objInput.SupplierOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }
                    if (ReportHead == "TRANSFERINSUMMARY") // Transfer In Summary 
                    {
                        tmpqry = "RptSupplierSummary";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);
                        cmd.Parameters.AddWithValue("pSupplierOrderStatus", objInput.SupplierOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "TRANSFERINITEMWISE") // Transfer In Summary 
                    {
                        tmpqry = "RptSupplierInwardItemwise";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);
                        cmd.Parameters.AddWithValue("pSupplierOrderStatus", objInput.SupplierOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }
                    if (ReportHead == "SUPPLIERORDERCANCEL") // Transfer In Summary 
                    {
                        tmpqry = "RptSupplierOrderCancel";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);

                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "SUPPLIERCREDITNOTES") // Transfer In Summary 
                    {
                        tmpqry = "RptSupplierCreditSummary";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.StockId);
                        cmd.Parameters.AddWithValue("pSupplierOrderStatus", objInput.SupplierOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);

                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : MIS Supplier Report - " + ReportHead);

                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting MIS Supplier Report {ReportHead} - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet MISReceiptPrint(MISReceiptInput objInput)
        {
            DataSet ds = new DataSet();
            string ReportHead = string.Empty;
            try
            {
                ReportHead = objInput.ReportHead.ToString().ToUpper();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "";

                    if (ReportHead == "SUPPLIERORDERPRINT") // Suppliers Order Summary 
                    {
                        tmpqry = "prtSupplierOrderEntry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pSupplierOrderNumber", objInput.SupplierOrderNumber);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "SUPPLIERINWARDPRINT") // Suppliers Order Summary 
                    {
                        tmpqry = "prtSupplierInwardEntry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pSupplierOrderNumber", objInput.SupplierInwardNumber);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "SUPPLIERRETURNPRINT") // Suppliers Order Summary 
                    {
                        tmpqry = "prtSupplierReturnEntry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pSupplierOrderNumber", objInput.SupplierReturnNumber);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "STOCKTRANSFERPRINT") // STOCK TRANSFER ENTRY 
                    {
                        tmpqry = "prtStockTransferEntry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pStockTransferNumber", objInput.StocktransferNumber);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "STOCKRETURNPRINT") // STOCK TRANSFER ENTRY 
                    {
                        tmpqry = "prtStockreturnEntry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pStockReturnNumber", objInput.StockReturnNumber);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "ORDERENTRYPRINT") // order entry print 
                    {
                        tmpqry = "prtOrderEntry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pOrderEntryNumber", objInput.OrderEntryNumber);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "BILLENTRYPRINT") // bill entry print 
                    {
                        tmpqry = "prtBillEntry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pBillEntryNumber", objInput.BillEntryNumber);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }


                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : MIS RECEIPT PRINT - " + ReportHead);

                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting MIS RECEIPT PRINT  {ReportHead} - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet MISStoresreport(MISStoresReportInput objInput)
        {
            DataSet ds = new DataSet();
            string ReportHead = string.Empty;
            try
            {
                ReportHead = objInput.ReportHead.ToString().ToUpper();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "";
                    if (ReportHead == "STOCKTRANSFERSUMMARY") // Stock transfer summary
                    {
                        tmpqry = "RptStoresSummary";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pStoresOrderStatus", objInput.StoreOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "STOCKTRANSFERITEMWISE") // stock transfer itemwise
                    {
                        tmpqry = "RptStoresItemwise";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pStoresOrderStatus", objInput.StoreOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "STOCKRETURNSUMMARY") // STOCK RETURN Summary 
                    {
                        tmpqry = "RptStoresSummary";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pStoresOrderStatus", objInput.StoreOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }
                    if (ReportHead == "STOCKRETURNITEMWISE") // STOCK RETURN ITEMWISE
                    {
                        tmpqry = "RptStoresItemwise";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pStoresOrderStatus", objInput.StoreOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "TRANSFEROUTSUMMARY") // Transfer OUT Summary 
                    {
                        tmpqry = "RptStoresSummary";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pStoresOrderStatus", objInput.StoreOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    if (ReportHead == "TRANSFEROUTITEMWISE") // Transfer OUT ITEMWISE
                    {
                        tmpqry = "RptStoresItemwise";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pStoresOrderStatus", objInput.StoreOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }
                    if (ReportHead == "STOCKINTRANSIT") // Transfer OUT ITEMWISE
                    {
                        tmpqry = "RptStoresItemwise";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pStoresOrderStatus", objInput.StoreOrderStatus);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : MIS Stores Report - " + ReportHead);

                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting MIS Stores Report {ReportHead} - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet MISStockreport(MISStockReportInput objInput)
        {
            DataSet ds = new DataSet();
            string ReportHead = string.Empty;
            try
            {
                ReportHead = objInput.ReportHead.ToString().ToUpper();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "";
                    if ((ReportHead == "STORESSTOCKMOVEMENTREPORT") || (ReportHead == "STORESSTOCKMOVEMENTREPORTBATCHWISE"))
                    {
                        tmpqry = "RptStock";
                    }
                    if ((ReportHead == "STORESCURRENTSTOCKMOVEMENTREPORT") || (ReportHead == "STORESCURRENTSTOCKMOVEMENTREPORTBATCHWISE"))
                    {
                        tmpqry = "RptCurrentStock";
                    }
                    if (ReportHead == "STORESCOSTCOMREPORT")
                    {
                        tmpqry = "RptStockCostcom";
                    }
                    if (ReportHead == "STORESEXPIRYREPORT")
                    {
                        tmpqry = "RptStockExpiry";
                    }
                    if ((ReportHead == "SECTIONSTOCKMOVEMENTREPORT") || (ReportHead == "SECTIONSTOCKMOVEMENTREPORTBATCHWISE"))
                    {
                        tmpqry = "RptSectionStock";
                    }
                    if ((ReportHead == "SECTIONCURRENTSTOCKMOVEMENTREPORT") || (ReportHead == "SECTIONCURRENTSTOCKMOVEMENTREPORTBATCHWISE"))
                    {
                        tmpqry = "RptSectionCurrentStock";
                    }
                    if (ReportHead == "SECTIONCOSTCOMREPORT")
                    {
                        tmpqry = "RptSectionStockCostcom";
                    }
                    if (ReportHead == "SECTIONEXPIRYREPORT")
                    {
                        tmpqry = "RptSectionStockExpiry";
                    }
                    if ((ReportHead == "ALLSTOCKMOVEMENTREPORT") || (ReportHead == "ALLSTOCKMOVEMENTREPORTBATCHWISE"))
                    {
                        tmpqry = "RptAllStock";
                    }

                    if (tmpqry != "")
                    {

                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pStockTypeId", objInput.stockType);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : MIS Stock Report - " + ReportHead);

                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting MIS StoCK Report {ReportHead} - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet MISCustomerreport(MISCustomerReportInput objInput)
        {
            DataSet ds = new DataSet();
            string ReportHead = string.Empty;
            try
            {
                ReportHead = objInput.ReportHead.ToString().ToUpper();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "";
                    if (ReportHead == "ORDERCOLLECTIONREPORT")
                    {
                        tmpqry = "RptOrderCollection";
                    }
                    if (ReportHead == "DELIVERYCOLLECTIONREPORT")
                    {
                        tmpqry = "RptDeliveryCollection";
                    }
                    if (ReportHead == "DIRECTSALECOLLECTIONREPORT")
                    {
                        tmpqry = "RptDeliveryCollection";
                    }
                    if ((ReportHead == "ORDERCANCELLATIONREPORT") || (ReportHead == "BILLCANCELLATIONREPORT"))
                    {
                        tmpqry = "RptOrderCancellation";
                    }
                    if (ReportHead == "ORDERDISCOUNTREPORT")
                    {
                        tmpqry = "RptOrderDiscount";
                    }
                    if (ReportHead == "STOCKDEDUCTIONREPORT")
                    {
                        tmpqry = "RptStockDeduction";
                    }
                    if (ReportHead == "ORDERDETAILSREPORT")
                    {
                        tmpqry = "Rptorderdetails";
                    }
                    if (ReportHead == "DIRECTORDERDETAILSREPORT")
                    {
                        tmpqry = "Rptdirectorderdetails";
                    }
                    if (ReportHead == "DELIVERYDETAILSREPORT")
                    {
                        tmpqry = "Rptdeliverydetails";
                    }
                    if (ReportHead == "UNDELIVERYREPORT")
                    {
                        tmpqry = "RptundeliveryDetails";
                    }
                    if (ReportHead == "SALESPERSONREPORT")
                    {
                        tmpqry = "Rptsalesperson";
                    }
                    if (tmpqry != "")
                    {

                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pPaymentId", objInput.PaymentId);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : MIS Customers Report - " + ReportHead);

                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting MIS Customer Report {ReportHead} - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet MISTrackingReport(MISTrackingReportInput objInput)
        {
            DataSet ds = new DataSet();
            string ReportHead = string.Empty;
            try
            {
                ReportHead = objInput.ReportHead.ToString().ToUpper();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "";
                    if (ReportHead == "ORDERTRACKINGREPORT")
                    {
                        tmpqry = "Rptordertracking";
                    }
                    if (ReportHead == "ORDERTRACKINGONTIMEREPORT")
                    {
                        tmpqry = "Rptordertrackingontime";
                    }
                    if (ReportHead == "HOURLYREPORT")
                    {
                        tmpqry = "RptHourly";
                    }
                    if (tmpqry != "")
                    {

                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        cmd.Parameters.AddWithValue("pTrackingId", objInput.TrackingId);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : MIS TRacking Report - " + ReportHead);

                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting MIS Tracking Report {ReportHead} - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet MISAccountReport(MISAccountReportInput objInput)
        {
            DataSet ds = new DataSet();
            string ReportHead = string.Empty;
            try
            {
                ReportHead = objInput.ReportHead.ToString().ToUpper();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "";
                    if (ReportHead == "HSNWISEREPORT")
                    {
                        tmpqry = "Rpthsnwise";
                    }
                    if (ReportHead == "POSTALREPORT")
                    {
                        tmpqry = "Rptpostal";
                    }
                    if (ReportHead == "CASHFLOWREPORT")
                    {
                        tmpqry = "Rptcashflow";
                    }
                    if (ReportHead == "AUDITREPORT")
                    {
                        tmpqry = "Rptaudit";
                    }
                    if (ReportHead == "TALLYSALESSUMMARYREPORT")
                    {
                        tmpqry = "Rpttallysalessummary";
                    }
                    if (ReportHead == "TALLYPURCHASESUMMARYREPORT")
                    {
                        tmpqry = "Rpttallypurchasessummary";
                    }
                    if (tmpqry != "")
                    {

                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.OrgId);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.Zoneid);
                        cmd.Parameters.AddWithValue("pLocationId", objInput.LocationId);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pStoreId", objInput.StoreId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.SectionId);
                        cmd.Parameters.AddWithValue("pReport", objInput.ReportHead);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }

                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : MIS Account Report - " + ReportHead);

                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting MIS Account Report {ReportHead} - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }


    }

}