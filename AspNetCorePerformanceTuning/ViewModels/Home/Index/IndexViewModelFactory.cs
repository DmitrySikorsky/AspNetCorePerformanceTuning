// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using AspNetCorePerformanceTuning.Data;
using AspNetCorePerformanceTuning.Models;
using AspNetCorePerformanceTuning.ViewModels.Shared;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorePerformanceTuning.ViewModels.Home
{
  public class IndexViewModelFactory
  {
    private Storage storage;
    private ICache cache;

    public IndexViewModelFactory(Storage storage, ICache cache)
    {
      this.storage = storage;
      this.cache = cache;
    }

    public IndexViewModel Create()
    {
      IEnumerable<Category> categories = this.cache.GetWithDefaultValue<IEnumerable<Category>>(
        "categories", () => this.storage.Categories.AsNoTracking().ToList()
      );

      return new IndexViewModel()
      {
        Categories = categories.Select(
          c => new CategoryViewModelFactory().Create(c)
        ),
        // Uncomment to try it without the categories caching
        //Categories = this.storage.Categories/*.AsNoTracking()*/.ToList().Select(
        //  c => new CategoryViewModelFactory().Create(c)
        //),
        Articles = this.storage.Articles.Include(a => a.Photos).OrderBy(a => a.Id).Take(20).ToList().Select(
          a => new ArticleViewModelFactory(this.storage, this.cache).Create(a)
        ),
        // Uncomment to try it without including photos
        //Articles = this.storage.Articles.OrderBy(a => a.Id).Take(20).ToList().Select(
        //  a => new ArticleViewModelFactory(this.storage, this.cache).Create(a)
        //)
      };
    }
  }
}