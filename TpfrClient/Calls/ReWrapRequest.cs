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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">The full path to the media file whose partial offsets are being requested.</param>
        /// <param name="firstFrame">Timecode of the first frame requested</param>
        /// <param name="lastFrame">Timecode of the last frame requested</param>
        /// <param name="frameRate">Frame rate, as returned in the file status report</param>
        /// <param name="inByte">Byte offset of start of partial file relative to original file</param>
        /// <param name="outByte">Byte offset of end of partial file relative to original file</param>
        /// <param name="partialRestoreFilePath">Full UNC path to partial restored file fragment</param>
        /// <param name="outputFileName">output file name for partial media file (care should be taken that this does not clash with other part restores, e.g. from other sections of the same source file)</param>
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