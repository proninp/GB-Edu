using System.Drawing;

namespace Asteroids
{
    class Explode : BaseObject
    {
        internal int VisabilityTicksCount = Settings.ExplodeTicksVisible;
        static Image[] Images { get; set; } = new Image[4]
        {
            Properties.Resources.explode_1,
            Properties.Resources.explode_2,
            Properties.Resources.explode_3,
            Properties.Resources.explode_4
        };
        internal int ImageIndex { get; set; } = Rand.Next(0, Images.Length);
        public Explode(Point pos) : base(pos, new Point(0, 0)) { }
        public override void Draw() => Game.Buffer.Graphics.DrawImage(Images[ImageIndex], Pos);
        public override void Update() => VisabilityTicksCount--;
        internal bool IsValidLifeCycle() => VisabilityTicksCount > 0 && VisabilityTicksCount < Settings.ExplodeTicksVisible;
    }
}
