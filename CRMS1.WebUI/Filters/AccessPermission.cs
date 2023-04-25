using CRMS1.Core.Models;
using CRMS1.Services;
using CRMS1.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMS1.WebUI.Filters
{
    public class AccessPermission
    {
        public static bool CheckPermission(String formAccessCode,string action)
        {
            bool RoleCode = (bool)HttpContext.Current.Session["RoleCode"];
            if (RoleCode == false)
            {
                List<FormRoleMapping> list = HttpContext.Current.Session["Permission"] as List<FormRoleMapping>;
                if (list != null)
                {
                    using (CRMSEntities db = new CRMSEntities())
                    {
                        Guid FormId = db.FormMst.Where(x => x.FormAccessCode == formAccessCode).Select(x => x.Id).FirstOrDefault();
                        FormRoleMapping form = list.Where(x => x.FormId == FormId).FirstOrDefault();
                        if (form != null)
                        {
                            if (form.AllowView == true)
                            {
                                CheckRolePermission.View = form.AllowView;
                                CheckRolePermission.Insert = form.AllowInsert;
                                CheckRolePermission.Update = form.AllowUpdate;
                                CheckRolePermission.Delete = form.AllowDelete;
                                if (action == CheckRolePermission.FormAccessCode.IsView.ToString())
                                {
                                    return form.AllowView;
                                }
                                else if (action == CheckRolePermission.FormAccessCode.IsInsert.ToString())
                                {
                                    return form.AllowInsert;
                                }
                                else if (action == CheckRolePermission.FormAccessCode.IsUpdate.ToString())
                                {
                                    return form.AllowUpdate;
                                }
                                else if (action == CheckRolePermission.FormAccessCode.IsDelete.ToString())
                                {
                                    return form.AllowDelete;
                                }
                            }
                            else
                            {
                                CheckRolePermission.View = false;
                                CheckRolePermission.Insert = false;
                                CheckRolePermission.Update = false;
                                CheckRolePermission.Delete = false;
                                return false;
                            }
                            return false;
                        }
                        else
                        {
                            CheckRolePermission.View = false;
                            CheckRolePermission.Insert = false;
                            CheckRolePermission.Update = false;
                            CheckRolePermission.Delete = false;
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                CheckRolePermission.View = true;
                CheckRolePermission.Insert = true;
                CheckRolePermission.Update = true;
                CheckRolePermission.Delete = true;
                return true;
            }
        }
    }
}