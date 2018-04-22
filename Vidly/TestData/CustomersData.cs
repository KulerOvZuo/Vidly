using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.TestData
{
    public static class CustomersData
    {
        public static IList<Customer> Customers;

        public static void Initialize()
        {
            Customers = new List<Customer>
            {
                new Customer {Id = 1, Name = "John Smith" },
                new Customer {Id = 2, Name = "Mary Williams"},
                new Customer {Id = 3, Name = "Eduardo Abbrams"}
            };
        }
    }
}