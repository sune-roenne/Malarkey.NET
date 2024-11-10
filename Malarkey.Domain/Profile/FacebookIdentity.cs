﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malarkey.Domain.Profile;
public sealed record FacebookIdentity(
    Guid ProfileId,
    string FacebookId,
    string Name,
    string? MiddleNames,
    string? LastName
    ) : ProfileIdentity(
        ProfileId,
        FacebookId,
        Name,
        MiddleNames,
        LastName
        );
