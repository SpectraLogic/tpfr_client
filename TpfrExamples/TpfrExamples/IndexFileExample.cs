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
using TpfrClient.Calls;
using TpfrClient.Model;

namespace TpfrExamples
{
    public static class IndexFileExample
    {
        public static void Main()
        {
            const string hostName = "hostName";
            const int port = 60792;
            const string proxy = ""; //you can provide a proxy

            // Create the client using the host name and the port where the Indexer 
            var client = new TpfrClient.TpfrClient(hostName, port)
                .WithProxy(proxy);

            /*
             *   The index files are generated in a parallel folder structure on the managed storage.
             *   If the call references a mounted drive, e.g. C:\Media Folder\filename.ext, the index files are created in a folder in the form C:\PFR-INDEX\Media Folder\.
             *   If the call references a UNC path, e.g. \\HostServerName\SharedFolder\Media Folder\filename.ext, the index files will be created in a folder in the form \\HostServerName\SharedFolder\PFR-INDEX\.
             *   The PFR-INDEX folder will then replicate any lower level folder structures found with the source file.
             */
            var status = client.IndexFile(new IndexFileRequest(@"C:\Media Folder\filename.ext"));
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
                    Console.WriteLine($"{status.IndexResult}");
                    break;
                case IndexResult.Unknown:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}