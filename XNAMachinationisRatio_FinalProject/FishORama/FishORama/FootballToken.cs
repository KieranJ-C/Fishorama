using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;      // Required to use XNA features.
using XNAMachinationisRatio;        // Required to use the XNA Machinationis Ratio Engine.

namespace FishORama
{

    class FootballToken : X2DToken
    {
        #region Data Members
        private AquariumToken mAquarium;  // Reference to the aquarium in which the creature lives.
        private FootballMind mMind;       // Explicit reference to the mind the token is using to enact its behaviors.
        #endregion

        #region Properties
        // No custom properties yet.
        #endregion

        #region Constructors
        /// <param name="pTokenName">Name of the token.</param>
        /// <param name="pAquarium">Reference to the aquarium in which the token lives.</param>
        public FootballToken(String pTokenName, AquariumToken pAquarium)
            : base(pTokenName)
        {
            mAquarium = pAquarium;          // Store reference to aquarium in which the creature is living.
        }

        #endregion

        #region Methods
        protected override void DefaultProperties()
        {
            // Specify which image should be associated to this token, assigning
            // the name of the graphic asset to be used ("ChickenLegVisuals" in this case)
            // to the property 'GraphicProperties.AssetID' of the token.
            this.GraphicProperties.AssetID = "FootballVisuals";
            FootballMind myMind = new FootballMind(this);
            mMind = myMind;     // Store explicit reference to mind being used.
            mMind.Aquarium = mAquarium; 
        }

        #endregion
    }
}