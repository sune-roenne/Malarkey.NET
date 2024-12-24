﻿using Malarkey.Domain.Authentication;
using Malarkey.Domain.Profile;
using Malarkey.Domain.Util;
using Malarkey.Integration.Authentication.Naming;
using Malarkey.Integration.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malarkey.Integration.Authentication.OAuthFlowHandlers;
internal class MalarkeySpotifyOAuthFlowHandler : MalarkeyOAuthFlowHandler
{
    public override MalarkeyOAuthIdentityProvider HandlerFor => MalarkeyOAuthIdentityProvider.Spotify;

    public MalarkeySpotifyOAuthFlowHandler(IOptions<MalarkeyIntegrationConfiguration> intConf) : base(intConf)
    {
    }
    public override string AuthorizationEndpoint => _conf.AuthorizationEndpointTemplate;

    protected override MalarkeyOAuthNamingScheme ProduceNamingScheme() => MalarkeySpotifyOAuthNamingScheme.Init(_conf.NamingSchemeOverwrites);
    protected override MalarkeyIdentityProviderConfiguration ProduceConfiguration() => _intConf.Spotify;

    public override IReadOnlyDictionary<string, string> ProduceRequestQueryParameters(MalarkeyAuthenticationSession session)
    {
        var returnee = new Dictionary<string, string>();
        returnee[_namingScheme.ClientId] = _conf.ClientId;
        returnee[_namingScheme.ResponseType] = _conf.ResponseType!;
        returnee[_namingScheme.RedirectUri] = _intConf.RedirectUrl;
        returnee[_namingScheme.Scope] = (_conf.Scopes ?? DefaultScopes)
            .MakeString(" ")
            .UrlEncoded();
        returnee[_namingScheme.CodeChallenge] = session.CodeChallenge;
        returnee[_namingScheme.CodeChallengeMethod] = DefaultCodeChallengeMethod;
        returnee[_namingScheme.State] = session.State;
        return returnee;
    }

    public override async Task<IMalarkeyOAuthFlowHandler.RedirectData?> ExtractRedirectData(HttpRequest request)
    {
        var keyValued = request.Query
            .Select(_ => (_.Key, Value: _.Value.ToString()))
            .ToDictionarySafe(_ => _.Key,  _ => _.Value);
        if (!keyValued.TryGetValue("code", out var code))
            return null;
        if(!keyValued.TryGetValue("state", out var state))
            return null;
        await Task.CompletedTask;
        var returnee = new IMalarkeyOAuthFlowHandler.RedirectData(
            State: state,
            Code: code
            );
        return returnee;
    }

    public override Task<MalarkeyProfileIdentity?> ResolveIdentity(MalarkeyAuthenticationSession session, IMalarkeyOAuthFlowHandler.RedirectData redirectData)
    {
        throw new NotImplementedException();
    }
}


/*
 * ######################## Spotify ##############################

const clientId = 'YOUR_CLIENT_ID';
const redirectUri = 'http://localhost:8080';

const scope = 'user-read-private user-read-email';
const authUrl = new URL("https://accounts.spotify.com/authorize")

// generated in the previous step
window.localStorage.setItem('code_verifier', codeVerifier);

const params =  {
  response_type: 'code',
  client_id: clientId,
  scope,
  code_challenge_method: 'S256',
  code_challenge: codeChallenge,
  redirect_uri: redirectUri,
}

authUrl.search = new URLSearchParams(params).toString();
window.location.href = authUrl.toString();

REDEEM ACCESS TOKEN:
grant_type	Required	This field must contain the value authorization_code.
code	Required	The authorization code returned from the previous request.
redirect_uri	Required	This parameter is used for validation only (there is no actual redirection). The value of this parameter must exactly match the value of redirect_uri supplied when requesting the authorization code.
client_id	Required	The client ID for your app, available from the developer dashboard.
code_verifier	Required	The value of this parameter must match the value of the code_verifier that your app generated in the previous step.
The request must include the following HTTP header:

Header Parameter	Relevance	Value
Content-Type	Required	Set to application/x-www-form-urlencoded. 
 
 * 
 * 
 * 
 */
