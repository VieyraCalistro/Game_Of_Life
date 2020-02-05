using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Game_of_Life
{
    public partial class Form1 : Form
    {
        // Set the universe max height.
        static int maxHeight = Properties.Settings.Default.MaxHeight;


        // Set the universe max width.
        static int maxWidth = Properties.Settings.Default.MaxWidth;


        // The universe array
        bool[,] universe = new bool[maxWidth, maxHeight];


        // The scratchpad array
        bool[,] scratchPad = new bool[maxWidth, maxHeight];


        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;


        // The Timer class
        Timer timer = new Timer();


        // Seed for the random universe.
        int seed;


        // Generation count
        int generations = 0;


        // Create a color for the Hud display.
        Color HudColor = Color.Black;
        


        public Form1()
        {
            InitializeComponent();

            // Setup the timer.
            timer.Interval = Properties.Settings.Default.TimerInterval; // 100 milliseconds
            timer.Tick += Timer_Tick;

            // Upload last saved few settings.
            graphicsPanel1.BackColor = Properties.Settings.Default.PanelColorInSettings;

            cellColor = Properties.Settings.Default.CellColorInSettings;

            gridColor = Properties.Settings.Default.GridColorInSettings;

            HudColor = Properties.Settings.Default.HudDisplayColor;

            // Check for correct Hud display in the Client area.
             if(Properties.Settings.Default.ToroidalOrFinite == "Finite")
             {
                toroidalToolStripMenuItem.Checked = false;
                finiteToolStripMenuItem.Checked = true;
             }

            // Update status strip.
            BottomStripDisplay();


            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();

        }

        private int CountingToroidalNeighbors(int xAxis, int yAxis)
        {
            // Set livingNeighbors to start at 0.
            int livingNeighbors = 0;

            // Loop through around given cell and count only the living neighbors.
            for (var x = -1; x < 2; x++)
            {
                for (var y = -1; y < 2; y++)
                {
                    // Create integers for re-setting cordniates to opposite sides.
                    int combineX = xAxis + x;
                    int combineY = yAxis + y;


                    // Re-set cordinates back to opposite sides if it goes out of bounds.
                    if(combineX > universe.GetLength(0) - 1)
                    {
                        combineX = 0;
                    }
                    else if(combineX < 0)
                    {
                        combineX = universe.GetLength(0) - 1;
                    }
                    if(combineY > universe.GetLength(1) -1)
                    {
                        combineY = 0;
                    }
                    else if(combineY < 0)
                    {
                        combineY = universe.GetLength(1) - 1;
                    }

                    if (xAxis + x >= 0 && xAxis + x < universe.GetLength(0) && yAxis + y >= 0 && yAxis + y < universe.GetLength(1))
                    {
                        if (x == 0 && y == 0)
                        {
                            // Do nothing, avoid counting yuorself.
                            continue;
                        }
                        else
                        {
                            // If universe[xAxis + x, yAxis + y] == true, then store the count.
                            if (universe[xAxis + x, yAxis + y] == true)
                            {
                                livingNeighbors++;
                                continue;
                            }

                        }
                    }

                    if (x == 0 && y == 0)
                    {
                        // Do nothing, avoid counting yuorself.
                        continue;
                    }
                    else
                    {
                        // If universe[xAxis + x, yAxis + y] == true, then store the count.
                        if (universe[combineX, combineY] == true)
                        {
                            livingNeighbors++;
                            continue;
                        }

                    }

                }
            }

            return livingNeighbors;
        }

        private int CountingNeighbors(int xAxis, int yAxis)
        {
            // Set livingNeighbors to start at 0.
            int livingNeighbors = 0;

            // Loop through around given cell and count only the living neighbors.
            for (var x = -1; x < 2; x++)
            {
                for (var y = -1; y < 2; y++)
                {
                    if (xAxis + x >= 0 && xAxis + x < universe.GetLength(0) && yAxis + y >= 0 && yAxis + y < universe.GetLength(1))
                    {
                        if (x == 0 && y == 0)
                        {
                            // Do nothing, avoid counting yuorself.
                        }
                        else
                        {
                            // If universe[xAxis + x, yAxis + y] == true, then store the count.
                            if (universe[xAxis + x, yAxis + y] == true)
                            {
                                livingNeighbors++;
                            }

                        }
                    }

                }
            }

            return livingNeighbors;
        }


        // Calculate the next generation of cells
        private void NextGeneration()
        {
            // Create a variable to keep track of all the living neighbors.
            int livingNeighbors = 0;


            // Loop through the universe and check the state of each cell.
            for (int yAxis = 0; yAxis < universe.GetLength(1); yAxis++)
            {
                for (int xAxis = 0; xAxis < universe.GetLength(0); xAxis++)
                {

                    // Check to see if it is toroidal or not, then apply the correct neighbor count.
                    if(toroidalToolStripMenuItem.Checked == true)
                    {
                        // Grab the living neighbors from created method.
                        livingNeighbors = CountingToroidalNeighbors(xAxis, yAxis);
                    }
                    else
                    {
                        // Grab the living neighbors from created method.
                        livingNeighbors = CountingNeighbors(xAxis, yAxis);
                    }
                    

                    // Set up your rules upon living neighbors and the state of each cell.
                    if (universe[xAxis, yAxis] == true)
                    {
                        if (livingNeighbors < 2)
                        {
                            // Rule number 1.
                            scratchPad[xAxis, yAxis] = false;
                        }
                        else if (livingNeighbors > 3)
                        {
                            // Rule number 2.
                            scratchPad[xAxis, yAxis] = false;
                        }
                        else
                        {
                            // Rule number 3.
                            scratchPad[xAxis, yAxis] = true;
                        }
                    }
                    else
                    {
                        // Rule number 4.
                        if (livingNeighbors == 3)
                        {
                            scratchPad[xAxis, yAxis] = true;
                        }
                        else
                        {
                            scratchPad[xAxis, yAxis] = false;
                        }
                    }
                }
            }


            // Gather the cells that are alive through a created method.
            Properties.Settings.Default.AliveCells = CountLivingCells();


            //Swap the universe with the scratchPad.
            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;


            // Increment generation count
            generations++;


            // Update status strip.
            BottomStripDisplay();


            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }


        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }


        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // A brush for The hud display.
            Brush HudBrush = new SolidBrush(HudColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;


                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }


                    // Check to see if grid on check box is checked.
                    // If so, draw the grid on the screen.
                    if(gridToolStripMenuItem.Checked == true)
                    {
                        // Outline the cell with a pen
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    }
                    

                    // Check to see if the number neighbors count check box is checked.
                    // If so, print out the neighbors in each cell.
                    if(neighborCountToolStripMenuItem.Checked == true)
                    {
                        Font font = new Font("Arial", 10f);

                        StringFormat stringFormat = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };

                        int neighbors = CountingNeighbors(x, y);


                        // Only display neighbor count if it greater than 0.
                        if(neighbors > 0)
                        {
                            e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Black, cellRect, stringFormat);
                        }

                    }

                }
            }


            // Hud display font of choice.
            Font font2 = new Font("Arail", 20f);


            // Align Hud display.
            StringFormat stringFormat2 = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Far
            };


            // Check to see if Hud display is enabled.
            if (hudToolStripMenuItem.Checked == true)
            {

                HudInClientArea.Text = "Generations = " + generations.ToString() + "\n" + "Interval = " + Properties.Settings.Default.TimerInterval + "\n" + "Alive Cells = " + CountLivingCells() + "\n" + "Universe Size: (Width " + Properties.Settings.Default.MaxWidth + " Height " + Properties.Settings.Default.MaxHeight + ")" + "\n" + "Boundry Type: " + BoundryTypeCheck();
                e.Graphics.DrawString(HudInClientArea.Text, font2, HudBrush, graphicsPanel1.ClientRectangle, stringFormat2);

            }


            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];


                // Increment the number of alive cells.
                Properties.Settings.Default.AliveCells = CountLivingCells();
                
                
                // Update status strip.
                BottomStripDisplay();


                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close out the form.
            Close();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer.Enabled = true; // start timer running
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer.Enabled = false; // Pause timer running.
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // Increment the next generation by one.
            NextGeneration();
        }


        // New button.
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            // Pause the ticker/game.
            timer.Enabled = false;


            // Turn off all cells for a re-set.
            for (var indexY = 0; indexY < universe.GetLength(1); indexY++)
            {
                for (var indexX = 0; indexX < universe.GetLength(0); indexX++)
                {
                    universe[indexX, indexY] = false;
                    scratchPad[indexX, indexY] = false;
                }
            }


            // Re-set generation count.
            generations = 0;


            // Re-set alive cell count.
            Properties.Settings.Default.AliveCells = 0;


            // Update status strips.
            BottomStripDisplay();


            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        // Added feature that is turned off right now.
        // Feature is to hold down the mouse button and draw cells without clicking everytime.
        private void graphicsPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);


                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;


                // Toggle the cell's state
                if (x < universe.GetLength(0) && x >= 0 && y < universe.GetLength(1) && y >= 0)
                {
                    universe[x, y] = true;
                }


                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }


        // Get a living cell count.
        private int CountLivingCells()
        {
            int liveCells = 0;


            // Loop through each cell for an accurate count.
            for (int yAxis = 0; yAxis < universe.GetLength(1); yAxis++)
            {
                for (int xAxis = 0; xAxis < universe.GetLength(0); xAxis++)
                { 

                    //If true increase live cell count.
                    if(universe[xAxis, yAxis] == true)
                    {
                        liveCells++;
                    }
                }
            }
                    return liveCells;
        }

        
        // User provides input for the random seed.
        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Pause timer ticks.
            timer.Enabled = false;


            // turn off all cells.
            for (var indexY = 0; indexY < universe.GetLength(1); indexY++)
            {
                for (var indexX = 0; indexX < universe.GetLength(0); indexX++)
                {
                    universe[indexX, indexY] = false;
                    scratchPad[indexX, indexY] = false;
                }
            }


            // Re-set generation count.
            generations = 0;


            // Update status strip generations.
            BottomStripDisplay();


            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();


            // Create an instance of FromSeedDialog.
            FromSeedDialog seedDialog = new FromSeedDialog();

            seedDialog.FromSeedDialogRandomize = Properties.Settings.Default.Seed;

            // Show dialog.
            if (DialogResult.OK == seedDialog.ShowDialog())
            {

                // Get chosen seed info and apply.
                Properties.Settings.Default.Seed = (int)seedDialog.FromSeedDialogRandomize;


                seedDialog.FromSeedDialogRandomize = Properties.Settings.Default.Seed;


                seed = Properties.Settings.Default.Seed;


                Random random = new Random(seed);


                // Loop through and turn corresponding cells on.
                for(int y = 0; y < universe.GetLength(1); y++)
                {
                    for(int x =0; x < universe.GetLength(0); x++)
                    {
                        if(random.Next(0, 2) ==  0)
                        {
                            universe[x, y] = true;
                        }
                    }
                }


                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
                
            }
            
        }


        // Create a file of universe to save.
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open up Stream Writer.
            StreamWriter myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                DefaultExt = "cells"
            };


            // Display dialog.
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myStream = new StreamWriter(saveFileDialog1.FileName);
                myStream.WriteLine("!Your Game of Life file.");


                // Loop through and start writing corresponding text to the file.
                for (var indexY = 0; indexY < universe.GetLength(1); indexY++)
                {
                    string currentRow = string.Empty;

                    for (var indexX = 0; indexX < universe.GetLength(0); indexX++)
                    {
                        if(universe[indexX, indexY] == true)
                        {
                            currentRow += "O";
                            
                        }
                        else
                        {
                            currentRow += ".";
                        }
                    }

                    myStream.WriteLine(currentRow);

                }
                
                myStream.Close();
                
            }
            
        }


        // Create an open file option.
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;


            // Loop through and prepare, turn off all cells.
            for (var indexY = 0; indexY < universe.GetLength(1); indexY++)
            {
                for (var indexX = 0; indexX < universe.GetLength(0); indexX++)
                {
                    universe[indexX, indexY] = false;
                    scratchPad[indexX, indexY] = false;
                }
            }


            // Re-set generation count.
            generations = 0;


            // Update status strips.
            BottomStripDisplay();


            // Re-paint screen.
            graphicsPanel1.Invalidate();


            // Begin opening file.
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "All Files|*.*|Cells|*.cells",
                FilterIndex = 2
            };


            // SHow dialog.
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                StreamReader reader = new StreamReader(openFileDialog.FileName);


                Properties.Settings.Default.MaxWidth = 0;
                Properties.Settings.Default.MaxHeight = 0;


                while (!reader.EndOfStream)
                {

                    string row = reader.ReadLine();

                    // Check to see if the first line is a comment.
                    if(row[0] == '!')
                    {
                        continue;
                    }


                    // If line is not a comment, increase counts for future universe size.
                    Properties.Settings.Default.MaxHeight++;
                    Properties.Settings.Default.MaxWidth = row.Length;
                }


                // Re-seize the univers and scratchPad grid.
                universe = new bool [Properties.Settings.Default.MaxWidth, Properties.Settings.Default.MaxHeight];
                scratchPad = new bool[Properties.Settings.Default.MaxWidth, Properties.Settings.Default.MaxHeight];


                // Set file pointer back to the beginning.
                reader.BaseStream.Seek(0, SeekOrigin.Begin);


                int counter = 0;


                // CHeck to make sure we are not at the end of a stream.
                while(!reader.EndOfStream)
                {
                    string row = reader.ReadLine();


                    // Check to see if line is a comment.
                    if(row[0] == '!')
                    {
                        continue;
                    }


                    // Loop through universe and assign correct state of cell.
                    for (var xPos = 0; xPos < row.Length; xPos++)
                    {
                        if (row[xPos] == 'O')
                        {
                            universe[xPos, counter] = true;
                        }
                        else if (row[xPos] == '.')
                        {
                            universe[xPos, counter] = false;
                        }
                    }

                    // Increment the Y axis counter.
                    counter++;
                }


                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();


                // Close the reader stream.
                reader.Close();
            }
            
        }


        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Create an instance od Options_Dialog.
            Options_Dialog option = new Options_Dialog
            {
                // Set appropriate sizes.
                OptionsTimer = Properties.Settings.Default.TimerInterval,


                OptionsUniverseWidth = Properties.Settings.Default.MaxWidth,


                OptionsUniverseHeight = Properties.Settings.Default.MaxHeight
            };


            // Show dialog box.
            if (DialogResult.OK == option.ShowDialog())
            {

                // Save user inout settings.
                Properties.Settings.Default.TimerInterval = (int)option.OptionsTimer;


                Properties.Settings.Default.MaxWidth = (int)option.OptionsUniverseWidth;
                

                Properties.Settings.Default.MaxHeight = (int)option.OptionsUniverseHeight;


                // Create the new universe  and scratchPad of correct size.
                universe = new bool[Properties.Settings.Default.MaxWidth, Properties.Settings.Default.MaxHeight];
                scratchPad = new bool[Properties.Settings.Default.MaxWidth, Properties.Settings.Default.MaxHeight];


                // Update displays.
                BottomStripDisplay();



                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }


        // Create a bottom strip display method.
        private void BottomStripDisplay()
        {
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString() + "  " + "Interval = " + Properties.Settings.Default.TimerInterval + "  " + "Alive Cells = " + Properties.Settings.Default.AliveCells;

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }


        // Create Hud tool strip mthod and toggle on and off..
        private void hudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hudToolStripMenuItem.Checked == true)
            {
                hudToolStripMenuItem.Checked = false;
                hUDToolStripMenuItem1.Checked = false;
            }
            else
            {
                hudToolStripMenuItem.Checked = true;
                hUDToolStripMenuItem1.Checked = true;
            }


            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();

        }

        // Toggle neighbor count of choice.
        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(neighborCountToolStripMenuItem.Checked == false)
            {
                neighborCountToolStripMenuItem.Checked = true;
                neighborCountToolStripMenuItem1.Checked = true;
            }
            else
            {
                neighborCountToolStripMenuItem.Checked = false;
                neighborCountToolStripMenuItem1.Checked = false;
            }
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        // Toggle bottom strip over time.
        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridToolStripMenuItem.Checked == false)
            {
                gridToolStripMenuItem.Checked = true;
                gridToolStripMenuItem1.Checked = true;
                
            }
            else
            {
                gridToolStripMenuItem.Checked = false;
                gridToolStripMenuItem1.Checked = false;
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }


        // Create access to a back color.
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colordlg = new ColorDialog
            {
                Color = graphicsPanel1.BackColor
            };

            if (DialogResult.OK == colordlg.ShowDialog())
            {
                graphicsPanel1.BackColor = colordlg.Color;
            }
        }


        // Create access to cell colors.
        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colordlg = new ColorDialog
            {
                Color = cellColor
            };

            if (DialogResult.OK == colordlg.ShowDialog())
            {
                cellColor = colordlg.Color;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }


        // Create grid color options.
        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colordlg = new ColorDialog
            {
                Color = gridColor
            };


            if (DialogResult.OK == colordlg.ShowDialog())
            {
                gridColor = colordlg.Color;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }


        // Reset tool properties.
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();

            graphicsPanel1.BackColor = Properties.Settings.Default.PanelColorInSettings;

            cellColor = Properties.Settings.Default.CellColorInSettings;

            gridColor = Properties.Settings.Default.GridColorInSettings;

            timer.Interval = Properties.Settings.Default.TimerInterval;

            universe = new bool[Properties.Settings.Default.MaxWidth, Properties.Settings.Default.MaxHeight];
            scratchPad = new bool[Properties.Settings.Default.MaxWidth, Properties.Settings.Default.MaxHeight];

            HudColor = Properties.Settings.Default.HudDisplayColor;


            // Update Hud display and bottom strip.
            BottomStripDisplay();


            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();

        }


        // CLose the form down the right way, while saving all the previous settings.
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.PanelColorInSettings = graphicsPanel1.BackColor;

            Properties.Settings.Default.CellColorInSettings = cellColor;

            Properties.Settings.Default.GridColorInSettings = gridColor;

            Properties.Settings.Default.Seed = seed;

            Properties.Settings.Default.HudDisplayColor = HudColor;

            Properties.Settings.Default.ToroidalOrFinite = BoundryTypeCheck();

            Properties.Settings.Default.Save();
        }


        // Allow user to have the option of re-load.
        private void reloadLastSavedColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();

            graphicsPanel1.BackColor = Properties.Settings.Default.PanelColorInSettings;

            cellColor = Properties.Settings.Default.CellColorInSettings;

            gridColor = Properties.Settings.Default.GridColorInSettings;

            universe = new bool[Properties.Settings.Default.MaxWidth, Properties.Settings.Default.MaxHeight];
            scratchPad = new bool[Properties.Settings.Default.MaxWidth, Properties.Settings.Default.MaxHeight];

            timer.Interval = Properties.Settings.Default.TimerInterval;

            HudColor = Properties.Settings.Default.HudDisplayColor;


            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();

        }


        // Allow the user to change the Hud color as well.
        private void hudColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            // Create a dialog.
            ColorDialog ColorDlg = new ColorDialog
            {
                Color = HudColor
            };


            // SHow the dialog of the instance.
            if (DialogResult.OK == ColorDlg.ShowDialog())
            {
                HudColor = ColorDlg.Color;


                // Save color for start up.
                Properties.Settings.Default.HudDisplayColor = HudColor;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }


        // Create a method to randomize cell activation;
        private void fromCurrentSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;


            // Loop through universe and turn off cells.
            for (var indexY = 0; indexY < universe.GetLength(1); indexY++)
            {
                for (var indexX = 0; indexX < universe.GetLength(0); indexX++)
                {
                    universe[indexX, indexY] = false;
                    scratchPad[indexX, indexY] = false;
                }
            }


            generations = 0;

            // Update status strip generations
            BottomStripDisplay();
            

            // Get initail seed count.
            seed = Properties.Settings.Default.Seed;


            // Creat a random instance.
            Random random = new Random(seed);


            // Loop through and set all corresponding cells on.
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (random.Next(0, 2) == 0)
                    {
                        universe[x, y] = true;
                    }
                }
            }


            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        // Create a random by time method.
        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;


            // First turn off all cells.
            for (var indexY = 0; indexY < universe.GetLength(1); indexY++)
            {
                for (var indexX = 0; indexX < universe.GetLength(0); indexX++)
                {
                    universe[indexX, indexY] = false;
                    scratchPad[indexX, indexY] = false;
                }
            }


            generations = 0;


            // Update status strip generations
            BottomStripDisplay();


            // Create an instance of FromSeedDialog.
            FromSeedDialog seedDialog = new FromSeedDialog();


            Random random = new Random();
            
            
            seedDialog.FromSeedDialogRandomize = random.Next();


            // Go through universe and assign true based on some additions.
            for (int y = 0; y < universe.GetLength(1); y++)
            {

                for (int x = 0; x < universe.GetLength(0); x++)
                {

                    if (random.Next(0, 2) == 0)
                    {

                        universe[x, y] = true;
                    }
                }
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }


        // Create method to import supported files.
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;


            // Create an instance of OpenFileDialog.
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "All Files|*.*|Cells|*.cells",
                FilterIndex = 2
            };


            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                StreamReader reader = new StreamReader(openFileDialog.FileName);


                ImportFileFail ImportFailed = new ImportFileFail();


                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                int counter = 0;


                // Loop through entire import file as long as an exception is not thrown.
                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine();


                    if (row[0] == '!')
                    {
                        continue;
                    }


                    // If file is too big, view message.
                    if (counter > universe.GetLength(1) - 1)
                    {
                        reader.Close();

                        ImportFailed.ShowDialog();

                        return;

                    }

                    
                    // Loop through and begin file read.
                    // Throw message if file is currently bigger than universe.
                    for (var xPos = 0; xPos < row.Length; xPos++)
                    {
                        if(row.Length > universe.GetLength(0)-1)
                        {
                            reader.Close();

                            ImportFailed.ShowDialog();

                            return;

                        }
                        if (row[xPos] == 'O')
                        {
                            universe[xPos, counter] = true;
                        }
                        else if (row[xPos] == '.')
                        {
                            universe[xPos, counter] = false;
                        }
                    }

                    counter++;
                    
                }


                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();


                // CLose the file.
                reader.Close();
            }
        }


        // Inverse the state of toroidal when checked.
        private void toroidalToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Make sure to swap the state of the finite if you check the toroidal.
            if(toroidalToolStripMenuItem.Checked == true)
            {
                toroidalToolStripMenuItem.Checked = false;
                finiteToolStripMenuItem.Checked = true;
            }
            else
            {
                toroidalToolStripMenuItem.Checked = true;
                finiteToolStripMenuItem.Checked = false;
            }

            // Re-paint on screen.
            graphicsPanel1.Invalidate();
        }


        // Method to check the boundry type.
        private string BoundryTypeCheck()
        {
            if (toroidalToolStripMenuItem.Checked == true)
            {
                return "Torodial";
            }
            else
            {
                return "Finite";
            }
        }
        

        // Inverse the sate of finite when checked.
        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Make sure to swap the state of the toridal if you check the finite.
            if (finiteToolStripMenuItem.Checked == true)
            {
                finiteToolStripMenuItem.Checked = false;
                toroidalToolStripMenuItem.Checked = true;
            }
            else
            {
                finiteToolStripMenuItem.Checked = true;
                toroidalToolStripMenuItem.Checked = false;
            }

            // Re-paint on screen.
            graphicsPanel1.Invalidate();
        }
    }
}
