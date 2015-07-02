﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Exchange.Data.TextConverters.TextCache
// Assembly: Microsoft.Exchange.Data.Common, Version=15.0.1040.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 60AF4FF7-547F-476B-8FAC-6C80D63CB41A
// Assembly location: C:\Users\Thomas\Downloads\Microsoft.Exchange.Data.Common.dll

using System;
using System.Linq;

namespace Butler.Schema.Data.TextConverters
{
  internal class TextCache
  {

      private TextCache.CacheEntry headEntry;
    private TextCache.CacheEntry tailEntry;
    private TextCache.CacheEntry freeList;

    public int Length { get; private set; }

      public void Reset()
    {
      while (this.headEntry != null)
      {
        this.headEntry.Reset();
        TextCache.CacheEntry cacheEntry = this.headEntry;
        this.headEntry = this.headEntry.Next;
        if (this.headEntry == null)
          this.tailEntry = (TextCache.CacheEntry) null;
        cacheEntry.Next = this.freeList;
        this.freeList = cacheEntry;
      }
      this.Length = 0;
    }

    public void GetBuffer(int size, out char[] buffer, out int offset, out int realSize)
    {
      if (this.tailEntry != null && this.tailEntry.GetBuffer(size, out buffer, out offset, out realSize))
        return;
      this.AllocateTail(size);
      this.tailEntry.GetBuffer(size, out buffer, out offset, out realSize);
    }

    public void Commit(int count)
    {
      this.tailEntry.Commit(count);
      this.Length += count;
    }

    public void GetData(out char[] outputBuffer, out int outputOffset, out int outputCount)
    {
      this.headEntry.GetData(out outputBuffer, out outputOffset, out outputCount);
    }

    public void ReportRead(int count)
    {
      this.headEntry.ReportRead(count);
      this.Length -= count;
      if (this.headEntry.Length != 0)
        return;
      TextCache.CacheEntry cacheEntry = this.headEntry;
      this.headEntry = this.headEntry.Next;
      if (this.headEntry == null)
        this.tailEntry = (TextCache.CacheEntry) null;
      cacheEntry.Next = this.freeList;
      this.freeList = cacheEntry;
    }

    public int Read(char[] buffer, int offset, int count)
    {
      int num1 = 0;
      while (count != 0)
      {
        int num2 = this.headEntry.Read(buffer, offset, count);
        offset += num2;
        count -= num2;
        num1 += num2;
        this.Length -= num2;
        if (this.headEntry.Length == 0)
        {
          TextCache.CacheEntry cacheEntry = this.headEntry;
          this.headEntry = this.headEntry.Next;
          if (this.headEntry == null)
            this.tailEntry = (TextCache.CacheEntry) null;
          cacheEntry.Next = this.freeList;
          this.freeList = cacheEntry;
        }
        if (count == 0 || this.headEntry == null)
          break;
      }
      return num1;
    }

    private void AllocateTail(int size)
    {
      TextCache.CacheEntry cacheEntry = this.freeList;
      if (cacheEntry != null)
      {
        this.freeList = cacheEntry.Next;
        cacheEntry.Next = (TextCache.CacheEntry) null;
      }
      else
        cacheEntry = new TextCache.CacheEntry(size);
      if (this.tailEntry != null)
        this.tailEntry.Next = cacheEntry;
      else
        this.headEntry = cacheEntry;
      this.tailEntry = cacheEntry;
    }

    internal class CacheEntry
    {
      private const int DefaultMaxLength = 4096;
      private char[] buffer;
        private int offset;

        public int Length { get; private set; }

        public TextCache.CacheEntry Next { get; set; }

        public CacheEntry(int size)
      {
        this.AllocateBuffer(size);
      }

      public void Reset()
      {
        this.Length = 0;
      }

      public bool GetBuffer(int size, out char[] buffer, out int offset, out int realSize)
      {
        if (this.Length == 0)
        {
          this.offset = 0;
          if (this.buffer.Length < size)
            this.AllocateBuffer(size);
        }
        if (this.buffer.Length - (this.offset + this.Length) >= size)
        {
          buffer = this.buffer;
          offset = this.offset + this.Length;
          realSize = this.buffer.Length - offset;
          return true;
        }
        if (this.Length < 64 && this.buffer.Length - this.Length >= size)
        {
          Buffer.BlockCopy((Array) this.buffer, this.offset * 2, (Array) this.buffer, 0, this.Length * 2);
          this.offset = 0;
          buffer = this.buffer;
          offset = this.offset + this.Length;
          realSize = this.buffer.Length - offset;
          return true;
        }
        buffer = (char[]) null;
        offset = 0;
        realSize = 0;
        return false;
      }

      public void Commit(int count)
      {
        this.Length += count;
      }

      public void GetData(out char[] outputBuffer, out int outputOffset, out int outputCount)
      {
        outputBuffer = this.buffer;
        outputOffset = this.offset;
        outputCount = this.Length;
      }

      public void ReportRead(int count)
      {
        this.offset += count;
        this.Length -= count;
      }

      public int Read(char[] buffer, int offset, int count)
      {
        int num = Math.Min(count, this.Length);
        Buffer.BlockCopy((Array) this.buffer, this.offset * 2, (Array) buffer, offset * 2, num * 2);
        this.Length -= num;
        this.offset += num;
        count -= num;
        offset += num;
        return num;
      }

      private void AllocateBuffer(int size)
      {
        if (size < 2048)
          size = 2048;
        size = (size * 2 + 1023) / 1024 * 1024;
        this.buffer = new char[size];
      }
    }
  }
}
