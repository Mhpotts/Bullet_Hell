using BulletHell.Source.Entities;
using BulletHell.Source.Entities.PowerUps;
using BulletHell.Source.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Controllers
{
    public sealed class EntityController
    {
        // Everything that isn't the player or player shot bullets
        private List<Entity> entities;
        private List<Entity> stagedEntities;
        // Projectiles shot by the player
        private List<Entity> playerBullets;
        // The player
        private List<Player> players;
        // The visible zone of the game, anything outside is kill zone
        private Boundary bounds;
        private CollisionDetector collisionDetector;

        private EntityController()
        {
            entities = new List<Entity>();
            stagedEntities = new List<Entity>();
            playerBullets = new List<Entity>();
            players = new List<Player>();
            bounds = GameController.visibleBounds;
            collisionDetector = new CollisionDetector(players, entities, playerBullets);
        }
        private static EntityController instance = null;
        public static EntityController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntityController();
                }
                return instance;
            }
        }

        public void setBoundary(Boundary boundary)
        {
            this.bounds = boundary;
        }

        public void addPlayerBullets(List<Projectile> bullets)
        {
            this.playerBullets.AddRange(bullets);
        }

        public void addPlayerBullet(Projectile bullet)
        {
            this.playerBullets.Add(bullet);
        }

        public void addEntities(List<Entity> entities)
        {
            entities = entities.FindAll(delegate (Entity x) { return x != null; });
            this.stagedEntities.AddRange(entities);
        }

        public void addEntity(Entity entity)
        {
            if (entity != null)
            {
                this.stagedEntities.Add(entity);
            }
        }

        public void addPlayer(Player player)
        {
            this.players.Add(player);
        }

        public void clearNPC()
        {
            foreach (Entity x in entities)
            {
                x.kill();
            }
        }

        public void reset()
        {
            entities = new List<Entity>();
            stagedEntities = new List<Entity>();
            playerBullets = new List<Entity>();
            players = new List<Player>();
            bounds = GameController.visibleBounds;
            collisionDetector = new CollisionDetector(players, entities, playerBullets);
        }



        public void Update(GameTime gameTime)
        {
            foreach (Entity x in this.entities)
            {
                x.update(gameTime);
            }
            
            foreach (Entity x in this.playerBullets)
            {
                x.update(gameTime);
            }

            foreach (Player x in this.players)
            {
                x.update(gameTime);
            }

            // check if stuff hits eachother
            collisionDetector.checkCollisions();

            this.entities.RemoveAll(isDead);
            this.playerBullets.RemoveAll(isDead);

            if (bounds != null)
            {
                this.entities.RemoveAll(outOfBounds);
                this.playerBullets.RemoveAll(outOfBounds);
            }

            if (stagedEntities.Count > 0)
            {
                foreach (Entity x in stagedEntities)
                {
                    this.entities.Add(x);
                }
                stagedEntities.Clear();
            }
        }

        private bool isDead(Entity x)
        {
            return !x.isAlive();
        }

        private bool outOfBounds(Entity x)
        {
            return !bounds.inBounds(x.getPosition());
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (Player x in players)
            {
                x.draw(_spriteBatch);
            }
            //entities.ForEach(delegate (Entity x) { x.draw(_spriteBatch); });

            foreach(Entity x in entities)
            {
                x.draw(_spriteBatch);
            }
            foreach (Projectile x in playerBullets)
            {
                x.draw(_spriteBatch);
            }
        }
    }
}
