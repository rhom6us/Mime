﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Exchange.Data.Globalization.Iso2022JpEncoding
// Assembly: Microsoft.Exchange.Data.Common, Version=15.0.1040.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 60AF4FF7-547F-476B-8FAC-6C80D63CB41A
// Assembly location: C:\Users\Thomas\Downloads\Microsoft.Exchange.Data.Common.dll

using System;
using System.Linq;
using System.Text;

namespace Butler.Schema.Data.Globalization
{
  internal class Iso2022JpEncoding : Encoding
  {

      internal Iso2022DecodingMode KillSwitch => Iso2022JpEncoding.InternalReadKillSwitch();

      public override int CodePage => this.DefaultEncoding.CodePage;

      public override string BodyName => this.DefaultEncoding.BodyName;

      public override string EncodingName => this.DefaultEncoding.EncodingName;

      public override string HeaderName => this.DefaultEncoding.HeaderName;

      public override string WebName => this.DefaultEncoding.WebName;

      public override int WindowsCodePage => this.DefaultEncoding.WindowsCodePage;

      public override bool IsBrowserDisplay => this.DefaultEncoding.IsBrowserDisplay;

      public override bool IsBrowserSave => this.DefaultEncoding.IsBrowserSave;

      public override bool IsMailNewsDisplay => this.DefaultEncoding.IsMailNewsDisplay;

      public override bool IsMailNewsSave => this.DefaultEncoding.IsMailNewsSave;

      public override bool IsSingleByte => this.DefaultEncoding.IsSingleByte;

      internal Encoding DefaultEncoding { get; }

      public Iso2022JpEncoding(int codePage)
      : base(codePage)
    {
      if (codePage == 50220)
        this.DefaultEncoding = Encoding.GetEncoding(50220);
      else if (codePage == 50221)
      {
        this.DefaultEncoding = Encoding.GetEncoding(50221);
      }
      else
      {
        if (codePage != 50222)
          throw new ArgumentException("codePage", string.Format("Iso2022JpEncoding does not support codepage {0}", (object) codePage));
        this.DefaultEncoding = Encoding.GetEncoding(50222);
      }
    }

    internal static Iso2022DecodingMode InternalReadKillSwitch()
    {
      switch (Common.RegistryConfigManager.Iso2022JpEncodingOverride)
      {
        case 1:
          return Iso2022DecodingMode.Override;
        case 2:
          return Iso2022DecodingMode.Throw;
        default:
          return Iso2022DecodingMode.Default;
      }
    }

    public override byte[] GetPreamble()
    {
      return this.DefaultEncoding.GetPreamble();
    }

    public override int GetMaxByteCount(int charCount)
    {
      return this.DefaultEncoding.GetMaxByteCount(charCount);
    }

    public override int GetMaxCharCount(int byteCount)
    {
      switch (this.KillSwitch)
      {
        case Iso2022DecodingMode.Default:
          return this.DefaultEncoding.GetMaxCharCount(byteCount);
        case Iso2022DecodingMode.Override:
          return this.DefaultEncoding.GetMaxCharCount(byteCount);
        case Iso2022DecodingMode.Throw:
          throw new NotImplementedException();
        default:
          throw new InvalidOperationException();
      }
    }

    public override int GetByteCount(char[] chars, int index, int count)
    {
      return this.DefaultEncoding.GetByteCount(chars, index, count);
    }

    public override int GetByteCount(string s)
    {
      return this.DefaultEncoding.GetByteCount(s);
    }

    public override unsafe int GetByteCount(char* chars, int count)
    {
      return this.DefaultEncoding.GetByteCount(chars, count);
    }

    public override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      return this.DefaultEncoding.GetBytes(s, charIndex, charCount, bytes, byteIndex);
    }

    public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      return this.DefaultEncoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
    }

    public override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
    {
      return this.DefaultEncoding.GetBytes(chars, charCount, bytes, byteCount);
    }

    public override int GetCharCount(byte[] bytes, int index, int count)
    {
      switch (this.KillSwitch)
      {
        case Iso2022DecodingMode.Default:
          return this.DefaultEncoding.GetCharCount(bytes, index, count);
        case Iso2022DecodingMode.Override:
          return this.GetDecoder().GetCharCount(bytes, index, count);
        case Iso2022DecodingMode.Throw:
          throw new NotImplementedException();
        default:
          throw new InvalidOperationException();
      }
    }

    public override unsafe int GetCharCount(byte* bytes, int count)
    {
      switch (this.KillSwitch)
      {
        case Iso2022DecodingMode.Default:
          return this.DefaultEncoding.GetCharCount(bytes, count);
        case Iso2022DecodingMode.Override:
          throw new NotImplementedException();
        case Iso2022DecodingMode.Throw:
          throw new NotImplementedException();
        default:
          throw new InvalidOperationException();
      }
    }

    public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
      switch (this.KillSwitch)
      {
        case Iso2022DecodingMode.Default:
          return this.DefaultEncoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
        case Iso2022DecodingMode.Override:
          return this.GetDecoder().GetChars(bytes, byteIndex, byteCount, chars, charIndex);
        case Iso2022DecodingMode.Throw:
          throw new NotImplementedException();
        default:
          throw new InvalidOperationException();
      }
    }

    public override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
    {
      switch (this.KillSwitch)
      {
        case Iso2022DecodingMode.Default:
          return this.DefaultEncoding.GetChars(bytes, byteCount, chars, charCount);
        case Iso2022DecodingMode.Override:
          throw new NotImplementedException();
        case Iso2022DecodingMode.Throw:
          throw new NotImplementedException();
        default:
          throw new InvalidOperationException();
      }
    }

    public override string GetString(byte[] bytes, int index, int count)
    {
      switch (this.KillSwitch)
      {
        case Iso2022DecodingMode.Default:
          return this.DefaultEncoding.GetString(bytes, index, count);
        case Iso2022DecodingMode.Override:
          Decoder decoder = this.GetDecoder();
          char[] chars1 = new char[this.GetMaxCharCount(count)];
          int chars2 = decoder.GetChars(bytes, index, count, chars1, 0);
          return new string(chars1, 0, chars2);
        case Iso2022DecodingMode.Throw:
          throw new NotImplementedException();
        default:
          throw new InvalidOperationException();
      }
    }

    public override Decoder GetDecoder()
    {
      switch (this.KillSwitch)
      {
        case Iso2022DecodingMode.Default:
        case Iso2022DecodingMode.Override:
          return (Decoder) new Iso2022Jp.Iso2022JpDecoder(this);
        case Iso2022DecodingMode.Throw:
          throw new NotImplementedException();
        default:
          throw new InvalidOperationException();
      }
    }

    public override Encoder GetEncoder()
    {
      return this.DefaultEncoding.GetEncoder();
    }

    public override object Clone()
    {
      return (object) (Iso2022JpEncoding) this.MemberwiseClone();
    }
  }
}