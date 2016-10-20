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

namespace TpfrClientTest
{
    [TestFixture]
    public class TpfrClientTest
    {
        private IPluginSdk _pluginSdk;

        [SetUp]
        public void Setup()
        {
            _pluginSdk = new PluginSdk(new MockNetwork());
        }

        [Test]
        public void TestIndexFile()
        {
            _pluginSdk.IndexFile(null);
        }

        [Test]
        public void TestIndexStatus()
        {
            _pluginSdk.IndexStatus(null);
        }

        [Test]
        public void TestQuestionTimecode()
        {
            _pluginSdk.QuestionTimecode(null, null, null);
        }

        [Test]
        public void TestReWrap()
        {
            _pluginSdk.ReWrap(null, null, null);
        }
    }
}
