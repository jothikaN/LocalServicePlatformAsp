using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Application.ApplicationConstants
{
    public class ApplicationConstant
    {

    }

    public static class CommonMessage
    {
        public static string RecordCreated = " Created Successfully";
        public static string RecordUpdated = " Updated Successfully";
        public static string RecordDeleted = " Deleted Successfully";
    }

    public static class CustomRole
    {
        public const string MasterAdmin = "MASTERADMIN";
        public const string Tasker = "TASKER";
        public const string Customer = "CUSTOMER";
    }
}