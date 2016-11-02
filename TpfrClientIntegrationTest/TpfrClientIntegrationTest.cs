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
using TpfrClient.Model;

namespace TpfrClientIntegrationTest
{
    [TestFixture]
    public class TpfrClientIntegrationTest
    {
        private ITpfrClient _client;
        [SetUp]
        public void Setup()
        {
            _client = new TpfrClient.TpfrClient("HostServerName", 60792).WithProxy(new Uri("http://localhost:8888"));
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
            var firstFrame = new TimeCode("00:00:10:00");
            var lastFrame = new TimeCode("00:05:00:00");
            _client.QuestionTimecode(new QuestionTimecodeRequest(@"C:\Media\SampleFile.mxf", firstFrame, lastFrame, "25"));
        }

        [Test]
        public void TestReWrap()
        {
            var firstFrame = new TimeCode("00:00:10:00");
            var lastFrame = new TimeCode("00:05:00:00");
            _client.ReWrap(new ReWrapRequest(@"C:\Media\SampleFile.mxf", firstFrame, lastFrame, "25", "0x0060000", "0x0080000", @"C:\Media\PartialSampleFile.mfx", "PartSampleFile"));
        }

        [Test]
        public void TestReWrapStatus()
        {
            _client.ReWrapStatus(new ReWrapStatusRequest("PartSampleFile"));
        }
    }
}
