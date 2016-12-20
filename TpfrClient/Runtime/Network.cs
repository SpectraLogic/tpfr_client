/*
 * ******************************************************************************
 *   Copyright 2014 - 2016 Spectra Logic Corporation. All Rights Reserved.
 *   Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *   this file except in compliance with the License. A copy of the License is located at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 *   or in the "license" file accompanying this file.
 *   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *   CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *   specific language governing permissions and limitations under the License.
 * ****************************************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TpfrClient.Calls;

namespace TpfrClient.Runtime
{
    public class Network : INetwork
    {
        public Network(string hostServerName, int hostServerPort)
        {
            HostServerName = hostServerName;
            HostServerPort = hostServerPort;
        }

        private string HostServerName { get; }
        private int HostServerPort { get; }
        private Uri Proxy { get; set; }

        public INetwork WithProxy(Uri proxy)
        {
            Proxy = proxy;
            return this;
        }

        public IHttpWebResponse Invoke(RestRequest request)
        {
            var httpWebRequest = CreateHttpWebRequest(request);

            try
            {
                return httpWebRequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw;
                }
                return new TpfrHttpWebResponse((HttpWebResponse) e.Response);
            }
        }

        private IHttpWebRequest CreateHttpWebRequest(RestRequest request)
        {
            var uriBuilder = new UriBuilder(HostServerName)
            {
                Path = Uri.EscapeDataString(request.Path),
                Query = BuildQueryParams(request.QueryParams),
                Port = HostServerPort
            };


            var httpRequest = (HttpWebRequest) WebRequest.Create(uriBuilder.Uri);
            httpRequest.Method = request.Verb.ToString();

            if (Proxy != null)
            {
                var webProxy = new WebProxy
                {
                    Address = Proxy
                };
                httpRequest.Proxy = webProxy;
            }

            httpRequest.Date = DateTime.UtcNow;
            httpRequest.Host = CreateHostString();
            httpRequest.UserAgent = "Spectra Tpfr";

            return new TpfrHttpWebRequest(httpRequest);
        }

        private string CreateHostString()
        {
            return $"{HostServerName}:{HostServerPort}";
        }

        private static string BuildQueryParams(Dictionary<string, string> queryParams)
        {
            return string.Join(
                "&",
                from kvp in queryParams
                orderby kvp.Key
                let encodedKey = Uri.EscapeDataString(kvp.Key)
                select !string.IsNullOrEmpty(kvp.Value)
                    ? encodedKey + "=" + Uri.EscapeDataString(kvp.Value)
                    : encodedKey
            );
        }
    }
}