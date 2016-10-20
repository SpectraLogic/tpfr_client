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

namespace TpfrClient
{
    public interface IPluginSdk
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Status IndexFile(string filePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Status IndexStatus(string filePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="indexFilePath"></param>
        /// <param name="timecodes"></param>
        /// <returns></returns>
        IEnumerable<ByteRange> QuestionTimecode(string clipName, string indexFilePath, IEnumerable<TimecodeRange> timecodes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileToProcessPath"></param>
        /// <param name="indexFilePath"></param>
        /// <param name="timecodes"></param>
        void ReWrap(string fileToProcessPath, string indexFilePath, IEnumerable<TimecodeRange> timecodes);
    }
}
