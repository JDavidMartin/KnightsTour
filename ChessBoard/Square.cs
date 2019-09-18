using System;
namespace knightJourney
{
    public class Square
    {
        public Square(int x, int y)
        {
            xPos = x;
            yPos = y;
            active = false;
        }

        private int xPos;
        private int yPos;
        private bool landed { get; set; }
        private bool active { get; set; } // 

        public bool isActive()
        {
            return active;
        }

        public void moveToSquare()
        {
            Console.WriteLine($"Moved to square ({this.xPos}, {this.yPos})");
            landed=true;
            active = true;
        }
        public void moveFromSquare()
        {
            active = false;
        }

        public int getX()
        {
            return xPos;
        }
        public int getY()
        {
            return yPos;
        }

        public Boolean LandedOn(){
            return landed;
        }

    };

}