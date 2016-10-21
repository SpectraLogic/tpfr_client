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
using TpfrClient.Calls;
using TpfrClient.ResponseParsers;

namespace TpfrClient
{
    public class TpftClient : ITpftClient
    {
        private INetwork _network;

        public TpftClient(string hostServerName, int hostServerPort)
        {
            _network = new Network(hostServerName, hostServerPort);
        }

        public TpftClient(INetwork network)
        {
            _network = network;
        }

        public Status IndexFile(IndexFileRequest request)
        {
            return new IndexFileResponseParser().Parse(_network.Invoke(request));
        }

        public Status IndexStatus(IndexStatusRequest request)
        {
            return new IndexStatusResponseParser().Parse(_network.Invoke(request));
        }

        public IEnumerable<ByteRange> QuestionTimecode(QuestionTimecodeRequest request)
        {
            return new QuestionTimecodeResponseParser().Parse(_network.Invoke(request));
        }

        public void ReWrap(ReWrapRequest request)
        {
            _network.Invoke(request);
        }
    }
}