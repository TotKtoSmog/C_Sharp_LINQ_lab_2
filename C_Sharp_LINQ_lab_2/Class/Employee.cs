namespace C_Sharp_LINQ_lab_2.Class
{
    internal class Employee
    {
        internal int Id_Employee;
        internal string Fname;
        internal string Lname;
        internal double Salary;
        internal int Id_department;
        internal Employee(int id, string fname, string lname, double salary, int id_department)
        {
            Id_Employee = id;
            Fname = fname;
            Lname = lname;
            Salary = salary;
            Id_department = id_department;
        }
    }
}