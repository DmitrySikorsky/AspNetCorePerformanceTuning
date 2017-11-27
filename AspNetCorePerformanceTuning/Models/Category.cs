// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace AspNetCorePerformanceTuning.Models
{
  public class Category
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Article> Articles { get; set; }
  }
}