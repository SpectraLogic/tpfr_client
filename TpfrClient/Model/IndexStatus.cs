﻿/*
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

namespace TpfrClient.Model
{
    public class IndexStatus
    {
        public IndexResult IndexResult { get; set; }
        public string IndexTime { get; set; }
        public string FileStartTc { get; set; }
        public string FileDuration { get; set; }
        public string FileFrameRate { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

    }

    public enum IndexResult
    {
        Failed,
        Succeeded,
        ErrorFileNotFound,
        NotIndexed,
        Indexing,
        Unknown
    }
}
