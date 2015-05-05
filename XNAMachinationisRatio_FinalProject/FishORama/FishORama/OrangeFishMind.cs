﻿using System;
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
        private double TwoFinishTime;
        private double ThreeFinishTime;
        private int BehaviourNumber = 0;
        Random RandNumb = new Random();
        private int SinkPix;
        private int TPixSink;
        private float DashPix;


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

            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.X = tokenPosition.X + (mSpeed * mFacingDirection);
            this.PossessedToken.Position = tokenPosition;
            if (tokenPosition.X >= 350 || tokenPosition.X <= -350)
            {
                mFacingDirection = -mFacingDirection;
                if (tokenPosition.X >= 350)
                {
                    tokenPosition.X = 344;
                }
                if (tokenPosition.X <= -350)
                {
                    tokenPosition.X = -344;
                }
            }


            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                                        this.PossessedToken.Orientation.Y,
                                                        this.PossessedToken.Orientation.Z);
        }

        public void HungryHorizontalSwimBehaviour()
        {

            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.X = tokenPosition.X + (mSpeed * mFacingDirection);
            if (tokenPosition.X >= StartHX + 75 || tokenPosition.X <= StartHX - 75 || tokenPosition.X >= 350 || tokenPosition.X <= -350)
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
            Vector3 tokenPosition = this.PossessedToken.Position;
            tokenPosition.Y ++;
            this.PossessedToken.Position = tokenPosition;
            this.PossessedToken.Orientation = new Vector3(mFacingDirection,
                                                        this.PossessedToken.Orientation.Y,
                                                        this.PossessedToken.Orientation.Z);
        }

        public void RandomBehvaiour()
        {
            int BehaviourRandomNum = RandNumb.Next(1, 5);
            BehaviourNumber = BehaviourRandomNum;
        }

        public void SinkPixRand()
        {
            int TPixSinkNum = RandNumb.Next(50, 150);
            TPixSink = TPixSinkNum;
        }

        public override void Update(ref GameTime pGameTime)
        {
            Vector3 tokenPosition = this.PossessedToken.Position;
            currTime = pGameTime.TotalGameTime.TotalSeconds;

            #region NewLoop
            if (Newloop == true)
            {
                StartHX = tokenPosition.X;
                StartTime = pGameTime.TotalGameTime.TotalSeconds;
                TwoFinishTime = StartTime + 15;
                ThreeFinishTime = StartTime + 5;
                SinkPixRand();
                DashPix = 0;
                SinkPix = 0;
                Newloop = false;
            }
            #endregion

            #region Default

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
                    BehaviourNumber = 0;
                }
            }
            #endregion

            #region Acceleration
            else if (BehaviourNumber == 2)
            {
                HorizontalSwimBehaviour();
                if (currTime >= TwoFinishTime)
                {
                    BehaviourNumber = 0;
                }
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
                    BehaviourNumber = 0;
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
                    BehaviourNumber = 0;
                }
                else if (tokenPosition.Y <= -240)
                {
                    BehaviourNumber = 0;
                }
            }

            #endregion
        }

    }
    }
        #endregion