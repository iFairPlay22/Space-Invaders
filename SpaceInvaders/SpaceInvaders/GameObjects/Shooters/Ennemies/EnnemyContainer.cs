using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceInvaders.GameObjects.Shooters
{
    class EnnemyContainer : GameObject
    {
        private readonly HashSet<EnnemyObject> ennemies = new HashSet<EnnemyObject>();

        private bool right = true;

        public EnnemyContainer(params EnnemyObject[] ennemies) : 
            this(new HashSet<EnnemyObject>(GameException.RequireNonNull(ennemies))) {}

        private EnnemyContainer(HashSet<EnnemyObject> ennemies) : base(Team.ENNEMY, new Vecteur2D(0, 0))
        {
            this.ennemies = GameException.RequireNonNull(ennemies);

            foreach (EnnemyObject ennemy in ennemies)
                Game.game.AddNewGameObject(ennemy);
        }

        public override void Draw(Game gameInstance, Graphics graphics) {}

        public override bool IsAlive()
        {
            bool isAlive = ennemies.Count() != 0;

            if (!isAlive) throw new Exception("You loose!");

            return isAlive;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            bool decalage = ennemies.Any(e => !e.CanMove(gameInstance, deltaT, right, false));
           
            if (decalage) right = !right;
            

            foreach (EnnemyObject ennemy in ennemies)
            {
                if (decalage)
                {
                    ennemy.Accelerate();
                    
                    for (int i = 0; i < 2; i++)
                        if (ennemy.CanMove(gameInstance, deltaT, right, null))
                            ennemy.Move(gameInstance, deltaT, right, false);

                } else if (ennemy.CanMove(gameInstance, deltaT, right, null))
                {
                    ennemy.Move(gameInstance, deltaT, right, null);
                }

                if (!IsAlive()) Game.game.gameStateManager.FinishGame(true);
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
