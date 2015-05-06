using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;              // Required to use XNA features.
using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio Engine general features.
using XNAMachinationisRatio.AI;             // Required to use the XNA Machinationis Ratio general AI features.
using Microsoft.Xna.Framework.Input;



namespace FishORama
{
    class FootballMind : AIPlayer
    {
        #region Data Members

        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).
        private int mSpeed;
        private Vector3 startPos;
        private bool FootballOnStage = true;
        private float ResumePoint;

        #endregion

        #region Properties
        public AquariumToken Aquarium
        {
            set { mAquarium = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="pToken">Token to be associated with the mind.</param>
        public FootballMind(X2DToken pToken)
        {

            this.Possess(pToken);       // Possess token.
            mFacingDirection = 1;       // Current direction the fish is facing.
            Vector3 tokenPosition = this.PossessedToken.Position;
            startPos = tokenPosition;
        }

        #endregion

        #region Methods
        public override void Update(ref GameTime pGameTime)
        {
           GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
           Vector3 tokenPosition = this.PossessedToken.Position;
           if (gamePadState.IsConnected)
           {
               // If the game pad is connected this code can now be run
               // If the Y button is pressed the Football is "saved" and moved off screen
               if (gamePadState.Buttons.Y == ButtonState.Pressed && FootballOnStage == true)
               {
                   ResumePoint = tokenPosition.X;
                   tokenPosition.X = 1000;
                   FootballOnStage = false;
               }
               // If the B button is pressed the football is returend to its original position
               if (gamePadState.Buttons.B == ButtonState.Pressed && FootballOnStage == false)
               {
                   tokenPosition.X = ResumePoint;
                   FootballOnStage = true;
               }

               if (gamePadState.DPad.Up == ButtonState.Pressed && FootballOnStage == true && tokenPosition.Y <= 300)
               {
                   // Move up
                   tokenPosition.Y += 5;
               }
               if (gamePadState.DPad.Down == ButtonState.Pressed && FootballOnStage == true && tokenPosition.Y >= -300)
               {
                   // Move down
                   tokenPosition.Y -= 5;
               }
               if (gamePadState.DPad.Left == ButtonState.Pressed && FootballOnStage == true && tokenPosition.X >= -400)
               {
                   // Move left
                   tokenPosition.X -= 5;
               }
               if (gamePadState.DPad.Right == ButtonState.Pressed && FootballOnStage == true && tokenPosition.X <= 400)
               {
                   // Move right
                   tokenPosition.X += 5;
               }
           }
           if (gamePadState.IsConnected == false)
           {
               ResumePoint = tokenPosition.X;
               tokenPosition.X = 1000;
               FootballOnStage = false;
           }
           this.PossessedToken.Position = tokenPosition;
        }
        #endregion
    }
}
