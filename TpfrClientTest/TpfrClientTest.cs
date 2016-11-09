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
using System.Net;
using Moq;
using NUnit.Framework;
using TpfrClient.Calls;
using TpfrClient.Model;
using TpfrClient.Runtime;
using TpfrClientTest.Mock;

namespace TpfrClientTest
{
    [TestFixture]
    public class TpfrClientTest
    {
        [Test]
        public void TestFailedIndexFile()
        {
            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.FailedToIndex.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.IndexFile(new IndexFileRequest("filePath"));

            Assert.AreEqual(IndexResult.Failed, status.IndexResult);
            Assert.AreEqual("2011/10/21 15:30:15", status.IndexTime);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestSuccessfulIndexFile()
        {
            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.SuccessfulIndexFileOrFileStatusCall.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.IndexFile(new IndexFileRequest("filePath"));

            Assert.AreEqual(IndexResult.Succeeded, status.IndexResult);
            Assert.AreEqual("2011/10/21 11:40:53", status.IndexTime);
            Assert.AreEqual("01:00:00;00", status.FileStartTc);
            Assert.AreEqual("1800", status.FileDuration);
            Assert.AreEqual("29.97", status.FileFrameRate);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestFailedIndexStatus()
        {
            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.FailedToIndex.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.FileStatus(new FileStatusRequest("filePath"));

            Assert.AreEqual(IndexResult.Failed, status.IndexResult);
            Assert.AreEqual("2011/10/21 15:30:15", status.IndexTime);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestSuccessfulFileStatus()
        {
            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.SuccessfulIndexFileOrFileStatusCall.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.FileStatus(new FileStatusRequest("filePath"));

            Assert.AreEqual(IndexResult.Succeeded, status.IndexResult);
            Assert.AreEqual("2011/10/21 11:40:53", status.IndexTime);
            Assert.AreEqual("01:00:00;00", status.FileStartTc);
            Assert.AreEqual("1800", status.FileDuration);
            Assert.AreEqual("29.97", status.FileFrameRate);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestFileNotFoundFileStatus()
        {
            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.FileStatusWhenFileNotPresent.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.FileStatus(new FileStatusRequest("filePath"));

            Assert.AreEqual(IndexResult.ErrorFileNotFound, status.IndexResult);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestFileNotIndexedFileStatus()
        {
            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.FileStatusWhenFileNotIndexed.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.FileStatus(new FileStatusRequest("filePath"));

            Assert.AreEqual(IndexResult.NotIndexed, status.IndexResult);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestSucceededQuestionTimecode()
        {
            Assert.Ignore("Not Implemented");

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.GoodFileOffsetsCall.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.QuestionTimecode(
                new QuestionTimecodeRequest(
                    "filePath", new TimeCode("00:00:00:00"), new TimeCode("00:00:00:00"), "00"));

            Assert.AreEqual(OffsetsResult.Succeeded, status.OffsetsResult);
            Assert.AreEqual("0x0060000", status.InBytes);
            Assert.AreEqual("0x0080000", status.OutBytes);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestFileNotFoundQuestionTimecode()
        {
            Assert.Ignore("Not Implemented");

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.FileNotFoundOffsetsCall.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.QuestionTimecode(
                new QuestionTimecodeRequest(
                    "filePath", new TimeCode("00:00:00:00"), new TimeCode("00:00:00:00"), "00"));

            Assert.AreEqual(OffsetsResult.ErrorFileNotFound, status.OffsetsResult);

            mockNetwork.VerifyAll();
        }

        private static readonly object[] GoodTimeCodes = 
        {
            new object[] {"00:00:00:00"},
            new object[] {"00:00:00;00"},
            new object[] {"12:34:56:78"},
            new object[] {"12:34:56;78"}
        };

        [Test, TestCaseSource(nameof(GoodTimeCodes))]
        public void TestGoodTimeCodeFormat(string timeCode)
        {
            Assert.AreEqual(timeCode, new TimeCode(timeCode).Time);
        }

        private static readonly object[] BadTimeCodes =
        {
            new object[] {"00:00:00.00"},
            new object[] {"00.00.00.00"},
            new object[] {"00"},
            new object[] {"00:00"},
            new object[] {"00:00:00"},
            new object[] {"x0:00:00:00"},
            new object[] {"00:x0:00:00"},
            new object[] {"00:00:x0:00"},
            new object[] {"00:00:00:x0"}
        };

        [Test, TestCaseSource(nameof(BadTimeCodes))]
        public void TestBadTimeCodeFormat(string timeCode)
        {
            Assert.Throws<ArgumentException>(() => new TimeCode(timeCode));
        }

        [Test]
        public void TestReWrap()
        {
            Assert.Ignore("Not Implemented");

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            client.ReWrap(
                new ReWrapRequest(
                    "filePath", new TimeCode("00:00:00:00"), new TimeCode("00:00:00:00"), "00",
                    "0x0060000", "0x0080000", "partialFilePath", "outputFileName"));

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestFailedReWrap()
        {
            Assert.Ignore("Not Implemented");

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("", HttpStatusCode.BadRequest));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            Assert.Throws<Exception>(() => client.ReWrap(
                new ReWrapRequest(
                    "filePath", new TimeCode("00:00:00:00"), new TimeCode("00:00:00:00"), "00",
                    "0x0060000", "0x0080000", "partialFilePath", "outputFileName")));

            mockNetwork.VerifyAll();
        }

        private static readonly object[] ReWrapStatusObjects =
        {
            new object[] {"JobPending.xml", Phase.Pending, "0"},
            new object[] {"JobParsing.xml", Phase.Parsing, "25"},
            new object[] {"JobTransferring.xml", Phase.Transferring, "50"},
            new object[] {"JobComplete.xml", Phase.Complete, "100"},
            new object[] { "JobFailed.xml", Phase.Failed, "0"}
        };

        [Test, TestCaseSource(nameof(ReWrapStatusObjects))]
        public void TesReWrapStatus(string xmlFile, Phase phase, string percentComplete)
        {
            Assert.Ignore("Not Implemented");

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles."+xmlFile, HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.ReWrapStatus(new ReWrapStatusRequest("outputFileName"));

            Assert.AreEqual(phase, status.Phase);
            Assert.AreEqual(percentComplete, status.Percentcomplete);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TesReWrapError()
        {
            Assert.Ignore("Not Implemented");

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.PartialFileStatusError.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.ReWrapStatus(new ReWrapStatusRequest("outputFileName"));

            Assert.AreEqual("Job not found", status.Error);

            mockNetwork.VerifyAll();
        }
    }
}