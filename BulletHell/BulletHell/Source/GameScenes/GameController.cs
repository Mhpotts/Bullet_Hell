using BulletHell.Factories;
using BulletHell.Source.Controllers;
using BulletHell.Source.Entities;
using BulletHell.Source.Entities.PowerUps;
using BulletHell.Source.Factories;
using BulletHell.Source.GameScenes;
using BulletHell.Source.Menus;
using BulletHell.Source.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using static BulletHell.StageReader;

namespace BulletHell.Source.GameStates
{
    public class GameController : Scene
    {
        private Root stage1;
        private MainGame game;

        private Song song;

        private GraphicsDevice _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D backgroundTexture;
        private TimeSpan gameStartTime = TimeSpan.FromSeconds(-1);
        public Boundary bounds;

        private Dictionary<string, List<Timer>> timers;

        private Vector2 initialPosition;

        private Texture2D playerTexture;
        private Texture2D midBossTexture;
        private Texture2D finalBossTexture;
        private Texture2D missileTexture;
        private Texture2D laserTexture;
        private Texture2D laserPowerUpTexture;
        private Texture2D shieldTexture;
        private Texture2D oneUpTexture;
        private Texture2D nukeTexture;

        private Player player;

        private static int clientBoundsHeight = 600;
        private static int clientBoundsWidth = 593;
        public static Boundary visibleBounds = new Boundary(0, 0, 593, 600);

        private bool wave1spawned;
        private bool wave2spawned;
        private bool wave3spawned;
        private bool wave4spawned;

        public int lives = 5;

        private SpriteFont UIText;

        private SideMenu sideMenu;

        //FACTORIES
        private ScorpionFactory scorpionFactory;
        private BeetleFactory beetleFactory;

        public GameController(MainGame mainGame, GraphicsDevice graphicsDevice, ContentManager manager) 
            : base(mainGame, graphicsDevice, manager)
        {
            _graphics = graphicsDevice;
            this.game = mainGame;

            this.manager = manager;
            this.manager.RootDirectory = "Content";

            Initialize();
            LoadContent();
            //Init();
        }

        protected  void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected  void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PresentationParameters.BackBufferHeight = clientBoundsHeight;
            _graphics.PresentationParameters.BackBufferWidth = clientBoundsWidth;

            _graphics.PresentationParameters.BackBufferHeight = clientBoundsHeight;
            _graphics.PresentationParameters.BackBufferWidth = Convert.ToInt32(clientBoundsWidth * 1.5);

            initialPosition = new Vector2(_graphics.PresentationParameters.BackBufferWidth / 2,
                _graphics.PresentationParameters.BackBufferHeight / 2);

            bounds = new Boundary(50, 50, clientBoundsWidth - 50, clientBoundsHeight - 50);

            // menu
            sideMenu = new SideMenuBuilder()
                .createMenu(manager.Load<Texture2D>("background"), new Vector2(clientBoundsWidth + 75, 100))
                .buildDifficulty(manager.Load<SpriteFont>("Fonts/UIText"), "hard")
                .buildHighScore(manager.Load<SpriteFont>("Fonts/UIText"), "2304")
                .buildScore(manager.Load<SpriteFont>("Fonts/UIText"), "103")
                .buildLives(manager.Load<SpriteFont>("Fonts/UIText"), lives.ToString())
                .buildPower(manager.Load<SpriteFont>("Fonts/UIText"), "2")
                .getProduct();
        }

        protected  void LoadContent()
        {
            StageReader stageReader = new StageReader();
            _spriteBatch = new SpriteBatch(_graphics);

            this.song = manager.Load<Song>("Walk_and_Talk");
            MediaPlayer.Volume = 10f;
            MediaPlayer.Play(song);
            
            // TODO: use this.Content to load your game content here
            backgroundTexture = manager.Load<Texture2D>("Background");

            playerTexture = manager.Load<Texture2D>("Ship");

            finalBossTexture = manager.Load<Texture2D>("FinalBoss");
            missileTexture = manager.Load<Texture2D>("Missile");
            laserTexture = manager.Load<Texture2D>("LaserSquare");
            midBossTexture = manager.Load<Texture2D>("Miniboss");
            laserPowerUpTexture = manager.Load<Texture2D>("LaserPowerUp");
            shieldTexture = manager.Load<Texture2D>("Shield");
            oneUpTexture = manager.Load<Texture2D>("1up");
            nukeTexture = manager.Load<Texture2D>("Bomb");

            UIText = manager.Load<SpriteFont>("Fonts/UIText");
            stage1 = stageReader.LoadJSON();
            
            Init();
        }

