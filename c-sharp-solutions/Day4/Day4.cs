using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas.Day4
{
    public class Day4 : PuzzleSolver
    {
        protected override string Solve(string[] inputLines)
        {
            var drawPile = inputLines[0].Split(',').Select(int.Parse);
            var boards = inputLines.Skip(2).Where(s => !string.IsNullOrEmpty(s))
                .Select((s, i) => (s, i))
                .GroupBy(l => l.i / 5)
                .Select(g => new BingoBoard(g.Select(x => x.s).ToArray())).ToList();
            
            foreach (var number in drawPile)
                foreach (var board in boards)
                    if (board.Play(number, out var score))
                        return score.ToString();
            return null;
        }
        
        private record Cell(int Value, int X, int Y)
        {
            private bool Marked { get; set; }

            public int GetBingoScore(BingoBoard board)
            {
                Marked = true;
                if (board.Board[Y].All(c => c.Marked))
                    return board.Cells.Values.Where(v => !v.Marked).Sum(c => c.Value) * Value;
                if (board.Board.All(r => r[X].Marked))
                    return board.Cells.Values.Where(v => !v.Marked).Sum(c => c.Value) * Value;
                return -1;
            }
        }

        private class BingoBoard
        {
            public Cell[][] Board { get; }
            public Dictionary<int, Cell> Cells { get; } = new();
            
            public BingoBoard(IEnumerable<string> board)
            {
                Board = board.Select((s, y) => s.Split(' ').Where(l => !string.IsNullOrWhiteSpace(l)).Select((v, x) =>
                {
                    var value = int.Parse(v);
                    var cell = new Cell(value, x, y);
                    Cells.Add(value, cell);
                    return cell;
                }).ToArray()).ToArray();
            }

            public bool Play(int number, out int score)
            {
                if (Cells.ContainsKey(number))
                {
                    score = Cells[number].GetBingoScore(this);
                    if (score > 0) return true;
                }
                score = -1;
                return false;
            }
        }
    }
}
