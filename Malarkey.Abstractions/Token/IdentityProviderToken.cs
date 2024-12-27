﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malarkey.Abstractions.Token;
public record IdentityProviderToken(
    string Token,
    DateTime Issued,
    DateTime Expires,
    string? RefreshToken,
    string[] Scopes
    );