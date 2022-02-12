using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Misc
{
    public sealed class GameSettings
    {
        private Dictionary<string, object> settings;
        private GameSettings()
        {
            settings = new Dictionary<string, object>();
            initSettings();
        }
        private static GameSettings instance = null;
        public static GameSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameSettings();
                }
                return instance;
            }
        }

        public object getSetting(string setting)
        {
            try
            {
                return settings[setting];
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public void setSetting(string setting, object value)
        {
            settings[setting] = value;
        }

        public void initSettings()
        {
            settings["playerHealth"] = 100;
            settings["playerStartingLives"] = 5;
            settings["playerRespawnImmunityCooldown"] = TimeSpan.FromSeconds(3);
            settings["playerMissileDamage"] = 50;
            settings["playerMissileCooldown"] = TimeSpan.FromSeconds(.35);
            settings["playerUp"] = Keys.Up;
            settings["playerDown"] = Keys.Down;
            settings["playerLeft"] = Keys.Left;
            settings["playerRight"] = Keys.Right;
            settings["playerFire"] = Keys.Space;
            settings["playerSpeedLow"] = 150f;
            settings["playerSpeedHigh"] = 500f;
            settings["playerHBSize"] = new Vector2(25, 25);
            settings["playerImageSize"] = new Vector2(30, 30);

            settings["missileDamage"] = 50;
            settings["missileCooldown"] = TimeSpan.FromSeconds(1);

            settings["laserDamage"] = 5;

            settings["scorpionHealth"] = 50;
            settings["scorpionDamage"] = 75;
            settings["scorpionHBSize"] = new Vector2(35, 35);
            settings["scorpionImageSize"] = new Vector2(30, 30);
            settings["beetleHealth"] = 100;
            settings["beetleDamage"] = 50;
            settings["beetleHBSize"] = new Vector2(45, 45);
            settings["beetleImageSize"] = new Vector2(40, 40);

        }
    }
}
