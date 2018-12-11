using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

//TODO
// Add customizable
//      | Radius
//      | Optional Corners
//      | Color selection or random color
//      | Customizable background color

namespace ClickRippler
{
    class ClickRipple
    {
        const int GRID_SIZE = 50;
        const int START_X = 10;
        const int START_Y = 10;
        const int BUTTON_HEIGHT = 20;
        const int BUTTON_WIDTH = 20;
        const int RIPPLE_DELAY = 500;
        
        int rippleRadius;
        int currentColorIndex;  // Consider defaulting to some value?

        Button[] grid;

        Color[] colors = { Color.Red, Color.Pink, Color.Violet, Color.DarkBlue, Color.Blue, Color.LightBlue, Color.Green, Color.LightGreen, Color.Yellow, Color.Orange, Color.DarkOrange, Color.OrangeRed };
        Color originColor;

        Random rand;

        static void Main(string[] args)
        {
            ClickRipple rippler = new ClickRipple();
        }

        public ClickRipple()
        {
            rand = new Random();
            grid = new Button[GRID_SIZE*GRID_SIZE];

            rippleRadius = 5;
            setupForm();
        }

        void setupForm()
        {
            Form mainForm = new Form();
            int counter = 0;
            for(int i = 0; i < GRID_SIZE; i++)
            {
                for(int j = 0; j < GRID_SIZE; j++)
                {
                    Button b = new Button();

                    b.Top = START_X + (i * BUTTON_HEIGHT);
                    b.Left = START_Y + (j * BUTTON_WIDTH);
                    b.Width = BUTTON_WIDTH;
                    b.Height = BUTTON_HEIGHT;
                    b.Click += buttonClick;
                    b.BackColor = Color.Black;  // Consider making this customizable
                    grid[counter] = b;

                    mainForm.Controls.Add(b);

                    counter++;
                }
            }
            mainForm.Size = new Size(BUTTON_WIDTH * GRID_SIZE + 40, BUTTON_HEIGHT * GRID_SIZE + 60);        // 60 = An additional 20 for the menu bar, 20 + 20 = 40 for the margin
            mainForm.ShowDialog();
        }

        async void buttonClick(object sender, EventArgs args)
        {
            originColor = colors[rand.Next(colors.Length)];
            ripple(Array.IndexOf(grid, (Button)sender), originColor);
            await Task.Delay(RIPPLE_DELAY);
        }

        // ring1 from origin | oIndex-GRID_SIZE, oIndex-(GRID_SIZE-1), oIndex-(GRID_SIZE+1), oIndex-1, oIndex+1, oIndex+(GRID_SIZE-1), oIndex+GRID_SIZE, oIndex+(GRID_SIZE+1)
        //
        // if cell != null, paint, delay, revert to default
        async void ripple(int origin, Color originColor)
        {
            for (int i = 0; i <= rippleRadius; i++)
            {
                paintRing(i, origin, colorGradient());
                await Task.Delay(RIPPLE_DELAY);
                paintRing(i, origin, Color.Black);
            }
        }

        void paintRing(int ringDegree, int origin, Color color)
        {
            // corners - TL, TR, BR, BL
            // directionals - up,right,down,left
            // gaps between corner and directionals
            // always 4 corners, 4 directionals, 8 sections of gaps each with a length of (ringDegree - 1 gaps)
            //paintCorners(ringDegree, origin, color);
            paintDirectionals(ringDegree, origin, color);
            paintGaps(ringDegree, origin, color);
        }

        void paintCorners(int ringDegree, int origin, Color color)
        {
            // top corners
            grid[origin - (ringDegree * GRID_SIZE - ringDegree)].BackColor = color;
            grid[origin - (ringDegree * GRID_SIZE + ringDegree)].BackColor = color;

            //bottom corners
            grid[origin + (ringDegree * GRID_SIZE - ringDegree)].BackColor = color;
            grid[origin + (ringDegree * GRID_SIZE + ringDegree)].BackColor = color;
        }

        void paintDirectionals(int ringDegree, int origin, Color color)
        {
            // top
            grid[origin - (ringDegree * GRID_SIZE)].BackColor = color;

            // right
            grid[origin - ringDegree].BackColor = color;

            // bottom
            grid[origin + (ringDegree * GRID_SIZE)].BackColor = color;

            // left
            grid[origin + ringDegree].BackColor = color;
        }

        void paintGaps(int ringDegree, int origin, Color color)
        {
            for(int i = 1; i < ringDegree; i++)
            {
                // TR gap
                grid[origin - (ringDegree * GRID_SIZE + i)].BackColor = color;
                // TL gap
                grid[origin - (ringDegree * GRID_SIZE - i)].BackColor = color;
                // RT gap
                grid[origin - (i * GRID_SIZE - ringDegree)].BackColor = color;
                // RB gap
                grid[origin + (i * GRID_SIZE + ringDegree)].BackColor = color;
                // BR gap
                grid[origin + (ringDegree * GRID_SIZE + i)].BackColor = color;
                // BL gap
                grid[origin + (ringDegree * GRID_SIZE - i)].BackColor = color;
                // LB gap
                grid[origin - (i * GRID_SIZE + ringDegree)].BackColor = color;
                // LT gap
                grid[origin + (i * GRID_SIZE - ringDegree)].BackColor = color;
            }
        }

        Color colorGradient()
        {
            int direction = rand.Next(1);

            if(direction == 1)
            {
                // cycle to 'left' of array
                if(currentColorIndex - 1 < 0)
                {
                    currentColorIndex = colors.Length - 1;
                } else
                {
                    currentColorIndex -= 1;
                }
                originColor = colors[currentColorIndex];

                return originColor;
            } else
            {
                // cycle to 'right' of array
                if (currentColorIndex + 1 >= colors.Length)
                {
                    currentColorIndex = 0;
                }
                else
                {
                    currentColorIndex += 1;
                }
                originColor = colors[currentColorIndex];

                return originColor;
            }
            // pick random from array, choose direction (L or R), iterate through

            // cycle to next color in order, if last color go forwards or backwards

            // SHOULD NEVER RETURN HERE!
            return originColor;
        }
    }
}
