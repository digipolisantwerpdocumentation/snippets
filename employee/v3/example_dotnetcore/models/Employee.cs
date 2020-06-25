namespace EmployeeExample.models
{
    public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string NickName { get; set; }
        public string AvatarUrl { get; set; }
        public int CrsNumber { get; set; }
        public Source Source { get; set; }
        public Identity Identity { get; set; }
        public Adaccount AdAccount { get; set; }
        public Organisation Organisation { get; set; }
    }

    public class Source
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public string ReferenceKey { get; set; }
    }

    public class Identity
    {
        public string NationalNumber { get; set; }
        public string PersonCrsNumber { get; set; }
    }

    public class Adaccount
    {
        public string Domain { get; set; }
        public string Account { get; set; }
    }

    public class Organisation
    {
        public int OrganisationCrsNumber { get; set; }
        public string Description { get; set; }
    }

}
