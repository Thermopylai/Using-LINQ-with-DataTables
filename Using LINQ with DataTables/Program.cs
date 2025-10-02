using System.Data;

namespace Using_LINQ_with_DataTables
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var employees = new Employees(); // Create an instance of the Employees DataTable
            employees.Rows.Add(1, "Alice", 30, "HR", 60000);    
            employees.Rows.Add(2, "Bob", 25, "IT", 70000);
            employees.Rows.Add(3, "Charlie", 28, "IT", 75000);
            employees.Rows.Add(4, "David", 35, "Finance", 80000);
            employees.Rows.Add(5, "Eve", 32, "HR", 62000);
            employees.Rows.Add(6, "Frank", 29, "IT", 72000);
            employees.Rows.Add(7, "Grace", 31, "Finance", 81000);   
            employees.Rows.Add(8, "Hannah", 27, "IT", 68000);
            employees.Rows.Add(9, "Ivy", 33, "HR", 63000);
            employees.Rows.Add(10, "Jack", 26, "IT", 71000);

            Console.WriteLine("Employees in IT Department ordered by Salary (Descending):");

            // Query to select employees from the IT department
            // and order them by salary in descending order.
            // Use AsEnumerable() to work with LINQ to DataSet.
            // AsEnumerable creates an enumerable collection of DataRow
            // which allows LINQ operations.
            // Use Field<T> to access the column values in a type-safe manner.
            // This avoids the need for casting and potential runtime errors.

            var query = from emp in employees.AsEnumerable() // Convert DataTable to Enumerable of DataRow
                        where emp.Field<string>("Department") == "IT" // Filter employees where Department is "IT".
                        orderby emp.Field<decimal>("Salary") descending // Order by Salary in descending order.
                        select new // Select a new anonymous object with relevant fields.

                        // Create an anonymous type to hold the selected fields.
                        // This helps in projecting only the necessary data
                        // and makes it easier to work with the results.
                        // Each property corresponds to a column in the DataTable.
                        // Use object initializer syntax to set the properties.
                        // This creates a new object for each row in the result set.
                        // The properties are strongly typed based on the column data types.
                        // This improves code readability and maintainability.
                        // The selected fields are Id, Name, Age, Department, and Salary.
                        {
                            // Use Field<T> to get the value of the specified column.    
                            Id = emp.Field<int>("Id"),
                            Name = emp.Field<string>("Name"),
                            Age = emp.Field<int>("Age"),
                            Department = emp.Field<string>("Department"),
                            Salary = emp.Field<decimal>("Salary")
                        };

            // Display the results
            foreach (var emp in query)
            {
                Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Age: {emp.Age}, Department: {emp.Department}, Salary: {emp.Salary}");
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    // Define a strongly-typed DataTable for Employees
    // This is optional but helps with clarity and type safety
    // You can also use a regular DataTable and define columns dynamically
    // Here, we define a strongly-typed DataTable for Employees 
    // with columns Id, Name, Age, Department, and Salary
    // This class inherits from DataTable and sets up the schema in the constructor
    public class Employees : DataTable // Inherit from DataTable
    {
        public Employees()
        {
            this.Columns.Add("Id", typeof(int)); // Primary key column
            this.Columns.Add("Name", typeof(string)); // Employee name column
            this.Columns.Add("Age", typeof(int)); // Employee age column    
            this.Columns.Add("Department", typeof(string)); // Employee department column   
            this.Columns.Add("Salary", typeof(decimal)); // Employee salary column
        } 
    }
}
