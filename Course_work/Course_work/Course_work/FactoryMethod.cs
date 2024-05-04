using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Course_work
{
    abstract class GameObjectCreator
    {
        abstract public GameObject Create(int posX, int posY, Image image, Size size);
    }
    class PlatformCreator : GameObjectCreator
    {
        public override GameObject Create(int posX, int posY, Image image, Size size)
        {
            return new Platform(posX, posY, image, size);
        }
    }
    class ObstacleCreator : GameObjectCreator
    {
        public override GameObject Create(int posX, int posY, Image image, Size size)
        {
            return new Obstacle(posX, posY, image, size);
        }
    }
    class InscriptionCreator : GameObjectCreator
    {
        public override GameObject Create(int posX, int posY, Image image, Size size)
        {
            return new Obstacle(posX, posY, image, size);
        }
    }

    abstract class GameObject : PictureBox
    {
        private bool _closely = false;
        public void ItemMove()
        { 
            this.Location = new System.Drawing.Point(this.Location.X - 6, this.Location.Y);
        }
        public bool CheckDistance(PictureBox player)
        {
            if (this.Location.X - player.Location.X < 60 && !_closely)
            {
                _closely = true;
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
    class Platform : GameObject
    {
        static readonly int _Height = 60; 
        static readonly int _Width = 60;
        public Platform(int posX, int posY, Image image, Size size) : base()
        {
            this.Location = new System.Drawing.Point(1300 + posX*60, 50 + 60 * posY);
            this.Size = size;
            this.Image = image;
        }
    }
    class Obstacle : GameObject
    {
        static readonly int _Height = 60;
        static readonly int _Width = 60;
        public Obstacle(int posX, int posY, Image image, Size size) : base()
        {
            this.Location = new System.Drawing.Point(1300 + posX * 60, 50 + 60 * posY);
            this.BackColor = Color.Black;
            this.Size = size;
            this.Image = image;
        }
    }
    class Inscription : GameObject
    {
        static readonly int _Height = 60;
        static readonly int _Width = 60;
        public Inscription(int posX, int posY, Image image, Size size) : base()
        {
            this.Location = new System.Drawing.Point(1300 + posX * 60, 50 + 60 * posY);
            this.BackColor = Color.Black;
            this.Size = size;
            this.Image = image;
        }
    }
    
}
