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
    public class QuestionTimecodeRequest : RestRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">The full path (via a mapped drive) to the media file whose partial offsets are being requested.</param>
        /// <param name="firstFrame">Timecode of the first frame requested</param>
        /// <param name="lastFrame">Timecode of the last frame requested</param>
        /// <param name="frameRate">Frame rate, as returned in the file status report</param>
        public QuestionTimecodeRequest(string filePath, TimeCode firstFrame, TimeCode lastFrame, string frameRate)
        {
            AddQueryParam("filepath", filePath);
            AddQueryParam("tcin", firstFrame.Time);
            AddQueryParam("tcout", lastFrame.Time);
            AddQueryParam("fileframerate", frameRate);
        }

        internal override HttpVerb Verb => HttpVerb.GET;
        internal override string Path => "fileoffsets";
    }
}