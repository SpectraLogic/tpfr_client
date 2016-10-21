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

using NUnit.Framework;
using TpfrClient;
using TpfrClient.Calls;

namespace TpfrClientTest
{
    [TestFixture]
    public class TpfrClientTest
    {
        [SetUp]
        public void Setup()
        {
            _tpfrClient = new TpfrClient.TpfrClient(new MockNetwork());
        }

        private ITpfrClient _tpfrClient;

        [Test]
        public void TestIndexFile()
        {
            _tpfrClient.IndexFile(new IndexFileRequest(null));
        }

        [Test]
        public void TestIndexStatus()
        {
            _tpfrClient.IndexStatus(new IndexStatusRequest(null));
        }

        [Test]
        public void TestQuestionTimecode()
        {
            _tpfrClient.QuestionTimecode(new QuestionTimecodeRequest(null, null, null));
        }

        [Test]
        public void TestReWrap()
        {
            _tpfrClient.ReWrap(new ReWrapRequest(null, null, null));
        }
    }
}