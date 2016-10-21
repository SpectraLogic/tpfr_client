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

using System.Collections.Generic;
using System.IO;

namespace TpfrClient.Requests
{
    public abstract class RestRequest
    {
        internal abstract HttpVerb Verb
        {
            get;
        }

        internal abstract string Path
        {
            get;
        }

        internal virtual long GetContentLength()
        {
            return 0;
        }

        internal virtual Stream GetContentStream()
        {
            return Stream.Null;
        }

        internal virtual Dictionary<string, string> QueryParams { get; } = new Dictionary<string, string>();

        public string GetDescription(string paramstring)
        {
            return $"{this.Verb} {this.Path}{(string.IsNullOrEmpty(paramstring) ? "" : "?")}{(string.IsNullOrEmpty(paramstring) ? "" : paramstring)}";
        }
    }

    internal enum HttpVerb { GET, PUT, POST, DELETE, HEAD, PATCH };
}
