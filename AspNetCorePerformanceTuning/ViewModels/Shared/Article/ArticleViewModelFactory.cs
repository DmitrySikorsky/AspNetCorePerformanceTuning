// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using AspNetCorePerformanceTuning.Data;
using AspNetCorePerformanceTuning.Models;

namespace AspNetCorePerformanceTuning.ViewModels.Shared
{
  public class ArticleViewModelFactory
  {
    private Storage storage;
    private ICache cache;

    public ArticleViewModelFactory(Storage storage, ICache cache = null)
    {
      this.storage = storage;
      this.cache = cache;
    }

    public ArticleViewModel Create(Article article)
    {
      IEnumerable<Category> categories = this.cache.GetWithDefaultValue<IEnumerable<Category>>(
        "categories", () => this.storage.Categories.ToList()
      );

      Category category = categories.Single(c => c.Id == article.CategoryId);
      // Uncomment to try it without the categories caching
      //Category category = this.storage.Categories.Find(article.CategoryId);
      // Uncomment to try it without internal Entity Framework Core cache
      //Category category = this.storage.Categories.Single(c => c.Id == article.CategoryId);

      Photo photo = article.Photos.FirstOrDefault(ph => ph.IsDefault);
      //Photo photo = this.storage.Photos.FirstOrDefault(ph => ph.ArticleId == article.Id && ph.IsDefault);

      return new ArticleViewModel()
      {
        Category = new CategoryViewModelFactory().Create(category),
        Name = article.Name,
        Price = article.Price,
        Photo = new PhotoViewModelFactory().Create(photo)
      };
    }
  }
}