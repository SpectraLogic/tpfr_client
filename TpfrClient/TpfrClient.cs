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

using System;
using TpfrClient.Calls;
using TpfrClient.Model;
using TpfrClient.ResponseParsers;
using TpfrClient.Runtime;

namespace TpfrClient
{
    public class TpfrClient : ITpfrClient
    {
        private readonly INetwork _network;

        public TpfrClient(string hostServerName, int hostServerPort)
        {
            _network = new Network(hostServerName, hostServerPort);
        }

        public TpfrClient(INetwork network)
        {
            _network = network;
        }

        /// <summary>
        /// This method will block while the index is created and will only return when either the index file has been created or for some reason it has not been possible to create the index file.
        /// On HFS systems that leave stub files on disk, such as StorNext, if a request is made to create an index for a media file which has been truncated by StorNext, this call will cause the entire media file to be restored by StorNext.
        /// The Web Service will support multiple concurrent calls to this command.
        /// </summary>
        /// <param name="request"><seealso cref="IndexFileRequest" /></param>
        /// <returns></returns>
        public IndexStatus IndexFile(IndexFileRequest request)
        {
            return new IndexFileResponseParser().Parse(_network.Invoke(request));
        }

        /// <summary>
        /// This method will block while retrieving the index status for a previously indexed file.
        /// This method internally uses an XML file, generated by the indexer to retrieve the detailed status.This XML file is only generated by Quantum PFR version 1.1 and later.For files that were indexed in an earlier version of Quantum PFR, minimal information will be retrieved (just whether the file had been indexed or not).
        /// The Web Service will support multiple concurrent calls to this API call.
        /// </summary>
        /// <param name="request"><seealso cref="FileStatusRequest" /></param>
        /// <returns></returns>
        public IndexStatus FileStatus(FileStatusRequest request)
        {
            return new FileStatusResponseParser().Parse(_network.Invoke(request));
        }

        /// <summary>
        /// This method will block whilst retrieving the start and end byte offsets for the requested timecodes. The offsets are extended in order to handle GOP and interleave ordering.
        /// Timecode format should be in form hh:mm:ss:ff for non-drop framerates and hh:mm:ss;ff for drop framerates
        /// </summary>
        /// <param name="request"><seealso cref="QuestionTimecodeRequest" /></param>
        /// <returns></returns>
        public OffsetsStatus QuestionTimecode(QuestionTimecodeRequest request)
        {
            return new QuestionTimecodeResponseParser().Parse(_network.Invoke(request));
        }

        /// <summary>
        /// This method will use the parameters supplied to generate a Marquis XML file that will be used to create the partial output file.
        /// </summary>
        /// <param name="request"><seealso cref="ReWrapRequest" /></param>
        /// <returns></returns>
        public ReWrapResponse ReWrap(ReWrapRequest request)
        {
            return new ReWrapResponseParser().Parse(_network.Invoke(request));
        }

        /// <summary>
        /// This method will return status (% complete) for the creation of a partial media file initiated using the Partial File Request API call.
        /// </summary>
        /// <param name="request"><seealso cref="ReWrapStatusRequest" /></param>
        /// <returns></returns>
        public ReWrapStatus ReWrapStatus(ReWrapStatusRequest request)
        {
            return new ReWrapStatusResponseParser().Parse(_network.Invoke(request));
        }

        public TpfrClient WithProxy(string proxy)
        {
            return !string.IsNullOrEmpty(proxy) ? WithProxy(new Uri(proxy)) : this;
        }

        private TpfrClient WithProxy(Uri proxy)
        {
            _network.WithProxy(proxy);
            return this;
        }
    }
}