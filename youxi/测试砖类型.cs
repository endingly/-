using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using youxi;

namespace youxi
{
    public partial class 测试砖类型 : Form
    {
        BrickFactory m_Factory = null;
        Brick m_Brick = null;

        public 测试砖类型()
        {
            InitializeComponent();
            TemplateArry array = new TemplateArry();
            array.Add("0000001000011100000000000", Color.Blue);
            array.Add("0000000000111100000000000", Color.Green);
            array.Add("0000000110011000000000000", Color.Red);
            array.Add("0000000100011100000000000", Color.DarkCyan);
            array.Add("0000000100011000100000000", Color.OldLace);
            array.Add("0000000000011100100000000", Color.Orange);
            array.Add("0000000000011000110000000", Color.PeachPuff);
            //Point[] points = new Point[] { new Point(0, -1), new Point(1, -1), new Point(0, 0), new Point(0, 1) };
            m_Factory = new BrickFactory(array, Color.Black, 20);
            m_Brick = m_Factory.CreatBrick();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            m_Brick.DeasilRotate();
            pictureBox1.Refresh();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            m_Brick.Y = m_Brick.Y - 1;
            pictureBox1.Refresh();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            bool flag = true;
            List<int> temp = new List<int>();
            for (int i = 0; i < m_Brick.Points.Length; i++)
                temp[i] = m_Brick.Points[i].Y - 1;
            foreach (int i in temp)
                if (i <= 0)
                    flag = false;
            if (flag == true)
                m_Brick.X = m_Brick.X - 1;
            pictureBox1.Refresh();

        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            m_Brick.Paint(g);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            m_Brick = m_Factory.CreatBrick();
            pictureBox1.Refresh();
        }
    }
}
