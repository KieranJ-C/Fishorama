﻿using System;
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
        //// This token needs to interact with the aquarium to swim in it (it needs information
        //// regarding the aquarium's boundaries). Hence, it needs a "link" to the aquarium,
        //// which is why it stores in an instance variable a reference to its aquarium.
        //private AquariumToken mAquarium;  // Reference to the aquarium in which the creature lives.
        //private ChickenLegMind mMind;       // Explicit reference to the mind the token is using to enact its behaviors.
        #endregion

        #region Properties
        // No custom properties yet.
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the chicken leg.
        /// Uses base class to initialize the token name, and adds code to
        /// initialize custom members.
        /// </summary
        /// <param name="pTokenName">Name of the token.</param>
        /// <param name="pAquarium">Reference to the aquarium in which the token lives.</param>
        public FootballToken(String pTokenName)
            : base(pTokenName) {
        }

        #endregion

        #region Methods
        protected override void DefaultProperties()
        {
            // Specify which image should be associated to this token, assigning
            // the name of the graphic asset to be used ("ChickenLegVisuals" in this case)
            // to the property 'GraphicProperties.AssetID' of the token.
            this.GraphicProperties.AssetID = "FootballVisuals";
        }

        #endregion
    }
}