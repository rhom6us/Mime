﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Exchange.Data.Globalization.Charset
// Assembly: Microsoft.Exchange.Data.Common, Version=15.0.1040.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 60AF4FF7-547F-476B-8FAC-6C80D63CB41A
// Assembly location: C:\Users\Thomas\Downloads\Microsoft.Exchange.Data.Common.dll

using System;
using System.Linq;
using System.Text;

namespace Butler.Schema.Data.Globalization
{
  [Serializable]
  public class Charset
  {

      private short mapIndex;
      private bool available;
    private Encoding encoding;

      public static Charset DefaultMimeCharset => Culture.Default.MimeCharset;

      public static bool FallbackToDefaultCharset => Culture.FallbackToDefaultCharset;

      public static Charset DefaultWebCharset => Culture.Default.WebCharset;

      public static Charset DefaultWindowsCharset => Culture.Default.WindowsCharset;

      public static Charset ASCII => CultureCharsetDatabase.Data.AsciiCharset;

      public static Charset UTF8 => CultureCharsetDatabase.Data.Utf8Charset;

      public static Charset Unicode => CultureCharsetDatabase.Data.UnicodeCharset;

      public int CodePage { get; }

      public string Name { get; private set; }

      public Culture Culture { get; private set; }

      public bool IsDetectable
    {
      get
      {
        if ((int) this.mapIndex >= 0)
          return CodePageFlags.None != (CodePageMapData.codePages[(int) this.mapIndex].flags & CodePageFlags.Detectable);
        return false;
      }
    }

    public bool IsAvailable
    {
      get
      {
        if (!this.available)
          return false;
        if (this.encoding == null)
          return this.CheckAvailable();
        return true;
      }
    }

    public bool IsWindowsCharset { get; private set; }

      public string Description { get; private set; }

      internal static int MaxCharsetNameLength => CultureCharsetDatabase.Data.MaxCharsetNameLength;

      internal int MapIndex => (int) this.mapIndex;

      internal CodePageKind Kind
    {
      get
      {
        if ((int) this.mapIndex >= 0)
          return CodePageMapData.codePages[(int) this.mapIndex].kind;
        return CodePageKind.Unknown;
      }
    }

    internal CodePageAsciiSupport AsciiSupport
    {
      get
      {
        if ((int) this.mapIndex >= 0)
          return CodePageMapData.codePages[(int) this.mapIndex].asciiSupport;
        return CodePageAsciiSupport.Unknown;
      }
    }

    internal CodePageUnicodeCoverage UnicodeCoverage
    {
      get
      {
        if ((int) this.mapIndex >= 0)
          return CodePageMapData.codePages[(int) this.mapIndex].unicodeCoverage;
        return CodePageUnicodeCoverage.Unknown;
      }
    }

    internal bool IsSevenBit
    {
      get
      {
        if ((int) this.mapIndex >= 0)
          return CodePageFlags.None != (CodePageMapData.codePages[(int) this.mapIndex].flags & CodePageFlags.SevenBit);
        return false;
      }
    }

    internal int DetectableCodePageWithEquivalentCoverage
    {
      get
      {
        if ((int) this.mapIndex < 0)
          return 0;
        if ((CodePageMapData.codePages[(int) this.mapIndex].flags & CodePageFlags.Detectable) == CodePageFlags.None)
          return (int) CodePageMapData.codePages[(int) this.mapIndex].detectCpid;
        return this.CodePage;
      }
    }

    internal Charset(int codePage, string name)
    {
      this.CodePage = codePage;
      this.Name = name;
      this.Culture = (Culture) null;
      this.available = true;
      this.mapIndex = (short) -1;
    }

    public static Charset GetCharset(string name)
    {
      Charset charset;
      if (!Charset.TryGetCharset(name, out charset))
        throw new InvalidCharsetException(name);
      return charset;
    }

