using SpaceInvaders.GameObjects.Projectiles;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceInvaders.GameObjects.Shooters
{
    class EnnemyContainer : GameObject
    {
        private readonly HashSet<Ennemy> ennemies = new HashSet<Ennemy>();

        private bool right = true;

        public EnnemyContainer(params Ennemy[] ennemies) : 
            this(new HashSet<Ennemy>(GameException.RequireNonNull(ennemies))) {}

        private EnnemyContainer(HashSet<Ennemy> ennemies) : base(new TeamManager(GameObjects.Team.ENNEMY), new Vecteur2D(0, 0))
        {
            this.ennemies = GameException.RequireNonNull(ennemies);

            foreach (Ennemy ennemy in ennemies)
                Game.game.AddNewGameObject(ennemy);
        }

        public override void Draw(Game gameInstance, Graphics graphics) {}

        public override bool IsAlive()
        {
            return ennemies.Count() != 0;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            bool decalage = ennemies.Any(e => !e.CanMove(gameInstance, deltaT, right, false));
           
            if (decalage) right = !right;
            

            foreach (Ennemy ennemy in ennemies)
            {
                if (decalage)
                {
                    ennemy.Accelerate();
                    
                    for (int i = 0; i < 2; i++)
                        ennemy.Move(gameInstance, deltaT, right, false);

                } else
                {
                    ennemy.Move(gameInstance, deltaT, right, null);
                }
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
