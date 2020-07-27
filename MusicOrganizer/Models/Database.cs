using MusicOrganizer;
using System;
using MySql.Data.MySqlClient;

namespace MusicOrganizer.Models
{
  public class DB
  {
    public static MySqlConnection Connection()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
  }
}