using System;

namespace Graphs
{
    public struct Position
    {
        public int Col;
        public int Row;

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int GetDistanceTo(Position position)
        {
            var dy = Col - position.Col;
            var dx = Row - position.Row;

            return Math.Abs(dx) + Math.Abs(dy); //Math.Sqrt(dx * dx + dy * dy);
        }

        public override string ToString()
        {
            return string.Format("[{0},{1}]", Row, Col);
        }
    }
}