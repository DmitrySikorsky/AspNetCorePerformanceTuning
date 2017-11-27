// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace AspNetCorePerformanceTuning.Models
{
  public class Photo
  {
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public string Filename { get; set; }
    public bool IsDefault { get; set; }

    public virtual Article Article { get; set; }
  }
}