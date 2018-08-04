using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace youxi
{
    public partial class 游戏界面 : Form
    {
        private GamePalette m_GamePalette;      //定义一个游戏画布
        public 游戏界面()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 按键触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
            {
                if(m_GamePalette!=null)     //关闭当前游戏
                {
                    m_GamePalette.Close();
                    m_GamePalette = null;
                }
                TemplateArry array = new TemplateArry();
                array.Add("0000001000011100000000000", Color.FromArgb(-128));
                array.Add("0000000000111100000000000", Color.FromArgb(-65536));
                array.Add("0000000110011000000000000", Color.FromArgb(-16711936));
                array.Add("0000000100011100000000000", Color.FromArgb(-4144960));
                array.Add("0000000100011000100000000", Color.FromArgb(-16776961));
                array.Add("0000000000011100100000000", Color.FromArgb(-65281));
                array.Add("0000000000011000110000000", Color.FromArgb(-8323073));
                //开始新游戏
                m_GamePalette = new GamePalette(13, 20, array, 20, Color.Black, pbMainPalette.CreateGraphics(), pbNextPalette.CreateGraphics(), 0, Color.Red, true);
                m_GamePalette.Start();
            }
            else
            {
                if (m_GamePalette == null || m_GamePalette.IsGameOver)
                    return;
                if (e.KeyCode == Keys.F3)
                {
                    if (m_GamePalette.IsRunning)
                        m_GamePalette.Pause();
                    else
                        m_GamePalette.Resume();
                }
                else if (e.KeyCode == Keys.Left)
                    m_GamePalette.MoveLeft();
                else if (e.KeyCode == Keys.Right)
                    m_GamePalette.MoveRight();
                else if (e.KeyCode == Keys.Down)
                    m_GamePalette.MoveDown();
                else if (e.KeyCode == Keys.Up)
                    m_GamePalette.DeasilRotate();
                else if (e.KeyCode == Keys.Space)
                    m_GamePalette.DropDown();
            }
        }

        private void pbMainPalette_Paint(object sender, PaintEventArgs e)       //主画布
        {
            if (m_GamePalette != null)
                m_GamePalette.PaintPalette(e.Graphics);
        }

        private void pbNextPalette_Paint(object sender, PaintEventArgs e)       //次画布
        {
            if (m_GamePalette != null)
                m_GamePalette.PaintNext(e.Graphics);
        }
    }
}
