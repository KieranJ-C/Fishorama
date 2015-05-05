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
    class PiranhaMind : AIPlayer
    {
        #region Data Members

        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).
        private float mSpeed = 5;
        private bool EatLegX = false;
        private bool EatLegY = false;
        private bool justAte = false;
        private Vector3 EatVector;
        private double currTime;
        private double FinishTime;


        #endregion

        #region Properties

        /// <summary>
        /// Set Aquarium in which the mind's behavior should be enacted.
        /// </summary>
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
        public PiranhaMind(X2DToken pToken)
        {
            this.Possess(pToken);       // Possess token.
            mFacingDirection = 1;       // Current direction the fish is facing.
        }

        #endregion

        #region Methods

        /// <summary>
        /// AI Update method.
        /// </summary>
        /// <param name="pGameTime">Game time</param>

        public void Hungry()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.X = tokenPosition.X + (mSpeed * mFacingDirection);
            this.PossessedToken.Position = tokenPosition;
            if (tokenPosition.X >= 400 || tokenPosition.X <= -400)
            {
                mFacingDirection = -mFacingDirection;
            }


            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                                        this.PossessedToken.Orientation.Y,
                                                        this.PossessedToken.Orientation.Z);  
        }


        public void Feeding()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            if (justAte)
            {
                mSpeed = 1;
            }
            if (tokenPosition.X - mAquarium.ChickenLeg.Position.X < 0)
            {
                mFacingDirection = 1;
            }
            else
            {
                mFacingDirection = -1;
            }

            EatVector = tokenPosition - mAquarium.ChickenLeg.Position;
            EatVector.Normalize();
            tokenPosition = tokenPosition - EatVector * mSpeed;


            if (tokenPosition.X - mAquarium.ChickenLeg.Position.X <= 5 && tokenPosition.X - mAquarium.ChickenLeg.Position.X >= -5)
            {
                EatLegX = true;
            }

            if (tokenPosition.Y - mAquarium.ChickenLeg.Position.Y <= 5 && tokenPosition.Y - mAquarium.ChickenLeg.Position.Y >= -5)
            {
                EatLegY = true;
            }

            if (EatLegX == true && EatLegY == true)
            {
                mAquarium.RemoveChickenLeg();
                EatLegX = false;
                EatLegY = false;
                justAte = true;
                FinishTime = currTime + 5;
            }
            mSpeed = 5;
            this.PossessedToken.Position = tokenPosition;
            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                            this.PossessedToken.Orientation.Y,
                                            this.PossessedToken.Orientation.Z);  
        }

        public void Full()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            mSpeed = 1;
            tokenPosition.X = tokenPosition.X + (mSpeed * mFacingDirection);
            this.PossessedToken.Position = tokenPosition;
            if (tokenPosition.X >= 400 || tokenPosition.X <= -400)
            {
                mFacingDirection = -mFacingDirection;
            }
            if (currTime >= FinishTime)
            {
                justAte = false;
            }
            mSpeed = 5;
            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                                        this.PossessedToken.Orientation.Y,
                                                        this.PossessedToken.Orientation.Z);  
        }

        public override void Update(ref GameTime pGameTime)
        {
            currTime = pGameTime.TotalGameTime.TotalSeconds;
            Vector3 tokenPosition = this.PossessedToken.Position;
            this.PossessedToken.Position = tokenPosition;

            if (mAquarium.ChickenLeg != null) 
            {
                Feeding();
            }

            else if(mAquarium.ChickenLeg == null && justAte == true)
            {
                Full();
            }

            else
                {
                    Hungry();
                }

        }        
        #endregion
    }
}
