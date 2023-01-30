using System;
using System.Collections.Generic;
using System.Text;

namespace aravindMiddleware.Data
{
    public class MISReportInput
    {
        public string tenentid { get; set; }
        public string entityid { get; set; }
        public string sectionid { get; set; }
        public string ordernumber { get; set; }
        public string billnumber { get; set; }
        public string purchasenumber { get; set; }
        public string purchasereturnnumber { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string fromtime { get; set; }
        public string totime { get; set; }
        public string CategoryId { get; set; }
        public string SupplierId { get; set; }
        public string paymentid { get; set; }
        public string reportType { get; set; }
        public string ReportFormat { get; set; }

    }

    public class MISSupplierReportInput
    {
        public string OrgId { get; set; }
        public string Zoneid { get; set; }
        public string LocationId { get; set; }
        public string SupplierId { get; set; }
        public string CategoryId { get; set; }
        public string StoreId { get; set; }
        public string StockId { get; set; }
        public string SupplierOrderStatus { get; set; }
        public string ReportHead { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string ReportFormat { get; set; }
    }
    public class MISReceiptInput
    {
        public string OrgId { get; set; }
        public string Zoneid { get; set; }
        public string LocationId { get; set; }
        public string SectionId { get; set; }
        public string ReportHead { get; set; }
        public string SupplierOrderNumber { get; set; }
        public string SupplierInwardNumber { get; set; }
        public string SupplierReturnNumber { get; set; }
        public string StocktransferNumber { get; set; }
        public string StockReturnNumber { get; set; }
        public string OrderEntryNumber { get; set; }
        public string BillEntryNumber { get; set; }
        public string ReportFormat { get; set; }


    }
    public class MISStoresReportInput
    {
        public string OrgId { get; set; }
        public string Zoneid { get; set; }
        public string LocationId { get; set; }
        public string SupplierId { get; set; }
        public string CategoryId { get; set; }
        public string StoreId { get; set; }
        public string SectionId { get; set; }
        public string StoreOrderStatus { get; set; }
        public string ReportHead { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string ReportFormat { get; set; }
    }
    public class MISStockReportInput
    {
        public string OrgId { get; set; }
        public string Zoneid { get; set; }
        public string LocationId { get; set; }
        public string SupplierId { get; set; }
        public string CategoryId { get; set; }
        public string StoreId { get; set; }
        public string SectionId { get; set; }
        public string ReportHead { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string stockType { get; set; }
        public string ReportFormat { get; set; }
    }

    public class MISCustomerReportInput
    {
        public string OrgId { get; set; }
        public string Zoneid { get; set; }
        public string LocationId { get; set; }
        public string SectionId { get; set; }
        public string PaymentId { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string ReportHead { get; set; }
        public string ReportFormat { get; set; }
    }

    public class MISTrackingReportInput
    {
        public string OrgId { get; set; }
        public string Zoneid { get; set; }
        public string LocationId { get; set; }
        public string SectionId { get; set; }
        public string TrackingId { get; set; }
        public string SupplierId { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string ReportHead { get; set; }
        public string ReportFormat { get; set; }
    }

    public class MISAccountReportInput
    {
        public string OrgId { get; set; }
        public string Zoneid { get; set; }
        public string LocationId { get; set; }
        public string StoreId { get; set; }
        public string SectionId { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string ReportHead { get; set; }
        public string ReportFormat { get; set; }

    }
}

