# 8_puzzles
In this coursework different search algorithms are
compared. Some advantages and disadvantages of search meth-
ods are described.

The task is to solve the game of ”Blocksworld tile puzzle”.
An ’agent’ moves in a simulated N×N grid world with the
goal of building towers of blocks. Each grid space contains
either a ’tile’ or the agent. Some tiles have letters on them
these are the ’blocks’. All the other tiles are white. The agent
moves up/down/left/right (except where borders prevent it).
As the agent moves, the tile that they move onto slides under
them into the position that they just came from. The exact start
state and goal state for the assignment is shown below. The
goal is to build a tower, with these exact blocks in these exact
positions as shown on figure 1. The position of the agent at
the end doesnt matter.

To solve this problem the three components were specified:
problem states, moves and goal. Each tile configuration is a
state. For representation of each configuration the list of three
positions for ’a’,’b’,’c’ is used. Each letter has specific row
and column number in matrix 4 × 4, all other cells are empty,
this representation of matrix needs less memory then if we
use the whole matrix for each cells. The position of an agent
is represented separately as a tuple of 2 numbers, where first
number is a row, and second number is a column. The moves
are represented as the offsets from the current position of an
agent to the left, up, right and down. Start and goal states are
the positions of ’a’, ’b’, ’c’ letters on the board (or matrix)
(as shown in Figure 1).
The main idea of the task is to compare different algorithms
in a tree search. A search strategy is defined by picking the
order of node expansion. Strategies are evaluated along the
following dimensions:

• completeness: whether it always find a solution if one
exists;

• time complexity: number of nodes generated;
space complexity: maximum number of nodes in mem-
ory;

• optimality: whether it always find a least-cost solution;
Time and space complexity are measured in terms of
b: maximum branching factor of the search tree
d: depth of the least-cost solution
m: maximum depth of the state space (may be ∞).
I implemented 5 search strategies:
1) breadth-first search;
2) depth-first search;
3) iterative deepening search based on depth-limited search;
4) iterative deepening search based on breadth-limited
search;
5) A* heuristic search.

