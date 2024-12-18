﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malarkey.Domain.Token;
public abstract record MalarkeyToken
{
    public Guid TokenId { get; private set; }
    public string IssuedTo { get; private set; }
    public DateTime IssuedAt { get; private set; }
    public DateTime ValidUntil { get; private set; }

    internal MalarkeyToken(
        Guid tokenId, 
        string issuedTo,
        DateTime issuedAt, 
        DateTime validUntil    
) 
    {
        TokenId = tokenId;
        IssuedTo = issuedTo;
        IssuedAt = issuedAt;
        ValidUntil = validUntil;
    }
}
