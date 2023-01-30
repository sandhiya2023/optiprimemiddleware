using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace aravindMiddleware.Data
{
    public class UserLogin
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string username { get; set; }
        public string password { get; set; }

    }



    #region "Customer Order Related"
        public class Header
        {
            public string counterid { get; set; }
            public string countername { get; set; }
            public string ordernumber { get; set; }
            public string financialyearid { get; set; }
            public string financialyear { get; set; }
            public string orderdtm { get; set; }            
        }

        public class ShippingAddress
        {
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string pincode { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string phone { get; set; }
        }
        public class PatientInfo
        {
            public string outprescription { get; set; }
            public string mrn { get; set; }
            public string uin { get; set; }
            public string unit { get; set; }
            public string name { get; set; }
            public string age { get; set; }
            public string gender { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string pincode { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string email { get; set; }   
            public string phone { get; set; }
            public string altphone { get; set; }
            public string gstin { get; set; }

            public string chkshippingaddress { get; set; }    
            public ShippingAddress shippingaddress { get; set; }
            public string shippingaddressjson { get; set; }
          
        }
        public class OrderDetail
        {
            public string sameglasspres { get; set; }
            public string ordertype { get; set; } //whether it is frame only, lens only or frame and lens only
            public Frame frame { get; set; }
            public RightEye righteye { get; set; }
            public LeftEye lefteye { get; set; }
            public DeliveryItems deliverybox { get; set; }
            public DeliveryItems deliverycloth { get; set; }
    }
        public class Frame
        {
            public string ownframe { get; set; } = "";
            public string ownframeremarks { get; set; } = "";
            public string productid { get; set; }
            public string barcode { get; set; } = "";
            public string itemdesc { get; set; } = "";
            public string batchno { get; set; } = "";
            public string expirydate { get; set; } = "";
            public string cost { get; set; } = "";
            public string iscancelled { get; set; } = "0";
            public string reuse { get; set; } = "";
            public string refundamt { get; set; } = "";
            public string isdiscounted { get; set; } = "0";
            public string discountedamount { get; set; } = "";
        }
        public class Attributes
        {
            public string sph { get; set; } = "";
            public string cyl { get; set; } = "";
            public string axis { get; set; } = "";
            public string prism { get; set; } = "";
            public string fittingheight { get; set; } = "";
            public string add { get; set; } = "";
            public string ipd { get; set; } = "";
        }
        public class RightEye
        {
            public string ownlens { get; set; } = "";
            public string ownlensremarks { get; set; } = "";
            public string barcode { get; set; } = "";
            public string itemdesc { get; set; } = "";
            public string productid { get; set; }
            public string batchno { get; set; } = "";
            public string expirydate { get; set; } = "";
            public Attributes attributes { get; set; }
            public string attributesjson { get; set; } = "";
            public string cost { get; set; } = "";

            public string iscancelled { get; set; } = "0";         
            public string reuse { get; set; } = "";
            public string refundamt { get; set; } = "";

            public string isdiscounted { get; set; } = "0";
            public string discountedamount { get; set; } = "";
        }
        public class LeftEye
        {
            public string ownlens { get; set; } = "";
            public string ownlensremarks { get; set; } = "";
            public string productid { get; set; }
            public string barcode { get; set; } = "";
            public string itemdesc { get; set; } = "";
            public string batchno { get; set; } = "";
            public string expirydate { get; set; } = "";
            public Attributes attributes { get; set; }
            public string attributesjson { get; set; } = "";
            public string cost { get; set; } = "";
            public string reuse { get; set; } = "";
            public string refundamt { get; set; } = "";
            public string isdiscounted { get; set; } = "0";
            public string discountedamount { get; set; } = "";
        }
        
        public class DeliveryItems
        {
            public string productid { get; set; } = "";
            public string barcode { get; set; } = "";
            public string itemdesc { get; set; } = "";
            public string batchno { get; set; } = "";
            public string expirydate { get; set; } = "";
            public string qty { get; set; } = "";
            public string cost { get; set; } = "0";
        }
        public class DirectSale
        {
            public string categoryid { get; set; } = "";
            public string categorydesc { get; set; } = "";
            public string productid { get; set; }
        
            public string itemdesc { get; set; }
            public string barcode { get; set; } = "";
            public string itemtype { get; set; } = "";
            public string batchno { get; set; } = "";
            public string expirydate { get; set; } = "";
            public string qty { get; set; } = "";
            public string sellingprice { get; set; } = "";
            public string cost { get; set; } = "";
            public string reuse { get; set; } = "";
            public string refundamount { get; set; } = "";
        }
        public class PaymentTransaction
        {
            public string paymentmode { get; set; } = "";
            public string paymentmodedesc { get; set; } = "";
            public string paymentmoderemarks { get; set; } = "";
            public string paymentamt { get; set; } = "";
            public string paymentdtm { get; set; } = "";
            public string paymentstagedesc { get; set; } = "";

    }
        public class PaymentDetails
        {
            public string orderid { get; set; } = "";
            public string paymentmode { get; set; } = "";
            public string paymentmoderemarks { get; set; } = "";
            public string deliverymode { get; set; } = "";
            public string schdeliverydate { get; set; } = "";
            public string schdeliverytime { get; set; } = "";
            public string misccharges { get; set; } = "";
            public string parcelcharges { get; set; } = "";
            public string miscchargesrefundamt { get; set; } = "";
            public string parcelchargesrefundamt { get; set; } = "";

            public string fittingcharges { get; set; }
            public string fittingchargesrefundamt { get; set; }
            public string tintcharges { get; set; }
            public string tintchargesrefundamt { get; set; }


            public string totalamt { get; set; } = "";
            public string ordertotalcost { get; set; } = "";
            public string advanceamt { get; set; } = "";
            public string balanceamt { get; set; } = "";
            public string userid { get; set; } = "";
            public string username { get; set; } = "";
            public string password { get; set; } = "";
            public string remarks { get; set; } = "";
        }
        public class OrderDelivery
        {
            public string paymentmode { get; set; } = "";
            public string paymentmoderemarks { get; set; } = "";
            public string balanceamt { get; set; } = "";
            public string userid { get; set; } = "";
            public string username { get; set; } = "";
            public string deliverydtm { get; set; } = "";
            public string deliverycounterid { get; set; } = "0";
        }

        public class OrderCancel
        {
            public string cancelreason { get; set; } = "";
            public string recommendedby { get; set; } = "";
        }

        public class OrderDiscount
        {
            public string discountcategory { get; set; } = "";
            public string discountreason { get; set; } = "";
            public string recommendedby { get; set; } = "";
        }

        public class CustomerOrder
        {
       
            public string orgid { get; set; } = "";
            public string zoneid { get; set; } = "";
            public string locationid { get; set; } = "";
            public string storeid { get; set; } = "";
            public string counterid { get; set; } = "";
            public string countername { get; set; } = "";            
            public string orderid { get; set; } = "";
            public string orderno { get; set; } = "";
			public string displayorderno { get; set; } = "";
            public string billnumber { get; set; } = "";
            public string customerorderid { get; set; } = "";
            public string customerid { get; set; } = "";
            public string isdelivery { get; set; } = "";
            public string iscancelled { get; set; } = "";
            public string isdiscount { get; set; } = "";

            public string isreadyfordelivery { get; set; } = "";
            public string ispartialpayment { get; set; } = "";
            public string isdirectsale { get; set; } = "";
            public string timezoneid { get; set; } = "";

            public Header header { get; set; }
            public PatientInfo patientInfo { get; set; }
            public OrderDetail orderDetail { get; set; }
            public List<DirectSale> directSales { get; set; }
            public List<PaymentTransaction> paymentTransaction { get; set; }
            public PaymentDetails paymentDetails { get; set; }
            public OrderDelivery orderDelivery { get; set; }
            public OrderCancel orderCancel { get; set; }
            public OrderDiscount orderDiscount { get; set; }
        }      
        public class CustomerOrderProductsInput
        {
        
            public string orgid { get; set; }
            public string zoneid { get; set; }
            public string locationid { get; set; }
            public string counterid { get; set; }
            public string deliverycounterid { get; set; } = "0";

            public string customerid { get; set; }
            public string categoryid { get; set; }
            public string orderid { get; set; }
            public string productid { get; set; }
            public string barcode { get; set; }
            public string batchno { get; set; }
            public string expirydate { get; set; }
            public string itemtype { get; set; }
            public string ownitem { get; set; }
            public string qty { get; set; }
            public string itemcost { get; set; }
            public string cost { get; set; }
            public string attributes { get; set; }
            public string remarks { get; set; }
            public string refundamount { get; set; } = "0";
            public string iscancelled { get; set; } = "0";

        public string po_orderid { get; set; } = "0";
            public string po_ordernumber { get; set; } = ""; 
        }

    #endregion
   

    public class DashboardInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string uin { get; set; }
        public string orderno { get; set; }
        public string patientname { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string active { get; set; }
        public string counter { get; set; }

        public string status { get; set; }
        public string zoneid { get; set; }		
        public string timezoneid { get; set; }
        public string phonenumber { get; set; }
        public string datetype { get; set; } // value one of orderdate (or) deliverydate
    }

    public class ProductMasterInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string storeId { get; set; }
        public string counterid { get; set; }
        public string stocktypeid { get; set; } = "0";
        public string categoryid { get; set; }
        public string categoryname { get; set; }
        public string barcode { get; set; }             
        public string searchstring { get; set; }
        public string supplierid { get; set; } = "0";

    }
    public class RefractionDetailsInput
    {
        public string tenentId { get; set; }
        public string sph { get; set; }
        public string cyl { get; set; }
        public string axis { get; set; }
        public string add { get; set; }
    }

    public class SupplierMasterInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string active { get; set; }

    }

    public class crudSupplierDetailsInput
    {
        public int orgid { get; set; }
        public int supplierId { get; set; }
        public int zoneid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int pincode { get; set; }
        public string homephone { get; set; }
        public string officephone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string tngstno { get; set; }
        public string cstno { get; set; }
        public string trackingCompanyId { get; set; }
        public int tallyType { get; set; }
        public int stateId { get; set; }
        public int active { get; set; }
        public int taxfree { get; set; }
        public int lenssupplier { get; set; }
        public int countryid { get; set; }
    }

    public class crudProductDetailsInput
    {
        public string orgid { get; set; }
        public string productid { get; set; }
        public string name { get; set; }
        public string categoryid { get; set; }
        public string barcode { get; set; }
        public string supplierid { get; set; }
        public string zoneid { get; set; }
        public string brand { get; set; }
        public string color { get; set; }
        public string model { get; set; }
        public string type { get; set; }
        public string size { get; set; }
        public string hsncode { get; set; }
        public string externalid { get; set; }
        public string company { get; set; }
        public string ratefixingcategoryid { get; set; }
        public string purchaseprice { get; set; }
        public string sellingprice { get; set; }
        public string active { get; set; }
        public lensmatrixitem lensmatrix { get; set; }
    }

    public class lensmatrixitem
    {
        public string cylpositivefrom { get; set; }
        public string cylpositiveto { get; set; }
        public string cylnegativefrom { get; set; }
        public string cylnegativeto { get; set; }
        public string totalpositivefrom { get; set; }
        public string totalpositiveto { get; set; }
        public string totalnegativefrom { get; set; }
        public string totalnegativeto { get; set; }
        public string additionpositivefrom { get; set; }
        public string additionpositiveto { get; set; }
    }

    public class LookupMasterDetails
    {
        public int orgid { get; set; }
        public int tblId { get; set; }
        public string ptype { get; set; }

    }

    public class crudLookupDetailsInput
    {
        public int orgid { get; set; }
        public int locationid { get; set; }
        public int lookupid { get; set; }
        public string displayname { get; set; }
        public string shortname { get; set; }
        public string lookuptype { get; set; }
        public string custom { get; set; }
        public int active { get; set; }
        public int displayorder { get; set; }
	    public int tblId { get; set; }
        public int productattributes { get; set; }
    }  
    
    public class UserDetailsInput
    {
        public string orgid { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
        public string roleid { get; set; }

    }
    public class crudUserDetailsInput
    {
        public int orgid { get; set; }
        public int locationid { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string empcode { get; set; }
        public string displayname { get; set; }
        public int roleid { get; set; }
        public int active { get; set; }
    }


    public class SupplierOrderWorklistInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string storeid { get; set; }
        public string transtype { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string supplierid { get; set; }
        public string categoryid { get; set; }
        public string stocktype { get; set; }
        public string supplierorderstatusid { get; set; }
    }

    public class StoreOrderWorklistInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string categoryid { get; set; }
        public string fromid { get; set; }
        public string toid { get; set; }
        public string stockorderstatusid { get; set; }
        public string transtype { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        
    }
  
    public class SupplierTransCanceldetails
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string actiontype { get; set; }
        public string reason { get; set; }
        public string reasondescription { get; set; }
        public string remarks { get; set; }
        public string approvedby { get; set; }
        public string approvedbyname { get; set; }
    }

    public class SupplierTransEditdetails
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string actiontype { get; set; }
        public string reason { get; set; }
        public string reasondescription { get; set; }
        public string remarks { get; set; }
        public string approvedby { get; set; }
        public string approvedbyname { get; set; }
    }

    public class SupplierTransGeneraldetails
    {
        public string storeid { get; set; }
        public string storename { get; set; }
        public string categoryid { get; set; }
        public string categoryname { get; set; }
        public string supplierid { get; set; }
        public string suppliername { get; set; }

        public string supplieraddress { get; set; }
        public string taxfree { get; set; }
        public string stocktype { get; set; }
        public string billtype { get; set; }
        public string billrefno { get; set; }
        public string billdate { get; set; }
        public string poFromdate { get; set; }
        public string poTodate { get; set; }
    }

    public class SupplierTransPaymentdetail
    {
        public string mode { get; set; }
        public string orderpaymentid { get; set; }
        public string isdeleted { get; set; }
        public string paymentmode { get; set; }
        public string paymentremarks { get; set; }
        public string paymentamount { get; set; }
    }

    public class SupplierTransProductdetail
    {
        public string orderitemid { get; set; }
        public string productid { get; set; }
        public string barcode { get; set; }
        public string name { get; set; }
        public string batchno { get; set; }
        public string expirydate { get; set; }
        public string requestedquantity { get; set; }
        public string receivedquantity { get; set; }
        public string balance { get; set; }
        public string purchaseprice { get; set; }
        public string totalamount { get; set; }
        public string discountperc { get; set; }
        public string discountval { get; set; }
        public string purchasepricediscount { get; set; }
        public string sellingprice { get; set; }
        public string supplierinwardnumber { get; set; }
        public string supplierinwardorderid { get; set; }
        public string gst { get; set; }
        public string sgsttaxperc { get; set; }
        public string sgsttaxval { get; set; }
        public string cgsttaxperc { get; set; }
        public string cgsttaxval { get; set; }
        public string igsttaxperc { get; set; }
        public string igsttaxval { get; set; }        
        public string hsncode { get; set; }
        public string isdeleted { get; set; }

        public string ratefixingcategoryid { get; set; }
        public string ratefixingcategoryvalue { get; set; }        
        public string hsnvalue { get; set; }

    }

    public class SupplierTransSummary
    {
        public string totalquantity { get; set; }
        public string totalamount { get; set; }
        public string totaldiscount { get; set; }
        public string expirydate { get; set; }
        public string freightcharges { get; set; }
        public string othercharges { get; set; }
        public string roundoff { get; set; }
        public string cashdiscount { get; set; }
        public string returnreason { get; set; }
        public string returnmode { get; set; }

        public string returntype { get; set; }
        //to be decided columns below
        public string gstamount { get; set; }

        public string netval { get; set; }


    }

    public class StoreTransaction
    {
        public string orgid { get; set; }
        public string zoneid { get; set; }
        public string locationid { get; set; }
        public string userid { get; set; }
        public string type { get; set; }
        public string typedesc { get; set; }

        public string orderstatus { get; set; }
        public string orderstatusdesc { get; set; }
        public string orderid { get; set; } //primary key
        public string orderdtm { get; set; }
        public string ordernumber { get; set; } //STMDU2223-00001

        public string iscancelled { get; set; }
        public string isacceptedrejected { get; set; }
        public StoreTransGeneraldetails generaldetails { get; set; }        
        public StoreTransEditdetails editdetails { get; set; }
        public List<StoreTransEditdetails> editdetailslist { get; set; }
        public StoreTransCanceldetails canceldetails { get; set; }
        public List<StoreTransCanceldetails> canceldetailslist { get; set; }        
        public List<StoreTransProductdetail> productdetails { get; set; }
   
    }

    public class ConsumptionOrderDetails
    {
        public string orderid { get; set; }
        public string customerordernumber { get; set; }
        public string orderitemid { get; set; }
        public string orderstatustrackingid { get; set; }

    }
    public class StoreTransProductdetail
    {
        public string orderitemid { get; set; }

        public string storeorderid { get; set; }
        public string productid { get; set; }
        public string barcode { get; set; }
        public string name { get; set; }
        public string batchno { get; set; }
        public string expirydate { get; set; }
        public string transferredquantity { get; set; }
        public string acceptedquantity { get; set; }
        public string rejectedqty { get; set; }
        public string sellingprice { get; set; }
        public string supplierinwardnumber { get; set; }
        public string supplierinwardorderid { get; set; }

        public string remarks { get; set; }
        public string rejectedreason { get; set; }
        public string hsncode { get; set; }
        public string isdeleted { get; set; }

    }

    public class StoreTransGeneraldetails
    {
        public string fromid { get; set; }
        public string fromname { get; set; }
        public string toid { get; set; }
        public string toname { get; set; }
        public string categoryid { get; set; }
        public string categoryname { get; set; }
        public string transfermode { get; set; }
        public string totalquantity { get; set; }
        public string totalacceptedqty { get; set; }
        public string totalrejectedqty { get; set; }

    }

    public class StoreTransCanceldetails
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string actiontype { get; set; }
        public string reason { get; set; }
        public string remarks { get; set; }
        public string approvedby { get; set; }
        public string reasondescription { get; set; }
        public string approvedbyname { get; set; }

    }

    public class StoreTransEditdetails
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string actiontype { get; set; }
        public string reason { get; set; }
        public string remarks { get; set; }
        public string approvedby { get; set; }
    }

    public class SupplierTransaction
    {
        public string orgid { get; set; }
        public string zoneid { get; set; }
        public string locationid { get; set; }
        public string userid { get; set; }
        public string type { get; set; }
        public string typedesc { get; set; }

        public string orderstatus { get; set; }
        public string orderid { get; set; } //primary key
        public string orderdtm { get; set; }
        public string ordernumber { get; set; } //POMDU2223-00001

        public string parentsupplierorderid { get; set; }
        public string parentsupplierordernumber { get; set; }

        public string iscancelled { get; set; }
        public string isaborted { get; set; }
        public SupplierTransGeneraldetails generaldetails { get; set; }
        public SupplierTransSummary summary { get; set; }
        public SupplierTransEditdetails editdetails { get; set; }
        
        public List<SupplierTransEditdetails> editdetailslist { get; set; }
        public SupplierTransCanceldetails canceldetails { get; set; }
        public List<SupplierTransCanceldetails> canceldetailslist { get; set; }
        public List<SupplierTransPaymentdetail> paymentdetails { get; set; }
        public List<SupplierTransProductdetail> productdetails { get; set; }
        public List<ConsumptionOrderDetails> consumpionorderdetails { get; set; }
    }

    public class SupplierTransReturnRemarks
    {
        public string supplierorderid { get; set; }
        public string creditnoteremarks { get; set; }
        public string replacementremarks { get; set; }
        public string type { get; set; }
    }

    public class SupplierTransactionHistoryInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string barcode { get; set; }
        public string categoryid { get; set; }
        public string transtype { get; set; }
        public string historytype { get; set; }

    }
    public class StockDeductionItem {        
        public string stockdeductionitemsid { get; set; }
        public string productid { get; set; }
        public string barcode { get; set; }
        public string batchno { get; set; }
        public string expirydate { get; set; }
        public string qty { get; set; }
    }
    public class StockDeduction
    {
        public string orgid { get; set; }
        public string zoneid { get; set; }
        public string locationid { get; set; }
        public string storeid { get; set; }
        public string counterid { get; set; }
        public string categoryid { get; set; }
        public string transtype { get; set; }
        public string userid { get; set; }

        public string stockdeductionid { get; set; }
        public string stockdeductionnumber { get; set; }


        public List<StockDeductionItem> productdetails { get; set; }
    }

    public class InsCustomerVisit
    {
        public string orgid { get; set; }
        public string zoneid { get; set; }
        public string locationid { get; set; }
        public string counterid { get; set; }
        public string patientrejectreason { get; set; }
        public string uin { get; set; }

    }

    public class CustomerOrderStatusTracking
    {
        public string orderid { get; set; }
        public string orderstatusid { get; set; }
        public string userid { get; set; }
        public string rejectedreasonuserid { get; set; }
        public string remarks { get; set; }
        public string labmachineid { get; set; }
        public string supplierid { get; set; }
        public string rejectedreasonid { get; set; }
        public lensdetail leftlens { get; set; }
        public lensdetail rightlens { get; set; }


    }

    public class lensdetail
    {
        public string supplierid { get; set; }
        public string lensstatus { get; set; }
        public string orderstatusid { get; set; }
        public string rejectedreasonid { get; set; }

    }

    public class LensMatrixInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string sph { get; set; }
        public string cyl { get; set; }
        public string add { get; set; }
        public string barcode { get; set; }
    }

    public class ConsumptionBasedPOInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string storeid { get; set; }
        public string categoryid { get; set; }
        public string supplierid { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
    }
    public class crudRoleDetailsInput
    {
        public string orgid { get; set; }
        public string roleid { get; set; }
        public string rolename { get; set; }
        public string active { get; set; }
    }

    public class crudUserRoleDetailsInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string userroleid { get; set; }
        public string userid { get; set; }
        public string roleid { get; set; }
        public string active { get; set; }
        public string defaultrole {get;set;}
        public string scopejson { get; set; }
        public string actionjson { get; set; }
    }

    public class crudUserStoreDetailsInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string userstoreid { get; set; }
        public string userid { get; set; }
        public string storeid { get; set; }
        public string defaultselected { get; set; }
        public string active { get; set; }

    }
    public class crudUserCounterDetailsInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string usercounterid { get; set; }
        public string userid { get; set; }
        public string storeid { get; set; }
        public string counterid { get; set; }
        public string defaultselected { get; set; }
        public string active { get; set; }

    }
    //newly added model
    public class crudCounterInput
    {
        public string orgid { get; set; }
        public string locationid { get; set; }
        public string storeid { get; set; }
        public string counterid { get; set; }
        public string countername { get; set; }
        public string active { get; set; }
        public string shortname { get; set; }
        public string isdeliverycounter { get; set; }
        public string custom { get; set; }

    }

    public class menuitem
    {
        public string label { get; set; }
        public bool isTitle { get; set; }
        public string icon { get; set; }
        public string link { get; set; }
        public bool collapsed { get; set; }

        public List<submenuitem> children = new List<submenuitem>();

    }

    public class submenuitem
    {
        public string key { get; set; }
        public string label { get; set; }
        public string link { get; set; }
        public string parentKey { get; set; }        

    }

}
