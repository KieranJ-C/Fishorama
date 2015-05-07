using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;      // Required to use XNA features.
using XNAMachinationisRatio;        // Required to use the XNA Machinationis Ratio Engine.
using XNAMachinationisRatio.AI;     // Required to use the XNA Machinationis Ratio general AI features


namespace FishORama
{
    /// <summary>
    /// Abstraction to represent the Submarine which moves peacefully in the aquarium.
    /// 
    /// This class is derived from class X2DToken. In the XNA Machinationis Ratio engine
    /// class X2DToken is a base class for all classes representing objects which
    /// have a visual representation and interactive behaviors in a 2D simulation.
    /// X2DToken implements a number of functionalities that make it easy for developers
    /// to add interactivity to objects minimizing the amount of coded required.
    /// 
    /// Hence, whenever we want to create a new type of object, we must create a new
    /// class derived from X2DToken.
    /// </summary>
    /// 
    class SubmarineToken : X2DToken
    {
        #region Data members

        // This token needs to interact with the aquarium to swim in it (it needs information
        // regarding the aquarium's boundaries). Hence, it needs a "link" to the aquarium,
        // which is why it stores in an instance variable a reference to its aquarium.
        private AquariumToken mAquarium;    // Reference to the aquarium in which the creature lives.

        private SubmarineMind mMind;       // Explicit reference to the mind the token is using to enact its behaviors.

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

        /// Constructor for the orange fish.
        /// Uses base class to initialize the token name, and adds code to
        /// initialize custom members.
        /// <param name="pTokenName">Name of the token.</param>
        /// <param name="pAquarium">Reference to the aquarium in which the token lives.</param>
        public SubmarineToken(String pTokenName, AquariumToken pAquarium)
            : base(pTokenName)
        {
            mAquarium = pAquarium;          // Store reference to aquarium in which the creature is living.
            mMind.Aquarium = mAquarium;     // Provide to the mind a reference to the aquarium, required to swim appropriately.
        }

        #endregion

        #region Methods

        /* LEARNING PILL: XNA Machinationis Ration token properties.
         * All tokens created through the XNA Machinationis Ratio engine have standard
         * attributes that define their behavior in a simulation. These standard
         * attributes can be initialized in a very efficient and simple way using
         * the DeafultProperties() method.
         */

        /// <summary>
        /// Setup default properties of the token.
        /// </summary>
        protected override void DefaultProperties()
        {

            // Specify which image should be associated to this token, assigning
            // the name of the graphic asset to be used ("OrangeFishVisuals" in this case)
            // to the property 'GraphicProperties.AssetID' of the token.
            this.GraphicProperties.AssetID = "SubmarineVisuals";

            // Specify mass of the fish. This can be used by
            // physics-based behaviors (work in progress, not functional yet).
            this.PhysicsProperties.Mass = 3;

            /* LEARNING PILL: Token behaviors in the XNA Machinationis Ratio engine
             * Some simulation tokens may need to enact specific behaviors in order to
             * participate in the simulation. The XNA Machinationis Ratio engine
             * allows a token to enact a behavior by associating an artificial intelligence
             * mind to it. Mind objects are created from subclasses of the class AIPlayer
             * included in the engine. In order to associate a mind to a token, a new
             * mind object must be created, passing to the constructor of the mind a reference
             * of the object that must be associated with the mind. This must be done in
             * the DefaultProperties method of the token. Upon creation of the mind, XNA
             * Machinationis Ratio automatically "injects" its into the token, establishing
             * a link which is not visible to the programmer (but it there!)
             * 
             * In this case, instances of the class OrangeFishToken can enact a simple swimming
             * behavior. The behavior is implemented through the class SimpleSwimMind.
             */

           SubmarineMind myMind = new SubmarineMind(this);   // Create mind, implicitly associating it to the token.

            Random sSpeed = new Random();
            mMind = myMind;     // Store explicit reference to mind being used.
            mMind.Aquarium = mAquarium;   // Provide to mind explicit reference to Aquarium.
        }

        #endregion

    }
}
