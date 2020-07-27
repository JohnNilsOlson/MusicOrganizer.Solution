using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MusicOrganizer.Models
{
  public class Record
  {
    public string Title { get; set; }
    public string Artist { get; set; }
    public int Id { get; set; }

    public Record (string title, string artist)
    {
      Title = title;
      Artist = artist;
    }
    public Record(string title, string artist, int id)
    {
      Title = title;
      Artist = artist;
      Id = id;
    }
    public static List<Record> GetAll()
    {
      List<Record> allRecords = new List<Record> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM records;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int recordId = rdr.GetInt32(0);
        string recordTitle = rdr.GetString(1);
        string recordArtist = rdr.GetString(2);
        Record newRecord = new Record(recordTitle, recordArtist, recordId);
        allRecords.Add(newRecord);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRecords;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM records;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherRecord)
    {
      if (!(otherRecord is Record))
      {
        return false;
      }
      else
      {
        Record newRecord = (Record) otherRecord;
        bool idEquality = (this.Id == newRecord.Id);
        bool titleEquality = (this.Title == newRecord.Title);
        return (idEquality && titleEquality);
      }
    }

    public static Record Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `records` WHERE id = @thisID;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int recordId = 0;
      string recordTitle = "";
      string recordArtist = "";
      while (rdr.Read())
      {
        recordId = rdr.GetInt32(0);
        recordTitle = rdr.GetString(1);
      }
      Record foundRecord = new Record(recordTitle, recordArtist, recordId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundRecord;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO records (title, artist) VALUES (@RecordTitle, @RecordArtist);";
      MySqlParameter title = new MySqlParameter();
      title.ParameterName = "@RecordTitle";
      title.Value = this.Title;
      cmd.Parameters.Add(title);
      MySqlParameter artist = new MySqlParameter();
      artist.ParameterName = "@RecordArtist";
      artist.Value = this.Artist;
      cmd.Parameters.Add(artist);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}