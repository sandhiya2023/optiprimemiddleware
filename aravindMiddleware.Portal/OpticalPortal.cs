using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using aravindMiddleware.Data;
using aravindMiddleware.Data.DapperClasses;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace aravindMiddleware.Portal
{
    public class OpticalPortal
    {
        OpticalDataContext dataManagementContext = new OpticalDataContext();

        public JObject LookupData(string orgid, string locationid, string lookuptype, string orgcode)
        {
            try
            {
                //DateTime utctime = ConvertToNativeTimeZone("India Standard Time", "2022-02-22 14:30:00");
                if (lookuptype == "LOGIN")
                {
                    DataSet orgds = dataManagementContext.ValidateOrgCode(orgcode);
                    if (orgds.Tables.Count > 0)
                    {
                        if (orgds.Tables[0].Rows[0]["error"].ToString() == "0")
                            orgid = orgds.Tables[0].Rows[0]["orgid"].ToString();
                        else
                        {
                            JObject returnfailure = ConvertToJsonResponseContent("1", "Failure", orgds.Tables[0].Rows[0]["message"].ToString());
                            return returnfailure;
                        }

                    }
                }

                DataSet result = dataManagementContext.LookupData(orgid, locationid, lookuptype);

                if (result.Tables.Count > 0)
                {
                    if (lookuptype == "DASHBOARD")
                    {
                        result.Tables[0].TableName = "orderStatus";
                        result.Tables[1].TableName = "Counter";
                        result.Tables[2].TableName = "datetype";
                    }
                    if (lookuptype == "LOGIN")
                    {
                        result.Tables[0].TableName = "org";
                        result.Tables[1].TableName = "location";
                    }
                    if (lookuptype == "ORDERENTRY")
                    {
                        result.Tables[0].TableName = "section";
                        result.Tables[1].TableName = "financialYear";
                        result.Tables[2].TableName = "state";
                        result.Tables[3].TableName = "prism";
                        result.Tables[4].TableName = "miscItemCategory";
                        result.Tables[5].TableName = "paymentMode";
                        result.Tables[6].TableName = "deliveryMode";
                        result.Tables[7].TableName = "cancelState";
                        result.Tables[8].TableName = "unit";
                        result.Tables[9].TableName = "users";
                        result.Tables[10].TableName = "category";
                        result.Tables[11].TableName = "country";
                        result.Tables[12].TableName = "discountCategory";
                        result.Tables[13].TableName = "discountReason";
                        result.Tables[14].TableName = "returnReason";
                        result.Tables[15].TableName = "deliveryTime";
                        result.Tables[16].TableName = "advanceper";
                        result.Tables[17].TableName = "gprange";
                        result.Tables[18].TableName = "fittingheight";
                        result.Tables[19].TableName = "store";
                        result.Tables[20].TableName = "deliverycounter";
                        result.Tables[21].TableName = "PatientVisitTrackingRejectReason";

                    }
                    if (lookuptype == "SUPPLIER")
                    {
                        result.Tables[0].TableName = "state";
                        result.Tables[1].TableName = "lensCompany";
                        result.Tables[2].TableName = "country";
                    }
                    if (lookuptype == "PRODUCTATTR")
                    {
                        result.Tables[0].TableName = "category";
                        result.Tables[1].TableName = "hsn";
                        result.Tables[2].TableName = "rateFixingCategory";

                    }
                    if ((lookuptype == "FRAMEATTR") || (lookuptype == "LENSATTR") || (lookuptype == "MISCATTR"))
                    {
                        result.Tables[0].TableName = "brand";
                        result.Tables[1].TableName = "model";
                        result.Tables[2].TableName = "type";
                        result.Tables[3].TableName = "color";
                        result.Tables[4].TableName = "size";
                    }

                    if (lookuptype == "DRUGSATTR")
                    {
                        result.Tables[0].TableName = "DrugType";
                        result.Tables[1].TableName = "DrugUnit";
                    }


                    if (lookuptype == "LOOKUP")
                    {
                        result.Tables[0].TableName = "lookupMaster";
                    }

                    //if (lookuptype == "USERMANAGEMENT")
                    //{
                    //    result.Tables[0].TableName = "userrole";

                    //}
                    if (lookuptype == "PURCHASERETURN")
                    {
                        result.Tables[0].TableName = "returnreason";
                        result.Tables[1].TableName = "returnmode";

                    }
                    if (lookuptype == "SUPPILERORDERDETAILS")
                    {
                        result.Tables[0].TableName = "category";
                        result.Tables[1].TableName = "storetype";
                        result.Tables[2].TableName = "supplier";
                        result.Tables[3].TableName = "stocktype";
                        result.Tables[4].TableName = "billtype";
                        result.Tables[5].TableName = "cancelreason";
                        result.Tables[6].TableName = "editreason";
                        result.Tables[7].TableName = "users";
                        result.Tables[8].TableName = "returnmode";
                        result.Tables[9].TableName = "paymentmode";
                        result.Tables[10].TableName = "returnreason";
                        result.Tables[11].TableName = "returntype";
                        result.Tables[12].TableName = "consumptionbilltype";
                        result.Tables[13].TableName = "roundoff";

                    }
                    if (lookuptype == "SUPPLIERORDERWORKLIST")
                    {
                        result.Tables[0].TableName = "supplier";
                        result.Tables[1].TableName = "category";
                        result.Tables[2].TableName = "stocktype";
                        result.Tables[3].TableName = "status";
                    }
                    if (lookuptype == "USERMANAGEMENT")
                    {
                        result.Tables[0].TableName = "role";
                        result.Tables[1].TableName = "user";
                        result.Tables[2].TableName = "location";

                    }
                    if (lookuptype == "SUPPLIERREPORT")
                    {
                        result.Tables[0].TableName = "location";
                        result.Tables[1].TableName = "supplier";
                        result.Tables[2].TableName = "category";
                        result.Tables[3].TableName = "storetype";
                        result.Tables[4].TableName = "stocktype";
                        result.Tables[5].TableName = "orderstatus";
                        result.Tables[6].TableName = "reporttype";
                        result.Tables[7].TableName = "section";
                        result.Tables[8].TableName = "stockreporttype";
                        result.Tables[9].TableName = "paymode";
                        result.Tables[10].TableName = "tracking";
                    }
                    if (lookuptype == "STOREORDERWORKLIST")
                    {
                        result.Tables[0].TableName = "category";
                        result.Tables[1].TableName = "store";
                        result.Tables[2].TableName = "counter";
                        result.Tables[3].TableName = "transferoutstore";
                        result.Tables[4].TableName = "status";
                    }
                    if (lookuptype == "STOREORDERDETAILS")
                    {
                        result.Tables[0].TableName = "fromstore";
                        result.Tables[1].TableName = "tocounter";
                        result.Tables[2].TableName = "category";
                        result.Tables[3].TableName = "deliverymode";
                        result.Tables[4].TableName = "stockreturnreason";
                        result.Tables[5].TableName = "approvedby";
                        result.Tables[6].TableName = "transferoutstore";
                        result.Tables[7].TableName = "cancelreason";
                        result.Tables[8].TableName = "storeacceptrejectreason";
                    }
                    if (lookuptype == "LAB")
                    {
                        result.Tables[0].TableName = "customerorderrejectreason";
                        result.Tables[1].TableName = "labmachinename";
                        result.Tables[2].TableName = "ordertrackingstatus";
                        result.Tables[3].TableName = "supplier";
                        result.Tables[4].TableName = "user";
                    }


                }


                JObject resultObject = new JObject();
                JObject lookupObject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    foreach (DataTable dt in result.Tables)
                    {
                        jArray = new JArray();
                        foreach (DataRow dr in dt.Rows)
                        {
                            jgridObject = new JObject();
                            jgridObject.Add("id", (dr["ID"].ToString()));
                            jgridObject.Add("description", (dr["description"].ToString()));

                            if (lookuptype == "LOGIN" && dt.TableName == "tenent")
                                jgridObject.Add("currencysymbol", (dr["currencysymbol"].ToString()));


                            if (lookuptype == "ORDERENTRY" && dt.TableName == "deliveryMode")
                                jgridObject.Add("deliverycharges", (dr["custom"].ToString()));

                            if (lookuptype == "ORDERENTRY" && dt.TableName == "gprange")
                            {
                                jgridObject.Add("sphmin", (dr["sphmin"].ToString()));
                                jgridObject.Add("sphmax", (dr["sphmax"].ToString()));
                                jgridObject.Add("cylmin", (dr["cylmin"].ToString()));
                                jgridObject.Add("cylmax", (dr["cylmax"].ToString()));
                                jgridObject.Add("axismin", (dr["axismin"].ToString()));
                                jgridObject.Add("axismax", (dr["axismax"].ToString()));
                                jgridObject.Add("addmin", (dr["addmin"].ToString()));
                                jgridObject.Add("addmax", (dr["addmax"].ToString()));
                            }

                            // if (lookuptype == "ORDERENTRY" && (dt.TableName == "deliveryTime" || dt.TableName == "discountCategory" || dt.TableName == "users"))
                            if (lookuptype == "ORDERENTRY" && (dt.TableName == "deliveryTime" || dt.TableName == "discountCategory"))
                                jgridObject.Add("custom", (dr["custom"].ToString()));

                            if (lookuptype == "PRODUCTATTR" && dt.TableName == "rateFixingCategory")
                                jgridObject.Add("ratepercentage", (dr["Rateper"].ToString()));

                            if (lookuptype == "SUPPILERORDERDETAILS" && dt.TableName == "roundoff")
                                jgridObject.Add("custom", (dr["custom"].ToString()));

                            if ((lookuptype == "SUPPILERORDERDETAILS" || lookuptype == "SUPPLIERORDERWORKLIST") && dt.TableName == "supplier")
                                jgridObject.Add("address", (dr["address"].ToString()));


                            if (lookuptype == "STOREORDERDETAILS" && dt.TableName == "tocounter")
                                jgridObject.Add("storeid", (dr["storeid"].ToString()));

                            jArray.Add(jgridObject);
                        }
                        lookupObject.Add(dt.TableName, jArray);
                    }

                    resultObject.Add("lookupData", lookupObject);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject GetProductDetailsByCode(ProductMasterInput productInput)
        {
            DataSet ds = dataManagementContext.GetProductDetailsByCode(productInput);

            JObject resultObject = new JObject();
            JArray jArray;
            JObject jgridObject;
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    resultObject.Add("productId", ds.Tables[0].Rows[0]["productid"].ToString());
                    resultObject.Add("barcode", ds.Tables[0].Rows[0]["barcode"].ToString());
                    resultObject.Add("productName", ds.Tables[0].Rows[0]["productname"].ToString());
                    resultObject.Add("hsnCode", ds.Tables[0].Rows[0]["hsncode"].ToString());
                    resultObject.Add("hsncodedesc", ds.Tables[0].Rows[0]["hsncodedesc"].ToString());
                    resultObject.Add("hsncodeshortname", ds.Tables[0].Rows[0]["hsncodeshortname"].ToString());
                    resultObject.Add("hsntaxperc", ds.Tables[0].Rows[0]["hsntaxperc"].ToString());

                    resultObject.Add("stockOnHand", ds.Tables[0].Rows[0]["stockonhand"].ToString());
                    resultObject.Add("sellingprice", ds.Tables[0].Rows[0]["sellingprice"].ToString());
                    resultObject.Add("ratefixingcategoryid", ds.Tables[0].Rows[0]["ratefixingcategoryid"].ToString());
                    resultObject.Add("ratefixingcategorydesc", ds.Tables[0].Rows[0]["ratefixingcategorydesc"].ToString());
                    resultObject.Add("ratefixingcategoryperc", ds.Tables[0].Rows[0]["ratefixingcategoryperc"].ToString());

                    resultObject.Add("itemtype", ds.Tables[0].Rows[0]["itemtype"].ToString());
                    resultObject.Add("itemmodel", ds.Tables[0].Rows[0]["itemmodel"].ToString());

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        jArray = new JArray();
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            jgridObject = new JObject();
                            jgridObject.Add("barcode", (dr["barcode"].ToString()));
                            jgridObject.Add("batchno", (dr["batchno"].ToString()));
                            jgridObject.Add("expirydate", (dr["expirydate"].ToString()));
                            jgridObject.Add("sellingprice", (dr["sellingprice"].ToString()));
                            jgridObject.Add("supplierorderid", dr["supplierorderid"].ToString());
                            jgridObject.Add("supplierordernumber", dr["supplierordernumber"].ToString());
                            jgridObject.Add("purchaseprice", dr["purchaseprice"].ToString());
                            jgridObject.Add("stockonhandinbatch", dr["stockonhandinbatch"].ToString());

                            jgridObject.Add("gst", dr["gst"].ToString());
                            jgridObject.Add("sgsttaxper", dr["sgsttaxper"].ToString());
                            jgridObject.Add("sgsttaxval", dr["sgsttaxval"].ToString());
                            jgridObject.Add("cgsttaxper", dr["cgsttaxper"].ToString());
                            jgridObject.Add("cgsttaxval", dr["cgsttaxval"].ToString());
                            jgridObject.Add("igsttaxper", dr["igsttaxper"].ToString());
                            jgridObject.Add("igsttaxval", dr["igsttaxval"].ToString());


                            jArray.Add(jgridObject);
                        }
                        resultObject.Add("batchlist", jArray);
                    }
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<users> ValidateUserLogin(UserLogin userLogin)
        {
            try
            {
                userLogin.password = CommonConstraints.Encrypt(userLogin.password, true);


                DataSet result = dataManagementContext.ValidateUserLogin(userLogin);
                List<users> objusers = new List<users>();
                users objuser;
                if (result.Tables.Count > 0)
                {
                    if (result.Tables[0].Rows.Count > 0)
                    {
                        objusers = ConvertToList<users>(result.Tables[0]);
                        /*
                        foreach (users u in objusers)
                        {
                            JObject jObject = GetMenuItems(u.userId, u.roleid);
                            u.rolesmatrix = jObject.ToString();
                        }
                        */

                        objusers[0].expirymonths = ConvertToClassObject<expirymonths>(result.Tables[1]);
                        objusers[0].userstores = ConvertToList<userstore>(result.Tables[2]);
                        objusers[0].usercounters = ConvertToList<usercounter>(result.Tables[3]);
                    }
                }

                return objusers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "Search order - Loading response Json"

        private void LoadHeader(CustomerOrder co, DataTable dt)
        {
            co.orgid = dt.Rows[0]["orgid"].ToString();
            co.zoneid = dt.Rows[0]["zoneid"].ToString();
            co.locationid = dt.Rows[0]["locationid"].ToString();
            co.customerid = dt.Rows[0]["customerid"].ToString();
            co.customerorderid = dt.Rows[0]["customerorderid"].ToString();
            co.orderid = dt.Rows[0]["orderno"].ToString();
            co.orderno = dt.Rows[0]["orderno"].ToString();
            co.counterid = dt.Rows[0]["counterid"].ToString();
            co.storeid = dt.Rows[0]["storeid"].ToString();
            co.countername = dt.Rows[0]["countername"].ToString();
            co.customerorderid = dt.Rows[0]["customerorderid"].ToString();
            co.displayorderno = dt.Rows[0]["displayordernumber"].ToString();

            co.iscancelled = dt.Rows[0]["iscancelled"].ToString();
            co.isdelivery = dt.Rows[0]["isdelivered"].ToString();
            co.isdiscount = dt.Rows[0]["isdiscounted"].ToString();
            co.isreadyfordelivery = dt.Rows[0]["isreadyfordelivery"].ToString();
            // co.billnumber = dt.Rows[0]["billnumber"].ToString();
            co.ispartialpayment = "";

            Header ch = new Header();
            ch.counterid = dt.Rows[0]["counterid"].ToString();
            ch.countername = dt.Rows[0]["countername"].ToString();
            ch.ordernumber = dt.Rows[0]["displayordernumber"].ToString();
            ch.financialyearid = dt.Rows[0]["financialyearid"].ToString();
            ch.financialyear = dt.Rows[0]["financialyear"].ToString();
            ch.orderdtm = dt.Rows[0]["orderdtm"].ToString();
            co.header = ch;
        }

        private void LoadPatientInfo(CustomerOrder co, DataTable dt)
        {
            PatientInfo pinfo = new PatientInfo();
            pinfo.outprescription = dt.Rows[0]["outprescription"].ToString();

            pinfo.uin = dt.Rows[0]["uin"].ToString();
            pinfo.unit = dt.Rows[0]["unit"].ToString();
            pinfo.name = dt.Rows[0]["name"].ToString();
            pinfo.age = dt.Rows[0]["age"].ToString();
            pinfo.gender = dt.Rows[0]["sex"].ToString();
            pinfo.address1 = dt.Rows[0]["address1"].ToString();
            pinfo.address2 = dt.Rows[0]["address2"].ToString();
            pinfo.city = dt.Rows[0]["city"].ToString();
            pinfo.pincode = dt.Rows[0]["pincode"].ToString();
            pinfo.state = dt.Rows[0]["state"].ToString();
            pinfo.country = dt.Rows[0]["country"].ToString();
            pinfo.email = dt.Rows[0]["email"].ToString();
            pinfo.phone = dt.Rows[0]["phone"].ToString();
            pinfo.altphone = dt.Rows[0]["altphone"].ToString();
            pinfo.chkshippingaddress = dt.Rows[0]["chkshippingaddress"].ToString();
            //pinfo.shippingaddress = ConvertToClassObject<ShippingAddress>(dt);
            pinfo.shippingaddress = JsonConvert.DeserializeObject<ShippingAddress>((string)dt.Rows[0]["shippingaddress"]);
            pinfo.gstin = dt.Rows[0]["gstin"].ToString();
            co.patientInfo = pinfo;

        }


        public static T ConvertToClassObject<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name].ToString());
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).First();
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name].ToString());
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }

        private void LoadFrameInfo(OrderDetail objDetail, DataTable dt)
        {

            Frame objframe = new Frame();
            if (dt.Rows.Count > 0) objframe = ConvertToClassObject<Frame>(dt);
            objDetail.frame = objframe;
        }

        private void LoadRightLensInfo(OrderDetail objDetail, DataTable dt)
        {
            RightEye objRE = new RightEye();

            if (dt.Rows.Count > 0) objRE = ConvertToClassObject<RightEye>(dt);
            if (dt.Rows.Count > 0) objRE.attributes = ConvertToClassObject<Attributes>(dt);
            objDetail.righteye = objRE;
        }

        private void LoadLeftLensInfo(OrderDetail objDetail, DataTable dt)
        {
            LeftEye objLE = new LeftEye();
            if (dt.Rows.Count > 0) objLE = ConvertToClassObject<LeftEye>(dt);
            if (dt.Rows.Count > 0) objLE.attributes = ConvertToClassObject<Attributes>(dt);
            objDetail.lefteye = objLE;
        }

        private void LoadPaymentTransactions(CustomerOrder objDetail, DataTable dt)
        {
            if (dt.Rows.Count > 0) objDetail.paymentTransaction = ConvertToList<PaymentTransaction>(dt);
        }


        private void LoadPaymentInfo(CustomerOrder objDetail, DataTable dt)
        {
            PaymentDetails pd = new PaymentDetails();
            if (dt.Rows.Count > 0) pd = ConvertToClassObject<PaymentDetails>(dt);
            objDetail.paymentDetails = pd;
        }

        private void LoadDirectSales(CustomerOrder objDetail, DataTable dt)
        {

            if (dt.Rows.Count > 0) objDetail.directSales = ConvertToList<DirectSale>(dt);
            if (dt.Rows.Count > 0) objDetail.isdirectsale = objDetail.directSales.Count > 0 ? "1" : "0";
        }

        private void LoadOrderDelivery(CustomerOrder objDetail, DataTable dt)
        {
            OrderDelivery od = new OrderDelivery();
            if (dt.Rows.Count > 0) od = ConvertToClassObject<OrderDelivery>(dt);
            objDetail.orderDelivery = od;
        }


        private void LoadOrderCancel(CustomerOrder objDetail, DataTable dt)
        {
            OrderCancel od = new OrderCancel();
            if (dt.Rows.Count > 0) od = ConvertToClassObject<OrderCancel>(dt);
            objDetail.orderCancel = od;
        }

        private void LoadDeliveryBox(OrderDetail objDetail, DataTable dt)
        {
            DeliveryItems od = new DeliveryItems();
            if (dt.Rows.Count > 0) od = ConvertToClassObject<DeliveryItems>(dt);
            // objDetail.deliverybox = new DeliveryItems();
            objDetail.deliverybox = od;
        }

        private void LoadDeliveryCloth(OrderDetail objDetail, DataTable dt)
        {
            DeliveryItems od = new DeliveryItems();
            if (dt.Rows.Count > 0) od = ConvertToClassObject<DeliveryItems>(dt);
            objDetail.deliverycloth = od;
        }

        private void LoadOrderDiscount(CustomerOrder objDetail, DataTable dt)
        {
            OrderDiscount od = new OrderDiscount();
            if (dt.Rows.Count > 0) od = ConvertToClassObject<OrderDiscount>(dt);
            objDetail.orderDiscount = od;
        }


        #endregion
        public JObject GetCustomerOrder(string orgid, string locationid, string counterid, string orderno, string finyearId)
        {
            try
            {

                DataSet ds = new DataSet();
                JObject jgridObject = new JObject();
                JObject resultObject = new JObject();
                string myJsonString = String.Empty;

                ds = dataManagementContext.GetCustomerOrder(orgid, locationid, counterid, orderno, finyearId);

                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "header";
                    ds.Tables[1].TableName = "patientinfo";
                    ds.Tables[2].TableName = "frameinfo";
                    ds.Tables[3].TableName = "rightlensinfo";
                    ds.Tables[4].TableName = "leftlensinfo";
                    ds.Tables[5].TableName = "deliverybox";
                    ds.Tables[6].TableName = "deliverycloth";
                    ds.Tables[7].TableName = "paytrans";
                    ds.Tables[8].TableName = "paydetails";
                    ds.Tables[9].TableName = "directsales";
                    ds.Tables[10].TableName = "orderdelivery";
                    ds.Tables[11].TableName = "ordercancel";
                    ds.Tables[12].TableName = "orderdiscount";
                }

                if (ds.Tables.Count > 0 && ds.Tables["header"].Rows.Count > 0)
                {
                    CustomerOrder objCO = new CustomerOrder();
                    //objCO.tenentid = tenentId;
                    //objCO.entityid = entityId;

                    LoadHeader(objCO, ds.Tables["header"]);

                    LoadPatientInfo(objCO, ds.Tables["patientinfo"]);

                    OrderDetail objDetail = new OrderDetail();
                    objDetail.sameglasspres = ds.Tables["patientinfo"].Rows[0]["sameglassprescription"].ToString();
                    objDetail.ordertype = ds.Tables["patientinfo"].Rows[0]["ordertype"].ToString();

                    LoadFrameInfo(objDetail, ds.Tables["frameinfo"]);
                    LoadRightLensInfo(objDetail, ds.Tables["rightlensinfo"]);
                    LoadLeftLensInfo(objDetail, ds.Tables["leftlensinfo"]);
                    LoadDeliveryBox(objDetail, ds.Tables["deliverybox"]);
                    LoadDeliveryCloth(objDetail, ds.Tables["deliverycloth"]);
                    objCO.orderDetail = objDetail;

                    LoadPaymentTransactions(objCO, ds.Tables["paytrans"]);
                    LoadPaymentInfo(objCO, ds.Tables["paydetails"]);
                    LoadDirectSales(objCO, ds.Tables["directsales"]);


                    LoadOrderDelivery(objCO, ds.Tables["orderdelivery"]);
                    LoadOrderCancel(objCO, ds.Tables["ordercancel"]);
                    LoadOrderDiscount(objCO, ds.Tables["orderdiscount"]);

                    myJsonString = JsonConvert.SerializeObject(objCO, Formatting.None);

                    JObject json = new JObject();
                    if (myJsonString != String.Empty)
                        json = JObject.Parse(myJsonString);
                    return json;

                }
                else
                {
                    JObject jResponse = new JObject();
                    JObject jContent = new JObject();
                    JObject jResult = new JObject();

                    jResponse.Add("error", "1");
                    jResponse.Add("sys_msg", "0");
                    jResponse.Add("message", "Customer order - " + orderno + " not found");


                    jResult.Add("Content", jContent);
                    jResult.Add("Response", jResponse);
                    return jResult;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CheckNullOrEmpty(string input)
        {
            return ((String.IsNullOrEmpty(input)) ? "0" : input);
        }

        public JObject CustomerOrderEntry(CustomerOrder customerorder)
        {
            try
            {
                //few conditional checks and assignments
                customerorder.orderid = CheckNullOrEmpty(customerorder.orderid);
                customerorder.displayorderno = CheckNullOrEmpty(customerorder.displayorderno);
                customerorder.customerorderid = CheckNullOrEmpty(customerorder.customerorderid);

                customerorder.patientInfo.state = CheckNullOrEmpty(customerorder.patientInfo.state);
                customerorder.patientInfo.country = CheckNullOrEmpty(customerorder.patientInfo.country);
                customerorder.isdirectsale = CheckNullOrEmpty(customerorder.isdirectsale);
                customerorder.isdiscount = CheckNullOrEmpty(customerorder.isdiscount);
                customerorder.isdelivery = CheckNullOrEmpty(customerorder.isdelivery);
                customerorder.iscancelled = CheckNullOrEmpty(customerorder.iscancelled);

                //customerorder.orderDetail.frame.discountedamt = CheckNullOrEmpty(customerorder.orderDetail.frame.discountedamt);
                //customerorder.orderDetail.lefteye.discountedamt = CheckNullOrEmpty(customerorder.orderDetail.lefteye.discountedamt);
                //customerorder.orderDetail.righteye.discountedamt = CheckNullOrEmpty(customerorder.orderDetail.righteye.discountedamt);

                customerorder.orderDetail.frame.ownframe = (String.IsNullOrEmpty(customerorder.orderDetail.frame.ownframe) || customerorder.orderDetail.frame.ownframe == "false") ? "0" : "1";
                customerorder.orderDetail.righteye.ownlens = (String.IsNullOrEmpty(customerorder.orderDetail.righteye.ownlens) || customerorder.orderDetail.righteye.ownlens == "false") ? "0" : "1";
                customerorder.orderDetail.lefteye.ownlens = (String.IsNullOrEmpty(customerorder.orderDetail.lefteye.ownlens) || customerorder.orderDetail.lefteye.ownlens == "false") ? "0" : "1";

                customerorder.orderDetail.frame.reuse = (String.IsNullOrEmpty(customerorder.orderDetail.frame.reuse) || customerorder.orderDetail.frame.reuse == "false") ? "0" : "1";
                customerorder.orderDetail.righteye.reuse = (String.IsNullOrEmpty(customerorder.orderDetail.righteye.reuse) || customerorder.orderDetail.righteye.reuse == "false") ? "0" : "1";
                customerorder.orderDetail.lefteye.reuse = (String.IsNullOrEmpty(customerorder.orderDetail.lefteye.reuse) || customerorder.orderDetail.lefteye.reuse == "false") ? "0" : "1";

                customerorder.orderDetail.righteye.attributesjson = JsonConvert.SerializeObject(customerorder.orderDetail.righteye.attributes);
                customerorder.orderDetail.lefteye.attributesjson = JsonConvert.SerializeObject(customerorder.orderDetail.lefteye.attributes);
                customerorder.patientInfo.shippingaddressjson = JsonConvert.SerializeObject(customerorder.patientInfo.shippingaddress);

                customerorder.orderDetail.frame.refundamt = CheckNullOrEmpty(customerorder.orderDetail.frame.refundamt);
                customerorder.orderDetail.lefteye.refundamt = CheckNullOrEmpty(customerorder.orderDetail.lefteye.refundamt);
                customerorder.orderDetail.righteye.refundamt = CheckNullOrEmpty(customerorder.orderDetail.righteye.refundamt);
                customerorder.paymentDetails.miscchargesrefundamt = CheckNullOrEmpty(customerorder.paymentDetails.miscchargesrefundamt);
                customerorder.paymentDetails.parcelchargesrefundamt = CheckNullOrEmpty(customerorder.paymentDetails.parcelchargesrefundamt);


                //check for product availability before save
                ProductMasterInput productInput = new ProductMasterInput();
                //productInput.tenantId = customerorder.tenentid;
                //productInput.entityId = customerorder.entityid;
                //productInput.sectionId = customerorder.header.sectionid;

                if (customerorder.isdirectsale == "0" && customerorder.orderDetail.frame.ownframe != "1"
                    && customerorder.iscancelled == "0" && customerorder.orderDetail.frame.barcode != "")
                {
                    productInput.barcode = customerorder.orderDetail.frame.barcode;
                    productInput.categoryname = "FRAME";

                    DataSet ds = dataManagementContext.GetProductDetailsByCode(productInput);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt64(ds.Tables[0].Rows[0]["stockonhand"].ToString()) <= 0)
                        {
                            return GetErrorMessageJson(productInput.barcode);
                        }
                    }
                }

                if (customerorder.isdirectsale == "0" && customerorder.orderDetail.lefteye.ownlens != "1"
                    && customerorder.iscancelled == "0" && customerorder.orderDetail.lefteye.barcode != "")
                {
                    productInput.barcode = customerorder.orderDetail.lefteye.barcode;
                    productInput.categoryname = "LENS";

                    DataSet ds = dataManagementContext.GetProductDetailsByCode(productInput);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt64(ds.Tables[0].Rows[0]["stockonhand"].ToString()) <= 0)
                        {
                            return GetErrorMessageJson(productInput.barcode);
                        }
                    }
                }

                if (customerorder.isdirectsale == "0" && customerorder.orderDetail.righteye.ownlens != "1"
                    && customerorder.iscancelled == "0" && customerorder.orderDetail.righteye.barcode != "")
                {
                    productInput.barcode = customerorder.orderDetail.righteye.barcode;
                    productInput.categoryname = "LENS";

                    DataSet ds = dataManagementContext.GetProductDetailsByCode(productInput);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt64(ds.Tables[0].Rows[0]["stockonhand"].ToString()) <= 0)
                        {
                            return GetErrorMessageJson(productInput.barcode);
                        }
                    }
                }

                if (customerorder.isdirectsale == "1" && customerorder.iscancelled == "0")
                {
                    foreach (DirectSale directsale in customerorder.directSales)
                    {
                        productInput.barcode = directsale.barcode;
                        productInput.categoryname = directsale.categorydesc;

                        DataSet ds = dataManagementContext.GetProductDetailsByCode(productInput);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToInt64(ds.Tables[0].Rows[0]["stockonhand"].ToString()) <= 0)
                            {
                                return GetErrorMessageJson(productInput.barcode);
                            }
                        }

                    }
                }
                bool isNewOrder = false;

                if (customerorder.customerorderid == "0" || customerorder.customerorderid == "")
                    isNewOrder = true;


                DataSet result = dataManagementContext.OrderManagement(customerorder);

                string messsage = string.Empty;
                if (customerorder.isdelivery == "1" && customerorder.iscancelled == "0")
                    messsage = "Customer Order - " + customerorder.displayorderno + " delivered successfully";
                else if (customerorder.iscancelled == "1")
                    messsage = "Customer Order - " + customerorder.displayorderno + " cancelled successfully";
                else if (customerorder.isdiscount == "1" && customerorder.iscancelled == "0")
                    messsage = "Customer Order - " + customerorder.displayorderno + " discount applied successfully";
                else if (customerorder.isdirectsale == "1")
                    messsage = "Customer Order - " + customerorder.displayorderno + " delivered successfully";
                else if (isNewOrder)
                    messsage = "Customer Order - " + customerorder.displayorderno + " placed successfully";
                else
                    messsage = "Customer Order - " + customerorder.displayorderno + " updated successfully";

                JObject jResponse = new JObject();
                JObject jContent = new JObject();
                JObject jResult = new JObject();

                jResponse.Add("error", "0");
                jResponse.Add("sys_msg", "0");
                //jResponse.Add("message", "Customer Order[Order No: " + customerorder.orderid + " ; Display order No: " + customerorder.displayorderno + "] processed successfully");
                jResponse.Add("message", messsage);

                jContent.Add("orgid", customerorder.orgid);
                jContent.Add("locationid", customerorder.locationid);
                jContent.Add("customerid", customerorder.customerid);
                jContent.Add("customerorderid", customerorder.customerorderid);
                jContent.Add("orderid", customerorder.orderid);
                jContent.Add("displayordernumber", customerorder.displayorderno);
                jContent.Add("billnumber", customerorder.billnumber);



                jResult.Add("Content", jContent);
                jResult.Add("Response", jResponse);

                return jResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private JObject GetErrorMessageJson(string code)
        {
            JObject jErrResponse = new JObject();
            JObject jErrcontent = new JObject();
            JObject jErrResult = new JObject();

            jErrResponse = new JObject();
            jErrcontent = new JObject();
            jErrResult = new JObject();

            jErrResponse.Add("error", "0");
            jErrResponse.Add("sys_msg", "0");
            jErrResponse.Add("message", "Barcode : " + code + " - Insufficient quantity. Please recheck the items");

            jErrcontent.Add("productcode", code);
            jErrResult.Add("Content", jErrcontent);
            jErrResult.Add("Response", jErrResponse);

            return jErrResult;

        }

        /*
         public JObject PurchaseTransactions(PurchaseTrans data)
         {
             try
             {

                 DataSet dsData = dataManagementContext.PurchaseTransactions(data);

                 string message = string.Empty;
                 if (data.type == "1") //purchaseentry
                     message = "Purchase Entries added successfully";
                 else
                     message = "Purchase Returns added successfully";

                 JObject jResponse = new JObject();
                 JObject jContent = new JObject();
                 JObject jResult = new JObject();

                 jResponse.Add("error", "0");
                 jResponse.Add("sys_msg", "0");

                 jResponse.Add("message", message);

                 jContent.Add("tenentId", data.orgid);
                 jContent.Add("entityId", data.locationid);
                 jContent.Add("purchaseorderno", data.purchaseorderno);
                 jContent.Add("purchaseorderdtm", data.purchaseorderdtm);


                 jResult.Add("Content", jContent);
                 jResult.Add("Response", jResponse);

                 return jResult;
             }
             catch (Exception ex)
             {
                 throw ex;
             }

         }
        */

        public JObject GetDashboardDetails(DashboardInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.GetDashboardDetails(objInput);


                JObject resultObject = new JObject();
                JObject DashboardDetailsObject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("orderid", (dr["orderid"].ToString()));
                        jgridObject.Add("entityname", (dr["entityname"].ToString()));
                        jgridObject.Add("orderumber", (dr["ordernumber"].ToString()));
                        jgridObject.Add("orderdatetime", (dr["orderdatetime"].ToString()));
                        jgridObject.Add("sectionid", (dr["sectionId"].ToString()));
                        jgridObject.Add("sectionname", (dr["sectionname"].ToString()));

                        jgridObject.Add("customername", (dr["customername"].ToString()));
                        jgridObject.Add("phonenumber", (dr["phonenumber"].ToString()));
                        jgridObject.Add("uin", (dr["uin"].ToString()));

                        jgridObject.Add("totalamount", (dr["orderTotalCost"].ToString()));
                        jgridObject.Add("itemsTotalCost", (dr["itemsTotalCost"].ToString()));
                        jgridObject.Add("miscCharges", (dr["miscCharges"].ToString()));
                        jgridObject.Add("parcelcharges", (dr["parcelcharges"].ToString()));
                        jgridObject.Add("tintcharges", (dr["tintcharges"].ToString()));
                        jgridObject.Add("fittingcharges", (dr["fittingcharges"].ToString()));
                        jgridObject.Add("discountAmt", (dr["discountAmt"].ToString()));
                        jgridObject.Add("advance", (dr["advanceAmt"].ToString()));
                        jgridObject.Add("balance", (dr["balanceAmt"].ToString()));

                        jgridObject.Add("deliverymodename", (dr["deliverymodename"].ToString()));
                        jgridObject.Add("deliverydatetime", (dr["deliverydatetime"].ToString()));
                        jgridObject.Add("cancelleddatetime", (dr["cancelleddatetime"].ToString()));
                        jgridObject.Add("iscancelled", (dr["isCancelled"].ToString()));
                        //praveen changes start 09-02-2022
                        jgridObject.Add("status", (dr["status"].ToString()));
                        jgridObject.Add("isdelivered", (dr["isdelivered"].ToString()));
                        jgridObject.Add("billnumber", (dr["billnumber"].ToString()));
                        jgridObject.Add("isreadyfordelivery", (dr["isreadyfordelivery"].ToString()));
                        jgridObject.Add("orderstatusdesc", (dr["orderstatusdesc"].ToString()));
                        //praveen changes end 09-02-2022
                        jArray.Add(jgridObject);
                    }



                    resultObject.Add("DashboardData", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        public JObject GetProductDetails(ProductMasterInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.GetProductDetails(objInput);


                JObject resultObject = new JObject();
                JObject DashboardDetailsObject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();
                JObject jlenmatrixObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("productId", (dr["productId"].ToString()));
                        jgridObject.Add("productcode", (dr["productcode"].ToString()));
                        jgridObject.Add("productname", (dr["productname"].ToString()));
                        jgridObject.Add("brand", (dr["brand"].ToString()));
                        jgridObject.Add("model", (dr["model"].ToString()));
                        jgridObject.Add("type", (dr["type"].ToString()));
                        jgridObject.Add("color", (dr["color"].ToString()));
                        jgridObject.Add("size", (dr["size"].ToString()));
                        jgridObject.Add("category", (dr["category"].ToString()));
                        jgridObject.Add("suppliername", (dr["suppliername"].ToString()));
                        jgridObject.Add("hsncode", (dr["hsncode"].ToString()));
                        jgridObject.Add("purchaseprice", (dr["purchaseprice"].ToString()));
                        jgridObject.Add("ratefixper", (dr["ratefixper"].ToString()));
                        jgridObject.Add("sellingprice", (dr["sellingprice"].ToString()));
                        //praveen changes start- 04-02-2022
                        jgridObject.Add("status", (dr["status"].ToString()));
                        jgridObject.Add("islocked", (dr["islocked"].ToString()));

                        jgridObject.Add("brandId", (dr["brandId"].ToString()));
                        jgridObject.Add("modelId", (dr["modelId"].ToString()));
                        jgridObject.Add("typeId", (dr["typeId"].ToString()));
                        jgridObject.Add("colorId", (dr["colorId"].ToString()));
                        jgridObject.Add("sizeId", (dr["sizeId"].ToString()));
                        jgridObject.Add("categoryId", (dr["categoryId"].ToString()));
                        jgridObject.Add("hsncodeId", (dr["hsncodeId"].ToString()));
                        jgridObject.Add("ratefixperId", (dr["ratefixperId"].ToString()));
                        jgridObject.Add("supplierId", (dr["supplierId"].ToString()));
                        //praveen changes end - 04-02-2022

                        jlenmatrixObject = new JObject();

                        jlenmatrixObject.Add("cylpositivestarting", (dr["cylpositivestarting"].ToString()));
                        jlenmatrixObject.Add("cylpositiveending", (dr["cylpositiveending"].ToString()));
                        jlenmatrixObject.Add("cylnegativestarting", (dr["cylnegativestarting"].ToString()));
                        jlenmatrixObject.Add("cylnegativeending", (dr["cylnegativeending"].ToString()));
                        jlenmatrixObject.Add("totalpositivestarting", (dr["totalpositivestarting"].ToString()));
                        jlenmatrixObject.Add("totalpositiveending", (dr["totalpositiveending"].ToString()));
                        jlenmatrixObject.Add("totalnegativestarting", (dr["totalnegativestarting"].ToString()));
                        jlenmatrixObject.Add("totalnegativeending", (dr["totalnegativeending"].ToString()));
                        jlenmatrixObject.Add("additionpositivestarting", (dr["additionpositivestarting"].ToString()));
                        jlenmatrixObject.Add("additionpositiveending", (dr["additionpositiveending"].ToString()));

                        jgridObject.Add("lensmatrix", jlenmatrixObject);

                        jArray.Add(jgridObject);
                    }
                    resultObject.Add("ProductDetails", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public JObject GetSupplierDetails(SupplierMasterInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.GetSupplierDetails(objInput);


                JObject resultObject = new JObject();
                JObject DashboardDetailsObject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("supplierId", (dr["supplierId"].ToString()));
                        jgridObject.Add("name", (dr["name"].ToString()));
                        jgridObject.Add("address", (dr["address"].ToString()));
                        jgridObject.Add("city", (dr["city"].ToString()));
                        jgridObject.Add("pincode", (dr["pincode"].ToString()));
                        jgridObject.Add("homephone", (dr["homephone"].ToString()));
                        jgridObject.Add("officephone", (dr["officephone"].ToString()));
                        jgridObject.Add("fax", (dr["fax"].ToString()));
                        jgridObject.Add("email", (dr["email"].ToString()));
                        jgridObject.Add("tngstno", (dr["tngstno"].ToString()));
                        jgridObject.Add("cstno", (dr["cstno"].ToString()));
                        jgridObject.Add("trackingCompanyId", (dr["trackingCompanyId"].ToString()));
                        jgridObject.Add("trackingCompanyName", (dr["trackingCompanyName"].ToString()));
                        jgridObject.Add("tallytype", (dr["tallytype"].ToString()));
                        jgridObject.Add("statename", (dr["statename"].ToString()));
                        jgridObject.Add("active", (dr["active"].ToString()));
                        jgridObject.Add("taxfree", (dr["taxfree"].ToString()));
                        jgridObject.Add("lenssupplier", (dr["lenssupplier"].ToString()));
                        jgridObject.Add("stateId", (dr["stateId"].ToString()));
                        jArray.Add(jgridObject);
                    }

                    resultObject.Add("SupplierDetails", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject crudsupplier(crudSupplierDetailsInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.crudsupplier(objInput);
                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {
                    errcode = "1";
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }
                else
                {
                    errcode = "0";
                    message = "Supplier details added/updated successfully";
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject crudproduct(crudProductDetailsInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.crudproduct(objInput);
                string errcode = string.Empty;
                string message = string.Empty;
                if (result.Tables.Count > 0)
                {
                    errcode = "1";
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }
                else
                {
                    errcode = "0";
                    message = "Product details added/updated successfully";
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public JObject ConvertToJsonResponseContent(string code, String status, String message)
        {

            JObject jResponse = new JObject();
            JObject jContent = new JObject();
            JObject jResult = new JObject();

            jResponse.Add("error", code);
            jResponse.Add("sys_msg", status);
            jResponse.Add("message", message);


            jResult.Add("Content", jContent);
            jResult.Add("Response", jResponse);

            return jResult;
        }
        //praveen changes - end - 20-01-2022

        public JObject ConvertToJsonResponse(string code, String status, String message)
        {
            JObject jObject = new JObject();
            jObject.Add("code", code);
            jObject.Add("status", status);
            jObject.Add("message", message);
            return jObject;
        }

        //praveen changes - start - 09-02-2022
        public JObject GetLookupMasterDetails(LookupMasterDetails objInput)
        {
            try
            {
                DataSet result = dataManagementContext.GetLookupMasterDetails(objInput);


                JObject resultObject = new JObject();
                JObject LookupMasterDetails = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("lookupId", (dr["lookupId"].ToString()));
                        jgridObject.Add("displayname", (dr["displayname"].ToString()));
                        jgridObject.Add("shortname", (dr["shortname"].ToString()));
                        jgridObject.Add("custom", (dr["custom"].ToString()));
                        jgridObject.Add("active", (dr["active"].ToString()));
                        jgridObject.Add("displayorder", (dr["displayorder"].ToString()));
                        jArray.Add(jgridObject);
                    }

                    resultObject.Add("LookupMasterDetails", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject crudLookup(crudLookupDetailsInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.crudLookup(objInput);
                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {
                    errcode = "1";
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }
                else
                {
                    errcode = "0";
                    message = "Lookup details successfully processed";
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //praveen changes - end - 09-02-2022
        public JObject GetUserDetails(string orgid, string locationid, string roleId)
        {
            try
            {
                DataSet result = dataManagementContext.GetUserDetails(orgid, locationid, roleId);


                JObject resultObject = new JObject();
                JObject UserDetails = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("userid", (dr["userid"].ToString()));
                        jgridObject.Add("username", (dr["username"].ToString()));
                        string decryptedpassword = CommonConstraints.Decrypt(dr["password"].ToString(), true);
                        jgridObject.Add("password", (decryptedpassword));
                        jgridObject.Add("empcode", (dr["empcode"].ToString()));
                        jgridObject.Add("roleid", (dr["roleid"].ToString()));
                        jgridObject.Add("rolename", (dr["rolename"].ToString()));
                        jgridObject.Add("active", (dr["active"].ToString()));

                        jArray.Add(jgridObject);
                    }

                    resultObject.Add("UserDetails", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public JObject crudUserDetails(crudUserDetailsInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.crudUserDetails(objInput);
                string message = string.Empty;
                string errcode = string.Empty;
                /*
                if (result.Tables.Count > 0)
                {
                    errcode = "0";
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }
                else
                {
                    errcode = "1";
                    message = "User details successfully processed";
                }
                */

                if (result.Tables.Count > 0)
                {

                    errcode = result.Tables[0].Rows[0]["error"].ToString();
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject GetRecentPurchaseDetails(string tenantid, string entityid, string category, string transtype)
        {
            try
            {
                DataSet result = dataManagementContext.GetRecentPurchaseDetails(tenantid, entityid, entityid, category, transtype);


                JObject resultObject = new JObject();
                JObject DashboardDetailsObject = new JObject();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    if (result.Tables[0].Rows.Count > 0)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("purchaseentryid", (result.Tables[0].Rows[0]["purchaseentryid"].ToString()));
                        jgridObject.Add("purchasenumber", (result.Tables[0].Rows[0]["purchasenumber"].ToString()));
                        jgridObject.Add("purchasedtm", (result.Tables[0].Rows[0]["purchasedtm"].ToString()));

                    }
                }

                return jgridObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject getorderpaymentdetails(string orderid)
        {
            try
            {
                DataSet result = dataManagementContext.getorderpaymentdetails(orderid);


                JObject resultObject = new JObject();
                JObject DashboardDetailsObject = new JObject();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    if (result.Tables[0].Rows.Count > 0)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("purchaseentryid", (result.Tables[0].Rows[0]["purchaseentryid"].ToString()));
                        jgridObject.Add("purchasenumber", (result.Tables[0].Rows[0]["purchasenumber"].ToString()));
                        jgridObject.Add("purchasedtm", (result.Tables[0].Rows[0]["purchasedtm"].ToString()));

                    }
                }

                return jgridObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject ValidateRefractionDetails(RefractionDetailsInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.ValidateRefractionDetails(objInput.tenentId, objInput.sph, objInput.cyl, objInput.axis, objInput.add);
                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {
                    errcode = "1";
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }
                else
                {
                    errcode = "0";
                    message = "";
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject GetSupplierOrderWorklist(SupplierOrderWorklistInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.GetSupplierOrderWorklist(objInput);


                JObject resultObject = new JObject();
                JObject DashboardDetailsObject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("orgid", (dr["orgid"].ToString()));
                        jgridObject.Add("locationid", (dr["locationid"].ToString()));
                        jgridObject.Add("storeid", (dr["storeid"].ToString()));
                        jgridObject.Add("storename", (dr["storename"].ToString()));
                        jgridObject.Add("supplierorderid", (dr["supplierorderid"].ToString()));
                        jgridObject.Add("supplierordernumber", (dr["supplierordernumber"].ToString()));
                        jgridObject.Add("supplierorderdtm", (dr["supplierorderdtm"].ToString()));
                        jgridObject.Add("supplierid", (dr["supplierid"].ToString()));
                        jgridObject.Add("suppliername", (dr["suppliername"].ToString()));
                        jgridObject.Add("stocktype", (dr["stocktype"].ToString()));
                        jgridObject.Add("categoryid", (dr["categoryid"].ToString()));
                        jgridObject.Add("categorydesc", (dr["categorydesc"].ToString()));
                        jgridObject.Add("type", (dr["type"].ToString()));
                        jgridObject.Add("orderstatus", (dr["orderstatus"].ToString()));
                        jgridObject.Add("totalqty", (dr["totalqty"].ToString()));
                        jgridObject.Add("totalamount", (dr["totalamount"].ToString()));

                        jgridObject.Add("showviewpo", (dr["showviewpo"].ToString()));
                        jgridObject.Add("showeditpo", (dr["showeditpo"].ToString()));
                        jgridObject.Add("showcancelpo", (dr["showcancelpo"].ToString()));
                        jgridObject.Add("showabortpo", (dr["showabortpo"].ToString()));
                        jgridObject.Add("showaddinward", (dr["showaddinward"].ToString()));
                        jgridObject.Add("showviewinward", (dr["showviewinward"].ToString()));

                        jgridObject.Add("showeditinward", (dr["showeditinward"].ToString()));
                        jgridObject.Add("showcancelinward", (dr["showcancelinward"].ToString()));
                        jgridObject.Add("showviewreturn", (dr["showviewreturn"].ToString()));
                        jgridObject.Add("showcancelreturn", (dr["showcancelreturn"].ToString()));

                        jArray.Add(jgridObject);
                    }

                    resultObject.Add("SupplierOrderWorklist", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject GetStoreOrderWorklist(StoreOrderWorklistInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.GetStoreOrderWorklist(objInput);

                JObject resultObject = new JObject();
                JObject DashboardDetailsObject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("orgid", (dr["orgid"].ToString()));
                        jgridObject.Add("locationid", (dr["locationid"].ToString()));
                        jgridObject.Add("fromid", (dr["fromid"].ToString()));
                        jgridObject.Add("fromname", (dr["fromname"].ToString()));
                        jgridObject.Add("toid", (dr["toid"].ToString()));
                        jgridObject.Add("toname", (dr["toname"].ToString()));
                        jgridObject.Add("storeorderid", (dr["storeorderid"].ToString()));
                        jgridObject.Add("storeordernumber", (dr["storeordernumber"].ToString()));
                        jgridObject.Add("storeorderdtm", (dr["storeorderdtm"].ToString()));
                        jgridObject.Add("categoryid", (dr["categoryid"].ToString()));
                        jgridObject.Add("categorydesc", (dr["categorydesc"].ToString()));
                        jgridObject.Add("type", (dr["type"].ToString()));
                        jgridObject.Add("orderstatus", (dr["orderstatus"].ToString()));
                        jgridObject.Add("totalqty", (dr["totalqty"].ToString()));

                        jgridObject.Add("showedittransfer", (dr["showedittransfer"].ToString()));
                        jgridObject.Add("showcanceltransfer", (dr["showcanceltransfer"].ToString()));
                        jgridObject.Add("showacceptrejecttransfer", (dr["showacceptrejecttransfer"].ToString()));

                        jgridObject.Add("showeditreturn", (dr["showeditreturn"].ToString()));
                        jgridObject.Add("showcancelreturn", (dr["showcancelreturn"].ToString()));
                        jgridObject.Add("showacceptrejectreturn", (dr["showacceptrejectreturn"].ToString()));
                        /*
                        jgridObject.Add("showedittransferout", (dr["showedittransferout"].ToString()));
                        jgridObject.Add("showcanceltransferout", (dr["showcanceltransferout"].ToString()));
                        jgridObject.Add("showacceptrejecttransferout", (dr["showacceptrejecttransferout"].ToString()));
                        */

                        jArray.Add(jgridObject);
                    }

                    resultObject.Add("StoreOrderWorklist", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public JObject SupplierTransactions(SupplierTransaction data)
        {
            try
            {
                JObject jResponse = new JObject();
                JObject jContent = new JObject();
                JObject jResult = new JObject();

                DataSet dsPreValidation = dataManagementContext.PreValidateSupplierOrder(data.orderid, data.generaldetails.supplierid, data.generaldetails.billrefno);
                if (dsPreValidation.Tables.Count > 0)
                {
                    if (dsPreValidation.Tables[0].Rows.Count > 0)
                    {
                        string errcode = dsPreValidation.Tables[0].Rows[0]["errorcode"].ToString();
                        string errormessage = dsPreValidation.Tables[0].Rows[0]["errormessage"].ToString();
                        jResponse.Add("error", errcode);
                        jResponse.Add("sys_msg", "0");

                        jResponse.Add("message", errormessage);

                        jContent.Add("orgid", 0);
                        jContent.Add("locationid", 0);
                        jContent.Add("orderid", 0);
                        jContent.Add("orderdtm", "");
                        jContent.Add("ordernumber", "");

                        jResult.Add("Content", jContent);
                        jResult.Add("Response", jResponse);
                    }
                }
                else
                {
                    DataSet dsData = dataManagementContext.SupplierTransactions(data);

                    string message = string.Empty;
                    if (data.type == "1") //purchase order
                        message = "Supplier Order processed successfully";
                    else if (data.type == "2") //purchase entry
                        message = "Supplier Inward processed successfully";
                    else if (data.type == "3") //purchase return
                        message = "Supplier Return processed successfully";
                    else //transferin
                        message = "Transfer in processed successfully";


                    jResponse.Add("error", "0");
                    jResponse.Add("sys_msg", "0");

                    jResponse.Add("message", message);

                    jContent.Add("orgid", data.orgid);
                    jContent.Add("locationid", data.locationid);
                    jContent.Add("orderid", data.orderid);
                    jContent.Add("orderdtm", data.orderdtm);
                    jContent.Add("ordernumber", data.ordernumber);

                    jResult.Add("Content", jContent);
                    jResult.Add("Response", jResponse);

                }
                return jResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject StoreTransactions(StoreTransaction data)
        {
            try
            {

                DataSet dsData = dataManagementContext.StoreTransactions(data);

                string message = string.Empty;
                if (data.type == "1") //stock transfer
                    message = "Stock Transfer processed successfully";
                else if (data.type == "2") //stock return
                    message = "Stock Return processed successfully";
                else
                    message = "Transfer Out processed successfully";

                JObject jResponse = new JObject();
                JObject jContent = new JObject();
                JObject jResult = new JObject();

                jResponse.Add("error", "0");
                jResponse.Add("sys_msg", "0");

                jResponse.Add("message", message);

                jContent.Add("orgid", data.orgid);
                jContent.Add("locationid", data.locationid);
                jContent.Add("orderid", data.orderid);
                jContent.Add("orderdtm", data.orderdtm);
                jContent.Add("ordernumber", data.ordernumber);

                jResult.Add("Content", jContent);
                jResult.Add("Response", jResponse);

                return jResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        //praveen changes - 07-03-2022
        public DataSet MISreport(MISReportInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.MISreport(objInput);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //praveen changes - 07-03-2022

        public JObject GetSupplierTransactionDetails(string transtype, string orderid)
        {
            string myJsonString = String.Empty;

            try
            {
                DataSet ds = dataManagementContext.GetSupplierTransactionDetails(transtype, orderid);

                SupplierTransaction objST = new SupplierTransaction();

                JObject resultObject = new JObject();
                JObject DashboardDetailsObject = new JObject();
                JObject jgridObject = new JObject();

                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "header";
                    ds.Tables[1].TableName = "generaldetails";
                    ds.Tables[2].TableName = "summary";
                    ds.Tables[3].TableName = "editdetails";
                    ds.Tables[4].TableName = "canceldetails";
                    ds.Tables[5].TableName = "paymentdetails";
                    ds.Tables[6].TableName = "productdetails";
                }

                if (ds.Tables.Count > 0 && ds.Tables["header"].Rows.Count > 0)
                {
                    objST = new SupplierTransaction();
                    LoadSupplierOrderHeader(objST, ds.Tables["header"]);
                    LoadSupplierOrderGeneralDetails(objST, ds.Tables["generaldetails"]);
                    LoadSupplierOrderSummaryDetails(objST, ds.Tables["summary"]);
                    LoadSupplierOrderEditDetails(objST, ds.Tables["editdetails"]);
                    LoadSupplierOrderCancelDetails(objST, ds.Tables["canceldetails"]);
                    LoadPaymentDetails(objST, ds.Tables["paymentdetails"]);
                    LoadSupplierOrderProductDetails(objST, ds.Tables["productdetails"]);
                }
                else
                {
                    JObject jResponse = new JObject();
                    JObject jContent = new JObject();
                    JObject jResult = new JObject();

                    jResponse.Add("error", "1");
                    jResponse.Add("sys_msg", "0");
                    jResponse.Add("message", "Supplier Order - " + orderid + " not found");


                    jResult.Add("Content", jContent);
                    jResult.Add("Response", jResponse);
                    return jResult;
                }

                myJsonString = JsonConvert.SerializeObject(objST, Formatting.None);

                JObject json = new JObject();
                if (myJsonString != String.Empty)
                    json = JObject.Parse(myJsonString);
                return json;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public JObject GetStoreTransactionDetails(string transtype, string orderid)
        {
            string myJsonString = String.Empty;

            try
            {
                DataSet ds = dataManagementContext.GetStoreTransactionDetails(transtype, orderid);

                StoreTransaction objST = new StoreTransaction();

                JObject resultObject = new JObject();
                JObject DashboardDetailsObject = new JObject();
                JObject jgridObject = new JObject();

                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "header";
                    ds.Tables[1].TableName = "generaldetails";
                    ds.Tables[2].TableName = "editdetails";
                    ds.Tables[3].TableName = "canceldetails";
                    ds.Tables[4].TableName = "productdetails";
                }

                if (ds.Tables.Count > 0 && ds.Tables["header"].Rows.Count > 0)
                {
                    objST = new StoreTransaction();
                    LoadStoreOrderHeader(objST, ds.Tables["header"]);
                    LoadStoreOrderGeneralDetails(objST, ds.Tables["generaldetails"]);
                    LoadStoreOrderEditDetails(objST, ds.Tables["editdetails"]);
                    LoadStoreOrderCancelDetails(objST, ds.Tables["canceldetails"]);
                    LoadStoreOrderProductDetails(objST, ds.Tables["productdetails"]);
                }
                else
                {
                    JObject jResponse = new JObject();
                    JObject jContent = new JObject();
                    JObject jResult = new JObject();

                    jResponse.Add("error", "1");
                    jResponse.Add("sys_msg", "0");
                    jResponse.Add("message", "Store Order - " + orderid + " not found");


                    jResult.Add("Content", jContent);
                    jResult.Add("Response", jResponse);
                    return jResult;
                }

                myJsonString = JsonConvert.SerializeObject(objST, Formatting.None);

                JObject json = new JObject();
                if (myJsonString != String.Empty)
                    json = JObject.Parse(myJsonString);
                return json;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void LoadStoreOrderHeader(StoreTransaction obj, DataTable dt)
        {

            obj.orgid = dt.Rows[0]["orgid"].ToString();
            obj.zoneid = dt.Rows[0]["zoneid"].ToString();
            obj.locationid = dt.Rows[0]["locationid"].ToString();
            obj.userid = dt.Rows[0]["userid"].ToString();
            obj.type = dt.Rows[0]["type"].ToString();
            obj.typedesc = dt.Rows[0]["typedesc"].ToString();
            obj.orderstatus = dt.Rows[0]["orderstatus"].ToString();
            obj.orderstatusdesc = dt.Rows[0]["orderstatusdesc"].ToString();
            obj.orderid = dt.Rows[0]["orderid"].ToString();
            obj.orderdtm = dt.Rows[0]["orderdtm"].ToString();
            obj.ordernumber = dt.Rows[0]["ordernumber"].ToString();
            obj.iscancelled = dt.Rows[0]["iscancelled"].ToString();
        }

        private void LoadStoreOrderGeneralDetails(StoreTransaction obj, DataTable dt)
        {
            StoreTransGeneraldetails gd = new StoreTransGeneraldetails();
            gd.fromid = dt.Rows[0]["fromid"].ToString();
            gd.fromname = dt.Rows[0]["fromname"].ToString();
            gd.toid = dt.Rows[0]["toid"].ToString();
            gd.toname = dt.Rows[0]["toname"].ToString();
            gd.categoryid = dt.Rows[0]["categoryid"].ToString();
            gd.categoryname = dt.Rows[0]["categoryname"].ToString();
            gd.transfermode = dt.Rows[0]["transfermode"].ToString();
            gd.totalquantity = dt.Rows[0]["totalquantity"].ToString();
            gd.totalacceptedqty = dt.Rows[0]["totalacceptedqty"].ToString();
            gd.totalrejectedqty = dt.Rows[0]["totalrejectedqty"].ToString();

            obj.generaldetails = gd;
        }

        private void LoadStoreOrderEditDetails(StoreTransaction obj, DataTable dt)
        {
            if (dt.Rows.Count > 0) obj.editdetailslist = ConvertToList<StoreTransEditdetails>(dt);
        }

        private void LoadStoreOrderCancelDetails(StoreTransaction obj, DataTable dt)
        {
            if (dt.Rows.Count > 0) obj.canceldetailslist = ConvertToList<StoreTransCanceldetails>(dt);
        }
        private void LoadStoreOrderProductDetails(StoreTransaction obj, DataTable dt)
        {
            if (dt.Rows.Count > 0) obj.productdetails = ConvertToList<StoreTransProductdetail>(dt);
        }

        private void LoadSupplierOrderHeader(SupplierTransaction obj, DataTable dt)
        {

            obj.orgid = dt.Rows[0]["orgid"].ToString();
            obj.zoneid = dt.Rows[0]["zoneid"].ToString();
            obj.locationid = dt.Rows[0]["locationid"].ToString();
            obj.userid = dt.Rows[0]["userid"].ToString();
            obj.type = dt.Rows[0]["type"].ToString();
            obj.typedesc = dt.Rows[0]["typedesc"].ToString();
            obj.orderstatus = dt.Rows[0]["orderstatus"].ToString();
            obj.orderid = dt.Rows[0]["orderid"].ToString();
            obj.orderdtm = dt.Rows[0]["orderdtm"].ToString();
            obj.ordernumber = dt.Rows[0]["ordernumber"].ToString();
            obj.iscancelled = dt.Rows[0]["iscancelled"].ToString();
            obj.isaborted = dt.Rows[0]["isaborted"].ToString();
            obj.parentsupplierorderid = dt.Rows[0]["parentsupplierorderid"].ToString();
            obj.parentsupplierordernumber = dt.Rows[0]["parentsupplierordernumber"].ToString();
        }

        private void LoadSupplierOrderGeneralDetails(SupplierTransaction obj, DataTable dt)
        {
            SupplierTransGeneraldetails gd = new SupplierTransGeneraldetails();
            gd.storeid = dt.Rows[0]["storeid"].ToString();
            gd.storename = dt.Rows[0]["storename"].ToString();
            gd.categoryid = dt.Rows[0]["categoryid"].ToString();
            gd.categoryname = dt.Rows[0]["categoryname"].ToString();
            gd.supplierid = dt.Rows[0]["supplierid"].ToString();
            gd.suppliername = dt.Rows[0]["suppliername"].ToString();
            gd.supplieraddress = dt.Rows[0]["supplieraddress"].ToString();
            gd.taxfree = dt.Rows[0]["taxfree"].ToString();
            gd.stocktype = dt.Rows[0]["stocktype"].ToString();
            gd.billtype = dt.Rows[0]["billtype"].ToString();
            gd.billrefno = dt.Rows[0]["billrefno"].ToString();
            gd.billdate = dt.Rows[0]["billdate"].ToString();
            gd.poFromdate = "";
            gd.poTodate = "";

            obj.generaldetails = gd;
        }

        private void LoadSupplierOrderSummaryDetails(SupplierTransaction obj, DataTable dt)
        {
            SupplierTransSummary ss = new SupplierTransSummary();
            ss.totalquantity = dt.Rows[0]["totalquantity"].ToString();
            ss.totalamount = dt.Rows[0]["totalamount"].ToString();
            ss.expirydate = dt.Rows[0]["expirydate"].ToString();
            ss.freightcharges = dt.Rows[0]["freightcharges"].ToString();
            ss.othercharges = dt.Rows[0]["othercharges"].ToString();
            ss.roundoff = dt.Rows[0]["roundoff"].ToString();
            ss.cashdiscount = dt.Rows[0]["cashdiscount"].ToString();
            ss.returnreason = dt.Rows[0]["returnreason"].ToString();
            ss.returnmode = dt.Rows[0]["returnmode"].ToString();
            ss.returntype = dt.Rows[0]["returntype"].ToString();

            obj.summary = ss;
        }

        private void LoadSupplierOrderEditDetails(SupplierTransaction obj, DataTable dt)
        {
            if (dt.Rows.Count > 0) obj.editdetailslist = ConvertToList<SupplierTransEditdetails>(dt);
        }

        private void LoadSupplierOrderCancelDetails(SupplierTransaction obj, DataTable dt)
        {
            if (dt.Rows.Count > 0) obj.canceldetailslist = ConvertToList<SupplierTransCanceldetails>(dt);
        }

        private void LoadPaymentDetails(SupplierTransaction obj, DataTable dt)
        {
            if (dt.Rows.Count > 0) obj.paymentdetails = ConvertToList<SupplierTransPaymentdetail>(dt);
        }

        private void LoadSupplierOrderProductDetails(SupplierTransaction obj, DataTable dt)
        {
            if (dt.Rows.Count > 0) obj.productdetails = ConvertToList<SupplierTransProductdetail>(dt);
        }

        public JObject SearchProductDetails(ProductMasterInput productInput)
        {
            DataSet ds = dataManagementContext.SearchProductDetails(productInput);

            JObject resultObject = new JObject();
            JArray jArray;
            JObject jgridObject;
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("productId", dr["productid"].ToString());
                        jgridObject.Add("barcode", dr["barcode"].ToString());
                        jgridObject.Add("productdetail", dr["productdetail"].ToString());
                        jArray.Add(jgridObject);
                    }
                    resultObject.Add("productlist", jArray);


                }
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject GetUserManagementDetails(string orgid, string locationid, string type)
        {
            DataSet ds = dataManagementContext.GetUserManagementDetails(orgid, locationid, type);

            JObject resultObject = new JObject();
            JArray jArray;
            JObject jgridObject;
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    jArray = new JArray();
                    if (type == "USER")
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            jgridObject = new JObject();
                            jgridObject.Add("userid", dr["userid"].ToString());
                            jgridObject.Add("username", dr["username"].ToString());
                            string decryptedPassword = CommonConstraints.Decrypt(dr["password"].ToString(), true);
                            jgridObject.Add("password", decryptedPassword);
                            jgridObject.Add("displayname", dr["displayname"].ToString());
                            jgridObject.Add("active", dr["active"].ToString());
                            jArray.Add(jgridObject);
                        }
                        resultObject.Add("user", jArray);
                        jArray = new JArray();
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            jgridObject = new JObject();
                            jgridObject.Add("userroleid", dr["userroleid"].ToString());
                            jgridObject.Add("userid", dr["userid"].ToString());
                            jgridObject.Add("username", dr["username"].ToString());
                            jgridObject.Add("roleid", dr["roleid"].ToString());
                            jgridObject.Add("rolename", dr["rolename"].ToString());
                            jgridObject.Add("defaultrole", dr["defaultrole"].ToString());
                            jgridObject.Add("active", dr["active"].ToString());
                            jArray.Add(jgridObject);
                        }
                        resultObject.Add("userrole", jArray);

                    }
                    else if (type == "ROLE")
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            jgridObject = new JObject();
                            jgridObject.Add("roleid", dr["roleid"].ToString());
                            jgridObject.Add("rolename", dr["rolename"].ToString());
                            jgridObject.Add("active", dr["active"].ToString());
                            jArray.Add(jgridObject);
                        }
                        resultObject.Add("role", jArray);
                    }
                    else if (type == "USERROLE")
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            jgridObject = new JObject();
                            jgridObject.Add("userroleid", dr["userroleid"].ToString());
                            jgridObject.Add("userid", dr["userid"].ToString());
                            jgridObject.Add("username", dr["username"].ToString());
                            jgridObject.Add("roleid", dr["roleid"].ToString());
                            jgridObject.Add("rolename", dr["rolename"].ToString());
                            jgridObject.Add("defaultrole", dr["defaultrole"].ToString());
                            jgridObject.Add("active", dr["active"].ToString());
                            jArray.Add(jgridObject);
                        }
                        resultObject.Add("userrole", jArray);
                    }
                    else if (type == "USERSTORE")
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            jgridObject = new JObject();
                            jgridObject.Add("userstoreid", dr["userstoreid"].ToString());
                            jgridObject.Add("userid", dr["userid"].ToString());
                            jgridObject.Add("username", dr["username"].ToString());
                            jgridObject.Add("storeid", dr["storeid"].ToString());
                            jgridObject.Add("storename", dr["storename"].ToString());
                            jgridObject.Add("active", dr["active"].ToString());
                            jgridObject.Add("defaultselected", dr["defaultselected"].ToString());
                            jArray.Add(jgridObject);
                        }
                        resultObject.Add("userstore", jArray);
                    }
                    else if (type == "USERCOUNTER")
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            jgridObject = new JObject();
                            jgridObject.Add("usercounterid", dr["usercounterid"].ToString());
                            jgridObject.Add("userid", dr["userid"].ToString());
                            jgridObject.Add("username", dr["username"].ToString());
                            jgridObject.Add("storetype", dr["storetype"].ToString());
                            jgridObject.Add("storeid", dr["storeid"].ToString());
                            jgridObject.Add("storename", dr["storename"].ToString());
                            jgridObject.Add("counterid", dr["counterid"].ToString());
                            jgridObject.Add("countername", dr["countername"].ToString());
                            jgridObject.Add("active", dr["active"].ToString());
                            jgridObject.Add("defaultselected", dr["defaultselected"].ToString());
                            jArray.Add(jgridObject);
                        }
                        resultObject.Add("usercounter", jArray);
                    }



                }
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject GetSupplierTransactionHistory(SupplierTransactionHistoryInput input)
        {
            DataSet ds = dataManagementContext.GetSupplierTransactionHistory(input);

            JObject resultObject = new JObject();
            JArray jArray;
            JObject jgridObject;
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("supplierorderid", dr["supplierorderid"].ToString());
                        jgridObject.Add("supplierordernumber", dr["supplierordernumber"].ToString());
                        jgridObject.Add("supplierorderdtm", dr["supplierorderdtm"].ToString());
                        jgridObject.Add("supplierid", dr["supplierid"].ToString());
                        jgridObject.Add("suppliername", dr["suppliername"].ToString());
                        jgridObject.Add("storeid", dr["storeid"].ToString());
                        jgridObject.Add("storename", dr["storename"].ToString());
                        jgridObject.Add("purchaseprice", dr["purchaseprice"].ToString());
                        jgridObject.Add("discountper", dr["discountper"].ToString());
                        jArray.Add(jgridObject);
                    }
                    resultObject.Add("productlist", jArray);


                }
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public JObject GetSupplierOrderReturnNotes(string supplierorderid, string type)
        {
            try
            {
                DataSet result = dataManagementContext.GetSupplierOrderReturnNotes(supplierorderid, type);


                JObject resultObject = new JObject();
                JObject LookupMasterDetails = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("supplierorderid", (dr["supplierorderid"].ToString()));
                        jgridObject.Add("notes", (dr["notes"].ToString()));
                        jArray.Add(jgridObject);
                    }

                    resultObject.Add("returnnotes", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public JObject CRUDSupplierOrderReturnNotes(SupplierTransReturnRemarks objInput)
        {
            try
            {
                DataSet result = dataManagementContext.CRUD_SupplierOrderReturnNotes(objInput);
                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {
                    errcode = "1";
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }
                else
                {
                    errcode = "0";
                    message = "Replacement/CreditNote remarks details successfully processed";
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject GetAllCounters(string orgid, string locationid, string storeid)
        {
            try
            {
                DataSet result = dataManagementContext.GetAllCounters(orgid, locationid, storeid);


                JObject resultObject = new JObject();
                JObject countersobject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("id", (dr["id"].ToString()));
                        jgridObject.Add("description", (dr["description"].ToString()));

                        jArray.Add(jgridObject);
                    }



                    resultObject.Add("counters", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject GetAllStores(string orgid, string locationid, string counterid)
        {
            try
            {
                DataSet result = dataManagementContext.GetAllStores(orgid, locationid, counterid);


                JObject resultObject = new JObject();
                JObject countersobject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("id", (dr["id"].ToString()));
                        jgridObject.Add("description", (dr["description"].ToString()));

                        jArray.Add(jgridObject);
                    }



                    resultObject.Add("stores", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public JObject GetAllTransferStores(string orgid, string locationid, string storeid)
        {
            try
            {
                DataSet result = dataManagementContext.GetAllTransferStores(orgid, locationid, storeid);


                JObject resultObject = new JObject();
                JObject countersobject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("id", (dr["id"].ToString()));
                        jgridObject.Add("description", (dr["description"].ToString()));

                        jArray.Add(jgridObject);
                    }



                    resultObject.Add("stores", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject StockDeductions(StockDeduction data)
        {
            try
            {

                DataSet dsData = dataManagementContext.StockDeductions(data);

                string message = "Stock Deduction processed successfully";


                JObject jResponse = new JObject();
                JObject jContent = new JObject();
                JObject jResult = new JObject();

                jResponse.Add("error", "0");
                jResponse.Add("sys_msg", "0");

                jResponse.Add("message", message);

                jContent.Add("orgid", data.orgid);
                jContent.Add("locationid", data.locationid);
                jContent.Add("orderid", data.stockdeductionid);
                jContent.Add("displayordernumber", data.stockdeductionnumber);


                jResult.Add("Content", jContent);
                jResult.Add("Response", jResponse);

                return jResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject GetTransactionHistory(string uin)
        {
            try
            {
                DataSet result = dataManagementContext.GetTransactionHistory(uin);


                JObject resultObject = new JObject();
                JObject DashboardDetailsObject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {

                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("customerordernumber", (dr["customerordernumber"].ToString()));
                        jgridObject.Add("orderdtm", (dr["orderdtm"].ToString()));
                        jgridObject.Add("locationname", (dr["locationname"].ToString()));
                        jgridObject.Add("orderstatus", (dr["orderstatus"].ToString()));
                        jgridObject.Add("ordertype", (dr["ordertype"].ToString()));
                        jgridObject.Add("totalamount", (dr["totalamount"].ToString()));


                        jArray.Add(jgridObject);
                    }

                    resultObject.Add("UINHistory", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject InsCustomerVisit(InsCustomerVisit objInput)
        {
            try
            {
                DataSet result = dataManagementContext.InsCustomerVisit(objInput);
                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {
                    errcode = "1";
                    message = "User details successfully processed";

                }
                else
                {
                    errcode = "0";
                    message = "User details not inserted";
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject GetCustomerOrderStatusDetails(string orderid, string roleid)
        {
            try
            {
                DataSet ds = dataManagementContext.GetCustomerOrderStatusDetails(orderid, roleid);


                JObject resultObject = new JObject();
                JArray jArray;
                JObject jgridObject;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    resultObject.Add("customerorderid", ds.Tables[0].Rows[0]["customerorderid"].ToString());
                    resultObject.Add("orderno", ds.Tables[0].Rows[0]["orderno"].ToString());
                    resultObject.Add("displayordernumber", ds.Tables[0].Rows[0]["displayordernumber"].ToString());
                    resultObject.Add("orderdtm", ds.Tables[0].Rows[0]["orderdtm"].ToString());
                    resultObject.Add("currentorderstatus", ds.Tables[0].Rows[0]["currentorderstatus"].ToString());
                    resultObject.Add("schdeliverydate", ds.Tables[0].Rows[0]["schdeliverydate"].ToString());
                    resultObject.Add("schdeliverytime", ds.Tables[0].Rows[0]["schdeliverytime"].ToString());
                    resultObject.Add("deliverymode", ds.Tables[0].Rows[0]["deliverymode"].ToString());

                    resultObject.Add("framedetails", ds.Tables[0].Rows[0]["framedetails"].ToString());
                    resultObject.Add("framecost", ds.Tables[0].Rows[0]["framecost"].ToString());
                    resultObject.Add("leftlensdetails", ds.Tables[0].Rows[0]["leftlensdetails"].ToString());
                    resultObject.Add("leftlenscost", ds.Tables[0].Rows[0]["leftlenscost"].ToString());
                    resultObject.Add("leftlensattributes", ds.Tables[0].Rows[0]["leftlensattributes"].ToString());

                    resultObject.Add("rightlensdetails", ds.Tables[0].Rows[0]["rightlensdetails"].ToString());
                    resultObject.Add("rightlenscost", ds.Tables[0].Rows[0]["rightlenscost"].ToString());
                    resultObject.Add("rightlensattributes", ds.Tables[0].Rows[0]["rightlensattributes"].ToString());

                    resultObject.Add("leftorderstatusid", ds.Tables[0].Rows[0]["leftorderstatusid"].ToString());
                    resultObject.Add("leftorderstatusdesc", ds.Tables[0].Rows[0]["leftorderstatusdesc"].ToString());
                    resultObject.Add("leftsupplierid", ds.Tables[0].Rows[0]["leftsupplierid"].ToString());
                    resultObject.Add("leftlensstatus", ds.Tables[0].Rows[0]["leftlensstatus"].ToString());

                    resultObject.Add("rightorderstatusid", ds.Tables[0].Rows[0]["rightorderstatusid"].ToString());
                    resultObject.Add("rightorderstatusdesc", ds.Tables[0].Rows[0]["rightorderstatusdesc"].ToString());
                    resultObject.Add("rightsupplierid", ds.Tables[0].Rows[0]["rightsupplierid"].ToString());
                    resultObject.Add("rightlensstatus", ds.Tables[0].Rows[0]["rightlensstatus"].ToString());

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        //for (int i = ds.Tables[1].Rows.Count-1;  i >= 0; i--)
                        //{
                        //    if (i >0) ds.Tables[1].Rows[i]["timeout"] = ds.Tables[1].Rows[i - 1]["timein"].ToString();
                        //}
                        jArray = new JArray();
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            jgridObject = new JObject();
                            jgridObject.Add("orderstatusid", (dr["orderstatusid"].ToString()));
                            jgridObject.Add("orderstatusdesc", (dr["orderstatusdesc"].ToString()));
                            jgridObject.Add("timein", (dr["timein"].ToString()));
                            jgridObject.Add("timeout", (dr["timeout"].ToString()));
                            jgridObject.Add("userid", dr["userid"].ToString());
                            jgridObject.Add("username", dr["username"].ToString());
                            jgridObject.Add("remarks", dr["remarks"].ToString());
                            jgridObject.Add("supplierid", dr["supplierid"].ToString());
                            jgridObject.Add("suppliername", dr["suppliername"].ToString());
                            jgridObject.Add("rejectedreasonid", dr["rejectedreasonid"].ToString());
                            jgridObject.Add("rejectedreasondesc", dr["rejectedreasondesc"].ToString());
                            jgridObject.Add("rejecteduserid", dr["rejecteduserid"].ToString());
                            jgridObject.Add("rejectedreasonusername", dr["rejectedreasonusername"].ToString());

                            jArray.Add(jgridObject);

                        }
                        resultObject.Add("orderstatushistory", jArray);
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        jArray = new JArray();
                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            jgridObject = new JObject();
                            jgridObject.Add("ID", (dr["ID"].ToString()));
                            jgridObject.Add("Description", (dr["Description"].ToString()));
                            jArray.Add(jgridObject);
                        }
                        resultObject.Add("orderstatuslookup", jArray);
                    }

                    if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    {
                        jArray = new JArray();
                        foreach (DataRow dr in ds.Tables[3].Rows)
                        {
                            jgridObject = new JObject();
                            jgridObject.Add("ID", (dr["ID"].ToString()));
                            jgridObject.Add("Description", (dr["Description"].ToString()));
                            jArray.Add(jgridObject);
                        }
                        resultObject.Add("lensorderstatuslookup", jArray);
                    }
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject CRUD_CustomerOrderStatusTracking(CustomerOrderStatusTracking objInput)
        {
            try
            {
                DataSet result = dataManagementContext.CRUD_CustomerOrderStatusTracking(objInput);
                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {
                    errcode = "1";
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }
                else
                {
                    errcode = "0";
                    message = "Order Status successfully processed";
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject GetLensProductDetails(LensMatrixInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.GetLensProductDetails(objInput);


                JObject resultObject = new JObject();
                JObject LensMatrixBarcodeObject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("productid", (dr["productid"].ToString()));
                        jgridObject.Add("barcode", (dr["barcode"].ToString()));
                        jgridObject.Add("productdetail", (dr["productdetail"].ToString()));
                        jgridObject.Add("productname", (dr["productname"].ToString()));
                        jArray.Add(jgridObject);
                    }

                    resultObject.Add("lensdetails", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject ValidateLensBarcode(LensMatrixInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.ValidateLensBarcode(objInput);


                JObject resultObject = new JObject();
                JObject LensMatrixBarcodeObject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("status", (dr["status"].ToString()));
                        jArray.Add(jgridObject);
                    }

                    resultObject.Add("result", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject GetConsumptionBasedPODetails(ConsumptionBasedPOInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.GetConsumptionBasedPODetails(objInput);


                JObject resultObject = new JObject();
                JObject DashboardDetailsObject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("productid", (dr["productid"].ToString()));
                        jgridObject.Add("productname", (dr["productname"].ToString()));
                        jgridObject.Add("barcode", (dr["barcode"].ToString()));
                        jgridObject.Add("orderid", (dr["orderid"].ToString()));
                        jgridObject.Add("customerordernumber", (dr["customerordernumber"].ToString()));
                        jgridObject.Add("orderitemid", (dr["orderitemid"].ToString()));
                        jgridObject.Add("orderstatustrackingid", (dr["orderstatustrackingid"].ToString()));
                        jgridObject.Add("supplierid", (dr["supplierid"].ToString()));
                        jgridObject.Add("name", (dr["name"].ToString()));
                        jgridObject.Add("orderstatusid", (dr["orderstatusid"].ToString()));
                        jgridObject.Add("orderstatus", (dr["orderstatus"].ToString()));
                        jgridObject.Add("timein", (dr["timein"].ToString()));
                        jgridObject.Add("qty", (dr["qty"].ToString()));
                        jgridObject.Add("purchaseprice", (dr["purchaseprice"].ToString()));
                        jgridObject.Add("total", (dr["total"].ToString()));
                        jgridObject.Add("hsncode", (dr["hsncode"].ToString()));
                        jgridObject.Add("hsncodedesc", (dr["hsncodedesc"].ToString()));
                        jgridObject.Add("hsntaxperc", (dr["hsntaxperc"].ToString()));

                        jArray.Add(jgridObject);
                    }

                    resultObject.Add("consumptionpo", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject crudRoleDetails(crudRoleDetailsInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.crudRoleDetails(objInput);
                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {

                    errcode = result.Tables[0].Rows[0]["error"].ToString();
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JObject crudUserRoleDetails(crudUserRoleDetailsInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.crudUserRoleDetails(objInput);
                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {

                    errcode = result.Tables[0].Rows[0]["error"].ToString();
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject crudUserStoreDetails(crudUserStoreDetailsInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.crudUserStoreDetails(objInput);
                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {

                    errcode = result.Tables[0].Rows[0]["error"].ToString();
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject crudUserCounterDetails(crudUserCounterDetailsInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.crudUserCounterDetails(objInput);
                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {

                    errcode = result.Tables[0].Rows[0]["error"].ToString();
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject CRUD_CounterDetails(crudCounterInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.CRUD_CounterDetails(objInput);
                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {

                    errcode = result.Tables[0].Rows[0]["error"].ToString();
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject GetCounterDetails(crudCounterInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.GetCounterDetails(objInput);


                JObject resultObject = new JObject();
                JObject LensMatrixBarcodeObject = new JObject();
                JArray jArray = new JArray();
                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jArray = new JArray();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jgridObject = new JObject();
                        jgridObject.Add("counterid", (dr["counterid"].ToString()));
                        jgridObject.Add("storeid", (dr["storeid"].ToString()));
                        jgridObject.Add("storename", (dr["storename"].ToString()));
                        jgridObject.Add("countername", (dr["countername"].ToString()));
                        jgridObject.Add("shortname", (dr["shortname"].ToString()));
                        jgridObject.Add("active", (dr["active"].ToString()));
                        jgridObject.Add("isdeliverycounter", (dr["isdeliverycounter"].ToString()));
                        jgridObject.Add("camptype", (dr["camptype"].ToString()));
                        jgridObject.Add("campdate", (dr["campdate"].ToString()));
                        jgridObject.Add("campplace", (dr["campplace"].ToString()));
                        jgridObject.Add("expectedoutpatientcount", (dr["expectedoutpatientcount"].ToString()));

                        jArray.Add(jgridObject);
                    }

                    resultObject.Add("lensdetails", jArray);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public JObject GetUserRoleMenuAccessRights(crudUserRoleDetailsInput objinput)
        {
            try
            {
                DataSet result = dataManagementContext.GetUserRoleMenuAccessRights(objinput);

                JObject jgridObject = new JObject();

                if (result.Tables.Count > 0)
                {
                    jgridObject = new JObject();
                    string myJsonString = result.Tables[0].Rows[0]["scopejson"].ToString();
                    JObject json = new JObject();
                    if (myJsonString != String.Empty)
                        json = JObject.Parse(myJsonString);

                    jgridObject.Add("scopejson", json);
                }

                return jgridObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JObject CRUD_UserRoleMenuAccessRights(crudUserRoleDetailsInput objInput)
        {
            try
            {

                DataSet result = dataManagementContext.CRUD_UserRoleMenuAccessRights(objInput);


                JObject jObject = GetMenuItems(objInput.userid, objInput.roleid);
                objInput.actionjson = jObject.ToString();

                result = dataManagementContext.CRUD_UserRoleMenuAccessRights(objInput);


                string message = string.Empty;
                string errcode = string.Empty;
                if (result.Tables.Count > 0)
                {
                    errcode = "1";
                    message = result.Tables[0].Rows[0]["errormessage"].ToString();
                }
                else
                {
                    errcode = "0";
                    message = "User-Role-Menu-rights successfully processed";
                }

                JObject resultObject = ConvertToJsonResponseContent(errcode, "Success", message);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject GetMenuItems(string userid, string roleid)
        {
            JObject resultObject = new JObject();
            JObject menuObject = new JObject();
            JArray menuArrayObject = new JArray();
            JArray actionArrayObject = new JArray();
            JArray inwardArrayObject = new JArray();
            JArray returnArrayObject = new JArray();
            string showsuppliertrans = String.Empty, showstoretrans = String.Empty,
                showcustomerorders = String.Empty, showcustomerorderworklist = String.Empty,
                showmasterdetails = String.Empty, showreports = String.Empty, showlaborderworklist = string.Empty;
            string myJsonString;
            menuitem menuitemjson = new menuitem();

            string showsubmenu_orderentry = string.Empty, showsubmenu_directsales = string.Empty, showsubmenu_orderdiscount = string.Empty,
            showsubmenu_orderdelivery = string.Empty, showsubmenu_ordercancel = string.Empty, showsubmenu_stockdeduction = string.Empty;
            try
            {
                DataSet result = dataManagementContext.GetMenuItems(roleid, userid);
                if (result.Tables.Count > 0)
                {
                    showsuppliertrans = result.Tables[0].Rows[0]["showsuppliertrans"].ToString();
                    showstoretrans = result.Tables[0].Rows[0]["showstoretrans"].ToString();
                    showcustomerorders = result.Tables[0].Rows[0]["showcustomerorders"].ToString();
                    showcustomerorderworklist = result.Tables[0].Rows[0]["showcustomerorderworklist"].ToString();
                    showlaborderworklist = result.Tables[0].Rows[0]["showlaborderworklist"].ToString();
                    showmasterdetails = result.Tables[0].Rows[0]["showmasterdetails"].ToString();
                    showreports = result.Tables[0].Rows[0]["showreports"].ToString();
                    showsubmenu_orderentry = result.Tables[0].Rows[0]["showsubmenu_orderentry"].ToString();
                    showsubmenu_directsales = result.Tables[0].Rows[0]["showsubmenu_directsales"].ToString();
                    showsubmenu_orderdiscount = result.Tables[0].Rows[0]["showsubmenu_orderdiscount"].ToString();
                    showsubmenu_orderdelivery = result.Tables[0].Rows[0]["showorderdelivery"].ToString();
                    showsubmenu_ordercancel = result.Tables[0].Rows[0]["showordercancel"].ToString();
                    showsubmenu_stockdeduction = result.Tables[0].Rows[0]["showstockdeduction"].ToString();

                }

                if (showcustomerorderworklist == "1")
                {
                    menuitemjson = new menuitem();
                    menuitemjson.label = "Customer Order Worklist";
                    menuitemjson.isTitle = false;
                    menuitemjson.icon = "airplay";
                    menuitemjson.link = "apps/dashboard";
                    menuitemjson.children = null;
                    myJsonString = JsonConvert.SerializeObject(menuitemjson, Formatting.None);
                    if (myJsonString != String.Empty)
                        menuArrayObject.Add(JObject.Parse(myJsonString));

                }

                if (showsuppliertrans == "1")
                {
                    menuitemjson = new menuitem();
                    menuitemjson.label = "Supplier Transactions";
                    menuitemjson.isTitle = false;
                    menuitemjson.icon = "shopping-bag";
                    menuitemjson.link = "apps/supplier-order";
                    menuitemjson.children = null;
                    myJsonString = JsonConvert.SerializeObject(menuitemjson, Formatting.None);

                    if (myJsonString != String.Empty)
                        menuArrayObject.Add(JObject.Parse(myJsonString));


                }
                if (showstoretrans == "1")
                {
                    menuitemjson = new menuitem();
                    menuitemjson.label = "Store Transactions";
                    menuitemjson.isTitle = false;
                    menuitemjson.icon = "shopping-cart";
                    menuitemjson.link = "apps/stock-order";
                    menuitemjson.children = null;

                    myJsonString = JsonConvert.SerializeObject(menuitemjson, Formatting.None);

                    if (myJsonString != String.Empty)
                        menuArrayObject.Add(JObject.Parse(myJsonString));
                }
                if (showcustomerorders == "1")
                {
                    menuitemjson = new menuitem();
                    menuitemjson.label = "Customer Orders";
                    menuitemjson.isTitle = false;
                    menuitemjson.icon = "book";
                    menuitemjson.link = "apps/dashboard";
                    menuitemjson.collapsed = true;

                    submenuitem submenuitemjson = new submenuitem();
                    if (showsubmenu_orderentry == "1")
                    {
                        submenuitemjson.key = "Order Entry";
                        submenuitemjson.label = "Order Entry";
                        submenuitemjson.link = "/apps/orders/order-entry/New";
                        submenuitemjson.parentKey = "Customer Entry";
                        menuitemjson.children.Add(submenuitemjson);
                    }

                    if (showsubmenu_directsales == "1")
                    {
                        submenuitemjson = new submenuitem();
                        submenuitemjson.key = "Direct Sales";
                        submenuitemjson.label = "Direct Sales";
                        submenuitemjson.link = "/apps/orders/order-entry/DirectSale";
                        submenuitemjson.parentKey = "Customer Entry";
                        menuitemjson.children.Add(submenuitemjson);
                    }

                    if (showsubmenu_orderdiscount == "1")
                    {
                        submenuitemjson = new submenuitem();
                        submenuitemjson.key = "Order Discount";
                        submenuitemjson.label = "Order Discount";
                        submenuitemjson.link = "/apps/orders/order-entry/Discount";
                        submenuitemjson.parentKey = "Customer Entry";
                        menuitemjson.children.Add(submenuitemjson);
                    }
                    if (showsubmenu_orderdelivery == "1")
                    {
                        submenuitemjson = new submenuitem();
                        submenuitemjson.key = "Order Delivery";
                        submenuitemjson.label = "Order Delivery";
                        submenuitemjson.link = "/apps/orders/order-entry/Delivery";
                        submenuitemjson.parentKey = "Customer Entry";
                        menuitemjson.children.Add(submenuitemjson);
                    }
                    if (showsubmenu_ordercancel == "1")
                    {
                        submenuitemjson = new submenuitem();
                        submenuitemjson.key = "Order Cancel";
                        submenuitemjson.label = "Order Cancel";
                        submenuitemjson.link = "/apps/orders/order-entry/Cancel";
                        submenuitemjson.parentKey = "Customer Entry";
                        menuitemjson.children.Add(submenuitemjson);

                    }

                    if (showsubmenu_stockdeduction == "1")
                    {
                        submenuitemjson = new submenuitem();
                        submenuitemjson.key = "stock";
                        submenuitemjson.label = "Stock Deduction";
                        submenuitemjson.link = "/apps/orders/order-entry/stock";
                        submenuitemjson.parentKey = "Customer Entry";
                        menuitemjson.children.Add(submenuitemjson);
                    }


                    myJsonString = JsonConvert.SerializeObject(menuitemjson, Formatting.None);
                    if (myJsonString != String.Empty)
                        menuArrayObject.Add(JObject.Parse(myJsonString));

                }
                if (showlaborderworklist == "1")
                {
                    menuitemjson = new menuitem();
                    menuitemjson.label = "Lab Order Worklist";
                    menuitemjson.isTitle = false;
                    menuitemjson.icon = "eye";
                    menuitemjson.link = "apps/lab-order-worklist";
                    menuitemjson.children = null;

                    myJsonString = JsonConvert.SerializeObject(menuitemjson, Formatting.None);
                    if (myJsonString != String.Empty)
                        menuArrayObject.Add(JObject.Parse(myJsonString));

                }


                if (showmasterdetails == "1")
                {
                    menuitemjson = new menuitem();
                    menuitemjson.label = "Master Details";
                    menuitemjson.isTitle = false;
                    menuitemjson.icon = "clipboard";
                    menuitemjson.link = "apps/dashboard";
                    menuitemjson.collapsed = true;

                    submenuitem submenuitemjson = new submenuitem();
                    submenuitemjson.key = "Entity Configurations";
                    submenuitemjson.label = "Entity Configurations";
                    submenuitemjson.link = "/apps/master-details/common-master";
                    submenuitemjson.parentKey = "Master Entry";
                    menuitemjson.children.Add(submenuitemjson);

                    submenuitemjson = new submenuitem();
                    submenuitemjson.key = "Master Entry";
                    submenuitemjson.label = "Master Entry";
                    submenuitemjson.link = "/apps/master-details/common-master";
                    submenuitemjson.parentKey = "Master Entry";
                    menuitemjson.children.Add(submenuitemjson);

                    submenuitemjson = new submenuitem();
                    submenuitemjson.key = "Supplier Master";
                    submenuitemjson.label = "Supplier Master";
                    submenuitemjson.link = "/apps/master-details/supplier-master";
                    submenuitemjson.parentKey = "Supplier Entry";
                    menuitemjson.children.Add(submenuitemjson);

                    submenuitemjson = new submenuitem();
                    submenuitemjson.key = "Product Entry";
                    submenuitemjson.label = "Product Entry";
                    submenuitemjson.link = "/apps/master-details/product-master";
                    submenuitemjson.parentKey = "Product Entry";
                    menuitemjson.children.Add(submenuitemjson);

                    submenuitemjson = new submenuitem();
                    submenuitemjson.key = "User Master";
                    submenuitemjson.label = "User Master";
                    submenuitemjson.link = "/apps/user-details/user-management";
                    submenuitemjson.parentKey = "User Management";
                    menuitemjson.children.Add(submenuitemjson);


                    myJsonString = JsonConvert.SerializeObject(menuitemjson, Formatting.None);
                    if (myJsonString != String.Empty)
                        menuArrayObject.Add(JObject.Parse(myJsonString));

                }

                if (showreports == "1")
                {
                    menuitemjson = new menuitem();
                    menuitemjson.label = "Reports";
                    menuitemjson.isTitle = false;
                    menuitemjson.icon = "briefcase";
                    menuitemjson.link = "apps/reports";
                    menuitemjson.children = null;

                    myJsonString = JsonConvert.SerializeObject(menuitemjson, Formatting.None);
                    if (myJsonString != String.Empty)
                        menuArrayObject.Add(JObject.Parse(myJsonString));

                }
                resultObject.Add("menus", menuArrayObject);

           

                string addsupplierorder = string.Empty, addsupplierreturn = string.Empty, showsupplierorderworklist = string.Empty,
                viewpo = string.Empty, editpo = string.Empty, cancelpo = string.Empty, addinwardentries = string.Empty, reprint = string.Empty,
                supplierinwardworklist = string.Empty, viewinward = string.Empty, inwardreprint = string.Empty,
                supplierreturnworklist = string.Empty, viewreturn = string.Empty, cancelreturn = string.Empty, creditnotereceived = string.Empty,
                replacementreceived = string.Empty, addstocktransfer = string.Empty, addstockreturn = string.Empty, stocktransferworklist = string.Empty,
                viewtransfer = string.Empty, acceptrejecttransfer = string.Empty, canceltransfer = string.Empty,
                showstockreturnworklist = string.Empty, acceptrejectreturn = string.Empty, stockreturnworklist = string.Empty,
                orderentry = String.Empty, directsales = String.Empty, orderdiscount = String.Empty, orderdelivery = String.Empty, ordercancel = String.Empty, stockdeduction = String.Empty,
                submenu = String.Empty, customerordersworklist = String.Empty, customerfetch = String.Empty,
                customervieworderhistory = String.Empty,
                customerorderstatuschange = String.Empty,
                customerorderview = String.Empty,
                customerorderedit = String.Empty,
                customerbillreprint = String.Empty, customerorderdiscount = String.Empty, customerorderdelivery = String.Empty, customerordercancel = String.Empty,

                labordersworklist = String.Empty,
                labfetch = String.Empty,
                labvieworderhistory = String.Empty,
                laborderstatuschange = String.Empty,
                laborderview = String.Empty,
                laborderedit = String.Empty,
                labbillreprint = String.Empty, laborderdiscount = String.Empty, laborderdelivery = String.Empty, labordercancel = String.Empty,
                entityconfigurations = String.Empty, masterentry = String.Empty, suppliermaster = String.Empty, productmaster = String.Empty, usermaster = String.Empty, mastersubmenu = String.Empty,
                reports = String.Empty, showsuppliertransfer = String.Empty,
                viewstockreturn = String.Empty, stockreprint = String.Empty, cancelstockreturn = String.Empty,
                editinward = String.Empty, cancelinward = String.Empty,
                abortpo = string.Empty, customerorderreprint = string.Empty,
                supplierorderreport = string.Empty, showsupplierreports = string.Empty, supplierordercancellationreport = string.Empty, supplierinwardreport = string.Empty,
                supplierinwardpromotionalofferreport = string.Empty, supplierreturnreport = string.Empty, suppliercreditnotesreport = string.Empty,
                showstoresreport = string.Empty, stocktransferreport = string.Empty, stockreturnreport = string.Empty,
                stockintransitreport = string.Empty, showstockreport = string.Empty, storesstockmovementreport = string.Empty, storescurrentstockreport = string.Empty,
                storescostcomreport = string.Empty, storesexpirayreport = string.Empty, sectionstockmovementreport = string.Empty, sectioncurrentstockreport = string.Empty,
                sectioncostcomreport = string.Empty, sectionexpiryreport = string.Empty, allstockmovementreport = string.Empty, showcustomerreport = string.Empty,
                ordercollectionreport = string.Empty, deliverycollectionreport = string.Empty, directsalecollectionreport = string.Empty, ordercancellationreport = string.Empty,
                orderdiscountreport = string.Empty, stockdeductionreport = string.Empty, orderdetailsreport = string.Empty, directorderdetailsreport = string.Empty,
                undeliveryreport = string.Empty, salespersonreport = string.Empty, showtrackingreport = string.Empty, ontimetrackingreport = string.Empty,
                ordertrackingreport = string.Empty, hourlyreport = string.Empty, showaccountsreport = string.Empty, hsnwisereport = string.Empty,
                postalreport = string.Empty, cashflowreport = string.Empty, auditreport = string.Empty, tallysalessummaryreport = string.Empty,
                tallypurchasesummaryreport = string.Empty, tallysummaryreport = string.Empty;

                /* Supplier transactions - action items - Enable/disable */
                #region "Supplier Transactions - screen elements access rights"

                if (result.Tables.Count > 1)
                {
                    // showsuppliertransfer= result.Tables[1].Rows[0]["showsuppliertransfer"].ToString();
                    addsupplierorder = result.Tables[1].Rows[0]["addsupplierorder"].ToString();
                    addsupplierreturn = result.Tables[1].Rows[0]["addsupplierreturn"].ToString();
                    showsupplierorderworklist = result.Tables[2].Rows[0]["showsupplierorderworklist"].ToString();
                    viewpo = result.Tables[2].Rows[0]["viewpo"].ToString();
                    editpo = result.Tables[2].Rows[0]["editpo"].ToString();
                    cancelpo = result.Tables[2].Rows[0]["cancelpo"].ToString();
                    addinwardentries = result.Tables[2].Rows[0]["addinwardentries"].ToString();
                    abortpo = result.Tables[2].Rows[0]["abortpo"].ToString();
                    reprint = result.Tables[2].Rows[0]["reprint"].ToString();
                    supplierinwardworklist = result.Tables[3].Rows[0]["supplierinwardworklist"].ToString();
                    viewinward = result.Tables[3].Rows[0]["viewinward"].ToString();
                    editinward = result.Tables[3].Rows[0]["editinward"].ToString();
                    cancelinward = result.Tables[3].Rows[0]["cancelinward"].ToString();
                    inwardreprint = result.Tables[3].Rows[0]["reprint"].ToString();
                    supplierreturnworklist = result.Tables[4].Rows[0]["supplierreturnworklist"].ToString();
                    viewreturn = result.Tables[4].Rows[0]["viewreturn"].ToString();
                    cancelreturn = result.Tables[4].Rows[0]["cancelreturn"].ToString();
                    creditnotereceived = result.Tables[4].Rows[0]["creditnotereceived"].ToString();
                    replacementreceived = result.Tables[4].Rows[0]["replacementreceived"].ToString();
                    reprint = result.Tables[4].Rows[0]["reprint"].ToString();
                    addstocktransfer = result.Tables[5].Rows[0]["addstocktransfer"].ToString();
                    addstockreturn = result.Tables[5].Rows[0]["addstockreturn"].ToString();
                    stocktransferworklist = result.Tables[6].Rows[0]["stocktransferworklist"].ToString();
                    viewtransfer = result.Tables[6].Rows[0]["viewtransfer"].ToString();
                    acceptrejecttransfer = result.Tables[6].Rows[0]["acceptrejecttransfer"].ToString();
                    canceltransfer = result.Tables[6].Rows[0]["canceltransfer"].ToString();

                    stockreturnworklist = result.Tables[7].Rows[0]["stockreturnworklist"].ToString();
                    viewstockreturn = result.Tables[7].Rows[0]["viewreturn"].ToString();
                    acceptrejectreturn = result.Tables[7].Rows[0]["acceptrejectreturn"].ToString();
                    cancelstockreturn = result.Tables[7].Rows[0]["cancelreturn"].ToString();
                    stockreprint = result.Tables[7].Rows[0]["reprint"].ToString();

                    submenu = result.Tables[8].Rows[0]["submenu"].ToString();
                    orderentry = result.Tables[8].Rows[0]["orderentry"].ToString();
                    directsales = result.Tables[8].Rows[0]["directsales"].ToString();
                    orderdiscount = result.Tables[8].Rows[0]["orderdiscount"].ToString();
                    orderdelivery = result.Tables[8].Rows[0]["orderdelivery"].ToString();
                    ordercancel = result.Tables[8].Rows[0]["ordercancel"].ToString();
                    stockdeduction = result.Tables[8].Rows[0]["stockdeduction"].ToString();

                    customerordersworklist = result.Tables[9].Rows[0]["customerordersworklist"].ToString();
                    customerfetch = result.Tables[9].Rows[0]["fetch"].ToString();
                    customervieworderhistory = result.Tables[9].Rows[0]["vieworderhistory"].ToString();
                    customerorderstatuschange = result.Tables[9].Rows[0]["orderstatuschange"].ToString();
                    customerorderview = result.Tables[9].Rows[0]["orderview"].ToString();
                    customerorderedit = result.Tables[9].Rows[0]["orderedit"].ToString();

                    customerorderdiscount = result.Tables[9].Rows[0]["orderdiscount"].ToString();
                    customerorderdelivery = result.Tables[9].Rows[0]["orderdelivery"].ToString();
                    customerordercancel = result.Tables[9].Rows[0]["ordercancel"].ToString();
                    customerorderreprint = result.Tables[9].Rows[0]["orderreprint"].ToString();
                    customerbillreprint = result.Tables[9].Rows[0]["billreprint"].ToString();




                    labordersworklist = result.Tables[10].Rows[0]["labordersworklist"].ToString();
                    labfetch = result.Tables[10].Rows[0]["fetch"].ToString();
                    labvieworderhistory = result.Tables[10].Rows[0]["vieworderhistory"].ToString();
                    laborderstatuschange = result.Tables[10].Rows[0]["orderstatuschange"].ToString();
                    laborderview = result.Tables[10].Rows[0]["orderview"].ToString();
                    laborderedit = result.Tables[10].Rows[0]["orderedit"].ToString();
                    laborderdiscount = result.Tables[10].Rows[0]["orderdiscount"].ToString();
                    laborderdelivery = result.Tables[10].Rows[0]["orderdelivery"].ToString();
                    labordercancel = result.Tables[10].Rows[0]["ordercancel"].ToString();
                    labbillreprint = result.Tables[10].Rows[0]["billreprint"].ToString();


                    mastersubmenu = result.Tables[11].Rows[0]["mastersubmenu"].ToString();
                    entityconfigurations = result.Tables[11].Rows[0]["entityconfigurations"].ToString();
                    masterentry = result.Tables[11].Rows[0]["masterentry"].ToString();
                    suppliermaster = result.Tables[11].Rows[0]["suppliermaster"].ToString();
                    productmaster = result.Tables[11].Rows[0]["productmaster"].ToString();
                    usermaster = result.Tables[11].Rows[0]["usermaster"].ToString();

                    reports = result.Tables[12].Rows[0]["reports"].ToString();


                }

                #endregion

                JObject supplierorderworklistObj = new JObject();

                supplierorderworklistObj.Add("viewpo", viewpo);
                supplierorderworklistObj.Add("editpo", editpo);
                supplierorderworklistObj.Add("cancelpo", cancelpo);
                supplierorderworklistObj.Add("addinwardentries", addinwardentries);
                supplierorderworklistObj.Add("abortpo", abortpo);
                supplierorderworklistObj.Add("reprint", reprint);

                //  JObject showsupplierinwardworklistObj = new JObject();
                // showsupplierinwardworklistObj.Add("showsupplierinwardworklist", supplierinwardworklist);
                JObject supplierinwardworklistObj = new JObject();

                supplierinwardworklistObj.Add("viewinward", viewinward);
                supplierinwardworklistObj.Add("editinward", editinward);
                supplierinwardworklistObj.Add("cancelinward", cancelinward);
                supplierinwardworklistObj.Add("reprint", reprint);

                JObject supplierreturnworklistObj = new JObject();
                supplierreturnworklistObj.Add("viewreturn", viewreturn);
                supplierreturnworklistObj.Add("cancelreturn", cancelreturn);
                supplierreturnworklistObj.Add("creditnotereceived", creditnotereceived);
                supplierreturnworklistObj.Add("replacementreceived", replacementreceived);
                supplierreturnworklistObj.Add("reprint", reprint);

                JObject suppliertransactionsObj = new JObject();
                suppliertransactionsObj.Add("addsupplierorder", addsupplierorder);
                suppliertransactionsObj.Add("addsupplierreturn", addsupplierreturn);

                suppliertransactionsObj.Add("showsupplierorderworklist", showsupplierorderworklist);
                suppliertransactionsObj.Add("supplierorderworklist", supplierorderworklistObj);

                suppliertransactionsObj.Add("showsupplierinwardworklist", supplierinwardworklist);

                // supplierorderworklistObj.Add("showsupplierinwardworklist", supplierinwardworklist);
                suppliertransactionsObj.Add("supplierinwardworklist", supplierinwardworklistObj);

                suppliertransactionsObj.Add("showsupplierreturnworklist", supplierreturnworklist);
                suppliertransactionsObj.Add("supplierreturnworklist", supplierreturnworklistObj);

                JObject stocktransferworklistObj = new JObject();

                stocktransferworklistObj.Add("viewtransfer", viewtransfer);
                stocktransferworklistObj.Add("acceptrejecttransfer", acceptrejecttransfer);
                stocktransferworklistObj.Add("canceltransfer", canceltransfer);
                stocktransferworklistObj.Add("reprint", reprint);

                JObject stockreturnworklistObj = new JObject();

                stockreturnworklistObj.Add("viewstockreturn", viewstockreturn);
                stockreturnworklistObj.Add("acceptrejectreturn", acceptrejectreturn);
                stockreturnworklistObj.Add("cancelstockreturn", cancelstockreturn);
                stockreturnworklistObj.Add("stockreprint", stockreprint);

                JObject storetransactionsObj = new JObject();
                storetransactionsObj.Add("addstocktransfer", addstocktransfer);
                storetransactionsObj.Add("addstockreturn", addstockreturn);
                storetransactionsObj.Add("showstocktransferworklist", stocktransferworklist);
                storetransactionsObj.Add("stocktransferworklist", stocktransferworklistObj);
                storetransactionsObj.Add("showstockreturnworklist", stockreturnworklist);
                storetransactionsObj.Add("stockreturnworklist", stockreturnworklistObj);
                /*
                JObject submenuObj = new JObject();  
                submenuObj.Add ("orderentry", orderentry);
                submenuObj.Add ("directsales", directsales);
                submenuObj.Add ("orderdiscount", orderdiscount);
                submenuObj.Add ("orderdelivery", orderdelivery);
                submenuObj.Add ("ordercancel", ordercancel);
                submenuObj.Add ("stockdeduction", stockdeduction);

                JObject customerordersObj = new JObject();
                customerordersObj.Add("showsubmenu",submenu);
                customerordersObj.Add("submenu",submenuObj);
                */
                JObject customerordersworklistactionObj = new JObject();
                customerordersworklistactionObj.Add("fetch", customerfetch);
                customerordersworklistactionObj.Add("vieworderhistory", customervieworderhistory);
                customerordersworklistactionObj.Add("orderstatuschange", customerorderstatuschange);
                customerordersworklistactionObj.Add("orderview", customerorderview);
                customerordersworklistactionObj.Add("orderedit", customerorderedit);
                customerordersworklistactionObj.Add("orderdiscount", customerorderdiscount);
                customerordersworklistactionObj.Add("orderdelivery", customerorderdelivery);
                customerordersworklistactionObj.Add("ordercancel", customerordercancel);
                customerordersworklistactionObj.Add("orderreprint", customerorderreprint);
                customerordersworklistactionObj.Add("billreprint", customerbillreprint);

                JObject customerordersworklistObj = new JObject();
                customerordersworklistObj.Add("showcustomerordersworklist", customerordersworklist);
                customerordersworklistObj.Add("action", customerordersworklistactionObj);

                JObject labordersworklistactionObj = new JObject();
                labordersworklistactionObj.Add("fetch", labfetch);
                labordersworklistactionObj.Add("vieworderhistory", labvieworderhistory);
                labordersworklistactionObj.Add("orderstatuschange", laborderstatuschange);
                labordersworklistactionObj.Add("orderview", laborderview);
                labordersworklistactionObj.Add("orderedit", laborderedit);
                labordersworklistactionObj.Add("orderdiscount", laborderdiscount);
                labordersworklistactionObj.Add("orderdelivery", laborderdelivery);
                labordersworklistactionObj.Add("ordercancel", labordercancel);
                labordersworklistactionObj.Add("billreprint", labbillreprint);

                JObject labordersworklistObj = new JObject();
                labordersworklistObj.Add("showlabordersworklist", labordersworklist);
                labordersworklistObj.Add("action", labordersworklistactionObj);
                /*
                JObject mastersubmenuObj = new JObject();  
                mastersubmenuObj.Add ("entityconfigurations", entityconfigurations);
                mastersubmenuObj.Add ("masterentry", masterentry);
                mastersubmenuObj.Add ("suppliermaster", suppliermaster);
                mastersubmenuObj.Add ("productmaster", productmaster);
                mastersubmenuObj.Add ("usermaster", usermaster);

                JObject masterdetailsObj = new JObject();
                masterdetailsObj.Add("showsubmenu",mastersubmenu);
                masterdetailsObj.Add("submenu",mastersubmenuObj);
                */
                JObject reportsObj = new JObject();
                reportsObj.Add("showsupplierreports", showsupplierreports);
                reportsObj.Add("showreports", reports);
                JObject supplierreportsObj = new JObject();
                supplierreportsObj.Add("supplierorderreport", supplierorderreport);
                supplierreportsObj.Add("supplierordercancellationreport", supplierordercancellationreport);
                supplierreportsObj.Add("supplierinwardreport", supplierinwardreport);
                supplierreportsObj.Add("supplierinwardpromotionalofferreport", supplierinwardpromotionalofferreport);
                supplierreportsObj.Add("supplierreturnreport", supplierreturnreport);
                supplierreportsObj.Add("suppliercreditnotesreport", suppliercreditnotesreport);

                reportsObj.Add("supplierreports", supplierreportsObj);


                reportsObj.Add("showstoresreport", showstoresreport);
                JObject storesreportObj = new JObject();
                storesreportObj.Add("stocktransferreport", stocktransferreport);
                storesreportObj.Add("stockreturnreport", stockreturnreport);
                storesreportObj.Add("stockintransitreport", stockintransitreport);

                reportsObj.Add("storesreport", storesreportObj);

                reportsObj.Add("showstockreport", showstockreport);
                JObject stockreportObj = new JObject();
                stockreportObj.Add("storesstockmovementreport", storesstockmovementreport);
                stockreportObj.Add("storescurrentstockreport", storescurrentstockreport);
                stockreportObj.Add("storescostcomreport", storescostcomreport);
                stockreportObj.Add("storesexpirayreport", storesexpirayreport);
                stockreportObj.Add("sectionstockmovementreport", sectionstockmovementreport);
                stockreportObj.Add("sectioncurrentstockreport", sectioncurrentstockreport);
                stockreportObj.Add("sectioncostcomreport", sectioncostcomreport);
                stockreportObj.Add("sectionexpiryreport", sectionexpiryreport);
                stockreportObj.Add("allstockmovementreport", allstockmovementreport);

                reportsObj.Add("showcustomerreport", showcustomerreport);
                JObject customerreporttObj = new JObject();
                customerreporttObj.Add("ordercollectionreport", ordercollectionreport);
                customerreporttObj.Add("deliverycollectionreport", deliverycollectionreport);
                customerreporttObj.Add("directsalecollectionreport", directsalecollectionreport);
                customerreporttObj.Add("ordercancellationreport", ordercancellationreport);
                customerreporttObj.Add("orderdiscountreport", orderdiscountreport);
                customerreporttObj.Add("stockdeductionreport", stockdeductionreport);
                customerreporttObj.Add("orderdetailsreport", orderdetailsreport);
                customerreporttObj.Add("directorderdetailsreport", directorderdetailsreport);
                customerreporttObj.Add("undeliveryreport", undeliveryreport);
                customerreporttObj.Add("salespersonreport", salespersonreport);

                reportsObj.Add("customerreport", customerreporttObj);

                reportsObj.Add("showtrackingreport", showtrackingreport);


                JObject trackingreporttObj = new JObject();
                trackingreporttObj.Add("ontimetrackingreport", ontimetrackingreport);
                trackingreporttObj.Add("ordertrackingreport", ordertrackingreport);
                trackingreporttObj.Add("hourlyreport", hourlyreport);

                reportsObj.Add("trackingreport", trackingreporttObj);

                reportsObj.Add("showaccountsreport", showaccountsreport);
                JObject accountsreportObj = new JObject();
                accountsreportObj.Add("hsnwisereport", hsnwisereport);
                accountsreportObj.Add("postalreport", postalreport);
                accountsreportObj.Add("cashflowreport", cashflowreport);
                accountsreportObj.Add("auditreport", auditreport);
                accountsreportObj.Add("tallysalessummaryreport", tallysalessummaryreport);
                accountsreportObj.Add("tallypurchasesummaryreport", tallypurchasesummaryreport);
                accountsreportObj.Add("tallysummaryreport", tallysummaryreport);

                reportsObj.Add("accountsreport", accountsreportObj);


                JObject actionsObj = new JObject();

                actionsObj.Add("suppliertransactions", suppliertransactionsObj);
                actionsObj.Add("storetransactions", storetransactionsObj);
                //actionsObj.Add("customerorders", customerordersObj);
                actionsObj.Add("customerordersworklist", customerordersworklistObj);
                actionsObj.Add("labordersworklist", labordersworklistObj);
                //actionsObj.Add("masterdetasils", masterdetailsObj);
                actionsObj.Add("reports", reportsObj);
                actionArrayObject.Add(actionsObj);
                resultObject.Add("actions", actionArrayObject);

            }
            catch (Exception ex)
            {

            }
            return resultObject;

        }

        public JObject BuildMenuItems(string userid, string roleid)
        {
            JObject resultObject = new JObject();
            JObject menuObject = new JObject();
            JArray menuArrayObject = new JArray();
            JArray actionArrayObject = new JArray();
            string showsuppliertrans = String.Empty, showstoretrans = String.Empty,
                showcustomerorders = String.Empty, showcustomerorderworklist = String.Empty,
                showmasterdetails = String.Empty, showreports = String.Empty, showlaborderworklist = string.Empty;
            string myJsonString;
            menuitem menuitemjson = new menuitem();
            try
            {
                DataSet result = dataManagementContext.GetRoleMenuJson(userid, roleid);
                if (result.Tables.Count > 0)
                {
                    dynamic stuff = JsonConvert.DeserializeObject(result.Tables[0].Rows[0]["scopejson"].ToString());

                    JArray test = stuff.menus;
                    //var res =test.Where(x => (isview == "true")).ToList();

                }
            }
            catch (Exception ex)
            {

            }
            return resultObject;

        }

    }
}


