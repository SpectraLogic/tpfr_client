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
using TpfrClient.Model;

namespace TpfrClient.Calls
{
    public class QuestionTimecodeRequest : RestRequest
    {
        public QuestionTimecodeRequest(string filePath, TimeCode firstFrame, TimeCode lastFrame, string frameRate)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                QueryParams.Add("filepath", filePath);
            }
            else
            {
                throw new ArgumentNullException(filePath);
            }

            QueryParams.Add("tcin", firstFrame.Time);
            QueryParams.Add("tcout", lastFrame.Time);

            if (!string.IsNullOrWhiteSpace(frameRate))
            {
                QueryParams.Add("fileframerate", frameRate);
            }
            else
            {
                throw new ArgumentNullException(frameRate);
            }
        }

        internal override HttpVerb Verb => HttpVerb.GET;
        internal override string Path => "fileoffsets";
    }
}