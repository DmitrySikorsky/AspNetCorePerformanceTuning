// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace AspNetCorePerformanceTuning.ViewModels.Shared
{
  public class ArticleViewModel
  {
    public CategoryViewModel Category { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public PhotoViewModel Photo { get; set; }
  }
}