﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Exchange.Data.TextConverters.Internal.Format.LengthUnits
// Assembly: Microsoft.Exchange.Data.Common, Version=15.0.1040.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 60AF4FF7-547F-476B-8FAC-6C80D63CB41A
// Assembly location: C:\Users\Thomas\Downloads\Microsoft.Exchange.Data.Common.dll

using System;
using System.Linq;

namespace Butler.Schema.Data.TextConverters.Internal.Format
{
  internal enum LengthUnits : byte
  {
    BaseUnits,
    Twips,
    Points,
    Picas,
    Inches,
    Centimeters,
    Millimeters,
    HtmlFontUnits,
    Pixels,
    Ems,
    Exs,
    RelativeHtmlFontUnits,
    Percents,
    Multiple,
  }
}