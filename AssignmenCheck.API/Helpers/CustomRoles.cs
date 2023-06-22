namespace AssignmenCheck.API.Extensions
{
    public class CustomRoles
    {
        private const string ADMIN = "Admin";
        private const string TEACHER = "Teacher";
        private const string STUDENT = "Student";

        public const string STUDENT_ROLE = STUDENT + "," + ADMIN_ROLE;
        public const string TEACHER_ROLE = TEACHER + "," + ADMIN_ROLE;
        public const string ADMIN_ROLE = ADMIN;
    }
}
