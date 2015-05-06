using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework;              // Required to use XNA features.
using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio Engine general features.
using XNAMachinationisRatio.AI;             // Required to use the XNA Machinationis Ratio general AI features.

namespace FishORama
{
        class SubmarineMind : AIPlayer
    {
        #region Data Members
        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).
        private float mSpeed = 5;
        private bool SigX = false;
        private bool SigY = false;
        private bool Rest = false;
        private bool RightExit = false;
        private bool LeftExit = false;

        #endregion

        #region Properties
        public AquariumToken Aquarium
        {
            set { mAquarium = value; }
        }
        
        #endregion

        #region Constructors

        public SubmarineMind(X2DToken pToken)
        {
            this.Possess(pToken);       // Possess token.
            mFacingDirection = 1;       // Current direction the fish is facing.
        }

        #endregion

        #region Methods
        public void Search()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;

            if (tokenPosition.X < mAquarium.Signal.Position.X)
            {
                mFacingDirection = 1;
                tokenPosition.X += mSpeed;
            }

            else if (tokenPosition.X > mAquarium.Signal.Position.X)
            {
                mFacingDirection = -1;
                tokenPosition.X -= mSpeed;
            }

            if (tokenPosition.Y < mAquarium.Signal.Position.Y)
            {

                tokenPosition.Y += mSpeed;
            }

            else if (tokenPosition.Y > mAquarium.Signal.Position.Y)
            {

                tokenPosition.Y -= mSpeed;
            }

            if (tokenPosition.X - mAquarium.Signal.Position.X <= 5 && tokenPosition.X - mAquarium.Signal.Position.X >= -5)
            {
                tokenPosition.X = mAquarium.Signal.Position.X;
                SigX = true;
            }

            if (tokenPosition.Y - mAquarium.Signal.Position.Y <= 5 && tokenPosition.Y - mAquarium.Signal.Position.Y >= -5)
            {
                tokenPosition.Y = mAquarium.Signal.Position.Y;
                SigY = true;
            }

            if (SigX == true && SigY == true)
            {
                mAquarium.RemoveSignal();
                SigX = false;
                SigY = false;
            }
            this.PossessedToken.Position = tokenPosition;
            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                            this.PossessedToken.Orientation.Y,
                                            this.PossessedToken.Orientation.Z);
        }

        public void Sink()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.Y = tokenPosition.Y + -mSpeed;
            this.PossessedToken.Position = tokenPosition;
            
        }

        public void exitRight()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            mFacingDirection = 1;
            tokenPosition.X += mSpeed;
            this.PossessedToken.Position = tokenPosition;
            this.PossessedToken.Position = tokenPosition;
            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                            this.PossessedToken.Orientation.Y,
                                            this.PossessedToken.Orientation.Z);
        }

        public void exitLeft()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            mFacingDirection = -1;
            tokenPosition.X -= mSpeed;
            this.PossessedToken.Position = tokenPosition;
            this.PossessedToken.Position = tokenPosition;
            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                            this.PossessedToken.Orientation.Y,
                                            this.PossessedToken.Orientation.Z);
        }

        public override void Update(ref GameTime pGameTime)
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            this.PossessedToken.Position = tokenPosition;

            if (mAquarium.Signal != null)
            {
                Search();
            }

            if (mAquarium.Signal == null && tokenPosition.X >= -2000 && tokenPosition.X <= 2000)
            {
                if (tokenPosition.X > 0)
                {
                    RightExit = true;
                }
                if (tokenPosition.X <= 0)
                {
                    LeftExit = true;
                }
                if (RightExit == true)
                {
                    RightExit = false;
                    exitRight();
                }
                if (LeftExit == true)
                {
                    LeftExit = false;
                    exitLeft();
                }
            }
            
        }        
        #endregion
    }
}
