using System;
using System.Collections.Generic;
using System.Linq;
using Graphs;

namespace ConsoleApplication1
{
    public class GridState : State
    {
        public Position Agent;

        public List<Tuple<Position, string>>
            Puzzles = new List<Tuple<Position, string>>();

        public override int HeuristicDifference(State state)
        {
            if (ReferenceEquals(this, state))
                return 0;

            var gridState = (GridState)state;

            int sumDistanceBetweenPuzzles = 0;

            foreach (var puzzle1 in Puzzles)
            {
                var puzzle2 = gridState.Puzzles.FirstOrDefault(t => t.Item2 == puzzle1.Item2);

                if (puzzle2 != null)
                {
                    var distance = puzzle1.Item1.GetDistanceTo(puzzle2.Item1);

                    sumDistanceBetweenPuzzles += distance;
                }
                else
                    return int.MaxValue;
            }

            return sumDistanceBetweenPuzzles;
        }

        public override object Clone()
        {
            var state = new GridState
            {
                Agent = Agent
            };

            state.Puzzles.AddRange(Puzzles);

            return state;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj == null)
                return false;

            var gridState = (GridState)obj;

            if (Puzzles.Count != gridState.Puzzles.Count)
                return false;

            var puzzles1 = Puzzles.OrderBy(p => p.Item2).ToList();
            var puzzles2 = gridState.Puzzles.OrderBy(p => p.Item2).ToList();

            for (int i = 0; i < puzzles1.Count; i++)
            {
                if (!puzzles1[i].Item1.Equals(puzzles2[i].Item1))
                    return false;

                if (puzzles1[i].Item2 != puzzles2[i].Item2)
                    return false;
            }
            
            return Agent.Equals(gridState.Agent);
        }
    }
}