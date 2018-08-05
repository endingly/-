using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

using youxi;
using System.Collections;
using System.Timers;

namespace youxi
{
    /*
    public class youxiControl : Control       //定义游戏的控制类，设置游戏过程中的返回行数、参数、等级以及积分和方块等操作参数
    {
        private const int rowCount = 21;    //行数
        private const int colCount = 11;    //列数
        private int brickWidth = 16;        //小块的宽度
        private int brickHeigth = 16;       //小块的高度
        private ImageList imageList;        //方块素材
        private Bitmap backBitmap;          //背景图片
        private List<List<List<Point>>> brickTemplets = new List<List<List<Point>>>();      //方块的模板[模板序号，朝向]
        private byte[,] points = new byte[colCount, rowCount];                              //点阵
        private byte brickIndex = 0;        //模板的序号
        private byte facingIndex = 0;       //当前的变化号
        private Point brickPoint = new Point();     //方块的位置
        private byte afterBrickIndex = 0;           //下一个模板序号
        private byte afterFacingIndex = 0;          //下一个变化号
        private System.Windows.Forms.Timer timer;   //控制下落的定时器
        private int lines;                  //消行数
        private Random random = new Random();       //随机数
        private int level = 0;              //当前速度
        private int score = 0;              //成绩
        private int[] speeds = new int[] { 700, 500, 400, 300, 320, 320, 100, 80, 70, 60, 50 };     //下落速度，数值表示每次下落的时间差，以毫秒为单位
        private int[] scoress = new int[] { 0001, 0100, 0300, 0500, 1000, 3200 };           //每次消除行所增加的积分
        private bool palying = false;       //玩家是否正在游戏
        private youxiNext youxiNext;        //下一个方块的显示控件
        private youxiScore youxiScore;      //积分显示控件
        private int stepIndex = -1;         //当前回放的步数
        private bool reviewing = false;     //是否正在回放
        private Thread threadReview = null; //回放使用的线程
        private int reviewSpeed = 1;        //回放的速度，数值表示倍数
        private List<StepInfo> stepInfos = new List<StepInfo>();                            //记录玩家的每一步操作
        private int lastRecordTime = 0;     //最后记录的时间
        private bool recordMode = false;    //是否采用记录模式
        private ProgressBar progressBar;    //回放进度条
        private bool extended = false;      //扩展方块

        public int Lines { get { return lines; } }      //消除的行数
        public int Score { get { return score; } }      //当前积分
        public int Level { get { return level; } }      //当前的关数

        public enum BrickOperates       //方块的操作
        {
            boMoveLeft = 0,       //左移
            boMoveRight = 1,      //右移
            boMoveDown = 2,       //下移
            boMoveBottom = 3,     //直下
            boturnLeft = 4,       //左旋
            boturnRight = 5       //右旋
        }

        public void Repaly(bool ARecordMode,bool AExtended)
        {
            if(threadReview!=null)
            {
                threadReview.Abort();
                threadReview = null;
            }
            if (AExtended != extended)
                NewTemplets(AExtended);
            reviewing = false;
            palying = true;
            recordMode = ARecordMode;
            Clear();
            stepInfos.Clear();
            afterBrickIndex = (byte)random.Next(brickTemplets.Count);
            afterFacingIndex = (byte)random.Next(brickTemplets[afterBrickIndex].Count);
            if (youxiNext != null)
                youxiNext.Update(this);
            if(recordMode&&!reviewing)
            {
                stepInfos.Add(new StepInfo(0, 0, afterBrickIndex, afterFacingIndex));
                lastRecordTime = Environment.TickCount;
            }
            level = 0;
            score = 0;
            lines = 0;
            stepIndex = -1;
            if (progressBar != null)
                progressBar.Value = 0;
            NextBrick();
            timer.Interval = speeds[level];
            timer.Enabled = true;
            if (youxiScore != null)
                youxiScore.Update(this);
            if (CanFocus)
                Focus();
        }

        public void Review()
        {
            if(threadReview!=null)
            {
                threadReview.Abort();
                threadReview = null;
            }
            timer.Enabled = false;
            reviewing = true;
            palying = true;
            Clear();
            level = 0;
            score = 0;
            lines = 0;
            stepIndex = 0;
            NextStep();
            NextStep();
            if (progressBar != null)
                progressBar.Maximum = stepInfos.Count;
            threadReview = new Thread(new ThreadStart(DoReview));
            ThreadStart();
        }

        private void DoReview()
        {
            while(reviewing)
            {
                if(stepIndex<0||stepIndex>=stepInfos.Count)
                {
                    reviewing = false;
                    break;
                }
                Thread.Sleep((int)((double)stepInfos[stepIndex].timeTick/reviewSpeed));
                if (!reviewing)
                    break;
                Invoke(new EventHandler(DoInvoke));
            }
            Invoke(new EventHandler(DoInvoke));
            threadReview = null;
        }

        public void LoadFromFile(string AfileName)
        {
            if (!File.Exists(AfileName))
                return;
            FileStream vFileStream = new FileStream(AfileName, FileMode.Open, FileAccess.Read);
            LoadFromFile(vFileStream.Name);
            vFileStream.Close();
        }

        public void LoadFromStream(Stream AStream)      //从流中载入数据
        {
            if (AStream == null)
                return;
            byte[] vBuffer = new byte[3];
            if (AStream.Read(vBuffer, 0, vBuffer.Length) != 3)
                return;
            if (vBuffer[0] != 116 || vBuffer[1] != 114 || vBuffer[2] != 102)
                return;
            if (colCount != (byte)AStream.ReadByte())
                return;
            if (rowCount != (byte)AStream.ReadByte())
                return;
            if(threadReview!=null)
            {
                threadReview.Abort();
                threadReview = null;
            }
            timer.Enabled = false;
            palying = false;
            reviewing = false;
            brickTemplets.Clear();
            if (progressBar != null)
                progressBar.Value = 0;
            int vTempletsCount = AStream.ReadByte();
            for(int i=0;i<vTempletsCount;i++)
            {
                List<List<Point>> templets = new List<List<Point>>();
                int vPointsLength = AStream.ReadByte();
                for(int j=0;j<vPointsLength;j++)
                {
                    List<Point> bricks = new List<Point>();
                    int vPointCount = AStream.ReadByte();
                    for(int k=0;k<vPointCount;k++)
                    {
                        int vData = AStream.ReadByte();
                        if (vData < 0)
                            break;
                        bricks.Add(new Point(vData & 3, vData >> 4 & 3));
                    }
                    templets.Add(bricks);
                }
                brickTemplets.Add(templets);
            }
            stepInfos.Clear();
            vBuffer = new byte[sizeof(int)];
            if (AStream.Read(vBuffer, 0, vBuffer.Length) != vBuffer.Length)
                return;
            int vStepCount = BitConverter.ToInt32(vBuffer, 0);
            for(int i=0;i<vStepCount;i++)
            {
                stepInfo vStepInfo = new stepInfo();
                vStepInfo.paraml = (byte)AStream.ReadByte();
                vBuffer = new byte[sizeof(ushort)];
                if (AStream.Read(vBuffer, 0, vBuffer.Length) != vBuffer.Length)
                    return;
                vStepInfo.timeTick = (ushort)BitConverter.ToUInt16(vBuffer, 0);
                int vData = AStream.ReadByte();
                vStepInfo.command = (byte)(vData & 3);
                vStepInfo.param2 = (byte)(vData >> 4 & 3);
                stepInfos.Add(vStepInfo);
            }
            Clear();
            Invalidate();
        }

        public void SaveToFile(string AfileName)        //将玩家的操作记录保存到指定文件中
        {
            FileStream vFileStream = new FileStream(AfileName, FileMode.Create, FileAccess.Write);
            SaveToStream(vFileStream);
            vFileStream.Close();
        }
      
        public void SaveToStream(Stream AStream)        //将玩家的操作记录保存到指定的流中
        {
            if (AStream == null)
                return;
            byte[] vBuffer = Encoding.ASCII.GetBytes("trf");
            AStream.Write(vBuffer, 0, vBuffer.Length);  //写入头信息
            AStream.WriteByte((byte)colCount);
            AStream.WriteByte((byte)rowCount);
            byte vByte = (byte)brickTemplets.Count;
            AStream.WriteByte(vByte);
            foreach(List<List<Point>> vList in brickTemplets)
            {
                vByte = (byte)vList.Count;
                AStream.WriteByte(vByte);
                foreach(List<Point> vPoints in vList)
                {
                    vByte = (byte)vPoints.Count;
                    AStream.WriteByte(vByte);
                    foreach(Point vPoint in vPoints)
                    {
                        vByte = (byte)(vPoint.Y << 4 | vPoint.X);
                        AStream.WriteByte(vByte);
                    }
                }
            }
            AStream.Write(BitConverter.GetBytes(stepInfos.Count), 0, sizeof(int));
            foreach(StepInfo vStepInfo in StepInfos)
            {
                AStream.WriteByte(vStepInfo.param1);
                AStream.Write(BitConverter.GetBytes(vStepInfo.timeTick), 0, sizeof(ushort));
                vByte = (byte)(vStepInfo.param2 << 4 | vStepInfo.command);
                AStream.WriteByte(vByte);
            }
        }

        public void DrawPoint(Graphics AGraphics,Point APoint,byte Abrick)      //俄罗斯方块中一个点的图像绘制函数
        {
            if（ImageList==null)
                return;
            if (ImageList.Images.Count <= 0)
                return;
            if (APoint.X < 0 || APoint.X >= colCount)
                return;
            if (APoint.Y < 0 || APoint.Y >= rowCount)
                return;
            Rectangle vRectangle = new Rectangle(APoint.X * brickWidth, APoint.Y * brickHeigth, brickWidth, brickHeigth);
            AGraphics.FillRectangle(new SolidBrush(BackColor), vRectangle);
            if (Abrick <= 0)
                return;
            Abrick = (byte)(Abrick - 1) % ImageList.Images.Count;
            Image vImage = ImageList.Images(Abrick);
            AGraphics.DrawImage(vImage, vRectangle.Location);

        }

        public void DrawPoints(Graphics AGraphics)      //在窗体内绘制整个点阵
        {
            if (imageList == null)
                return;
            if (imageList.Images.Count <= 0)
                return;
            for (int i = 0; i < colCount; i++)
                for (int j = 0; j < rowCount; j++)
                    DrawPoint(AGraphics, new Point(i, j), points[i, j]);
        }

        public void DrawCurrent(Graphics AGraphics,bool AClear)     //该方法在窗体内绘制当前被控制的方块
        {
            if (imageList == null)
                return;
            if (imageList.Images.Count <= 0)
                return;
            foreach (Point vPoint in brickTemplets[brickIndex][facingIndex])
                DrawPoint(AGraphics, new Point(vPoint.X + brickPoint.X, vPoint.Y + brickPoint.Y), AClear ? (byte)0 : (byte)(brickIndex + 1));
        }

        public void DrawNext(Graphics AGraphics)        //该方法在窗体内绘制下一个出现的方块
        {
            if (AGraphics == null)
                return;
            if (imageList == null)
                return;
            if (imageList.Images.Count <= 0)
                return;
            foreach (Point vPoint in brickTemplets[afterBrickIndex][afterFacingIndex])
                DrawPoint(AGraphics, new Point(vPoint.X, vPoint.Y), (byte)(afterBrickIndex + 1));
        }

        public void DrawScore(Graphics Agraphics)       //该方法在窗体内绘制积分框
        {
            if (Agraphics == null)
                return;
            Rectangle vRectangleF = new Rectangle(0, 0, brickWidth * 4, brickHeigth);
            StringFormat vStringFormat = new StringFormat();
            vStringFormat.FormatFlags |= StringFormatFlags.LineLimit;
            vStringFormat.Alignment = StringAlignment.Center;
            Agraphics.DrawString("Score", new Font(Font, FontStyle.Bold), Brushes.White, vRectangleF, vStringFormat);
            vRectangleF.Offset(0, brickHeigth);
            Agraphics.DrawString(score.ToString(), Font, Brushes.White, vRectangleF, vStringFormat);
            vRectangleF.Offset(0, brickHeigth);
            Agraphics.DrawString("Level", new Font(Font, FontStyle.Bold), Brushes.White, vRectangleF, vStringFormat);
            vRectangleF.Offset(0, brickHeigth);
            Agraphics.DrawString(level.ToString(), Font, Brushes.White, vRectangleF, vStringFormat);
            vRectangleF.Offset(0, brickHeigth);
            Agraphics.DrawString("Lines", new Font(Font, FontStyle.Bold), Brushes.White, vRectangleF, vStringFormat);
            vRectangleF.Offset(0, brickHeigth);
            Agraphics.DrawString(lines.ToString(), Font, Brushes.White, vRectangleF, vStringFormat);
        }

        public bool CheckBrick(byte ABrickIndex,byte AFacingIndex,Point ABrickPoint)        //通过此方法检查方块是否可以移动或变化到此位置
        {
            foreach(Point vPoint in brickTemplets[ABrickIndex][afterFacingIndex])
            {
                if (vPoint.X + ABrickPoint.X < 0 || vPoint.X + ABrickPoint.X >= colCount)
                    return false;
                if (vPoint.Y + ABrickPoint.Y < 0 || vPoint.Y + ABrickPoint.Y >= rowCount)
                    return false;
                if (points[vPoint.X + ABrickPoint.X, vPoint.Y + ABrickPoint.Y] != 0)
                    return false;
            }
            return true;
        }

        public void FreeLine()      //该方法进行消行处理，即操作成功之后将方块行删除
        {
            int vFreeCount = 0;
            for(int j=rowCount-1;j>=0;j--)
            {
                bool vExistsFull = true;        //是否存在满行
                for (int i = 0; i < colCount && vExistsFull; i++)
                    if (points[i, j] == 0)
                        vExistsFull = false;
                if (!vExistsFull)
                    continue;
                #region 图片下移
                Graphics vGraphics = Graphics.FromImage(backBitmap);
                Rectangle srcRect = new Rectangle(0, 0, backBitmap.Width, j * brickHeigth);
                Rectangle destRect = srcRect;
                destRect.Offset(0, brickHeigth);
                Bitmap vBitmap = new Bitmap(srcRect.Width, srcRect.Height);
                Graphics.FromImage(vBitmap).DrawImage(backBitmap, 0, 0);
                vGraphics.DrawImage(vBitmap, destRect, srcRect, GraphicsUnit.Pixel);
                vGraphics.FillRectangle(new SolidBrush(BackColor), 0, 0, backBitmap.Width, brickHeigth);
                #endregion 图片下移
                lines++;
                vFreeCount++;
                for(int k=j;k>=0;k--)
                {
                    for (int i = 0; i < colCount; i++)
                        if (k == 0)
                            points[i, k] = 0;
                        else
                            points[i, k] = points[i, k - 1];
                }
                j++;
            }
            score += scoress[vFreeCount];
            if(vFreeCount>0)
            {
                level = Math.Min(lines / 30, speeds.Length - 1);
                timer.Interval = speeds[level];
                Invalidate();
            }
            if (youxiScore != null)
                youxiScore.Update(this);
        }

        public bool BrickOperate(BrickOperates ABrickOperatess)     //通过该方法设置对方块的变化处理
        {
            byte vFacingIndex = facingIndex;
            Point vBrickPoint = brickPoint;
            switch(ABrickOperatess)
            {
                case BrickOperates.boturnLeft:
                    vFacingIndex = (byte)((vFacingIndex + 1) % brickTemplets[brickIndex].Count);
                    break;
                case BrickOperates.boturnRight:
                    vFacingIndex = (byte)((brickTemplets[brickIndex].Count + vFacingIndex - 1) % brickTemplets[brickIndex].Count);
                    break;
                case BrickOperates.boMoveLeft:
                    vBrickPoint.Offset(-1, 0);
                    break;
                case BrickOperates.boMoveRight:
                    vBrickPoint.Offset(+1, 0);
                    break;
                case BrickOperates.boMoveDown:
                    vBrickPoint.Offset(0, +1);
                    break;
                case BrickOperates.boMoveBottom:
                    vBrickPoint.Offset(0, +1);
                    while (CheckBrick(brickIndex, vFacingIndex, vBrickPoint))
                        vBrickPoint.Offset(0, -1);
                    vBrickPoint.Offset(0, -1);
                    break;
            }
            if(CheckBrick(brickIndex,vFacingIndex,vBrickPoint))
            {
                if(palying&&recordMode&&!reviewing)
                {
                    stepInfos.Add(new StepInfo(Environment.TickCount - lastRecordTime, 2, (byte)ABrickOperatess, 0));
                    lastRecordTime = Environment.TickCount;
                }
                Graphics vGraphics = Graphics.FromImage(backBitmap);
                DrawCurrent(vGraphics, true);
                facingIndex = vFacingIndex;
                brickPoint = vBrickPoint;
                DrawCurrent(vGraphics, false);
                if (ABrickOperatess == BrickOperates.boMoveBottom)
                    Downfall();
                else Invalidate();
            }
            else if(ABrickOperatess==BrickOperates.boMoveDown)
            {
                if(palying&&recordMode&&!reviewing)
                {
                    stepInfos.Add(new StepInfo(Environment.TickCount - lastRecordTime, 2, (byte)ABrickOperatess, 0));
                    lastRecordTime = Environment.TickCount;
                }
                Downfall();
            }
            return true;
        }

        public void NextBrick()     //通过该方法预览显示下一个出现的方框
        {
            brickIndex = afterBrickIndex;
            facingIndex = afterBrickIndex;
            brickPoint.X = colCount / 2 - 1;
            brickPoint.Y=0; 
            afterBrickIndex = (byte)random.Next(brickTemplets.Count);
            afterFacingIndex = (byte)random.Next(brickTemplets[afterBrickIndex].Count);
            if (palying && recordMode && !reviewing)
            {
                stepInfos.Add(new StepInfo(0, 1, afterBrickIndex, afterFacingIndex));
                lastRecordTime = Environment.TickCount;
            }
            if (youxiNext != null && afterBrickIndex != brickIndex)
                youxiNext.Update(this);
            DrawCurrent(Graphics.FromImage(backBitmap), false);
            if(!CheckBrick(brickIndex,facingIndex,brickPoint))
            {
                if(palying &&recordMode &&!reviewing)
                {
                    stepInfos.Add(new StepInfo(0, 3, 0, 0));
                    lastRecordTime = Environment.TickCount;
                }
                GameOver();
            }
            Invalidate();
        }

        private void Downfall()     //执行方块落底后的处理
        {
            foreach (Point vPoint in brickTemplets[brickIndex][facingIndex])
                points[vPoint.X + brickPoint.X, vPoint.Y + brickPoint.Y] = (byte)(brickIndex + 1);
            FreeLine();
            if (palying && !reviewing)
                NextBrick();
        }

        private void ImageListRecreateHandle(object sender,EventArgs e)
        {
            DockChanged();
        }

        private void DetachImageList(object sender,EventArgs e)
        {
            imageList = null;
        }

        public ImageList ImageList
        {
            get
            {
                return imageList;
            }
            set
            {
                if(value!=imageList)
                {
                    EventHandler handler = new EventHandler(ImageListRecreateHandle);
                    EventHandler handler2 = new EventHandler(DetachImageList);
                    if(imageList!=null)
                    {
                        imageList.RecreateHandle -= handler;
                        imageList.Disposed -= handler2;
                    }
                    imageList = value;
                    if(value!=null)
                    {
                        brickWidth = ImageList.ImageSize.Width;
                        brickHeigth = ImageList.ImageSize.Height;
                        DockChanged();
                        if (!palying && != reviewing)
                            GameOver();
                        if(youxiNext!=null)
                        {
                            youxiNext.BackColor = BackColor;
                            youxiNext.SetSize(brickWidth * 4, brickHeigth * 4);
                            youxiNext.Update(this);
                        }
                        value.RecreateHandle += handler;
                        value.Disposed += handler2;
                    }
                }
            }
        }

        private void DetachyouxiScore(object sender,EventArgs e)
        {
            youxiScore = null;
        }

        public youxiScore TetrisScore       //属性：设置得到游戏积分
        {
            get
            {
                return youxiScore;
            }
            set
            {
                if(value!=youxiScore)
                {
                    EventHandler handler = new EventHandler(DetachyouxiScore);
                    if (youxiScore != null)
                        youxiScore.Disposed -= handler;
                    youxiScore = value;
                    if(value!=null)
                    {
                        value.SetSize(4 * brickWidth, 6 * brickHeigth);
                        value.Update(this);
                        value.Disposed += handler;
                    }
                }
            }
        }

        private void DetachProgressBar(object sender,EventArgs e)
        {
            progressBar = null;
        }
        ///<summary>
        ///回放进度条
        ///</summary>>
        public ProgressBar ProgressBar      //设置实现回放进度条的处理
        {
            get
            {
                return progressBar;
            }
            set
            {
                if(value !=progressBar)
                {
                    EventHandler handler = new EventHandler(DetachProgressBar);
                    if (progressBar != null)
                        progressBar.Disposed -= handler;
                    progressBar = value;
                    if(value!=null)
                    {
                        progressBar.Minimum = 0;
                        progressBar.Maximum = stepInfos.Count;
                        progressBar.Value = stepIndex < 0 ? 0 : stepIndex;
                        value.Disposed += handler;
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (backBitmap != null)
                e.Graphics.DrawImage(backBitmap, 0, 0);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return (keyData == Keys.Left) || (keyData == Keys.Up) || (keyData == Keys.Left) || (keyData == Keys.Right) || (keyData == Keys.Escape) || base.IsInputKey(keyData);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (CanFocus)
                Focus();
        }

        protected override void Dispose(bool disposing)
        {
            if (threadReview != null)
                threadReview.Abort();
            base.Dispose(disposing);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!palying || reviewing)
                return;
            switch(e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:     //左移
                    BrickOperate(BrickOperates.boMoveLeft);
                    break;
                case Keys.D:
                case Keys.Right:    //右移        
                    BrickOperate(BrickOperates.boMoveRight);
                    break;
                case Keys.W:
                case Keys.Up:       //左旋
                    BrickOperate(BrickOperates.boturnLeft);
                    break;
                case Keys.Back:
                case Keys.F:        //右旋
                    BrickOperate(BrickOperates.boturnRight);
                    break;
                case Keys.Down:
                case Keys.S:        //直下
                    BrickOperate(BrickOperates.boMoveDown);
                    break;
                case Keys.Enter:
                case Keys.Space:
                case Keys.End:
                case Keys.J:        
                    BrickOperate(BrickOperates.boMoveBottom);
                    break;
            }
        }
    }

    public class youxiNext : Control      //显示下一个出现的方块控件
    {
        public youxiNext()      //构造函数
        {
            BackColor = Color.Black;
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private Bitmap backBitmap;
        public void Clear()     //使控件一定区域内的显示块消失
        {
            Graphics vGraphics = Graphics.FromImage(backBitmap);
            vGraphics.FillRectangle(new SolidBrush(BackColor), vGraphics.ClipBounds);
        }

        public void Update(youxiControl AyouxiControl)
        {
            if (AyouxiControl == null)
                return;
            Clear();
            AyouxiControl.DrawNext(Graphics.FromImage(backBitmap));
            Invalidate();
        }

        public void SetSize(int Awidth, int Aheight)
        {
            Width = Awidth;
            Height = Aheight;
            backBitmap = new Bitmap(Awidth, Aheight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (backBitmap != null)
                e.Graphics.DrawImage(backBitmap, 0, 0);
        }
    }

    public class youxiScore : Control     //积分显示控件
    {
        private Bitmap backBitmap;

        public youxiScore()
        {
            BackColor = Color.Black;
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void Clear()     //使控件一定区域内的显示块消失
        {
            Graphics vGraphics = Graphics.FromImage(backBitmap);
            vGraphics.FillRectangle(new SolidBrush(BackColor), vGraphics.ClipBounds);
        }

        public void Update(youxiControl AyouxiControl)
        {
            if (AyouxiControl == null)
                return;
            Clear();
            AyouxiControl.DrawScore(Graphics.FromImage(backBitmap));
            Invalidate();
        }

        public void SetSize(int Awidth, int Aheight)
        {
            Width = Awidth;
            Height = Aheight;
            backBitmap = new Bitmap(Awidth, Aheight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (backBitmap != null)
                e.Graphics.DrawImage(backBitmap, 0, 0);
        }
    }
    */
    /// <summary>
    /// 俄罗斯方块类型
    /// </summary>
    class Brick
    {
        #region 事件
        public delegate void LabelChangeHandler(int ChangeValue);
        public event LabelChangeHandler XChange;
        public event LabelChangeHandler YChange;
        #endregion

