using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.TestData
{
    public static class DataManager
    {
        public static void Initialize()
        {
            CustomersData.Initialize();
            MoviesData.Initialize();

            MoviesData.Movies[0].CustomerIds.Add(1);
            MoviesData.Movies[0].CustomerIds.Add(2);
            MoviesData.Movies[2].CustomerIds.Add(2);
            MoviesData.Movies[3].CustomerIds.Add(3);
        }
    }
}