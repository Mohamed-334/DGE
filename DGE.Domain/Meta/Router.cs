namespace DGE.Domain.Meta
{
    public static class Router
    {
        //public const string Domain = "https://localhost:5001";
        public const string Root = "/api/v{version:apiVersion}/";
        public static class AuthenticationRouting
        {
            public const string Prefix = Root + "Authentication/";
            public const string GoogleAuthenticationRequest = Prefix + "GoogleAuthenticationRequest";
            public const string LinkedInAuthenticationRequest = Prefix + "LinkedInAuthenticationRequest";
            public const string ExternalAuthentication = Prefix + "ExternalAuthentication";
            public const string SignUp = Prefix + "SignUp";
            public const string SignIn = Prefix + "SignIn";
            public const string ResendOtp = Prefix + "ResendOtp";
            public const string VerifyRegistrationOtp = Prefix + "VerifyRegistrationOtp";
            public const string VerifyResetPasswordOtp = Prefix + "VerifyResetPasswordOtp";
            public const string ChangePassword = Prefix + "ChangePassword";
            public const string ResetPasswordRequest = Prefix + "ResetPasswordRequest";
            public const string ResetPassword = Prefix + "ResetPassword";
            public const string Logout = Prefix + "Logout";
            public const string CsrfToken = Prefix + "CsrfToken";
        }
        public static class UserRouting
        {
            public const string Prefix = Root + "User/";
            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetUserRoles = Prefix + "GetUserRoles/{id}";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class RoleRouting
        {
            public const string Prefix = Root + "Role/";
            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string Delete = Prefix + "Delete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class EmailRouting
        {
            public const string Prefix = Root + "Email/";
            public const string SendEmail = Prefix + "SendEmail";
        }
        public static class NotificationRouting
        {
            public const string Prefix = Root + "Notification/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Send = Prefix + "Send";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class PermissionRouting
        {
            public const string Prefix = Root + "Permission/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class RolePermissionRouting
        {
            public const string Prefix = Root + "RolePermission/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class UserPermissionRouting
        {
            public const string Prefix = Root + "UserPermission/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class ProductRouting
        {
            public const string Prefix = Root + "Product/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class ProductCategoryRouting
        {
            public const string Prefix = Root + "ProductCategory/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class ProductImageRouting
        {
            public const string Prefix = Root + "ProductImage/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class ProductMineralRouting
        {
            public const string Prefix = Root + "ProductMineral/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class ProductTypeRouting
        {
            public const string Prefix = Root + "ProductType/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class ProductVarietyRouting
        {
            public const string Prefix = Root + "ProductVariety/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class ProductVitaminRouting
        {
            public const string Prefix = Root + "ProductVitamin/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class VitaminRouting
        {
            public const string Prefix = Root + "Vitamin/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class MineralRouting
        {
            public const string Prefix = Root + "Mineral/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class TypeRouting
        {
            public const string Prefix = Root + "Type/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class PackingTypeRouting
        {
            public const string Prefix = Root + "PackingType/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class ProductPackingRouting
        {
            public const string Prefix = Root + "ProductPacking/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class ProductHarvestMonthRouting
        {
            public const string Prefix = Root + "ProductHarvestMonth/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class CertificationRouting
        {
            public const string Prefix = Root + "Certification/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class ExhibitionRouting
        {
            public const string Prefix = Root + "Exhibition/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
        public static class ContactUsRouting
        {
            public const string Prefix = Root + "ContactUs/";

            public const string GetById = Prefix + "GetById/{id}";
            public const string GetList = Prefix + "GetList";
            public const string GetPaginatedList = Prefix + "GetPaginatedList";
            public const string GetDropDownList = Prefix + "GetDropDownList";
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string HardDelete = Prefix + "HardDelete/{id}";
            public const string SoftDeleteAndActivate = Prefix + "SoftDeleteAndActivate/{id}";
        }
    }
}
