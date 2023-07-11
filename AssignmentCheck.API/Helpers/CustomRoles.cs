namespace AssignmentCheck.Api.Helpers
{
    public class CustomRoles
    {
        private const string ADMIN = "Admin";
        private const string TEACHER = "Teacher";
        private const string STUDENT = "Student";
        private const string USER = "User";

        public const string USER_ROLE = USER + "," + ADMIN_ROLE + "," + TEACHER_ROLE + "," + STUDENT_ROLE;
        public const string STUDENT_ROLE = STUDENT + "," + ADMIN_ROLE;
        public const string TEACHER_ROLE = TEACHER + "," + ADMIN_ROLE;
        public const string ADMIN_ROLE = ADMIN;
    }
}
