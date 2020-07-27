using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MusicOrganizer.Models;
using System;
using MySql.Data.MySqlClient;

namespace MusicOrganizer.Tests
{
  [TestClass]
  public class RecordTest : IDisposable
  {
    public void Dispose()
    {
      Record.ClearAll();
    }
    public RecordTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=music_organizer_test;";
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_RecordList()
    {
      List<Record> newList = new List<Record> { };
      List<Record> result = Record.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }
  }
}