using System.Collections.Generic;
using System.Drawing;

namespace Asteroids
{
    class Settings
    {
        #region Base game settings
        public static int FieldWidth { get; } = 900;
        public static int FieldHeight { get; } = 675;
        internal static int ScreenUpdateFrequency { get; } = 50;        
        public static Size ButtonSize { get; set; } = new Size(250, 50);
        public static int HeightBetweenButtons { get; } = 40;
        public static Size PauseLblSize { get; } = new Size(150, 50);        
        #endregion

        #region Game settings        
        public static int AsteroidsCount { get; } = 20;
        public static int AsteroidMaxSpeedX { get; } = 10;
        public static int AsteroidMinSpeedX { get; } = 4;
        public static int AsteroidMaxSpeedY { get; } = 5;
        public static int AsteroidMinSpeedY { get; } = -5;

        public static int StarsCount { get; } = 30;
        public static int StarSizeMax { get; } = 3;
        public static int StarSizeMin { get; } = 1;

        public static int PlayerBulletSpeed { get; set; } = 10;
        public static int PlayerBulletPower { get; set; } = 10;

        public static int ExplodeTicksVisible { get; set; } = 5;
        #endregion

        #region Ship Settings
        public static double ShipSizeRounding { get; set; } = 0.8;
        public static int SpaceShipMaxHealth { get; set; } = 100;
        public static int SpaceShipMaxEnergy { get; set; } = 100;
        public static int[] EnergyCostShoot { get; set; } = new int[] { 6, 8, 10 };
        public static int[] EnergyRecoveryChance { get; set; } = new int[] { 3, 4, 5 };
        public static int SpaceShipStep { get; set; } = 7;
        public static Point SpaceShipStartPos { get; set; } = new Point(50, 300);
        public static Point HPBarPos { get; set; } = new Point(10, 10);
        public static Size HPBarSize { get; set; } = new Size(200, 12);
        public static Point EnergyBarPos { get; set; } = new Point(10, HPBarPos.Y + HPBarSize.Height + 20);
        public static Size EnergyBarSize { get; set; } = new Size(200, 12);
        #endregion

        #region Empire ships settings
        public static int[] EmpireShipsCount { get; set; } = new int[] { 10, 15, 20 };
        public static int[] EmpireShipsCreatingChance { get; set; } = new int[] { 12, 10, 8 };
        public static int EmpireShipAvgHeight { get; set; } = 75;
        public static int[] EmpireShipMaxDamage { get; } = new int[] { 20, 40, 70 };
        public static int[] EmpireShipMinDamage { get; } = new int[] { 5, 15, 25 };
        public static int[] EmpireShipShotChance { get; } = new int[] { 40, 50, 60 };
        public static int EmpireShipBulletIndex { get; } = 1;
        public static int[] EmpireShipBulletSpeed { get; } = new int[] { -15, -20, -25 };
        public static Dictionary<int, int[]> EmpireShipsDirection { get; } = new Dictionary<int, int[]>
        {
            [0] = new int[] { -10, -5 },
            [1] = new int[] { -12, -5 },
            [2] = new int[] { -15, -5 }
        };
        #endregion

        #region Kits
        public static int[] KitDir { get; } = new int[] { 5, 7, 9 };
        public static int[] KitsCount { get; } = new int[] { 1, 2, 3 };
        public static int KitAppearence { get; } = 150;
        public static double KitAppearHelthPerc { get; } = 0.8;
        #endregion

        #region Score and difficulty level
        public static int[] DifficultyLevels { get; } = new int[] { 0, 1, 2 };
        public static Point ScoreStatPos { get; set; } = new Point(FieldWidth - 150, 10);
        public static Point LevelStatPos { get; set; } = new Point(FieldWidth - 150, 25);
        public static string ScoreStatText { get; set; } = "Score: ";
        public static string DiffLevelStatText { get; set; } = "Level: ";
        #endregion

        #region Game Descriptions
        public static string MainFont { get; set; } = "Verdana";
        public static string[] ButtonNames { get; set; } = { "NewGame", "Exit", "Continue", "Records" };
        public static string GameStart { get; set; } = "New Game";
        public static string GameContinue { get; set; } = "Resume Game";
        public static string Records { get; set; } = "Records";
        public static string GameRules { get; set; } = "Shot - space;\nShip movement - keyboard arrows.";
        public static string GameEnd { get; set; } = "Exit";
        public static string LooseMessage { get; } = "Your ship has been shot down!\nDo you want to exit?";
        public static string LooseMessageHeader { get; } = "You've lost!";
        public static string NewLevel { get; } = "You have shot down all the opponents!\nDo you want to go to the next level?";
        public static string Greetings { get; } = "Congratulate!";
        public static string GameComplete { get; } = "You have successfully completed the game!\nDo you want to exit?";
        public static string RestartOrQuit { get; } = "Play again or quit?\nPlay it again?";
        public static string RecordsInitString { get; } = "--==  RECORDS  ==--\n";
        public static string RecordsFirst { get; } = "First place: ";
        public static string RecordsSecond { get; } = "Second place: ";
        public static string RecordsThird { get; } = "Third place: ";
        public static string RecordsFourth { get; } = "Fourth place: ";
        public static string RecordsFifth { get; } = "Fifth place: ";
        public static string UserName { get; set; } = (!System.Security.Principal.WindowsIdentity.GetCurrent().Name.Contains("\\") ?
            System.Security.Principal.WindowsIdentity.GetCurrent().Name :
            System.Security.Principal.WindowsIdentity.GetCurrent().Name.
            Substring(System.Security.Principal.WindowsIdentity.GetCurrent().Name.IndexOf("\\") + 1));
        public static string RecordsFile { get; } = System.IO.Directory.GetCurrentDirectory() + "\\Records.txt";
        #endregion
    }
}
