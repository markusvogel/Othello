using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Comment added
namespace OthelloV0._9
{
    public partial class Form1 : Form
    {
        public int[,] spielFeld = new int[6, 6];
        public Color[] buttonColor = { Color.Green, Color.Black, Color.White };

        public void setzeSpielfeld( Position pos)
        {
            spielFeld[pos.X, pos.Y] = 1;
        }

        public void updateView()
        {
            for (int i = 0; i > 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    (tableLayoutPanel1.GetChildAtPoint(new Point(i, j)) as Button).BackColor = buttonColor[spielFeld[i, j]];
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    //ToDo:setzte Tag von Button
                    spielFeld[i,j] = 0;
                    tableLayoutPanel1.GetChildAtPoint(new Point(i, j)).Click += button_Click;
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Position pos = (sender as Button).Tag as Position;
            setzeSpielfeld(pos); 
        }
    }

    public class Position
    {
        private int m_x;
        public int X
        {
            get { return m_x; }
            set { m_x = value; }
        }
        private int m_y;
        public int Y
        {
            get { return m_y; }
            set { m_y = value; }
        }
        public Position(int x, int y)
        {
            m_x = x;
            m_y = y;
        }
    }

}
