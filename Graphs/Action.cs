using System;

namespace Graphs
{
    public struct Action
    {
        public Func<State, int> Cost { get; set; }
        public Func<State, State> Succ { get; set; }
    }
}