    public static bool TryGetCharset(string name, out Charset charset)
    {
      if (name == null)
      {
        charset = (Charset) null;
        return false;
      }
      if (CultureCharsetDatabase.Data.NameToCharset.TryGetValue(name, out charset))
        return true;
      if (name.StartsWith("cp", StringComparison.OrdinalIgnoreCase) || name.StartsWith("ms", StringComparison.OrdinalIgnoreCase))
      {
        int codePage = 0;
        for (int index = 2; index < name.Length; ++index)
        {
          if ((int) name[index] < 48 || (int) name[index] > 57)
            return false;
          codePage = codePage * 10 + ((int) name[index] - 48);
          if (codePage >= 65536)
            return false;
        }
        if (codePage == 0)
          return false;
        return Charset.TryGetCharset(codePage, out charset);
      }
      return (Charset.FallbackToDefaultCharset && Charset.DefaultMimeCharset != null || !Charset.FallbackToDefaultCharset && Charset.DefaultMimeCharset != null && Charset.DefaultMimeCharset.Name.Equals("iso-2022-jp", StringComparison.OrdinalIgnoreCase)) && CultureCharsetDatabase.Data.NameToCharset.TryGetValue(Charset.DefaultMimeCharset.Name, out charset);
    }

    public static Charset GetCharset(int codePage)
    {
      Charset charset;
      if (!Charset.TryGetCharset(codePage, out charset))
        throw new InvalidCharsetException(codePage);
      return charset;
    }

    public static Charset GetCharset(Encoding encoding)
    {
      return Charset.GetCharset(CodePageMap.GetCodePage(encoding));
    }

    public static bool TryGetCharset(int codePage, out Charset charset)
    {
      return CultureCharsetDatabase.Data.CodePageToCharset.TryGetValue(codePage, out charset);
    }

    public static bool TryGetEncoding(int codePage, out Encoding encoding)
    {
      Charset charset;
      if (Charset.TryGetCharset(codePage, out charset))
        return charset.TryGetEncoding(out encoding);
      encoding = (Encoding) null;
      return false;
    }

    public static bool TryGetEncoding(string name, out Encoding encoding)
    {
      Charset charset;
      if (Charset.TryGetCharset(name, out charset))
        return charset.TryGetEncoding(out encoding);
      encoding = (Encoding) null;
      return false;
    }

    public static Encoding GetEncoding(int codePage)
    {
      return Charset.GetCharset(codePage).GetEncoding();
    }

    public static Encoding GetEncoding(string name)
    {
      return Charset.GetCharset(name).GetEncoding();
    }

    public Encoding GetEncoding()
    {
      Encoding encoding;
      if (!this.TryGetEncoding(out encoding))
        throw new CharsetNotInstalledException(this.CodePage, this.Name);
      return encoding;
    }

    internal void SetCulture(Culture culture)
    {
      this.Culture = culture;
    }

    internal void SetDescription(string description)
    {
      this.Description = description;
    }

    internal void SetDefaultName(string name)
    {
      this.Name = name;
    }

    internal void SetWindows()
    {
      this.IsWindowsCharset = true;
    }

    internal void SetMapIndex(int index)
    {
      this.mapIndex = (short) index;
    }

    internal bool CheckAvailable()
    {
      Encoding encoding;
      return this.TryGetEncoding(out encoding);
    }

    public static bool TryGetCharset(Encoding encoding, out Charset charset)
    {
      return Charset.TryGetCharset(encoding.CodePage, out charset);
    }

    public bool TryGetEncoding(out Encoding encoding)
    {
      if (this.encoding == null)
      {
        if (this.available)
        {
          try
          {
            this.encoding = this.CodePage != 20127 ? (this.CodePage == 28591 || this.CodePage == 28599 ? (Encoding) new RemapEncoding(this.CodePage) : (this.CodePage == 50220 || this.CodePage == 50221 || this.CodePage == 50222 ? (Encoding) new Iso2022JpEncoding(this.CodePage) : Encoding.GetEncoding(this.CodePage))) : Encoding.GetEncoding(this.CodePage, (EncoderFallback) new AsciiEncoderFallback(), DecoderFallback.ReplacementFallback);
          }
          catch (ArgumentException ex)
          {
            this.encoding = (Encoding) null;
          }
          catch (NotSupportedException ex)
          {
            this.encoding = (Encoding) null;
          }
          if (this.encoding == null)
            this.available = false;
        }
      }
      encoding = this.encoding;
      return encoding != null;
    }
  }
}