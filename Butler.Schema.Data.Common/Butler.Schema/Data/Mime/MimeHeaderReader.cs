﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Exchange.Data.Mime.MimeHeaderReader
// Assembly: Microsoft.Exchange.Data.Common, Version=15.0.1040.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 60AF4FF7-547F-476B-8FAC-6C80D63CB41A
// Assembly location: C:\Users\Thomas\Downloads\Microsoft.Exchange.Data.Common.dll

using System;
using System.Linq;

namespace Butler.Schema.Data.Mime
{
  public struct MimeHeaderReader
  {

      internal MimeReader MimeReader { get; }

      public HeaderId HeaderId
    {
      get
      {
        this.AssertGood(true);
        return this.MimeReader.HeaderId;
      }
    }

    public string Name
    {
      get
      {
        this.AssertGood(true);
        return this.MimeReader.HeaderName;
      }
    }

    public bool IsAddressHeader
    {
      get
      {
        this.AssertGood(true);
        return Header.TypeFromHeaderId(this.HeaderId) == typeof (AddressHeader);
      }
    }

    public MimeAddressReader AddressReader
    {
      get
      {
        if (!this.IsAddressHeader)
          throw new InvalidOperationException(CtsResources.Strings.HeaderCannotHaveAddresses);
        if (this.MimeReader.ReaderState == MimeReaderState.HeaderStart)
          this.MimeReader.TryCompleteCurrentHeader(true);
        return new MimeAddressReader(this.MimeReader, true);
      }
    }

    public bool CanHaveParameters
    {
      get
      {
        this.AssertGood(true);
        Type type = Header.TypeFromHeaderId(this.HeaderId);
        if (!(type == typeof (ContentTypeHeader)))
          return type == typeof (ContentDispositionHeader);
        return true;
      }
    }

    public MimeParameterReader ParameterReader
    {
      get
      {
        if (!this.CanHaveParameters)
          throw new InvalidOperationException(CtsResources.Strings.HeaderCannotHaveParameters);
        if (this.MimeReader.ReaderState == MimeReaderState.HeaderStart)
          this.MimeReader.TryCompleteCurrentHeader(true);
        return new MimeParameterReader(this.MimeReader);
      }
    }

    public string Value
    {
      get
      {
        DecodingResults decodingResults;
        string str;
        if (!this.TryGetValue(this.MimeReader.HeaderDecodingOptions, out decodingResults, out str))
          MimeCommon.ThrowDecodingFailedException(ref decodingResults);
        return str;
      }
    }

    internal MimeHeaderReader(MimeReader reader)
    {
      this.MimeReader = reader;
    }

    public DateTime ReadValueAsDateTime()
    {
      this.AssertGood(true);
      if (this.MimeReader.ReaderState == MimeReaderState.HeaderStart)
        this.MimeReader.TryCompleteCurrentHeader(true);
      if (this.MimeReader.CurrentHeaderObject == null)
        return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
      DateHeader dateHeader = this.MimeReader.CurrentHeaderObject as DateHeader;
      if (dateHeader != null)
        return dateHeader.DateTime;
      return DateHeader.ParseDateHeaderValue(this.MimeReader.CurrentHeaderObject.Value);
    }

    public bool ReadNextHeader()
    {
      this.AssertGood(false);
      while (this.MimeReader.ReadNextHeader())
      {
        if (this.MimeReader.HeaderName != null)
          return true;
      }
      return false;
    }

    public bool TryGetValue(out string value)
    {
      DecodingResults decodingResults;
      return this.TryGetValue(this.MimeReader.HeaderDecodingOptions, out decodingResults, out value);
    }

    public bool TryGetValue(DecodingOptions decodingOptions, out DecodingResults decodingResults, out string value)
    {
      this.AssertGood(true);
      if (this.MimeReader.ReaderState == MimeReaderState.HeaderStart)
        this.MimeReader.TryCompleteCurrentHeader(true);
      if (this.MimeReader.CurrentHeaderObject != null)
      {
        TextHeader textHeader = this.MimeReader.CurrentHeaderObject as TextHeader;
        if (textHeader != null)
        {
          value = textHeader.GetDecodedValue(decodingOptions, out decodingResults);
          if (!decodingResults.DecodingFailed)
            return true;
          value = (string) null;
          return false;
        }
        value = this.MimeReader.CurrentHeaderObject.Value;
      }
      else
        value = (string) null;
      decodingResults = new DecodingResults();
      return true;
    }

    private void AssertGood(bool checkPositionedOnHeader)
    {
      if (this.MimeReader == null)
        throw new NotSupportedException(CtsResources.Strings.HeaderReaderNotInitialized);
      this.MimeReader.AssertGoodToUse(true, true);
      if (!MimeReader.StateIsOneOf(this.MimeReader.ReaderState, MimeReaderState.PartStart | MimeReaderState.HeaderStart | MimeReaderState.HeaderIncomplete | MimeReaderState.HeaderComplete | MimeReaderState.EndOfHeaders | MimeReaderState.InlineStart))
        throw new NotSupportedException(CtsResources.Strings.HeaderReaderCannotBeUsedInThisState);
      if (checkPositionedOnHeader && MimeReader.StateIsOneOf(this.MimeReader.ReaderState, MimeReaderState.PartStart | MimeReaderState.EndOfHeaders))
        throw new InvalidOperationException(CtsResources.Strings.HeaderReaderIsNotPositionedOnAHeader);
    }
  }
}