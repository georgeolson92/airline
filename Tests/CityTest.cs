using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Airline.Objects
{
  public class CityTest : IDisposable
  {
    public CityTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = City.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfCitiesAreTheSame()
    {
      City firstCity = new City("Portland");
      City secondCity = new City("Portland");
      Assert.Equal(firstCity, secondCity);
    }


    [Fact]
    public void Test_Save_SavesCityToDatabase()
    {
      City testCity = new City("Portland");
      testCity.Save();

      List<City> testCities = new List<City>{testCity};
      List<City> resultCities = City.GetAll();

      Assert.Equal(testCities, resultCities);
    }

    [Fact]
    public void Test_Save_AssignsIdToCity()
    {
      //Arrange
      City testCity = new City("Seattle");

      //Act
      testCity.Save();
      City savedCity = City.GetAll()[0];

      int result = savedCity.GetId();
      int testId = testCity.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsCityInDatabase()
    {
      City testCity = new City("Salem");
      testCity.Save();
      City foundCity = City.Find(testCity.GetId());
      Assert.Equal(testCity, foundCity);
    }

    [Fact]
    public void Test_Update_UpdatesCityInDatabase()
    {
      City testCity = new City("Branson");
      testCity.Save();
      string newName = "Nashville";
      testCity.Update(newName);
      Assert.Equal(newName, testCity.GetName());
    }



    public void Dispose()
    {
      City.DeleteAll();
    }

  }
}
