using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Settings
    {
        #region Base game settings
        public static int FieldWidth { get; } = 900;
        public static int FieldHeight { get; } = 675;
        internal static int ScreenUpdateFrequency { get; } = 50;
        #endregion
        #region Game settings
        public static int[] DifficultyLevels { get; } = new int[] { 1, 2, 3 };
        public static int CurrentDifficultyLevel { get; set; } = DifficultyLevels[0];
        public static int AsteroidsCount { get; } = 20;
        public static int AsteroidMaxSpeedX { get; } = 10;
        public static int AsteroidMinSpeedX { get; } = 4;
        public static int AsteroidMaxSpeedY { get; } = 5;
        public static int AsteroidMinSpeedY { get; } = -5;

        public static int StarsCount { get; } = 30;
        public static int StarSizeMax { get; } = 6;
        public static int StarSizeMin { get; } = 2;

        public static int PlayerBulletSpeed { get; set; } = 10;
        public static int PlayerBulletPower { get; set; } = 10;

        public static int ExplodeTicksVisible { get; set; } = 5;
        #endregion
    }
}
