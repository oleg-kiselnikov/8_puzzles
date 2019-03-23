using System;

namespace Graphs
{
    public abstract class State : ICloneable
    {
        public int Depth { get; set; }
        public State From { get; set; }

        public abstract object Clone();

        public abstract int HeuristicDifference(State state);

    }
}