        public void Init()
        {
            timers = new Dictionary<string, List<Timer>>();
            timers.Add("wave1", new List<Timer>());
            timers.Add("wave2", new List<Timer>());
            timers.Add("wave3", new List<Timer>());
            timers.Add("wave4", new List<Timer>());

            initialPosition = new Vector2(clientBoundsWidth / 2, clientBoundsHeight - 100);
            bounds = new Boundary(50, 50, clientBoundsWidth - 50, clientBoundsHeight - 50);

            player = new Player(initialPosition, new Vector2(30, 30), bounds, playerTexture, missileTexture);
            player.playerHasDied += handlePlayerDeath;

            EntityController.Instance.addPlayer(player);

            Boundary enemyBounds = new Boundary(50, -10, clientBoundsWidth - 50, clientBoundsHeight + 100);

            this.scorpionFactory = new ScorpionFactory(manager, enemyBounds);
            this.beetleFactory = new BeetleFactory(manager, enemyBounds);


            

        }

        public void clearTimers()
        {
            foreach (List<Timer> timerList in timers.Values)
            {
                timerList.ForEach(delegate (Timer x) { x.Dispose(); });
            }
        }

        public void clearTimers(string waveToKeep)
        {
            foreach (string wave in timers.Keys)
            {
                if (!wave.Equals(waveToKeep))
                {
                    timers[wave].ForEach(delegate (Timer x) { x.Dispose(); });
                }
            }
        }

        public TimeSpan getTime(GameTime gameTime)
        {
            return gameTime.TotalGameTime - gameStartTime;
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.game.Exit();

            if (gameStartTime < TimeSpan.Zero) gameStartTime = gameTime.TotalGameTime;

            EntityController.Instance.Update(gameTime);
            double wave1Start = double.Parse(stage1.waves.wave1.time.startTime);
            double wave2Start = double.Parse(stage1.waves.wave2.time.startTime);
            double wave3Start = double.Parse(stage1.waves.wave3.time.startTime);
            double wave4Start = double.Parse(stage1.waves.wave4.time.startTime);
            if (!wave1spawned && (getTime(gameTime) > TimeSpan.FromSeconds(wave1Start)))
            {
                clearTimers("wave1");
                EntityController.Instance.clearNPC();
                wave1();
            }
            if (!wave2spawned && (getTime(gameTime) > TimeSpan.FromSeconds(wave2Start)))
            {
                clearTimers("wave2");
                EntityController.Instance.clearNPC();
                wave2();
            }
            if (!wave3spawned && (getTime(gameTime) > TimeSpan.FromSeconds(wave3Start)))
            {
                clearTimers("wave3");
                EntityController.Instance.clearNPC();
                wave3();
            }

            if (!wave4spawned && (getTime(gameTime) > TimeSpan.FromSeconds(wave4Start)))
            {
                clearTimers("wave4");
                EntityController.Instance.clearNPC();
                wave4();
            }

            sideMenu.Update("lives", lives.ToString());

        }
        
