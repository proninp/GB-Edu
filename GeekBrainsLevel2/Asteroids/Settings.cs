using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Settings
    {
        #region Game field settings
        public static int FieldWidth { get; set; } = 900;
        public static int FieldHeight { get; set; } = 675;
        #endregion
        #region Game settings
        public static int AsteroidsCount { get; } = 10;
        public static int AsteroidMaxSpeedX { get; } = 15;
        public static int AsteroidMinSpeedX { get; } = 5;
        public static int AsteroidMaxSpeedY { get; } = 5;
        public static int AsteroidMinSpeedY { get; } = -5;

        public static int StarsCount { get; } = 30;
        public static int StarSizeMax { get; } = 5;
        public static int StarSizeMin { get; } = 1;
        #endregion
    }
}
