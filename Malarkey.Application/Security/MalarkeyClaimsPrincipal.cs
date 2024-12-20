﻿using Malarkey.Domain.Profile;
using Malarkey.Domain.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Malarkey.Application.Security;
public class MalarkeyClaimsPrincipal : ClaimsPrincipal
{
    public const string MalarkeyIdClaimType = "malid";
    public const string MalarkeyNameClaimType = "malnam";
    public const string MalarkeyIdentityName = "malarkey";
    public const string IdentityTypeName = "idtyp";
    public MalarkeyClaimsPrincipal(MalarkeyProfileToken profileToken, IEnumerable<MalarkeyIdentityToken> externalIdentities)
        : this(profileToken.Profile, externalIdentities.Select(_ => _.Identity))
    {

    }

    public MalarkeyClaimsPrincipal(MalarkeyProfile profile, IEnumerable<MalarkeyProfileIdentity> externalIdentities)
    {

        AddIdentity(new MalarkeyClaimsIdentity(claims: [
            new Claim(IdentityTypeName, MalarkeyIdentityName),
            new Claim(MalarkeyIdClaimType, profile.ProfileId.ToString()),
            new Claim(MalarkeyNameClaimType, profile.ProfileName)
           ])
        );
        foreach (var extId in externalIdentities)
        {
            AddIdentity(new MalarkeyClaimsIdentity(claims: [
                new Claim(IdentityTypeName, extId.IdentityType),
                        new Claim(MalarkeyIdClaimType, extId.ProviderId),
                        new Claim(MalarkeyNameClaimType, extId.FirstName)
               ])
            );
        }
    }



}


public static class MalarkeyClaimsPrincipalExtensions
{
    public static MalarkeyClaimsPrincipal ToClaimsPrincipal(this MalarkeyProfile profile, IEnumerable<MalarkeyProfileIdentity> identities) => new MalarkeyClaimsPrincipal(
        profile,
        identities
        );

    public static MalarkeyClaimsPrincipal ToClaimsPrincipal(this MalarkeyProfileToken profile, IEnumerable<MalarkeyIdentityToken> identities) => new MalarkeyClaimsPrincipal(
        profile,
        identities
        );

}