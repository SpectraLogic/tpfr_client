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

            Assert.AreEqual(Result.Failed, status.IndexResult);
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

            Assert.AreEqual(Result.Succeeded, status.IndexResult);
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
            var status = client.IndexStatus(new IndexStatusRequest("filePath"));

            Assert.AreEqual(Result.Failed, status.IndexResult);
            Assert.AreEqual("2011/10/21 15:30:15", status.IndexTime);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestSuccessfulIndexStatus()
        {
            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.SuccessfulIndexFileOrFileStatusCall.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.IndexStatus(new IndexStatusRequest("filePath"));

            Assert.AreEqual(Result.Succeeded, status.IndexResult);
            Assert.AreEqual("2011/10/21 11:40:53", status.IndexTime);
            Assert.AreEqual("01:00:00;00", status.FileStartTc);
            Assert.AreEqual("1800", status.FileDuration);
            Assert.AreEqual("29.97", status.FileFrameRate);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestFileNotFoundIndexStatus()
        {
            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.FileStatusWhenFileNotPresent.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.IndexStatus(new IndexStatusRequest("filePath"));

            Assert.AreEqual(Result.ErrorFileNotFound, status.IndexResult);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestFileNotIndexedIndexStatus()
        {
            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(It.IsAny<RestRequest>()))
                .Returns(new MockHttpWebResponse("TpfrClientTest.TestFiles.FileStatusWhenFileNotIndexed.xml", HttpStatusCode.OK));

            var client = new TpfrClient.TpfrClient(mockNetwork.Object);
            var status = client.IndexStatus(new IndexStatusRequest("filePath"));

            Assert.AreEqual(Result.NotIndexed, status.IndexResult);

            mockNetwork.VerifyAll();
        }

        [Test]
        public void TestQuestionTimecode()
        {
            //client.QuestionTimecode(new QuestionTimecodeRequest(null, null, null));
        }

        [Test]
        public void TestReWrap()
        {
            //client.ReWrap(new ReWrapRequest(null, null, null));
        }
    }
}