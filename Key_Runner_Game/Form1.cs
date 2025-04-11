using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyRunnerGame
{
    public partial class Form1 : Form
    {


        bool goLeft, goRight, jump, hasKey;

        int jumpSpeed = 10;
        int force = 8;
        int score = 0;


        int playerSpeed = 8;
        int backgroundSpeed = 10;



        public Form1()
        {
            InitializeComponent();
            txtScore.Parent = background;
            txtScore.Left = 12;
            txtScore.BringToFront();
            
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Bani: " + score;
            player.Top += jumpSpeed;

            if (goLeft == true && player.Left > 80)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left + (player.Width + 80) < this.ClientSize.Width )
            {
                player.Left += playerSpeed;
            }


            if (goLeft == true && background.Left < 0)
            {
                txtScore.Left -= backgroundSpeed;
                background.Left += backgroundSpeed;
                MoveGameElements("forward");
            }

            if (goRight == true && background.Left > -1400)
            {
                txtScore.Left += backgroundSpeed;
                background.Left -= backgroundSpeed;
                MoveGameElements("back");
            }

            if (jump == true)
            {
                jumpSpeed = -10;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            if (jump == true && force < 0)
            {
                jump = false;
            }

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "platform")
                {

                    if (player.Bounds.IntersectsWith(x.Bounds) && jump == false)
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;
                        jumpSpeed = 0;
                    }

                    x.BringToFront();

                }
                if (x is PictureBox && (string)x.Tag == "coin")
                {

                    if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        score += 1;
                    }

                }
            }

            if (player.Bounds.IntersectsWith(key.Bounds))
            {
                key.Visible = false;
                hasKey = true;
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && hasKey == true)
            {
                door.Image = Properties.Resources.door_open;
                GameTimer.Stop();
                MessageBox.Show("Felicitari ! Ai castigat ! " + Environment.NewLine + "Apasa pe butonul OK pentru a reincepe jocul !");
                RestartGame();
            }

            if (player.Top + player.Height > this.ClientSize.Height)
            {
                GameTimer.Stop();
                MessageBox.Show("Ai pierdut !" + Environment.NewLine + "Apasa pe butonul OK pentru a reincepe jocul !");
                RestartGame();
            }
        }


        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jump == false)
            {
                jump = true;
            }


        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jump == true)
            {
                jump = false;
            }
        }

        private void CloseGame(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void RestartGame()
        {
            Form1 newWindow = new Form1();
            newWindow.Show();
            this.Hide();
        }

        private void MoveGameElements(string direction)
        {

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "coin" || x is PictureBox && (string)x.Tag == "key" || x is PictureBox && (string)x.Tag == "door")
                {

                    if (direction == "back")
                    {
                        x.Left -= backgroundSpeed;
                    }
                    if (direction == "forward")
                    {
                        x.Left += backgroundSpeed;
                    }


                }
            }



        }

    }
}
