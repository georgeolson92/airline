using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Airline.Objects
{
  public class FlightTest : IDisposable
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

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Flight testFlight = new Flight("Delta12", new DateTime(2015, 1, 18), new DateTime(2015, 1, 19));

      //Act
      testFlight.Save();
      List<Flight> result = Flight.GetAll();
      List<Flight> testFlights = new List<Flight>{testFlight};

      //Assert
      Assert.Equal(testFlights, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Flight testFlight = new Flight("Delta33", new DateTime(2015, 1, 18), new DateTime(2015, 1, 19));

      //Act
      testFlight.Save();
      Flight savedFlight = Flight.GetAll()[0];

      int result = savedFlight.GetId();
      int testId = testFlight.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsFlightInDatabase()
    {
      //Arrange
      Flight testFlight = new Flight("Delta33", new DateTime(2015, 1, 18), new DateTime(2015, 1, 19));
      testFlight.Save();

      //Act
      Flight foundFlight = Flight.Find(testFlight.GetId());

      //Assert
      Assert.Equal(testFlight, foundFlight);
    }
    [Fact]
    public void Test_Update_UpdatesFlightInDatabase()
    {
      //Arrange
      Flight testFlight = new Flight("Delta33", new DateTime(2015, 1, 18), new DateTime(2015, 1, 19));
      testFlight.Save();
      string newFlightName = "Delta66";

      //Act
      testFlight.Update(newFlightName, new DateTime(2015, 1, 18), new DateTime(2015, 1, 19), "On Time");

      //Assert
      Assert.Equal(newFlightName, testFlight.GetName());
    }

    [Fact]
    public void Test_Update_UpdatesFlightStatusInDatabase()
    {
      //Arrange
      Flight testFlight = new Flight("Delta33", new DateTime(2015, 1, 18), new DateTime(2015, 1, 19));
      testFlight.Save();
      string newStatus = "Late";

      //Act
      testFlight.Update("Delta33", new DateTime(2015, 1, 18), new DateTime(2015, 1, 19), newStatus);

      //Assert
      Assert.Equal(newStatus, testFlight.GetStatus());
    }

    [Fact]
    public void Test_AddCities_AddsCitiesToFlight()
    {
      //Arrange
      City departure = new City("Portland");
      City arrival = new City("Dallas");
      departure.Save();
      arrival.Save();

      Flight testFlight = new Flight("Delta44", new DateTime(2015, 1, 18), new DateTime(2015, 1, 19));
      testFlight.Save();

      //Act
      testFlight.AddCities(departure, arrival);
      List<City> result = testFlight.GetCities();
      List<City> testList = new List<City>{departure, arrival};

      //Assert
      Assert.Equal(testList, result);

    }

    public void Dispose()
    {
      Flight.DeleteAll();
      City.DeleteAll();
    }
  }
}
