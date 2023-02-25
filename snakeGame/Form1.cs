using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeachAlgorthims;
namespace snakeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initilaizeSnake();
            
            
        }

        private void initilaizeSnake()
        {
            this.Snake = new Snake();
            for (int i = 0; i < this.Snake.snakeStartLength; i++)
            {
                this.Controls.Add(this.Snake.snakeParts[i]);
            }

            this.Cout = new Cout(this.Snake);
            this.createNewFood() ;
            this.Controls.Add(Cout.food);
            Algo   = new GreadyAlgorthim<Point>(this.Cout);
        }

        private void clearSanke()
        {
            for (int i = 0; i < this.Snake.Length; i++)
            {
                this.Controls.Remove(this.Snake.snakeParts[i]);
                this.Controls.Remove(Cout.food);
            }
        }
        private void createNewFood()
        {
            bool foodOnSnake = false;
            Point foodpost = new Point();

            do
            {
                 foodpost = Cout.getFoodPost();


                 if (foodpost.X % 13 != 0)
                 {
                     foodpost.X -= foodpost.X % 13;
                 }
                if( foodpost.Y % 13 != 0 )
                {
                    foodpost.Y -= foodpost.Y % 13;
                }
                     
                    for (int i = 0; i < Snake.Length; i++)
                     {
                         if (Snake.snakeParts[i].Location.X == foodpost.X || Snake.snakeParts[i].Location.Y == foodpost.Y)
                         {
                             foodOnSnake = true;
                             break;
                         }
                     }
                 
            } while ( !foodOnSnake );

            //this.label1.Text = foodpost.X.ToString() + " " + foodpost.Y.ToString();
            Cout.food.Location = foodpost;


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != this.Snake.lastDir && e.KeyCode != this.Snake.notlastDir)
            {
                Snake.snakeState st = this.Snake.move(e.KeyCode);
                if (st == Snake.snakeState.eatFood)
                {
                    this.createNewFood();
                    this.Controls.Add(this.Snake.increaseLenght());
                    this.timer1.Interval -= 50;
                }
                else if (st == snakeGame.Snake.snakeState.CrossItself)
                {
                    clearSanke();
                    initilaizeSnake();
                    this.timer1.Interval = 500;
                }
            }

        }

        SeachAlgorthims.SearchAlogrthim<Point> Algo;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            Snake.snakeState st = this.Snake.move(this.Snake.lastDir);
            if (st == Snake.snakeState.eatFood)
            {
                this.createNewFood();
                this.Controls.Add(this.Snake.increaseLenght());
                //this.timer1.Interval -= 50;
            }
            else if (st == snakeGame.Snake.snakeState.CrossItself)
            {
                clearSanke();
                initilaizeSnake();
                this.timer1.Interval = 500;
            }

           
            this.timer1.Stop();
            this.timer1.Start();
            /*
             * this.timer1.Enabled = false;
            this.timer1.Enabled = true;
             * 
             */

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.createNewFood();
            
        }
    }
}
