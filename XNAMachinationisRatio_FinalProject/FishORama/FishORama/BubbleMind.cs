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
        private VolcanoToken mVolcano;
        



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

        public VolcanoToken Volcano
        {

            set { mVolcano = value; }
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
            // The first five bubbles are meant to react to the fish as they move from the fish's mouth
            Vector3 tokenPosition = this.PossessedToken.Position;
            if (this.PossessedToken.Name != "Bubble6")
            {
                this.PossessedToken.Position = mOrangeFish.Position;
                startY = mOrangeFish.Position.Y;
            }
            // The other bubble is to do with the Volcano
            else if (this.PossessedToken.Name == "Bubble6")
            {
                this.PossessedToken.Position = mVolcano.Position;
                startY = mVolcano.Position.Y;
            }
            justSpawned = true;

            #region Individual Bubble Checks
            // Rather messy way of givig each bubble a unique random speed each time they spawn
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
            if (this.PossessedToken.Name == "Bubble5" && justSpawned == true)
            {
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
            }
            if (this.PossessedToken.Name == "Bubble6" && justSpawned == true)
            {
                mSpeed = randomSpeed.Next(3, 6);
                mSpeed = randomSpeed.Next(3, 6);
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
           if (this.PossessedToken.Name != "Bubble6")
           {
               tokenPosition.X = tokenPosition.X + (((float)Math.Sin(i)) * 2);
           }
           else if (this.PossessedToken.Name == "Bubble6")
           {
               tokenPosition.X = tokenPosition.X + (((float)Math.Cos(i)) * 5);
           }
           this.PossessedToken.Position = tokenPosition;
           if (justSpawned == true && this.PossessedToken.Name != "Bubble6")
           {
               startY = mOrangeFish.Position.Y;
               justSpawned = false;
           }
           else if(justSpawned == true && this.PossessedToken.Name == "Bubble6")
           {
               startY = mVolcano.Position.Y;
               justSpawned = false;
           }

           if (tokenPosition.Y >= startY + 150 && this.PossessedToken.Name != "Bubble6")
           {

               BubbleRespawn();
           }

           else if (tokenPosition.Y >= startY + 550 && this.PossessedToken.Name == "Bubble6")
           {

               BubbleRespawn();
           }

        }
        #endregion
    }
}
