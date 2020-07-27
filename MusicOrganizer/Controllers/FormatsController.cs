using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using MusicOrganizer.Models;

namespace MusicOrganizer.Controllers
{
  public class FormatsController : Controller
  {
    [HttpGet("/formats")]
    public ActionResult Index()
    {
      List<Format> allformats = Format.GetAll();
      return View(allformats);
    }
    
    [HttpGet("/formats/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/formats")]
    public ActionResult Create(string formatType)
    {
      Format newFormat = new Format(formatType);
      return RedirectToAction("Index");
    }
    
    [HttpGet("/formats/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Format selectedFormat = Format.Find(id);
      List<Record> formatRecords = selectedFormat.Records;
      model.Add("format", selectedFormat);
      model.Add("records", formatRecords);
      return View(model);
    }
    [HttpPost("/formats/{formatId}/records")]
    public ActionResult Create(int formatId, string recordTitle, string recordArtist, string recordGenre)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Format foundFormat = Format.Find(formatId);
      Record newRecord = new Record(recordTitle, recordArtist, recordGenre);
      newRecord.Save();
      foundFormat.AddRecord(newRecord);
      List<Record> formatRecords = foundFormat.Records;
      model.Add("records", formatRecords);
      model.Add("format", foundFormat);
      return View("Show", model);
    }
  }
}