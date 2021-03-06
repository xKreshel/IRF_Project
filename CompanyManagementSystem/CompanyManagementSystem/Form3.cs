﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyManagementSystem
{
    //Credit: Jason FinTech for Drawing -> https://www.youtube.com/watch?v=5Y-Qq3_PWrQ
    public partial class Form3 : Form
    {
        private readonly List<LineList> MyLines = new List<LineList>();
        public Point MouseDownLocation;
        private bool IsMouseDown = false;
        private int m_StartX;
        private int m_StartY;
        private int m_CurX;
        private int m_CurY;
        private string DrawCase = "Line";
        Point Point1 = new Point();
        Point Point2 = new Point();
        Point StartDownLocation = new Point();
        int Count = 0;
        int DistanceUnit = 1;
        Point move;

        public Form3()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;

            m_StartX = e.X;
            m_StartY = e.Y;
            m_CurX = e.X;
            m_CurY = e.Y;
            StartDownLocation = e.Location;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == false) return;
            m_CurX = e.X;
            m_CurY = e.Y;
            switch (DrawCase)
            {
                case "Line":
                    {
                        break;
                    }
                case "CopyLine":
                    {
                        int i;
                        i = MyLines.Count - 1;
                        if (i >= 0)
                        {
                            Point1.X = e.X + MyLines[i].X1 - StartDownLocation.X;
                            Point1.Y = e.Y + MyLines[i].Y1 - StartDownLocation.Y;
                            Point2.X = e.X + MyLines[i].X2 - StartDownLocation.X;
                            Point2.Y = e.Y + MyLines[i].Y2 - StartDownLocation.Y;
                        }
                        break;

                    }
                case "MoveLine":
                    {
                        int i;
                        i = MyLines.Count - 1;
                        if (i >= 0)
                        {
                            Point1.X = e.X + MyLines[i].X1 - StartDownLocation.X;
                            Point1.Y = e.Y + MyLines[i].Y1 - StartDownLocation.Y;
                            Point2.X = e.X + MyLines[i].X2 - StartDownLocation.X;
                            Point2.Y = e.Y + MyLines[i].Y2 - StartDownLocation.Y;
                        }
                        break;

                    }
            }

            pictureBox1.Invalidate();
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;

            if (e.Button == MouseButtons.Left)
            {
                switch (DrawCase)
                {
                    case "Line":
                        {
                            LineList DrawLine = new LineList
                            {
                                X1 = m_StartX,
                                Y1 = m_StartY,
                                X2 = m_CurX,
                                Y2 = m_CurY
                            };
                            MyLines.Add(DrawLine);
                            break;
                        }
                    case "CopyLine":
                        {
                            LineList DrawLine = new LineList
                            {
                                X1 = Point1.X,
                                Y1 = Point1.Y,
                                X2 = Point2.X,
                                Y2 = Point2.Y
                            };
                            MyLines.Add(DrawLine);
                            break;
                        }
                    case "MoveLine":
                        {
                            LineList DrawLine = new LineList
                            {
                                X1 = Point1.X,
                                Y1 = Point1.Y,
                                X2 = Point2.X,
                                Y2 = Point2.Y
                            };
                            MyLines.Add(DrawLine);
                            int count = MyLines.Count - 1;
                            MyLines.RemoveAt(count - 1);
                            break;
                        }
                }
                pictureBox1.Invalidate();
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int i, x1, y1, x2, y2;

            for (i = 0; i <= MyLines.Count - 1; i++)
            {
                Pen LinePen = new Pen(Color.FromArgb(87, 175, 247), 3);
                x1 = MyLines[i].X1;
                x2 = MyLines[i].X2;
                y1 = MyLines[i].Y1;
                y2 = MyLines[i].Y2;
                e.Graphics.DrawLine(LinePen, x1, y1, x2, y2);
            }


            if (IsMouseDown == true)
            {
                switch (DrawCase)
                {
                    case "Line":
                        {
                            Pen dashed_pen = new Pen(Color.Blue, 1);
                            e.Graphics.DrawLine(dashed_pen, m_StartX, m_StartY, m_CurX, m_CurY);
                            break;
                        }
                    case "CopyLine":
                        {
                            Pen dashed_pen = new Pen(Color.Blue, 1);
                            e.Graphics.DrawLine(dashed_pen, Point1.X, Point1.Y, Point2.X, Point2.Y);
                            break;
                        }
                    case "MoveLine":
                        {
                            Pen dashed_pen = new Pen(Color.Blue, 1);
                            e.Graphics.DrawLine(dashed_pen, Point1.X, Point1.Y, Point2.X, Point2.Y);
                            break;
                        }

                }
            }
        }

        private void CopyLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawCase = "CopyLine";
        }

        private void LineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawCase = "Line";
        }

        private void MoveLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawCase = "MoveLine";
        }

        private void CaptureBtn_Click(object sender, EventArgs e)
        {
            SelectArea sa = new SelectArea();
            sa.Show();
        }

        private void AddEmployeeBtn_Click(object sender, EventArgs e)
        {
            Count++;
            for (int i = 0; i < Count; i++)
            {
                TextBox l = new TextBox
                {
                    BackColor = Color.LightBlue,
                    Top = DistanceUnit * 30,
                    Left = 150
                };
                l.MouseDown += L_MouseDown;
                l.MouseMove += L_MouseMove;
                DistanceUnit++;
                l.Parent = pictureBox1;
                Count--;
            }
        }

        private void L_MouseMove(object sender, MouseEventArgs e)
        {
            Control o = (Control)sender;
            if (e.Button == MouseButtons.Left)
            {
                o.Left += e.X - move.X;
                o.Top += e.Y - move.Y;
            }
        }

        private void L_MouseDown(object sender, MouseEventArgs e)
        {
            move = e.Location;
        }
    }
}
