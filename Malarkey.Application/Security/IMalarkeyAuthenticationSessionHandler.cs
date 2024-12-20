﻿using Malarkey.Domain.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malarkey.Application.Security;
public interface IMalarkeyAuthenticationSessionHandler
{
    Task<MalarkeyAuthenticationSession> InitSession(HttpContext context);
    Task<MalarkeyAuthenticationSession?> SessionForState(string state);

}
