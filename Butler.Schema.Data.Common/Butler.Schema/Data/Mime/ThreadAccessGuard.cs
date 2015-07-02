﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Exchange.Data.Mime.ThreadAccessGuard
// Assembly: Microsoft.Exchange.Data.Common, Version=15.0.1040.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 60AF4FF7-547F-476B-8FAC-6C80D63CB41A
// Assembly location: C:\Users\Thomas\Downloads\Microsoft.Exchange.Data.Common.dll

using System;
using System.Linq;

namespace Butler.Schema.Data.Mime
{
  internal class ThreadAccessGuard : IDisposable
  {
    private bool isDisposed;

    private ThreadAccessGuard(ObjectThreadAccessToken token)
    {
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    internal static IDisposable EnterPublic(ObjectThreadAccessToken token)
    {
      return (IDisposable) null;
    }

    internal static IDisposable EnterPrivate(ObjectThreadAccessToken token)
    {
      return (IDisposable) null;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.isDisposed)
        return;
      this.isDisposed = true;
    }
  }
}