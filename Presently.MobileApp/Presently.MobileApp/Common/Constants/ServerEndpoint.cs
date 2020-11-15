namespace Presently.MobileApp.Common.Constants
{
    public static class ServerEndpoint
    {
        public const string AuthLogin = "api/auth/login";
        public const string AuthGetProfile = "api/auth/me";
        public const string AttendanceLogGetAll = "api/attendance-log/get-all";
        public const string AttendanceLogGet = "api/attendance-log?recordId={0}";
        public const string AttendanceLogCreate = "api/attendance-log/create";
        public const string EmployeeSites = "api/employee/sites";
    }
}
