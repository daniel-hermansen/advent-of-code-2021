using System.Collections.Generic;
using System.Linq;

namespace Merry.Christmas.Day4
{
    public class Day4 : PuzzleSolver
    {
        protected override string Solve(string[] inputLines)
        {
            var drawPile = inputLines[0].Split(',').Select(int.Parse);
            var boards = inputLines.Skip(2).Where(s => !string.IsNullOrWhiteSpace(s))
                .Select((s, i) => (s, i))
                .GroupBy(l => l.i / 5)
                .Select(g => new BingoBoard(g.Select(x => x.s).ToArray())).ToList();            
            return Play(drawPile, boards);
        }

        protected virtual string Play(IEnumerable<int> drawPile, List<BingoBoard> boards)
        {
            foreach (var number in drawPile)
                foreach (var board in boards)
                    if (board.Play(number, out var score))
                        return score.ToString();
            return null;
        }

        public record Cell(int Value, int X, int Y)
        {
            public bool Marked { get; private set; }

            public int Play(BingoBoard board)
            {
                Marked = true;
                if (board.Board[Y].All(c => c.Marked) || board.Board.All(r => r[X].Marked))
                    return board.Cells.Values.Where(v => !v.Marked).Sum(c => c.Value) * Value;
                return -1;
            }
        }

        public class BingoBoard
        {
            public Cell[][] Board { get; }
            public Dictionary<int, Cell> Cells { get; } = new();
            public int? Score { get; private set; }
            public bool HasBingo => Score > 0;
            
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
                    Score = score = Cells[number].Play(this);
                    if (score > 0) return true;
                }
                score = -1;
                return false;
            }
        }
    }
}
