﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Exchange.Data.Mime.HeaderNameIndex
// Assembly: Microsoft.Exchange.Data.Common, Version=15.0.1040.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 60AF4FF7-547F-476B-8FAC-6C80D63CB41A
// Assembly location: C:\Users\Thomas\Downloads\Microsoft.Exchange.Data.Common.dll

using System;
using System.Linq;

namespace Butler.Schema.Data.Mime
{
  internal enum HeaderNameIndex : byte
  {
    Unknown = (byte) 0,
    ReturnPath = (byte) 1,
    ResentDate = (byte) 2,
    MessageId = (byte) 3,
    Subject = (byte) 4,
    Summary = (byte) 6,
    XPriority = (byte) 7,
    Precedence = (byte) 8,
    ListUnsubscribe = (byte) 9,
    RR = (byte) 10,
    Comments = (byte) 11,
    ContentLocation = (byte) 12,
    ResentFrom = (byte) 13,
    ContentLanguage = (byte) 14,
    ContentMD5 = (byte) 15,
    NewsGroups = (byte) 16,
    Importance = (byte) 17,
    ResentCc = (byte) 18,
    ResentBcc = (byte) 19,
    ResentSender = (byte) 20,
    ContentClass = (byte) 21,
    ListHelp = (byte) 22,
    AdHoc = (byte) 23,
    ContentBase = (byte) 24,
    From = (byte) 25,
    ReturnReceiptTo = (byte) 26,
    NntpPostingHost = (byte) 27,
    Lines = (byte) 28,
    ContentDisposition = (byte) 29,
    Bytes = (byte) 31,
    ContentId = (byte) 32,
    Cc = (byte) 33,
    FollowUpTo = (byte) 34,
    ReplyBy = (byte) 35,
    Bcc = (byte) 36,
    ResentMessageId = (byte) 37,
    XMSMailPriority = (byte) 39,
    XExchangeBcc = (byte) 40,
    DeferredDelivery = (byte) 41,
    Expires = (byte) 42,
    Organization = (byte) 44,
    ContentTransferEncoding = (byte) 45,
    Encoding = (byte) 46,
    XExchangeCrossPremisesBcc = (byte) 47,
    ContentType = (byte) 48,
    ApparentlyTo = (byte) 49,
    Approved = (byte) 50,
    Received = (byte) 52,
    Path = (byte) 53,
    References = (byte) 54,
    Sender = (byte) 55,
    ResentTo = (byte) 56,
    Supercedes = (byte) 58,
    ResentReplyTo = (byte) 59,
    Keywords = (byte) 60,
    Control = (byte) 61,
    InReplyTo = (byte) 62,
    ListSubscribe = (byte) 63,
    ContentDescription = (byte) 64,
    Encrypted = (byte) 65,
    MimeVersion = (byte) 67,
    Article = (byte) 68,
    DispositionNotificationTo = (byte) 69,
    Distribution = (byte) 70,
    Date = (byte) 71,
    XRef = (byte) 72,
    Sensitivity = (byte) 74,
    To = (byte) 75,
    ReplyTo = (byte) 76,
    ExpiryDate = (byte) 77,
    Priority = (byte) 78,
  }
}