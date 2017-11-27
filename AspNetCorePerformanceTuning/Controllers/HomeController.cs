// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using AspNetCorePerformanceTuning.Data;
using AspNetCorePerformanceTuning.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePerformanceTuning
{
  public class HomeController : Controller
  {
    private Storage storage;
    private ICache cache;

    public HomeController(Storage storage, ICache cache)
    {
      this.storage = storage;
      this.cache = cache;
    }

    [HttpGet]
    // Caching option #1. Uncomment to turn on built-in server-side response caching
    //[ResponseCache(Duration = 3600)]
    public IActionResult Index()
    {
      return this.View(new IndexViewModelFactory(this.storage, this.cache).Create());
    }
  }
}