        public void wave1()
        {
            wave1spawned = true;
            Boundary enemyBounds = new Boundary(50, -10, clientBoundsWidth - 50, clientBoundsHeight + 100);
            beetleFactory = new BeetleFactory(this.manager, enemyBounds);
            scorpionFactory = new ScorpionFactory(this.manager, enemyBounds);
            Dictionary<string, MovementPattern> patterns = new Dictionary<string, MovementPattern>();

            foreach (BulletHell.StageReader.Patterns pattern in stage1.waves.wave1.patterns)
            {
                if (pattern.type == "spiral")
                {
                    SpiralPattern spiral = new SpiralPattern(Double.Parse(pattern.radius), Double.Parse(pattern.forwardMomentum));
                    patterns.Add(pattern.id, spiral);
                } 
                else if (pattern.type == "zigzag")
                {
                    ZigZagPattern zigZag = new ZigZagPattern(float.Parse(pattern.speed), enemyBounds, bool.Parse(pattern.goRight));
                    patterns.Add(pattern.id, zigZag);
                }            
            }

            
            string startTime = stage1.waves.wave1.time.startTime;
            string endTime = stage1.waves.wave1.time.endTime;
            Double duration = Double.Parse(endTime) - Double.Parse(startTime);
            int numberOfEnemies = int.Parse(stage1.waves.wave1.enemyAmount);
            TimeSpan spawnRate = TimeSpan.FromSeconds(duration / numberOfEnemies);



            Timer gruntTimer = new Timer(
                e =>
                {
                    foreach (BulletHell.StageReader.Entity entity in stage1.waves.wave1.entities)
                    {
                        float x = float.Parse(entity.x);
                        float y = float.Parse(entity.y);
                        Vector2 position = new Vector2(x, y);

                        MovementPattern pattern = patterns[entity.pattern_id].getCopy();

                        if (entity.type == "scorpion")
                        {
                            Grunt scorpion = this.scorpionFactory.spawnGrunt(position, pattern);
                            scorpion.gruntHasDied += handleEnemyDeath;
                            EntityController.Instance.addEntity(scorpion);
                        }
                        else if (entity.type == "beetle")
                        {
                            Grunt beetle = this.beetleFactory.spawnGrunt(position, pattern);
                            beetle.gruntHasDied += handleEnemyDeath;
                            EntityController.Instance.addEntity(beetle);
                        }
                    }


                },
                null,
                TimeSpan.FromSeconds(0),
                spawnRate
            );

            timers["wave1"].Add(gruntTimer);

        }
        public void wave2()
        {
            wave2spawned = true;
            //midboss
            /*
            BackAndForthPattern movePattern = new BackAndForthPattern(150, 25, 100);
            MidBoss bigBeanBurrito = new MidBoss(300, 100, new Vector2(clientBoundsWidth / 2, 100), new Vector2(85, 85), movePattern, midBossTexture, new Vector2(80, 80));
            bigBeanBurrito.midBossHasDied += handleMidBossDeath;
            EntityController.Instance.addEntity(bigBeanBurrito);
            MissileLauncher gun = new MissileLauncher(laserTexture, new Vector2(bigBeanBurrito.getPosition().X + 50, bigBeanBurrito.getPosition().Y), player, movePattern, bigBeanBurrito);
            EntityController.Instance.addEntity(gun);*/
            Dictionary<string, MovementPattern> patterns = new Dictionary<string, MovementPattern>();
            foreach (BulletHell.StageReader.Patterns pattern in stage1.waves.wave2.patterns)
            {
                if (pattern.type == "backandforth")
                {
                    BackAndForthPattern backandForth = new BackAndForthPattern(float.Parse(pattern.radius), float.Parse(pattern.speed), float.Parse(pattern.wobble));
                    patterns.Add(pattern.id, backandForth);
                }
            }
            string startTime = stage1.waves.wave2.time.startTime;
            string endTime = stage1.waves.wave2.time.endTime;
            Double duration = Double.Parse(endTime) - Double.Parse(startTime);
            int numberOfEnemies = int.Parse(stage1.waves.wave2.bossAmount);
            TimeSpan spawnRate = TimeSpan.FromSeconds(duration / numberOfEnemies);
            Timer bossTimer = new Timer(
                e =>
                {
                    foreach (BulletHell.StageReader.Entity entity in stage1.waves.wave2.entities)
                    {
                        float x = float.Parse(entity.x);
                        float y = float.Parse(entity.y);
                        Vector2 position = new Vector2(x, y);

                        MovementPattern pattern = patterns[entity.pattern_id].getCopy();

                        if (entity.type == "MidBoss")
                        {
                            MidBoss bowser = new MidBoss(int.Parse(entity.health), int.Parse(entity.damage), new Vector2(clientBoundsWidth / 2, 100), new Vector2(x, y), pattern, midBossTexture, new Vector2(float.Parse(entity.xDimension), float.Parse(entity.yDimension)));
                            bowser.midBossHasDied += handleEnemyDeath;
                            EntityController.Instance.addEntity(bowser);
                            MissileLauncher gun = new MissileLauncher(laserTexture, new Vector2(x + 50, y + 50), player, pattern, bowser);
                            EntityController.Instance.addEntity(gun);
                        }
                    }

                },
                null,
                TimeSpan.FromSeconds(0),
                spawnRate
            );

            timers["wave2"].Add(bossTimer);
        }

