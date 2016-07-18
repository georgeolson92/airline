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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO flights (name, departure, arrival) OUTPUT INSERTED.id VALUES (@FlightName, @FlightDeparture, @FlightArrival);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@FlightName";
      nameParameter.Value = this.GetName();

      SqlParameter departureParameter = new SqlParameter();
      departureParameter.ParameterName = "@FlightDeparture";
      departureParameter.Value = this.GetDeparture();

      SqlParameter arrivalParameter = new SqlParameter();
      arrivalParameter.ParameterName = "@FlightArrival";
      arrivalParameter.Value = this.GetArrival();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(departureParameter);
      cmd.Parameters.Add(arrivalParameter);

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

      while(rdr.Read())
      {
        foundFlightId = rdr.GetInt32(0);
        foundFlightName = rdr.GetString(1);
        foundDeparture = rdr.GetDateTime(2);
        foundArrival = rdr.GetDateTime(3);
      }
      Flight foundFlight = new Flight(foundFlightName, foundDeparture, foundArrival, foundFlightId);

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

    public void Update(string newName, DateTime newDeparture, DateTime newArrival)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE flights SET name = @NewName, departure = @NewDeparture, arrival = @NewArrival OUTPUT INSERTED.name, INSERTED.departure, INSERTED.arrival WHERE id = @FlightId;", conn);

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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM flights;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
