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

using TpfrClient.Model;

namespace TpfrClient.Calls
{
    public class ReWrapRequest : RestRequest
    {
        public ReWrapRequest(string filePath, TimeCode firstFrame, TimeCode lastFrame, string frameRate, string inByte,
            string outByte, string partialRestoreFilePath, string outputFileName)
        {
            AddQueryParam("filepath", filePath);
            AddQueryParam("tcin", firstFrame.Time);
            AddQueryParam("tcout", lastFrame.Time);
            AddQueryParam("fileframerate", frameRate);
            AddQueryParam("in_byte", inByte);
            AddQueryParam("out_byte", outByte);
            AddQueryParam("part_file", partialRestoreFilePath);
            AddQueryParam("out_fileName", outputFileName);
        }

        internal override HttpVerb Verb => HttpVerb.PUT;
        internal override string Path => "partialfile";
    }
}