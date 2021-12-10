using System;
using System.Collections.Generic;

namespace Merry.Christmas.Day4
{
    public class Day4Part2 : Day4
    {
        protected override string Play(IEnumerable<int> drawPile, List<BingoBoard> boards)
        {
            var remainingBoards = boards.Count;
            foreach (var number in drawPile)
                foreach (var board in boards)
                    if (!board.HasBingo && board.Play(number, out var score))
                    {
                        remainingBoards--;
                        if (remainingBoards == 0) return score.ToString();
                    }

            return null;
        }
    }
}