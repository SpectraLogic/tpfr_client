﻿/*
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
using System.IO;
using System.Net;

namespace TpfrClient.Runtime
{
    public sealed class TpfrHttpWebResponse : IHttpWebResponse
    {
        private readonly HttpWebResponse _httpWebResponse;

        internal TpfrHttpWebResponse(HttpWebResponse httpWebResponse)
        {
            _httpWebResponse = httpWebResponse;
        }
        public Stream GetResponseStream()
        {
            return _httpWebResponse.GetResponseStream();
        }

        public HttpStatusCode StatusCode => _httpWebResponse.StatusCode;

        public void Dispose()
        {
            Dispose(true);
            _httpWebResponse.Close();
            GC.SuppressFinalize(this);
        }

        private static void Dispose(bool disposing)
        {
        }
    }
}
