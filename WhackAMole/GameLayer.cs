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

        }
    }
}

