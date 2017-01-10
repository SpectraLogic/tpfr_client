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

using System.Configuration;
using NUnit.Framework;
using TpfrClient;
using TpfrClient.Calls;
using TpfrClient.Model;

namespace TpfrClientIntegrationTest
{
    [TestFixture]
    public class TpfrClientIntegrationTest
    {
        [SetUp]
        public void Setup()
        {
            _client = new TpfrClient.TpfrClient(
                    ConfigurationManager.AppSettings["HostName"],
                    int.Parse(ConfigurationManager.AppSettings["Port"]))
                .WithProxy(ConfigurationManager.AppSettings["Proxy"]);

            _path = ConfigurationManager.AppSettings["Path"];
        }

        private ITpfrClient _client;
        private string _path;

        [Test]
        public void TestErrorFileNotFoundFileStatus()
        {
            var status = _client.FileStatus(new FileStatusRequest($"{_path}not_found.mov"));
            Assert.AreEqual(IndexResult.ErrorFileNotFound, status.IndexResult);
        }

        [Test]
        public void TestErrorReWrapStatus()
        {
            var reWrapStatus = _client.ReWrapStatus(new ReWrapStatusRequest("JobNotFound"));
            Assert.AreEqual(null, reWrapStatus.Phase);
            Assert.AreEqual("Job not found", reWrapStatus.Error);
        }

        [Test]
        public void TestFailedFileIndex()
        {
            var status = _client.IndexFile(new IndexFileRequest($"{_path}error.mov"));
            Assert.AreEqual(IndexResult.Failed, status.IndexResult);
            Assert.AreEqual("-2132778994", status.ErrorCode);
            Assert.AreEqual(@"Failed to parse MOV file [\\ISV_RETROSPECT1\Share\error.mov] Error [Source could not be opened.]", status.ErrorMessage);
        }

        [Test]
        public void TestNotIndexedFileStatus()
        {
            var status = _client.FileStatus(new FileStatusRequest($"{_path}not_indexed.mov"));
            Assert.AreEqual(IndexResult.NotIndexed, status.IndexResult);
        }

        [Test]
        public void TestOkFileIndex()
        {
            var status = _client.IndexFile(new IndexFileRequest($"{_path}sample.mov"));
            Assert.AreEqual(IndexResult.Succeeded, status.IndexResult);
        }

        [Test]
        public void TestOkFileStatus()
        {
            var status = _client.FileStatus(new FileStatusRequest($"{_path}sample.mov"));
            Assert.AreEqual(IndexResult.Succeeded, status.IndexResult);
        }

        [Test]
        public void TestQuestionTimecode()
        {
            var firstFrame = new TimeCode("00:00:00:00");
            var lastFrame = new TimeCode("00:00:10:00");
            var response =
                _client.QuestionTimecode(new QuestionTimecodeRequest($"{_path}sample.mov", firstFrame, lastFrame,
                    "29.97"));
            Assert.AreEqual(OffsetsResult.Succeeded, response.OffsetsResult);
            Assert.AreEqual("0x0", response.InBytes);
            Assert.AreEqual("0x3647974", response.OutBytes);
        }

        [Test]
        public void TestQuestionTimecodeFileNotFound()
        {
            var firstFrame = new TimeCode("00:00:00:00");
            var lastFrame = new TimeCode("00:00:10:00");
            var response =
                _client.QuestionTimecode(new QuestionTimecodeRequest($"{_path}not_found.mov", firstFrame, lastFrame,
                    "29.97"));
            Assert.AreEqual(OffsetsResult.ErrorFileNotFound, response.OffsetsResult);
        }

        [Test]
        public void TestReWrap()
        {
            var firstFrame = new TimeCode("01:00:00;00");
            var lastFrame = new TimeCode("01:00:10;00");
            var response = _client.ReWrap(new ReWrapRequest($"{_path}sample.mov", firstFrame, lastFrame, "29.97",
                $"{_path}sample_10sec.mov", "sampleRestore"));
            Assert.AreEqual(ReWrapResult.Succeeded, response.Result);
        }

        [Test]
        public void TestReWrapWithBadRestoreFile()
        {
            var firstFrame = new TimeCode("00:00:00;00");
            var lastFrame = new TimeCode("00:00:10;00");
            var response = _client.ReWrap(new ReWrapRequest($"{_path}sample.mov", firstFrame, lastFrame, "29.97",
                $"{_path}Sample_10sec.mov", "errorSampleRestore"));
            Assert.AreEqual(ReWrapResult.Succeeded, response.Result);
        }

        [Test]
        public void TestReWrapErrorBadFramerate()
        {
            var firstFrame = new TimeCode("00:00:00:00");
            var lastFrame = new TimeCode("00:00:10:00");
            var response =
                _client.ReWrap(new ReWrapRequest($"{_path}sample.mov", firstFrame, lastFrame, "0",
                    $"{_path}sample_10sec.mov", "sampleRestore"));
            Assert.AreEqual(ReWrapResult.ErrorBadFramerate, response.Result);
        }

        [Test]
        public void TestReWrapStatus()
        {
            var reWrapStatus = _client.ReWrapStatus(new ReWrapStatusRequest("sampleRestore"));
            Assert.AreEqual(Phase.Complete, reWrapStatus.Phase);
        }

        [Test]
        public void TestReWrapStatusError()
        {
            var reWrapStatus = _client.ReWrapStatus(new ReWrapStatusRequest("errorSampleRestore"));
            Assert.AreEqual(Phase.Failed, reWrapStatus.Phase);
            Assert.AreEqual("0", reWrapStatus.Percentcomplete);
            Assert.AreEqual("Requested subclip out of bounds.", reWrapStatus.ErrorMessage);
        }

        [Test]
        public void TestReWrapStatusJobNotFound()
        {
            var reWrapStatus = _client.ReWrapStatus(new ReWrapStatusRequest("notFound"));
            Assert.AreEqual(null, reWrapStatus.Phase);
            Assert.AreEqual("Job not found", reWrapStatus.Error);
        }
    }
}