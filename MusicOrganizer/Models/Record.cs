using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MusicOrganizer.Models
{
  public class Record
  {
    public string Title { get; set; }
    public int Id { get; }

    public Record (string title)
    {
      Title = title;
    }
public Record(string title, int id)
{
  Title = title;
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
        Record newRecord = new Record(recordTitle, recordId);
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
        bool titleEquality = (this.Title == newRecord.Title);
        return titleEquality;
      }
    }

    public static Record Find(int searchId)
    {
      Record placeHolder = new Record("placeHolder");
      return placeHolder;
    }
  }
}