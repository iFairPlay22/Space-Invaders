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
    /// </summary>
    class ObjectsContainer : GameObject
    {
        /// <summary>
        /// Store all the ennemies
        /// </summary>
        private readonly HashSet<EnnemyObject> Ennemies = new HashSet<EnnemyObject>();

        /// <summary>
        /// Store the user
        /// </summary>
        private readonly User User;

        /// <summary>
        /// Current direction
        /// </summary>
        private bool RightDirection = true;

        /// <summary>
        /// Create a random numbers of ennemies in the board
        /// </summary>
        /// <param name="user">the user</param>
        public ObjectsContainer(User user) : base(Team.ENNEMY, new Vector2D(0, 0))
        {
            // Create ennemies

            for (int i = 1; i < 5; i++)
                AddLine(
                    (Vector2D src, Vector2D dst) => RandomEnnemy(src, dst),    // Create subclass of EnnemyObjects
                    i * (Game.Instance.GameSize.Height / 10),                  // Insert to [1/10, 4/10] game height
                    RandomNumbers.Randint(2, 5),                               // Ennemies number
                    Ennemies
                );

            // Create bunkers

            AddLine(
                (Vector2D src, Vector2D dst) => new Bunker(dst),            // Create Bunkers
                3 * (Game.Instance.GameSize.Height / 5),                    // Insert to 3/5 game height
                RandomNumbers.Randint(2, 3)                                 // Bunkers number
            );

            this.User = user;
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
            HashSet<v> list = null
        ) where v : GameObject
        {
            int size = (int) GameException.RequireNonZero(entityNb) * 2;
            int dx = Game.Instance.GameSize.Width / size;
            int startX = -(Game.Instance.GameSize.Height - actualHeight);

            for (int i = 1; i < size; i += 2)
            {
                v entity = factory(
                    new Vector2D(i * dx, startX),           // Begin coords
                    new Vector2D(i * dx, actualHeight)      // Destination coords
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
        /// Nothing to draw
        /// </summary>
        /// <param name="gameInstance">the game instance</param>
        /// <param name="graphics">the graphics to draw in</param>
        public override void Draw(Game gameInstance, Graphics graphics) {}

        /// <summary>
        /// Finish the game if all the users are dead
        /// </summary>
        /// <returns>Does the block contain any enemies ?</returns>
        public override bool IsAlive()
        {
            bool alive = Ennemies.Count() != 0;
            
            if (!alive) Game.Instance.GameStateManager.FinishGame(true);

            return alive;
        }

        /// <summary>
        /// Manage the ennemy directions
        /// </summary>
        /// <param name="gameInstance">the game instance</param>
        /// <param name="deltaT">deltaT</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            bool decalage = Ennemies.Any(e => !e.CanMove(gameInstance, deltaT, RightDirection, false));
           
            if (decalage) RightDirection = !RightDirection;

            foreach (EnnemyObject ennemy in Ennemies)
            {
                bool isArrivedToDestination = ennemy.IsArrivedToDestination();

                if (decalage || !isArrivedToDestination)
                {
                    ennemy.MoveDown(gameInstance, deltaT);
                    if (isArrivedToDestination) ennemy.Accelerate();
                }
                else if (ennemy.CanMove(gameInstance, deltaT, RightDirection, null))
                    ennemy.Move(gameInstance, deltaT, RightDirection, null);
            }

            Ennemies.RemoveWhere(gameObject => !gameObject.IsAlive());

            if (decalage && Ennemies.Any(e => User.IsAbove(e))) Game.Instance.GameStateManager.FinishGame(false);
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
    }
}
