using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio general features.
using XNAMachinationisRatio.Resource;       // Required to use the XNA Machinationis Ratio resource management features.

/* LEARNING PILL: virtual world space and graphics in FishORama
 * In the Machinationis Ratio engine every object has graphic a position in the
 * virtual world expressed through a 3D vector (represented via a Vector3 object).
 * In 2D simulations the first coordinate of the vector is the horizontal (X)
 * position, the second coordinate (Y) represents the vertical position, and 
 * the third coordinate (Z) represents the depth. All simulation features are
 * based on world coordinates.
 * 
 * At any time, a portion of the scene is visible through a camera object (in FishO'Rama 
 * this is created, initialized and referenced through the Kernel class). For the purpose
 * of visualization, coordinates may also be expressed relative to the camera
 * origin (i.e. the center of the camera). In FishO'Rama the camera is centered on the
 * origin of the world, which makes camera coordinates coinciding with world coordinates.
 * This greatly simplifies all the operations.
 * 
 * The third coordinate of the world position of an object represents the depth, i.e.
 * how close an object is to the camera. This defines which objects are in front of others 
 * (for instance, an object with Z=3 will always be drawn in front of an object with
 * Z=2).
 */

namespace FishORama
{
    /// <summary>
    /// Kernel (orchestrator class) for this application.
    /// </summary>
    public class Kernel : XNAGame
    {

        #region Data Members

        /* LEARNING PILL: XNA Machinationis Ratio Scene
         * In order to manage visual tokens with the XNA Machinationis Ratio
         * engine, a scene must be created, and tokens must be placed into it.
         * 
         * More specifically, the main simulation application (represented
         * by class Kernel in the case of FishORama) must create, initialize
         * and store an I2DScene instance in one of its instance variables
         * (instance variable mScene in the case of FishORama).
         */
        
        I2DScene mScene = null;     // Reference to the FishORama scene, set to null before its initialization.
                                    // Creation and initialization is performed in the LoadContent method.

        /* LEARNING PILL: XNA Machinationis Ratio Camera
         * Just like it happens for movies, XNA Machinationis Ratio visualizes
         * a scene displaying it through a camera object. Hence, simulations using
         * XNA Machinationis Ratio must always create and use a camera and set it
         * up to allow the engine to display the simulation graphics. Once the camera
         * object has appropiately been created and initialized, the visual rendering
         * of the scene (i.e. the visualization of the scene) is performed 'under the hood'
         * by the engine, without any need for developers to write specific code.
         * 
         * More specifically, the main simulation application (represented
         * by class Kernel in the case of FishORama) must create, initialize
         * and store an I2DCamera instance in one of its instance variables
         * (instance variable mCamera in the case of FishORama).
         */


        I2DCamera mCamera = null;   // Reference to the FishORama camera, set to null before its initialization.
                                    // Creation and initialization is performed in the LoadContent method.


        #endregion

        #region Properties

        /// <summary>
        /// Get simulation scene.
        /// </summary>
        public I2DScene Scene
        {
            get { return mScene; }
        }

