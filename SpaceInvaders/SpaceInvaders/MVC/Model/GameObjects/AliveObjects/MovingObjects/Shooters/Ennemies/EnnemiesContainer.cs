using SpaceInvaders.GameObjects.Model.GameObjects.AliveObjects.Bunkers;
using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.Shooters.Ennemies;
using SpaceInvaders.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceInvaders.GameObjects.Shooters
{

    /// <summary>
    /// Manage the game objects
    /// - Creation of objects
    /// - Movement of ennemies
    /// </summary>
    class ObjectsContainer : GameObject
    {
        /// <summary>
        /// Store all the ennemies
        /// </summary>
        private readonly List<EnnemyObject> Ennemies = new List<EnnemyObject>();

        /// <summary>
        /// Store the user
        /// </summary>
        public readonly User User;

        /// <summary>
        /// Current direction
        /// </summary>
        private bool RightDirection = true;

        /// <summary>
        /// Create a random numbers of ennemies in the board
        /// </summary>
        /// <param name="user">the user</param>
        public ObjectsContainer() : base(Team.ENNEMY, new Vector2D(0, 0))
        {
            CreateEnnemies();
            CreateBunkers();
            this.User = CreateUser();
        }

        /// <summary>
        /// Create a few lines of ennemies
        /// </summary>
        private void CreateEnnemies()
        {
            for (int i = 1; i < 5; i++)
                AddLine(
                    (Vector2D src, Vector2D dst) => RandomEnnemy(src, dst),    // Create subclass of EnnemyObjects
                    i * (Game.Instance.GameSize.Height / 10),                  // Insert to [1/10, 4/10] game height
                    RandomNumbers.Randint(2, 5),                               // Ennemies number
                    Ennemies                                                   // Stock the created elements
                );
        }

        /// <summary>
        /// Create a lines of bunkers
        /// </summary>
        private void CreateBunkers()
        {
            AddLine(
                (Vector2D src, Vector2D dst) => new Bunker(dst),               // Create Bunkers
                3 * (Game.Instance.GameSize.Height / 5),                       // Insert to 3/5 game height
                RandomNumbers.Randint(2, 3)                                    // Bunkers number
            );
        }

        /// <summary>
        /// Create the user
        /// </summary>
        /// <returns>The user created</returns>
        private User CreateUser()
        {
            List<User> users = new List<User>();

            AddLine(
                (Vector2D src, Vector2D dst) => new User(dst),                 // Create Bunkers
                4 * (Game.Instance.GameSize.Height / 5),                       // Insert to 4/5 game height
                1,                                                             // Bunkers number
                users                                                          // Stock the created user
            );

            return users[0];
        }

        /// <summary>
        /// Create a line of ennemies in a specific height
        /// </summary>
        /// <param name="factory">a factory that create a instance (that extends GameObject)</param>
        /// <param name="actualHeight">the height to insert objects</param>
        /// <param name="entityNb">the number of instance that you want to create</param>
        /// <param name="list">a optional list to stock the created instances</param>
        private void AddLine<v>(
            Func<Vector2D, Vector2D, v> factory,
            int actualHeight,
            int entityNb,
            List<v> list = null
        ) where v : GameObject
        {
            int size = (int) GameException.RequireNonZero(entityNb) * 2;
            int dx = Game.Instance.GameSize.Width / size;

            for (int i = 1; i < size; i += 2)
            {
                v entity = factory(
                    new Vector2D(i * dx, 0),                                    // Begin coords
                    new Vector2D(i * dx, actualHeight)                          // Destination coords
                );
                Game.Instance.AddNewGameObject(entity);
                if (list != null) list.Add(entity);
            }

        }

        /// <summary>
        /// Create a random EnnemyObject
        /// </summary>>
        /// <param name="src">initial position of the ennemy</param>
        /// <param name="dst">destination to reach before horizontal movement</param>
        /// <returns>A object that extends EnnemyObject</returns>
        public static EnnemyObject RandomEnnemy(Vector2D src, Vector2D dst)
        {
            switch (RandomNumbers.Randint(0, 1))
            {
                case 0:
                    return new Ennemy1(src, dst);
                case 1:
                    return new Ennemy2(src, dst);
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Manage the ennemy directions
        /// </summary>
        /// <param name="gameInstance">the game instance</param>
        /// <param name="deltaT">deltaT</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            // Update the local Ennemies list (like the Game.Instance.GameObjects)
            Ennemies.RemoveAll(gameObject => !gameObject.IsAlive());

            // If the user is dead, the game is over
            if (!User.IsAlive()) Game.Instance.GameStateManager.FinishGame(false);

            // If the user is above an ennemy, the game is over
            if (Ennemies.Any(e => User.IsAbove(e))) Game.Instance.GameStateManager.FinishGame(false);

            // If all the ennemies are dead, the game is over
            if (!IsAlive()) Game.Instance.GameStateManager.FinishGame(true);


            ManageBlockMovement(gameInstance, deltaT);
        }

        /// <summary>
        /// Manage the movements of the ennmies with a block
        /// </summary>
        /// <param name="gameInstance">the game instance</param>
        /// <param name="deltaT">deltaT</param>
        private void ManageBlockMovement(Game gameInstance, double deltaT)
        {
            // True if any ennemy can't move in the current direction
            bool decalage = Ennemies.Any(e => !e.CanMove(gameInstance, deltaT, RightDirection, false));

            // If a ennemy can't move, change the direction of the ennemy group
            if (decalage) RightDirection = !RightDirection;

            foreach (EnnemyObject ennemy in Ennemies)
            {
                // True if an ennemy has not reached his objective
                // If True, he have to move down
                // Else, he have to move in the current direction
                bool isArrivedToDestination = ennemy.IsArrivedToDestination();

                // The user have to move down if the group direction 
                // has changed or if he don't reach his objective
                if (decalage || !isArrivedToDestination)
                {
                    ennemy.Move(gameInstance, deltaT, null, false);
                    if (isArrivedToDestination) ennemy.Accelerate();
                }
                // Else, he can move horizontally if he can't move
                else if (ennemy.CanMove(gameInstance, deltaT, RightDirection, null))
                {
                    ennemy.Move(gameInstance, deltaT, RightDirection, null);
                }
            }
        }

        /// <summary>
        /// Can't be in collision
        /// </summary>
        /// <param name="projectile">a projectile</param>
        /// <returns>Can the block be in collision ?</returns>
        public override bool CanCollision(ProjectileObject projectile)
        {
            return false;
        }

        /// <summary>
        /// Nothing to do
        /// </summary>
        /// <param name="projectile">a projectile</param>
        public override void OnCollision(ProjectileObject projectile) {}


        /// <summary>
        /// Nothing to draw
        /// </summary>
        /// <param name="gameInstance">the game instance</param>
        /// <param name="graphics">the graphics to draw in</param>
        public override void Draw(Game gameInstance, Graphics graphics) { }

        /// <summary>
        /// Finish the game if all the users are dead
        /// </summary>
        /// <returns>Does the block contain any enemies ?</returns>
        public override bool IsAlive()
        {
             return Ennemies.Count() != 0;
        }
    }
}
