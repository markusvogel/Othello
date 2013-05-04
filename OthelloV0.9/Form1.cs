using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OthelloV0._9
{
    public partial class Form1 : Form
    {
        const int size = 6;
        public int[,] spielFeld = new int[size, size];
        public Color[] buttonColor = { Color.Green, Color.Black, Color.White };
        public bool player1;

        public void clearMessage()
        {
            showMessage("");
        }

        public void showMessage(string msg)
        {
            label1.Text = msg;
        }

        public void nextPlayer()
        {
            label2.Text = player1 ? "WEISS" : "SCHWARZ";
            player1 = !player1;
        }

        /// <summary>
        /// aktualisiert Model nach gesetztem Stein, wendet also die eingeschlossenen Steine
        /// </summary>
        /// <param name="pos">Position des neuen Steins</param>
        /// <returns>Anzahl gewendete Steine</returns>
        public int updateModel(Position pos)
        {
            int x, y, c;
            int n = 0;

            //check Right
            if (pos.X < size - 3)
            {
                x = pos.X + 1;
                y = pos.Y;
                c = spielFeld[pos.X, pos.Y];
                while (x < size && spielFeld[x, y] != c && spielFeld[x, y] != 0)
                    x++;
                if (x != size && spielFeld[x, y] != 0)
                {
                    for (int i = pos.X + 1; i < x; i++)
                    {
                        spielFeld[i, y] = c;
                        n++;
                    }
                }
            }
            //check Left
            if (pos.X > 1)
            {
                x = pos.X - 1;
                y = pos.Y;
                c = spielFeld[pos.X, pos.Y];
                while (x >= 0 && spielFeld[x, y] != c && spielFeld[x, y] != 0)
                    x--;
                if (x >= 0 && spielFeld[x, y] != 0)
                {
                    for (int i = pos.X - 1; i > x; i--)
                    {
                        spielFeld[i, y] = c;
                        n++;
                    }
                }
            }
            //check Up
            if (pos.Y < size - 3)
            {
                x = pos.X;
                y = pos.Y + 1;
                c = spielFeld[pos.X, pos.Y];
                while (y < size && spielFeld[x, y] != c && spielFeld[x, y] != 0)
                    y++;
                if (y != size && spielFeld[x, y] != 0)
                {
                    for (int i = pos.Y + 1; i < y; i++)
                    {
                        spielFeld[x, i] = c;
                        n++;
                    }
                }
            }
            //check Down
            if (pos.Y > 1)
            {
                x = pos.X;
                y = pos.Y - 1;
                c = spielFeld[pos.X, pos.Y];
                while (y >= 0 && spielFeld[x, y] != c && spielFeld[x, y] != 0)
                    y--;
                if (y >= 0 && spielFeld[x, y] != 0)
                {
                    for (int i = pos.Y - 1; i > y; i--)
                    {
                        spielFeld[x, i] = c;
                        n++;
                    }
                }
            }
            //ToDo: check diagonal in vier Richtungen

            return n;
        }

        /// <summary>
        /// setzt Spielstein
        /// </summary>
        /// <param name="pos">Position des neuen Steins</param>
        /// <returns>true wenn Stein gesetzt werden kann</returns>
        public bool setzeSpielStein( Position pos)
        {
            bool ret = false;
            if (spielFeld[pos.X, pos.Y] == 0)
            {
                if (player1)
                    spielFeld[pos.X, pos.Y] = 1;
                else
                    spielFeld[pos.X, pos.Y] = 2;

                if (updateModel(pos) > 0)
                {
                    updateView();
                    ret = true;
                }
                else
                {
                    spielFeld[pos.X, pos.Y] = 0;
                    showMessage("Feld nicht erlaubt");
                }
            }
            else
                showMessage("Feld bereits belegt");
            return ret;
        }

        /// <summary>
        /// aktualisiert View basierend auf Model
        /// </summary>
        public void updateView()
        {
            int s = 0;
            int w = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Button stone = tableLayoutPanel1.GetControlFromPosition(i, j) as Button;
                    stone.BackColor = buttonColor[spielFeld[i, j]];
                    if (spielFeld[i, j] == 1)
                        s++;
                    else if (spielFeld[i, j] == 2)
                        w++;
                    label3.Text = "S: " + s.ToString();
                    label4.Text = "W: " + w.ToString();
                }
            }
        }

        /// <summary>
        /// initialisiert Form
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //Inititalisiert Spiel
            player1 = false;
            nextPlayer();

            //initialisiert View
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    spielFeld[i,j] = 0;
                    Button stone = tableLayoutPanel1.GetControlFromPosition(i, j) as Button;
                    //b.Text = "But" + i.ToString() + "_" + j.ToString();
                    stone.Text = "";
                    stone.Click += button_Click;
                    stone.Tag = new Position(i, j);
                }
            }

            //setzt Grundstellung Spiel in Model
            spielFeld[2, 2] = 1;
            spielFeld[2, 3] = 2;
            spielFeld[3, 2] = 2;
            spielFeld[3, 3] = 1;

            //aktualisiert View
            updateView();
        }

        private void button_Click(object sender, EventArgs e)
        {
            clearMessage();
            Position pos = (sender as Button).Tag as Position;
            if (setzeSpielStein(pos))
              nextPlayer();
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
