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
        Random speed = new Random();
        private int mSpeed;
        private float startY;

        private bool justSpawned = true;
        private double i = 0;
        Random randomSpeed = new Random();


        private OrangeFishToken mOrangeFish;
        



        #endregion

        #region Properties
        public AquariumToken Aquarium
        {
            set { mAquarium = value; }
        }

        public OrangeFishToken orangeFish1
        {

            set { mOrangeFish = value; }
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
            Vector3 tokenPosition = this.PossessedToken.Position;
            startY = tokenPosition.Y;
            
            mSpeed = speed.Next(3, 6);
        }

        #endregion

        #region Methods
        //When bubbles reach the top of the screen there speed is randomised and there position is adjusted and their Y position checked so that
        //the amount they rise can be measured so that they can respawn again after 150 height gain
        public void BubbleRespawn()
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            this.PossessedToken.Position = mOrangeFish.Position ;
            startY = mOrangeFish.Position.Y;
            justSpawned = true;

            #region Individual Bubble Checks
            if (this.PossessedToken.Name == "Bubble0" && justSpawned == true)
            {
                mSpeed = randomSpeed.Next(3, 6);
            }
            if (this.PossessedToken.Name == "Bubble1" && justSpawned == true)
            {
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
            }
            if (this.PossessedToken.Name == "Bubble2" && justSpawned == true)
            {
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
            }
            if (this.PossessedToken.Name == "Bubble3" && justSpawned == true)
            {
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
            }
            if (this.PossessedToken.Name == "Bubble4" && justSpawned == true)
            {
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
            }
            #endregion
            
        }


        public override void Update(ref GameTime pGameTime)
        {
           Vector3 tokenPosition = this.PossessedToken.Position;
            i += 0.1;
           tokenPosition.Y = tokenPosition.Y + (mSpeed);
           tokenPosition.X = tokenPosition.X + ((float)Math.Sin(i));
           this.PossessedToken.Position = tokenPosition;
           if (justSpawned == true)
           {
               startY = mOrangeFish.Position.Y;
               justSpawned = false;
           }

           if (tokenPosition.Y >= startY + 150)
           {

               BubbleRespawn();
           }

        }
        #endregion
    }
}
