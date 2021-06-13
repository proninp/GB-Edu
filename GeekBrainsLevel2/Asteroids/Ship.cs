using System.Drawing;

namespace Asteroids
{
    class Ship : BaseObject
    {
        public static Image Img { get; set; }
        public Bar HealthBar { get; set; }
        public Bar EnergyBar { get; set; }
        private bool fire;
        public override int LifePoints
        {
            get => lifePoints;
            set
            {
                lifePoints = value > Settings.SpaceShipMaxHealth ? Settings.SpaceShipMaxHealth : (value < 0 ? 0 : value);
                if (HealthBar != null) HealthBar.LifePoints = lifePoints;
            }
        }
        private int energy;
        public int Energy
        {
            get => energy;
            set 
            { 
                energy = (value < 0) ? 0 : (value > Settings.SpaceShipMaxEnergy ? Settings.SpaceShipMaxEnergy : value);
                EnergyBar.LifePoints = energy;
            }
        }
        public Ship(Point pos, Point dir, int health, int energy) : base(pos, dir, health)
        {
            Img = Properties.Resources.x_wing;
            HealthBar = new Bar(Settings.HPBarPos, LifePoints, Settings.HPBarSize, Game.Buffer?.Graphics, Color.Green, LifePoints, true);
            EnergyBar = new Bar(Settings.EnergyBarPos, energy, Settings.EnergyBarSize, Game.Buffer?.Graphics, Color.Blue, energy, false);
            Energy = energy;
            Rect = new Rectangle(Pos.X, Pos.Y, Img.Size.Width, Img.Size.Height);
            fire = false;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Img, Pos);
            Rect = new Rectangle(Pos.X, Pos.Y, (int)(Img.Size.Width * Settings.ShipSizeRounding), (int)(Img.Size.Height * Settings.ShipSizeRounding));
            HealthBar.Draw();
            EnergyBar.Draw();
        }
        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < 0)
                Pos.X = 0;
            else if (Pos.X > Game.Width - Img.Width)
                Pos.X = Game.Width - Img.Width;
            if (Pos.Y < 0)
                Pos.Y = 0;
            else if (Pos.Y > Game.Height - Img.Height * 2)
                Pos.Y = Game.Height - Img.Height * 2;
            if ((fire) && (Energy > 0))
            {
                Energy -= Settings.EnergyCostShoot[Game.CurrentDifficultyLevel];
                Game.PlayerBullets?.Add(new Bullet(new Point(Game.Ship.Pos.X + Bullet.Images[0].Size.Width / 2, Game.Ship.Pos.Y + Img.Size.Height / 4), new Point(15, 0), 10));
            }
            if (!fire && Rand.Next(0, Settings.EnergyRecoveryChance[Game.CurrentDifficultyLevel]) == Game.CurrentDifficultyLevel)
                Energy++;
        }
        internal void Fire(bool f) => fire = f;
        internal void Up(bool move) => Dir.Y = (move && Pos.Y > Img.Height / 2) ? -Settings.SpaceShipStep : 0;
        internal void Down(bool move) => Dir.Y = (move && Pos.Y < (Game.Height - Img.Height)) ? Settings.SpaceShipStep : 0;
        internal void Left(bool move) => Dir.X = (move && Pos.X > 0) ? -Settings.SpaceShipStep : 0;
        internal void Right(bool move) => Dir.X = (move && Pos.X < (Game.Width - Img.Width)) ? Settings.SpaceShipStep : 0;
        internal void GetDamage(int damage)
        {
            LifePoints -= damage;
            Energy -= (damage > 0) ? Rand.Next(0, damage) : Rand.Next(damage, 0);
        }
        internal bool IsAlive() => LifePoints > 0;
    }
}
