using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;              // Required to use XNA features.
using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio Engine general features.
using XNAMachinationisRatio.AI;             // Required to use the XNA Machinationis Ratio general AI features.


namespace FishORama
{
    class SeahorseMind : AIPlayer
    {
        #region Data Members

        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).
        private float mSpeed;
        private float startY;
        private Boolean CanRise = true;
        private bool justSpawned = true;
        static Random rnd = new Random();

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
        public SeahorseMind(X2DToken pToken)
        {

            this.Possess(pToken);       // Possess token.
            mFacingDirection = 1;       // Current direction the fish is facing.
            //Vector3 tokenPosition = this.PossessedToken.Position;
            //startY = tokenPosition.Y;
        }

        #endregion

        #region Methods

        /// <summary>
        /// AI Update method.
        /// </summary>
        /// <param name="pGameTime">Game time</param>

        public void HorizontalSwimBehaviour()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.X = tokenPosition.X + (mSpeed * mFacingDirection);
            this.PossessedToken.Position = tokenPosition;
            if (this.mAquarium.ReachedHorizontalBoundary(this.PossessedToken))
            {
                mFacingDirection = -mFacingDirection;
                mSpeed = rnd.Next(1, 6);
            }



            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                                        this.PossessedToken.Orientation.Y,
                                                        this.PossessedToken.Orientation.Z);
        }

        public void VerticalSwimBehaviourRise()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.Y = tokenPosition.Y + mSpeed;
            
            this.PossessedToken.Position = tokenPosition;   
            if (tokenPosition.Y >= (startY + 100))
            {
                CanRise = false;
            }   

            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                                        this.PossessedToken.Orientation.Y,
                                                        this.PossessedToken.Orientation.Z);
        }

        public void VerticalSwimBehaviourFall()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.Y = tokenPosition.Y + -mSpeed;
            this.PossessedToken.Position = tokenPosition;
            if (tokenPosition.Y <= (startY - 100))
            {

                CanRise = true;
            }  

            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                                        this.PossessedToken.Orientation.Y,
                                                        this.PossessedToken.Orientation.Z);
        }

        public override void Update(ref GameTime pGameTime)
        {
            //Random rnd = new Random();
            

            Vector3 tokenPosition = this.PossessedToken.Position;
          /*  if (tokenPosition.X >= 960 && this.mAquarium.ReachedHorizontalBoundary(this.PossessedToken))
            {
                mFacingDirection = -1;
                tokenPosition.X = 959;
            }
            if (tokenPosition.X <= -960 && this.mAquarium.ReachedHorizontalBoundary(this.PossessedToken))
            {
                mFacingDirection = 1;
                tokenPosition.X = -959;
            }*/

            if (mAquarium.ChickenLeg != null)
            {
                mSpeed += 5;
            }
            if (mAquarium.ChickenLeg != null && this.mAquarium.ReachedHorizontalBoundary(this.PossessedToken) == false)
            {
                if (tokenPosition.X <= mAquarium.ChickenLeg.Position.X)
                {
                    mFacingDirection = -1;
                }
                else if (tokenPosition.X > mAquarium.ChickenLeg.Position.X)
                {
                    mFacingDirection = 1;
                }
            }
            this.PossessedToken.Position = tokenPosition;
            HorizontalSwimBehaviour();
            if (this.PossessedToken.Name == "seahorse0" && justSpawned == true)
            {
                mSpeed = rnd.Next(1, 6);
            }

            if (this.PossessedToken.Name == "seahorse1" && justSpawned == true)
            {
                mSpeed = rnd.Next(1, 6);
                mSpeed = rnd.Next(1, 6);
            }

            if (this.PossessedToken.Name == "seahorse2" && justSpawned == true)
            {
                mSpeed = rnd.Next(1, 6);
                mSpeed = rnd.Next(1, 6);
                mSpeed = rnd.Next(1, 6);
            }

            if (justSpawned == true)
            {
                startY = tokenPosition.Y;
                justSpawned = false;
            }
            if (tokenPosition.X >= 960 && this.mAquarium.ReachedHorizontalBoundary(this.PossessedToken))
            {
                mFacingDirection = -1;
                tokenPosition.X = 959;
            }
            if (tokenPosition.X <= -960 && this.mAquarium.ReachedHorizontalBoundary(this.PossessedToken))
            {
                mFacingDirection = 1;
                tokenPosition.X = -959;
            }
            if (CanRise == true)
            {
                VerticalSwimBehaviourRise();
            }

            if (CanRise == false)
            {
                VerticalSwimBehaviourFall();
            }

            if (mAquarium.ChickenLeg != null)
            {
                mSpeed = 5;
            }
            justSpawned = false;



        }
        #endregion
    }
}
