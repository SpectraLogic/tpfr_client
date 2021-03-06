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
using System.Collections.Generic;

namespace TpfrClient.Calls
{
    public abstract class RestRequest
    {
        internal abstract HttpVerb Verb { get; }

        internal abstract string Path { get; }

        internal Dictionary<string, string> QueryParams { get; } = new Dictionary<string, string>();

        protected void AddQueryParam(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                QueryParams.Add(key, value);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }

    internal enum HttpVerb
    {
        GET,
        PUT
    };
}