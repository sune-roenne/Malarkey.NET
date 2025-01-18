﻿using Malarkey.Abstractions.Profile;
using Microsoft.AspNetCore.Components;

namespace Malarkey.UI.Pages.Profile;

public partial class ProfileIdentityProviderComponent
{

    [Parameter]
    public ProfileIdentityProviderEntry ProviderEntry { get; set; }

    [Parameter]
    public Action<MalarkeyIdentityProvider> OnAddIdentityClicked { get; set; }

    private MalarkeyIdentityProvider Provider => ProviderEntry.Provider;

    private IReadOnlyCollection<ShowIdentity> _identities = [];

    private string ImageFile => $"images/profile/{Provider.ToString().ToLower()}.webp";

    protected override void OnParametersSet()
    {
        _identities = ProviderEntry.Identities
            .Select(_ => new ShowIdentity(_))
            .OrderBy(_ => _.NameToUse)
            .ToList();
    }


    private record ShowIdentity(MalarkeyProfileIdentity Identity)
    {
        private const int MaxCharsPerLine = 10;

        private IReadOnlyCollection<string>? _lines;
        public IReadOnlyCollection<string> Lines => _lines ??= SplitToLines();

        public string NameToUse = "";

        private IReadOnlyCollection<string> SplitToLines()
        {
            NameToUse = Identity.EmailToUse ??
                Identity.PreferredNameToUse ??
                Identity.FirstName;
            var splitted = new List<string> { NameToUse };
            foreach(var splitter in new string[] { "@", "_" })
            {
                var nextRound = new List<string>();
                foreach(var ent in splitted)
                {
                    if(ent.Length < MaxCharsPerLine || !ent.Contains(splitter))
                        nextRound.Add(ent);
                    else
                    {
                        var stringSplitted = ent.Split(splitter);
                        for(int i = 0; i< stringSplitted.Length; i++)
                            nextRound.Add(stringSplitted[i] + (i == stringSplitted.Length - 1 ? "" : splitter));
                    }
                }
                splitted = nextRound;
            }
            return splitted;
        }
    }

}
