using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
using aravindMiddleware.Data;
using aravindMiddleware.Data.DapperClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace aravindMiddleware.Portal
{
    public class OpticalReportPortal
    {
        OpticalReportDataContext dataManagementContext = new OpticalReportDataContext();

        public DataSet MISSupplierreport(MISSupplierReportInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.MISSupplierreport(objInput);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet MISReceiptPrint(MISReceiptInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.MISReceiptPrint(objInput);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet MISStoresreport(MISStoresReportInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.MISStoresreport(objInput);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet MISStockreport(MISStockReportInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.MISStockreport(objInput);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet MISCustomerreport(MISCustomerReportInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.MISCustomerreport(objInput);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet MISTrackingReport(MISTrackingReportInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.MISTrackingReport(objInput);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet MISAccountReport(MISAccountReportInput objInput)
        {
            try
            {
                DataSet result = dataManagementContext.MISAccountReport(objInput);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
