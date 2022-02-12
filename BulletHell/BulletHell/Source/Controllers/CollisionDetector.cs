using BulletHell.Source.Commands;
using BulletHell.Source.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Controllers
{
    public class CollisionDetector
    {
        private List<Player> players;
        private List<Entity> entities;
        private List<Entity> playerBullets;
        private Hit playerHit;
        private Hit enemyHit;

        public CollisionDetector(List<Player> players, List<Entity> entities, List<Entity> playerBullets)
        {
            this.players = players;
            this.entities = entities;
            this.playerBullets = playerBullets;
        }

        public  void checkCollisions()
        {
            foreach (Player player in players)
            {
                // doesn't handle multiple players, just uses the one player that only gets added once
                // the list of players is just so we can reference the container of the player 
                // so we can instantiate this class before player
                if (player.isAlive())
                {
                    if (playerHit == null)
                    {
                        playerHit = new Hit(player, entities);
                    }

                    if (enemyHit == null)
                    {
                        enemyHit = new Hit(playerBullets, entities);
                    }

                    playerHit.execute();
                    enemyHit.execute();
                }
            }
        }
    }
}
