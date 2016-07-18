using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Airline.Objects
{
  public class Flight
  {
    int _id;
    string _name;
    DateTime _departure;
    DateTime _arrival;
    string _status;

    public Flight (string name, DateTime departure, DateTime arrival, string status = "On Time", int id =0)
    {
      _id = id;
      _name = name;
      _departure = departure;
      _arrival = arrival;
      _status = status;
    }

    public override bool Equals(System.Object otherFlight)
    {
      if (!(otherFlight is Flight))
      {
        return false;
      }
      else
      {
        Flight newFlight = (Flight) otherFlight;
        bool idEquality = this.GetId() == newFlight.GetId();
        bool nameEquality = this.GetName() == newFlight.GetName();
        bool departureEquality = this.GetDeparture() == newFlight.GetDeparture();
        bool arrivalEquality = this.GetArrival() == newFlight.GetArrival();
        bool statusEquality = this.GetStatus() == newFlight.GetStatus();
        return (idEquality && nameEquality && departureEquality && arrivalEquality && statusEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public DateTime GetDeparture()
    {
      return _departure;
    }

    public DateTime GetArrival()
    {
      return _arrival;
    }

    public string GetStatus()
    {
      return _status;
    }

    public void SetName(string name)
    {
      _name = name;
    }

    public void SetDeparture(DateTime departure)
    {
      _departure = departure;
    }

    public void SetArrival(DateTime arrival)
    {
      _arrival = arrival;
    }

    public void SetStatus(string status)
    {
      _status = status;
    }

    public static List<Flight> GetAll()
    {
      List<Flight> AllFlights = new List<Flight>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand ("SELECT * FROM flights;", conn);
      rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int flightId = rdr.GetInt32(0);
        string flightName = rdr.GetString(1);
        DateTime flightDeparture = rdr.GetDateTime(2);
        DateTime flightArrival = rdr.GetDateTime(3);
        string flightStatus = rdr.GetString(4);
        Flight newFlight = new Flight(flightName, flightDeparture, flightArrival, flightStatus, flightId);
        AllFlights.Add(newFlight);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllFlights;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO flights (name, departure, arrival, status) OUTPUT INSERTED.id VALUES (@FlightName, @FlightDeparture, @FlightArrival, @FlightStatus);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@FlightName";
      nameParameter.Value = this.GetName();

      SqlParameter departureParameter = new SqlParameter();
      departureParameter.ParameterName = "@FlightDeparture";
      departureParameter.Value = this.GetDeparture();

      SqlParameter arrivalParameter = new SqlParameter();
      arrivalParameter.ParameterName = "@FlightArrival";
      arrivalParameter.Value = this.GetArrival();

      SqlParameter statusParameter = new SqlParameter();
      statusParameter.ParameterName = "@FlightStatus";
      statusParameter.Value = this.GetStatus();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(departureParameter);
      cmd.Parameters.Add(arrivalParameter);
      cmd.Parameters.Add(statusParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Flight Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand ("SELECT * FROM flights WHERE id = @FlightId;", conn);
      SqlParameter flightIdParameter = new SqlParameter();
      flightIdParameter.ParameterName = "@FlightId";
      flightIdParameter.Value = id.ToString();
      cmd.Parameters.Add(flightIdParameter);
      rdr = cmd.ExecuteReader();

      int foundFlightId = 0;
      string foundFlightName = "";
      DateTime foundDeparture = new DateTime();
      DateTime foundArrival = new DateTime();
      string foundStatus = "";

      while(rdr.Read())
      {
        foundFlightId = rdr.GetInt32(0);
        foundFlightName = rdr.GetString(1);
        foundDeparture = rdr.GetDateTime(2);
        foundArrival = rdr.GetDateTime(3);
        foundStatus = rdr.GetString(4);
      }
      Flight foundFlight = new Flight(foundFlightName, foundDeparture, foundArrival, foundStatus, foundFlightId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundFlight;
    }

    public void Update(string newName, DateTime newDeparture, DateTime newArrival, string newStatus)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE flights SET name = @NewName, departure = @NewDeparture, arrival = @NewArrival, status = @NewStatus OUTPUT INSERTED.name, INSERTED.departure, INSERTED.arrival, INSERTED.status WHERE id = @FlightId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter newDepartureParameter = new SqlParameter();
      newDepartureParameter.ParameterName = "@NewDeparture";
      newDepartureParameter.Value = newDeparture;
      cmd.Parameters.Add(newDepartureParameter);

      SqlParameter newArrivalParameter = new SqlParameter();
      newArrivalParameter.ParameterName = "@NewArrival";
      newArrivalParameter.Value = newArrival;
      cmd.Parameters.Add(newArrivalParameter);

      SqlParameter newStatusParameter = new SqlParameter();
      newStatusParameter.ParameterName = "@NewStatus";
      newStatusParameter.Value = newStatus;
      cmd.Parameters.Add(newStatusParameter);

      SqlParameter flightIdParameter = new SqlParameter();
      flightIdParameter.ParameterName = "@FlightId";
      flightIdParameter.Value = this.GetId();
      cmd.Parameters.Add(flightIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
        this._departure = rdr.GetDateTime(1);
        this._arrival = rdr.GetDateTime(2);
        this._status = rdr.GetString(3);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void AddCities(City newDeparture, City newArrival)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO flights_cities (flight_id, departure_city, arriving_city) VALUES (@FlightId, @DepartureId, @ArrivalId)", conn);

      SqlParameter flightIdParameter = new SqlParameter();
      flightIdParameter.ParameterName = "@FlightId";
      flightIdParameter.Value = this.GetId();
      cmd.Parameters.Add(flightIdParameter);

      SqlParameter departureIdParameter = new SqlParameter();
      departureIdParameter.ParameterName = "@DepartureId";
      departureIdParameter.Value = newDeparture.GetId();
      cmd.Parameters.Add(departureIdParameter);

      SqlParameter arrivalIdParameter = new SqlParameter();
      arrivalIdParameter.ParameterName = "@ArrivalId";
      arrivalIdParameter.Value = newArrival.GetId();
      cmd.Parameters.Add(arrivalIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<City> GetCities()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT departure_city, arriving_city FROM flights_cities WHERE flight_id = @FlightId;", conn);
      SqlParameter flightIdParameter = new SqlParameter();
      flightIdParameter.ParameterName = "@FlightId";
      flightIdParameter.Value = this.GetId();
      cmd.Parameters.Add(flightIdParameter);

      rdr = cmd.ExecuteReader();

      List<int> cityIds = new List<int> {};
      while(rdr.Read())
      {
        int departureId = rdr.GetInt32(0);
        int arrivalId = rdr.GetInt32(1);
        cityIds.Add(departureId);
        cityIds.Add(arrivalId);
      }
      if (rdr != null)
      {
        rdr.Close();
      }

      List<City> cities = new List<City> {};
      foreach (int cityId in cityIds)
      {
        SqlDataReader queryReader = null;
        SqlCommand cityQuery = new SqlCommand("SELECT * FROM cities WHERE id = @CityId;", conn);

        SqlParameter cityIdParameter = new SqlParameter();
        cityIdParameter.ParameterName = "@CityId";
        cityIdParameter.Value = cityId;
        cityQuery.Parameters.Add(cityIdParameter);

        queryReader = cityQuery.ExecuteReader();
        while (queryReader.Read())
        {
          int thisCityId = queryReader.GetInt32(0);
          string cityName = queryReader.GetString(1);
          City foundCity = new City(cityName, thisCityId);
          cities.Add(foundCity);
        }
        if (queryReader != null)
        {
          queryReader.Close();
        }
      }
      return cities;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM flights WHERE id = @FlightId; DELETE FROM flights_cities WHERE flight_id = @FlightId;", conn);

      SqlParameter flightIdParameter = new SqlParameter();
      flightIdParameter.ParameterName = "@FlightId";
      flightIdParameter.Value = this.GetId();

      cmd.Parameters.Add(flightIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM flights;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
