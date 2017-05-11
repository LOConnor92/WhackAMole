using CocosSharp;

namespace WhackAMole
{
    public class Mole : CCNode
    {
        // The sprites for the hole and the mole
        CCSprite moleSprite;
        CCSprite holeSprite;

        // ctor
        public Mole(string spriteName) : base()
        {
            // Setup sprites
            moleSprite = new CCSprite(spriteName);
            holeSprite = new CCSprite(spriteName + "_hole");

            // Anchor sprites to middle
            moleSprite.AnchorPoint = CCPoint.AnchorMiddle;
            holeSprite.AnchorPoint = CCPoint.AnchorMiddle;

            // Make sprites children so they move with object
            AddChild(moleSprite);
            AddChild(holeSprite);

            // Make mole sprite inactive
            moleSprite.Visible = false;
        }

        public void ActivateMole(float time)
        {
            ShowMole();

            ScheduleOnce(MissedMole, time);
        }

        // Check if we missed the mole
        void MissedMole(float time)
        {
            // So if we missed the mole
            if (moleSprite.Visible)
            {
                ShowHole();
                GameController.self.LoseALife();
            }
        }

        // Check if mole should be hit
        public bool HitMole()
        {
            bool isMoleHit = moleSprite.Visible;

            // If we've hit the mole, swap it to the hole sprite
            if (isMoleHit)
                ShowHole();

            return isMoleHit;
        }

        // Activate mole sprite
        void ShowMole()
        {
            moleSprite.Visible = true;
            holeSprite.Visible = false;
        }

        // Activate hole sprite
        void ShowHole()
        {
            holeSprite.Visible = true;
            moleSprite.Visible = false;
        }

        public bool CheckCollision(CCPoint loc)
        {
            return moleSprite.BoundingBoxTransformedToParent.ContainsPoint(loc);
        }
    }
}