        /// <summary>
        /// Get simulation camera.
        /// </summary>
        public I2DCamera Camera
        {
            get { return mCamera; }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Kernel(): base("FishO'Rama")
        {
            this.IsMouseVisible = true;     // Display mouse cursor.
        }
        
        #endregion


        #region Methods

        /* LEARNING PILL: XNA Machinationis Ratio asset library
         * In order to work, simulations may require media assets such as
         * images used to display tokens. In order to load and manage assets
         * XNA Machinationis Ratio uses the class AssetLibrary, and the related
         * methods.
         */

        /// <summary>
        /// Create library of graphic assets.
        /// </summary>
        /// <returns>Library.</returns>
        protected override AssetLibrary GetAssetLibrary()
        {
            AssetLibrary lib = AssetLibrary.CreateAnEmptyLibrary();             // New asset library.
            X2DAsset A = null;                                                  // Temporary variable used to create graphic assets.
            
            /* LEARNING PILL: Creation of new graphic assets with XNA Machinationis ratio
             * Tokens with a visual representation must have an associated visual asset.
             * In the XNA Machinationis Ratio engine, graphic assets are created using the
             * class X2DAsset, based on images imported in the XNA contents pipeline.
             * After creation assets are imported in the simulation assets library.
             * Assets are created specifying:
             * - ID of the asset and ID of XNA resource in which the asset is contained (useful to support spritesheets).
             * - The local origin of the asset (in this case, the center of the image). It will be used to
             *   position and move it in the scene.
             * - The position of the asset in the XNA resource (from top-left corner of the image).
             * - The width of the asset.
             * - The height of the asset.
             * 
             * COMP2134 NOTE: all the required assets have already been loaded in the solution,
             * and are available to create graphic library assets using their assigned ID.
             */

            // Create a new graphic asset  for the aquarium visuals using class X2DAsset.
            A = new X2DAsset("AquariumVisuals", "AquariumBackground"). 
                UVOriginAt(960, 540).
                UVTopLeftCornerAt(0, 0).
                Width(1920).
                Height(1080); 
            
            // Import aquarium visual asset in the library.
            lib.ImportAsset(A);

            // Create a new graphic asset for the first progress marker visuals using class X2DAsset.
            A = new X2DAsset("ChickenLegVisuals", "ChickenLeg").
                UVOriginAt(64, 64).
                UVTopLeftCornerAt(0, 0).
                Width(128).
                Height(128);

            // Import first marker visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset for the first progress marker visuals using class X2DAsset.
            A = new X2DAsset("SignalVisuals", "Signal").
                UVOriginAt(64, 64).
                UVTopLeftCornerAt(0, 0).
                Width(128).
                Height(128);

            // Import first marker visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset for the first progress marker visuals using class X2DAsset.
            A = new X2DAsset("MarkerVisuals1", "Marker1").
                UVOriginAt(32, 15).
                UVTopLeftCornerAt(0, 0).
                Width(64).
                Height(30);

            // Import first marker visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset for the second progress marker visuals using class X2DAsset.
            A = new X2DAsset("MarkerVisuals2", "Marker2").
                UVOriginAt(32, 15).
                UVTopLeftCornerAt(0, 0).
                Width(64).
                Height(30);

            // Import second marker visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset  for the orange fish visuals using class X2DAsset.
            A = new X2DAsset("OrangeFishVisuals", "OrangeFish").
                UVOriginAt(64, 64).
                UVTopLeftCornerAt(0, 0).
                Width(128).
                Height(128);

            // Import orange fish visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset  for the piranha visuals using class X2DAsset.
            A = new X2DAsset("PiranhaVisuals", "Piranha").
                UVOriginAt(64, 64).
                UVTopLeftCornerAt(0, 0).
                Width(128).
                Height(128);

            // Import piranha visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset  for the first piranha visuals using class X2DAsset.
            A = new X2DAsset("PiranhaVisuals1", "Piranha1").
                UVOriginAt(64, 64).
                UVTopLeftCornerAt(0, 0).
                Width(128).
                Height(128);

            // Import first piranha visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset  for the second piranha visuals using class X2DAsset.
            A = new X2DAsset("PiranhaVisuals2", "Piranha2").
                UVOriginAt(64, 64).
                UVTopLeftCornerAt(0, 0).
                Width(128).
                Height(128);

            // Import second piranha visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset  for the Seahorse visuals using class X2DAsset.
            A = new X2DAsset("SeahorseVisuals", "seahorse").
                UVOriginAt(64, 64).
                UVTopLeftCornerAt(0, 0).
                Width(128).
                Height(128);

            // Import Seahorse visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset  for the Bubble visuals using class X2DAsset.
            A = new X2DAsset("BubbleVisuals", "Bubble").
                UVOriginAt(16, 16).
                UVTopLeftCornerAt(0, 0).
                Width(32).
                Height(32);

            // Import Bubble visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset  for the Bubble visuals using class X2DAsset.
            A = new X2DAsset("BubbleVisuals2", "Bubble2").
                UVOriginAt(32, 32).
                UVTopLeftCornerAt(0, 0).
                Width(64).
                Height(64);

            // Import Bubble visual asset in the library
            lib.ImportAsset(A);

            // Create Submarine visuals using class X2DAsset.
            A = new X2DAsset("SubmarineVisuals", "Submarine").
                UVOriginAt(64, 32).
                UVTopLeftCornerAt(0, 0).
                Width(128).
                Height(64);

            // Import Submarine visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset  for the Football visuals using class X2DAsset.
            A = new X2DAsset("FootballVisuals", "Football").
                UVOriginAt(32, 32).
                UVTopLeftCornerAt(0, 0).
                Width(64).
                Height(64);

            // Import Football visual asset in the library
            lib.ImportAsset(A);

            // Create a new graphic asset  for the Volcano visuals using class X2DAsset.
            A = new X2DAsset("VolcanoVisuals", "Volcano").
                UVOriginAt(256, 256).
                UVTopLeftCornerAt(0, 0).
                Width(512).
                Height(512);

            // Import Football visual asset in the library
            lib.ImportAsset(A);


            // Return library.
            return lib;
        }

        /// <summary>
        /// Load contents for the simulation.
        /// LoadContent will be called only once, at the beginning of the simulation,
        /// is the place to load all of your content (e.g. graphics and sounds).
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();



            // Instantiate and initialize scene, specifying its horizontal size (800)
            // and vertical size (600).
            // Note, the third parameter is set to 0 because unused in FishORama.
            mScene = XNAGame.CreateA2DScene(800, 600, 0);

            /*
             * Create Tokens
             */

            AquariumToken aquarium = new AquariumToken("Aquarium", this, 1920, 1080);         // Create aquarium token.

             /* LEARNING PILL: placing tokens in a scene.
             * In order to be managed by the Machinationis Ratio engine, tokens must be placed
             * in a scene.
             * 
             * In FishORama the procedure for the creation and placement of tokens that must be
             * placed in the scene at startup is carried out byn the method LoadContent of
             * class Kernel.
             * Tokens can also be created in runtime by any method of any class, provided that
             * the method has access to the simulation scene object encapsulated in class Kernel.
             * This object can be accessed through the property Scene of class Kernel.
             */

            /*
             * Place tokens in the scene.
             */

            Vector3 tokenPos;        // Empty Vector3 object to be used to position tokens.
  

            tokenPos = new Vector3(0, 0, 0);            // Define scene position for the aquarium.
            mScene.Place(aquarium, tokenPos);           // Place token in scene.

            Vector3 fishPos;
            OrangeFishToken orangeFish1 = new OrangeFishToken("OrangeFish", aquarium); 

            fishPos =  new Vector3(200, -150, 1);
            mScene.Place(orangeFish1, fishPos);

            
             /*
             * Create and Initialize camera
             */

            // Define the position for the camera as a 3D vector object, created as a new
            // instance of class Vector3, and initialized to (0, 0, 1),
            // which means that in FishORama it is centered on the origin of the world.
            Vector3 camPosition = new Vector3(0, 0, 1);

            // Instantiate and initialize camera, specifying its ID ("Camera 01"
            // in this case), and its position (camPosition in this case).
            mCamera = mScene.CreateCameraAt("Camera 01", camPosition);

            //Startup the visualization, giving the "...and ACTION!" directive.
            this.PlayScene(mScene);

            // Creates a random number between -320 and 320 for the X co-ordiantes and -200 and 200 for the Y co-ordinates
            Random Piranharandom = new Random();
            int PiranharandomX = Piranharandom.Next(-320, 320);
            int PiranharandomY = Piranharandom.Next(-200, 200);

            // My First Variables
            PiranhaToken Piranha = new PiranhaToken("Piranha", aquarium);
            tokenPos = new Vector3(PiranharandomX, PiranharandomY, 1);
            mScene.Place(Piranha, tokenPos);

            // Had to split this up to allow bubbles to have access to "Volcano" but I place it on scene last so it is behind everyting but the background image. 
            VolcanoToken Volcano = new VolcanoToken("Volcano", aquarium);


            List<BubbleToken> Blist = new List<BubbleToken>();
            Random rand = new Random();

            for (int i = 0; i < 7; i++)
            {

                Blist.Add(new BubbleToken("Bubble" + i, aquarium, orangeFish1, Volcano));

            }

            for (int i = 0; i < Blist.Count; i++)
            {
                if (i < 6)
                {
                    float x = fishPos.X;
                    float y = fishPos.Y + (i * 4);
                    float z = 2;
                    tokenPos = new Vector3(x, y, z);
                    mScene.Place(Blist[i], tokenPos);
                }
                else if (i == 6)
                {
                    float x = -710;
                    float y = -320;
                    float z = 2;
                    tokenPos = new Vector3(x, y, z);
                    mScene.Place(Blist[i], tokenPos);
                }

            }
            
            // This makes a list then you use the list to place all the contents of the list onto the scene
            List<SeahorseToken> SHlist = new List<SeahorseToken>();
            for(int i = 0; i <3; i++)
            {
            
                SHlist.Add(new SeahorseToken("seahorse"+i, aquarium));
            
            }

            for (int i = 0; i < SHlist.Count; i++)  
            {
                int x = rand.Next(-500, 500);
                int y = rand.Next(-400, 400);
                int z = 1;
               
                
                tokenPos = new Vector3(x, y, z);
                mScene.Place(SHlist[i], tokenPos);
            }

            SubmarineToken Submarine = new SubmarineToken("Submarine", aquarium);
            tokenPos = new Vector3(-1000, 0, 1);
            mScene.Place(Submarine, tokenPos);

            TargetToken Football = new TargetToken("Football", aquarium);
            tokenPos = new Vector3(0, -260, 1);
            mScene.Place(Football, tokenPos);

            tokenPos = new Vector3(-710, -320, 1);
            mScene.Place(Volcano, tokenPos);
 }  
                
                
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Added a way to close the game without Alt + F4, Escape is a common industy standard
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            // Perform standard update operations.
            base.Update(gameTime);
        }

        #endregion
    }
}
