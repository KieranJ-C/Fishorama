using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;      // Required to use XNA features.
using XNAMachinationisRatio;        // Required to use the XNA Machinationis Ratio Engine.
namespace FishORama
{
    class VolcanoToken : X2DToken
    {
        #region Data Members
        private AquariumToken mAquarium;    // Reference to the aquarium in which the creature lives.
        #endregion

        #region Properties
        /// <summary>
        /// Get aquarium in which the creature lives.
        /// </summary>
        public AquariumToken Aquarium
        {
            get { return mAquarium; }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the Volcano.
        /// Uses base class to initialize the token name, and adds code to
        /// initialize custom members.
        /// </summary
        /// <param name="pTokenName">Name of the token.</param>
        /// <param name="pAquarium">Reference to the aquarium in which the token lives.</param>
        public VolcanoToken(String pTokenName, AquariumToken pAquarium)
            : base(pTokenName)
        {
            mAquarium = pAquarium;          // Store reference to aquarium in which the creature is living.
        }

        #endregion

        #region Methods
        /// <summary>
        /// Setup default values for this token's porperties.
        /// </summary>
        protected override void DefaultProperties()
        {
            this.GraphicProperties.AssetID = "VolcanoVisuals";
        }

        #endregion
    }
}