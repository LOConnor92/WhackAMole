using System;
using System.Collections.Generic;

using CocosSharp;

namespace WhackAMole
{
    public enum GameState
    {
        Menu,
        Settings,
        Difficulty,
        Game,
        GameOver
    };

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public class GameController : CCNode
    {
        // Initialize a singleton
        static Lazy<GameController> Self = new Lazy<GameController>(() => new GameController());
        public static GameController self
        {
            get
            {
                return Self.Value;
            }
        }

        // Game variables
        public int score;
        public int highScore;
        public int lives;
        const int initialLives = 5;             // How many lives we should start with
        public string moleChoice = "Mole1";     // The name of the mole sprite to load
        public float moleVisibleFor;            // How long the mole pops out of its hole for
        public float timeBetweenMoles;          // How long before a new mole pops out of its hole
        public List<Mole> moles;                // The mole objects to activate random ones

        // To add the moles as children from the canvas object
        public delegate void MoleAction(Mole m);
        public MoleAction AddMoleAsChild;

        // Current game state
        public GameState currentState;

        // Change the mole sprite we use
        public void SetMoleSprite(string name)
        {
            moleChoice = name;
        }

        // Initialise the game
        public void InitializeGame(Difficulty difficulty, List<IntVector2> positions)
        {
            // Make sure mole list is empty
            moles.Clear();

            // Create all the moles
            for (int i = 0; i < positions.Count; i++)
            {
                Mole m = new Mole(moleChoice);
                m.AnchorPoint = CCPoint.AnchorLowerLeft;
                m.PositionX = positions[i].x;
                m.PositionY = positions[i].y;
                AddMoleAsChild(m);

                moles.Add(m);
            }

            // Set up lives
            lives = initialLives;

            // Set up difficulty variables
            switch(difficulty)
            {
                case Difficulty.Easy:
                    moleVisibleFor = 3f;
                    timeBetweenMoles = 1.2f;
                    break;
                case Difficulty.Medium:
                    moleVisibleFor = 1.5f;
                    timeBetweenMoles = 0.9f;
                    break;
                case Difficulty.Hard:
                    moleVisibleFor = 0.75f;
                    timeBetweenMoles = 0.5f;
                    break;
            }

            // Tell the game we've started
            currentState = GameState.Game;

            // Start game logic
            PickRandomMole();
        }

        // Our main game logic (float is so we can schedule it, if we set it in the function it avoids recreating the function)
        void PickRandomMole(float time = 0f)
        {
            // Make sure we're in game
            if (currentState == GameState.Game)
            {
                // Activate a random mole
                moles[CCRandom.GetRandomInt(0, moles.Count)].ActivateMole(moleVisibleFor);

                // Recursion.
                // I don't use Schedule because the time keeps changing
                ScheduleOnce(PickRandomMole, timeBetweenMoles);
            }
        }

        // When we touch the screen see if we hit a mole
        public void OnTouched(CCTouch touch)
        {
            // Get the location of the touch
            CCPoint position = touch.Location;

            // Check through each mole
            for (int i = 0; i < moles.Count; i++)
            {
                // Check we hit the mole and it's currently active
                if (moles[i].CheckCollision(position) && moles[i].HitMole())
                {
                    // Add one to the score and exit the loop
                    score++;
                    break;
                }
            }
        }

        // For when you lose a life
        public void LoseALife()
        {
            lives--;

            if (lives <= 0)
                GameOver();
        }

        // Handle game over logic
        void GameOver()
        {
            currentState = GameState.GameOver;
        }
    }
}