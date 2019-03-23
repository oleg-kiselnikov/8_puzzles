using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class BreadthFirstSearch : DepthLimitedSearch
    {
        public BreadthFirstSearch(State initialState, State targetState, IList<Action> actions, int maxDepth = int.MaxValue)
			:base(initialState, targetState, actions, maxDepth)
        {       
            IsEnd = s => s.HeuristicDifference(TargetState) == 0;
        }

        public override void Execute()
        {
            VisitedStatesCounter = 0;
            //VisitedStates.Clear();

            var queue = new Queue<State>();

            queue.Enqueue(InitialState);

            FinalState = InitialState;

            while (queue.Any())
            {
                var s = queue.Dequeue();

                VisitedStatesCounter++;
                
                FinalState = s;

                if (IsEnd(s))
                    break;

                //VisitedStates.Add(s);

                if (s.Depth == MaxDepth)
                    continue;

                foreach (var action in Actions)
                {
                    var n = action.Succ(s);                                      

                    if (n == null) continue;

                    //if (VisitedStates.Any(state => state.Equals(n)))
                    //    continue; //Ignore visited states

                    n.Depth = s.Depth + 1;

                    n.From = s;

                    queue.Enqueue(n);
                }
            }
        }
    }
}
