/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System.Threading.Tasks;
using CurseIO;
using CurseIO.Enum;
using Amplifir.Core.Interfaces;

namespace Amplifir.Core.DomainServices
{
    public class BadWordsService : IBadWordsService
    {
        public async Task<string> CleanAsync(string content)
        {
            Curse curseExtractor = new Curse();

            for (int i = 1; i <= 5; ++i)
            {
                switch (i)
                {
                    case 1:
                        curseExtractor.SetLanguage( Language.English );
                        break;

                    case 2:
                        curseExtractor.SetLanguage( Language.French );
                        break;

                    case 3:
                        curseExtractor.SetLanguage( Language.German );
                        break;

                    case 4:
                        curseExtractor.SetLanguage( Language.Italian );
                        break;

                    case 5:
                        curseExtractor.SetLanguage( Language.Spanish );
                        break;
                }

                content = await curseExtractor.CleanseAsync( content );
            }

            return content;
        }
    }
}
