using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace WhackAMole
{
    public class GameLayer : CCLayerColor
    {

        public GameLayer() : base(CCColor4B.Blue)
        {
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Use the bounds to layout the positioning of our drawable assets
            var bounds = VisibleBoundsWorldspace;

            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = OnTouched;
            AddEventListener(touchListener, this);
        }

        void OnTouched(List<CCTouch> touches, CCEvent e)
        {
            switch(GameController.self.currentState)
            {
                case GameState.Menu:
                    break;
                case GameState.Difficulty:
                    break;
                case GameState.Settings:
                    break;
                case GameState.Game:
                    GameController.self.OnTouched(touches[0]);
                    break;
                case GameState.GameOver:
                    break;
            }
        }

        public void AddMoleAsChild(Mole m)
        {
            AddChild(m);
        }

        void SwapGameState (GameState newState)
        {
            // Change to new state
            GameController.self.currentState = newState;

            // Remove current children
            RemoveAllChildren();

            // Sets the new scene up
            HandleNewState();
        }

        void HandleNewState()
        {

        }
    }
}

