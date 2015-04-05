using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;              // Required to use XNA features.
using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio Engine general features.
using XNAMachinationisRatio.AI;             // Required to use the XNA Machinationis Ratio general AI features.



namespace FishORama
{
    class BubbleMind : AIPlayer
    {
        #region Data Members

        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).
        private float mSpeed = 5;
        private bool CanRise = true;
        private Vector3 start;
        private float startY;

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
        public BubbleMind(X2DToken pToken)
        {
            this.Possess(pToken);       // Possess token.
            mFacingDirection = 1;       // Current direction the fish is facing.
            Vector3 tokenPosition = this.PossessedToken.Position;
            
            startY = OrangeFishToken.position.Y;


        }

        #endregion

        #region Methods

        /// <summary>
        /// AI Update method.
        /// </summary>
        /// <param name="pGameTime">Game time</param>

      /*  public void HorizontalSwimBehaviour()
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
        }*/

        public void VerticalSwimBehaviourRise()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.Y = tokenPosition.Y + mSpeed;
            this.PossessedToken.Position = tokenPosition;
            if (tokenPosition.Y >= (startY + 400) || tokenPosition.Y >= 600)
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
            Vector3 tokenPosition = this.PossessedToken.Position;
            this.PossessedToken.Position = tokenPosition;
            //HorizontalSwimBehaviour();
            if (CanRise == true)
            {
                VerticalSwimBehaviourRise();
            }

            if (CanRise == false)
            {
                VerticalSwimBehaviourFall();
            }



        }
        #endregion
    }
}
