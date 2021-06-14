using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext context;
        internal static BufferedGraphics Buffer;
        static Form MainForm { get; set; }
        private static Timer Timer = new Timer { Interval = Settings.ScreenUpdateFrequency };
        internal static Random Rand { get; set; }

        internal static int Width { get; set; }
        internal static int Height { get; set; }
        internal static int CurrentDifficultyLevel { get; set; } = Settings.DifficultyLevels[0];
        internal static bool GameStarting { get; set; }

        internal static Ship Ship { get; set; }
        internal static List<BaseObject> Asteroids { get; set; }
        internal static List<EmpireShip> EmpireShips { get; set; }
        internal static List<Bullet> PlayerBullets { get; set; }
        internal static List<Bullet> EnemiesBullets { get; set; }
        internal static List<Kit> Kits { get; set; }
        internal static List<Explode> Explodes { get; set; }
        internal static List<Stat> Stats { get; set; }
        internal static BaseObject[] Stars { get; set; }
        static Image Background = Properties.Resources.background;
        
        static Game() { }
        internal static void Init(Form form)
        {
            if (MainForm == null)
                MainForm = form;
            if (form.Height > 1000 || form.Width > 1000) throw new ArgumentOutOfRangeException("Form size");
            Graphics graphics = form.CreateGraphics();
            Rand = new Random();
            context = BufferedGraphicsManager.Current;            
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = context.Allocate(graphics, new Rectangle(0, 0, Width, Height));
            Load();
            Timer.Start();
            Timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        internal static void Load()
        {
            InitLists();
            CurrentDifficultyLevel = Settings.DifficultyLevels[0];
            EmpireShip.DestroyedShips = 0;
            EmpireShip.CreatedShips = 0;
            for (int i = 0; i < Stars.Length; i++)
                Stars[i] = new Star(i);
        }

        internal static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawImage(Background, new Point(0, 0));
            CreateEnemiesShips();
            CheckCollisions();
            foreach (var e in EmpireShips) e.Draw();
            foreach (var el in PlayerBullets) el.Draw();
            foreach (var e in EnemiesBullets) e.Draw();
            foreach (var el in Explodes) el.Draw();
            foreach (var e in Kits) e.Draw();
            foreach (var e in Stats) e.Draw();
            foreach (var el in Stars) el.Draw();
            Ship?.Draw();
            Buffer.Render();
        }

        internal static void Update()
        {
            foreach (var el in Stars) el.Update();
            foreach (var el in EmpireShips) el.Update();
            UpdateBullets();
            UpdateKits();
            Ship?.Update();
            UpdateExplodes();
            IsEndLevel();
        }
        static void CheckCollisions()
        {
            if (Ship == null)
                return;
            for (int i = EmpireShips.Count - 1; i >= 0; i--)
                CheckEmpireShipCollision(i);
            for (int i = EnemiesBullets.Count - 1; i >= 0; i--)
                CheckEnemiesBulletsCollision(i);
            for (int i = Kits.Count - 1; i >= 0; i--)
                CheckKitsCollision(i);
        }

        #region Game objects moderation
        private static void InitLists()
        {
            Stars = new BaseObject[Settings.StarsCount];
            EmpireShips = new List<EmpireShip>(Settings.EmpireShipsCount[CurrentDifficultyLevel]);
            PlayerBullets = new List<Bullet>();
            EnemiesBullets = new List<Bullet>();
            Explodes = new List<Explode>();
            Kits = new List<Kit>();
            Stats = new List<Stat>();
        }
        public static void BasicLoad()
        {
            GameStarting = true;
            Ship = new Ship(Settings.SpaceShipStartPos, new Point(0, 0), Settings.SpaceShipMaxHealth, Settings.SpaceShipMaxEnergy);
            Stats.Add(new Stat(Settings.ScoreStatPos, Settings.ScoreStatText, 0));
            Stats.Add(new Stat(Settings.LevelStatPos, Settings.DiffLevelStatText, 1));
        }
        public static void CreateEnemiesShips()
        {
            if (GameStarting && EmpireShip.IsCreateShipChanse())
                EmpireShips.Add(new EmpireShip());
        }
        private static void UpdateKits()
        {
            foreach (var el in Kits) el.Update();
            if (Kits.Count == 0 && Kit.GetKitAppearChanse() && Ship?.LifePoints < Settings.SpaceShipMaxHealth * Settings.KitAppearHelthPerc)
                Kits.Add(new Kit());
            for (int i = Kits.Count - 1; i >= 0; i--)
            {
                Kits[i].Update();
                if (Kits[i].CheckKitDelition()) Kits[i].Del(Kits, i);
            }
        }
        private static void UpdateExplodes()
        {
            for (int i = Explodes.Count - 1; i >= 0; i--)
            {
                Explodes[i].Update();
                if (Explodes[i].IsValidLifeCycle())
                { 
                    if (!Ship.IsAlive())
                        Finish(Settings.LooseMessage, Settings.LooseMessageHeader);
                }
                else
                {
                    Explodes[i].Pos.X = Settings.FieldWidth;
                    Explodes[i].Del(Explodes, i);
                }
            }
        }
        private static void UpdateBullets()
        {
            for (int i = 0; i < PlayerBullets.Count; i++)
                if (PlayerBullets[i].Pos.X > Settings.FieldWidth + Bullet.PlayerBulletImg.Size.Width)
                    PlayerBullets[i].Del(PlayerBullets, i);

            for (int i = 0; i < EnemiesBullets.Count; i++)
                if (EnemiesBullets[i].Pos.X < (0 - Bullet.EnemyBulletImg.Size.Width))
                    EnemiesBullets[i].Del(EnemiesBullets, i);

            foreach (var el in PlayerBullets) el.Update();
            foreach (var el in EnemiesBullets) el.Update();
        }
        private static void CheckEmpireShipCollision(int i)
        {
            if (EmpireShips[i].Collision(Ship))
            {
                Explodes.Add(new Explode(EmpireShips[i].Pos));
                Ship.GetDamage(EmpireShips[i].LifePoints);
                EmpireShips[i].Die(EmpireShips, i);
                return;
            }
            for (int j = PlayerBullets.Count - 1; j >= 0; j--)
                if (EmpireShips[i].Collision(PlayerBullets[j]))
                {
                    PlayerBullets[j].Del(PlayerBullets, j);
                    Explodes.Add(new Explode(EmpireShips[i].Pos));
                    EmpireShips[i].Die(EmpireShips, i);
                    break;
                }
        }
        private static void CheckEnemiesBulletsCollision(int i)
        {
            if (EnemiesBullets[i].Collision(Ship))
            {
                Explodes.Add(new Explode(EnemiesBullets[i].Pos));
                Ship.GetDamage(EnemiesBullets[i].LifePoints);
                EnemiesBullets[i].Del(EnemiesBullets, i);
                return;
            }
            for (int j = PlayerBullets.Count - 1; j >= 0; j--)
                if (EnemiesBullets[i].Collision(PlayerBullets[j]))
                {
                    PlayerBullets[j].Del(PlayerBullets, j);
                    Explodes.Add(new Explode(EnemiesBullets[i].Pos));
                    EnemiesBullets[i].Del(EnemiesBullets, i);
                    break;
                }
        }
        private static void CheckKitsCollision(int i)
        {
            if (Kits[i].Collision(Ship))
            {
                Ship.GetDamage(Kits[i].LifePoints);
                Kits[i].Del(Kits, i);
            }
        }
        #endregion

        #region Level control
        private static void IsEndLevel()
        {
            if (Settings.EmpireShipsCount[CurrentDifficultyLevel] == EmpireShip.DestroyedShips && Ship?.LifePoints > 0)
                if (CurrentDifficultyLevel == Settings.DifficultyLevels[Settings.DifficultyLevels.Length - 1])
                    Finish(Settings.GameComplete, Settings.Greetings);
                else
                    LevelUp();
        }
        internal static void LevelUp()
        {
            EmpireShip.DestroyedShips = 0;
            EmpireShip.CreatedShips = 0;
            if (Stats.Count > 1)
                Stats[1].StatValue = ++CurrentDifficultyLevel + 1;
        }
        #endregion

        #region Game process
        internal static void Finish(string message, string messageHeader)
        {
            Timer.Stop();
            Stats[0]?.WriteRecords();
            GameStarting = false;
            if (MessageBox.Show(message, messageHeader, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
            else
                Restart();
        }
        internal static void Restart()
        {
            InitLists();
            Ship = null;
            Timer.Tick -= Timer_Tick;
            Init(MainForm);
            BasicLoad();
        }
        internal static void Pause()
        {
            SplashScreen.ShowMenu(MainForm, Timer.Enabled);
            if (Timer.Enabled)
                Timer.Stop();
            else
                Timer.Start();
        }
        #endregion
    }
}
