﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return this.View();
    }

    public IActionResult Error()
    {
      return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
  }
}
