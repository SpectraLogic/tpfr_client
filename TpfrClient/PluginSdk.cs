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
using System.Collections.Generic;

namespace TpfrClient
{
    public class PluginSdk : IPluginSdk
    {
        private INetwork _network;

        public PluginSdk(string hostServerName, int hostServerPort)
        {
            _network = new Network(hostServerName, hostServerPort);
        }

        public PluginSdk(INetwork network)
        {
            _network = network;
        }

        public Status IndexFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public Status IndexStatus(string filePath)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ByteRange> QuestionTimecode(string clipName, string indexFilePath,
            IEnumerable<TimecodeRange> timecodes)
        {
            throw new NotImplementedException();
        }

        public void ReWrap(string fileToProcessPath, string indexFilePath, IEnumerable<TimecodeRange> timecodes)
        {
            throw new NotImplementedException();
        }
    }
}