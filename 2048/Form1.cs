using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
        public class CActor
        {
            public int X, Y, val, bn;
            public bool hil;
            public Bitmap img;
            public Rectangle r;
            public Color cl;
        }
    public partial class Form1 : Form
        {
            Bitmap off;
            int nb = 4;
            int type;
            int steps = 200;
            int time = 120;
            Timer tfr = new Timer();
            bool isstart = false;
            List<CActor> eboard = new List<CActor>();
            List<CActor> tiles = new List<CActor>();
            List<CActor> butt = new List<CActor>();
            Random r = new Random();
            Random r2 = new Random();
            int score = 0;
            bool gameover = false;
            int[] n = { 2, 4 };
            CActor[,]bo = new CActor [4, 4];
            List<Point> pos = new List<Point>();
            public Form1()
            {
                this.Load += new EventHandler(Form1_Load);
                this.Paint += new PaintEventHandler(Form1_Paint);
                this.KeyDown += new KeyEventHandler(Form1_KeyDown);
                this.MouseMove += new MouseEventHandler(Form1_MouseMove);
                this.MouseDown += new MouseEventHandler(Form1_MouseDown);
                tfr.Tick += new EventHandler(tfr_Tick);
                tfr.Start();
                tfr.Interval = 1000;
                this.BackColor = Color.DarkGray;
                this.ClientSize = new Size(350, 450);
            }
            void tfr_Tick(object sender, EventArgs e)
            {
                if (type == 2)
                {
                    if (time > 0 && !gameover)
                    {
                        time--;
                    }
                    this.Text = time.ToString();
                    if (time == 0)
                    {
                        gameover = true;
                        //tfr.Stop();
                    }
                }
                DrawDubb(this.CreateGraphics());

            }
            void Form1_KeyDown(object sender, KeyEventArgs e)
            {
                if (!gameover)
                {
                    if (e.KeyCode == Keys.Right)
                    {
                        move(1);
                    }
                    if (e.KeyCode == Keys.Left)
                    {
                        move(2);
                    }
                    if (e.KeyCode == Keys.Up)
                    {
                        move(3);
                    }
                    if (e.KeyCode == Keys.Down)
                    {
                        move(4);
                    }
                }
                if(/*gameover &&*/ e.KeyCode == Keys.Space)
                {
                    steps = 200;
                    time = 120;
                    tfr = new Timer();
                    tiles = new List<CActor>();
                    r = new Random();
                    r2 = new Random();
                    score = 0;
                    gameover = false;
                    bo = new CActor[4, 4];
                    deploy_tile();
                    deploy_tile();
                  
                }
                DrawDubb(this.CreateGraphics());
            }
            void move(int d)
            {
                int moved = 0;
            if (d == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 2; k >= 0; k--)
                    {
                        if (bo[i, k] != null)
                        {
                            //animation(1, bo[i, k], i, k, bo[i, k + 1]);
                            if (bo[i, k + 1] == null)
                            {
                                bo[i, k + 1] = bo[i, k];
                                bo[i, k + 1].X = pos[bo[i, k + 1].bn].X;
                                bo[i, k + 1].Y = pos[bo[i, k + 1].bn].Y;
                                bo[i, k + 1].bn++;
                                bo[i, k] = null;
                                k = 3;
                                moved = 1;
                            }
                        }
                        
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 2; k >= 0; k--)
                    {
                        if (bo[i, k] != null && bo[i, k + 1] != null)
                        {
                            if (bo[i, k].val == bo[i, k + 1].val )
                            {
                                bo[i, k + 1].val *= 2;
                                set_color(bo[i, k + 1]);
                                bo[i, k] = null;
                                moved = 1;
                                score += bo[i, k + 1].val;
                            }
                        }

                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 2; k >= 0; k--)
                    {
                        if (bo[i, k] != null)
                        {
                            //animation(1, bo[i, k], i, k, bo[i, k +1 ]);
                            if (bo[i, k + 1] == null)
                            {
                                bo[i, k + 1] = bo[i, k];
                                bo[i, k + 1].X = pos[bo[i, k + 1].bn].X;
                                bo[i, k + 1].Y = pos[bo[i, k + 1].bn].Y;
                                bo[i, k + 1].bn++;
                                bo[i, k] = null;
                                k = 3;
                                moved = 1;
                            }
                        }

                    }
                }
                if (moved == 1)
                {
                    steps--;
                    deploy_tile();
                }
            }
            if(d == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (bo[i, k + 1] != null)
                        {
                            if(bo[i,k] == null)
                            {
                                bo[i, k] = bo[i, k + 1];
                                bo[i, k].X = pos[bo[i, k + 1].bn - 2].X;
                                bo[i, k].Y = pos[bo[i, k + 1].bn - 2].Y;
                                bo[i, k ].bn -= 1;
                                bo[i, k + 1] = null;
                                k = -1;
                                moved = 1;
                            }
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (bo[i, k + 1] != null && bo[i, k] != null)
                        {
                            if(bo[i,k].val == bo[i,k + 1].val)
                            {
                                bo[i, k].val *= 2;
                                set_color(bo[i, k]);
                                bo[i, k + 1] = null;
                                moved = 1;
                                score += bo[i, k].val;
                            }
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (bo[i, k + 1] != null)
                        {
                            if (bo[i, k] == null)
                            {
                                bo[i, k] = bo[i, k + 1];
                                bo[i, k].X = pos[bo[i, k + 1].bn - 2].X;
                                bo[i, k].Y = pos[bo[i, k + 1].bn - 2].Y;
                                bo[i, k].bn -= 1;
                                bo[i, k + 1] = null;
                                k = -1;
                                moved = 1;

                            }
                        }
                    }
                }
                if (moved == 1)
                {
                    steps--;
                    deploy_tile();
                }
            }
            if(d == 3)
            {
                for(int i = 0; i < 4;i++)
                {
                    for(int k = 0; k < 3; k++)
                    {
                        if(bo[k + 1, i] != null)
                        {
                            if(bo[k , i] == null )
                            {
                                bo[k, i] = bo[k + 1, i];
                                bo[k, i].X = pos[bo[k + 1, i].bn - 5].X;
                                bo[k, i].Y = pos[bo[k + 1, i].bn - 5].Y;
                                bo[k, i].bn -= 4;
                                bo[k + 1, i] = null;
                                k = -1;
                                moved = 1;
                            }
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (bo[k, i] != null && bo[k + 1, i] != null)
                        {
                            if (bo[k + 1, i].val == bo[k, i].val)
                            {
                                bo[k, i].val *= 2;
                                set_color(bo[k, i]);
                                bo[k + 1, i] = null;
                                moved = 1;
                                score += bo[k, i].val;
                            }
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (bo[k + 1, i] != null)
                        {
                            if (bo[k, i] == null)
                            {
                                bo[k, i] = bo[k + 1, i];
                                bo[k, i].X = pos[bo[k + 1, i].bn - 5].X;
                                bo[k, i].Y = pos[bo[k + 1, i].bn - 5].Y;
                                bo[k, i].bn -= 4;
                                bo[k + 1, i] = null;
                                k = -1;
                                moved = 1;
                            }
                        }
                    }
                }
                if (moved == 1)
                {
                    steps--;
                    deploy_tile();
                }
            }
            if(d == 4)
            {
                for(int i = 0; i < 4; i++)
                {
                    for(int k = 2; k >= 0; k--)
                    {
                        if(bo[k, i] != null)
                        {
                            if(bo[k + 1, i] == null)
                            {
                                bo[k + 1, i] = bo[k, i];
                                bo[k + 1, i].X = pos[bo[k + 1, i].bn + 3].X;
                                bo[k + 1, i].Y = pos[bo[k + 1, i].bn + 3].Y;
                                bo[k + 1, i].bn += 4;
                                bo[k, i] = null;
                                k = 3;
                                moved = 1;
                            }
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 3; k >= 1; k--)
                    {
                        if (bo[k, i] != null && bo[k - 1, i] != null)
                        {
                            if (bo[k, i].val == bo[k - 1, i].val)
                            {
                                bo[k, i].val *= 2;
                                set_color(bo[k, i]);
                                bo[k - 1, i] = null;
                                moved = 1;
                                score += bo[k, i].val;
                            }
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 2; k >= 0; k--)
                    {
                        if (bo[k, i] != null)
                        {
                            if (bo[k + 1, i] == null)
                            {
                                bo[k + 1, i] = bo[k, i];
                                bo[k + 1, i].X = pos[bo[k + 1, i].bn + 3].X;
                                bo[k + 1, i].Y = pos[bo[k + 1, i].bn + 3].Y;
                                bo[k + 1, i].bn += 4;
                                bo[k, i] = null;
                                k = 3;
                                moved = 1;
                            }
                        }
                    }
                }
                if (moved == 1)
                {
                    steps--;
                    deploy_tile();
                }
            }
            if(steps == 0)
            {
                gameover = true;
            }
            DrawDubb(this.CreateGraphics());
            isdead();
            }
            void isdead()
            {
            int moved = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 2; k >= 0; k--)
                    {
                        if (bo[k, i] != null)
                        {
                            if (bo[k + 1, i] == null)
                            {
                                moved = 1;
                            }
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (bo[k + 1, i] != null)
                        {
                            if (bo[k, i] == null)
                            {
                                moved = 1;
                            }
                        }
                    }
                }
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (bo[i, k + 1] != null && bo[i, k] != null)
                    {
                        if (bo[i, k].val == bo[i, k + 1].val)
                        {
                            moved = 1;
                        }
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int k = 2; k >= 0; k--)
                {
                    if (bo[i, k] != null && bo[i, k + 1] != null)
                    {
                        if (bo[i, k].val == bo[i, k + 1].val)
                        {
                            moved = 1;
                        }
                    }

                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int k = 3; k >= 1; k--)
                {
                    if (bo[k, i] != null && bo[k - 1, i] != null)
                    {
                        if (bo[k, i].val == bo[k - 1, i].val)
                        {
                            moved = 1;
                        }
                    }
                }
            }
            if (moved == 0)
                {
                    gameover = true;
                }
            }
            void DrawScene(Graphics g)
            {
                    g.Clear(this.BackColor);
                    SolidBrush b;
                    SolidBrush b2 = new SolidBrush(Color.Black);
                    Font fn = new Font("system", 24);
                if (isstart)
                {
                    for (int i = 0; i < eboard.Count; i++)
                    {
                        b = new SolidBrush(Color.LightGray);
                        g.FillRectangle(b, eboard[i].X, eboard[i].Y, 75, 75);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            if (bo[i, k] != null)
                            {
                                string t = bo[i, k].val.ToString();

                                b = new SolidBrush(bo[i, k].cl);
                                g.FillRectangle(b, bo[i, k].X, bo[i, k].Y, 75, 75);
                                if (bo[i, k].val < 10)
                                {
                                    g.DrawString(bo[i, k].val.ToString(), fn, b2, bo[i, k].X + 21, bo[i, k].Y + 20);
                                }
                                if (bo[i, k].val < 100 && bo[i, k].val >= 10)
                                {
                                    g.DrawString(bo[i, k].val.ToString(), fn, b2, bo[i, k].X + 11, bo[i, k].Y + 20);
                                }
                                if (bo[i, k].val < 1000 && bo[i, k].val >= 100)
                                {
                                    g.DrawString(bo[i, k].val.ToString(), fn, b2, bo[i, k].X + 4, bo[i, k].Y + 20);
                                }
                                if (bo[i, k].val > 1000)
                                {
                                    g.DrawString(bo[i, k].val.ToString(), fn, b2, bo[i, k].X + 1, bo[i, k].Y + 15);
                                }
                            }
                        }
                    }
                }
                if(!isstart)
                {
                    for(int i = 0; i < 3; i++)
                    {
                        SolidBrush bbb = new SolidBrush(Color.Orange);
                        SolidBrush bbb2 = new SolidBrush(Color.FromArgb(200,100,100,100));
                        Font fnn = new Font("system", 20);
                        //g.FillRectangle(bbb, butt[i].X, butt[i].Y, 270, 50);
                        Bitmap bub = new Bitmap("b2.png");
                        g.DrawImage(bub, butt[i].X, butt[i].Y);
                        if(butt[i].hil)
                        {
                            g.FillRectangle(bbb2, butt[i].X, butt[i].Y, 270, 50);
                        }
                        if (i == 0)
                        {
                            g.DrawString("Normal Mode", fnn, b2, butt[i].X + 51, butt[i].Y + 7);
                        }
                        if (i == 1)
                        {
                            g.DrawString("Time Fighter", fnn, b2, butt[i].X + 60, butt[i].Y + 8);
                        }
                        if (i == 2)
                        {
                            g.DrawString("Steps Fighter", fnn, b2, butt[i].X + 60, butt[i].Y + 8);
                        }
                        Bitmap logo = new Bitmap("2048.png");
                        g.DrawImage(logo, 95, 7);
                    }
                }
                if(gameover)
                {
                    Font fn2 = new Font("system", 30);
                    Font fnn2 = new Font("system", 12);
                    SolidBrush s = new SolidBrush(Color.FromArgb(175, 100, 255, 200));
                    g.FillRectangle(s,0,0,this.ClientSize.Width,this.ClientSize.Height);
                    g.DrawString("GAME OVER", fn2, b2, this.ClientSize.Width/4 - 35, this.ClientSize.Height/2 - 15);
                    g.DrawString("press Space to replay", fnn2, b2, this.ClientSize.Width/4 + 22, this.ClientSize.Height/2 + 35);
                    Bitmap menu = new Bitmap("menu.png");
                    g.DrawImage(menu,this.ClientSize.Width/2 - 15, this.ClientSize.Height/2 + menu.Height + 10);
                }
                if (isstart)
                {
                    Font fnn = new Font("system", 16);
                    g.DrawString("Score: " + score.ToString(), fnn, b2, 5, 50);
                    Bitmap re = new Bitmap("menu.png");
                    g.DrawImage(re, 5, 0);
                }
                if(type == 2)
                {
                    Font fnn = new Font("system", 16);
                    g.DrawString("seconds left: "+time.ToString(), fnn, b2, 175, 50);
                    this.Text = "Time Fighter";
                }
                if(type == 3)
                {
                    Font fnn = new Font("system", 16);
                    g.DrawString("Steps left: "+steps.ToString(), fnn, b2, 200, 50);
                    this.Text = "Steps Fighter";
                }
            }
            void DrawDubb(Graphics g)
            {
                Graphics g2 = Graphics.FromImage(off);
                DrawScene(g2);
                g.DrawImage(off, 0, 0);
            }
            void Form1_Paint(object sender, PaintEventArgs e)
            {
                DrawDubb(e.Graphics);
            }
            void deploy_tile()
            {
               int f = 0, col;
               CActor p;
               while(true)
               {
                   f = 0;
                   int row = 0;
                   int rr = r.Next(0, pos.Count);
                if (rr + 1 > 0 && rr + 1 <= 4)
                {
                    row = 0;
                }
                if (rr + 1 > 4 && rr + 1 <= 8)
                {
                    row = 1;
                }
                if (rr + 1 > 8 && rr + 1 <= 12)
                {
                    row = 2;
                }
                if (rr + 1 > 12 && rr + 1 <= 16)
                {
                    row = 3;
                }
                col = rr % 4;
                if(bo[row, col] != null)
                {
                    f = 1;
                }
                if (f == 0)
                   {
                        p = new CActor();
                        p.X = pos[rr].X;
                        p.Y = pos[rr].Y;
                        p.bn = rr + 1;
                        p.val = n[r2.Next(0,2)];
                        set_color(p);
                        tiles.Add(p);
                        if(rr + 1 > 0 && rr + 1 <= 4 )
                        {
                            row = 0;
                        }
                        if (rr + 1 > 4 && rr + 1 <= 8)
                        {
                            row = 1;
                        }
                        if (rr + 1 > 8 && rr + 1 <= 12)
                        {
                            row = 2;
                        }
                        if (rr + 1 > 12 && rr + 1 <= 16)
                        {
                            row = 3;
                        }
                        col = rr % 4 ;
                        bo[row, col] = p;
                        break;
                   }
               }
            }
            void set_color(CActor p)
            {
                
                if(p.val == 2)
                {
                    p.cl = Color.FromArgb(238, 228, 218);
            }
                if (p.val == 4)
                {
                    p.cl = Color.FromArgb(204, 192, 180);
                
            }
                if (p.val == 8)
                {
                    p.cl = Color.FromArgb(252, 184, 113);
                }
                if (p.val == 16)
                {
                    p.cl = Color.FromArgb(254,162,89);
                }
                if (p.val == 32)
                {
                    p.cl = Color.FromArgb(255,143,87);
                }
                if (p.val == 64)
                {
                    p.cl = Color.FromArgb(255,117,50);
                }
                if (p.val == 128)
                {
                    p.cl = Color.FromArgb(241,203,102);
                }
                if (p.val == 256)
                {
                    p.cl = Color.FromArgb(245,197,86);
                }
                if (p.val == 512)
                {
                    p.cl = Color.FromArgb(245,194,66);
                }
                if (p.val == 1024)
                {
                    p.cl = Color.FromArgb(248,190,47);
                }
                if (p.val == 2048)
                {
                    p.cl = Color.FromArgb(247,187,29);
                }
        }
            void Form1_MouseMove(object sender, MouseEventArgs e)
            {
                if(!isstart)
                {
                    for(int i = 0; i < 3; i++)
                    {
                        if(e.X >= 40 && e.X <= 310 && e.Y >= butt[i].Y && e.Y <= butt[i].Y + 50)
                        {
                            butt[i].hil = true;
                        }
                        else
                        {
                            butt[i].hil = false;
                        }
                    }

                }
                DrawDubb(this.CreateGraphics());
            }
            void Form1_MouseDown(object sender, MouseEventArgs e)
            {
                if (!isstart)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (e.X >= 40 && e.X <= 310 && e.Y >= butt[i].Y && e.Y <= butt[i].Y + 50)
                        {
                            type = i+1;
                            isstart = true;
                            break;
                        }
                    }
                }
                if(gameover)
                {
                    if (e.X >= this.ClientSize.Width / 2 - 10 && e.X <= this.ClientSize.Width / 2 - 10 + 50 && e.Y >= this.ClientSize.Height / 2 + 60 && e.Y <= this.ClientSize.Height / 2 + 110)
                    {
                        type = 0;
                        steps = 200;
                        time = 120;
                        tfr = new Timer();
                        isstart = false;
                        tiles = new List<CActor>();
                        r = new Random();
                        r2 = new Random();
                        score = 0;
                        gameover = false;
                        bo = new CActor[4, 4];
                        deploy_tile();
                        deploy_tile();
                    }
                }
                if(isstart && e.X >= 5 && e.X <= 55 && e.Y >= 0 && e.Y <= 50)
                {
                        type = 0;
                        steps = 200;
                        time = 120;
                        tfr = new Timer();
                        isstart = false;
                        tiles = new List<CActor>();
                        r = new Random();
                        r2 = new Random();
                        score = 0;
                        gameover = false;
                        bo = new CActor[4, 4];
                        deploy_tile();
                        deploy_tile();
                }
            }
            void Form1_Load(object sender, EventArgs e)
            {
                off = new Bitmap(ClientSize.Width, ClientSize.Height);
                int[] pb =  { this.ClientSize.Height / 3 + 20,this.ClientSize.Height / 2 + 40,this.ClientSize.Height - 90};
                for(int i = 0; i < 3; i++)
                {
                    CActor pbb = new CActor();
                    pbb.X = 40;
                    pbb.Y = pb[i];
                    pbb.hil = false;
                    butt.Add(pbb);
                }
                int x = 10, y = 110;
                Point p;
                CActor pnn;
                for(int i = 0; i < 4; i++)
                {
                    p = new Point();
                    pnn = new CActor();
                    pnn.Y = y;
                    pnn.X = x;
                    eboard.Add(pnn);
                    p.X = pnn.X;
                    p.Y = pnn.Y;
                    pos.Add(p);
                    x += 85;
                }
                y += 85;
                x = 10;
                for (int i = 0; i < 4; i++)
                {
                    p = new Point();
                    pnn = new CActor();
                    pnn.Y = y;
                    pnn.X = x;
                    eboard.Add(pnn);
                    p.X = pnn.X;
                    p.Y = pnn.Y;
                    pos.Add(p);
                    x += 85;
                }
                y += 85;
                x = 10;
                for (int i = 0; i < 4; i++)
                {
                    p = new Point();
                    pnn = new CActor();
                    pnn.Y = y;
                    pnn.X = x;
                    eboard.Add(pnn);
                    p.X = pnn.X;
                    p.Y = pnn.Y;
                    pos.Add(p);
                    x += 85;
                }
                y += 85;
                x = 10;
                for (int i = 0; i < 4; i++)
                {
                    p = new Point();
                    pnn = new CActor();
                    pnn.Y = y;
                    pnn.X = x;
                    eboard.Add(pnn);
                    p.X = pnn.X;
                    p.Y = pnn.Y;
                    pos.Add(p);
                    x += 85;
                }
               /* pnn = new CActor();
                pnn.X = pos[1].X;
                pnn.Y = pos[1].Y;
                pnn.bn = 2;
                pnn.val = 2;
                set_color(pnn);
                bo[0,1] = pnn;
                pnn = new CActor();
                pnn.X = pos[5].X;
                pnn.Y = pos[5].Y;
                pnn.bn = 6;
                pnn.val = 4;
                set_color(pnn);
                bo[1, 1] = pnn;
                pnn = new CActor();
                pnn.X = pos[9].X;
                pnn.Y = pos[9].Y;
                pnn.bn = 10;
                pnn.val = 4;
                set_color(pnn);
                bo[2, 1] = pnn;
                pnn = new CActor();
                pnn.X = pos[13].X;
                pnn.Y = pos[13].Y;
                pnn.bn = 14;
                pnn.val = 8;
                set_color(pnn);
                bo[3, 1] = pnn;*/
                deploy_tile();
                deploy_tile();
                DrawDubb(this.CreateGraphics());
            }
        }
    }
