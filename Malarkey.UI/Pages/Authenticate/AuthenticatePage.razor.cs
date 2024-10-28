﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Malarkey.Application.ProfileImport;
using Malarkey.Domain.ProfileImport;

namespace Malarkey.UI.Pages.Authenticate;

public partial class AuthenticatePage
{
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    public IProfileImporter<MicrosoftImportProfile> ProfileImporter { get; set; }


    private string? _profilePhoto;
    private IReadOnlyCollection<string>? _photos;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        if (authState != null && authState.User?.Identity != null && authState.User.Identity.IsAuthenticated)
        {
            var user = authState.User;
            var identity = user.Identity;
            var profile = await ProfileImporter.LoadForImport();
        }

    }


}
