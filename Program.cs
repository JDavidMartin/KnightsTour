using System;

namespace knightJourney
{
    class Program
    {
        static void Main(string[] args)
        {

            Board myBoard = new Board(); //Define a board
            myBoard.InitiateBoard();

            Square firstSquare = myBoard.GetSquare(4, 4); // Define a starting Square
            firstSquare.moveToSquare();
            Square nextSquare = myBoard.getNextMove(firstSquare.getX(), firstSquare.getY()); // Get next square
            firstSquare.moveFromSquare(); // Move from old square
            nextSquare.moveToSquare(); // Move to new square
            Square oldSquare = nextSquare; // Change old square -> new square

            while (myBoard.TotalLanded() < 52) // repeat until complete
            {
                nextSquare = myBoard.getNextMove(oldSquare.getX(), oldSquare.getY());
                oldSquare.moveFromSquare();
                nextSquare.moveToSquare();

                oldSquare = nextSquare;
            }

        }
    }
}
