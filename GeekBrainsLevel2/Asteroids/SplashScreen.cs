using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Asteroids
{
    class SplashScreen
    {
        public static List<Button> ButtonsList { get; set; } = new List<Button>();
        public static Button NewGameBtn = new Button()
        {
            Name = Settings.ButtonNames[0],
            Size = Settings.ButtonSize,
            Text = Settings.GameStart,
            Font = new Font(Settings.MainFont, 18F, FontStyle.Italic),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            FlatStyle = FlatStyle.Popup,
            Location = new Point(Settings.FieldWidth / 2 - Settings.ButtonSize.Width / 2, 10)
        };
        public static Button ExitBtn = new Button()
        {
            Name = Settings.ButtonNames[1],
            Size = Settings.ButtonSize,
            Text = Settings.GameEnd,
            Font = new Font(Settings.MainFont, 18F, FontStyle.Italic),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            FlatStyle = FlatStyle.Popup,
            Location = new Point(Settings.FieldWidth / 2 - Settings.ButtonSize.Width / 2, 10)
        };
        public static Button GameContinueBtn = new Button()
        {
            Name = Settings.ButtonNames[2],
            Size = Settings.ButtonSize,
            Text = Settings.GameContinue,
            Font = new Font(Settings.MainFont, 18F, FontStyle.Italic),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            FlatStyle = FlatStyle.Popup,
            Location = new Point(Settings.FieldWidth / 2 - Settings.ButtonSize.Width / 2, 10)
        };
        public static Button RecordsBtn = new Button()
        {
            Name = Settings.ButtonNames[3],
            Size = Settings.ButtonSize,
            Text = Settings.Records,
            Font = new Font(Settings.MainFont, 18F, FontStyle.Italic),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            FlatStyle = FlatStyle.Popup,
            Location = new Point(Settings.FieldWidth / 2 - Settings.ButtonSize.Width / 2, 10)
        };
        public static void ControlsInit(Form form)
        {
            ButtonsList.Clear();
            ButtonsList.Add(GameContinueBtn);
            ButtonsList.Add(NewGameBtn);
            ButtonsList.Add(RecordsBtn);
            ButtonsList.Add(ExitBtn);
            ShowMenu(form, true);
            foreach (var b in ButtonsList)
                form.Controls.Add(b);
            
            #region Events description
            // Start game
            NewGameBtn.Click += (object sender, EventArgs e) =>
            {
                ShowMenu(form, false);
                if (MessageBox.Show("Game have been started!\n" + Settings.GameRules, $"Hello, {Settings.UserName}!",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    Game.Restart();
            };
            // Exit game
            ExitBtn.Click += (object sender, EventArgs e) =>
            {
                if (MessageBox.Show("Are you sure you want to get out?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Application.Exit();
            };
            // Resume game
            GameContinueBtn.Click += (object sender, EventArgs e) =>
            {
                if (Game.GameStarting)
                {
                    Game.Pause();
                    form.Focus();
                }
            };
            // Records
            RecordsBtn.Click += (object sender, EventArgs e) =>
            {
                ShowRecordsFile();
            };
            // Control buttons
            form.KeyDown += (object sender, KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.Space) Game.Ship?.Fire(true);
                if (e.KeyCode == Keys.Up) Game.Ship?.Up(true);
                if (e.KeyCode == Keys.Down) Game.Ship?.Down(true);
                if (e.KeyCode == Keys.Left) Game.Ship?.Left(true);
                if (e.KeyCode == Keys.Right) Game.Ship?.Right(true);
                if (e.KeyCode == Keys.Escape && Game.GameStarting)
                {
                    Game.Pause();
                }
            };
            form.KeyUp += (object sender, KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.Space) Game.Ship?.Fire(false);
                if (e.KeyCode == Keys.Up) Game.Ship?.Up(false);
                if (e.KeyCode == Keys.Down) Game.Ship?.Down(false);
                if (e.KeyCode == Keys.Left) Game.Ship?.Left(false);
                if (e.KeyCode == Keys.Right) Game.Ship?.Right(false);
            };
            #endregion
        }
        public static int GetStartPos() => 
            Settings.FieldHeight / 2 - (ButtonsList.Where(b => b.Visible).ToList().Count * Settings.ButtonSize.Height + 
            ButtonsList.Where(b => b.Visible).ToList().Count * Settings.HeightBetweenButtons) / 2;

        public static void ShowMenu(Form form, bool isShow)
        {
            foreach (Button btn in ButtonsList)
            {
                btn.Visible = isShow;
                if (btn.Name == Settings.ButtonNames[2])
                    btn.Visible = Game.GameStarting && isShow;
            }
            int startPos = GetStartPos();
            int deltaHeight = 0;
            foreach (Button btn in ButtonsList)
            {
                if (btn.Visible)
                {
                    btn.Location = new Point(btn.Location.X, startPos + deltaHeight);
                    deltaHeight += Settings.ButtonSize.Height + Settings.HeightBetweenButtons;
                }
            }
        }
        public static void ShowRecordsFile()
        {
            try
            {
                if (File.Exists(Settings.RecordsFile)) System.Diagnostics.Process.Start(Settings.RecordsFile);
            }
            catch (Exception e) { MessageBox.Show(e.Message, "Error!"); }
        }
    }
}
