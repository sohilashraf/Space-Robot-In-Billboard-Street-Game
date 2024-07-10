using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Large_Game
{
    public class CAdvActor
    {
        public Rectangle rcDst, rcSrc;
        public Bitmap img;
    }
    class CActor
    {
        public int x, y, flagR, dy = -1, dxE = -1;
        public List<Bitmap> imgs = new List<Bitmap>();
        public int iframe = 0;
        public int ctTick = 1;

    }
    class CNode
    {
        public int x, y, dx = -1, dy = -1, dy2 = 1;
        public Bitmap img;
        public int ctTick = 1;

    }
    class CRec
    {
        public int x, y, w, h, dy = 1;
    }
    public partial class Form1 : Form
    {
        Bitmap off;

        List<CAdvActor> LBackGround = new List<CAdvActor>();
        List<CActor> LHero = new List<CActor>();

        List<CNode> LLader = new List<CNode>();
        List<CNode> LTiles = new List<CNode>();
        List<CRec> LELv = new List<CRec>();
        List<CNode> LBanner = new List<CNode>();
        List<CNode> LImgs = new List<CNode>();
        List<CActor> LBullet = new List<CActor>();
        List<CActor> LBulletD = new List<CActor>();

        List<CNode> LCoin = new List<CNode>();
        List<CNode> LMedicen = new List<CNode>();

        List<CNode> LFire = new List<CNode>();
        List<CAdvActor> LHeart = new List<CAdvActor>();
        List<CRec> LLazer = new List<CRec>();
        List<CActor> LEnemy1 = new List<CActor>();
        List<CNode> LEnemy1Bullet = new List<CNode>();

        List<CRec> LVertical = new List<CRec>();

        List<CNode> LFart = new List<CNode>();
        List<CNode> LShip = new List<CNode>();
        List<CNode> LOver = new List<CNode>();
        List<CNode> LKey = new List<CNode>();
        List<CNode> LDoor = new List<CNode>();






        Timer tt = new Timer();

        int ctTick = 0;
        int ctTick2 = 1;
        int ctTick3 = 0;
        int ctTick4 = 1;

        int flagL = 1;
        int flagLL = 0;
        int totL = 0;

        int flagG = 0;

        int flagbullet = 0;

        int flagctTick = 1;

        int flagCreateE1 = 1;
        int flagIsBulletD = 0;

        int ctCoin = 0;
        int ctKills = 0;

        bool left, right, s, d, z, space;

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            tt.Tick += Tt_Tick;
            tt.Start();
            tt.Interval = 10;
            this.MouseDown += Form1_MouseDown;

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    left = false;
                    LHero[0].iframe = 8;
                    break;
                case Keys.Right:
                    right = false;
                    LHero[0].iframe = 8;
                    break;
                case Keys.S:
                    s = false;
                    //if (LBullet.Count >= 0)
                    //{
                    //    LBullet[0].iframe = 0;
                    //}
                    break;
                case Keys.D:
                    d = false;
                    //if (LBulletD.Count >= 0)
                    //{
                    //    LBulletD[0].iframe = 0;
                    //}
                    break;

            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    left = true;
                    break;
                case Keys.Right:
                    right = true;
                    break;
                case Keys.Up:
                    flagLL = 1;

                    if (LHero[0].x >= LLader[0].x - 85 && LHero[0].x <= LLader[0].x + 20)
                    {
                        flagL = 2;
                    }
                    if (LHero[0].y + LHero[0].imgs[0].Height / 2 >= LLader[0].y && flagL == 2)
                    {
                        LHero[0].y -= 56;
                        LHero[0].x += 20 + totL;
                    }
                    else
                    {
                        flagLL = 0;
                        flagL = 0;
                    }
                    totL += 20;
                    break;
                case Keys.S:
                    s = true;
                    LHero[0].iframe = 18;
                    CreateBullet();
                    break;
                case Keys.D:
                    d = true;
                    LHero[0].iframe = 18;

                    if (LBulletD.Count == 0)
                    {
                        CreateDubleBullet();
                    }
                    break;
                case Keys.Z:
                    z = true;
                    LHero[0].iframe = 18;
                    DrawLaser();
                    break;
                case Keys.Space:
                    space = true;
                    break;
            }
        }
        void CheckGravity() 
        {
            flagG = 0;
            for (int i = 0; i < LImgs.Count; i++)
            {
                if (LHero[0].x + LHero[0].imgs[0].Width > LImgs[i].x &&
                    LHero[0].x < LImgs[i].x + LImgs[i].img.Width - 50&&
                    LHero[0].y < LImgs[i].y
                   )
                {
                    flagG = 1;
                }
            }
            
            if (LHero[0].x + LHero[0].imgs[0].Width > LELv[0].x &&
                LHero[0].x < LELv[0].x + LELv[0].w - 50 &&
                LHero[0].y < LELv[0].y
               )
            {
                flagG = 1;
            }
            
            if (flagG == 0)
            {
                if (LHero[0].y < this.Height - 235)
                {
                    LHero[0].y += 10;
                }
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Text = e.Y + "";
        }
        private void Tt_Tick(object sender, EventArgs e)
        {
            //jump
            if (space)
            {
                if (LHero[0].x >= this.Width / 2)
                {
                    if (LBackGround[0].rcSrc.X + this.Width <= LBackGround[0].img.Width)
                    {
                        LBackGround[0].rcSrc.X += 1 * 15;
                    }
                }

                LHero[0].x+= 15;
                if (flagctTick == 0)
                {
                    LHero[0].y += 10;
                }
                else
                {
                    LHero[0].y -= 10;
                }

                if (LHero[0].y >= this.Height - 235  && flagctTick == 0)
                {
                    space = false;
                    flagctTick = 1;
                    ctTick2 = 1;
                    LHero[0].y = this.Height - 235;
                }
               
            }
            if (ctTick2 % 15 == 0 && flagctTick == 1)
            {
                flagctTick = 0;
            }
            if (space)
            {
                ctTick2++;

            }


            //ground fire
            if (ctTick % 5 == 0)
            {
                for (int i = 0; i < LFire.Count; i++)
                {
                    LFire[i].dx *= -1;
                }
                for (int i = 0; i < LFart.Count; i++)
                {
                    LFart[i].dx *= -1;
                }
                for (int i = 0; i < LCoin.Count; i++)
                {
                    LCoin[i].dx *= -1;
                }
                for (int i = 0; i < LMedicen.Count; i++)
                {
                    LMedicen[i].dx *= -1;
                }
               
            }

            if (ctTick4 % 8 == 0)
            {
                for (int i = 0; i < LShip.Count; i++)
                {
                    LShip[i].dy *= -1;

                }
            }
          


            if (ctTick3 % 30 == 0)
            {
                for (int i = 0; i < LEnemy1.Count; i++)
                {
                    LEnemy1[i].dxE *= -1;
                }
            }
            if (LEnemy1.Count >= 0)
            {
                MoveEnemy1();
            }
           

            if (right && flagLL == 0)
            {
                LHero[0].x += 15;
                LHero[0].iframe++;

                if (LHero[0].iframe >= 8)
                {
                    LHero[0].iframe = 0;
                }
                if (LHero[0].x >= this.Width/2)
                {
                    if (LBackGround[0].rcSrc.X + this.Width <= LBackGround[0].img.Width)
                    {
                        LBackGround[0].rcSrc.X += 1 * 15;
                    }
                }
            }

            if (left && flagLL == 0) 
            {
                LHero[0].x -= 15;
                LHero[0].iframe++;
                if (LHero[0].iframe >= 8)
                {
                    LHero[0].iframe = 0;
                }
                LBackGround[0].rcSrc.X -= 1 * 15;
                if (LBackGround[0].rcSrc.X < 0)
                {
                    LBackGround[0].rcSrc.X = 0;
                }
            }
            if (s || d || z)
            {
                LHero[0].iframe++;
                if (LHero[0].iframe >= 23)
                {
                    s = false;
                    LHero[0].iframe = 18;
                }
            }
            if (right == left == s == false) 
            {
                LHero[0].iframe++;
                if (LHero[0].iframe >= 18)
                {
                    LHero[0].iframe = 8;
                }
            }

           
            //singl bullet
            for (int i = 0; i < LBullet.Count; i++) 
            {
                if (LBullet[i].ctTick % 30 == 0)
                {
                    LBullet.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < LBullet.Count; i++)
            {
                LBullet[i].ctTick++;
            }

            // dublle bullet
            for (int i = 0; i < LBulletD.Count; i++)
            {
                if (LBulletD[i].ctTick % 30 == 0)
                {
                    LBulletD.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < LBulletD.Count; i++)
            {
                LBulletD[i].ctTick++;
            }


            if (flagCreateE1 ==1) 
            {
                CreateEnemy1Bullet();
                flagCreateE1 = 0;
            }
            for (int i = 0; i < LEnemy1Bullet.Count; i++)
            {
                if (LEnemy1Bullet[i].ctTick % 30 == 0)
                {
                    LEnemy1Bullet.RemoveAt(i);
                    flagCreateE1 = 1;
                    break;
                }
            }
            for (int i = 0; i < LEnemy1Bullet.Count; i++)
            {
                LEnemy1Bullet[i].ctTick++;
            }




            MoveElvator();
            MoveBullet();

            IsHitFire();
            IsHitEnemyBullet();
            ISHitLaser();
            IsHitShip();
            IsHitCoin();
            IsHitMedicen();
            IsHitKey();

            if (LEnemy1.Count >= 0)
            {
                IsHitBullet();
            }

            Movefire();
            MoveFlash();
            MoveShip();
            MoveCoin();
            MoveMedicen();
            ctTick++;
            ctTick2++;
            ctTick3++;
            ctTick4++;

            if (LHero[0].x > LLader[0].x + 380 && space == false)
            {
                CheckGravity();
            }

            DrawDubb(this.CreateGraphics());
            LLazer.Clear();
        }
        void ISHitLaser() 
        {
            for (int i = 0; i < LLazer.Count; i++)
            {
                for (int k = 0; k < LShip.Count; k++)
                {
                    if (LLazer[i].y + LLazer[i].h >= LShip[k].y &&
                       LLazer[i].y + LLazer[i].h <= LShip[k].y + LShip[k].img.Height)

                    {
                        LShip.RemoveAt(k);
                        ctKills++;
                        break;
                    }
                }
            }

        }
        void IsHitBullet()
        {
            for (int i = 0; i < LBullet.Count; i++)
            {
                for (int k= 0; k < LEnemy1.Count; k++) 
                {
                    if (LBullet[i].x >= LEnemy1[k].x &&
                       LBullet[i].x <= LEnemy1[k].x + LEnemy1[0].imgs[k].Width &&
                       LBullet[i].y >= LEnemy1[k].y &&                
                       LBullet[i].y <= LEnemy1[k].y + LEnemy1[0].imgs[k].Height)
                    {
                        LEnemy1.RemoveAt(0);
                        if (LEnemy1Bullet.Count >= 0) 
                        {
                            LEnemy1Bullet.Clear();
                        }
                        LBullet.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        void IsHitFire()
        {
            for (int i = 0; i < LFire.Count; i++)
            {
                if (LHero[0].x >= LFire[i].x &&
                    LHero[0].x <= LFire[i].x + LFire[i].img.Width 
                    
                    &&
                    LHero[0].y >= this.Height - 235 
                    )
                {
                    if (LHeart.Count >= 1)
                    {
                        LHeart.RemoveAt(LHeart.Count - 1);
                    }
                    if (LHeart.Count == 0)
                    {
                        CreateGameOver();
                        tt.Stop();
                    }
                    break;
                }
            }
        }
        void IsHitEnemyBullet()
        {
            for (int i = 0; i < LEnemy1Bullet.Count; i++)
            {
                if (LEnemy1Bullet[i].x >= LHero[0].x &&
                    LEnemy1Bullet[i].x <= LHero[0].x + LHero[0].imgs[0].Width && LHero[0].y >= LEnemy1Bullet[i].y)
                {
                    if (LHeart.Count >= 1)
                    {
                        LHeart.RemoveAt(LHeart.Count - 1);
                    }
                    if (LHeart.Count == 0)
                    {
                        CreateGameOver();
                        tt.Stop();
                    }
                    break;
                }
            }
        }
        void IsHitShip() 
        {
            for (int i = 0; i < LShip.Count; i++)
            {
                if (LShip[i].x >= LHero[0].x &&
                    LShip[i].x <= LHero[0].x + LHero[0].imgs[0].Width && LHero[0].y >= LShip[i].y)
                {
                    if (LHeart.Count >= 1)
                    {
                        LHeart.RemoveAt(LHeart.Count - 1);
                    }
                    if (LHeart.Count == 0)
                    {
                        CreateGameOver();
                        tt.Stop();
                    }
                    break;
                }
            }
        }
        void IsHitCoin()
        {
            for (int i = 0; i < LCoin.Count; i++)
            {
                if (LHero[0].x >= LCoin[i].x && LHero[0].x <= LCoin[i].x + LCoin[i].img.Width &&
               LHero[0].y <= this.Height - 305)
                {
                    LCoin.RemoveAt(i);
                    if (LHeart.Count > 0)
                    {
                        ctCoin += 10;
                    }
                }
            }
           
        }
        void IsHitMedicen()
        {
            for (int i = 0; i < LMedicen.Count; i++)
            {
                if (LHero[0].x >= LMedicen[i].x && LHero[0].x <= LMedicen[i].x + LMedicen[i].img.Width &&
               LHero[0].y <= 200)
                {
                    LMedicen.RemoveAt(i);
                    if (LHeart.Count > 0)
                    {
                        int x = LHeart[LHeart.Count - 1].rcDst.X + 15;
                        for (int k = 0; k < 3; k++)
                        {
                            CAdvActor pnn = new CAdvActor();
                            pnn.img = new Bitmap("heart.png");
                            pnn.rcDst.X = x;
                            pnn.rcDst.Y = LHeart[0].rcDst.Y;
                            pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
                            pnn.rcDst = new Rectangle(pnn.rcDst.X, pnn.rcDst.Y, 15, 15);
                            LHeart.Add(pnn);
                            x += 15;
                        }
                    }
                }
            }
        }
        void IsHitKey()
        {
            for (int i = 0; i < LKey.Count; i++)
            {
                if (LHero[0].x >= LKey[i].x && LHero[0].x <= LKey[i].x + LKey[i].img.Width)
                {
                    LKey.RemoveAt(i);
                    tt.Stop();
                    //CActor pnn = new CActor();
                    //pnn.x = LKey[0].x;
                    //pnn.y = this.Height - 235;
                    //Bitmap img = new Bitmap("sport-car.png");
                    //pnn.imgs.Add(img);
                    //LHero.Add(pnn);
                    //LHero[0].iframe = 24;
                }
            }
        }
        void Movefire()
        {
            for (int i = 0; i < LFire.Count; i++)
            {
                LFire[i].x += LFire[i].dx ;
            }
        }
        void MoveFlash()
        {
            for (int i = 0; i < LFart.Count; i++)
            {
                LFart[i].x += LFart[i].dx *2;
            }
        }
        void MoveShip()
        {
            for (int i = 0; i < LShip.Count; i++)
            {
                LShip[i].y += LShip[i].dy * 5;
                LShip[i].x--;
            }
        }
        void MoveCoin()
        {
            for (int i = 0; i < LCoin.Count; i++)
            {
                LCoin[i].x += LCoin[i].dx;
            }
        }
        void MoveMedicen()
        {
            for (int i = 0; i < LMedicen.Count; i++)
            {
                LMedicen[i].x += LMedicen[i].dx;
            }
        }
        void MoveEnemy1() 
        {
            if (LEnemy1.Count >=0 )
            {
                for (int i = 0; i < LEnemy1.Count; i++)
                {
                    LEnemy1[i].x += LEnemy1[i].dxE * 7;

                    if (LEnemy1[i].dxE == 1)
                    {
                        LEnemy1[i].iframe = 0;
                        flagIsBulletD = 0;
                    }

                    if (LEnemy1[i].dxE == -1)
                    {
                        LEnemy1[i].iframe = 1;
                        flagIsBulletD = 1;

                    }
                }
            }
           
        }
        //any bullet moved here
        void MoveBullet() 
        {
            for (int i = 0; i < LBullet.Count; i++)
            {
                LBullet[i].x += 30;
                LBullet[i].iframe++;
                if (LBullet[i].iframe >= 4)
                {
                    LBullet[i].iframe = 0;
                }
            }
            for (int i = 0; i < LBulletD.Count; i++)
            {
                LBulletD[i].x += 30;
                LBulletD[i].iframe++;
                if (LBulletD[i].iframe >= 4)
                {
                    LBulletD[i].iframe = 0;
                }
            }
            for (int i = 0; i < LEnemy1Bullet.Count; i++)
            {
                for (int k = 0; k < LEnemy1.Count; k++)
                {
                    if (LEnemy1[k].dxE == 1)
                    {
                        LEnemy1Bullet[i].x += 20;

                    }
                    if (LEnemy1[k].dxE == -1)
                    {
                        LEnemy1Bullet[i].x -= 20;
                    }
                }
            }
        }
        void MoveElvator()
        {
            if (LELv[0].y <= 250) 
            {
                LELv[0].dy = -1;
            }

            if (LHero[0].x >= LELv[0].x -40 && LHero[0].x <= LELv[0].x + LELv[0].w  - 100 &&
                LHero[0].y < LELv[0].y - 10) 
            {
                LELv[0].y -= 5 * LELv[0].dy;
                LHero[0].y -= 5 * LELv[0].dy;
            }
            if (LELv[0].y >=this.Height - 40)
            {
                LELv[0].dy *= -1;
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            CreateBackGround();
            CreateHero();
            LHero[0].iframe = 8;
            CreateLader();
            CreateTiles();
            CreateElevator();
            CreateBanner();
            CollectImgs();
            CreateCoin();
            CreateFire();
            CreateHeart();
            CreateEnemy1();
            CreateFart();
            CreateShip();
            CreateMedicen();
            CreateKey();
            CreateLine();
            CreateDoor();
        }
        void CreateBackGround()
        {
            CAdvActor pnn = new CAdvActor();
            pnn.img = new Bitmap("big play background.bmp");
            pnn.rcSrc = new Rectangle(0, 0, this.Width, this.Height);
            pnn.rcDst = new Rectangle(pnn.rcDst.X, pnn.rcDst.Y, this.Width, this.Height);
            LBackGround.Add(pnn);
        }
        void CreateHero()
        {
            CActor pnn = new CActor();
            pnn.x = 100;
            pnn.y = this.Height - 235;

            for (int i = 1; i <= 8; i++)
            {
                Bitmap img = new Bitmap("Run " + i + ".png");
      
                pnn.imgs.Add(img);
            }
            for (int i = 1; i <= 10; i++)
            {
                Bitmap img = new Bitmap("Idle (" + i + ").png");

                pnn.imgs.Add(img);
            }
            for (int i = 1; i <= 5; i++)
            {
                Bitmap img = new Bitmap("Shoot (" + i + ").png");

                pnn.imgs.Add(img);
            }
            //for (int i = 1; i <= 10; i++)
            //{
            //    Bitmap img = new Bitmap("Jump (" + i + ").png");

            //    pnn.imgs.Add(img);
            //}

            LHero.Add(pnn);
        }
        void CreateLader() 
        {
            CNode pnn = new CNode();
            pnn.img = new Bitmap("stair (1).png");
            pnn.x = 500;
            pnn.y = this.Height - 320;

            LLader.Add(pnn);

        }
        void CreateTiles()
        {
            int x = 750;
            for (int i = 0; i < 5; i++)
            {
                CNode pnn = new CNode();
                pnn.img = new Bitmap("box2.gif");
                pnn.x = x;
                pnn.y = this.Height - 305;

                LTiles.Add(pnn);
                x += 75;
            }
            x = 750;
            for (int i = 0; i < 5; i++)
            {
                CNode pnn = new CNode();
                pnn.img = new Bitmap("box2.gif");
                pnn.x = x;
                pnn.y = this.Height - 230;

                LTiles.Add(pnn);
                x += 75;
            }
            x = 750;
            for (int i = 0; i < 5; i++)
            {
                CNode pnn = new CNode();
                pnn.img = new Bitmap("box2.gif");
                pnn.x = x;
                pnn.y = this.Height - 155;

                LTiles.Add(pnn);
                x += 75;
            }

            x = 2500;
            for (int i = 0; i < 6; i++)
            {
                CNode pnn = new CNode();
                pnn.img = new Bitmap("box2.gif");
                pnn.x = x;
                pnn.y = 250;

                LTiles.Add(pnn);
                LImgs.Add(pnn);
                x += 75;
            }

        }
        void CreateElevator() 
        {
            CRec pnn = new CRec();
            pnn.x = 2290;
            pnn.y = LBackGround[0].img.Height - 40;
            pnn.w = 200;
            pnn.h = 20;

            LELv.Add(pnn);
        }
        void CreateBanner() 
        {
            CNode pnn = new CNode();
            pnn.img = new Bitmap("billboard1.bmp");
            Color clr = pnn.img.GetPixel(0, 0);
            pnn.img.MakeTransparent(clr);
            pnn.x = 2500;
            pnn.y = LBackGround[0].img.Height - 170;
            LBanner.Add(pnn);
        }
        void CollectImgs() 
        {
            CNode pnn = new CNode();
            
            for (int i = 0; i < 5; i++) 
            {
                pnn.img = new Bitmap("box2.gif");
                pnn.x = LTiles[i].x;
                pnn.y = LTiles[i].y;
                LImgs.Add(pnn);
            }
        }
        void CreateBullet() 
        {
            CActor pnn = new CActor();
            pnn.x = LHero[0].x + LHero[0].imgs[0].Width;
            pnn.y = LHero[0].y + LHero[0].imgs[0].Height / 2;
            for (int i = 0; i <= 4; i++) 
            {
                Bitmap img = new Bitmap("Bullet_" + i + ".png");

                pnn.imgs.Add(img);
            }
            LBullet.Add(pnn);
        }
        void CreateDubleBullet()
        {
            CActor pnn = new CActor();
            pnn.x = LHero[0].x + LHero[0].imgs[0].Width;
            pnn.y = LHero[0].y + LHero[0].imgs[0].Height / 2;
            for (int i = 0; i <= 4; i++)
            {
                Bitmap img = new Bitmap("Bullet_" + i + ".png");

                pnn.imgs.Add(img);
            }
            LBulletD.Add(pnn);           
        }
        void CreateCoin() 
        {
            int x = 790;
            for (int i = 0; i < 3; i++)
            {
                CNode pnn = new CNode();
                pnn.img = new Bitmap("money (3).bmp");
                Color clr = pnn.img.GetPixel(0, 0);
                pnn.img.MakeTransparent(clr);
                pnn.x = x;
                pnn.y = this.Height - 365;
                LCoin.Add(pnn);
                x += 120;
            }
        }
        void CreateMedicen()
        {
            int x = 2580;
            for (int i = 0; i < 2; i++)
            {
                CNode pnn = new CNode();
                pnn.img = new Bitmap("first-aid-kit.bmp");
                Color clr = pnn.img.GetPixel(0, 0);
                pnn.img.MakeTransparent(clr);
                pnn.x = x;
                pnn.y = 190;
                LMedicen.Add(pnn);
                x += 200;
            }
        }
        void CreateFire() 
        {
            int x = 1550;
            for (int i = 0; i < 3; i++)
            {
                CNode pnn = new CNode();
                pnn.img = new Bitmap("fire (1).png");
                Color clr = pnn.img.GetPixel(0, 0);
                pnn.img.MakeTransparent(clr);
                pnn.x = x;
                pnn.y = this.Height - 143;
                LFire.Add(pnn);
                x += 50;
            }
            x -= 100;
            CNode pn = new CNode();
            pn.img = new Bitmap("fire (1).png");
            Color cl = pn.img.GetPixel(0, 0);
            pn.img.MakeTransparent(cl);
            pn.x = x;
            pn.y = this.Height - 190;
            LFire.Add(pn);
        }
        void CreateHeart()
        {
            int xHeart = 85;
            for (int i = 0; i < 20; i++)
            {
                CAdvActor pnn = new CAdvActor();
                pnn.img = new Bitmap("heart.png");
                pnn.rcDst.X = xHeart;
                pnn.rcDst.Y = 6;
                pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
                pnn.rcDst = new Rectangle(pnn.rcDst.X, pnn.rcDst.Y, 15, 15);
                LHeart.Add(pnn);
                xHeart += 15;
            }
        }
        void DrawLaser()
        {
            CRec pnn = new CRec();

            pnn.x = LHero[0].x + 90;
            pnn.y = LHero[0].y + 50;
            pnn.w = 650;
            LLazer.Add(pnn);
        }
        void CreateEnemy1() 
        {
            CActor pnn = new CActor();
            pnn.x = 3300;
            pnn.y = this.Height - 280;
            for (int i = 1; i <= 2; i++)
            {
                Bitmap img = new Bitmap("Walk (" + i + ").png");
                Color clr = img.GetPixel(0, 0);
                img.MakeTransparent(clr);
                pnn.imgs.Add(img);
            }
            LEnemy1.Add(pnn);
        }
        void CreateEnemy1Bullet()
        {
            CNode pnn = new CNode();
            pnn.img = new Bitmap("medicine-ball.png");
            Color clr = pnn.img.GetPixel(0, 0);
            pnn.img.MakeTransparent(clr);
            for (int i = 0;i < LEnemy1.Count;i++) 
            {
                pnn.y = LEnemy1[i].y + 30;

                if (LEnemy1[i].dxE == 1)
                {
                    pnn.x = LEnemy1[i].x + LEnemy1[i].imgs[0].Width + 60;
                }
                if (LEnemy1[i].dxE == -1)
                {
                    pnn.x = LEnemy1[i].x - 60;
                }
                LEnemy1Bullet.Add(pnn);
            }
        }
        void CreateFart() 
        {
            int x = 4000;
            int y= 0;
            for (int i = 1; i <= 6; i++)
            {
                CNode pnn = new CNode();
                pnn.img = new Bitmap("flash" + i + ".png");
                Color clr = pnn.img.GetPixel(0, 0);
                pnn.img.MakeTransparent(clr);
                pnn.x = x;
                pnn.y = y;
                LFart.Add(pnn);
                x += 260;
            }
            x = 4000;
            y = 240;
            for (int i = 1; i <= 6; i++)
            {
                CNode pnn = new CNode();
                pnn.img = new Bitmap("flash" + i + ".png");
                Color clr = pnn.img.GetPixel(0, 0);
                pnn.img.MakeTransparent(clr);
                pnn.x = x;
                pnn.y = y;
                LFart.Add(pnn);
                x += 260;
            }
        }
        void CreateShip() 
        {
            Random RR = new Random();
            int y = 580;
            int x = 5600;
            for (int i = 0; i < 10; i++)
            {
                CNode pnn = new CNode();
                pnn.y = y;
                pnn.x = x;
               
                if (i % 2 == 0 )
                {
                    pnn.dy = -1;
                    pnn.img = new Bitmap("shipPink_manned.png");

                }
                else
                {
                    pnn.dy = 1;
                    pnn.img = new Bitmap("shipGreen_manned.png");

                }
                Color clr = pnn.img.GetPixel(0, 0);
                pnn.img.MakeTransparent(clr);
                LShip.Add(pnn);
                y += 5;
                x += 55;
            }
        }
        void CreateGameOver()
        {
            CNode pnn = new CNode();
            Bitmap img = new Bitmap("game-over (3).bmp");
            Color clr = img.GetPixel(0, 0);
            img.MakeTransparent(clr);
            pnn.img = img;
            pnn.x = 700;
            pnn.y = 500;
            LOver.Add(pnn);
        }
        void CreateKey()
        {
            CNode pnn = new CNode();
            Bitmap img = new Bitmap("key.bmp");
            Color clr = img.GetPixel(0, 0);
            img.MakeTransparent(clr);
            pnn.img = img;
            pnn.x = 5400;
            pnn.y = this.Height - 230;
            LKey.Add(pnn);
        }

        void CreateLine() 
        {
            CRec pnn = new CRec();

            pnn.x =  5600;
            pnn.y =  13;
            pnn.h = 740;
            LVertical.Add(pnn);

        }
        void CreateDoor()
        {
            CNode pnn = new CNode();
            Bitmap img = new Bitmap("door-open.jpg");
            Color clr = img.GetPixel(0, 0);
            img.MakeTransparent(clr);
            pnn.img = img;
            pnn.x = 5550;
            pnn.y = this.Height - 260;
            LDoor.Add(pnn);
        }
        void DrawScene(Graphics g2)
        {
            g2.Clear(Color.Black);
            for (int i = 0; i < LBackGround.Count; i++)
            {
                g2.DrawImage(LBackGround[i].img, LBackGround[i].rcDst, LBackGround[i].rcSrc, GraphicsUnit.Pixel);
            }
           
            for (int i = 0; i < LLader.Count; i++)
            {
                g2.DrawImage(LLader[i].img , LLader[i].x - LBackGround[0].rcSrc.X, LLader[i].y - LBackGround[0].rcSrc.Y);
            }
            for (int i = 0; i < LTiles.Count; i++)
            {
                g2.DrawImage(LTiles[i].img, LTiles[i].x - LBackGround[0].rcSrc.X, LTiles[i].y - LBackGround[0].rcSrc.Y);
            }

            for (int i = 0; i < LELv.Count; i++)
            {
                Pen Pn = new Pen(Color.White, 10);
                g2.DrawRectangle(Pn, LELv[i].x - LBackGround[0].rcSrc.X, LELv[i].y - LBackGround[0].rcSrc.Y, LELv[0].w, LELv[0].h);

                SolidBrush b = new SolidBrush(Color.Black);
                g2.FillRectangle(b, LELv[i].x - LBackGround[0].rcSrc.X, LELv[i].y - LBackGround[0].rcSrc.Y, 200, 20);

            }
            for (int i = 0; i < LBanner.Count; i++)
            {
                g2.DrawImage(LBanner[i].img, LBanner[i].x - LBackGround[0].rcSrc.X, LBanner[i].y - LBackGround[0].rcSrc.Y);
            }
            for (int i = 0; i < LBullet.Count; i++)
            {
                int k = LBullet[i].iframe;
                g2.DrawImage(LBullet[i].imgs[k], LBullet[i].x - LBackGround[0].rcSrc.X, LBullet[i].y - LBackGround[0].rcSrc.Y);
            }

            for (int i = 0; i < LBulletD.Count; i++)
            {
                int k = LBulletD[i].iframe;
                g2.DrawImage(LBulletD[i].imgs[k], LBulletD[i].x - LBackGround[0].rcSrc.X, LBulletD[i].y - LBackGround[0].rcSrc.Y);
            }

            for (int i = 0; i < LCoin.Count; i++)
            {
                g2.DrawImage(LCoin[i].img, LCoin[i].x - LBackGround[0].rcSrc.X, LCoin[i].y - LBackGround[0].rcSrc.Y);
            }
            for (int i = 0; i < LMedicen.Count; i++)
            {
                g2.DrawImage(LMedicen[i].img, LMedicen[i].x - LBackGround[0].rcSrc.X, LMedicen[i].y - LBackGround[0].rcSrc.Y);
            }
            for (int i = 0; i < LFire.Count; i++)
            {
                g2.DrawImage(LFire[i].img, LFire[i].x - LBackGround[0].rcSrc.X, LFire[i].y - LBackGround[0].rcSrc.Y);
            }
            for (int i = 0; i < LHeart.Count; i++)
            {
                g2.DrawImage(LHeart[i].img, LHeart[i].rcDst, LHeart[i].rcSrc, GraphicsUnit.Pixel);
            }
           
            for (int i = 0; i < LEnemy1.Count; i++)
            {
                int k = LEnemy1[i].iframe;
                g2.DrawImage(LEnemy1[i].imgs[k], LEnemy1[i].x - LBackGround[0].rcSrc.X, LEnemy1[i].y - LBackGround[0].rcSrc.Y);
            }
            for (int i = 0; i < LEnemy1Bullet.Count; i++)
            {
                g2.DrawImage(LEnemy1Bullet[i].img, LEnemy1Bullet[i].x - LBackGround[0].rcSrc.X, LEnemy1Bullet[i].y - LBackGround[0].rcSrc.Y);
            }
            for (int i = 0; i < LFart.Count; i++)
            {
                g2.DrawImage(LFart[i].img, LFart[i].x - LBackGround[0].rcSrc.X, LFart[i].y - LBackGround[0].rcSrc.Y);
            }
            for (int i = 0; i < LShip.Count; i++)
            {
                g2.DrawImage(LShip[i].img, LShip[i].x - LBackGround[0].rcSrc.X, LShip[i].y - LBackGround[0].rcSrc.Y);
            }
            for (int i = 0; i < LKey.Count; i++)
            {
                g2.DrawImage(LKey[i].img, LKey[i].x - LBackGround[0].rcSrc.X, LKey[i].y - LBackGround[0].rcSrc.Y);
            }

            for (int i = 0; i < LHero.Count; i++)
            {
                int k = LHero[i].iframe;
                g2.DrawImage(LHero[i].imgs[k], LHero[i].x - LBackGround[0].rcSrc.X, LHero[i].y - LBackGround[0].rcSrc.Y);
            }
            for (int i = 0; i < LLazer.Count; i++)
            {
                SolidBrush b = new SolidBrush(Color.Yellow);
                g2.FillRectangle(b, LLazer[i].x - LBackGround[0].rcSrc.X, LLazer[i].y - LBackGround[0].rcSrc.Y, LLazer[i].w, 4);
            }








            for (int i = 0; i < LVertical.Count; i++)
            {
                SolidBrush b = new SolidBrush(Color.Black);
                g2.FillRectangle(b, LVertical[i].x - LBackGround[0].rcSrc.X, LVertical[i].y - LBackGround[0].rcSrc.Y, 25, LVertical[i].h);
            }
            for (int i = 0; i < LDoor.Count; i++)
            {
                g2.DrawImage(LDoor[i].img, LDoor[i].x - LBackGround[0].rcSrc.X, LDoor[i].y - LBackGround[0].rcSrc.Y);
            }
            for (int i = 0; i < LOver.Count; i++)
            {
                g2.DrawImage(LOver[i].img, LOver[i].x, LOver[i].y );
            }
            g2.DrawString("HEALTH:", new Font("system", 12), Brushes.White, 11, 3);
            g2.DrawString("KILLS:", new Font("system", 12), Brushes.White, 700, 3);
            g2.DrawString(ctKills +"", new Font("system", 12), Brushes.White, 760, 3);


            g2.DrawString("COIN:", new Font("system", 12), Brushes.White, 1400, 3);
            g2.DrawString(ctCoin + "", new Font("system", 12), Brushes.White, 1455, 3);


        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
