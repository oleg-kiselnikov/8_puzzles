using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public abstract class DepthLimitedSearch : Search
    {    

        public int MaxDepth;

        public DepthLimitedSearch(State initialState, State targetState, IList<Action> actions, int maxDepth = int.MaxValue)
            :base(initialState, targetState, actions)
        {            
            InitialState = initialState;
            TargetState = targetState;
            MaxDepth = maxDepth;
        }
    }
}
