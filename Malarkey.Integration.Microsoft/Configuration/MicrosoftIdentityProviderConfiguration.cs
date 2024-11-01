﻿using Malarkey.Integration.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malarkey.Integration.Microsoft.Configuration;
public class MicrosoftIdentityProviderConfiguration : IdentityProviderConfiguration
{
    public string Instance { get; set; }
    public string TenantId { get; set; }
    public string[] ClientCapabilities { get; set; }
    public MicrsoftClientCertificateConfiguration[] ClientCertificates { get; set; }



}