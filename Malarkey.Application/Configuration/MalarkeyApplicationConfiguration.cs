﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malarkey.Application.Configuration;
public class MalarkeyApplicationConfiguration
{
    public const string ConfigurationElementName = "Application";
    public MalarkeyCertificateConfiguration SigningCertificate { get; set; }

    public MalarkeyCertificateConfiguration HostingCertificate { get; set; }

}
