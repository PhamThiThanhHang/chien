using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Media;
using System.IO;

namespace game2048doAn
{
    public partial class Form1 : Form
    {
        private int kc = 10;
        private Label[,] show = new Label[4, 4];
        private int[,] hide = new int[4, 4];
        private int highs;
        private int score ;
        Stack<int[,]> st = new Stack<int[,]> ();
        Stack<int> st_score = new Stack<int>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    show[i, j] = new Label();
                    show[i, j].Location = new Point(kc + i * (100 + kc), kc + j * (100 + kc));
                    show[i, j].Size = new Size(100, 100);
                    show[i, j].Font = new Font("Consolas", 18, FontStyle.Regular);
                    show[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    show[i, j].BorderStyle = BorderStyle.FixedSingle;
                    this.Controls.Add(show[i, j]);
                }
            }
            init();
        }
        private void Form1_design(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (hide[i, j] == 0)
                        show[i, j].Text = " ";
                    if (hide[i, j] != 0)
                    {
                        show[i, j].Text = hide[i, j].ToString();
                    }
                    datMau(i, j);
                }
            }
            diem.Text = score.ToString();
            diemcao.Text = highs.ToString();
        }
        public void datMau(int i, int j)    
        {
            if (hide[i, j] == 0) show[i, j].BackColor = Color.Gray;
            if (hide[i, j] == 2) show[i, j].BackColor = Color.Cyan;
            if (hide[i, j] == 4) show[i, j].BackColor = Color.Crimson;
            if (hide[i, j] == 8) show[i, j].BackColor = Color.DarkViolet;
            if (hide[i, j] == 16) show[i, j].BackColor = Color.Gold;
            if (hide[i, j] == 32) show[i, j].BackColor = Color.Green;
            if (hide[i, j] == 64) show[i, j].BackColor = Color.Indigo;
            if (hide[i, j] == 128) show[i, j].BackColor = Color.DeepSkyBlue;
            if (hide[i, j] == 256) show[i, j].BackColor = Color.DeepPink;
            if (hide[i, j] == 512) show[i, j].BackColor = Color.Magenta;
            if (hide[i, j] == 1024) show[i, j].BackColor = Color.MediumPurple;
            if (hide[i, j] == 2048) show[i, j].BackColor = Color.MediumVioletRed;
            if (hide[i, j] == 4096) show[i, j].BackColor = Color.Yellow;
        }
        public void creatRandomCard()
        {
            //sinh 1 số tại vị trí x,y
            int x, y;
            Random rd = new Random();
            do
            {
                x = rd.Next(0, 4);
                y = rd.Next(0, 4);
            } while (hide[x, y] != 0);
            int s = rd.Next(0, 100);
            if (s > 90) hide[x, y] = 4;
            else hide[x, y] = 2;
        }
        public void init()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    hide[i, j] = 0;
            creatRandomCard();
            creatRandomCard();
            score = 0;
            st = new Stack<int[,]>();
            st_score = new Stack<int>();
            this.Refresh();
        }
        public void luuVaoStack()
        {
            int[,] k = new int[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    k[i, j] = hide[i, j];
            st.Push(k);
            st_score.Push(score);
        }
        public void kiemTra( bool isDo)
        {
            if ((isDo) == true)
            {
                creatRandomCard();
            }
            else
            {
                st.Pop();
                st_score.Pop();
            }
        }
        public void Up()
         {
            luuVaoStack();
             bool isDo=false;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int y1 = y + 1; y1 < 4; y1++)
                    {
                        if (hide[x, y1] > 0)
                        {
                            if (hide[x, y] == 0)
                            {
                                hide[x, y] = hide[x, y1];
                                hide[x, y1] = 0;
                                y--;
                                isDo = true;
                            }
                            else if (hide[x, y] == hide[x, y1])
                            {
                                hide[x, y] *= 2;
                                hide[x, y1] = 0;
                                score += hide[x, y];
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            kiemTra( isDo);
        }
        public void Down()
        {
            luuVaoStack();
            bool isDo = false;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 3; y >= 1; y--)
                {
                    for (int y1 = y - 1; y1 >= 0; y1--)
                    {
                        if (hide[x, y1] > 0)
                        {
                            if (hide[x, y] == 0)
                            {
                                hide[x, y] = hide[x, y1];
                                hide[x, y1] = 0;
                                y++;
                                isDo = true;
                            }
                            else if (hide[x, y] == hide[x, y1])
                            {
                                hide[x, y] *= 2;
                                hide[x, y1] = 0;
                                score += hide[x, y];
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            kiemTra( isDo);
        }
        public void Left()
        {
            luuVaoStack();
            bool isDo = false;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    for (int x1 = x + 1; x1 < 4; x1++)
                    {
                        if (hide[x1, y] > 0)
                        {
                            if (hide[x, y] == 0)
                            {
                                hide[x, y] = hide[x1, y];
                                hide[x1, y] = 0;
                                x--;
                                isDo = true;
                            }
                            else if (hide[x, y] == hide[x1, y])
                            {
                                hide[x, y] *= 2;
                                hide[x1, y] = 0;
                                score += hide[x, y];
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            kiemTra(isDo);
        }
        public void Right()
        {
            luuVaoStack();
            bool isDo = false;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 3; x >= 1; x--)
                {
                    for (int x1 = x - 1; x1 >= 0; x1--)
                    {
                        if (hide[x1, y] > 0)
                        {
                            if (hide[x, y] == 0)
                            {
                                hide[x, y] = hide[x1, y];
                                hide[x1, y] = 0;
                                x++;
                                isDo = true;
                            }
                            else if (hide[x, y] == hide[x1, y])
                            {
                                hide[x, y] *= 2;
                                hide[x1, y] = 0;
                                score += hide[x, y];
                                isDo = true;
                            }
                            break;
                        }
                    }
                }
            }
            kiemTra( isDo);
        }
        bool overGame()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (hide[x, y] == 0 ||(y < 3 && hide[x, y] == hide[x, y + 1]) || (x < 3 && hide[x, y] == hide[x + 1, y]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up) Up();
            if (e.KeyData == Keys.Down) Down();
            if (e.KeyData == Keys.Left) Left();
            if (e.KeyData == Keys.Right) Right();
            this.Refresh();  
            if (overGame())
            {
                DialogResult dia = MessageBox.Show("SCORE: " + score.ToString() + "\n" + "Bạn có muốn chơi lại không?", "Game Over!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dia == DialogResult.No) Application.Exit();
                if (dia == DialogResult.Yes)
                {
                    if (score > highs)
                    {
                        highs = score;
                        diemcao.Text = highs.ToString();
                    }
                    init();
                }
            }
        }
        private void undo_Click(object sender, EventArgs e)
        {
            if (st.Count > 0)
            {
                int[,] k = new int[4, 4];
                k = st.Pop();
                score = st_score.Pop();
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        hide[i, j] = k[i, j];
                diem.Text = score.ToString();
                this.Refresh();
            }
        }
        private void newGame_Click(object sender, EventArgs e)
        {
            if (score > highs)
            {
                highs = score;
                diemcao.Text = highs.ToString();
            }
            init();
        }
        private void help_Click(object sender, EventArgs e)
        {
            Help t = new Help();
            t.Show();
        }
    }
}
