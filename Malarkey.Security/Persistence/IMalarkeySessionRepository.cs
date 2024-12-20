﻿using Malarkey.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malarkey.Security.Persistence;
public interface IMalarkeySessionRepository
{
    Task<MalarkeyAuthenticationSession> InitNewSession(string idProvider, string nonce, string? forwarder, string codeChallenge, string codeVerifier, DateTime initTime);
    Task<MalarkeyAuthenticationSession?> SessionFor(string state);
    Task<MalarkeyAuthenticationSession?> UpdateWithAuthenticationInfo(string state, DateTime authenticatedTime, string profileTokenId, string identityTokenId);


}