using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using SeachAlgorthims;
namespace snakeGame
{
    class Cout:ISearchAlgorthimAble<Point>,IGreedyAlgorthimAble<Point>
    {
        
        public Point GoalState
        {
            set { food.Location = value; }
            get { return food.Location; }

        }
        public Point StartState
        {
            set { snake.snakeParts[0].Location = new Point(value.X * snake.Increase, value.Y * snake.Increase); }
            get { return new Point(snake.snakeParts[0].Location.X / snake.Increase,
                snake.snakeParts[0].Location.Y / snake.Increase);
            }

        }
        public List<Point> nodeChildern(Point Position)
        {
            List<Point> Childern = new List<Point>();
            int inc = snake.Increase;
            if (Position.X + 1 < Width/snake.Increase && snake.isPartOfSnake(new Point(Position.Y*inc, (Position.X+1)*inc)))
            {
                Childern.Add(new Point(Position.X + 1, Position.Y));
            }
            if (Position.X - 1 >= 0 && snake.isPartOfSnake(new Point(Position.Y*inc, (Position.X-1)*inc)))
            {
                Childern.Add(new Point(Position.X - 1, Position.Y));
            }
            if (Position.Y + 1 < Height && snake.isPartOfSnake(new Point((Position.Y+1) * inc, Position.X * inc)))
            {
                Childern.Add(new Point(Position.X, Position.Y + 1));
            }
            if (Position.Y - 1 >= 0 && snake.isPartOfSnake(new Point((Position.Y-1) * inc, Position.X * inc )))
            {
                Childern.Add(new Point(Position.X, Position.Y - 1));
            }


            return Childern;
        }
        public static int Height = 754 - 13;
        public static int Width = 702 - 13;

        public static  Label food;
        public Random foodPostion;

        public Point getFoodPost()
        {
            Point post = new Point ();
           
                post.X = foodPostion.Next(0 + 13,Width);
                post.Y = foodPostion.Next(0 +13, Height);
                return post;
        }
        Snake snake;
        public Cout(Snake snake)
        {
            this.snake = snake;
            this.foodPostion = new Random();

             food = new Label();
             food.AutoSize = true;
             food.BackColor = System.Drawing.Color.Lime;
             food.ForeColor = System.Drawing.Color.LawnGreen;
             food.Location = new System.Drawing.Point(313, 98);
             food.Name = "food";
             food.Size = new System.Drawing.Size(13, 13);
             food.TabIndex = 0;
             food.Text = "  ";
        }

        public double estimateCostToGoal(Point current)
        {
            return Math.Sqrt(Math.Pow(current.X - this.GoalState.X, 2) + Math.Pow(current.Y - this.GoalState.Y, 2)); 
        }
    }
}
