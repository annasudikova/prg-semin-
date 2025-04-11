using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace malovani
{
    public partial class Form1 : Form
    {
        Graphics g;
        bool mouseDown;
        Point lastPosition;
        bool colorRed;
        bool colorBlack;
        bool eraser;
        bool panelBlack;
        bool myPen;
        private int trackBarValue = 0; // Proměnná pro uložení hodnoty
        public Form1()
        {
            InitializeComponent();
            g=panel1.CreateGraphics();
            panel1.Refresh();
            InitializeComponent();
            
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_mouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastPosition = e.Location;
        }

        private void panel1_mouseMove(object sender, MouseEventArgs e) //barva pera
        {
            if (mouseDown == true)
            {
                Pen myPen= new Pen(Brushes.Black);
                myPen.Width= trackBarValue;
                if (colorRed == true)
                {
                    g.DrawLine(Pens.Red, e.Location, lastPosition);
                    lastPosition = e.Location;
                }
                if (colorBlack == true)
                {
                    g.DrawLine(Pens.Black, e.Location, lastPosition);
                    lastPosition = e.Location;
                }
                if (eraser == true && panelBlack == true)
                {
                    g.DrawLine(Pens.Black, e.Location, lastPosition);
                    lastPosition = e.Location;
                }




            }
        }

        private void panel1_mouseUp(object sender, MouseEventArgs e) 
        {
            mouseDown = false;
        }

        private void buttonBlack_Click(object sender, EventArgs e) //cerne platno
        {
            colorBlack = true;
            colorRed = false;
            eraser = false;
        }

        private void buttonRed_Click(object sender, EventArgs e) //cervene pero
        {
            colorRed = true;
            colorBlack= false;
            eraser = false;
        }

        private void buttonNew_Click(object sender, EventArgs e) //nove platno
        {
            panel1.Refresh();
        }

        private void buttonColor1_Click(object sender, EventArgs e) //barva pozadi
        {
            panel1.BackColor = Color.Black;
            panelBlack = true;
        }

        private void buttonEraser_Click(object sender, EventArgs e) //eraser
        {
            eraser= true;
            colorRed = false;
            colorBlack = false;

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBarValue = trackBar1.Value; // Uložení hodnoty
            
        }

        private void Value(object sender, EventArgs e)
        {
            
        }
    }
}
