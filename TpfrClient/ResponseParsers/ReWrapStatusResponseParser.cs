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

using System.Net;
using TpfrClient.Model;
using TpfrClient.Runtime;

namespace TpfrClient.ResponseParsers
{
    public class ReWrapStatusResponseParser : IResponseParser<ReWrapStatus>
    {
        public ReWrapStatus Parse(IHttpWebResponse response)
        {
            using (response)
            {
                ResponseParseUtils.HandleStatusCode(response, (HttpStatusCode)200);
                using (var stream = response.GetResponseStream())
                {
                    var element = XmlExtensions.ReadDocument(stream).ElementOrThrow("partialfilestatus");

                    return new ReWrapStatus
                    {
                        Phase = ResponseParseUtils.GetPhaseResult(element.AttributeTextOrNull("phase")),
                        Percentcomplete = element.AttributeTextOrNull("percentcomplete"),
                        Error = element.AttributeTextOrNull("error"),
                    };
                }
            }
        }
    }
}
