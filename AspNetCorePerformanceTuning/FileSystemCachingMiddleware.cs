// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AspNetCorePerformanceTuning
{
  // WARNING: you should NOT use this class in production, it just illustrates the idea of file system caching
  public class FileSystemCachingMiddleware
  {
    private readonly RequestDelegate next;

    public FileSystemCachingMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext context, IHostingEnvironment hostingEnvironment)
    {
      string filepath = this.GenerateFilepath(context, hostingEnvironment);
      byte[] content = null;

      if (!File.Exists(filepath))
      {
        content = await this.GetResponseBytes(context);

        File.WriteAllBytes(filepath, content);
      }

      if (content == null)
        content = File.ReadAllBytes(filepath);

      await this.WriteResponse(context, content);
    }

    private async Task<byte[]> GetResponseBytes(HttpContext context)
    {
      Stream response = context.Response.Body;

      using (MemoryStream buffer = new MemoryStream())
      {
        try
        {
          context.Response.Body = buffer;

          await this.next.Invoke(context);
        }

        finally
        {
          context.Response.Body = response;
        }

        return buffer.ToArray();
      }
    }

    private async Task WriteResponse(HttpContext context, byte[] content)
    {
      await context.Response.Body.WriteAsync(content, 0, content.Length);
    }

    private string GenerateFilepath(HttpContext context, IHostingEnvironment hostingEnvironment)
    {
      string key = this.GenerateKey(context);

      return $@"{hostingEnvironment.ContentRootPath}\Cache\{key}.cache";
    }

    private string GenerateKey(HttpContext context)
    {
      return this.CalculateHash(context.Request.Path);
    }

    public string CalculateHash(string input)
    {
      using (MD5 hasher = MD5.Create())
      {
        byte[] hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

        return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
      }
    }
  }
}