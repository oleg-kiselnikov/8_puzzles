using System;
using System.Collections.Generic;

namespace Graphs
{
    public class UniformCostSearch : Search
    {
        public UniformCostSearch(State initialState, State targetState, IList<Action> actions)
            : base(initialState, targetState, actions)
        {
        }

        public override void Execute()
        {
            VisitedStatesCounter = 0;
            //VisitedStates.Clear();

            var frontier = new PriorityQueue<State, int>();

            frontier.Enqueue(InitialState, 0);

            FinalState = InitialState;

            while (!frontier.IsEmpty)
            {
                State s;

                var p = frontier.Dequeue(out s);

                VisitedStatesCounter++;

                FinalState = s;

                if (IsEnd(s))
                    break;

                //VisitedStates.Add(s);

                foreach (var action in Actions)
                {
                    var n = action.Succ(s);

                    if (n == null) continue;

                    //if (VisitedStates.Any(state => state.Equals(n)))
                    //    continue; //Ignore visited states

                    n.From = s;

                    frontier.Enqueue(n, p + action.Cost(n));
                }
            }
        }
    }
}