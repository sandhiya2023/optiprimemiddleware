using Microsoft.AspNetCore.Mvc;
using aravindMiddleware.Portal;
using Newtonsoft.Json;
using aravindMiddleware.API.Services;
using aravindMiddleware.Data.DapperClasses;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using aravindMiddleware.Data;
using System.Data;
using FastReport.Export.PdfSimple;
using FastReport;
using RestSharp;
using System.Text.Json;

namespace aravindMiddleware.API
{
    [Authorize]
    [ApiController]
    [Route("api/v1")]
    public class OpticalController : ControllerBase
    {
        OpticalPortal objPortal;
        string CLASS_NAME = "OpticalController";
        private readonly IConfiguration Configuration;
        private IUserServices _userService = new UserServices();
        string emrPostOrderInfoAPI;

        public OpticalController(IConfiguration configuration)
        {
            Configuration = configuration;
            emrPostOrderInfoAPI = Configuration["KeySettings:EMRPostOrderInfoAPI"];
        }

        [AllowAnonymous] //to do
        [HttpGet]
        [Route("LookupData/{orgid}/{locationid}/{lookuptype}/{orgcode?}")]
        public IActionResult LookupData([FromRoute] string orgid, string locationid, String lookuptype, String? orgcode = null)
        {
            string METHOD_NAME = "LookupData";
            string inputdataSave = Configuration["KeySettings:keyname"];
            lookuptype = lookuptype.ToUpper();

            try
            {
                objPortal = new OpticalPortal();
                if (lookuptype != "LOGIN" && lookuptype != "DASHBOARD" && lookuptype != "ORDERENTRY" && lookuptype != "SUPPLIER" && lookuptype != "PRODUCTATTR"
                  && lookuptype != "FRAMEATTR" && lookuptype != "LENSATTR" && lookuptype != "MISCATTR" && lookuptype != "ALL" && lookuptype != "LOOKUP"
                  && lookuptype != "USERMANAGEMENT" && lookuptype != "PURCHASERETURN" && lookuptype != "SUPPILERORDERDETAILS" && lookuptype != "SUPPLIERORDERWORKLIST"
                  && lookuptype != "SUPPLIERREPORT" && lookuptype != "STOREORDERWORKLIST" && lookuptype != "STOREORDERDETAILS" && lookuptype != "LAB")
                {
                    return BadRequest(new { message = "Invalid Look Type requested" });
                }

                var status = objPortal.LookupData(orgid, locationid, lookuptype, orgcode);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }


        [AllowAnonymous] //to do
        [HttpPost]
        [Route("GetproductDetailByCode")]
        public IActionResult GetproductDetailByCode([FromBody] ProductMasterInput productInput)
        {
            string METHOD_NAME = "GetproductDetailByCode";

            try
            {
                objPortal = new OpticalPortal();
                if (productInput.categoryname.ToUpper() != "FRAME" && productInput.categoryname.ToUpper() != "LENS"
                    && productInput.categoryname.ToUpper() != "MISC ITEMS")
                {
                    return BadRequest(new { message = "Invalid category type requested. Should be one of FRAME/LENS/MISC ITEMS" });
                }

                var status = objPortal.GetProductDetailsByCode(productInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }




        /// <summary>
        ///     Returns Userdetails if login is successful
        /// </summary>
        /// 
        [AllowAnonymous]
        [HttpPost]
        [Route("ValidateUserLogin")]
        public IActionResult ValidateUserLogin([FromBody] UserLogin userdata)
        {
            string METHOD_NAME = "ValidateUserLogin";
            string Secret = Configuration["KeySettings:Secret"];

            try
            {
                objPortal = new OpticalPortal();
                List<users> loginUsers = objPortal.ValidateUserLogin(userdata);


                JObject jResponse = new JObject();
                JObject jContent = new JObject();
                JObject jResult = new JObject();

                if (loginUsers.Count > 0)
                {
                    string token = _userService.Authenticate(Secret);

                    jResponse.Add("error", "0");
                    jResponse.Add("sys_msg", "0");
                    jResponse.Add("message", "Login Successful");

                    jContent.Add("orgid", loginUsers[0].orgid);
                    jContent.Add("zoneid", loginUsers[0].zoneid);
                    jContent.Add("locationid", loginUsers[0].locationid);
                    jContent.Add("userid", loginUsers[0].userId);
                    jContent.Add("username", loginUsers[0].username);
                    jContent.Add("currencysymbol", loginUsers[0].currencySymbol);
                    jContent.Add("token", token);

                    JObject jobj1 = new JObject();
                    jobj1.Add("frame", loginUsers[0].expirymonths.frame);
                    jobj1.Add("lens", loginUsers[0].expirymonths.lens);
                    jobj1.Add("miscitem", loginUsers[0].expirymonths.miscitem);
                    jContent.Add("expirymonths", jobj1);

                    JArray jcounters = new JArray();
                    JObject jCounterObject;
                    foreach (usercounter loginuser in loginUsers[0].usercounters)
                    {
                        jCounterObject = new JObject();
                        jCounterObject.Add("storeid", loginuser.storeid); 
                        jCounterObject.Add("countertype", loginuser.countertype);
                        jCounterObject.Add("counterid", loginuser.counterid);
                        jCounterObject.Add("countername", loginuser.countername);
                        jcounters.Add(jCounterObject);
                    }
                    jContent.Add("usercounters", jcounters);

                    JArray jstores = new JArray();
                    JObject jStoreObject;
                    foreach (userstore loginuser in loginUsers[0].userstores)
                    {
                        jStoreObject = new JObject();
                        jStoreObject.Add("storetype", loginuser.storetype);
                        jStoreObject.Add("storeid", loginuser.storeid);
                        jStoreObject.Add("storename", loginuser.storename);
                        jstores.Add(jStoreObject);
                    }
                    jContent.Add("userstores", jstores);          


                    JArray jRoles = new JArray();
                    JObject jRoleObject;
                    foreach (users loginuser in loginUsers)
                    {
                        jRoleObject = new JObject();
                        jRoleObject.Add("roleid", loginuser.roleid);
                        jRoleObject.Add("rolename", loginuser.rolename);
                        //jRoleObject.Add("rolesmatrix", loginuser.rolesmatrix.ToString());
                        jRoleObject.Add("rolesmatrix", loginuser.action.ToString());
                        jRoleObject.Add("defaultpage", loginuser.defaultpage);
                        jRoleObject.Add("defaultuserrole", loginuser.defaultuserrole);
                        jRoleObject.Add("scopejson", loginuser.scopejson.ToString());
                        jRoles.Add(jRoleObject);
                    }
                    jContent.Add("roles", jRoles);

                    jResult.Add("Content", jContent);
                    jResult.Add("Response", jResponse);


                }
                else
                {
                    jResponse.Add("error", "1");
                    jResponse.Add("sys_msg", "Error");
                    jResponse.Add("message", "Login failed. Incorrect UserId/password provided");

                    jContent.Add("orgid", string.Empty);
                    jContent.Add("locationid", string.Empty);
                    jContent.Add("userid", string.Empty);
                    jContent.Add("username", string.Empty);
                    jContent.Add("currencysymbol", string.Empty);
                    jContent.Add("token", string.Empty);

                    JArray jRoles = new JArray();

                    jContent.Add("roles", jRoles);

                    jResult.Add("Content", jContent);
                    jResult.Add("Response", jResponse);
                }

                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(jResult.ToString());
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused!- " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CustomerOrderEntry")]
        public IActionResult CustomerOrderEntry([FromBody] CustomerOrder objData)
        {
            string METHOD_NAME = "CustomerOrderEntry";
            string filepath = string.Empty;


            try
            {
                int inputdataSave = Convert.ToInt16(Configuration["KeySettings:InputFileSave"]);
                string inputfilepath = Convert.ToString(Configuration["KeySettings:InputFilePath"]);

                if (inputdataSave == 1)
                {
                    filepath = inputfilepath + "OrderEntry_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".json";
                    string inputjson = JsonConvert.SerializeObject(objData);
                    JObject json = JObject.Parse(inputjson);
                    System.IO.File.WriteAllText(@filepath, json.ToString());
                }

                bool isNewOrder = false;

                if (objData.customerorderid == "0" || objData.customerorderid == "")
                    isNewOrder = true;

                objPortal = new OpticalPortal();
                var status = objPortal.CustomerOrderEntry(objData);


                if (isNewOrder && objData.isdirectsale == "0" && objData.patientInfo.uin != String.Empty)
                {
                    var options = new JsonDocumentOptions
                    {
                        AllowTrailingCommas = true,
                        CommentHandling = JsonCommentHandling.Skip
                    };

                    var client = new RestClient(string.Format(@emrPostOrderInfoAPI, objData.patientInfo.uin, objData.displayorderno.Replace('/', '-'), objData.header.countername != "" ? objData.header.countername : "main"));


                    Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "response content - " +
                        (string.Format(@emrPostOrderInfoAPI, objData.patientInfo.uin, objData.displayorderno.Replace('/', '-'),
                        objData.header.countername != "" ? objData.header.countername : "main")));

                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (response.Content != String.Empty)
                            Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "response content - " + response.Content);

                    }
                    else
                        Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Status Code - " + response.StatusCode + " ;Status Description - " + response.StatusDescription);
                }


                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("CustomerOrderEntry/{orgid}/{locationid}/{counterid}/{orderno}/{finYearId}")]
        public IActionResult CustomerOrderEntry([FromRoute] string orgid, string locationid, string counterid, string orderno, string finYearId)
        {
            string METHOD_NAME = "Get - CustomerOrderEntry";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.GetCustomerOrder(orgid, locationid, counterid, orderno, finYearId);


                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }
        /*

        [AllowAnonymous]
        [HttpPost]
        [Route("PurchaseTransactions")]
        public IActionResult PurchaseTransactions([FromBody] PurchaseTrans objData)
        {
            string METHOD_NAME = "PurchaseTransactions";
            string filepath = string.Empty;


            try
            {
                int inputdataSave = Convert.ToInt16(Configuration["KeySettings:InputFileSave"]);
                string inputfilepath = Convert.ToString(Configuration["KeySettings:InputFilePath"]);

                if (inputdataSave == 1)
                {
                    filepath = inputfilepath + "PurchaseTrans_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".json";
                    string inputjson = JsonConvert.SerializeObject(objData);
                    JObject json = JObject.Parse(inputjson);
                    System.IO.File.WriteAllText(@filepath, json.ToString());
                }

                objPortal = new OpticalPortal();
                var status = objPortal.PurchaseTransactions(objData);


                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }
        */

        [AllowAnonymous]
        [HttpPost]
        [Route("GetDashboardDetails")]
        public IActionResult GetDashboardDetails([FromBody] DashboardInput objInput)
        {
            string METHOD_NAME = "GetDashboardDetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.GetDashboardDetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetAllProductDetails")]
        public IActionResult GetAllProductDetails([FromBody] ProductMasterInput objInput)
        {
            string METHOD_NAME = "GetAllProductDetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.GetProductDetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetAllSupplierDetails")]
        public IActionResult GetAllSupplierDetails([FromBody] SupplierMasterInput objInput)
        {
            string METHOD_NAME = "GetAllSupplierDetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.GetSupplierDetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        //praveen changes start - 04-02-2022
        [AllowAnonymous]
        [HttpPost]
        [Route("ValidateRefractionDetails")]
        public IActionResult ValidateRefractionDetails([FromBody] RefractionDetailsInput objInput)
        {
            string METHOD_NAME = "ValidateRefractionDetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.ValidateRefractionDetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPut]
        [Route("crudsupplier")]
        public IActionResult crudsupplier([FromBody] crudSupplierDetailsInput objInput)
        {
            string METHOD_NAME = "crudsupplier";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.crudsupplier(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("CRUDSupplierOrderReturnNotes")]
        public IActionResult CRUDSupplierOrderReturnNotes([FromBody] SupplierTransReturnRemarks objInput)
        {
            string METHOD_NAME = "CRUDSupplierOrderReturnNotes";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.CRUDSupplierOrderReturnNotes(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetSupplierOrderReturnNotes/{supplierorderid}/{type}")]
        public IActionResult GetSupplierOrderReturnNotes([FromRoute] string supplierorderid, string type)
        {
            string METHOD_NAME = "GetSupplierOrderReturnNotes";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.GetSupplierOrderReturnNotes(supplierorderid, type.ToUpper());
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPut]
        [Route("crudproduct")]
        public IActionResult crudproduct([FromBody] crudProductDetailsInput objInput)
        {
            string METHOD_NAME = "crudproduct";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.crudproduct(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("GetLookupMasterDetails")]
        public IActionResult GetLookupMasterDetails([FromBody] LookupMasterDetails objInput)
        {
            string METHOD_NAME = "GetLookupMasterDetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.GetLookupMasterDetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("crudLookup")]
        public IActionResult crudLookup([FromBody] crudLookupDetailsInput objInput)
        {
            string METHOD_NAME = "crudLookup";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.crudLookup(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("GetUserDetails/{orgid}/{locationid}/{roleId}")]
        public IActionResult UserDetails([FromRoute] string orgid, string locationid, string roleId)
        {
            string METHOD_NAME = "GetUserDetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.GetUserDetails(orgid, locationid, roleId);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("crudUserDetails")]
        public IActionResult crudUserDetails([FromBody] crudUserDetailsInput objInput)
        {
            string METHOD_NAME = "crudUserDetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.crudUserDetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }


        [AllowAnonymous] //to do
        [HttpGet]
        [Route("getorderpaymentdetails/{orderid}")]
        public IActionResult getorderpaymentdetails(string orderid)
        {
            string METHOD_NAME = "getorderpaymentdetails";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.getorderpaymentdetails(orderid);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetSupplierOrderWorklist")]
        public IActionResult GetSupplierOrderWorklist([FromBody] SupplierOrderWorklistInput objInput)
        {
            string METHOD_NAME = "GetSupplierOrderWorklist";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.GetSupplierOrderWorklist(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("GetStoreOrderWorklist")]
        public IActionResult GetStoreOrderWorklist([FromBody] StoreOrderWorklistInput objInput)
        {
            string METHOD_NAME = "GetStoreOrderWorklist";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.GetStoreOrderWorklist(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("SupplierTransactions")]
        public IActionResult SupplierTransactions([FromBody] SupplierTransaction objData)
        {
            string METHOD_NAME = "SupplierTransactions";
            string filepath = string.Empty;


            try
            {
                int inputdataSave = Convert.ToInt16(Configuration["KeySettings:InputFileSave"]);
                string inputfilepath = Convert.ToString(Configuration["KeySettings:InputFilePath"]);

                if (inputdataSave == 1)
                {
                    filepath = inputfilepath + "SupplierTrans_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".json";
                    string inputjson = JsonConvert.SerializeObject(objData);
                    JObject json = JObject.Parse(inputjson);
                    System.IO.File.WriteAllText(@filepath, json.ToString());
                }

                objPortal = new OpticalPortal();
                var status = objPortal.SupplierTransactions(objData);


                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("StoreTransactions")]
        public IActionResult StoreTransactions([FromBody] StoreTransaction objData)
        {
            string METHOD_NAME = "StoreTransactions";
            string filepath = string.Empty;


            try
            {
                int inputdataSave = Convert.ToInt16(Configuration["KeySettings:InputFileSave"]);
                string inputfilepath = Convert.ToString(Configuration["KeySettings:InputFilePath"]);

                if (inputdataSave == 1)
                {
                    filepath = inputfilepath + "StoreTrans_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".json";
                    string inputjson = JsonConvert.SerializeObject(objData);
                    JObject json = JObject.Parse(inputjson);
                    System.IO.File.WriteAllText(@filepath, json.ToString());
                }

                objPortal = new OpticalPortal();
                var status = objPortal.StoreTransactions(objData);


                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous] //to do
        [HttpGet]
        [Route("GetSupplierTransactionDetails/{transtype}/{orderid}")]
        public IActionResult GetSupplierTransactionDetails(string transtype, string orderid)
        {
            string METHOD_NAME = "GetSupplierTransactionDetails";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.GetSupplierTransactionDetails(transtype, orderid);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }


        [AllowAnonymous] //to do
        [HttpGet]
        [Route("GetStoreTransactionDetails/{transtype}/{orderid}")]
        public IActionResult GetStoreTransactionDetails(string transtype, string orderid)
        {
            string METHOD_NAME = "GetStoreTransactionDetails";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.GetStoreTransactionDetails(transtype, orderid);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }



        [AllowAnonymous] //to do
        [HttpGet]
        [Route("GetUserManagementDetails/{orgid}/{locationid}/{type}")]
        public IActionResult GetUserManagementDetails(string orgid, string locationid, string type)
        {
            string METHOD_NAME = "GetUserManagementDetails";

            try
            {
                objPortal = new OpticalPortal();
                type = type.ToUpper();
                var status = objPortal.GetUserManagementDetails(orgid, locationid, type);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }

        [AllowAnonymous] //to do
        [HttpPost]
        [Route("SearchProductDetails")]
        public IActionResult SearchProductDetails([FromBody] ProductMasterInput productInput)
        {
            string METHOD_NAME = "SearchProductDetails";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.SearchProductDetails(productInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }


        [AllowAnonymous] //to do
        [HttpPost]
        [Route("GetSupplierTransactionHistory")]
        public IActionResult GetSupplierTransactionHistory([FromBody] SupplierTransactionHistoryInput productInput)
        {
            string METHOD_NAME = "GetSupplierTransactionHistory";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.GetSupplierTransactionHistory(productInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }

        [AllowAnonymous] //to do
        [HttpGet]
        [Route("GetAllCounters/{orgid}/{locationid}/{storeid}")]
        public IActionResult GetAllCounters(string orgid, string locationid, string storeid)
        {
            string METHOD_NAME = "GetAllCounters";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.GetAllCounters(orgid, locationid, storeid);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }

        [AllowAnonymous] //to do
        [HttpGet]
        [Route("GetAllStores/{orgid}/{locationid}/{counterid}")]
        public IActionResult GetAllStores(string orgid, string locationid, string counterid)
        {
            string METHOD_NAME = "GetAllStores";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.GetAllStores(orgid, locationid, counterid);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }

        [AllowAnonymous] //to do
        [HttpGet]
        [Route("GetAllTransferStores/{orgid}/{locationid}/{storeid}")]
        public IActionResult GetAllTransferStores(string orgid, string locationid, string storeid)
        {
            string METHOD_NAME = "GetAllTransferStores";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.GetAllTransferStores(orgid, locationid, storeid);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("StockDeductions")]
        public IActionResult StockDeductions([FromBody] StockDeduction objData)
        {
            string METHOD_NAME = "StockDeduction";
            string filepath = string.Empty;


            try
            {
                int inputdataSave = Convert.ToInt16(Configuration["KeySettings:InputFileSave"]);
                string inputfilepath = Convert.ToString(Configuration["KeySettings:InputFilePath"]);

                if (inputdataSave == 1)
                {
                    filepath = inputfilepath + "StockDeduction_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".json";
                    string inputjson = JsonConvert.SerializeObject(objData);
                    JObject json = JObject.Parse(inputjson);
                    System.IO.File.WriteAllText(@filepath, json.ToString());
                }

                objPortal = new OpticalPortal();
                var status = objPortal.StockDeductions(objData);


                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }
        [AllowAnonymous] //to do
        [HttpGet]
        [Route("GetTransactionHistory/{uin}")]
        public IActionResult GetTransactionHistory(string uin)
        {
            string METHOD_NAME = "GetTransactionHistory";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.GetTransactionHistory(uin);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("InsCustomerVisit")]
        public IActionResult InsCustomerVisit([FromBody] InsCustomerVisit objInput)
        {
            string METHOD_NAME = "InsCustomerVisit";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.InsCustomerVisit(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("GetCustomerOrderStatusDetails/{roleid}/{orderid}")]
        public IActionResult GetCustomerOrderStatusDetails([FromRoute] string roleid, string orderid)
        {
            string METHOD_NAME = "Get - GetCustomerOrderStatusDetails";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.GetCustomerOrderStatusDetails(orderid, roleid);


                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("CRUDCustomerOrderStatusTracking")]
        public IActionResult CRUDCustomerOrderStatusTracking([FromBody] CustomerOrderStatusTracking objInput)
        {
            string METHOD_NAME = "CRUDCustomerOrderStatusTracking";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.CRUD_CustomerOrderStatusTracking(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("GetLensProductDetails")]
        public IActionResult GetLensProductDetails([FromBody] LensMatrixInput objInput)
        {
            string METHOD_NAME = "GetLensMatrixBarcode";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.GetLensProductDetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ValidateLensBarcode")]
        public IActionResult ValidateLensBarcode([FromBody] LensMatrixInput objInput)
        {
            string METHOD_NAME = "ValidateLensBarcode";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.ValidateLensBarcode(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetConsumptionBasedPODetails")]
        public IActionResult GetConsumptionBasedPODetails([FromBody] ConsumptionBasedPOInput objInput)
        {
            string METHOD_NAME = "GetConsumptionBasedPODetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.GetConsumptionBasedPODetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("crudRoleDetails")]
        public IActionResult crudRoleDetails([FromBody] crudRoleDetailsInput objInput)
        {
            string METHOD_NAME = "crudUserDetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.crudRoleDetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("crudUserRoleDetails")]
        public IActionResult crudUserRoleDetails([FromBody] crudUserRoleDetailsInput objInput)
        {
            string METHOD_NAME = "crudUserDetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.crudUserRoleDetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetUserRoleMenuAccessRights")]
        public IActionResult GetUserRoleMenuAccessRights([FromBody] crudUserRoleDetailsInput objInput)
        {
            string METHOD_NAME = "GetUserRoleMenuAccessRights";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.GetUserRoleMenuAccessRights(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("CRUD_UserRoleMenuAccessRights")]
        public IActionResult CRUD_UserRoleMenuAccessRights([FromBody] crudUserRoleDetailsInput objInput)
        {
            string METHOD_NAME = "CRUD_UserRoleMenuAccessRights";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.CRUD_UserRoleMenuAccessRights(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetMenuItems/{roleid}/{userid}")]
        public IActionResult GetMenuItems([FromRoute] string roleid, string userid)
        {
            string METHOD_NAME = "GetMenuItems";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.GetMenuItems(roleid, userid);


                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("BuildMenuItems/{roleid}/{userid}")]
        public IActionResult BuildMenuItems([FromRoute] string roleid, string userid)
        {
            string METHOD_NAME = "BuildMenuItems";

            try
            {
                objPortal = new OpticalPortal();
                var status = objPortal.BuildMenuItems(roleid, userid);


                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("crudUserStoreDetails")]
        public IActionResult crudUserStoreDetails([FromBody] crudUserStoreDetailsInput objInput)
        {
            string METHOD_NAME = "crudUserStoreDetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.crudUserStoreDetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPut]
        [Route("crudUserCounterDetails")]
        public IActionResult crudUserCounterDetails([FromBody] crudUserCounterDetailsInput objInput)
        {
            string METHOD_NAME = "crudUserCounterDetails";
            try
            {
                objPortal = new OpticalPortal();
                var statusfinal = objPortal.crudUserCounterDetails(objInput);
                Log4Net.LogEvent(LogLevel.Information, CLASS_NAME, METHOD_NAME, "Successfully completed the Request");
                return Ok(statusfinal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to process the details. Sorry for the inconvenience caused! - Exception - " + ex.Message });
            }
        }



    }
}