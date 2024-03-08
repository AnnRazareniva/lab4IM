using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4IM
{
    public partial class Form1 : Form
    {

        int N = 20, M = 35;
        int i, j;

        int[,] matrix;
        MatrixGrid grid;

        public Form1()
        {
            InitializeComponent();

            matrix = new int[M, N];//первое значение количество столбцов второе строк

            grid = new MatrixGrid()
            {
                Parent = kletka,
                Dock = DockStyle.Fill,
                GridSize = new Size(M, N),
                BorderStyle = BorderStyle.FixedSingle,
            };

            grid.CellNeeded += grid_CellNeeded;
            grid.CellClick += grid_CellClick;
        }
        void grid_CellClick(object sender, MatrixGrid.CellClickEventArgs e)
        {
            matrix[e.Cell.X, e.Cell.Y] = 1 - matrix[e.Cell.X, e.Cell.Y];
        }

        void grid_CellNeeded(object sender, MatrixGrid.CellNeededEventArgs e)
        {
            e.BackColor = matrix[e.Cell.X, e.Cell.Y] > 0 ? Color.Blue : Color.White;
        }

        private void button2_Click(object sender, EventArgs e)//кнопка очистки, если что чистка рисунка задерживается(может это только на ноутбуке) нужно ждать
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        Random rand = new Random();

        private void btRandom_Click(object sender, EventArgs e)
        {
            for (i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, 0] = rand.Next(0, 2);//заполнение от 0 до 1 //i-столбец, 0-строка, реализацию брала в интернете, там отдельным классом сделали matrixGrid, у них строки со столбцами перепутаны
            }

            Invalidate(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            grid.Refresh();

            for (int j = 0; j < N; j++)
            {
                for (int i = 0; i < M; i++)
                {
                    
                    Rule(i, j);
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        public void Rule(int ii, int jj)
        {
            int neighbours = Neighbours(ii, jj);

            if (neighbours < 3 && neighbours > 1)
            {
                matrix[ii, jj] = 0;
            }
            else
                if (neighbours == 5 && (jj + 1) < N && matrix[ii, jj + 1] == 1)
            {
                matrix[ii, jj] = 0;
            }
            else
                if (neighbours == 1)
            {
                matrix[ii, jj] = 1;
            }
            else
                if (neighbours > 6)
            {
                matrix[ii, jj] = 0;
            }
            else
                if (neighbours == 7 && (ii - 1) > 0 && (ii + 1) < M && matrix[ii + 1, jj] == 1 && matrix[ii - 1, jj] == 1)
            {
                matrix[ii - 1, jj] = 0;//умирает сосед снизу
            }

        }

        public int Neighbours(int iii, int jjj)//окрестность на 11 соседей
        {
            int neighbours = 0;

            for (int j = jjj - 1; j < jjj + 2; j++)
            {
                for (int i = iii - 1; i < iii + 3; i++)
                {
                    if (i >= 0 && j >= 0 && i < M && j < N && (i != iii || j != jjj))
                    {
                        if (matrix[i, j] > 0)
                            neighbours++;
                    }
                }
            }
            return neighbours;
        }



    }
}
