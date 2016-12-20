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

namespace TpfrClient.Calls
{
    public class ReWrapStatusRequest : RestRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputFileName">The out_filename value passed in the Partial File Request. This is Case Sensitive</param>
        public ReWrapStatusRequest(string outputFileName)
        {
            AddQueryParam("targetpartialname", outputFileName);
        }

        internal override HttpVerb Verb => HttpVerb.GET;
        internal override string Path => "partialfilestatus";
    }
}