using System;

namespace SpaceInvaders.GameObjects
{
    public enum Team
    {
        PLAYER,
        ENNEMY
    }

    class TeamManager {
        public Team Team { get; private set; }

        public TeamManager(Team team)
        {
            this.Team = GameException.RequireNonNull(team);
        }

        public Team getEnnemy()
        {
            if (Team == Team.PLAYER) return Team.ENNEMY;
            if (Team == Team.ENNEMY) return Team.PLAYER;
            
            throw new InvalidOperationException();
        }
    }
}
