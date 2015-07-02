﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Exchange.Data.ContentTypes.Tnef.TnefPropertyReader
// Assembly: Microsoft.Exchange.Data.Common, Version=15.0.1040.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 60AF4FF7-547F-476B-8FAC-6C80D63CB41A
// Assembly location: C:\Users\Thomas\Downloads\Microsoft.Exchange.Data.Common.dll

using System;
using System.IO;
using System.Linq;

namespace Butler.Schema.Data.ContentTypes.Tnef
{
  public struct TnefPropertyReader
  {
    internal TnefReader Reader;

    public int RowCount => this.Reader.RowCount;

      public int PropertyCount => this.Reader.PropertyCount;

      public TnefPropertyTag PropertyTag => this.Reader.PropertyTag;

      public bool IsMultiValuedProperty => this.Reader.PropertyTag.IsMultiValued;

      public int ValueCount => this.Reader.PropertyValueCount;

      public bool IsNamedProperty => this.Reader.PropertyTag.IsNamed;

      public TnefNameId PropertyNameId => this.Reader.PropertyNameId;

      public Type ValueType => this.Reader.PropertyValueClrType;

      public bool IsObjectProperty => this.Reader.PropertyTag.ValueTnefType == TnefPropertyType.Object;

      public Guid ObjectIid => this.Reader.PropertyValueOleIID;

      public bool IsEmbeddedMessage => this.Reader.IsPropertyEmbeddedMessage;

      public bool IsLargeValue => this.Reader.IsLargePropertyValue;

      public bool IsComputedProperty => this.Reader.IsComputedProperty;

      public int RawValueStreamOffset => this.Reader.PropertyRawValueStreamOffset;

      public int RawValueLength => this.Reader.PropertyRawValueLength;

      internal TnefPropertyReader(TnefReader reader)
    {
      this.Reader = reader;
    }

    public bool ReadValueAsBoolean()
    {
      return this.Reader.ReadPropertyValueAsBool();
    }

    public short ReadValueAsInt16()
    {
      return this.Reader.ReadPropertyValueAsShort();
    }

    public int ReadValueAsInt32()
    {
      return this.Reader.ReadPropertyValueAsInt();
    }

    public long ReadValueAsInt64()
    {
      return this.Reader.ReadPropertyValueAsLong();
    }

    public Guid ReadValueAsGuid()
    {
      return this.Reader.ReadPropertyValueAsGuid();
    }

    public float ReadValueAsFloat()
    {
      return this.Reader.ReadPropertyValueAsFloat();
    }

    public double ReadValueAsDouble()
    {
      return this.Reader.ReadPropertyValueAsDouble();
    }

    public DateTime ReadValueAsDateTime()
    {
      return this.Reader.ReadPropertyValueAsDateTime();
    }

    public string ReadValueAsString()
    {
      return this.Reader.ReadPropertyValueAsString();
    }

    public byte[] ReadValueAsBytes()
    {
      return this.Reader.ReadPropertyValueAsByteArray();
    }

    public object ReadValue()
    {
      return this.Reader.ReadPropertyValue();
    }

    public int ReadTextValue(char[] buffer, int offset, int count)
    {
      return this.Reader.ReadPropertyTextValue(buffer, offset, count);
    }

    public int ReadRawValue(byte[] buffer, int offset, int count)
    {
      return this.Reader.ReadPropertyRawValue(buffer, offset, count, false);
    }

    public TnefReader GetEmbeddedMessageReader()
    {
      return this.Reader.GetEmbeddedMessageReader();
    }

    public Stream GetRawValueReadStream()
    {
      return this.Reader.GetRawPropertyValueReadStream();
    }

    public bool ReadNextValue()
    {
      return this.Reader.ReadNextPropertyValue();
    }

    public bool ReadNextProperty()
    {
      return this.Reader.ReadNextProperty();
    }

    public bool ReadNextRow()
    {
      return this.Reader.ReadNextRow();
    }
  }
}