using aravindMiddleware.Data.DapperClasses;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using  aravindMiddleware.Data;
using Org.BouncyCastle.Utilities;
using System.Xml.Linq;


namespace aravindMiddleware.Data
{
    public class OpticalDataContext
    {
        #region Constraints
        private readonly string connectionString = CommonConstraints.ConnectionString;
        #endregion       

        public DataSet ValidateUserLogin(UserLogin userlogin)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "ValidateUserLogin";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrgId", userlogin.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", userlogin.locationid);
                    cmd.Parameters.AddWithValue("pUserName", userlogin.username);
                    cmd.Parameters.AddWithValue("pPassword", userlogin.password);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : ValidateOrgCode");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of ValidateOrgCode - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }
        public async Task<users> GetUsers(string sUserName,string sPassword,string UserId)
        {
            try
            {
                using (var db = new MySqlConnection(connectionString))
                {
                    users users = new users();
                    var @params = new { username = sUserName, password = sPassword, UserId = @UserId };

                    /*
                    string sql = "SELECT CASE WHEN t.entityId IS NULL THEN e.entityID ELSE t.entityId END AS tenentID,REPLACE(JSON_EXTRACT(t.custom, '$.currencysymbol'),'\"','') AS currencySymbol, " +
                                " cast(rm.custom as char(10000)) as rolesMatrix, rm.custom1 as 'defaultpage', u.* FROM users u" +
                                " INNER JOIN rolemaster rm on rm.roleId = u.roleId" + 
                                " INNER JOIN entity e ON e.entityId = u.entityId" +
                                " LEFT JOIN entity t ON t.entityId = e.parentId "+
                                " WHERE BINARY userName = @username AND password = @password AND e.entityId = @entityId and u.active = 1";
                    */
                    string sql = "SELECT e.orgid as orgid,REPLACE(JSON_EXTRACT(e.custom, '$.currencysymbol'),'\"','')  " +
                                " AS currencySymbol,  cast(rm.scope as char(10000)) as rolesMatrix," +
                                " 'apps/dashboard' as 'defaultpage', u.* " +
                                " FROM entityuser u " +
                                " inner join entityuserrole ur on ur.userid = u.userid " +
                                " INNER JOIN entityrole rm on rm.roleId = ur.roleid" +
                                " INNER JOIN entityorg e ON e.orgid = 1" +
                                " -- LEFT JOIN entity t ON t.entityId = e.parentId" +
                                " WHERE BINARY userName = @username AND password = @password AND e.orgid = @entityid and u.active = 1";


                    var userData = await db.QueryAsync<users>(sql,@params);
                    users user = userData.SingleOrDefault();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : GetUsers");
                    return user;

                }
            }
            catch(Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting user details - Exception: : {ex}");
                throw ex;
            }
        }

