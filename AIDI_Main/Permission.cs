using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDI_Main
{
    /// <summary>
    /// 权限类
    /// </summary>
    class Permission
    {

        /// <summary>
        /// 当前权限等级
        /// </summary>
        private static PermissionLevel currentPermission = PermissionLevel.NoPermission;
        internal static PermissionLevel CurrentPermission
        {
            get { return Permission.currentPermission; }
            set
            {
                try
                {
                    Permission.currentPermission = value;
                    string loginInfo = string.Empty;
                    switch (value)
                    {
                        case PermissionLevel.NoPermission:
                   
                                loginInfo = "未登录";
                            break;
                        case PermissionLevel.Operator:
                            loginInfo = "操作员";
                            break;
                        case PermissionLevel.Admin:
                            loginInfo = "管理员";
                            break;
                        case PermissionLevel.Developer:
                            loginInfo = "开发人员";
                            break;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }


        /// <summary>
        /// 检查权限等级
        /// </summary>
        /// <param name="permission">能进行此操作的最低权限等级</param>
        /// <returns></returns>
        internal static bool CheckPermission(PermissionLevel permission)
        {            
            if ((int)currentPermission < (int)permission)
            {
               // Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Insufficient permissions, please login to a higher level of permissions and try again" : "权限不足，请登录更高一级权限后重试", Color.Red);
                return false;
            }
            return true;
        }

    }
    internal enum PermissionLevel
    {
        NoPermission,
        Operator,
        Admin,
        Developer,
    }
}
