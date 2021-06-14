using System.Collections.Generic;
using System.Drawing;
using St = Asteroids.Settings;

namespace Asteroids
{
    class EmpireShip : BaseObject
    {
        internal static int DestroyedShips { get; set; } = 0;
        internal static int CreatedShips { get; set; } = 0;
        internal static Image[] Images { get; set; } = new Image[4]
        {
            Properties.Resources.Advanced_x1_1,
            Properties.Resources.Advanced_x1_2,
            Properties.Resources.TIE_Interceptor_1,
            Properties.Resources.TIE_Interceptor_2
        };
        internal static bool IsCreateShipChanse() =>
            (CreatedShips < St.EmpireShipsCount[Game.CurrentDifficultyLevel]) &&
            (Rand.Next(St.EmpireShipsCreatingChance[Game.CurrentDifficultyLevel]) == Rand.Next(St.EmpireShipsCreatingChance[Game.CurrentDifficultyLevel]));

        private static int GetMinDirectionValue() =>
            Rand.Next(St.EmpireShipsDirection[Game.CurrentDifficultyLevel][0], St.EmpireShipsDirection[Game.CurrentDifficultyLevel][1]);

        private static int GetMaxDirectionValue() => (DestroyedShips + 2) / 2 * (Rand.Next(0, 2) * 2 - 1);

        int ImgIndex { get; set; } = Rand.Next(0, Images.Length);

        public EmpireShip(Point pos, Point dir, int power) : base(pos, dir, power) => CreatedShips++;

        public EmpireShip() : this(
                    new Point(Game.Width + Rand.Next(50, 100), Rand.Next(50, Game.Width)),
                    new Point(GetMinDirectionValue(), GetMaxDirectionValue()),
                    Rand.Next(St.EmpireShipMinDamage[Game.CurrentDifficultyLevel], St.EmpireShipMaxDamage[Game.CurrentDifficultyLevel])) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Images[ImgIndex], Pos);
            Rect = new Rectangle(Pos.X, Pos.Y, Images[ImgIndex].Size.Width, Images[ImgIndex].Size.Height);
            LifePoints = GetLifePointsQuantity();
            if (GetShotChance())
                Game.EnemiesBullets?.Add(new Bullet(
                    NewBulletPosition(),
                    new Point(St.EmpireShipBulletSpeed[Game.CurrentDifficultyLevel], 0),
                    GetShotPower(),
                    St.EmpireShipBulletIndex));
        }
        public void Hide() => Pos = new Point(St.FieldWidth + Size.Width, Rand.Next(0, St.FieldHeight - Size.Height));

        public void Die(List<EmpireShip> list, int i)
        {
            if (Game.Stats.Count > 0 && Game.Stats[0] != null) Game.Stats[0].StatValue += LifePoints;
            DestroyedShips++;
            Del(list, i);
        }

        private bool GetShotChance() =>
            Rand.Next(0, St.EmpireShipShotChance[Game.CurrentDifficultyLevel]) == Rand.Next(0, St.EmpireShipShotChance[Game.CurrentDifficultyLevel]);

        private static int GetLifePointsQuantity() =>
            Game.Rand.Next(St.EmpireShipMinDamage[Game.CurrentDifficultyLevel], St.EmpireShipMaxDamage[Game.CurrentDifficultyLevel]);

        private Point NewBulletPosition() => new Point(Pos.X - Bullet.EnemyBulletImg.Size.Width / 2, Pos.Y + Images[ImgIndex].Size.Height / 4);

        private int GetShotPower() => Rand.Next(St.EmpireShipMinDamage[Game.CurrentDifficultyLevel], St.EmpireShipMaxDamage[Game.CurrentDifficultyLevel]);
    }
}
