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

    [TestMethod]
    public void Equals_ReturnsTrueIfTitlesAreTheSame_Record()
    {
      Record firstRecord = new Record("test");
      Record secondRecord = new Record("test");

      Assert.AreEqual(firstRecord, secondRecord);
    }

    [TestMethod]
    public void Save_SavesToDatabase_RecordList()
    {
      Record testRecord = new Record("test title");
      testRecord.Save();
      List<Record> result = Record.GetAll();
      List<Record> testList = new List<Record> {testRecord};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsRecords_RecordsList()
    {
      string title1 = "test title 1";
      string title2 = "test title 2";
      Record newRecord1 = new Record(title1);
      Record newRecord2 = new Record(title2);
      newRecord1.Save();
      newRecord2.Save();
      List<Record> newList = new List<Record> { newRecord1, newRecord2 };

      List<Record> result = Record.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }
  }
}