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
using System.IO;
using System.Linq;
using System.Net;
using TpfrClient.Model;
using TpfrClient.Runtime;

namespace TpfrClient.ResponseParsers
{
    internal static class ResponseParseUtils
    {
        public static void HandleStatusCode(IHttpWebResponse response, params HttpStatusCode[] expectedStatusCodes)
        {
            var actualStatusCode = response.StatusCode;
            if (expectedStatusCodes.Contains(actualStatusCode)) return;

            var responseContent = GetResponseContent(response);
            throw new Exception(responseContent);
        }

        private static string GetResponseContent(IHttpWebResponse response)
        {
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        public static IndexResult GetIndexResult(string result)
        {
            switch (result)
            {
                case "Succeeded":
                    return IndexResult.Succeeded;
                case "Failed":
                    return IndexResult.Failed;
                case "Error File Not Found":
                    return IndexResult.ErrorFileNotFound;
                case "Not Indexed":
                    return IndexResult.NotIndexed;
                case "Indexing":
                    return IndexResult.Indexing;
                default:
                    return IndexResult.Unknown;
            }
        }

        public static OffsetsResult GetOffsetsResult(string result)
        {
            switch (result)
            {
                case "Succeeded":
                    return OffsetsResult.Succeeded;
                case "Error File Not Found":
                    return OffsetsResult.ErrorFileNotFound;
                default:
                    return OffsetsResult.Unknown;
            }
        }

        public static Phase? GetPhaseResult(string result)
        {
            if (result == null) return null;

            switch (result)
            {
                case "Pending":
                    return Phase.Pending;
                case "Parsing":
                    return Phase.Parsing;
                case "Transferring":
                    return Phase.Transferring;
                case "Complete":
                    return Phase.Complete;
                case "Failed":
                    return Phase.Failed;
                default:
                    return Phase.Unknown;
            }
        }

        public static ReWrapResult GetReWrapResult(string result)
        {
            switch (result)
            {
                case "Succeeded":
                    return ReWrapResult.Succeeded;
                case "Error Duplicate parameter":
                    return ReWrapResult.ErrorDuplicateParameter;
                case "Error Missing parameter":
                    return ReWrapResult.ErrorMissingParameter;
                case "Error Bad framerate":
                    return ReWrapResult.ErrorBadFramerate;
                default:
                    return ReWrapResult.Unknown;
            }
        }
    }
}