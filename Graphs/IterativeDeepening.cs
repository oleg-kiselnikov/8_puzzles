using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    public class IterativeDeepening : DepthLimitedSearch 
    {
        public DepthLimitedSearch _depthLimitedSearch;

        public IterativeDeepening(State initialState, State targetState, IList<Action> actions, DepthLimitedSearch depthLimitedSearch, int maxDepth)
            : base(initialState, targetState, actions, maxDepth)
        {
            _depthLimitedSearch = depthLimitedSearch;
        }        

        public override void Execute()
        {
            VisitedStatesCounter = 0;

            FinalState = null;

            for (var i = 1; i <= MaxDepth; i++)
            {
                _depthLimitedSearch.MaxDepth = i;

                _depthLimitedSearch.Execute();

                VisitedStatesCounter += _depthLimitedSearch.VisitedStatesCounter;

                FinalState = _depthLimitedSearch.FinalState;

                if (FinalState != null && IsEnd(FinalState))
                    break;
            }
        }  
    }
}