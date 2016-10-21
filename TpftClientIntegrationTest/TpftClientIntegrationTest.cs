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
using NUnit.Framework;
using TpfrClient;
using TpfrClient.Calls;

namespace TpftClientIntegrationTest
{
    [TestFixture]
    public class TpftClientIntegrationTest
    {
        private ITpftClient _client;
        [SetUp]
        public void Setup()
        {
            _client = new TpftClient("HostServerName", 60792).WithProxy(new Uri("http://localhost:8888"));
        }

        [Test]
        public void TestFileIndex()
        {
            _client.IndexFile(new IndexFileRequest(@"C:\Media\SampleFile.mxf"));
        }

        [Test]
        public void TestFileStatus()
        {
            _client.IndexStatus(new IndexStatusRequest(@"C:\Media\SampleFile.mxf"));
        }

        [Test]
        public void TestQuestionTimecode()
        {
            _client.QuestionTimecode(new QuestionTimecodeRequest("", "", new List<TimecodeRange>()));
        }

        [Test]
        public void TestReWrap()
        {
            _client.ReWrap(new ReWrapRequest("", "", new List<TimecodeRange>()));
        }
    }
}
