using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Course_work
{
    public partial class Form1 : Form
    {
        private int _gravity = 6;
        private double _gravity_modifier = 1;
        private bool _on_ground = false;
        private bool _on_first_jump = false;

        private List<int[,]> lvl1 = new List<int[,]>();
        private int lvl1Counter = 0;

        private List<GameObject> itemBox = new List<GameObject>();
        private List<GameObject> realItem = new List<GameObject>();
        private List<GameObject> clear = new List<GameObject>();
        private int _lvlCounter = 0;

        private PlatformCreator platformCreator = new PlatformCreator();
        private ObstacleCreator obstacleCreator = new ObstacleCreator();

        private Image playerAnimationRunImg1;
        private Image playerAnimationRunImg2;
        private int playerAnimationFrame = 0;

        private Image groundAnimationRun1;
        private int groundAnimationFrame = 0;
        private Image groundAnimationRun2;

        private Image BGAnimationRun;
        private int BGAnimationFrame = 0;

        private Image BlockImage;
        private Image BlockImage1;
        private Image BlockImage2;
        private Image BlockImage3;
        private Image BlockImage4;
        private Image BlockImage5;
        private Image BlockImage6;
        private Image BlockImage7;
        private Image BlockImage8;
        private Image BlockImage9;

        private Image BlockImage180x120down;
        private Image BlockImage180x120up;

        private Image BlockImage60x120down;
        private Image BlockImage60x120up;

        private Image BlockImage60x180down;
        private Image BlockImage60x180up;

        private Image BlockImage60x240down;
        private Image BlockImage60x240up;

        private Image ObstacleImage60x60down;
        private Image ObstacleImage60x60up;

        private Image ObstacleSprite60x60down;
        private Image ObstacleSprite60x60up;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams CP = base.CreateParams;
                CP.ExStyle = CP.ExStyle | 0x2000000;
                return CP;
            }
        }
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeMap();
        }
        private void _update(object sender, EventArgs e)
        {
            Player.Location = new Point( Player.Location.X, (int) (Player.Location.Y + _gravity * _gravity_modifier));
            Player.Refresh();  
            bool find = false;
            find = _check_ground();

            foreach (GameObject item in itemBox)
            {
                item.ItemMove();
                if (item.CheckDistance(Player))
                {
                    realItem.Add(item);
                }
                if (realItem.Contains(item))
                {

                }
                    if (((Player.Location.X > item.Location.X && Player.Location.X < item.Location.X + item.Width)
                            || (Player.Location.X + Player.Width > item.Location.X && Player.Location.X < item.Location.X + item.Width)))
                    {
                        if (item.GetType().ToString() == "Course_work.Platform")
                        {
                            if (_gravity > 0)
                            {
                                if ((item.Location.Y - Player.Location.Y <= Player.Size.Height + _gravity * _gravity_modifier
                                    && item.Location.Y - Player.Location.Y >= Player.Size.Height - _gravity * _gravity_modifier))
                                {
                                    _gravity_modifier = 0;
                                    Player.Location = new Point(Player.Location.X, (int)(item.Location.Y - Player.Size.Height));
                                    _on_ground = true;
                                    find = true;
                                }
                            }
                            else
                            {
                                if ((Player.Location.Y - item.Location.Y <= item.Size.Height + _gravity * _gravity_modifier * -1
                                    && Player.Location.Y - item.Location.Y >= item.Size.Height - _gravity * _gravity_modifier * -1))
                                {
                                    _gravity_modifier = 0;
                                    Player.Location = new Point(Player.Location.X, (int)(item.Location.Y + item.Size.Height));
                                    _on_ground = true;
                                    find = true;
                                }
                            }
                        }
                        _check_death(item);
                    }
                    if (item.Location.X + item.Size.Width < 0)
                    {
                        clear.Add(item);
                        item.Dispose();
                    }            
            }
            foreach(GameObject item in clear)
            {
                itemBox.Remove(item);
                realItem.Remove(item);
            }
            clear.Clear();
            _check_death_on_ceiling();
            if (!find)
            {
                _on_ground = false;
            }
            if (!_on_ground)
            {
                _gravity_modifier += 0.12;
            }
        }
        private void _jump()
        {
            if (_on_ground)
            {
                _on_first_jump = true;
                _gravity_modifier = -2.5;
                _on_ground = false;
                if (_gravity > 0)
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg1, 0, 0, new Rectangle(new Point(0, 0), new Size(60, 60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                }
                else
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg2, 0, 0, new Rectangle(new Point(0, 0), new Size(60, 60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                }
            }

        }
        private void _changing_the_polarity()
        {
            if (_on_ground || _on_first_jump)
            {
                _gravity *= -1;
                _gravity_modifier *= -1;
                _on_ground = false;
                _on_first_jump = false;
                if (_gravity > 0)
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg1, 0, 0, new Rectangle(new Point(0, 0), new Size(60, 60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                }
                else
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg2, 0, 0, new Rectangle(new Point(0, 0), new Size(60, 60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                }
            }

        }
        private bool _check_ground()
        {
            if (_gravity > 0)
            {
                if (ground.Location.Y - Player.Location.Y  <= Player.Size.Height)
                {
                    _gravity_modifier = 0;
                    Player.Location = new Point (Player.Location.X, (int)(ground.Location.Y - Player.Size.Height));
                    _on_ground = true;
                    return true;
                }
            }
            else
            {
                if ( Player.Location.Y - (ceiling.Location.Y + ceiling.Size.Height) <= 0)
                {
                    _gravity_modifier = 0;
                    Player.Location = new Point(Player.Location.X, (int)(ceiling.Location.Y + ceiling.Size.Height));
                    _on_ground = true;
                    return true;
                }
            }
            return false;

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void OKP(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            switch (e.KeyCode.ToString())
            {
                case "W":
                    _jump();
                    break;
                case "E":
                    _changing_the_polarity();
                    break;
            }
        }
        private void _check_death(GameObject item)
        {
            if ((Player.Location.Y >= item.Location.Y && Player.Location.Y < item.Location.Y + item.Height)
                || (Player.Location.Y + Player.Height > item.Location.Y && Player.Location.Y + Player.Height < item.Location.Y + item.Height))
            {
                _death();
            }
        }
        private void _check_death_on_ceiling()
        {
            if (_gravity > 0)
            {
                if (Player.Location.Y - ceiling.Location.Y < ceiling.Size.Height)
                {
                    _death();
                }
            }
            if (_gravity < 0)
            {
                if (ground.Location.Y < Player.Location.Y + Player.Size.Height)
                {
                    _death();
                }
            }
        }
        private void _death()
        {
            this.Dispose();
            //Application.Restart();
            //MessageBox.Show("e.KeyCode.ToString()");
        }
        private void _updateAnimationRun(object sender, EventArgs e)
        {
            if (_on_ground == true)
            {
                if (_gravity > 0)
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg1, 0, 0, new Rectangle(new Point(60* playerAnimationFrame, 0), new Size(60,60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                    playerAnimationFrame++;
                    if (playerAnimationFrame > 9)
                    {
                        playerAnimationFrame = 0;
                    }
                }
                else
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg2, 0, 0, new Rectangle(new Point(60 * playerAnimationFrame, 0), new Size(60, 60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                    playerAnimationFrame++;
                    if (playerAnimationFrame > 9)
                    {
                        playerAnimationFrame = 0;
                    }
                }
            }
        }
        private void _updateAnimationGround(object sender, EventArgs e)
        {
            Image groundPart = new Bitmap(1300, 50);
            Graphics g = Graphics.FromImage(groundPart);
            g.DrawImage(groundAnimationRun1, 0, 0, new Rectangle(new Point(groundAnimationFrame, 0), new Size(1300, 50)), GraphicsUnit.Pixel);
            ground.Size = new Size(1300, 50);
            ground.Image = groundPart;

            Image ceilingPart = new Bitmap(1300, 50);
            Graphics g2 = Graphics.FromImage(ceilingPart);
            g2.DrawImage(groundAnimationRun2, 0, 0, new Rectangle(new Point(groundAnimationFrame, 0), new Size(1300, 50)), GraphicsUnit.Pixel);
            ceiling.Size = new Size(1300, 50);
            ceiling.Image = ceilingPart;

            groundAnimationFrame += 6;
            if (groundAnimationFrame > 11700)
            {
                groundAnimationFrame = 0;
            }
        }
        private void _updateAnimationBG(object sender, EventArgs e)
        {
            Image groundPart = new Bitmap(1300, 800);
            Graphics g = Graphics.FromImage(groundPart);
            g.DrawImage(BGAnimationRun, 0, 0, new Rectangle(new Point(BGAnimationFrame, 0), new Size(1300, 800)), GraphicsUnit.Pixel);
            BG.Image = groundPart; 

            BGAnimationFrame += 1;

            if (BGAnimationFrame > 1300)
            {
                BGAnimationFrame = 0;
            }

        }
        private void InitializingPartLvl(int[,] lvl)
        {
            int rows = lvl.GetUpperBound(0) + 1;    // количество строк
            int columns = lvl.Length / rows;        // количество столбцов
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    switch (lvl[i, j])
                    {
                        case 1:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage180x120down, new Size(180, 120)));
                            break;
                        case 2:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage180x120up, new Size(180, 120)));
                            break;
                        case 3:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage60x120down, new Size(60, 120)));
                            break;
                        case 4:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage60x120up, new Size(60, 120)));
                            break;
                        case 5:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage60x180down, new Size(60, 180)));
                            break;
                        case 6:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage60x180up, new Size(60, 180)));
                            break;
                        case 7:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage60x240down, new Size(60, 240)));
                            break;
                        case 8:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage60x240up, new Size(60, 240)));
                            break;
                        case 10:
                            itemBox.Add(obstacleCreator.Create(i, j, ObstacleImage60x60down, new Size(60, 60)));
                            break;
                        case 11:
                            itemBox.Add(obstacleCreator.Create(i, j, ObstacleImage60x60up, new Size(60, 60)));
                            break;
                        case 13:
                            itemBox.Add(obstacleCreator.Create(i, j, ObstacleSprite60x60down, new Size(60, 60)));
                            break;
                        case 14:
                            itemBox.Add(obstacleCreator.Create(i, j, ObstacleSprite60x60up, new Size(60, 60)));
                            break;
                        case 21:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage1, new Size(60, 60)));
                            break;
                        case 22:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage2, new Size(60, 60)));
                            break;
                        case 23:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage3, new Size(60, 60)));
                            break;
                        case 24:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage4, new Size(60, 60)));
                            break;
                        case 25:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage5, new Size(60, 60)));
                            break;
                        case 26:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage6, new Size(60, 60)));
                            break;
                        case 27:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage7, new Size(60, 60)));
                            break;
                        case 28:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage2, new Size(60, 60)));
                            break;
                        case 29:
                            itemBox.Add(platformCreator.Create(i, j, BlockImage9, new Size(60, 60)));
                            break;

                    }
                }            
                foreach (GameObject objects in itemBox)
                {
                    this.Controls.Add(objects);
                    objects.BringToFront();
                }
            }
        }
        private void InitializeLvl()
        {
            // 1 - 180x120down; 2 - 180x120up; 3 - 60x120down; 4 - 60x120up;
            // 5 - 60x180down; 6 - 60x180up; 7 - 60x240down; 8 - 60x240up;
            // 10 - magma60x60down; 11 - magma60x60up; 12 - intoMagma60x60down; 13 - intoMagma60x60up;
            int[,] lvl1Part1 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 2, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },  
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 5, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 4, 0, 0, 0, 0, 0, 7, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },  
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 21, 24 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 22, 25 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 22, 25 },
                                        { 0, 0, 0, 0, 0, 0, 1, 0, 25, 25 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 25, 25 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 26, 26 },
                                        };
            InitializingPartLvl(lvl1Part1);
        }
        private void InitializeMap()
        {
            playerAnimationRunImg1 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Player.png");
            playerAnimationRunImg2 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\PlayerUP.png");
            groundAnimationRun1 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Ground.png");
            groundAnimationRun2 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Ground2.png");
            BGAnimationRun = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\BG.png");

            BlockImage = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block.png");
            BlockImage1 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block1.png");
            BlockImage2 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block2.png");
            BlockImage3 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block3.png");
            BlockImage4 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block4.png");
            BlockImage5 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block5.png");
            BlockImage6 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block6.png");
            BlockImage7 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block7.png");
            BlockImage8 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block8.png");
            BlockImage9 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block9.png");

            BlockImage180x120down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block180x120.png");
            BlockImage180x120up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block180x120up.png");
            BlockImage60x120down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x120down.png");
            BlockImage60x120up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x120up.png");
            BlockImage60x180down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x180down.png");
            BlockImage60x180up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x180up.png");
            BlockImage60x240down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x240down.png");
            BlockImage60x240up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x240up.png");
            ObstacleImage60x60down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Magma60x60down.png");
            ObstacleImage60x60up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Magma60x60up.png");
            ObstacleSprite60x60down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\MagmaSprite60x60down.png");
            ObstacleSprite60x60up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\MagmaSprite60x60up.png");

            this.KeyDown += new KeyEventHandler(OKP);

            BG.Location = new Point(0, 0);
            BG.Size = new Size(1300, 700);
            BG.Image = new Bitmap(60, 60);

            BG.Controls.Add(Player);
            Player.BackColor = Color.Transparent;

            ground.Location = new Point(0, 650);
            ground.Size = new Size(1300, 50);

            Player.BringToFront();
            ground.BringToFront();
            ceiling.BringToFront();

            timerBG.Tick += new EventHandler(_update);
            timerBG.Tick += new EventHandler(_updateAnimationRun);
            timerBG.Tick += new EventHandler(_updateAnimationGround);
            timerBG.Tick += new EventHandler(_updateAnimationBG);
            timerBG.Interval = 15;
            timerBG.Start();

            DoubleBuffered = true;

            ceiling.Size = new Size(1300, 50);
            ground.Size = new Size(1300, 50);
            InitializeLvl();
        }
    }
}
