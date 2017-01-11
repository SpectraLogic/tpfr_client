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
using TpfrClient;
using TpfrClient.Calls;
using TpfrClient.Model;

namespace TpfrExamples
{
    public static class FileStatusExample
    {
        public static void Main()
        {
            const string hostName = "hostName";
            const int port = 60792;
            const string proxy = ""; //you can provide a proxy

            // Create the client using the host name and the port where the Indexer 
            ITpfrClient client = new TpfrClient.TpfrClient(hostName, port)
                .WithProxy(proxy);

            var status = client.FileStatus(new FileStatusRequest(@"\\Media Folder\filename.ext"));
            switch (status.IndexResult)
            {
                case IndexResult.Succeeded:
                {
                    Console.WriteLine(
                        $"{status.IndexResult}, {status.IndexTime}, {status.FileStartTc}, {status.FileDuration}, {status.FileFrameRate}");
                    break;
                }
                case IndexResult.Failed:
                case IndexResult.ErrorFileNotFound:
                case IndexResult.NotIndexed:
                case IndexResult.Indexing:
                    Console.WriteLine($"{status.IndexResult}");
                    break;
                case IndexResult.Unknown:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}