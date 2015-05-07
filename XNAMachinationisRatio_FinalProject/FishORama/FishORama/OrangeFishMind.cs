using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;              // Required to use XNA features.
using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio Engine general features.
using XNAMachinationisRatio.AI;             // Required to use the XNA Machinationis Ratio general AI features.

namespace FishORama
{
    class OrangeFishMind : AIPlayer
    {
        #region Data Members
        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.
        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).
        private float mSpeed = 5;
        private double currTime;
        private bool Newloop = true;
        private float StartHX;
        private double StartTime;
        private double FinishTime;
        private double TwoFinishTime;
        private double ThreeFinishTime;
        private double FourFinishTime;
        private int BehaviourNumber = 2;
        Random RandNumb = new Random();
        private int SinkPix;
        private int TPixSink;
        private float DashPix;
        private float i = 0;

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
        public OrangeFishMind(X2DToken pToken)
        {
            /* LEARNING PILL: associating a mind with a token
             * In order for a mind to control a token, it must be associated with the token.
             * This is done when the mind is constructed, using the method Possess inherited
             * from class AIPlayer.
             */
            this.Possess(pToken);       // Possess token.
            mFacingDirection = 1;       // Current direction the fish is facing.
        }

        #endregion

        #region Methods
        /// <summary>
        /// AI Update method.
        /// </summary>
        /// <param name="pGameTime">Game time</param>
        public void HorizontalSwimBehaviour()
        {
            // This is the base fish behaviour of swimming left and right is used in Dash and acceleration
            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.X = tokenPosition.X + (mSpeed * mFacingDirection);
            this.PossessedToken.Position = tokenPosition;
            if (this.mAquarium.ReachedHorizontalBoundary(this.PossessedToken))
            {
                mFacingDirection = -mFacingDirection;
            }


            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                                        this.PossessedToken.Orientation.Y,
                                                        this.PossessedToken.Orientation.Z);
        }

        public void HungryHorizontalSwimBehaviour()
        {
            // This is the behaviour to allow the fish to swim left and right when it is hungry
            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.X = tokenPosition.X + (mSpeed * mFacingDirection);
            if (tokenPosition.X >= StartHX + 75 || tokenPosition.X <= StartHX - 75 || this.mAquarium.ReachedHorizontalBoundary(this.PossessedToken))
            {
                mFacingDirection = -mFacingDirection;

            }

            this.PossessedToken.Position = tokenPosition;
            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                                        this.PossessedToken.Orientation.Y,
                                                        this.PossessedToken.Orientation.Z);
        }

        
        public void VerticalSwimBehaviour()
        {
            // This is the behaviour that allows the fish to swim up and down
            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.Y ++;
            this.PossessedToken.Position = tokenPosition;
            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                                        this.PossessedToken.Orientation.Y,
                                                        this.PossessedToken.Orientation.Z);
        }

        public void RandomBehvaiour()
        {
            // This gives the fish a random behvaiour.
            int BehaviourRandomNum = RandNumb.Next(1, 6);
            BehaviourNumber = BehaviourRandomNum;
        }

        public void SinkPixRand()
        {
            // This is how far down the fish goes when the Sink behaviour occurs.
            int TPixSinkNum = RandNumb.Next(50, 150);
            TPixSink = TPixSinkNum;
        }

        public override void Update(ref GameTime pGameTime)
        {
            // If the Fish does get stuck at the edge of the screen then after about 10 seconds on average it becomes unstuck.
            Vector3 tokenPosition = this.PossessedToken.Position;
            currTime = pGameTime.TotalGameTime.TotalSeconds;
            Console.WriteLine(BehaviourNumber);

            #region NewLoop
            if (Newloop == true)
            {
                // This resets all neccessary variables for any new behaviour. 
                StartHX = tokenPosition.X;
                StartTime = pGameTime.TotalGameTime.TotalSeconds;
                FinishTime = StartTime + 2;
                TwoFinishTime = StartTime + 15;
                ThreeFinishTime = StartTime + 5;
                FourFinishTime = StartTime + 30;
                SinkPixRand();
                DashPix = 0;
                SinkPix = 0;
                i = 0;
                Newloop = false;
            }
            #endregion

            #region Reset
            // Unused just for testing purposes.
            if (BehaviourNumber == 0)
            {
                if (tokenPosition.X == 0 && tokenPosition.Y == 0)
                {
                    RandomBehvaiour();
                    Newloop = true;
                }

                if (tokenPosition.X > 0)
                {
                    tokenPosition.X--;
                    mFacingDirection = -1;
                }
                else if (tokenPosition.X < 0)
                {
                    tokenPosition.X++;
                    mFacingDirection = 1;
                }
                if (tokenPosition.Y > 0)
                {
                    tokenPosition.Y--;
                }
                else if (tokenPosition.Y < 0)
                {
                    tokenPosition.Y++;
                }
                this.PossessedToken.Position = tokenPosition;
                this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                                            this.PossessedToken.Orientation.Y,
                                                            this.PossessedToken.Orientation.Z);
            }

            #endregion

            #region Dash
            if (BehaviourNumber == 1)
            {
                mSpeed += 10;
                DashPix += mSpeed;
                HorizontalSwimBehaviour();
                mSpeed -= 10;
                if(DashPix >= 250)
                {
                    BehaviourNumber = 5;
                }
            }
            #endregion

            #region Acceleration
            else if (BehaviourNumber == 2)
            {
                // It does 60 updates per second so every 60 updates it changes the speed by 1 if it hasn't been 15 seconds it increases the speed,
                // if it has been more than 15 seconds it decreases the speed by 1.
                i++;
                if (i == 60)
                {
                    if (currTime < TwoFinishTime)
                    {
                       mSpeed++;
                       i = 0;
                    }
                    if (currTime > TwoFinishTime && currTime <= FourFinishTime)
                    {
                        mSpeed--;
                        i = 0;
                    }
                }
                if (currTime > FourFinishTime)
                {
                    BehaviourNumber = 5;
                }
                HorizontalSwimBehaviour();

            }
            #endregion

            #region Hungry
            else if (BehaviourNumber == 3)
            {
                this.PossessedToken.Position = tokenPosition;
                if (tokenPosition.X <= StartHX + 75 || tokenPosition.X >= StartHX - 75)
                {
                    HungryHorizontalSwimBehaviour();
                }

                if (tokenPosition.Y <= 250)
                {
                    VerticalSwimBehaviour();
                }
                if (currTime >= ThreeFinishTime)
                {
                    BehaviourNumber = 5;
                }
            }
            #endregion

            #region Sink
            else if (BehaviourNumber == 4)
            {
                if (tokenPosition.Y >= -240)
                {
                    tokenPosition.Y -= 5;
                }
                this.PossessedToken.Position = tokenPosition;
                SinkPix += 5;

                if(SinkPix >= TPixSink)
                {
                    BehaviourNumber = 5;
                }
                else if (tokenPosition.Y <= -240)
                {
                    BehaviourNumber = 5;
                }
            }

            #endregion

            #region Default
            else if (BehaviourNumber == 5)
            {
                HorizontalSwimBehaviour();
                if (currTime >= FinishTime)
                {
                    Newloop = true;
                    RandomBehvaiour();
                }
            }



            #endregion
        }

    }
    }
        #endregion