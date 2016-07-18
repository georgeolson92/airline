using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Airline.Objects
{
  public class FlightTest
  {
    public FlightTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Flight.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfFlightsAreTheSame()
    {
      //Arrange, Act
      Flight firstFlight = new Flight("Delta12", new DateTime(2015, 1, 18), new DateTime(2015, 1, 19));
      Flight secondFlight = new Flight("Delta12", new DateTime(2015, 1, 18), new DateTime(2015, 1, 19));

      //Assert
      Assert.Equal(firstFlight, secondFlight);
    }
  }
}
