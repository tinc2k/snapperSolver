using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnapperSolver.Models
{
    public class Solver
    {
        public List<string> Solve(Matrix matrix, int numberOfMoves)
        {
            var results = new List<string>();
            var solutions = new Dictionary<int, Dictionary<string, int>>();
            for (int i = 1; i <= numberOfMoves; i++)
            {
                solutions.Add(i, new Dictionary<string, int>());
            }

            /*figure out strategy based on allowed number of moves*/
            if (numberOfMoves <= 3)
            {
                /*start calculation*/
                MyLittleRecursion(matrix.Clone(), solutions, numberOfMoves);
            }
            else
            {
                MyLittleRecursion(matrix.Clone(), solutions, 3);
                /*get top 30 solutions*/
                //var top_thirty = solutions[3].OrderByDescending(x => x.Value).Take(30);
                var top_thirty = solutions[3].OrderBy(x => x.Value).Take(30);
                foreach (var attempt in top_thirty)
                {
                    var game = new Game(matrix.Clone());
                    game.MoveSequence(attempt.Key);
                    MyLittleRecursion(game.Matrix.Clone(), solutions, 3, 1, attempt.Key);
                }
            }

            var level = solutions.FirstOrDefault(x => x.Value.Values.Contains(0));
            if (level.Value != null) /* we have at least one solution at this level?*/
            {
                var slsns = solutions[level.Key].Where(x => x.Value == 0).ToList();
                foreach (var s in slsns)
                {
                    results.Add(s.Key); /*add each winning game (sequence of moves) to result list*/
                }
            }
            return results;
        }

        private void MyLittleRecursion(
            Matrix matrix,
            Dictionary<int, Dictionary<string, int>> solutions,
            int level_max,
            int level_current = 1,
            string move_sequence = "")
        {
            var internal_matrix = matrix.Clone();
            var possible_moves = internal_matrix.Blocks.ToList();    /*matrix passes current state from recursion to recursion*/
            foreach (var attempt in possible_moves)
            {
                var game = new Game(internal_matrix.Clone());
                game.Move(attempt.Row, attempt.Column);
                var new_move_sequence = move_sequence + attempt.Row.ToString() + attempt.Column.ToString() + " ";
                solutions[level_current].Add(new_move_sequence, game.Matrix.Sum()); /*write move and result to solutions*/
                if (level_current < level_max && !solutions[level_current].Any(x => x.Value == 0)) /*if we didn't find a solution, and aren't at level_max, go deeper*/
                {
                    MyLittleRecursion(game.Matrix.Clone(), solutions, level_max, level_current + 1, string.Copy(new_move_sequence));
                }
            }
        }

    }
}