        public void wave3()
        {
            wave3spawned = true;
            /*
            Boundary enemyBounds = new Boundary(50, -10, clientBoundsWidth - 50, clientBoundsHeight + 100);
            Timer gruntTimer = new Timer(
                e => {
                    SpiralPattern spiral = new SpiralPattern(200, 75);
                    spiral = spiral.invert();
                    Grunt scorpion = this.scorpionFactory.spawnGrunt(new Vector2(50, 0), spiral);
                    scorpion.gruntHasDied += handleEnemyDeath;
                    EntityController.Instance.addEntity(scorpion);
                    scorpion.gruntHasDied += handleEnemyDeath;
                    Grunt scorpion2 = this.scorpionFactory.spawnGrunt(new Vector2(clientBoundsWidth - 50, 0), spiral);
                    EntityController.Instance.addEntity(scorpion2);

                    ZigZagPattern zigzag = new ZigZagPattern(100, enemyBounds, true);
                    Grunt beetle = this.beetleFactory.spawnGrunt(new Vector2(clientBoundsWidth - 50, 0), zigzag);
                    beetle.gruntHasDied += handleEnemyDeath;
                    EntityController.Instance.addEntity(beetle);
                },
                null,
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(2)
                );

            timers["wave3"].Add(gruntTimer);
            */
            Boundary enemyBounds = new Boundary(50, -10, clientBoundsWidth - 50, clientBoundsHeight + 100);
            beetleFactory = new BeetleFactory(this.manager, enemyBounds);
            scorpionFactory = new ScorpionFactory(this.manager, enemyBounds);
            Dictionary<string, MovementPattern> patterns = new Dictionary<string, MovementPattern>();

            foreach (BulletHell.StageReader.Patterns pattern in stage1.waves.wave3.patterns)
            {
                if (pattern.type == "spiral")
                {
                    SpiralPattern spiral = new SpiralPattern(Double.Parse(pattern.radius), Double.Parse(pattern.forwardMomentum));
                    patterns.Add(pattern.id, spiral);
                }
                else if (pattern.type == "zigzag")
                {
                    ZigZagPattern zigZag = new ZigZagPattern(float.Parse(pattern.speed), enemyBounds, bool.Parse(pattern.goRight));
                    patterns.Add(pattern.id, zigZag);
                }
            }


            string startTime = stage1.waves.wave3.time.startTime;
            string endTime = stage1.waves.wave3.time.endTime;
            Double duration = Double.Parse(endTime) - Double.Parse(startTime);
            int numberOfEnemies = int.Parse(stage1.waves.wave3.enemyAmount);
            TimeSpan spawnRate = TimeSpan.FromSeconds(duration / numberOfEnemies);



            Timer gruntTimer = new Timer(
                e =>
                {
                    foreach (BulletHell.StageReader.Entity entity in stage1.waves.wave3.entities)
                    {
                        float x = float.Parse(entity.x);
                        float y = float.Parse(entity.y);
                        Vector2 position = new Vector2(x, y);

                        MovementPattern pattern = patterns[entity.pattern_id].getCopy();

                        if (entity.type == "scorpion")
                        {
                            Grunt scorpion = this.scorpionFactory.spawnGrunt(position, pattern);
                            scorpion.gruntHasDied += handleEnemyDeath;
                            EntityController.Instance.addEntity(scorpion);
                        }
                        else if (entity.type == "beetle")
                        {
                            Grunt beetle = this.beetleFactory.spawnGrunt(position, pattern);
                            beetle.gruntHasDied += handleEnemyDeath;
                            EntityController.Instance.addEntity(beetle);
                        }
                    }


                },
                null,
                TimeSpan.FromSeconds(0),
                spawnRate
            );

            timers["wave3"].Add(gruntTimer);

        }