        #region 数据成员
        private Point[] m_Points;       //方块坐标数组，保存该俄罗斯方块的坐标信息

        private  int labelOfX;           //用于表示整个方块相对于画图区域的位置，用于对此方块进行计算
        private  int labelOfY;           //同上
        
        private Color brickOfColor;     //砖块的颜色
        private Color backGroundColor;  //背景颜色
        private int cellPixel;          //单元格像素
        private SolidBrush brush;       //画笔
        #endregion

        #region 属性区
        /// <summary>
        /// 获取砖块模板
        /// </summary>
        /// <param name="index"></param>
        public Point[] Points
        {
            get { return m_Points; }
            set { }
        }
        
        /// <summary>
        /// 获取砖块颜色
        /// </summary>
        public Color Color
        {
            get { return brickOfColor; }
        }

        /// <summary>
        /// 获取砖块中心点在主画布中的X坐标
        /// </summary>
        public int X
        {
            get { return labelOfX; }
            set
            {
                if (XChange != null)
                    XChange(value);
                labelOfX += value;
            }
        }
        /// <summary>
        /// 获取砖块中心在主画布中的Y坐标
        /// </summary>
        public int Y
        {
            get { return labelOfY; }
            set
            {
                if (YChange != null)
                    YChange(value);
                labelOfY += value;
            }
        }
        #endregion