        public DataSet GetProductDetailsByCode(ProductMasterInput productInput)
        {
                    
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetProductDetailsByCode";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrgId", productInput.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", productInput.locationid);
                    cmd.Parameters.AddWithValue("pStoreId", productInput.storeId);
                    cmd.Parameters.AddWithValue("pCounterId", productInput.counterid);
                    cmd.Parameters.AddWithValue("pStockTypeId", productInput.stocktypeid);
                    cmd.Parameters.AddWithValue("pBarCode", productInput.barcode);
                    cmd.Parameters.AddWithValue("pCategory", productInput.categoryname);
                    cmd.Parameters.AddWithValue("pSupplierId", productInput.supplierid);


                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : GetProductDetailsByCode");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting GetProductDetailsByCode - Exception: : {ex}");
                throw ex;
            }

            return ds;

        }

        public DataSet ValidateOrgCode(string orgcode)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "validateorgcode";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrgCode", orgcode);                   

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : ValidateOrgCode");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of ValidateOrgCode - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet PreValidateSupplierOrder(string supplierorderid,string supplierid,string billrefnumber)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "PreValidateSupplierOrder";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pSupplierOrderId", supplierorderid);
                    cmd.Parameters.AddWithValue("pSupplierId", supplierid);
                    cmd.Parameters.AddWithValue("pBillRefNumber", billrefnumber);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : ValidateOrgCode");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of ValidateOrgCode - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet LookupData(string orgid, string locationid, string lookuptype)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetLookupDetails";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrgId", orgid);
                    cmd.Parameters.AddWithValue("pLocationId", locationid);
                    cmd.Parameters.AddWithValue("pType", lookuptype);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : LookupData");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting lookup data - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }
        public DataSet ValidateRefractionDetails(string tenentId, string sph, string cyl, string axis, string add)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "ValidateRefractionDetails";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pTenentId", tenentId);
                    cmd.Parameters.AddWithValue("pSph", sph);
                    cmd.Parameters.AddWithValue("pCyl", cyl);
                    cmd.Parameters.AddWithValue("pAxis", axis);
                    cmd.Parameters.AddWithValue("pAdd", add);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : ValidateRefractionDetails");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of ValidateRefractionDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public MySqlCommand CRUDCustomer(string orgid, string customerid, string zoneid, string locationid, PatientInfo patientInfo)
        {
            MySqlCommand cmd;
            try
            {
                    string sql = "CRUD_Customer";
                    cmd = new MySqlCommand(sql);
                    cmd.Parameters.AddWithValue("pCustomerId", customerid);
                    cmd.Parameters.AddWithValue("pOrgId", orgid);
                    cmd.Parameters.AddWithValue("pZoneId", zoneid);
                    cmd.Parameters.AddWithValue("pLocationId", locationid);                                 
                    cmd.Parameters.AddWithValue("pUIN", patientInfo.uin);

                    cmd.Parameters.AddWithValue("pName", patientInfo.name);
                    cmd.Parameters.AddWithValue("pAge", patientInfo.age);
                    cmd.Parameters.AddWithValue("pSex", patientInfo.gender);
                    cmd.Parameters.AddWithValue("pAddress1", patientInfo.address1);
                    cmd.Parameters.AddWithValue("pAddress2", patientInfo.address2);

                    cmd.Parameters.AddWithValue("pCity", patientInfo.city);
                    cmd.Parameters.AddWithValue("pState", patientInfo.state);
                    cmd.Parameters.AddWithValue("pPINCODE", patientInfo.pincode);
                    cmd.Parameters.AddWithValue("pCountry", patientInfo.country);
                    cmd.Parameters.AddWithValue("pPhone", patientInfo.phone);                    
                    cmd.Parameters.AddWithValue("pAltPhone", patientInfo.altphone);
                    cmd.Parameters.AddWithValue("pEmail", patientInfo.email);                   

                    cmd.CommandType = CommandType.StoredProcedure;
                                                   
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUDCustomer - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUDCustomerOrder(CustomerOrder customerOrder)
        {
            MySqlCommand cmd;
            try
            {
               
                    string sql = "CRUD_CustomerOrder";
                    cmd = new MySqlCommand(sql);
                    cmd.Parameters.AddWithValue("pOrderId", (customerOrder.customerorderid == null)? "0" : customerOrder.customerorderid);
                    cmd.Parameters.AddWithValue("pOrgId", customerOrder.orgid);
                    cmd.Parameters.AddWithValue("pZoneId", customerOrder.zoneid);
                    cmd.Parameters.AddWithValue("pLocationId", customerOrder.locationid);                
                    cmd.Parameters.AddWithValue("pCounterId", customerOrder.header.counterid);
                    cmd.Parameters.AddWithValue("pCustomerId", customerOrder.customerid);
                    cmd.Parameters.AddWithValue("pOrderNo", customerOrder.orderid);


                    DateTime utcdate1 = new DateTime();
                    if (customerOrder.timezoneid != "")
                        utcdate1 = Extension.ConvertToNativeTimeZone(customerOrder.timezoneid, customerOrder.header.orderdtm); 
                    else
                        utcdate1 = Convert.ToDateTime(customerOrder.header.orderdtm);

                    cmd.Parameters.AddWithValue("pOrderDtm", utcdate1);
                    //cmd.Parameters.AddWithValue("pOrderDtm", customerOrder.header.orderdtm );                    


                    cmd.Parameters.AddWithValue("pUnit", customerOrder.patientInfo.unit);
                    cmd.Parameters.AddWithValue("pOutPrescription", customerOrder.patientInfo.outprescription);
                    cmd.Parameters.AddWithValue("pOrderType", customerOrder.orderDetail.ordertype);
                    cmd.Parameters.AddWithValue("pItemTotalCost", customerOrder.paymentDetails.ordertotalcost);

                    cmd.Parameters.AddWithValue("pTotalAmount", customerOrder.paymentDetails.totalamt);
                    cmd.Parameters.AddWithValue("pMiscellaneouscharges", customerOrder.paymentDetails.misccharges);
                    cmd.Parameters.AddWithValue("pAdvance", customerOrder.paymentDetails.advanceamt);
                    cmd.Parameters.AddWithValue("pBalance", customerOrder.paymentDetails.balanceamt);
                    cmd.Parameters.AddWithValue("pDeliveryMode", customerOrder.paymentDetails.deliverymode);
                    cmd.Parameters.AddWithValue("pParcelCharges", customerOrder.paymentDetails.parcelcharges);
                    cmd.Parameters.AddWithValue("pFittingCharges", customerOrder.paymentDetails.fittingcharges);
                    cmd.Parameters.AddWithValue("pTintCharges", customerOrder.paymentDetails.tintcharges);


                    cmd.Parameters.AddWithValue("pDeliveryDate", customerOrder.paymentDetails.schdeliverydate);
                    cmd.Parameters.AddWithValue("pDeliveryTime", customerOrder.paymentDetails.schdeliverytime );
                    cmd.Parameters.AddWithValue("pSameglassPrescription", customerOrder.orderDetail.sameglasspres);
                    cmd.Parameters.AddWithValue("pProjectCreditCode", "0"); // todo
                    cmd.Parameters.AddWithValue("pChkShippingAddress",customerOrder.patientInfo.chkshippingaddress);
                    cmd.Parameters.AddWithValue("pShippingAddress", customerOrder.patientInfo.shippingaddressjson.ToString());
                    cmd.Parameters.AddWithValue("pGstin", customerOrder.patientInfo.gstin);


                    cmd.Parameters.AddWithValue("pUserId", customerOrder.paymentDetails.userid);
                    cmd.Parameters.AddWithValue("pRemarks", customerOrder.paymentDetails.remarks);
                    cmd.Parameters.AddWithValue("pPaymentMode", customerOrder.paymentDetails.paymentmode);
                    cmd.Parameters.AddWithValue("pPaymentModeRemarks", customerOrder.paymentDetails.paymentmoderemarks);

                    cmd.CommandType = CommandType.StoredProcedure;
                 
                    
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUDCustomerOrder - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUDCustomerOrderItems(CustomerOrderProductsInput productsInput)
        {
            MySqlCommand cmd;
            try
            {
                    string sql = "CRUD_CustomerOrderItems";
                    cmd = new MySqlCommand(sql);
                    cmd.Parameters.AddWithValue("pOrgId", productsInput.orgid);
                    cmd.Parameters.AddWithValue("pZoneId", productsInput.zoneid);
                    cmd.Parameters.AddWithValue("pLocationId", productsInput.locationid);
                    cmd.Parameters.AddWithValue("pStoreId", "0"); 

                    cmd.Parameters.AddWithValue("pOrderId", productsInput.orderid);                    
                    cmd.Parameters.AddWithValue("pCategoryId", productsInput.categoryid);
                    cmd.Parameters.AddWithValue("pProductId", productsInput.productid);
                    cmd.Parameters.AddWithValue("pBarcode", productsInput.barcode);

                    cmd.Parameters.AddWithValue("pBatchNo", productsInput.batchno);
                    cmd.Parameters.AddWithValue("pExpiryDate", productsInput.expirydate);
                    cmd.Parameters.AddWithValue("pItemType", productsInput.itemtype);
                    cmd.Parameters.AddWithValue("pOwnItem", productsInput.ownitem);
                    cmd.Parameters.AddWithValue("pQty", productsInput.qty);
                    cmd.Parameters.AddWithValue("pItemCost", productsInput.itemcost);
                    cmd.Parameters.AddWithValue("pTotalCost", productsInput.cost);
                    cmd.Parameters.AddWithValue("pAttributes", productsInput.attributes.ToString());
                    cmd.Parameters.AddWithValue("pRemarks", productsInput.remarks);
                    cmd.Parameters.AddWithValue("pIsCancelled", productsInput.iscancelled);
                    cmd.Parameters.AddWithValue("pRefundAmount", productsInput.refundamount);
                    cmd.Parameters.AddWithValue("pPOOrderid", productsInput.po_orderid);
                    cmd.Parameters.AddWithValue("pPOOrdernumber", productsInput.po_ordernumber);
                    cmd.Parameters.AddWithValue("pDeliveryCounterId",productsInput.deliverycounterid );

                    cmd.CommandType = CommandType.StoredProcedure;
                  
                    
                }            
                catch (Exception ex)
                {
                    Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUDCustomerOrderProducts - Exception: : {ex}");
                    throw ex;
                }

            return cmd;
        }
        public MySqlCommand CRUDCustomerDirectOrderCancel(CustomerOrderProductsInput productsInput)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_CustomerDirectOrderCancel";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pOrgId", productsInput.orgid);
                cmd.Parameters.AddWithValue("pZoneId", productsInput.zoneid);
                cmd.Parameters.AddWithValue("pLocationId", productsInput.locationid);
                cmd.Parameters.AddWithValue("pCustomerOrderId", productsInput.orderid);
                cmd.Parameters.AddWithValue("pcategoryId", productsInput.categoryid);
                cmd.Parameters.AddWithValue("pBarCode", productsInput.barcode);

                cmd.Parameters.AddWithValue("pBatchNo", productsInput.batchno);                
                cmd.Parameters.AddWithValue("pItemType", productsInput.itemtype);
                cmd.Parameters.AddWithValue("pOwnItem", productsInput.ownitem);
                cmd.Parameters.AddWithValue("pQty", productsInput.qty);
                cmd.Parameters.AddWithValue("pCost", productsInput.cost);
                cmd.Parameters.AddWithValue("pAttributes", productsInput.attributes.ToString());
                cmd.Parameters.AddWithValue("pRemarks", productsInput.remarks);
                cmd.Parameters.AddWithValue("pIsCancelled", productsInput.iscancelled);
                cmd.Parameters.AddWithValue("pRefundamount", productsInput.refundamount);
                cmd.CommandType = CommandType.StoredProcedure;

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUDCustomerDirectOrderCancel - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUDCustomerOrderCancel(CustomerOrder customerOrder)
        {            
            MySqlCommand cmd;
            try
            {
                    string sql = "CRUD_CustomerOrderCancel";
                    cmd = new MySqlCommand(sql);
                    cmd.Parameters.AddWithValue("pOrgId", customerOrder.orgid);
                    cmd.Parameters.AddWithValue("pZoneid", customerOrder.zoneid);     
                    cmd.Parameters.AddWithValue("pCounterId", customerOrder.header.counterid);
                    cmd.Parameters.AddWithValue("pCustomerOrderId", customerOrder.customerorderid);                    
                    cmd.Parameters.AddWithValue("pCancelReason", customerOrder.orderCancel.cancelreason);
                    cmd.Parameters.AddWithValue("pCancelUserId", customerOrder.paymentDetails.userid);
                    cmd.Parameters.AddWithValue("pCancelRecomBy", customerOrder.orderCancel.recommendedby);

                    cmd.Parameters.AddWithValue("pFrameReuse", customerOrder.orderDetail.frame.reuse);
                    cmd.Parameters.AddWithValue("pFrameReuseAmt", customerOrder.orderDetail.frame.refundamt);
                    cmd.Parameters.AddWithValue("pRLensReuse", customerOrder.orderDetail.righteye.reuse);
                    cmd.Parameters.AddWithValue("pRLensReuseAmt", customerOrder.orderDetail.righteye.refundamt);
                    cmd.Parameters.AddWithValue("pLLensReuse", customerOrder.orderDetail.lefteye.reuse);
                    cmd.Parameters.AddWithValue("pLLensReuseAmt", customerOrder.orderDetail.lefteye.refundamt);
                    cmd.Parameters.AddWithValue("pMiscChargesRefundAmt", customerOrder.paymentDetails.miscchargesrefundamt);
                    cmd.Parameters.AddWithValue("pParcelChargesRefundAmt", customerOrder.paymentDetails.parcelchargesrefundamt);
                    cmd.Parameters.AddWithValue("pFittingChargesRefundAmt", customerOrder.paymentDetails.fittingchargesrefundamt);
                    cmd.Parameters.AddWithValue("pTintChargesRefundAmt", customerOrder.paymentDetails.tintchargesrefundamt);
                    cmd.Parameters.AddWithValue("pDirectSale", customerOrder.isdirectsale);


                    cmd.CommandType = CommandType.StoredProcedure;                  
                                

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUDCustomerOrderCancel - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUDCustomerOrderDelivery(CustomerOrder customerOrder)
        {
            MySqlCommand cmd;
            try
            {
                    string sql = "CRUD_CustomerOrderDelivery";
                    cmd = new MySqlCommand(sql);
                    cmd.Parameters.AddWithValue("pOrgId", customerOrder.orgid);
                    cmd.Parameters.AddWithValue("pZoneId", customerOrder.zoneid);
                    cmd.Parameters.AddWithValue("pLocationId", customerOrder.locationid);
                    cmd.Parameters.AddWithValue("pCounterId", customerOrder.header.counterid);
                    cmd.Parameters.AddWithValue("pCustomerOrderId", customerOrder.customerorderid);
                    cmd.Parameters.AddWithValue("pPaymentMode", customerOrder.orderDelivery.paymentmode);
                    cmd.Parameters.AddWithValue("pPaymentModeRemarks", customerOrder.orderDelivery.paymentmoderemarks);
                    cmd.Parameters.AddWithValue("pBalance", customerOrder.orderDelivery.balanceamt);                    
                    cmd.Parameters.AddWithValue("pDeliverydtm", customerOrder.orderDelivery.deliverydtm);                    
                    cmd.Parameters.AddWithValue("pUserId", customerOrder.orderDelivery.userid);
                    cmd.Parameters.AddWithValue("pDeliveryCounterId", customerOrder.orderDelivery.deliverycounterid);

                    cmd.CommandType = CommandType.StoredProcedure;                    
                

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUDCustomerOrderDelivery - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUDCustomerOrderDiscount(CustomerOrder customerOrder)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_CustomerOrderDiscount";
                    cmd = new MySqlCommand(sql);
                   
                    cmd.Parameters.AddWithValue("pCustomerOrderId", customerOrder.customerorderid);
                    cmd.Parameters.AddWithValue("pDiscCategoryCode", customerOrder.orderDiscount.discountcategory);     
                    cmd.Parameters.AddWithValue("pDiscReason", customerOrder.orderDiscount.discountreason);
                    cmd.Parameters.AddWithValue("pDiscRecomBy", customerOrder.orderDiscount.recommendedby);
                    cmd.Parameters.AddWithValue("pDiscUserId", customerOrder.paymentDetails.userid);

                    cmd.Parameters.AddWithValue("pFrameDiscounted", (customerOrder.orderDetail.frame.discountedamount != "0") ? "1" : "0");
                    cmd.Parameters.AddWithValue("pFrameDiscAmt", customerOrder.orderDetail.frame.discountedamount);
                    cmd.Parameters.AddWithValue("pLLensDiscounted", (customerOrder.orderDetail.lefteye.discountedamount != "0") ? "1" : "0");
                    cmd.Parameters.AddWithValue("pLLensDiscAmt", customerOrder.orderDetail.lefteye.discountedamount);
                    cmd.Parameters.AddWithValue("pRLensDiscounted", (customerOrder.orderDetail.righteye.discountedamount != "0") ? "1" : "0");
                    cmd.Parameters.AddWithValue("pRLensDiscAmt", customerOrder.orderDetail.righteye.discountedamount);
                    
                    cmd.CommandType = CommandType.StoredProcedure;                   
                               

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUDCustomerOrderDiscount - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }
        /*
        public DataSet PurchaseTransactions(PurchaseTrans objInput)
        {
            DataSet ds = new DataSet();
            MySqlTransaction myTrans = null;
            MySqlConnection conn = null;

            try
            {

                using (conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "CRUD_PurchaseEntry";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pZoneId", objInput.zoneid);
                    cmd.Parameters.AddWithValue("pTransType", objInput.type);
                    cmd.Parameters.AddWithValue("pSupplierId", objInput.supplierid);
                    cmd.Parameters.AddWithValue("pDCNumber", objInput.dcinvoiceno);

                    DateTime date1;
                    if (!(String.IsNullOrEmpty(objInput.timezoneid)))
                        date1 = Extension.ConvertToNativeTimeZone(objInput.timezoneid, objInput.invoicedtm); //sakthipriya
                    else
                        date1 = Convert.ToDateTime(objInput.invoicedtm);

                    cmd.Parameters.AddWithValue("pInvoiceDtm", date1);
                    //cmd.Parameters.AddWithValue("pInvoiceDtm", objInput.invoicedtm);
                    cmd.Parameters.AddWithValue("pTotalQty", objInput.totalqty);
                    cmd.Parameters.AddWithValue("pTotalValue", objInput.totalamt);
                    cmd.Parameters.AddWithValue("pTotalDiscount", objInput.totaldiscount);
                    cmd.Parameters.AddWithValue("pgstAmt", objInput.gstamt);

                    cmd.Parameters.AddWithValue("pOtherCharges", objInput.othercharges);
                    cmd.Parameters.AddWithValue("pFreightCharges", objInput.freightcharges);
                    cmd.Parameters.AddWithValue("pNetVal", objInput.totalamt);
                    cmd.Parameters.AddWithValue("ptcsAmount", objInput.tcsamount);
                    cmd.Parameters.AddWithValue("pRoundOff", objInput.roundoff);

                    cmd.Parameters.AddWithValue("pCashDiscount", objInput.cashdiscount);
                    cmd.Parameters.AddWithValue("pRemarks", objInput.remarks);
                    cmd.Parameters.AddWithValue("pReasonId", objInput.reason);
                    cmd.Parameters.AddWithValue("pModeOfReturn", objInput.modeofreturn);


                    conn.Open();
                    myTrans = conn.BeginTransaction();
                    cmd.Transaction = myTrans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);


                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objInput.purchaseentryid = ds.Tables[0].Rows[0]["purchaseentryid"].ToString();
                        objInput.purchaseorderno = ds.Tables[0].Rows[0]["purchaseorderno"].ToString();
                        objInput.purchaseorderdtm = ds.Tables[0].Rows[0]["purchaseorderdtm"].ToString();
                    }

                    tmpqry = "CRUD_PurchaseEntryTrans";

                    foreach (PurchaseItemdetail detail in objInput.purchaseitemdetails)
                    {
                        cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                        cmd.Parameters.AddWithValue("pZoneId", objInput.zoneid);
                        cmd.Parameters.AddWithValue("pTransType", objInput.type);
                        cmd.Parameters.AddWithValue("pPurchaseEntryId", objInput.purchaseentryid);
                        cmd.Parameters.AddWithValue("pCategoryId", detail.categoryid);
                        cmd.Parameters.AddWithValue("pCategory", detail.category);

                        cmd.Parameters.AddWithValue("pProductId", detail.productid);
                        cmd.Parameters.AddWithValue("pQty", detail.qty);
                        cmd.Parameters.AddWithValue("pBatchno", detail.batchno);
                        cmd.Parameters.AddWithValue("pExpiryDate", detail.expirydate);
                        cmd.Parameters.AddWithValue("pPurchasePrice", detail.purchaseprice);
                        cmd.Parameters.AddWithValue("pDiscountPer", detail.discountperc);

                        cmd.Parameters.AddWithValue("pDiscountVal", detail.discountval);
                        cmd.Parameters.AddWithValue("pPurchasePriceDis", detail.purchasepricediscount);
                        cmd.Parameters.AddWithValue("pSellingPrice", detail.sellingprice);
                        cmd.Parameters.AddWithValue("pgst", detail.gst);
                        cmd.Parameters.AddWithValue("psgstTaxPer", detail.sgsttaxperc);

                        cmd.Parameters.AddWithValue("psgstTaxVal", detail.sgsttaxval);
                        cmd.Parameters.AddWithValue("pcgstTaxPer", detail.cgsttaxperc);
                        cmd.Parameters.AddWithValue("pcgstTaxVal", detail.cgsttaxval);
                        cmd.Parameters.AddWithValue("pigstTaxPer", detail.igsttaxperc);
                        cmd.Parameters.AddWithValue("pigstTaxVal", detail.igsttaxval);
                        cmd.Parameters.AddWithValue("pTotalAmount", detail.totalamount);

                        cmd.Parameters.AddWithValue("phsnCode", detail.hsncode);
                        cmd.Parameters.AddWithValue("pUserId", objInput.userid);

                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                    }
                    myTrans.Commit();

                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of PurchaseTransactions - Exception: : {ex}");

                try
                {
                    myTrans.Rollback();                  
                }
                catch (MySqlException err)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + err.GetType() +
                        " was encountered while attempting to roll back the transaction.");
                    }

                    throw ex;
                }
            }
            finally
            {
                conn.Close();
            }

            return ds;
        }
        */

        public DataSet GetProductDetails(ProductMasterInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "GetProductDetails";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pCategoryId", objInput.categoryid);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);                    
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of GetProductDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetSupplierDetails(SupplierMasterInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "GetSupplierDetails";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);                    
                    cmd.Parameters.AddWithValue("pActive", objInput.active);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of GetSupplierDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetDashboardDetails(DashboardInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "GetDashboardDetails";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);
                    cmd.Parameters.AddWithValue("pUIN", objInput.uin);
                    cmd.Parameters.AddWithValue("pOrderno", objInput.orderno);
                    cmd.Parameters.AddWithValue("pPatientname", objInput.patientname);
                    cmd.Parameters.AddWithValue("pPhoneNumber", objInput.phonenumber);

                    cmd.Parameters.AddWithValue("pDateType", objInput.datetype);

                    cmd.Parameters.AddWithValue("pFromdate", objInput.fromdate);
                    cmd.Parameters.AddWithValue("pTodate", objInput.todate);

                    cmd.Parameters.AddWithValue("pStatus", objInput.status);
                    cmd.Parameters.AddWithValue("pCounter", objInput.counter);
                    cmd.Parameters.AddWithValue("pZoneId", objInput.zoneid);                    
                  
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();

                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : GetDashboardDetails");
                    
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting dashboard details - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetCustomerOrder(string orgid,string locationid, string counterid, string orderno, string finyearid)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetCustomerOrderDetail";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrgId", orgid);
                    cmd.Parameters.AddWithValue("pLocationId", locationid);
                    cmd.Parameters.AddWithValue("pCounterId", counterid);
                    cmd.Parameters.AddWithValue("pOrderNo", orderno);
                    cmd.Parameters.AddWithValue("pFinYearId", finyearid);


                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : GetCustomerOrder");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of GetCustomerOrder - Exception: : {ex}");
                throw ex;
            }

            return ds;            
        }

        public DataSet crudproduct(crudProductDetailsInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "CRUD_ProductMaster";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pProductId", objInput.productid);
                    cmd.Parameters.AddWithValue("pName", objInput.name);
                    cmd.Parameters.AddWithValue("pCategoryId", objInput.categoryid);
                    cmd.Parameters.AddWithValue("pBarCode", objInput.barcode);
                    cmd.Parameters.AddWithValue("pZoneId", objInput.zoneid);
                    cmd.Parameters.AddWithValue("pBrand", objInput.brand);
                    cmd.Parameters.AddWithValue("pColor", objInput.color);
                    cmd.Parameters.AddWithValue("pModel", objInput.model);
                    cmd.Parameters.AddWithValue("pType", objInput.type);
                    cmd.Parameters.AddWithValue("pSize", objInput.size);
                    cmd.Parameters.AddWithValue("pHsncode", objInput.hsncode);
                    cmd.Parameters.AddWithValue("pExternalId", objInput.externalid);
                    cmd.Parameters.AddWithValue("pCompany", objInput.company);
                    cmd.Parameters.AddWithValue("pRatefixingcategoryid", objInput.ratefixingcategoryid);
                    cmd.Parameters.AddWithValue("pPurchasePrice", objInput.purchaseprice);
                    cmd.Parameters.AddWithValue("pSellingPrice", objInput.sellingprice);

                    cmd.Parameters.AddWithValue("pCylpositivestarting", objInput.lensmatrix.cylpositivefrom);
                    cmd.Parameters.AddWithValue("pCylpositiveending", objInput.lensmatrix.cylpositiveto);
                    cmd.Parameters.AddWithValue("pCylnegativestarting", objInput.lensmatrix.cylnegativefrom);
                    cmd.Parameters.AddWithValue("pCylnegativeending", objInput.lensmatrix.cylnegativeto);
                    cmd.Parameters.AddWithValue("ptotalpositivestarting", objInput.lensmatrix.totalpositivefrom);
                    cmd.Parameters.AddWithValue("ptotalpositiveending", objInput.lensmatrix.totalpositiveto);
                    cmd.Parameters.AddWithValue("ptotalnegativestarting", objInput.lensmatrix.totalnegativefrom);
                    cmd.Parameters.AddWithValue("ptotalnegativeending", objInput.lensmatrix.totalnegativeto);
                    cmd.Parameters.AddWithValue("padditionpositivestarting", objInput.lensmatrix.additionpositivefrom);
                    cmd.Parameters.AddWithValue("padditionpositiveending", objInput.lensmatrix.additionpositiveto);
                    cmd.Parameters.AddWithValue("pActive", objInput.active);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudproduct - Exception::: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet crudsupplier(crudSupplierDetailsInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "CRUD_SupplierMaster";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("psupplierId", objInput.supplierId);
                    
                    cmd.Parameters.AddWithValue("pName", objInput.name);
                    cmd.Parameters.AddWithValue("pAddress", objInput.address);
                    cmd.Parameters.AddWithValue("pCity", objInput.city);
                    cmd.Parameters.AddWithValue("pPincode", objInput.pincode);
                    cmd.Parameters.AddWithValue("pHomephone", objInput.homephone);
                    cmd.Parameters.AddWithValue("pOfficePhone", objInput.officephone);
                    cmd.Parameters.AddWithValue("pFax", objInput.fax);
                    cmd.Parameters.AddWithValue("pEmail", objInput.email);
                    cmd.Parameters.AddWithValue("pTngstno", objInput.tngstno);
                    cmd.Parameters.AddWithValue("pCstno", objInput.cstno);
                    cmd.Parameters.AddWithValue("pTrackingCompanyId", objInput.trackingCompanyId);
                    cmd.Parameters.AddWithValue("pTallyType", objInput.tallyType);
                    cmd.Parameters.AddWithValue("pStateId", objInput.stateId);
                    cmd.Parameters.AddWithValue("pActive", objInput.active);
                    cmd.Parameters.AddWithValue("pTaxFree", objInput.taxfree);
                    cmd.Parameters.AddWithValue("pLensSupplier", objInput.lenssupplier);
                    cmd.Parameters.AddWithValue("pCountryId", objInput.countryid);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudsupplier - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        //praveen changes - start - 09-02-2022
        public DataSet GetLookupMasterDetails(LookupMasterDetails objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "GetLookupMasterDetails";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pTblId", objInput.tblId);
                    cmd.Parameters.AddWithValue("pType", objInput.ptype);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during GetLookupMasterDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet crudLookup(crudLookupDetailsInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "CRUD_LookupMaster";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pLookupId", objInput.lookupid);
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);                    
                    cmd.Parameters.AddWithValue("pDisplayName", objInput.displayname);
                    cmd.Parameters.AddWithValue("pShortName", objInput.shortname);
                    cmd.Parameters.AddWithValue("pLookupType", objInput.lookuptype);
                    cmd.Parameters.AddWithValue("pProductAttributes", objInput.productattributes);
                    cmd.Parameters.AddWithValue("pCustom", objInput.custom);                    
                    cmd.Parameters.AddWithValue("pActive", objInput.active);
                    cmd.Parameters.AddWithValue("pDisplayOrder", objInput.displayorder);
                    cmd.Parameters.AddWithValue("pTblId", objInput.tblId);                   

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudLookup - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }


        public DataSet OrderManagement(CustomerOrder customerorder)
        {
            DataSet dsResult = new DataSet();
            MySqlTransaction myTrans = null;
            MySqlConnection conn = null;

            try
            {
                using (conn = new MySqlConnection(connectionString))
                {      
                    
                    MySqlCommand cmd = CRUDCustomer(customerorder.orgid,customerorder.customerid ,customerorder.zoneid,customerorder.locationid,customerorder.patientInfo);

                    cmd.Connection = conn;
                    conn.Open();
                    myTrans = conn.BeginTransaction();
                    cmd.Transaction = myTrans;

                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dsResult);

                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUDCustomer");

                    if (dsResult.Tables[0].Rows.Count > 0)
                      customerorder.customerid = dsResult.Tables[0].Rows[0]["CustomerId"].ToString();

                    dsResult = new DataSet();
                    cmd = CRUDCustomerOrder(customerorder);
                    cmd.Connection = conn;                    
                    da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dsResult);

                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUDCustomerOrder");

                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        customerorder.customerorderid = dsResult.Tables[0].Rows[0]["CustomerOrderId"].ToString();
						customerorder.orderno = dsResult.Tables[0].Rows[0]["OrderNo"].ToString();
                        customerorder.displayorderno = dsResult.Tables[0].Rows[0]["DisplayOrderNumber"].ToString();
                        customerorder.billnumber = dsResult.Tables[0].Rows[0]["DisplayBillNumber"].ToString();
                        customerorder.header.countername = dsResult.Tables[0].Rows[0]["SectionName"].ToString();
                    }

                    DataSet ds = new DataSet();

                    if (customerorder.isdirectsale == "0" && customerorder.isdiscount == "0" && customerorder.isdelivery == "0")
                    {
                        //Insert Frame detail
                        CustomerOrderProductsInput productsInput = new CustomerOrderProductsInput();

                        productsInput.orgid = customerorder.orgid;
                        productsInput.zoneid = customerorder.zoneid;
                        productsInput.locationid = customerorder.locationid;
                        productsInput.counterid = customerorder.header.counterid;
                        productsInput.orderid = customerorder.customerorderid;
                        productsInput.customerid = customerorder.customerid;                       
                        productsInput.categoryid = "0"; //1-Frame
                        productsInput.productid = customerorder.orderDetail.frame.productid;
                        productsInput.barcode = (customerorder.orderDetail.frame.barcode == string.Empty) ? "0" : customerorder.orderDetail.frame.barcode;
                        productsInput.expirydate = customerorder.orderDetail.frame.expirydate;
                        productsInput.itemtype = "frame";
                        productsInput.ownitem = customerorder.orderDetail.frame.ownframe;
                        productsInput.batchno = (customerorder.orderDetail.frame.batchno == string.Empty) ? "0" : customerorder.orderDetail.frame.batchno;
                        productsInput.qty = (customerorder.orderDetail.frame.ownframe == "1") ? "0" : "1"; //Qty will be 0 if it is ownframe otherwise it will be 0
                        productsInput.itemcost = (customerorder.orderDetail.frame.cost == "")?"0": customerorder.orderDetail.frame.cost;
                        productsInput.attributes = "{}";
                        productsInput.remarks = customerorder.orderDetail.frame.ownframeremarks;
                  

                        dsResult = new DataSet();
                        cmd = CRUDCustomerOrderItems(productsInput);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);

                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_CustomerOrderItems for Frame");

                        productsInput = new CustomerOrderProductsInput();
                        productsInput.orgid = customerorder.orgid;
                        productsInput.zoneid = customerorder.zoneid;
                        productsInput.locationid = customerorder.locationid;
                        productsInput.counterid = customerorder.header.counterid;
                        productsInput.orderid = customerorder.customerorderid;
                        productsInput.customerid = customerorder.customerid;
                        productsInput.categoryid = "0"; //2-Lens
                        productsInput.productid = customerorder.orderDetail.righteye.productid;
                        productsInput.barcode = (customerorder.orderDetail.righteye.barcode == string.Empty) ? "0" : customerorder.orderDetail.righteye.barcode;
                        productsInput.itemtype = "rightlens";
                        productsInput.ownitem = customerorder.orderDetail.righteye.ownlens;
                        productsInput.batchno = "0";
                        productsInput.qty = (customerorder.orderDetail.righteye.ownlens == "1") ? "0" : "1"; //Qty will be 0 if it is ownlens otherwise it will be 0
                        productsInput.itemcost = customerorder.orderDetail.righteye.cost;
                        
                        productsInput.attributes = customerorder.orderDetail.righteye.attributesjson;
                        productsInput.remarks = customerorder.orderDetail.righteye.ownlensremarks;

                        dsResult = new DataSet();
                        cmd = CRUDCustomerOrderItems(productsInput);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);

                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_CustomerOrderItems for rightlens");

                        productsInput = new CustomerOrderProductsInput();
                        productsInput.orgid = customerorder.orgid;
                        productsInput.zoneid = customerorder.zoneid;
                        productsInput.locationid = customerorder.locationid;
                        productsInput.counterid = customerorder.header.counterid;
                        productsInput.orderid = customerorder.customerorderid;
                        productsInput.customerid = customerorder.customerid;
                        productsInput.productid = customerorder.orderDetail.lefteye.productid;
                        productsInput.barcode = (customerorder.orderDetail.lefteye.barcode == string.Empty) ? "0" : customerorder.orderDetail.lefteye.barcode;
                        productsInput.categoryid = "0"; //2-Lens
                        productsInput.itemtype = "leftlens";
                        productsInput.ownitem = customerorder.orderDetail.lefteye.ownlens;
                        productsInput.batchno = "0";
                        productsInput.qty = (customerorder.orderDetail.lefteye.ownlens == "1") ? "0" : "1"; //Qty will be 0 if it is ownlens otherwise it will be 0                
                        productsInput.itemcost = customerorder.orderDetail.lefteye.cost;
                        productsInput.attributes = customerorder.orderDetail.lefteye.attributesjson;
                        productsInput.remarks = customerorder.orderDetail.lefteye.ownlensremarks;

                        dsResult = new DataSet();
                        cmd = CRUDCustomerOrderItems(productsInput);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);

                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_CustomerOrderItems for leftlens");
                    }
                    else if (customerorder.isdirectsale == "1")
                    {
                        foreach (DirectSale directsale in customerorder.directSales)
                        {
                            CustomerOrderProductsInput productsInput = new CustomerOrderProductsInput();
                            productsInput.orgid = customerorder.orgid;
                            productsInput.zoneid = customerorder.zoneid;
                            productsInput.locationid = customerorder.locationid;
                            productsInput.counterid = customerorder.header.counterid;
                            productsInput.orderid = customerorder.customerorderid;
                            productsInput.customerid = customerorder.customerid;
               
                            productsInput.categoryid = directsale.categoryid;
                            productsInput.productid = directsale.productid;
                            productsInput.barcode = directsale.barcode;
                            productsInput.itemtype = directsale.categorydesc;
                            productsInput.batchno = (directsale.batchno == string.Empty) ? "0" : directsale.batchno;
                            productsInput.expirydate = directsale.expirydate;
                            productsInput.qty = directsale.qty;
                            productsInput.itemcost = directsale.sellingprice;
                            productsInput.cost = directsale.cost;
                            productsInput.attributes = "{}";
                            productsInput.remarks = "";
                            productsInput.refundamount = (directsale.refundamount == string.Empty) ? "0" : directsale.refundamount; 
                            productsInput.iscancelled = (customerorder.iscancelled == string.Empty) ? "0" : customerorder.iscancelled;

                            dsResult = new DataSet();
                            if (customerorder.iscancelled == "0")
                                cmd = CRUDCustomerOrderItems(productsInput);
                            else
                                cmd = CRUDCustomerDirectOrderCancel(productsInput);

                            cmd.Connection = conn;
                            da = new MySqlDataAdapter();
                            da.SelectCommand = cmd;
                            da.Fill(dsResult);

                        }
                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_CustomerOrderItems for directsales (all products)");
                    }
                    
                    if (customerorder.isdiscount == "1" && customerorder.iscancelled == "0")
                    {
                        customerorder.orderDetail.frame.discountedamount = (customerorder.orderDetail.frame.discountedamount == String.Empty) ? "0" : customerorder.orderDetail.frame.discountedamount;
                        customerorder.orderDetail.lefteye.discountedamount = (customerorder.orderDetail.lefteye.discountedamount == String.Empty) ? "0" : customerorder.orderDetail.lefteye.discountedamount;
                        customerorder.orderDetail.righteye.discountedamount = (customerorder.orderDetail.righteye.discountedamount == String.Empty) ? "0" : customerorder.orderDetail.righteye.discountedamount;

                        dsResult = new DataSet();
                        cmd = CRUDCustomerOrderDiscount(customerorder);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);

                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUDCustomerOrderDiscount");

                    }
                    if (customerorder.isdelivery == "1" && customerorder.iscancelled == "0")
                    {
                        //Insert Frame detail
                        CustomerOrderProductsInput productsInput = new CustomerOrderProductsInput();

                        productsInput.orgid = customerorder.orgid;
                        productsInput.zoneid = customerorder.zoneid;
                        productsInput.locationid = customerorder.locationid;
                        productsInput.counterid = customerorder.header.counterid;
                        productsInput.orderid = customerorder.customerorderid;
                        productsInput.customerid = customerorder.customerid;
                        productsInput.categoryid = "3"; //1-Frame
                        productsInput.productid = customerorder.orderDetail.deliverybox.productid;
                        productsInput.barcode = (customerorder.orderDetail.deliverybox.barcode == string.Empty) ? "0" : customerorder.orderDetail.deliverybox.barcode;
                        productsInput.expirydate = customerorder.orderDetail.deliverybox.expirydate;
                        productsInput.itemtype = "deliverybox";
                        productsInput.ownitem = "0";
                        productsInput.batchno = (customerorder.orderDetail.deliverybox.batchno == string.Empty) ? "0" : customerorder.orderDetail.deliverybox.batchno;
                        productsInput.qty =  "1"; //Qty will be 0 if it is ownframe otherwise it will be 0
                        productsInput.itemcost = "0";
                        productsInput.attributes = "{}";
                        productsInput.remarks = "";
                        productsInput.deliverycounterid = customerorder.orderDelivery.deliverycounterid;


                        dsResult = new DataSet();
                        cmd = CRUDCustomerOrderItems(productsInput);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);
                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_CustomerOrderItems for deliverybox");


                        productsInput = new CustomerOrderProductsInput();

                        productsInput.orgid = customerorder.orgid;
                        productsInput.zoneid = customerorder.zoneid;
                        productsInput.locationid = customerorder.locationid;
                        productsInput.counterid = customerorder.header.counterid;
                        productsInput.orderid = customerorder.customerorderid;
                        productsInput.customerid = customerorder.customerid;
                        productsInput.categoryid = "0"; //1-Frame
                        productsInput.productid = customerorder.orderDetail.deliverycloth.productid;
                        productsInput.barcode = (customerorder.orderDetail.deliverycloth.barcode == string.Empty) ? "0" : customerorder.orderDetail.deliverycloth.barcode;
                        productsInput.expirydate = customerorder.orderDetail.deliverycloth.expirydate;
                        productsInput.itemtype = "deliverycloth";
                        productsInput.ownitem = "0";
                        productsInput.batchno = (customerorder.orderDetail.deliverycloth.batchno == string.Empty) ? "0" : customerorder.orderDetail.deliverycloth.batchno;
                        productsInput.qty = "1"; //Qty will be 0 if it is ownframe otherwise it will be 0
                        productsInput.itemcost = "0";
                        productsInput.attributes = "{}";
                        productsInput.remarks = "";
                        productsInput.deliverycounterid = customerorder.orderDelivery.deliverycounterid;


                        dsResult = new DataSet();
                        cmd = CRUDCustomerOrderItems(productsInput);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);
                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_CustomerOrderItems for deliverycloth");


                        dsResult = new DataSet();
                        cmd = CRUDCustomerOrderDelivery(customerorder);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);
                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUDCustomerOrderDelivery");


                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            customerorder.billnumber = dsResult.Tables[0].Rows[0]["billnumber"].ToString();                            
                        }
                    }

                    if (customerorder.iscancelled == "1")
                    {
                        dsResult = new DataSet();
                        cmd = CRUDCustomerOrderCancel(customerorder);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);
                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUDCustomerOrderCancel");
                    }
                    myTrans.Commit();
                }               

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during OrderManagement - Exception: : {ex}");

                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                    }
                    throw ex;
                }
                catch (MySqlException err)
                {
                    if (myTrans != null && myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + err.GetType() +
                        " was encountered while attempting to roll back the transaction.");
                    }

                    throw ex;
                }
            }
            finally
            {
                if (myTrans != null && myTrans.Connection != null)
                {
                    conn.Close();
                }
            }

            return dsResult;

        }

        
        public DataSet GetUserDetails(string orgid,string locationid,string roleid)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "GetUserDetails";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", orgid);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during GetUserDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }
        public DataSet crudUserDetails(crudUserDetailsInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                string encryptedPassword = CommonConstraints.Encrypt(objInput.password, true);
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "CRUD_User";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pUserId", objInput.userid);
                    cmd.Parameters.AddWithValue("pUserName", objInput.username);
                    cmd.Parameters.AddWithValue("pPassword", encryptedPassword);                    
                    cmd.Parameters.AddWithValue("pDisplayname", objInput.displayname);                    
                    cmd.Parameters.AddWithValue("pActive", objInput.active);                   


                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudUserDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetRecentPurchaseDetails(string orgid, string zoneid, string locationid, string category, string transtype)
        {

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetRecentPurchaseDetails";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrgId", orgid);
                    cmd.Parameters.AddWithValue("pZoneId", zoneid);                                        
                    cmd.Parameters.AddWithValue("pLocationId", locationid);      
   				    cmd.Parameters.AddWithValue("pCategory", category);                 
                    cmd.Parameters.AddWithValue("pTransType", transtype);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : GetRecentPurchaseDetails");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting GetRecentPurchaseDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;

        }

        public DataSet getorderpaymentdetails(string orderid)
        {

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "getorderpaymentdetails";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pTenantId", orderid);               
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : getorderpaymentdetails");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting getorderpaymentdetails - Exception: : {ex}");
                throw ex;
            }

            return ds;

        }

        public DataSet GetSupplierTransactionHistory(SupplierTransactionHistoryInput input)
        {

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetSupplierTransactionHistory";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrgid", input.orgid);
                    cmd.Parameters.AddWithValue("pLocationid", input.locationid);
                    cmd.Parameters.AddWithValue("pBarcode", input.barcode);
                    cmd.Parameters.AddWithValue("pCategoryId", input.categoryid);
                    cmd.Parameters.AddWithValue("pTransType", input.transtype);
                    cmd.Parameters.AddWithValue("pHistoryType", input.historytype);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : getorderpaymentdetails");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting getorderpaymentdetails - Exception: : {ex}");
                throw ex;
            }

            return ds;

        }

       

        public DataSet GetSupplierTransactionDetails(string transtype, string orderid)
        {

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetSupplierTransactionDetails";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pTransType", transtype);
                    cmd.Parameters.AddWithValue("pSupplierorderid", orderid);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : getorderpaymentdetails");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting getorderpaymentdetails - Exception: : {ex}");
                throw ex;
            }

            return ds;

        }

        public DataSet GetStoreTransactionDetails(string transtype, string orderid)
        {

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetStoreTransactionDetails";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pTransType", transtype);
                    cmd.Parameters.AddWithValue("pStoreorderid", orderid);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : getorderpaymentdetails");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting getorderpaymentdetails - Exception: : {ex}");
                throw ex;
            }

            return ds;

        }

        public DataSet GetSupplierOrderWorklist(SupplierOrderWorklistInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetSupplierOrderWorklist";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);                    
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);
                    cmd.Parameters.AddWithValue("pStoreId", objInput.storeid);
                    cmd.Parameters.AddWithValue("pTransType", objInput.transtype);
                    cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                    cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                    cmd.Parameters.AddWithValue("pSupplierId", objInput.supplierid);
                    cmd.Parameters.AddWithValue("pCategoryId", objInput.categoryid);
                    cmd.Parameters.AddWithValue("pStockType", objInput.stocktype);
                    cmd.Parameters.AddWithValue("pSupplierOrderStatusId", objInput.supplierorderstatusid);


                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : GetSupplierOrderWorklist");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of GetSupplierOrderWorklist - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetStoreOrderWorklist(StoreOrderWorklistInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetStoreOrderWorklist";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);
                    cmd.Parameters.AddWithValue("pCategoryId", objInput.categoryid);
                    cmd.Parameters.AddWithValue("pFromId", objInput.fromid);
                    cmd.Parameters.AddWithValue("pToId", objInput.toid);
                    cmd.Parameters.AddWithValue("pStockOrderStatusId", objInput.stockorderstatusid);
                    cmd.Parameters.AddWithValue("pTransType", objInput.transtype);
                    cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                    cmd.Parameters.AddWithValue("pToDate", objInput.todate);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : GetSupplierOrderWorklist");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of GetSupplierOrderWorklist - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }


        public DataSet GetSupplierOrderItemDetails(string transtype, string supplierorderid)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetSupplierOrderProductDetails";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);                  
                    cmd.Parameters.AddWithValue("pTransType", transtype);
                    cmd.Parameters.AddWithValue("pSupplierOrderId", supplierorderid);                 
                    
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : GetSupplierOrderWorklist");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of GetSupplierOrderWorklist - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }


        //praveen chnages - 14-03-2022
        public DataSet MISreport(MISReportInput objInput)
        {
            DataSet ds = new DataSet();
            string reportypename = string.Empty;
            try
            {
                reportypename = objInput.reportType.ToString().ToUpper();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "";
                    if (reportypename == "ORDERENTRYPRINT") // Order Entry Print Out 
                    {
                        tmpqry = "PrtOrderEntry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.sectionid);
                        cmd.Parameters.AddWithValue("pOrderNumber", objInput.ordernumber.Replace("%2F", "/"));
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }
                    if (reportypename == "DELIVERYENTRYPRINT") // Order Delivery Print Out 
                    {

                        tmpqry = "PrtDeliveryEntry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.sectionid);
                        cmd.Parameters.AddWithValue("pBillNumber", objInput.billnumber.Replace("%2F", "/"));
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();
                    }
                    if (reportypename == "PURCHASEENTRYPRINT") // purchse entry Print Out 
                    {

                        tmpqry = "prtPurchaseEntry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pPurchaseNumber", objInput.purchasenumber.Replace("%2F", "/"));
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();

                    }
                    if (reportypename == "PURCHASERETURNPRINT") // purchase return  Entry Print Out 
                    {
                        tmpqry = "prtPurchaseReturnEntry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pPurchaseReturnNumber", objInput.purchasereturnnumber.Replace("%2F", "/"));
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();

                    }

                    if (reportypename == "PURCHASEREPORT") // purchase report  purchase repor
                    {
                        tmpqry = "RptPurchase";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pFromTime", objInput.fromtime);
                        cmd.Parameters.AddWithValue("pToTime", objInput.totime);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();

                    }

                    if (reportypename == "PURCHASERETURNREPORT") // purchase RETURN  report  
                    {
                        tmpqry = "RptPurchaseReturn";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pFromTime", objInput.fromtime);
                        cmd.Parameters.AddWithValue("pToTime", objInput.totime);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pSupplierId", objInput.SupplierId);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();

                    }

                    if (reportypename == "ORDERCOLLECTIONREPORT") // Order Collection Report 
                    {
                        tmpqry = "RptOrderCollection";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pFromTime", objInput.fromtime);
                        cmd.Parameters.AddWithValue("pToTime", objInput.totime);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.sectionid);
                        cmd.Parameters.AddWithValue("pPaymentId", objInput.paymentid);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();

                    }

                    if (reportypename == "DELIVERYCOLLECTIONREPORT") // DELIVERY Collection Report 
                    {
                        tmpqry = "RptDeliveryCollection";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pFromTime", objInput.fromtime);
                        cmd.Parameters.AddWithValue("pToTime", objInput.totime);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.sectionid);
                        cmd.Parameters.AddWithValue("pPaymentId", objInput.paymentid);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();

                    }
                    if (reportypename == "ORDERCANCELLATIONREPORT") // Order CANCELLATION Report 
                    {
                        tmpqry = "RptOrderCancellation";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pFromTime", objInput.fromtime);
                        cmd.Parameters.AddWithValue("pToTime", objInput.totime);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.sectionid);
                        cmd.Parameters.AddWithValue("pPaymentId", objInput.paymentid);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();

                    }

                    if (reportypename == "STOCKMOVEMENTREPORT") // batchwise stock movement report
                    {
                        tmpqry = "RptStock";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.sectionid);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();

                    }

                    //praveen changes - 21-03-2022
                    if (reportypename == "STOCKMOVEMENTBATCHREPORT") // batchwise stock movement report
                    {
                        tmpqry = "RptStockBatchwise";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.sectionid);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();

                    }
                    //praveen changes - 21-03-2022

                    //praveen changes - 06-04-2022
                    if (reportypename == "UNDELIVERYREPORT") // undelivery report
                    {
                        tmpqry = "RptUndelivery";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.sectionid);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();

                    }

                    if (reportypename == "EXPIRYREPORT") // EXPIRY report
                    {
                        tmpqry = "RptExpiry";
                        MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                        cmd.Parameters.AddWithValue("pTenentId", objInput.tenentid);
                        cmd.Parameters.AddWithValue("pEntityid", objInput.entityid);
                        cmd.Parameters.AddWithValue("pFromDate", objInput.fromdate);
                        cmd.Parameters.AddWithValue("pToDate", objInput.todate);
                        cmd.Parameters.AddWithValue("pSectionId", objInput.sectionid);
                        cmd.Parameters.AddWithValue("pCategoryId", objInput.CategoryId);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        conn.Close();

                    }

                    //praveen changes - 06-04-2022


                    //a1
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : MIS Report - " + reportypename);

                }

            }
            catch (Exception ex)
            {
                //a2 
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting MIS Report {reportypename} - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        //praveen changes - 14-03-2022

        public MySqlCommand CRUD_SupplierOrder(SupplierTransaction suppliertrans)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_SupplierOrder";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pSupplierOrderId", suppliertrans.orderid);
                cmd.Parameters.AddWithValue("pType", suppliertrans.type);
                cmd.Parameters.AddWithValue("pOrgId", suppliertrans.orgid);
                cmd.Parameters.AddWithValue("pZoneId", suppliertrans.zoneid);
                cmd.Parameters.AddWithValue("pLocationId", suppliertrans.locationid);
                cmd.Parameters.AddWithValue("pStoreId", suppliertrans.generaldetails.storeid);

                cmd.Parameters.AddWithValue("pParentsupplierorderid", suppliertrans.parentsupplierorderid);
                cmd.Parameters.AddWithValue("pParentsupplierordernumber", suppliertrans.parentsupplierordernumber);
                cmd.Parameters.AddWithValue("pSupplierId", suppliertrans.generaldetails.supplierid);
                cmd.Parameters.AddWithValue("pFromStoreId", suppliertrans.generaldetails.storeid);
                cmd.Parameters.AddWithValue("pCategoryId", suppliertrans.generaldetails.categoryid);

                cmd.Parameters.AddWithValue("pOrderStatus", suppliertrans.orderstatus); 
                cmd.Parameters.AddWithValue("pStockType", suppliertrans.generaldetails.stocktype); 
                cmd.Parameters.AddWithValue("pBillType", suppliertrans.generaldetails.billtype);
                cmd.Parameters.AddWithValue("pBillRefNumber", suppliertrans.generaldetails.billrefno);
                cmd.Parameters.AddWithValue("pBillDate", suppliertrans.generaldetails.billdate);
                cmd.Parameters.AddWithValue("pTotalQty", suppliertrans.summary.totalquantity);

                cmd.Parameters.AddWithValue("pTotalAmount", suppliertrans.summary.totalamount);
                cmd.Parameters.AddWithValue("pTotalDiscount", suppliertrans.summary.totaldiscount);
                cmd.Parameters.AddWithValue("pGSTPercentage", "0"); //todo
                cmd.Parameters.AddWithValue("pExpiryDate",suppliertrans.summary.expirydate);
                cmd.Parameters.AddWithValue("pGSTAmount", suppliertrans.summary.gstamount);

                cmd.Parameters.AddWithValue("pOtherCharges", suppliertrans.summary.othercharges);
                cmd.Parameters.AddWithValue("pFreightCharges", suppliertrans.summary.freightcharges);
                cmd.Parameters.AddWithValue("pCashDiscount", suppliertrans.summary.cashdiscount);
                cmd.Parameters.AddWithValue("pRoundOff", suppliertrans.summary.roundoff);
                cmd.Parameters.AddWithValue("pNetVal", suppliertrans.summary.netval);
                   
                cmd.Parameters.AddWithValue("pRemarks", suppliertrans.editdetails.remarks);
                cmd.Parameters.AddWithValue("pReturnReasonId", suppliertrans.summary.returnreason);
                cmd.Parameters.AddWithValue("pReturnMode", suppliertrans.summary.returnmode);
                cmd.Parameters.AddWithValue("pReturnType", suppliertrans.summary.returntype);
                cmd.Parameters.AddWithValue("pIsDeleted", "0"); //todo
                cmd.Parameters.AddWithValue("pIsCancelled", suppliertrans.iscancelled);
                cmd.Parameters.AddWithValue("pIsAborted", suppliertrans.isaborted);
                cmd.Parameters.AddWithValue("pUserId", suppliertrans.userid);

                cmd.CommandType = CommandType.StoredProcedure;
                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_SupplierOrder - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUD_SupplierOrderItems(SupplierTransaction suppliertrans, SupplierTransProductdetail suppliertransproduct)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_SupplierOrderItems";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pOrderItemId", suppliertransproduct.orderitemid);
                cmd.Parameters.AddWithValue("pType", suppliertrans.type);
                cmd.Parameters.AddWithValue("pSupplierorderid", suppliertrans.orderid);
                cmd.Parameters.AddWithValue("pProductId", suppliertransproduct.productid);
                cmd.Parameters.AddWithValue("pBarcode", suppliertransproduct.barcode);
                cmd.Parameters.AddWithValue("pQty", suppliertransproduct.requestedquantity);

                cmd.Parameters.AddWithValue("pReceivedqty", suppliertransproduct.receivedquantity);
                cmd.Parameters.AddWithValue("pPurchaseprice", suppliertransproduct.purchaseprice);
                cmd.Parameters.AddWithValue("pTotalAmount", suppliertransproduct.totalamount);
                cmd.Parameters.AddWithValue("pStockType", suppliertrans.generaldetails.stocktype);
                cmd.Parameters.AddWithValue("pBatchNo", suppliertransproduct.batchno);

                cmd.Parameters.AddWithValue("pExpiryDate", suppliertransproduct.expirydate);
                if (suppliertrans.type == "1")
                {
                    cmd.Parameters.AddWithValue("pPOOrderId", 0);
                    cmd.Parameters.AddWithValue("pPoOrderNumber", "");
                }
                else if(suppliertrans.type == "2")
                {
                    cmd.Parameters.AddWithValue("pPOOrderId", suppliertrans.parentsupplierorderid);
                    cmd.Parameters.AddWithValue("pPoOrderNumber", suppliertrans.parentsupplierordernumber); 
                }
                else
                {
                    if (suppliertransproduct.supplierinwardorderid == "")
                        cmd.Parameters.AddWithValue("pPOOrderId", 0);
                    else
                        cmd.Parameters.AddWithValue("pPOOrderId", suppliertransproduct.supplierinwardorderid);
                    
                    cmd.Parameters.AddWithValue("pPoOrderNumber", suppliertransproduct.supplierinwardnumber); 
                }
                cmd.Parameters.AddWithValue("pDiscountPer", suppliertransproduct.discountperc);
                cmd.Parameters.AddWithValue("pDiscountVal", suppliertransproduct.discountval);

                cmd.Parameters.AddWithValue("pPurchasePriceDiscount", suppliertransproduct.purchasepricediscount);
                cmd.Parameters.AddWithValue("pSellingPrice", suppliertransproduct.sellingprice);
                cmd.Parameters.AddWithValue("pgst",suppliertransproduct.gst);
                cmd.Parameters.AddWithValue("psgstTaxPer", suppliertransproduct.sgsttaxperc);
                cmd.Parameters.AddWithValue("psgstTaxVal", suppliertransproduct.sgsttaxval);

                cmd.Parameters.AddWithValue("pcgstTaxPer", suppliertransproduct.cgsttaxperc);
                cmd.Parameters.AddWithValue("pcgstTaxVal", suppliertransproduct.cgsttaxval);
                cmd.Parameters.AddWithValue("pigstTaxPer", suppliertransproduct.igsttaxperc);
                cmd.Parameters.AddWithValue("pigstTaxVal", suppliertransproduct.igsttaxval);                
                cmd.Parameters.AddWithValue("phsnCode", suppliertransproduct.hsncode);

                cmd.Parameters.AddWithValue("pIsDeleted", suppliertransproduct.isdeleted);
                cmd.Parameters.AddWithValue("pUserId", suppliertrans.userid);               

                cmd.CommandType = CommandType.StoredProcedure;
                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_SupplierOrderItems - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUD_ConsumptionOrderDetails(ConsumptionOrderDetails orderdetails)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_ConsumptionOrderDetails";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pOrderStatusTrackingId", orderdetails.orderstatustrackingid);
                cmd.Parameters.AddWithValue("pOrderItemId", orderdetails.orderitemid);

                cmd.CommandType = CommandType.StoredProcedure;

                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_ConsumptionOrderDetails - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }


        public MySqlCommand CRUD_SupplierOrderActions_Edit(SupplierTransaction suppliertrans)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_SupplierOrderActions";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pSupplierorderid", suppliertrans.orderid);
                cmd.Parameters.AddWithValue("pActionType", suppliertrans.editdetails.actiontype);
                cmd.Parameters.AddWithValue("pReason", suppliertrans.editdetails.reason);
                cmd.Parameters.AddWithValue("pUserId", suppliertrans.userid);
                cmd.Parameters.AddWithValue("pAuthorizedBy", suppliertrans.editdetails.approvedby);
                cmd.Parameters.AddWithValue("pRemarks", suppliertrans.editdetails.remarks);
               
                cmd.CommandType = CommandType.StoredProcedure;

                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_SupplierOrderActions - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public DataSet CRUD_SupplierOrderReturnNotes(SupplierTransReturnRemarks supplierorderreturnremarks)
        {

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {                    
                    string sql = "CRUD_SupplierOrderReturnNotes";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pSupplierOrderId", supplierorderreturnremarks.supplierorderid);
                    cmd.Parameters.AddWithValue("pCreditNoteRemarks", supplierorderreturnremarks.creditnoteremarks);
                    cmd.Parameters.AddWithValue("pReplacementRemarks", supplierorderreturnremarks.replacementremarks);
                    cmd.Parameters.AddWithValue("pType", supplierorderreturnremarks.type.ToUpper());

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudsupplier - Exception: : {ex}");
                throw ex;
            }

            return ds;
          
        }

        public DataSet GetSupplierOrderReturnNotes(string supplierorderid, string type)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetSupplierOrderReturnNotes";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pSupplierOrderId", supplierorderid);
                    cmd.Parameters.AddWithValue("pType", type);
                    
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : GetSupplierOrderWorklist");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of GetSupplierOrderWorklist - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public MySqlCommand CRUD_SupplierOrderActions_Cancel(SupplierTransaction suppliertrans)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_SupplierOrderActions";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pSupplierorderid", suppliertrans.orderid);
                cmd.Parameters.AddWithValue("pActionType", suppliertrans.canceldetails.actiontype);
                cmd.Parameters.AddWithValue("pReason", suppliertrans.canceldetails.reason);
                cmd.Parameters.AddWithValue("pUserId", suppliertrans.userid);
                cmd.Parameters.AddWithValue("pAuthorizedBy", suppliertrans.canceldetails.approvedby);
                cmd.Parameters.AddWithValue("pRemarks", suppliertrans.canceldetails.remarks);

                cmd.CommandType = CommandType.StoredProcedure;

                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_SupplierOrderActions - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUD_SupplierOrderPayments( SupplierTransaction suppliertrans, SupplierTransPaymentdetail paymentdetail)
            { 
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_SupplierOrderPayments";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pMode", paymentdetail.mode);
                cmd.Parameters.AddWithValue("pOrderpaymentid", paymentdetail.orderpaymentid);
                cmd.Parameters.AddWithValue("pSupplierorderid", suppliertrans.orderid);
                cmd.Parameters.AddWithValue("pPaymentMode", paymentdetail.paymentmode);
                cmd.Parameters.AddWithValue("pPaymentModeRemarks", paymentdetail.paymentremarks);
                cmd.Parameters.AddWithValue("pPaymentamount", paymentdetail.paymentamount);
                cmd.Parameters.AddWithValue("pIsDeleted", paymentdetail.isdeleted);
                cmd.Parameters.AddWithValue("pUserId", suppliertrans.userid);
                cmd.CommandType = CommandType.StoredProcedure;
                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_SupplierOrderPayments - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }


        public DataSet SupplierTransactions(SupplierTransaction suppliertrans)
        {
            DataSet dsResult = new DataSet();
            MySqlTransaction myTrans = null;
            MySqlConnection conn = null;

            try
            {
                using (conn = new MySqlConnection(connectionString))
                {

                    MySqlCommand cmd = CRUD_SupplierOrder(suppliertrans);

                    cmd.Connection = conn;
                    conn.Open();
                    myTrans = conn.BeginTransaction();
                    cmd.Transaction = myTrans;

                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dsResult);
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_SupplierOrder");


                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        suppliertrans.orderid = dsResult.Tables[0].Rows[0]["orderid"].ToString();
                        suppliertrans.orderdtm= dsResult.Tables[0].Rows[0]["orderdtm"].ToString();
                        suppliertrans.ordernumber= dsResult.Tables[0].Rows[0]["ordernumber"].ToString();
                    }

                    foreach (SupplierTransProductdetail productdetail in suppliertrans.productdetails)
                    {
                        dsResult = new DataSet();
                        cmd = CRUD_SupplierOrderItems(suppliertrans, productdetail);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);
                        if (dsResult.Tables[0].Rows.Count > 0)
                            productdetail.orderitemid = dsResult.Tables[0].Rows[0]["orderitemid"].ToString();
                    }
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_SupplierOrderItems (All items)");

                    foreach (ConsumptionOrderDetails orderdetail in suppliertrans.consumpionorderdetails)
                    {
                        dsResult = new DataSet();
                        cmd = CRUD_ConsumptionOrderDetails(orderdetail);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);                        
                    }
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_ConsumptionOrderDetails (All items)");

                    if (suppliertrans.editdetails.reason != "")
                    {
                        dsResult = new DataSet();
                        cmd = CRUD_SupplierOrderActions_Edit(suppliertrans);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);
                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_SupplierOrderActions (Edit)");
                    }

                    if (suppliertrans.canceldetails.reason != "")
                    {
                        dsResult = new DataSet();
                        cmd = CRUD_SupplierOrderActions_Cancel(suppliertrans);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);
                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_SupplierOrderActions (Cancel) ");
                    }

                    foreach (SupplierTransPaymentdetail paymentdetail in suppliertrans.paymentdetails)
                    {
                        if (paymentdetail.paymentamount.Trim() != "" )
                        {
                            dsResult = new DataSet();
                            cmd = CRUD_SupplierOrderPayments(suppliertrans, paymentdetail);
                            cmd.Connection = conn;
                            da = new MySqlDataAdapter();
                            da.SelectCommand = cmd;
                            da.Fill(dsResult);                            
                        }
                    }
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_SupplierOrderPayments (All items)");

                    myTrans.Commit();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during OrderManagement - Exception: : {ex}");

                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                    }
                    throw ex;
                }
                catch (MySqlException err)
                {
                    if (myTrans != null && myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + err.GetType() +
                        " was encountered while attempting to roll back the transaction.");
                    }

                    throw ex;
                }
            }
            finally
            {
                if (myTrans != null && myTrans.Connection != null)
                {
                    conn.Close();
                }
            }

            return dsResult;

        }


        public DataSet SearchProductDetails(ProductMasterInput productInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "SearchProductDetails";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", productInput.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", productInput.locationid);
                    cmd.Parameters.AddWithValue("pStoreId", productInput.storeId);
                    cmd.Parameters.AddWithValue("pCounterId", productInput.counterid);
                    cmd.Parameters.AddWithValue("pSearchString", productInput.searchstring);
                    cmd.Parameters.AddWithValue("pCategory", productInput.categoryname);
                    cmd.Parameters.AddWithValue("pSupplierId", productInput.supplierid);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of SearchProductDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetUserManagementDetails(string orgid, string locationid, string type)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "GetUserManagementDetails";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", orgid);
                    cmd.Parameters.AddWithValue("pLocationId", locationid);
                    cmd.Parameters.AddWithValue("pType", type);    
                    
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of GetUserManagementDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }


        public MySqlCommand CRUD_StoreOrder(StoreTransaction storetrans)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_StoreOrder";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pStoreOrderId", storetrans.orderid);
                cmd.Parameters.AddWithValue("pType", storetrans.type);
                cmd.Parameters.AddWithValue("pOrgId", storetrans.orgid);
                cmd.Parameters.AddWithValue("pZoneId", storetrans.zoneid);
                cmd.Parameters.AddWithValue("pLocationId", storetrans.locationid);
                cmd.Parameters.AddWithValue("pFromId", storetrans.generaldetails.fromid);

                cmd.Parameters.AddWithValue("pToId", storetrans.generaldetails.toid);
                cmd.Parameters.AddWithValue("pCategoryId", storetrans.generaldetails.categoryid);
                cmd.Parameters.AddWithValue("pTotalQty", storetrans.generaldetails.totalquantity);                             

                cmd.Parameters.AddWithValue("pRejectReasonId", storetrans.orderstatus);
                cmd.Parameters.AddWithValue("pTransferModeId", storetrans.generaldetails.transfermode);                
                cmd.Parameters.AddWithValue("pIsCancelled", storetrans.iscancelled);
                cmd.Parameters.AddWithValue("pUserId", storetrans.userid);
                
                cmd.CommandType = CommandType.StoredProcedure;
                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_StoreOrder - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUD_StoreOrderItems(StoreTransaction storetrans, StoreTransProductdetail storetransproduct)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_StoreOrderItems";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pOrderItemId", storetransproduct.orderitemid);
                cmd.Parameters.AddWithValue("pType", storetrans.type);
                cmd.Parameters.AddWithValue("pStoreOrderId", storetrans.orderid);
                cmd.Parameters.AddWithValue("pProductId", storetransproduct.productid);
                cmd.Parameters.AddWithValue("pBarcode", storetransproduct.barcode);
                cmd.Parameters.AddWithValue("pTransferredQty", storetransproduct.transferredquantity);

                cmd.Parameters.AddWithValue("pAcceptedqty", storetransproduct.acceptedquantity);
                cmd.Parameters.AddWithValue("pBatchNo", storetransproduct.batchno);
                cmd.Parameters.AddWithValue("pExpiryDate", storetransproduct.expirydate);
                cmd.Parameters.AddWithValue("pSupplierInwardId", storetransproduct.supplierinwardorderid);
                cmd.Parameters.AddWithValue("pSupplierInwardNumber", storetransproduct.supplierinwardnumber);

                cmd.Parameters.AddWithValue("pRemarks", storetransproduct.remarks);
                cmd.Parameters.AddWithValue("pReason", storetransproduct.rejectedreason);
                cmd.Parameters.AddWithValue("pIsDeleted", "0");
                cmd.Parameters.AddWithValue("pUserId", storetrans.userid);
                cmd.Parameters.AddWithValue("pIsCancelled", storetrans.iscancelled);
                cmd.Parameters.AddWithValue("pIsAcceptedRejected", storetrans.isacceptedrejected);

                cmd.CommandType = CommandType.StoredProcedure;

                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_StoreOrderItems - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUD_StoreOrderActions_Edit(StoreTransaction storetrans)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_StoreOrderActions";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pStoreorderid", storetrans.orderid);
                cmd.Parameters.AddWithValue("pActionType", storetrans.editdetails.actiontype);
                cmd.Parameters.AddWithValue("pReason", storetrans.editdetails.reason);
                cmd.Parameters.AddWithValue("pUserId", storetrans.userid);
                cmd.Parameters.AddWithValue("pAuthorizedBy", storetrans.editdetails.approvedby);
                cmd.Parameters.AddWithValue("pRemarks", storetrans.editdetails.remarks);

                cmd.CommandType = CommandType.StoredProcedure;

                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_StoreOrderActions_Edit - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUD_StoreOrderActions_Cancel(StoreTransaction storetrans)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_StoreOrderActions";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pStoreorderid", storetrans.orderid);
                cmd.Parameters.AddWithValue("pActionType", storetrans.canceldetails.actiontype);
                cmd.Parameters.AddWithValue("pReason", storetrans.canceldetails.reason);
                cmd.Parameters.AddWithValue("pUserId", storetrans.userid);
                cmd.Parameters.AddWithValue("pAuthorizedBy", storetrans.canceldetails.approvedby);
                cmd.Parameters.AddWithValue("pRemarks", storetrans.canceldetails.remarks);

                cmd.CommandType = CommandType.StoredProcedure;

                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_StoreOrderActions_Cancel - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }


        public DataSet StoreTransactions(StoreTransaction storetrans)
        {
            DataSet dsResult = new DataSet();
            MySqlTransaction myTrans = null;
            MySqlConnection conn = null;

            try
            {
                using (conn = new MySqlConnection(connectionString))
                {

                    MySqlCommand cmd = CRUD_StoreOrder(storetrans);

                    cmd.Connection = conn;
                    conn.Open();
                    myTrans = conn.BeginTransaction();
                    cmd.Transaction = myTrans;

                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dsResult);
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_StoreOrder");

                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        storetrans.orderid = dsResult.Tables[0].Rows[0]["orderid"].ToString();
                        storetrans.orderdtm = dsResult.Tables[0].Rows[0]["orderdtm"].ToString();
                        storetrans.ordernumber = dsResult.Tables[0].Rows[0]["ordernumber"].ToString();
                    }

                    foreach (StoreTransProductdetail productdetail in storetrans.productdetails)
                    {
                        dsResult = new DataSet();
                        cmd = CRUD_StoreOrderItems(storetrans, productdetail);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);
                        if (dsResult.Tables[0].Rows.Count > 0)
                            productdetail.orderitemid = dsResult.Tables[0].Rows[0]["orderitemid"].ToString();
                    }
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_StoreOrderItems");

                    if (storetrans.editdetails.reason != "")
                    {
                        dsResult = new DataSet();
                        cmd = CRUD_StoreOrderActions_Edit(storetrans);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);
                        Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_StoreOrderActions_Edit");
                    }

                    if (storetrans.canceldetails.reason != "")
                    {
                        dsResult = new DataSet();
                        cmd = CRUD_StoreOrderActions_Cancel(storetrans);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);
                    }
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_StoreOrderActions_Cancel");

                    myTrans.Commit();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during OrderManagement - Exception: : {ex}");

                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                    }
                    throw ex;
                }
                catch (MySqlException err)
                {
                    if (myTrans != null && myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + err.GetType() +
                        " was encountered while attempting to roll back the transaction.");
                    }

                    throw ex;
                }
            }
            finally
            {
                if (myTrans != null && myTrans.Connection != null)
                {
                    conn.Close();
                }
            }

            return dsResult;

        }

        public DataSet GetAllCounters(string orgid, string locationid, string storeid)
        {
            DataSet ds = new DataSet();
            

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {

                    string sql = "select counterid as id, Name as `description` from entitycounter where orgid = @orgid "
                             + "and locationid = @locationid and storeid = @storeid and active = 1;";
                    
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@orgid", orgid);
                    cmd.Parameters.AddWithValue("@locationid", locationid);
                    cmd.Parameters.AddWithValue("@storeid", storeid);

                    conn.Open();
                    
                    cmd.CommandType = CommandType.Text;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }


            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_StoreOrderActions_Cancel - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetAllStores(string orgid, string locationid, string counterid)
        {
            DataSet ds = new DataSet();


            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {

                    string sql = "select es.storeid as id, es.name as `description` from entitystore es "
                             + "inner join entitycounter ec on ec.storeid = es.storeid and ec.counterid = @counterid"
                             + " and ec.orgid = @orgid and ec.locationid = @locationid and es.active = 1;";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@orgid", orgid);
                    cmd.Parameters.AddWithValue("@locationid", locationid);
                    cmd.Parameters.AddWithValue("@counterid", counterid);

                    conn.Open();

                    cmd.CommandType = CommandType.Text;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }


            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_StoreOrderActions_Cancel - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetAllTransferStores(string orgid, string locationid, string storeid)
        {
            DataSet ds = new DataSet();


            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {

                    string sql = "select es1.storeid as id, es1.name as `description` from entitystore es "
                             + "inner join entitystore es1 on es1.type = es.type  and es.storeid = @storeid"
                             + " and es.orgid = @orgid and es.locationid = @locationid and es1.active = 1 and es.storeid <> es1.storeid;";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@orgid", orgid);
                    cmd.Parameters.AddWithValue("@locationid", locationid);
                    cmd.Parameters.AddWithValue("@storeid", storeid);

                    conn.Open();

                    cmd.CommandType = CommandType.Text;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }


            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_StoreOrderActions_Cancel - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet StockDeductions(StockDeduction stockdeduction)
        {
            DataSet dsResult = new DataSet();
            MySqlTransaction myTrans = null;
            MySqlConnection conn = null;

            try
            {
                using (conn = new MySqlConnection(connectionString))
                {

                    MySqlCommand cmd = CRUD_StockDeduction(stockdeduction);

                    cmd.Connection = conn;
                    conn.Open();
                    myTrans = conn.BeginTransaction();
                    cmd.Transaction = myTrans;

                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dsResult);
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_StockDeduction");

                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        stockdeduction.stockdeductionid = dsResult.Tables[0].Rows[0]["stockdeductionid"].ToString();     
                        stockdeduction.stockdeductionnumber = dsResult.Tables[0].Rows[0]["stockdeductionnumber"].ToString();
                    }

                    foreach (StockDeductionItem productdetail in stockdeduction.productdetails)
                    {
                        dsResult = new DataSet();
                        cmd = CRUD_StockDeductionItems(stockdeduction, productdetail);
                        cmd.Connection = conn;
                        da = new MySqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsResult);
                        if (dsResult.Tables[0].Rows.Count > 0)
                            productdetail.stockdeductionitemsid = dsResult.Tables[0].Rows[0]["stockdeductionitemsid"].ToString();
                    }
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_StockDeductionItems (All items)");

                    myTrans.Commit();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during OrderManagement - Exception: : {ex}");

                try
                {
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                    }
                    throw ex;
                }
                catch (MySqlException err)
                {
                    if (myTrans != null && myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + err.GetType() +
                        " was encountered while attempting to roll back the transaction.");
                    }

                    throw ex;
                }
            }
            finally
            {
                if (myTrans != null && myTrans.Connection != null)
                {
                    conn.Close();
                }
            }

            return dsResult;

        }

        public MySqlCommand CRUD_StockDeduction(StockDeduction stockdeduction)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_StockDeduction";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pOrgId", stockdeduction.orgid);
                cmd.Parameters.AddWithValue("pZoneId", stockdeduction.zoneid);
                cmd.Parameters.AddWithValue("pLocationId", stockdeduction.locationid);
                cmd.Parameters.AddWithValue("pStoreId", stockdeduction.storeid);

                cmd.Parameters.AddWithValue("pCounterId", stockdeduction.counterid);
                cmd.Parameters.AddWithValue("pCategoryId", stockdeduction.categoryid);
                cmd.Parameters.AddWithValue("pTransType", stockdeduction.transtype);
                cmd.Parameters.AddWithValue("pUserId", stockdeduction.userid);                

                cmd.CommandType = CommandType.StoredProcedure;
                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_StockDeduction - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public MySqlCommand CRUD_StockDeductionItems(StockDeduction stockdeduction, StockDeductionItem stockdeductionitem)
        {
            MySqlCommand cmd;
            try
            {
                string sql = "CRUD_StockDeductionItems";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("pStockDeductionId", stockdeduction.stockdeductionid);
                cmd.Parameters.AddWithValue("pProductId", stockdeductionitem.productid);
                cmd.Parameters.AddWithValue("pBarcode", stockdeductionitem.barcode);
                cmd.Parameters.AddWithValue("pBatchNo", stockdeductionitem.batchno);

                cmd.Parameters.AddWithValue("pExpiryDate", stockdeductionitem.expirydate);
                cmd.Parameters.AddWithValue("pTransType", stockdeduction.transtype);
                cmd.Parameters.AddWithValue("pQty", stockdeductionitem.qty);

                cmd.CommandType = CommandType.StoredProcedure;

                
            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of CRUD_StockDeductionItems - Exception: : {ex}");
                throw ex;
            }

            return cmd;
        }

        public DataSet GetTransactionHistory(string uin)
        {

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetTransactionHistory";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pUIN", uin);
                    conn.Open();

                    MySqlDataAdapter da = new MySqlDataAdapter();
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : GetTransactionHistory");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting GetTransactionHistory - Exception: : {ex}");
                throw ex;
            }

            return ds;

        }

        public DataSet InsCustomerVisit(InsCustomerVisit objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {

                    string sql = "InsCustomerVisit";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pZoneId", objInput.zoneid);
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);
                    cmd.Parameters.AddWithValue("pCounterId", objInput.counterid);
                    cmd.Parameters.AddWithValue("pPatientRejectReason", objInput.patientrejectreason);
                    cmd.Parameters.AddWithValue("pUIN", objInput.uin);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : InsCustomerVisit");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during InsCustomerVisit - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetCustomerOrderStatusDetails(string orderid,string roleid)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {

                    string sql = "GetCustomerOrderStatusDetails";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrderId", orderid);
                    cmd.Parameters.AddWithValue("pRoleID", roleid);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during InsCustomerVisit - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet CRUD_CustomerOrderStatusTracking(CustomerOrderStatusTracking status)
        {

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "CRUD_CustomerOrderStatusTracking";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrderId", status.orderid);
                    cmd.Parameters.AddWithValue("pOrderStatusId", status.orderstatusid);
                    cmd.Parameters.AddWithValue("pUserId", status.userid);
                    cmd.Parameters.AddWithValue("pRejectedReasonUserId", status.rejectedreasonuserid);
                    cmd.Parameters.AddWithValue("pRemarks", status.remarks);
                    cmd.Parameters.AddWithValue("pLabMachineId", status.labmachineid);
                    cmd.Parameters.AddWithValue("pSupplierId", status.supplierid);
                    cmd.Parameters.AddWithValue("pRejectedReasonId", status.rejectedreasonid);
                    cmd.Parameters.AddWithValue("pLeftLensSupplierId", status.leftlens.supplierid);
                    cmd.Parameters.AddWithValue("pLeftLensStatus", status.leftlens.lensstatus);
                    cmd.Parameters.AddWithValue("pLeftLensOrderStatusId", status.leftlens.orderstatusid);
                    cmd.Parameters.AddWithValue("pLeftLensRejectReasonId", status.leftlens.rejectedreasonid);
                    cmd.Parameters.AddWithValue("pRightLensSupplierId", status.rightlens.supplierid);
                    cmd.Parameters.AddWithValue("pRightLensStatus", status.rightlens.lensstatus);
                    cmd.Parameters.AddWithValue("pRightLensOrderStatusId", status.rightlens.orderstatusid);
                    cmd.Parameters.AddWithValue("pRightLensRejectReasonId", status.rightlens.rejectedreasonid);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();

                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : CRUD_CustomerOrderStatusTracking");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during CRUD_CustomerOrderStatusTracking - Exception: : {ex}");
                throw ex;
            }
            return ds;
        }

        public DataSet GetLensProductDetails(LensMatrixInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "GetLensProductDetails";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);                    
                    cmd.Parameters.AddWithValue("psph", objInput.sph);
                    cmd.Parameters.AddWithValue("pcyl", objInput.cyl);
                    cmd.Parameters.AddWithValue("padd", objInput.add);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();

                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution GetLensProductDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet ValidateLensBarcode(LensMatrixInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "ValidateLensBarcode";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);                    
                    cmd.Parameters.AddWithValue("psph", objInput.sph);
                    cmd.Parameters.AddWithValue("pcyl", objInput.cyl);
                    cmd.Parameters.AddWithValue("padd", objInput.add);
                    cmd.Parameters.AddWithValue("pbarcode", objInput.barcode);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();               

                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of ValidateLensBarcode - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetConsumptionBasedPODetails(ConsumptionBasedPOInput data)
        {

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "GetConsumptionBasedPODetails";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("pOrgId", data.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", data.locationid);
                    cmd.Parameters.AddWithValue("pStoreId", data.storeid);
                    cmd.Parameters.AddWithValue("pCategory", data.categoryid);
                    cmd.Parameters.AddWithValue("pSupplierId", data.supplierid);
                    cmd.Parameters.AddWithValue("pFromdate", data.fromdate);
                    cmd.Parameters.AddWithValue("pTodate", data.todate);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                    Log4Net.LogEvent(LogLevel.Information, this.ClassName(), this.MethodName(), "Successfully executed : GetConsumptionBasedPODetails");
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of getting GetConsumptionBasedPODetails - Exception: : {ex}");
                throw ex;
            }

            return ds;

        }

        public DataSet crudRoleDetails(crudRoleDetailsInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "CRUD_Role";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pRoleId", objInput.roleid);
                    cmd.Parameters.AddWithValue("pRoleName", objInput.rolename);
                    cmd.Parameters.AddWithValue("pActive", objInput.active);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudRoleDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet crudUserRoleDetails(crudUserRoleDetailsInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "CRUD_UserRole";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);
                    cmd.Parameters.AddWithValue("pUserRoleId", objInput.userroleid);
                    cmd.Parameters.AddWithValue("pUserId", objInput.userid);
                    cmd.Parameters.AddWithValue("pRoleId", objInput.roleid);
                    cmd.Parameters.AddWithValue("pActive", objInput.active);
                    cmd.Parameters.AddWithValue("pDefaultRole", objInput.defaultrole);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudRoleDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetUserRoleMenuAccessRights(crudUserRoleDetailsInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "GetUserRoleMenuAccessRights";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);                    
                    cmd.Parameters.AddWithValue("pUserId", objInput.userid);
                    cmd.Parameters.AddWithValue("pRoleId", objInput.roleid);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudRoleDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet crudUserStoreDetails(crudUserStoreDetailsInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "CRUD_UserStore";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);
                    cmd.Parameters.AddWithValue("pUserStoreId", objInput.userstoreid);
                    cmd.Parameters.AddWithValue("pUserId", objInput.userid);
                    cmd.Parameters.AddWithValue("pStoreId", objInput.storeid);
                    cmd.Parameters.AddWithValue("pDefaultSelected", objInput.defaultselected);
                    cmd.Parameters.AddWithValue("pActive", objInput.active);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudRoleDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet crudUserCounterDetails(crudUserCounterDetailsInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "CRUD_UserCounter";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);
                    cmd.Parameters.AddWithValue("pUserCounterId", objInput.usercounterid);
                    cmd.Parameters.AddWithValue("pUserId", objInput.userid);
                    cmd.Parameters.AddWithValue("pStoreId", objInput.storeid);
                    cmd.Parameters.AddWithValue("pCounterId", objInput.counterid);
                    cmd.Parameters.AddWithValue("pDefaultSelected", objInput.defaultselected);
                    cmd.Parameters.AddWithValue("pActive", objInput.active);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudRoleDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet CRUD_CounterDetails(crudCounterInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "CRUD_Counter";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);
                    cmd.Parameters.AddWithValue("pStoreId", objInput.storeid);
                    cmd.Parameters.AddWithValue("pCounterId", objInput.counterid);
                    
                    cmd.Parameters.AddWithValue("pCounterName", objInput.countername);
                    cmd.Parameters.AddWithValue("pActive", objInput.active);
                    cmd.Parameters.AddWithValue("pShortName", objInput.shortname);                    
                    cmd.Parameters.AddWithValue("pIsDeliveryCounter", objInput.isdeliverycounter);
                    cmd.Parameters.AddWithValue("pCustom", objInput.custom);
                    
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudRoleDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetCounterDetails(crudCounterInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "GetCounterDetails";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during execution of GetProductDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet CRUD_UserRoleMenuAccessRights(crudUserRoleDetailsInput objInput)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "CRUD_UserRoleMenuAccessRights";
                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pOrgId", objInput.orgid);
                    cmd.Parameters.AddWithValue("pLocationId", objInput.locationid);
                    cmd.Parameters.AddWithValue("pRoleId", objInput.roleid);
                    cmd.Parameters.AddWithValue("pUserId", objInput.userid);
                    cmd.Parameters.Add(new MySqlParameter("pScopeJson", MySqlDbType.JSON)); // will be sent from front end
                    cmd.Parameters.Add(new MySqlParameter("pActionJson", MySqlDbType.JSON));
                    if (string.IsNullOrEmpty(objInput.scopejson))
                        cmd.Parameters["pScopeJson"].Value = DBNull.Value;
                    else
                        cmd.Parameters["pScopeJson"].Value = objInput.scopejson;

                    if (string.IsNullOrEmpty(objInput.actionjson))
                        cmd.Parameters["pActionJson"].Value = DBNull.Value;
                    else
                        cmd.Parameters["pActionJson"].Value = objInput.actionjson;

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudRoleDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetMenuItems(string roleid,string userid)
        {
            DataSet ds = new DataSet();
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "GetMenuItems";

                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pRoleId", roleid);
                    cmd.Parameters.AddWithValue("pUserId", userid);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudRoleDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

        public DataSet GetRoleMenuJson(string roleid, string userid)
        {
            DataSet ds = new DataSet();
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string tmpqry = "GetRoleMenuJson";

                    MySqlCommand cmd = new MySqlCommand(tmpqry, conn);
                    cmd.Parameters.AddWithValue("pRoleId", roleid);
                    cmd.Parameters.AddWithValue("pUserId", userid);

                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Log4Net.LogEvent(LogLevel.Error, this.ClassName(), this.MethodName(), $"Failed during crudRoleDetails - Exception: : {ex}");
                throw ex;
            }

            return ds;
        }

    }
}

