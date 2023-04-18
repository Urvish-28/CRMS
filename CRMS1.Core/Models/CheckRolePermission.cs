using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.Models
{
    public static class CheckRolePermission
    {
        public static bool View { get; set; }
        public static bool Insert { get; set; }
        public static bool Update { get; set; }
        public static bool Delete { get; set; }
        public enum FormAccessCode
        {
            IsView = 1,
            IsInsert = 2,
            IsUpdate = 3,
            IsDelete = 4
        }
    }
}
