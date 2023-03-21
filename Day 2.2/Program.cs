using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_2._2
{
    /*
     *--- Part Two ---
    The Elf finishes helping with the tent and sneaks back over to you. "Anyway, the second column says how the round needs to end: X means you need to lose, Y means you need to end the round in a draw, 
        and Z means you need to win. Good luck!"

    The total score is still calculated in the same way, but now you need to figure out what shape to choose so the round ends as indicated. The example above now goes like this:

    In the first round, your opponent will choose Rock (A), and you need the round to end in a draw (Y), so you also choose Rock. This gives you a score of 1 + 3 = 4.
    In the second round, your opponent will choose Paper (B), and you choose Rock so you lose (X) with a score of 1 + 0 = 1.
    In the third round, you will defeat your opponent's Scissors with Rock for a score of 1 + 6 = 7.
    Now that you're correctly decrypting the ultra top secret strategy guide, you would get a total score of 12.
    */

    class Program
    {
        static void Main(string[] args)
        {
            var values = ReadValues();

            var results = (from value in values
                select value.Trim().Split(" ")
                into strings
                let enemy = strings.First().ToUpperInvariant().First()
                let action = strings.Last().ToUpperInvariant().First()
                select enemy switch
                {
                    'A' => CaseRock(action),
                    'B' => CasePaper(action),
                    'C' => CaseScissors(action),
                    _ => 0
                }).ToList();

            Console.WriteLine(results.Sum());
        }


        private static int AdditionalBonusForChoice(char me)
        {
            return me switch
            {
                'X' => 1,
                'Y' => 2,
                'Z' => 3,
                _ => 0
            };
        }

        private static int CaseRock(char action)
        {
            var me = action switch
            {
                'X' => 'Z',
                'Y' => 'X',
                'Z' => 'Y',
                _ => ' '
            };
            return me switch
            {
                'X' => 3 + AdditionalBonusForChoice(me),
                'Y' => 6 + AdditionalBonusForChoice(me),
                'Z' => 0 + AdditionalBonusForChoice(me),
                _ => 0
            };
        }

        private static int CasePaper(char me)
        {
            return me switch
            {
                'X' => 0 + AdditionalBonusForChoice(me),
                'Y' => 3 + AdditionalBonusForChoice(me),
                'Z' => 6 + AdditionalBonusForChoice(me),
                _ => 0
            };
        }

        private static int CaseScissors(char action)
        {
            var me = action switch
            {
                'X' => 'Y',
                'Y' => 'Z',
                'Z' => 'X',
                _ => ' '
            };

            return me switch
            {
                'X' => 6 + AdditionalBonusForChoice(me),
                'Y' => 0 + AdditionalBonusForChoice(me),
                'Z' => 3 + AdditionalBonusForChoice(me),
                _ => 0
            };
        }

        private static IEnumerable<string> ReadValues()
        {
            var values = new List<string>();
            string value;
            do
            {
                value = Console.ReadLine();
                if (value != "end") values.Add(value);
            } while (value != "end");

            return values;
        }
    }
}