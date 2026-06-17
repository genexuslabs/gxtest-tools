// Copyright 2021 GeneXus S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation
{
    internal class GenerationState
    {
        private static GenerationState state;

        public static GenerationState State
        {
            get
            {
                if (state == null)
                    state = new GenerationState();

                return state;
            }
        }

        public static void Reset() => state = null;

        public bool OnPrompt { get; set; }
    }
}
