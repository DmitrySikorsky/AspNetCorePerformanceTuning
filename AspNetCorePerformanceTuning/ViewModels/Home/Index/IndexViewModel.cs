// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using AspNetCorePerformanceTuning.ViewModels.Shared;

namespace AspNetCorePerformanceTuning.ViewModels.Home
{
  public class IndexViewModel
  {
    public IEnumerable<CategoryViewModel> Categories { get; set; }
    public IEnumerable<ArticleViewModel> Articles { get; set; }
  }
}