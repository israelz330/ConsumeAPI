using System;
using System.Net.Http;

namespace ConsumeAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello API!\n");
            Console.WriteLine("Retriving data from the WebAPI [api/Employees/GetEmployeesData]...\n");


            using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44364/");
                    //HTTP GET
                    var responseTask = client.GetAsync("api/Employees/GetEmployeesData");

                    try
                    {
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {

                            var readTask = result.Content.ReadAsAsync<Employee[]>();
                            readTask.Wait();

                            var employees = readTask.Result;

                            foreach (var employee in employees)
                            {
                                Console.WriteLine($"Name: {employee.Name}");
                            }

                            Console.WriteLine("\nEnd of list");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error al intentar acceder a la API: " + e.Message);
                    }

                }

             Console.ReadLine();
        }
    }
}

