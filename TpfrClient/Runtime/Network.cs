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
using TpfrClient.Requests;

namespace TpfrClient
{
    public class Network : INetwork
    {
        private const int ReadWriteTimeout = 60 * 60 * 1000;
        private const int RequestTimeout = 60 * 60 * 1000;
        private const int ConnectionLimit = 12;

        public Network(string hostServerName, int hostServerPort)
        {
            HostServerName = hostServerName;
            HostServerPort = hostServerPort;
        }

        public string HostServerName { get; private set; }
        public int HostServerPort { get; private set; }
        public Uri Proxy { get; private set; }

        public INetwork WithProxy(Uri proxy)
        {
            Proxy = proxy;
            return this;
        }

        public HttpWebResponse Invoke(RestRequest request)
        {
            var httpWebRequest = CreateHttpWebRequest(request);
            return (HttpWebResponse) httpWebRequest.GetResponse();
        }

        private HttpWebRequest CreateHttpWebRequest(RestRequest request)
        {
            var uriBuilder = new UriBuilder(HostServerName)
            {
                Path = Uri.EscapeDataString(request.Path),
                Query = BuildQueryParams(request.QueryParams),
                Port = HostServerPort
            };


            var httpRequest = (HttpWebRequest) WebRequest.Create(uriBuilder.ToString());
            httpRequest.ServicePoint.ConnectionLimit = ConnectionLimit;
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
            httpRequest.ReadWriteTimeout = ReadWriteTimeout;
            httpRequest.Timeout = RequestTimeout;

            return httpRequest;
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