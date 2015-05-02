using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;              // Required to use XNA features.
using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio Engine general features.
using XNAMachinationisRatio.AI;             // Required to use the XNA Machinationis Ratio general AI features.



namespace FishORama
{
    class FootballMind : AIPlayer
    {
        #region Data Members

        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).
        private int mSpeed;
        private Vector3 startPos;




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
           Vector3 tokenPosition = this.PossessedToken.Position;
           tokenPosition.X = tokePosition.X + 5;
           this.PossessedToken.Position = tokenPosition;
        }
        #endregion
    }
}
