﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Exchange.Data.Internal.ReadableWritableDataStorageOnStream
// Assembly: Microsoft.Exchange.Data.Common, Version=15.0.1040.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 60AF4FF7-547F-476B-8FAC-6C80D63CB41A
// Assembly location: C:\Users\Thomas\Downloads\Microsoft.Exchange.Data.Common.dll

using System;
using System.IO;
using System.Linq;

namespace Butler.Schema.Data.Internal
{
  internal class ReadableWritableDataStorageOnStream : ReadableWritableDataStorage
  {
    protected Stream stream;
    protected bool ownsStream;

    public override long Length
    {
      get
      {
        this.ThrowIfDisposed();
        return this.stream.Length;
      }
    }

    public ReadableWritableDataStorageOnStream(Stream stream, bool ownsStream)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof(stream));
      this.stream = stream;
      this.ownsStream = ownsStream;
    }

    public override int Read(long position, byte[] buffer, int offset, int count)
    {
      this.ThrowIfDisposed();
      int num = 0;
      if (this.isReadOnly)
      {
        this.readOnlySemaphore.Wait();
        try
        {
          num = this.InternalRead(position, buffer, offset, count);
        }
        finally
        {
          this.readOnlySemaphore.Release();
        }
      }
      else
        num = this.InternalRead(position, buffer, offset, count);
      return num;
    }

    public override void Write(long position, byte[] buffer, int offset, int count)
    {
      this.ThrowIfDisposed();
      if (this.isReadOnly)
        throw new InvalidOperationException("Write to read-only DataStorage");
      this.stream.Position = position;
      this.stream.Write(buffer, offset, count);
    }

    public override void SetLength(long length)
    {
      this.ThrowIfDisposed();
      if (this.isReadOnly)
        throw new InvalidOperationException("Write to read-only DataStorage");
      this.stream.SetLength(length);
    }

    protected override void Dispose(bool disposing)
    {
      if (!this.IsDisposed)
      {
        if (disposing && this.ownsStream)
          this.stream.Dispose();
        this.stream = (Stream) null;
      }
      base.Dispose(disposing);
    }

    private int InternalRead(long position, byte[] buffer, int offset, int count)
    {
      this.stream.Position = position;
      return this.stream.Read(buffer, offset, count);
    }
  }
}