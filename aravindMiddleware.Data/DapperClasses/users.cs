using System;
using System.Collections.Generic;
using System.Text;

namespace aravindMiddleware.Data.DapperClasses
{   
    public class users
    {
        public string orgid { get; set; }
        public string zoneid { get; set; }
        public string locationid { get; set; }
        public string currencySymbol { get; set; }
        public string rolesmatrix { get; set; }
        public string defaultuserrole { get; set; }
        public string defaultpage { get; set; }
        public string empcode { get; set; }
        public string userId { get; set; }
        public string username { get; set; }        
        public string roleid { get; set; }
        public string rolename { get; set; }
        public string action { get; set; }
        public string scopejson { get; set; }
        public expirymonths expirymonths { get; set; }

        public List<usercounter> usercounters = new List<usercounter>();

        public List<userstore> userstores = new List<userstore>();

    }   
    public class expirymonths
    {
        public string frame { get; set; }
        public string lens { get; set; }
        public string miscitem { get; set; }
    }

    public class usercounter
    {
        public string storeid { get; set; }
        public string countertype { get; set; }
        public string counterid { get; set; }
        public string countername { get; set; }
    }

    public class userstore
    {
        public string storetype { get; set; }
        public string storeid { get; set; }
        public string storename { get; set; }
    }


}
