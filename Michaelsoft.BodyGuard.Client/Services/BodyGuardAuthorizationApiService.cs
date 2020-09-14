﻿using System;
using System.Net.Http;
using System.Text;
using Michaelsoft.BodyGuard.Client.Settings;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Client.Services
{
    public class BodyGuardAuthorizationApiService : BodyGuardBaseService
    {

        public BodyGuardAuthorizationApiService(IBodyGuardClientSettings settings,
                                                IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            BasePath = $"{settings.BasePath}/Authorization/";
        }

    }
}