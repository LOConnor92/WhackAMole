using CocosSharp;

namespace WhackAMole
{
    public class GameButton : CCNode
    {
        public int sizeX;
        public int sizeY;
        public int positionX;
        public int positionY;
        public string text;
        CCDrawNode button;

        public GameButton(int positionX, int positionY, int sizeX, int sizeY, string text)
        {
            button = new CCDrawNode();

            button.AnchorPoint = CCPoint.AnchorUpperLeft;

            button.PositionX = positionX;
            button.PositionY = positionY;


        }
    }
}