        #region 成员函数
        private Rectangle PointToRectangle(Point p)     //单点放大成矩形
        {
            Rectangle r = new Rectangle((labelOfX + p.X) * cellPixel + 1, (labelOfY + p.Y) * cellPixel + 1, cellPixel - 2, cellPixel - 2);
            return r;
        }

        ///<summary>
        ///构造砖块
        ///</summary>
        ///<param name="style">砖块样式</param>
        ///<param name="color">砖块颜色</param>
        ///<param name="backGroundColor">背景颜色</param>
        ///<param name="size">砖块单位大小</param>
        public Brick(Point[] style,Color color,Color bgColor,int size)
        {
            this.XChange += new LabelChangeHandler(ChangeLabelX);
            this.YChange += new LabelChangeHandler(ChangeLabelY);
            brickOfColor = color;
            backGroundColor = bgColor;
            cellPixel = size;
            m_Points = style;
            brush = new SolidBrush(color);
            labelOfX = 2;
            labelOfY = 2;
        }

        ///<summary>
        ///顺时针旋转90度
        /// </summary>
        public void DeasilRotate()
        {
            int temp;
            for(int i=0;i<m_Points.Length;i++)
            {
                temp = m_Points[i].X;
                m_Points[i].X = -m_Points[i].Y;
                m_Points[i].Y = temp;
            }
        }

