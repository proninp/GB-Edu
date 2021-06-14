using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bar : BaseObject
    {
        int BarWidth { get; set; }
        Graphics Shape { get; set; }
        Rectangle SubRect { get; set; }
        Color InnerColor { get; set; }
        int BarMaxPoints { get; set; }
        bool DynamicColorChange { get; set; }
        public Bar(Point pos, int hp, Size size, Graphics fromGraphics, Color innerColor, int maxPoints, bool changeColor) : base(pos, new Point(0, 0), hp, size)
        {
            Shape = fromGraphics;
            SubRect = new Rectangle(Pos.X - 2, Pos.Y - 2, size.Width + 4, size.Height + 4);
            InnerColor = innerColor;
            BarMaxPoints = maxPoints;
            DynamicColorChange = changeColor;
            Shape.DrawString($"{LifePoints}/{BarMaxPoints}", new Font(Settings.MainFont, 8, FontStyle.Bold), Brushes.White, Pos.X, Pos.Y + Size.Height);
        }
        public override void Draw()
        {
            BarWidth = Size.Width / BarMaxPoints * LifePoints;
            SolidBrush solidBrush = new SolidBrush(Color.Black);
            Shape.FillRectangle(solidBrush, SubRect);
            solidBrush.Color = DynamicColorChange ? ((double)BarWidth / Size.Width > 0.6 ? InnerColor : (double)BarWidth / Size.Width > 0.3 ? Color.Yellow : Color.Red) : InnerColor;
            Rect = new Rectangle(Pos, new Size(BarWidth, Size.Height));
            Shape.FillRectangle(solidBrush, Rect);
            solidBrush.Dispose();
            Shape.DrawString($"{LifePoints}/{BarMaxPoints}", new Font(Settings.MainFont, 8, FontStyle.Bold), Brushes.White, Pos.X, Pos.Y + Size.Height);
        }
    }
}
