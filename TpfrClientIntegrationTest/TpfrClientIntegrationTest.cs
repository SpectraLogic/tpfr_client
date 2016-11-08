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
using System.Configuration;
using System.Net;
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
        private string _path;

        [SetUp]
        public void Setup()
        {
            _client = new TpfrClient.TpfrClient(
                ConfigurationManager.AppSettings["HostName"],
                int.Parse(ConfigurationManager.AppSettings["Port"]))
                .WithProxy(ConfigurationManager.AppSettings["Proxy"]);

            _path = ConfigurationManager.AppSettings["Path"];
        }

        [Test]
        public void TestOkFileIndex()
        {
            var status = _client.IndexFile(new IndexFileRequest($"{_path}ok.mov"));
            Assert.AreEqual(IndexResult.Succeeded, status.IndexResult);
        }

        [Test]
        public void TestFailedFileIndex()
        {
            var status = _client.IndexFile(new IndexFileRequest($"{_path}error_file.xmf"));
            Assert.AreEqual(IndexResult.Failed, status.IndexResult);
        }

        [Test]
        public void TestOkFileStatus()
        {
            var status = _client.FileStatus(new FileStatusRequest($"{_path}ok.mov"));
            Assert.AreEqual(IndexResult.Succeeded, status.IndexResult);
        }

        [Test]
        public void TestErrorFileNotFoundFileStatus()
        {
            var status = _client.FileStatus(new FileStatusRequest($"{_path}not_found.mov"));
            Assert.AreEqual(IndexResult.ErrorFileNotFound, status.IndexResult);
        }

        [Test]
        public void TestQuestionTimecode()
        {
            Assert.Ignore();

            var firstFrame = new TimeCode("00:00:00:00");
            var lastFrame = new TimeCode("00:00:10:00");
            _client.QuestionTimecode(new QuestionTimecodeRequest(@"C:\Users\sharons\Videos\tpft\SampleFile.mov", firstFrame, lastFrame, "30"));
        }

        [Test]
        public void TestReWrap()
        {
            Assert.Ignore();

            var firstFrame = new TimeCode("00:00:10:00");
            var lastFrame = new TimeCode("00:05:00:00");
            _client.ReWrap(new ReWrapRequest(@"C:\Media\SampleFile.mxf", firstFrame, lastFrame, "25", "0x0060000", "0x0080000", @"C:\Media\PartialSampleFile.mfx", "PartSampleFile"));
        }

        [Test]
        public void TestReWrapStatus()
        {
            Assert.Ignore();

            _client.ReWrapStatus(new ReWrapStatusRequest("PartSampleFile"));
        }
    }
}
