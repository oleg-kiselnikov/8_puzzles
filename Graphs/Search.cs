using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public abstract class Search
    {
        public List<State> VisitedStates = new List<State>();
        public int VisitedStatesCounter;
        public IList<Action> Actions;
        public State InitialState;
        public State FinalState;
        public State TargetState;
        protected Func<State, bool> IsEnd;

        protected Search(State initialState, State targetState, IList<Action> actions)
        {
            InitialState = initialState;
            TargetState = targetState;
            Actions = actions;
            IsEnd = s => s.HeuristicDifference(TargetState) == 0;
        }
        public IEnumerable<State> Result
        {
            get
            {
                var result = new List<State>();

                if (FinalState == null)
                    return result;

                var finalState = FinalState;

                result.Add(finalState);

                while ((finalState = finalState.From) != null)
                    result.Add(finalState);

                result.Reverse();

                return result;
            }
        }       

        public abstract void Execute();
    }
}