        /// <summary>
        /// 画砖块到画布
        /// </summary>
        /// <param name="gp"></param>
        public void Paint(Graphics gp)
        {
            foreach(Point p in m_Points)
            {
                lock(gp)
                {
                    try
                    {
                        gp.FillRectangle(brush, PointToRectangle(p));       //给定指定 颜色的画笔和 坐标，填充矩形
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        /// <summary>
        /// 擦除画布上的砖块
        /// </summary>
        /// <param name="gp"></param>
        public void Erase(Graphics gp)
        {
            using (SolidBrush sb = new SolidBrush(backGroundColor))
            {
                foreach(Point p in m_Points)
                {
                    lock(gp)
                    {
                        try
                        {
                            gp.FillRectangle(sb, PointToRectangle(p));
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
            }
        }
        #endregion

        #region 事件处理函数
        private void ChangeLabelX(int ChangeValue)
        {
            for (int i = 0; i < this.Points.Length; i++)
                this.Points[i].X = this.Points[i].X + ChangeValue;
        }

        private void ChangeLabelY(int ChangeValue)
        {
            for (int i = 0; i < this.Points.Length; i++)
                this.Points[i].Y = this.Points[i].Y + ChangeValue;
        }
        #endregion
    }

    /// <summary>
    /// 砖块的模板类
    /// </summary>
    class BrickTemplate
    {
        #region 数据成员
        private string m_Code;      //砖块样式编码
        private Color m_Color;      //颜色
        #endregion

        #region 成员函数
        /// <summary>
        /// 构造砖块信息
        /// </summary>
        /// <param name="code">砖块编码字符串</param>
        /// <param name="color">砖块颜色</param>
        public BrickTemplate(string code,Color color)
        {
            if (code == null || code.Length != 25 || color == Color.Empty)
                throw new FormatException("砖块样式信息错误！");
            m_Code = code;
            m_Color = color;
        }
        #endregion

        #region 属性区
        /// <summary>
        /// 获取砖块样式编码
        /// </summary>
        public string Code
        {
            get { return m_Code; }
        }

        /// <summary>
        /// 获取砖块颜色
        /// </summary>
        public Color Color
        {
            get { return m_Color; }
        }
        #endregion
    }

    /// <summary>
    /// 砖块的维护类
    /// </summary>
    class TemplateArry
    {
        #region 数据成员
        private ArrayList m_List = new ArrayList();     //砖块模板列表
        #endregion

        #region 属性
        /// <summary>
        /// 获取砖块模板数
        /// </summary>
        public int CountOfTemplate
        {
            get { return m_List.Count; }
        }

        /// <summary>
        /// 获取砖块模板
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns>砖块模具</returns>
        public BrickTemplate this[int index]
        {
            get { return (BrickTemplate)m_List[index]; }
        }
        #endregion

        #region 成员函数
        /// <summary>
        /// 添加新砖块模板
        /// </summary>
        /// <param name="code">砖块样式编码</param>
        /// <param name="color">颜色</param>
        public void Add(string code,Color color)
        {
            m_List.Add(new BrickTemplate(code, color));
        }

        /// <summary>
        /// 清空所有砖块模板
        /// </summary>
        public void Clear()
        {
            m_List.Clear();
        }
        #endregion
    }

    /// <summary>
    /// 砖块生产机的类型
    /// </summary>
    class BrickFactory
    {
        #region 数据成员
        private TemplateArry m_BrickArray;      //砖块样式模板列表
        private Color m_BgColor;                //砖块背景颜色
        private int m_CellPixel;                //单元格大小
        #endregion

        #region 成员函数
        /// <summary>
        /// 砖块生产机构造
        /// </summary>
        /// <param name="info">砖块列表</param>
        /// <param name="bgColor">背景色</param>
        /// <param name="rectPixel">砖块大小</param>
        public BrickFactory(TemplateArry info,Color bgColor,int rectPixel)
        {
            m_BrickArray = info;
            m_CellPixel = rectPixel;
            m_BgColor = bgColor;
        }

        /// <summary>
        /// 随机获取下一个砖块
        /// </summary>
        /// <returns>砖块</returns>
        public Brick CreatBrick()
        {
            Random rd = new Random();
            int index = rd.Next(m_BrickArray.CountOfTemplate);      //获取一个固定范围之内的随机数所谓模板列表的标签
            string code = m_BrickArray[index].Code;                 //获取该标签内的模板

            List<Point> list = new List<Point>();                   //创建一个装入即将要绘制的点的容器
            for(int i=0;i<code.Length;i++)                          //筛选出将要绘制的点
            {
                if(code[i]=='1')                                    //模板编码里的数值为1
                {
                    Point p = new Point(i % 5, i / 5);              //那么就根据下标计算坐标
                    p.Offset(-2, -2);                               //使该点平移，让它的中心点坐标为（0，0）
                    list.Add(p);                                    //将该点放入容器
                }
            }
            Brick brick = new Brick(list.ToArray(), m_BrickArray[index].Color, m_BgColor, m_CellPixel);     //生成一个俄罗斯方块
            if (rd.Next(2) == 1)
                brick.DeasilRotate();
            return brick;
        }
        #endregion
    }

    /// <summary>
    /// 游戏核心：画布类
    /// </summary>
    class GamePalette
    {
        #region 常量区
        private readonly Color[] COLORS = new Color[] { Color.White, Color.Tomato, Color.Thistle, Color.Turquoise };        //闪烁颜色
        private readonly int[] TIME_SPANS = new int[] { 700, 600, 550, 500, 450, 400, 350, 300, 250, 200 };                 //等级对应的速度
        private readonly int[] SCORE_SPANS = new int[] { 100, 300, 500, 1000, 1500 };                                       //分值列表
        #endregion

        #region 变量
        private BrickFactory m_BrickFactory;    //砖块产生机
        private int m_Width;               //画布宽；
        private int m_Heigth;              //画布高
        private Color[,] m_ColorArray;          //固定砖块颜色数组
        private Color m_backGroundColor;        //背景色
        private Color m_GridColor;              //网格颜色
        private int m_CellSize;                 //单元格大小
        private int m_Level = 0;                //等级
        private int m_Score = 0;                //总分数
        private bool m_GameOver = false;        //是否结束
        private bool m_ShowGrid = false;        //显示网格
        private bool m_Pause = false;           //暂停标志
        private bool m_Ready = false;           //运行标志

        private Graphics m_MainPalette;         //游戏主画布
        private Graphics m_NextPalette;         //Next画布
        private Brick m_RunBrick;               //活动的砖块
        private Brick m_NextBrick;              //下一个砖块
        private System.Timers.Timer m_TimerBrick;//定时器，用来更新砖块
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造画布
        /// </summary>
        /// <param name="width">画布宽</param>
        /// <param name="heigth">画布高</param>
        /// <param name="info">砖块模板列表</param>
        /// <param name="size">单元格大小</param>
        /// <param name="bgColor">背景颜色</param>
        /// <param name="gpPalettte">主画布</param>
        /// <param name="gpNext">次画布</param>
        /// <param name="level">等级</param>
        /// <param name="gridColor">网格颜色</param>
        /// <param name="showGrid">是否显示网格</param>
        public GamePalette(int width,
                           int height,
                           TemplateArry info,
                           int size,
                           Color bgColor,
                           Graphics gpPalette,
                           Graphics gpNext,
                           int level,
                           Color gridColor,
                           bool showGrid)
        {
            m_Width = width;
            m_Heigth = height;
            m_ColorArray = new Color[width, height];
            m_backGroundColor = bgColor;
            m_MainPalette = gpPalette;
            m_NextPalette = gpNext;
            m_CellSize = size;
            m_Level = level;
            m_GridColor = gridColor;
            m_ShowGrid = showGrid;
            m_BrickFactory = new BrickFactory(info, bgColor, size);

            InitRandomBrick();      //生成level行随机砖块，增加游戏可玩度
        }
        #endregion

        #region 属性访问器
        /// <summary>
        /// 获取游戏是否结束的标志
        /// </summary>
        public bool IsGameOver
        {
            get { return m_GameOver; }
        }

        /// <summary>
        /// 获取当前的游戏等级
        /// </summary>
        public int Level
        {
            get { return m_Level; }
        }

        /// <summary>
        /// 获取当前游戏分数
        /// </summary>
        public int Score
        {
            get { return m_Score; }
        }

        /// <summary>
        /// 确定目前游戏是否正在运行
        /// </summary>
        public bool IsRunning
        {
            get { return m_Ready; }
        }

        /// <summary>
        /// 访问私有数据成员：画布宽度
        /// </summary>
        public int Width
        {
            get { return m_Width; }
            set
            {
                if (value != 0)
                    m_Width = value;        
            }
        }

        /// <summary>
        /// 访问私有数据成员画布高度
        /// </summary>
        public int Height
        {
            get { return m_Heigth ; }
            set
            {
                if (value != 0)
                    m_Heigth = value;
            }
        }
        #endregion

        #region 操作函数
        /// <summary>
        /// 下移一个单位
        /// </summary>
        public bool MoveDown()
        {
            if (m_RunBrick == null)              //如果没有可以活动的方块，那么返回 ture
                return true;
            int xPos = m_RunBrick.X;             //X坐标不变
            int yPos = m_RunBrick.Y - 1 ;        //Y坐标减1
            
            for(int i=0;i<m_RunBrick.Points.Length;i++)     //循环检测各个砖块点的下一个位置是否合法
            {
                //m_RunBrick.Points[i].Y++;
                if (yPos + m_RunBrick.Points[i].Y >= m_Heigth)
                    return false;
                if (xPos + m_RunBrick.Points[i].X >= m_Width)
                    return false;
            }

            m_RunBrick.Erase(m_MainPalette);    //清除当前砖块在画布上的图形
            m_RunBrick.Y++;                     //修正新的坐标偏移      
            m_RunBrick.Paint(m_MainPalette);    //重新绘制砖块图形到画布上
            return true;
        }

        /// <summary>
        /// 下移到底
        /// </summary>
        public void DropDown()
        {
            m_TimerBrick.Stop();        //暂停使用定时器
            while (MoveDown()) ;        //循环调用向下移动函数，直到不能移动为止
            m_TimerBrick.Start();       //重新启用定时器
        }

        /// <summary>
        /// 顺时针旋转90度
        /// </summary>
        /// <returns></returns>
        public bool DeasilRotate()
        {
            if (m_RunBrick == null)
                return true;
            for(int i=0;i<m_RunBrick.Points.Length;i++)
            {
                int x = m_RunBrick.X - m_RunBrick.Points[i].Y;      //计算旋转后的新坐标
                int y = m_RunBrick.Y + m_RunBrick.Points[i].X;

                if (x < 0 || x >= m_Width || y < 0 || y >= m_Heigth || !m_ColorArray[x, y].IsEmpty)     //判断新坐标点是否合适
                    return false;
            }
            m_RunBrick.Erase(m_MainPalette);
            m_RunBrick.DeasilRotate();
            m_RunBrick.Paint(m_MainPalette);
            return true;
        }

        /// <summary>
        /// 重画游戏画布
        /// </summary>
        /// <param name="gp"></param>
        public void PaintPalette(Graphics gp)
        {
            lock(gp)        //以背景色清除画布
            {
                gp.Clear(m_backGroundColor);
            }
            if (m_ShowGrid)     //画网格
                PaintGridLine(gp);
            PaintBricks(gp);    //画已有砖块
            if (m_RunBrick != null)     //画当前正在活动的砖块
                m_RunBrick.Paint(gp);
        }

        /// <summary>
        /// 重画下一个砖块
        /// </summary>
        /// <param name="gp"></param>
        public void PaintNext(Graphics gp)
        {
            lock(gp)
            {
                gp.Clear(m_backGroundColor);
            }
            if (m_NextBrick != null)
                m_NextBrick.Paint(gp);
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        public void Start()
        {
            m_RunBrick = m_BrickFactory.CreatBrick();       //随机生产一个砖块
            m_RunBrick.X = m_Width / 2;                     //水平位置在画布中心
            int y = 0;
            for(int i=0;i<m_RunBrick.Points.Length;i++)
            {
                if (m_RunBrick.Points[i].Y < y)
                    y = m_RunBrick.Points[i].Y;
            }
            m_RunBrick.Y = -y;      //垂直为止保证能完整显示
            PaintPalette(m_MainPalette);

            Thread.Sleep(20);       //延时产生下一个砖块
            m_NextBrick = m_BrickFactory.CreatBrick();
            PaintNext(m_NextPalette);

            m_TimerBrick = new System.Timers.Timer(TIME_SPANS[m_Level]);
            m_TimerBrick.Elapsed += new System.Timers.ElapsedEventHandler(OnBrickTimedEvent);
            m_TimerBrick.AutoReset = true;      
            m_TimerBrick.Start();         //开始定时器
        }

        /// <summary>
        /// 退出游戏
        /// </summary>
        public void Close()
        {
            if(m_TimerBrick!=null)
            {
                m_TimerBrick.Close();
                m_TimerBrick.Dispose();
            }
            m_NextPalette.Dispose();
            m_MainPalette.Dispose();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            m_Pause = true;
            m_TimerBrick.Stop();
        }

        /// <summary>
        /// 继续
        /// </summary>
        public void Resume()
        {
            m_Pause = false;
            m_TimerBrick.Start();
        }

        /// <summary>
        /// 左移
        /// </summary>
        /// <returns></returns>
        public bool MoveLeft()
        {
            if (m_RunBrick == null)
                return true;
            int xPos = m_RunBrick.X + 1;
            int yPos = m_RunBrick.Y;
            for(int i=0;i<m_RunBrick.Points.Length;i++)
            {
                if (yPos + m_RunBrick.Points[i].Y >= m_Heigth)
                    return false;
                if (!m_ColorArray[xPos + m_RunBrick.Points[i].X, yPos + m_RunBrick.Points[i].Y].IsEmpty)
                    return false;
            }
            m_RunBrick.Erase(m_MainPalette);
            m_RunBrick.X++;
            m_RunBrick.Paint(m_MainPalette);
            return true;
        }

        /// <summary>
        /// 右移
        /// </summary>
        /// <returns></returns>
        public bool MoveRight()
        {
            if (m_RunBrick == null)
                return true;
            int xPos = m_RunBrick.X - 1;
            int yPos = m_RunBrick.Y;
            for (int i = 0; i < m_RunBrick.Points.Length; i++)
            {
                if (yPos + m_RunBrick.Points[i].Y >= m_Heigth)
                    return false;
                if (!m_ColorArray[xPos + m_RunBrick.Points[i].X, yPos + m_RunBrick.Points[i].Y].IsEmpty)
                    return false;
            }
            m_RunBrick.Erase(m_MainPalette);
            m_RunBrick.X--; 
            m_RunBrick.Paint(m_MainPalette);
            return true;
        }
        #endregion

        #region 私有函数

        /// <summary>
        /// 实时更新砖块信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnBrickTimedEvent(object source,ElapsedEventArgs e)
        {
            MoveDown();     //首先下落一格
            if(m_Pause||m_GameOver)     
            {
                if (m_GameOver)     //如果游戏结束，那么绘制“Game Over”字样
                    PaintGameOver();
                return;
            }
            if(m_Ready)    
            {
                if (!MoveDown())        //如果正在游戏但并没有下降成功
                    CheckAndOverBrick();//检查俄罗斯方块
            }
        }

        /// <summary>
        /// 绘制网格
        /// </summary>
        /// <param name="gp"></param>
        private void PaintGridLine(Graphics gp)
        {
            try
            {
                lock(gp)
                {
                    using (Pen p = new Pen(m_GridColor, 1))
                    {
                        for (int column = 1; column < m_Width; column++)        //画网格纵线
                            gp.DrawLine(p, column * m_CellSize - 1, 0, column * m_CellSize - 1, m_Heigth * m_CellSize);
                        for (int row = 1; row < m_Heigth; row++)                //画网格横线
                            gp.DrawLine(p, 0, row * m_CellSize, m_Width + m_CellSize, row * m_CellSize);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// 画已有的砖块
        /// </summary>
        /// <param name="gp"></param>
        private void PaintBricks(Graphics gp)
        {
            lock(gp)
            {
                for(int row=0;row<m_Heigth;row++)
                {
                    for (int clm=0;clm<m_Width;clm++)
                    {
                        try
                        {
                            Color c = m_ColorArray[clm, row];
                            if (c.IsEmpty)
                                c = m_backGroundColor;
                            using (SolidBrush sb = new SolidBrush(c))
                            {
                                gp.FillRectangle(sb, clm * m_CellSize + 1, row * m_CellSize + 1, m_CellSize - 2, m_CellSize - 2);
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
                    

            }
        }

        /// <summary>
        /// 检查砖块和游戏状态
        /// </summary>
        private void CheckAndOverBrick()
        {
            for (int i = 0; i < m_RunBrick.Points.Length; i++)      //设定当前砖块到画布上
                m_ColorArray[m_RunBrick.X + m_RunBrick.Points[i].X, m_RunBrick.Y + m_RunBrick.Points[i].Y] = m_RunBrick.Color;
            CheckAndDelFullRow();       //检查并消除满行
            m_RunBrick = m_NextBrick;   //设定活动砖块
            m_RunBrick.X = m_Width / 2;
            int y = 0;
            for(int i=0;i<m_RunBrick.Points.Length;i++)
            {
                if (m_RunBrick.Points[i].Y < y)
                    y = m_RunBrick.Points[i].Y;
            }
            m_RunBrick.Y = -y;
            for(int i=0;i<m_RunBrick.Points.Length;i++)
            {
                if(!m_ColorArray[m_RunBrick.X+m_RunBrick.Points[i].X,m_RunBrick.Y+m_RunBrick.Points[i].Y].IsEmpty)      //如果砖块所在的位置已经有砖块，则视为到达顶部，即游戏结束
                {
                    m_RunBrick = null;
                    //闪烁效果
                    m_TimerBrick.Stop();
                    for(int row=m_Heigth-1;row>=0;row--)
                    {
                        for (int column = 0; column < m_Width; column++)
                            m_ColorArray[column, row] = m_backGroundColor;
                        PaintBricks(m_MainPalette);
                        System.Threading.Thread.Sleep(50);
                    }
                    m_GameOver = true;
                    m_Ready = false;
                    m_TimerBrick.Start();
                    return;
                }
            }
            m_RunBrick.Paint(m_MainPalette);

            m_NextBrick = m_BrickFactory.CreatBrick();
            PaintNext(m_NextPalette);
        }

        /// <summary>
        /// 检查并删除满行砖块
        /// </summary>
        private void CheckAndDelFullRow()
        {
            int fullRowCount = 0;       //满行数
            int upRow = m_RunBrick.Y - 2;       //当前砖块的上行标
            int downRow = m_RunBrick.Y + 2;       //当前砖块的下行标
            if (upRow < 0)
                upRow = 0;      //越界处理
            if (downRow >= m_Heigth)
                downRow = m_Heigth - 1;     //越界处理
            for(int row=upRow;row<=downRow;row++)
            {
                bool isFull = true;
                for(int column=0;column<m_Width;column++)
                {
                    if(m_ColorArray[column,row].IsEmpty)        //如果该行颜色都有砖块
                    {
                        isFull = false;
                        break;
                    }
                }
                //清除满行
                if(isFull)
                {
                    fullRowCount++;
                    m_TimerBrick.Stop();        //暂停游戏
                    //闪烁效果
                    for(int n=0;n<COLORS.Length;n++)
                    {
                        for (int column = 0; column < m_Width; column++)
                            m_ColorArray[column, row] = COLORS[n];
                        PaintBricks(m_MainPalette);
                        System.Threading.Thread.Sleep(50);
                    }
                    //此行上面的所有砖块下移一层
                    for (int rowIndex = row; rowIndex > 0; rowIndex--)
                        for (int column = 0; column < m_Width; column++)
                            m_ColorArray[column, rowIndex] = m_ColorArray[column, rowIndex - 1];
                    PaintBricks(m_MainPalette);
                    m_TimerBrick.Start();       //继续游戏
                }
            }
            if(fullRowCount>0)
            {
                int currentScore = SCORE_SPANS[fullRowCount - 1];      //当前得分
                int tempScore = m_Score;        //游戏得分
                m_Score += currentScore;        //追加得分
                if (m_Score > 10000 && m_Score.ToString()[0] != tempScore.ToString()[0])        //判断升级
                {
                    m_Level = (m_Level + 1) % TIME_SPANS.Length;        //当前级别
                    m_TimerBrick.Interval = TIME_SPANS[m_Level];        //游戏速度
                }
            }

        }

        /// <summary>
        /// 根据初始化的等级，产生随机砖块
        /// </summary>
        private void InitRandomBrick()
        {
            if(m_Level>0)
            {
                Random r = new Random();
                for(int row=0;row<m_Level;row++)
                {
                    for(int column=0;column<m_Width*2/3;column++)
                    {
                        int rc = r.Next(m_Width);
                        m_ColorArray[rc, m_Heigth - row - 1] = COLORS[r.Next(COLORS.Length)];
                    }
                }
            }
        }

        /// <summary>
        /// 打印游戏结束信息
        /// </summary>
        private void PaintGameOver()
        {
            StringFormat drawFormat = new StringFormat();
            Font font = new Font("Arial BLACK", 30f);
            drawFormat.Alignment = StringAlignment.Center;
            for(int j=0;j<COLORS.Length;j++)
            {
                lock(m_MainPalette)
                {
                    try
                    {
                        m_MainPalette.DrawString("GAME OVEE",
                                                 font,
                                                 new SolidBrush(COLORS[j]),
                                                 new RectangleF(0, m_Heigth * m_CellSize / 2 - 100, m_Width * m_CellSize, 100),
                                                 drawFormat);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                Thread.Sleep(100);
            }
        }

        #endregion
    }
}
