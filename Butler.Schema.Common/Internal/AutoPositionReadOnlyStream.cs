﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Exchange.Data.Internal.AutoPositionReadOnlyStream
// Assembly: Microsoft.Exchange.Data.Common, Version=15.0.1040.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 60AF4FF7-547F-476B-8FAC-6C80D63CB41A
// Assembly location: C:\Users\Thomas\Downloads\Microsoft.Exchange.Data.Common.dll

using System;
using System.IO;
using System.Linq;

namespace Butler.Schema.Data.Internal
{
  internal sealed class AutoPositionReadOnlyStream : Stream, ICloneableStream
  {
    private ReadableDataStorage storage;
    private long position;

    public override bool CanRead => this.storage != null;

      public override bool CanWrite => false;

      public override bool CanSeek => this.storage != null;

      public override long Length
    {
      get
      {
        if (this.storage == null)
          throw new ObjectDisposedException("AutoPositionReadOnlyStream");
        return this.storage.Length;
      }
    }

    public override long Position
    {
      get
      {
        if (this.storage == null)
          throw new ObjectDisposedException("AutoPositionReadOnlyStream");
        return this.position;
      }
      set
      {
        if (this.storage == null)
          throw new ObjectDisposedException("AutoPositionReadOnlyStream");
        if (value < 0L)
          throw new ArgumentOutOfRangeException(nameof(value), Resources.SharedStrings.CannotSeekBeforeBeginning);
        this.position = value;
      }
    }

    public AutoPositionReadOnlyStream(Stream wrapped, bool ownsStream)
    {
      this.storage = (ReadableDataStorage) new ReadableDataStorageOnStream(wrapped, ownsStream);
      this.position = wrapped.Position;
    }

    private AutoPositionReadOnlyStream(AutoPositionReadOnlyStream original)
    {
      original.storage.AddRef();
      this.storage = original.storage;
      this.position = original.position;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this.storage == null)
        throw new ObjectDisposedException("AutoPositionReadOnlyStream");
      int num = this.storage.Read(this.position, buffer, offset, count);
      this.position += (long) num;
      return num;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
      throw new NotSupportedException();
    }

    public override void Flush()
    {
      throw new NotSupportedException();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      if (this.storage == null)
        throw new ObjectDisposedException("AutoPositionReadOnlyStream");
      switch (origin)
      {
        case SeekOrigin.Begin:
          if (0L > offset)
            throw new ArgumentOutOfRangeException(nameof(offset), Resources.SharedStrings.CannotSeekBeforeBeginning);
          this.position = offset;
          return this.position;
        case SeekOrigin.Current:
          offset += this.position;
          goto case 0;
        case SeekOrigin.End:
          offset += this.Length;
          goto case 0;
        default:
          throw new ArgumentException("origin");
      }
    }

    public Stream Clone()
    {
      if (this.storage == null)
        throw new ObjectDisposedException("AutoPositionReadOnlyStream");
      return (Stream) new AutoPositionReadOnlyStream(this);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.storage != null)
      {
        this.storage.Release();
        this.storage = (ReadableDataStorage) null;
      }
      base.Dispose(disposing);
    }
  }
}
