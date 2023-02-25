using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace snakeGame
{
    class Snake
    {
        public Label[] snakeParts;
        private const int snakeMaxLength = 100;
        public int snakeStartLength = 10;
        public int Length;
        private Point startPoint;
        private Size snakePartSize;
        private Label snakeHeadStyle;
       
        public int startSpeed;




        private int increase;

        public int Increase
        {
            get { return increase; }
            set { increase = value; }
        }
        public Keys lastDir;
        public Keys notlastDir;
        public Snake()
        {
            snakeParts = new Label[snakeMaxLength];
            snakePartSize = new Size(13, 13);
            startPoint = new Point(snakeStartLength * snakePartSize.Width, 0);
            lastDir = Keys.Right;
            notlastDir = Keys.Left;
            Length = snakeStartLength;
            startSpeed = 500;

            increase = snakePartSize.Width;
            this.snakeHeadStyle = new Label();
            this.snakeHeadStyle.AutoSize = true;
            this.snakeHeadStyle.BackColor = System.Drawing.Color.White;
            this.snakeHeadStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.snakeHeadStyle.Location = startPoint; //new System.Drawing.Point(75, 202);
            this.snakeHeadStyle.Name = "snakeHeadStyle";
            this.snakeHeadStyle.Size = snakePartSize; //new System.Drawing.Size(13, 13);
            this.snakeHeadStyle.TabIndex = 3;
            this.snakeHeadStyle.Text = "  ";



            intilazeSnakeParts();
        }

        private void intilazeSnakeParts()
        {
            this.snakeParts[0] = snakeHeadStyle;

            for (int i = 1; i < snakeStartLength; i++)
            {
                this.snakeParts[i] = new Label();
                this.snakeParts[i].AutoSize = true;
                this.snakeParts[i].BackColor = System.Drawing.Color.Red;
                this.snakeParts[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                //this.snakeParts[i].Location = new System.Drawing.Point(75, 228);
                //this.snakeParts[i].Name = "snakeParts[i]";
                this.snakeParts[i].Size = snakePartSize;
                this.snakeParts[i].TabIndex = 2;
                this.snakeParts[i].Text = "  ";

                this.snakeParts[i].Location = new Point(snakeParts[i - 1].Location.X - snakePartSize.Width, snakeParts[0].Location.Y);
            }


        }

         public Label increaseLenght()
        {
            if (this.Length <= 100 )
            {

               this.snakeParts[  this.Length ] = new Label();
               this.snakeParts[this.Length ].AutoSize = true;
               this.snakeParts[this.Length].BackColor = System.Drawing.Color.Red;
               this.snakeParts[this.Length].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
               //this.snakeParts[i].Location = new System.Drawing.Point(75, 228);
               this.snakeParts[this.Length].Name = "snakeParts[i]";
               this.snakeParts[this.Length].Size = snakePartSize;
               this.snakeParts[this.Length].TabIndex = 2;
               this.snakeParts[this.Length].Text = "  ";

               this.snakeParts[this.Length].Location = new Point(snakeParts[this.Length - 1].Location.X - snakePartSize.Width, snakeParts[this.Length -1 ].Location.Y);
               
                return snakeParts[ this.Length++ ];


                
            }
             
            return new Label();
        }


        public  enum snakeState { eatFood, CrossItself, Normal };

        public snakeState move(Keys k)
        {
            for (int i = Length -1; i > 0; i--)
            {
                snakeParts[i].Location = new Point(snakeParts[i - 1].Location.X, snakeParts[i - 1].Location.Y);
            }

            switch (k)
            {
                case Keys.Up:

                    if (snakeParts[0].Location.Y == 0)
                    {
                        snakeParts[0].Location = new Point(snakeParts[0].Location.X, Cout.Height );
                    }
                    else
                    {
                        snakeParts[0].Location = new Point(snakeParts[0].Location.X, snakeParts[0].Location.Y - increase);
                    }

                   
                        

                    lastDir = Keys.Up;
                    notlastDir = Keys.Down;
                    break;

                case Keys.Down:
                    if (snakeParts[0].Location.Y + snakePartSize.Height == Cout.Height)
                    {
                        snakeParts[0].Location = new Point(snakeParts[0].Location.X , snakePartSize.Height);
                    }
                    else
                    {
                    snakeParts[0].Location = new Point(snakeParts[0].Location.X, snakeParts[0].Location.Y + increase);
                    }
                    lastDir = Keys.Down;
                    notlastDir = Keys.Up;
                    break;

                case Keys.Right:
                    if (snakeParts[0].Location.X + snakePartSize.Width == Cout.Width)
                    {
                        snakeParts[0].Location = new Point( snakePartSize.Width , snakeParts[0].Location.Y);
                    }
                    else
                    {
                        snakeParts[0].Location = new Point(snakeParts[0].Location.X + increase, snakeParts[0].Location.Y);
                    }
                    lastDir = Keys.Right;
                    notlastDir = Keys.Left;
                    break;

                case Keys.Left:
                    if (snakeParts[0].Location.X == 0)
                    {
                        snakeParts[0].Location = new Point(Cout.Width - snakePartSize.Width, snakeParts[0].Location.Y);
                    }
                    else
                    {
                        snakeParts[0].Location = new Point(snakeParts[0].Location.X - increase, snakeParts[0].Location.Y);
                    }
                    lastDir = Keys.Left;
                    notlastDir = Keys.Right;
                    break;
            }

            snakeState snkst ;
            if (snakeParts[0].Location.Y == Cout.food.Location.Y && snakeParts[0].Location.X == Cout.food.Location.X) //(snakeParts[0].Location.Y >= Cout.food.Location.Y && snakeParts[0].Location.Y <= Cout.food.Location.Y + Cout.food.Height) && (snakeParts[0].Location.X >= Cout.food.Location.X && snakeParts[0].Location.X <= snakeParts[0].Location.X + Cout.food.Width);
            {
                snkst = snakeState.eatFood;
            }
            else if (snakeCrossItself())
            {
                snkst = snakeState.CrossItself;
            }
            else
            {
                snkst = snakeState.Normal;
            }
                
                return snkst;
            //if( eatFood == true)
                
        }

        bool snakeCrossItself()
        {
            for (int i = 2; i < Length; i++)
            {
                if (snakeParts[0].Location.X == snakeParts[i].Location.X && snakeParts[0].Location.Y == snakeParts[i].Location.Y)
                    return true;
            }
            return false;
        }
        public bool isPartOfSnake(Point point)
        {
            for (int i = 0; i < Length; i++)
            {
                if (point.X == snakeParts[i].Location.X && point.Y == snakeParts[i].Location.Y)
                    return true;
            }
            return false;
        }
    }

}
