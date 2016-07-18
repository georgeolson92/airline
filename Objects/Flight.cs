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

    public Flight (string name, DateTime departure, DateTime arrival, int id =0)
    {
      _id = id;
      _name = name;
      _departure = departure;
      _arrival = arrival;
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
        return (idEquality && nameEquality && departureEquality && arrivalEquality);
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
        Flight newFlight = new Flight(flightName, flightDeparture, flightArrival, flightId);
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
  }
}
