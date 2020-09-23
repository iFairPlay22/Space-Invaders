using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.Shooters.Ennemies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;

namespace SpaceInvaders.GameObjects.Shooters
{
    class EnnemyContainer : GameObject
    {
        private readonly HashSet<EnnemyObject> ennemies = new HashSet<EnnemyObject>();

        private bool right = true;

        public EnnemyContainer(Game gameInstance) : base(Team.ENNEMY, new Vecteur2D(0, 0))
        {
            ennemies = new HashSet<EnnemyObject>();


            AddLine(
                gameInstance,
                (Vecteur2D src, Vecteur2D dst) => new Ennemy2(src, dst),
                2,
                1
            );

            AddLine(
                gameInstance, 
                (Vecteur2D src, Vecteur2D dst) => new Ennemy1(src, dst), 
                4,
                0
            );

            foreach (EnnemyObject ennemy in ennemies)
                Game.game.AddNewGameObject(ennemy);
        }

        private void AddLine(Game gameInstance, Func<Vecteur2D, Vecteur2D, EnnemyObject> createEnnemyFunction, int ennemiesNumber, int index)
        {
            GameException.RequireNonNull(createEnnemyFunction);
            GameException.RequireNonNull(gameInstance);

            int width = gameInstance.gameSize.Width;
            int xSpace = width / (ennemiesNumber + 1);
            int ySpace = -index * 100;

            for (int i = 1; i <= ennemiesNumber; i++)
            {
                ennemies.Add(
                    createEnnemyFunction(
                        new Vecteur2D(i * xSpace, ySpace),
                        new Vecteur2D(i * xSpace, gameInstance.gameSize.Height / 4 - ySpace)
                    )
                );
            }
        }

        public override void Draw(Game gameInstance, Graphics graphics) {}


        public override bool IsAlive()
        {
            bool alive = ennemies.Count() != 0;
            
            if (!alive) Game.game.gameStateManager.FinishGame(true);

            return alive;
        }

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
        }

        public override bool CanCollision(ProjectileObject projectile)
        {
            return false;
        }


        public override void OnCollision(ProjectileObject projectile) {}
    }
}
