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
        private readonly HashSet<EnnemyObject> ennemies = new HashSet<EnnemyObject>();

        /// <summary>
        /// Current direction
        /// </summary>
        private bool right = true;

        /// <summary>
        /// Store the user
        /// </summary>
        private readonly User user;

        /// <summary>
        /// Create a random numbers of ennemies in the board
        /// </summary>
        /// <param name="gameInstance">the gameInstance</param>
        /// <param name="user">the user</param>
        public ObjectsContainer(Game gameInstance, User user) : base(Team.ENNEMY, new Vecteur2D(0, 0))
        {
            ennemies = new HashSet<EnnemyObject>();

            List<Func<Vecteur2D, Vecteur2D, EnnemyObject>> list = new List<Func<Vecteur2D, Vecteur2D, EnnemyObject>>
            {
                (Vecteur2D src, Vecteur2D dst) => new Ennemy1(src, dst),
                (Vecteur2D src, Vecteur2D dst) => new Ennemy2(src, dst),
                (Vecteur2D src, Vecteur2D dst) => new Ennemy1(src, dst),
                (Vecteur2D src, Vecteur2D dst) => new Ennemy2(src, dst)
            };

            for (int i = 0; i < list.Count; i++)
                AddLine(
                    gameInstance,
                    list[i],
                    RandomNumbers.Randint(1, 5),
                    i,
                    list.Count
                );

            foreach (EnnemyObject ennemy in ennemies)
                Game.game.AddNewGameObject(ennemy);

            this.user = user;
        }

        /// <summary>
        /// Create a line of ennemies
        /// </summary>
        private void AddLine(Game gameInstance, Func<Vecteur2D, Vecteur2D, EnnemyObject> createEnnemyFunction, int ennemiesNumber, int actualLine, int totalLines)
        {
            GameException.RequireNonNull(createEnnemyFunction);
            GameException.RequireNonNull(gameInstance);

            int xSpace = gameInstance.gameSize.Width / (ennemiesNumber + 1);
            int ySpace = (gameInstance.gameSize.Height / 2) / (totalLines + 1);

            for (int i = 1; i <= ennemiesNumber; i++)
            {
                ennemies.Add(
                    createEnnemyFunction(
                        new Vecteur2D(i * xSpace, (-totalLines + 1 + actualLine) * ySpace),
                        new Vecteur2D(i * xSpace, (actualLine + 1) * ySpace)
                    )
                );
            }
        }

        /// <summary>
        /// Nothing to draw
        /// </summary>
        public override void Draw(Game gameInstance, Graphics graphics) {}

        /// <summary>
        /// Finish the game if all the users are dead
        /// </summary>
        public override bool IsAlive()
        {
            bool alive = ennemies.Count() != 0;
            
            if (!alive) Game.game.gameStateManager.FinishGame(true);

            return alive;
        }

        /// <summary>
        /// Manage the ennemy directions
        /// </summary>
        public override void Update(Game gameInstance, double deltaT)
        {
            bool decalage = ennemies.Any(e => !e.CanMove(gameInstance, deltaT, right, false));
           
            if (decalage) right = !right;

            foreach (EnnemyObject ennemy in ennemies)
            {
                bool isArrivedToDestination = ennemy.IsArrivedToDestination();

                if (decalage || !isArrivedToDestination)
                {
                    ennemy.MoveDown(gameInstance, deltaT);
                    if (isArrivedToDestination) ennemy.Accelerate();
                }
                else if (ennemy.CanMove(gameInstance, deltaT, right, null))
                    ennemy.Move(gameInstance, deltaT, right, null);
            }

            ennemies.RemoveWhere(gameObject => !gameObject.IsAlive());

            if (decalage && ennemies.Any(e => user.IsAbove(e))) Game.game.gameStateManager.FinishGame(false);
        }

        /// <summary>
        /// Can't be in collision
        /// </summary>
        public override bool CanCollision(ProjectileObject projectile)
        {
            return false;
        }

        /// <summary>
        /// Nothing to do
        /// </summary>
        public override void OnCollision(ProjectileObject projectile) {}
    }
}
