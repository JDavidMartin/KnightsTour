using System;

namespace knightJourney
{
    class Board
    {

        int[,] moveArray = new int[,] { { -1, 2 }, { -1, -2 }, { 1, 2 }, { 1, -2 }, { -2, -1 }, { -2, 1 }, { 2, -1 }, { 2, 1 } };
        // All possible moves for a knight

        Square[,] Squares = new Square[8, 8];
        // 2d Array to hold all Squares inside, where Sqaures[x,y] corresponds to Square with xPos=x and yPos=y

        public void InitiateBoard()
        {

            Console.WriteLine($"Creating Board with dimensions {8} by {8}");
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    Squares[x, y] = new Square(x, y);
                }
            }
        }

        public Square GetSquare(int x, int y) // Return Square with co-ordinates (x,y) if it exists, else return null
        {
            if (x < 0 || x > 7 || y < 0 || y > 7) // Outside boundaries, square doesn't exist
            {
                return null;
            }
            else
            {
                return Squares[x, y];
            }
        }

        public Square getNextMove(int StartingX, int StartingY) // Given a starting position, return Square with next move
        {
            int[,] availableMoves = getAllMoves(StartingX, StartingY); // Return all moves, whether possible or not
            int[] availableOnwardMoves = getNumOnwardMoves(availableMoves); //  Pass all moves in, and return array with number of onward moves.
            // e.g. if move was (0,0) there are 2 possible moves to go from there, if (1,0) there are 3 possible moves

            int lowestValue = 10; // Find lowest value that isn't 0
            // TODO Refactor this into something nicer
            for (int i = 0; i < availableOnwardMoves.Length; i++)
            {
                if (availableOnwardMoves[i] != 0)
                {
                    if (availableOnwardMoves[i] < lowestValue)
                    {
                        lowestValue = availableOnwardMoves[i];
                    }
                }
            }

            if (lowestValue == 10)
            {
                throw new System.ArgumentException("No possible onward move");
            }

            int lowestIndex = Array.IndexOf(availableOnwardMoves, lowestValue); // Get index of lowest value

            return GetSquare(availableMoves[lowestIndex, 0], availableMoves[lowestIndex, 1]); // Return Square with that index
        }

        public int[,] getAllMoves(int StartingX, int StartingY)
        // Given a square (x,y) return all possible onward squares regardless if they exist or not
        // Return a 2d array of (x,y) co-ordinates
        {
            int[,] availableMoves = new int[8, 2];
            for (int move = 0; move <= 7; move++)
            {
                int deltax = StartingX + moveArray[move, 0];
                int deltay = StartingY + moveArray[move, 1];

                availableMoves[move, 0] = deltax;
                availableMoves[move, 1] = deltay;

            }
            return availableMoves;
        }

        public int[] getNumOnwardMoves(int[,] allMoves)
        // Given all possible onward squares, if onward square has not already been landed on, get num possible onward squares again
        // return an 8 long array with num possible onward squares for each choice
        {
            int[] NumOnwardMoves = new int[8];
            for (int i = 0; i <= 7; i++)
            {
                int x = allMoves[i, 0];
                int y = allMoves[i, 1];
                if (GetSquare(x, y) != null) // Square exists
                {
                    if (!GetSquare(x, y).LandedOn()) // And not been landed on
                    {
                        int[,] AllSecondaryOnwardMoves = getAllMoves(x, y); // All possible onward moves
                        NumOnwardMoves[i] = getNumberOfSecondayOnwardMoves(AllSecondaryOnwardMoves);
                    }
                    else // Square has already been landed on
                    {
                        NumOnwardMoves[i] = 0;
                    }
                }
                else // Square doesn't exist
                {
                    NumOnwardMoves[i] = 0;
                }
            }

            return NumOnwardMoves;
        }

        public int getNumberOfSecondayOnwardMoves(int[,] allSecondaryMoves) // Given Secondary onward moves
        //See Which ones are viable and return number
        {
            int possibleMoveCounter = 0;
            for (int j = 0; j <= 7; j++)
            {
                int x = allSecondaryMoves[j, 0];
                int y = allSecondaryMoves[j, 1];

                if (GetSquare(x, y) != null)
                {
                    if (!GetSquare(x, y).LandedOn())
                    {
                        possibleMoveCounter++;
                    }
                }
            }
            return possibleMoveCounter;
        }


        public int TotalLanded() // Go through all Squares and Sum up any where LandedOn() returns true
        {
            int total = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (GetSquare(x, y).LandedOn())
                    {
                        total++;
                    }
                }
            }

            Console.WriteLine($"Total number of squares landed on is {total}");
            return total;
        }

    }

}