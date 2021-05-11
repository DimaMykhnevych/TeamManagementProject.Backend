using Microsoft.AspNetCore.Mvc;

namespace TeamManagement.Authorization
{
    public class RequireRolesAttribute : TypeFilterAttribute
    {
        public RequireRolesAttribute(string role) : base(typeof(RequireRolesFilter))
        {
            Arguments = new object[] { role.Split(",") };
        }
    }
}