        public void wave4()
        {
            wave4spawned = true;
            /*
            BackAndForthPattern movePattern = new BackAndForthPattern(75, 10, 50);
            FinalBoss mothra = new FinalBoss(750, 1000, new Vector2(clientBoundsWidth / 2, 100), new Vector2(115, 115), movePattern, finalBossTexture, new Vector2(100, 100));
            mothra.finalBossHasDied += winGame;
            EntityController.Instance.addEntity(mothra);
            
            MissileLauncher gunA = new MissileLauncher(laserTexture, new Vector2(mothra.getPosition().X - 50, mothra.getPosition().Y), player, new CirclePattern(50), mothra);
            MissileLauncher gunB = new MissileLauncher(laserTexture, new Vector2(mothra.getPosition().X + 50, mothra.getPosition().Y), player, new CirclePattern(50), mothra);
            EntityController.Instance.addEntity(gunA);
            EntityController.Instance.addEntity(gunB);
            */
            Dictionary<string, MovementPattern> patterns = new Dictionary<string, MovementPattern>();
            foreach (BulletHell.StageReader.Patterns pattern in stage1.waves.wave4.patterns)
            {
                if (pattern.type == "backandforth")
                {
                    BackAndForthPattern backandForth = new BackAndForthPattern(float.Parse(pattern.radius), float.Parse(pattern.speed), float.Parse(pattern.wobble));
                    patterns.Add(pattern.id, backandForth);
                }
            }
            string startTime = stage1.waves.wave4.time.startTime;
            string endTime = stage1.waves.wave4.time.endTime;
            Double duration = Double.Parse(endTime) - Double.Parse(startTime);
            int numberOfEnemies = int.Parse(stage1.waves.wave4.bossAmount);
            TimeSpan spawnRate = TimeSpan.FromSeconds(duration / numberOfEnemies);
            Timer bossTimer = new Timer(
                e =>
                {
                    foreach (BulletHell.StageReader.Entity entity in stage1.waves.wave4.entities)
                    {
                        float x = float.Parse(entity.x);
                        float y = float.Parse(entity.y);
                        Vector2 position = new Vector2(x, y);

                        MovementPattern pattern = patterns[entity.pattern_id].getCopy();

                        if (entity.type == "FinalBoss")
                        {
                            FinalBoss bowser = new FinalBoss(int.Parse(entity.health), int.Parse(entity.damage), new Vector2(clientBoundsWidth / 2, 100), new Vector2(x, y), pattern, finalBossTexture, new Vector2(float.Parse(entity.xDimension), float.Parse(entity.yDimension)));
                            bowser.finalBossHasDied += handleEnemyDeath;
                            EntityController.Instance.addEntity(bowser);
                            MissileLauncher gun = new MissileLauncher(laserTexture, new Vector2(x - 50, y), player, new CirclePattern(50), bowser);
                            MissileLauncher gun2 = new MissileLauncher(missileTexture, new Vector2(x + 50, y), player, new CirclePattern(50), bowser);
                            EntityController.Instance.addEntity(gun);
                            EntityController.Instance.addEntity(gun2);
                        }
                    }
                },
                null,
                TimeSpan.FromSeconds(0),
                spawnRate
            );

            timers["wave4"].Add(bossTimer);

        }

        

        public void handlePlayerDeath(object sender, EventArgs e)
        {
            lives--;
            if (lives <= 0)
            {
                EntityController.Instance.clearNPC();
                clearTimers();
                this.game.changeScene(new LoseGame(game, _graphics, this.manager)); ;
            }
            player.respawn(initialPosition);
        }

        public void handleEnemyDeath(object sender, EventArgs e)
        {
            if (sender is Grunt)
            {
                Grunt grunt = (Grunt)sender;
                Random r = new Random();
                int num = r.Next(100);
                if (num < 5)
                {
                    Nuke nuke = new Nuke(grunt.getPosition(), new Vector2(40, 40), nukeTexture, new Vector2(30, 30));
                    EntityController.Instance.addEntity(nuke);
                } 
                else if (num < 10) 
                {
                    ShieldPowerUp shield = new ShieldPowerUp(grunt.getPosition(), new Vector2(40, 40), shieldTexture, new Vector2(30, 30));
                    EntityController.Instance.addEntity(shield);
                } 
                else if (num < 15)
                {
                    OneUp oneUp = new OneUp(this, grunt.getPosition(), new Vector2(40, 40), oneUpTexture, new Vector2(30, 30));
                    EntityController.Instance.addEntity(oneUp);
                }
            }
        }

        public void handleMidBossDeath(object sender, EventArgs e)
        {
            if (sender is MidBoss)
            {
                MidBoss midBoss = (MidBoss)sender;
                EntityController.Instance.addEntity(new LaserPowerUp(laserTexture, midBoss.getPosition(), new Vector2(40, 40), laserPowerUpTexture, new Vector2(30, 30)));
            }
        }

        public void winGame(object sender, EventArgs e)
        {
            EntityController.Instance.reset();
            clearTimers();
            this.game.changeScene(new WinGame(game, _graphics, this.manager)); ;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphics.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            _spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            
            EntityController.Instance.Draw(_spriteBatch);
            
            _spriteBatch.Draw(backgroundTexture, new Vector2(clientBoundsWidth, 0), Color.White);

            sideMenu.Draw(_spriteBatch);
            //draw the side menu
            _spriteBatch.End();
        }

    }
}
