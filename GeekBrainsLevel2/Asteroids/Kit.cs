using System.Drawing;

namespace Asteroids
{
    class Kit : BaseObject
    {
        internal static Image Img { get; set; } = Properties.Resources.R2_D2;
        internal static bool GetKitAppearChanse() => Rand.Next(0, Settings.KitAppearence) == Rand.Next(0, Settings.KitAppearence);
        public Kit(Point pos, Point dir) : base(pos, dir) { }
        public Kit() : this(new Point(Settings.FieldWidth - Img.Size.Width, Rand.Next(Img.Size.Height, Settings.FieldHeight - Img.Size.Height)),
            new Point(-Settings.KitDir[Game.CurrentDifficultyLevel], Rand.Next(-Settings.KitDir[Game.CurrentDifficultyLevel], Settings.KitDir[Game.CurrentDifficultyLevel])))
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Img, Pos);
            Rect = new Rectangle(Pos.X, Pos.Y, Img.Size.Width, Img.Size.Height);
            LifePoints = Game.Rand.Next(-Settings.EmpireShipMaxDamage[Game.CurrentDifficultyLevel], -Settings.EmpireShipMinDamage[Game.CurrentDifficultyLevel]);
        }
        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.Y <= Img.Height || Pos.Y >= (Settings.FieldHeight - Img.Height))
                Dir.Y = -Dir.Y;
        }
        internal bool CheckKitDelition() => (Pos.X + Img.Size.Width < 0);
    }
}