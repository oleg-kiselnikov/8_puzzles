using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Graphs;
using Action = Graphs.Action;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static readonly int _colCount = 4;
        private static readonly int _rowCount = 4;

        private static void Main(string[] args)
        {
            Stopwatch stopwatch;

            TextWriter textWriter;

            var initialState = new GridState();

            initialState.Agent = new Position(0, 0);
            initialState.Puzzles.Add(new Tuple<Position, string>(new Position(1, 0), "a"));
            initialState.Puzzles.Add(new Tuple<Position, string>(new Position(1, 1), "b"));
            initialState.Puzzles.Add(new Tuple<Position, string>(new Position(1, 2), "c"));

            var goalState = new GridState();

            goalState.Puzzles.Add(new Tuple<Position, string>(new Position(1, 1), "a"));
            goalState.Puzzles.Add(new Tuple<Position, string>(new Position(2, 1), "b"));
            goalState.Puzzles.Add(new Tuple<Position, string>(new Position(3, 1), "c"));

            var offsets = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(-1, 0),
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(0, 1)
            };

            var actions = new List<Action>();

            foreach (var offset in offsets)
            {
                var o = offset;

                var action = new Action
                {
                    Cost = s => 1 + s.HeuristicDifference(goalState), //A* heuristic
                    Succ = s => MoveState(s, o)
                };

                actions.Add(action);
            }

            var depthFirstSearch =
				new DepthFirstSearch(initialState, goalState, actions, maxDepth: 16);

            var breadthFirstSearch =
                new BreadthFirstSearch(initialState, goalState, actions, maxDepth: 16);

            var iterativeDeepening1 =
                new IterativeDeepening(initialState, goalState, actions, depthFirstSearch, maxDepth: 16);

            var iterativeDeepening2 =
                new IterativeDeepening(initialState, goalState, actions, breadthFirstSearch, maxDepth: 16);
            
            var algorythms = new Dictionary<string, Search>(){
                { "a-star.txt", new UniformCostSearch(initialState, goalState, actions) },
				//{ "breadth-first-search.txt", breadthFirstSearch },
				//{ "depth-first-search.txt", depthFirstSearch },
                { "iterative-deepening-with-depth-first-search.txt", iterativeDeepening1 },
                //{ "iterative-deepening-with-breadth-first-search.txt", iterativeDeepening2 }               
            };

            foreach (var a in algorythms)
            {
                var fileName = a.Key;
                var algorythm = a.Value;
                //using (var textWriter = Console.Out)
                using (textWriter = File.CreateText(fileName))
                {
                    stopwatch = Stopwatch.StartNew();

                    algorythm.Execute();

                    stopwatch.Stop();

                    Print(textWriter, algorythm.Result);

                    textWriter.WriteLine("Elapsed time: {0}", stopwatch.Elapsed);
                    textWriter.WriteLine("Number of visited states: {0}", algorythm.VisitedStatesCounter);
					System.Console.WriteLine("the end of " +a.Key);
                }
            }
        }


        
        private static State MoveState(State state, Tuple<int, int> tuple)
        {
            var gridState = (GridState)state;

            gridState = (GridState)gridState.Clone();

            gridState.Agent.Row += tuple.Item1;
            gridState.Agent.Col += tuple.Item2;

            if (gridState.Agent.Row < 0 || gridState.Agent.Row >= _rowCount)
                return null;
            if (gridState.Agent.Col < 0 || gridState.Agent.Col >= _colCount)
                return null;

            var puzzle = gridState.Puzzles.FirstOrDefault(p => p.Item1.Equals(gridState.Agent));

            if (puzzle == null) return gridState;

            gridState.Puzzles.Remove(puzzle);

            var newPosition = new Position(puzzle.Item1.Row - tuple.Item1, puzzle.Item1.Col - tuple.Item2);

            puzzle = new Tuple<Position, string>(newPosition, puzzle.Item2);

            gridState.Puzzles.Add(puzzle);            

            return gridState;
        }

        private static void Print(TextWriter textWriter, IEnumerable<State> result)
        {
            foreach (GridState state in result)
            {
                var grid = new string[_rowCount, _colCount];

                for (var i = 0; i < _rowCount; i++)
                    for (var j = 0; j < _colCount; j++)
                        grid[i, j] = "_";

                foreach (var puzzle in state.Puzzles)
                    grid[puzzle.Item1.Row, puzzle.Item1.Col] = puzzle.Item2;

                grid[state.Agent.Row, state.Agent.Col] = "o";

                var stringBuilder = new StringBuilder();

                for (var i = 0; i < _rowCount; i++)
                    for (var j = 0; j < _colCount; j++)
                        stringBuilder.AppendFormat(j + 1 < _colCount ? "{0} " : "{0}\r\n", grid[i, j]);

                textWriter.WriteLine(stringBuilder.ToString());
            }

			textWriter.WriteLine ("Number of steps: {0}", result.Count() - 1);
        }
    }
}