using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randomization;

namespace Graphs
{
    public class DepthFirstSearch : DepthLimitedSearch
    {
        public DepthFirstSearch(State initialState, State targetState, IList<Action> actions, int maxDepth = int.MaxValue)
			: base(initialState, targetState, actions, maxDepth)
        {
        }

        public override void Execute()
        {
            VisitedStatesCounter = 0;
            //VisitedStates.Clear();
            
            var stack = new Stack<State>();

            stack.Push(InitialState);

            FinalState = InitialState;

            while (stack.Any())
            {
                var s = stack.Pop();

                VisitedStatesCounter++;
                
                FinalState = s;

                if (IsEnd(s))
                    break;

                //VisitedStates.Add(s);

                if (s.Depth == MaxDepth)
                    continue;

                Actions.Shuffle();

                foreach (var action in Actions)
                {
                    var n = action.Succ(s);

                    if (n == null) continue;

                    //if (VisitedStates.Any(state => state.Equals(n)))
                    //    continue; //Ignore visited states

                    n.Depth = s.Depth + 1;

                    n.From = s;

                    stack.Push(n);
                }
            }
        }
    }
}
