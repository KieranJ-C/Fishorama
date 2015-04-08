﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;      // Required to use XNA features.
using XNAMachinationisRatio;        // Required to use the XNA Machinationis Ratio Engine.

namespace FishORama
{
    class BubbleToken : X2DToken
    {
        #region Data Members
        private AquariumToken mAquarium;  // Reference to the aquarium in which the creature lives.
        private BubbleMind mMind;       // Explicit reference to the mind the token is using to enact its behaviors.
        #endregion

        #region Properties
        // No custom properties yet.
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the Bubble.
        /// Uses base class to initialize the token name, and adds code to
        /// initialize custom members.
        /// </summary
        /// <param name="pTokenName">Name of the token.</param>
        /// <param name="pAquarium">Reference to the aquarium in which the token lives.</param>
        public BubbleToken(String pTokenName, AquariumToken pAquarium)
            : base(pTokenName) {
                mAquarium = pAquarium;          // Store reference to aquarium in which the creature is living.

        }

        #endregion

        #region Methods

        /// <summary>
        /// Setup default values for this token's porperties.
        /// </summary>
        protected override void DefaultProperties()
        {
            this.GraphicProperties.AssetID = "BubbleVisuals";
            BubbleMind myMind = new BubbleMind(this);
            mMind = myMind;     // Store explicit reference to mind being used.
            mMind.Aquarium = mAquarium;  
        }

        #endregion
    }
}