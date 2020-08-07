using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace ProiectSpaceInvaders
{
    
    public partial class Form1 : Form
    {
        bool stanga;
        bool dreapta;
        int viteza = 3; // sinucigas = 5
        int scor = 0;
        bool apasat;
        int nrextraterestrii = 24;
        int vitezajucator = 6;
        int sec = 3;
        bool instantalaser=false;
        int timerlaser;

        SoundPlayer muzicameniu = new SoundPlayer();   

        bool startjoc=false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Player_Click(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.A )
            {
                stanga = true;
            }
            if (e.KeyCode == Keys.D )
            {
                dreapta = true;
            }
            if (e.KeyCode == Keys.Space && !apasat)
            {
                if (startjoc == true && instantalaser==false)
                {
                    armalaser();
                    instantalaser = true;
                    timerlaser = 1;
                    Lasertimer.Start();
                }
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                stanga = false;
            }
            if (e.KeyCode == Keys.D)
            {
                dreapta = false;
            }
            if (apasat)
            {
                apasat = false;
            }
        }

        private void armalaser()
        {
            Label laser = new Label();
            laser.Size = new Size(5, 20);
            laser.Tag = "laser";
            laser.BackColor = Color.MintCream;
            laser.Left = Player.Left + Player.Width / 2;
            laser.Top = Player.Top - 20;
            this.Controls.Add(laser);
           // laser.BringToFront();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (stanga && Player.Left > 10)
            {
                Player.Left -= vitezajucator;
            }
            else if (dreapta && Player.Left<625)
            {
                Player.Left += vitezajucator;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "Extraterestru")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(Player.Bounds)) 
                    {
                        
                        sfarsitJoc();
                        instructiuniOk.Visible = false;
                        startjoc = false;
                        PanelSfarsitJoc.Visible = true;
                        PanelSfarsitJoc.BringToFront();
                        BannerPierdut.Visible = true;
                        BannerPierdut.BringToFront();
                        labelpierdut.Visible = true;
                        labelpierdut.Text = "Scor : " + scor;
                        labelpierdut.BringToFront();
                        BannerPierdut.BringToFront();
                        buttoninchide.Visible = true;
                        buttoninchide.BringToFront();
                        label3.BringToFront();
                    }
                   ((PictureBox)x).Left += viteza;
                    if (((PictureBox)x).Left>720)
                    {
                        ((PictureBox)x).Top += ((PictureBox)x).Height + 10;
                        ((PictureBox)x).Left = -50;
                    }
                }
            }

            foreach (Control y in this.Controls)
            {
                if (y is Label && y.Tag == "laser") 
                {
                    y.Top -= 20;
                    if (((Label)y).Top <this.Height-490)
                    {
                        this.Controls.Remove(y);                       
                    }
                }
            }

            foreach (Control i in this.Controls)
            {
                foreach (Control j in this.Controls)
                {
                    if (i is PictureBox && i.Tag=="Extraterestru")
                    {
                        if (j is Label && j.Tag == "laser")
                        {
                            if (i.Bounds.IntersectsWith(j.Bounds))
                            {
                                scor++;                                
                                this.Controls.Remove(i);
                                this.Controls.Remove(j);
                            }
                        }
                    }
                }
            }

            label1.Text = "Scor : " + scor;
            if (scor>nrextraterestrii-1)
            {
                
                instructiuniOk.Visible = false;
                startjoc = false;
                sfarsitJoc();
                PanelSfarsitJoc.Visible = true;
                PanelSfarsitJoc.BringToFront();
                BannerVictorie.Visible = true;
                buttoninchide.BringToFront();
                buttoninchide.Visible = true;
                BannerVictorie.BringToFront();
                label3.BringToFront();
            }

        }
        private void sfarsitJoc()
        {
            timer1.Stop();           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
          //  muzicameniu.SoundLocation = ("F:\\ProiectSpeisInvadersFinal\\muzicameniu.wav");
          //  muzicameniu.PlayLooping();
            PanelSfarsitJoc.Visible = false;
            buttoninchide.Visible = false;
            PictureBoxInstructiuni.Visible = false;
            instructiunibox.Visible = false;
            label3.BringToFront();
            button3.BringToFront();
            LabelDificultate.Visible = false;
            foreach (Control x in this.Controls)
            {
                if (x.Tag == "Extraterestru" && x is PictureBox)
                    x.Visible = false;
            }
                    Player.Visible = false;
            label1.Visible = false;
            BannerVictorie.Visible = false;
            BannerPierdut.Visible = false;
            labelpierdut.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (viteza == 3)
            {
                LabelDificultate.Text = ("EzPz");
            }
            else if (viteza == 4)
            {
                LabelDificultate.Text = ("Mediocru");
            }
            else if (viteza == 5)
            {
               LabelDificultate.Text = ("Sinucigas");         
            }
            LabelDificultate.Visible = true;
            timer2.Start();
            this.Controls.Remove(Banner);
            this.Controls.Remove(button1);
            this.Controls.Remove(BannerGif);
            this.Controls.Remove(button2);
            this.Controls.Remove(button3);
            muzicameniu.Stop();
            Player.Visible = true; 
            label1.Visible  = true;
            foreach (Control x in this.Controls)
            {
                if (x.Tag == "Extraterestru" && x is PictureBox)
                    x.Visible = true;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        { 
            label2.Text = Convert.ToString(sec);
            sec--;
            if (sec <0)
            {
                timer1.Start();
                startjoc = true;
                this.Controls.Remove(label2);
                timer2.Stop();
                LabelDificultate.Visible = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Lasertimer_Tick(object sender, EventArgs e) 
        {
            timerlaser--;
            if (timerlaser == 0)
            {
                instantalaser = false;
                Lasertimer.Stop();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PictureBoxInstructiuni.Visible = true;
            PanelSfarsitJoc.Visible = true;
            instructiunibox.Visible = true;
            instructiuniOk.Visible = true;
            button3.Visible = false;
        }

        private void buttoninchide_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {

        }

        private void instructiuniOk_Click(object sender, EventArgs e)
        {
            PictureBoxInstructiuni.Visible = false;
            PanelSfarsitJoc.Visible = false;
            instructiunibox.Visible = false;
            instructiuniOk.Visible = false;
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (viteza==3)
            {
                button3.Text = ("Mediocru");
                viteza = 4;
            }
           else if (viteza==4)
            {
                button3.Text = ("Sinucigas");
                viteza = 5;
            }
          else  if (viteza==5)
            {
                button3.Text = ("EzPz");
                viteza = 3;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LabelDificultate_Click(object sender, EventArgs e)
        {

        }
    }
}
 