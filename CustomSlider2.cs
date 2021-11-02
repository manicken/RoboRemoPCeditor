using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;

/// <summary>
/// Use this for drawing custom graphics and text with transparency.
/// Inherit from DrawingArea and override the OnDraw method.
/// </summary>
public class CustomSlider2 : Control
{
    private string _label = "Label";
    private Point _textPosition = new Point(0, 0);
    private System.Drawing.Text.TextRenderingHint _TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

    private Rectangle bar;

    public bool setLabelToCurrentValue = true;
    public bool isHandleVisible = true;
    public int handleSize = 30;
    private Rectangle drawArea = Rectangle.Empty;

    [Description("Text to show"), Category("Data")]
    public override string Text
    {
        get { return _label; }
        set { _label = value; Refresh(); }
    }

    public System.Drawing.Text.TextRenderingHint TextRenderingHint
    {
        get { return _TextRenderingHint; }
        set { _TextRenderingHint = value; Refresh(); }
    }

    public System.Drawing.Drawing2D.InterpolationMode GraphicsInterpolationMode { get; set; } = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
    public System.Drawing.Drawing2D.PixelOffsetMode GraphicsPixelOffsetMode { get; set; } = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
    public System.Drawing.Drawing2D.SmoothingMode GraphicsSmoothingMode { get; set; } = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT 

            return cp;
        }
    }

    public CustomSlider2()
    {
        Init();
        
        RoboRemoPC.Debug.AddLine(this.GetHashCode() +  ":" +Environment.StackTrace + "\n");
    }
    public void Init()
    {
        base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        this.KeyUp += this_KeyUp;
        this.Resize += this_Resize;

        this.MouseEnter += this_MouseEnter;
        this.MouseMove += this_MouseMove;
        this.MouseWheel += this_MouseWheel;
        this.MouseDown += this_MouseDown;

        bar = new Rectangle(0, 0, this.Width, this.Height);
        //ValueSteps = this.Height;

    }

    GraphicsPath GrPath
    {
        get
        {
            int amount = 2;
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddArc(0, 0, amount, amount, 180, 90);
            grPath.AddArc(this.Width - amount, 0, amount, amount, 270, 90);
            grPath.AddArc(this.Width - amount, this.Height - amount, amount, amount, 0, 90);
            grPath.AddArc(0, this.Height - amount, amount, amount, 90, 90);
            //grPath.AddEllipse(this.ClientRectangle);
            return grPath;
        }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        drawArea = new Rectangle(new Point(0,0), this.Size); 
        // this be used to define how and where draw this controls
        // contents
        // so that the handle can be drawn outside of the slider bar
    }

    protected override void OnPaintBackground(PaintEventArgs pea)
    {
        if (Orientation == Orientation.Vertical)
            pea.Graphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
        else
            pea.Graphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        graphics.FillRectangle(new SolidBrush(_sliderBarColor), bar);//, 0, 0, this.Width, this.Height);

        if (isHandleVisible)
            DrawHandle(graphics);

        DrawLabel();
    }
    private void DrawHandle(Graphics graphics)
    {
        Image thumbImg;
        Rectangle imgRect;
        if (Orientation == Orientation.Horizontal)
        {
            thumbImg = RoboRemoPC.Resources.slider_thumb_h;
            imgRect = new Rectangle(bar.Right - handleSize/2, bar.Top, handleSize, this.Height);
        }
        else
        {
            thumbImg = RoboRemoPC.Resources.slider_thumb_v;
            imgRect = new Rectangle(bar.Left, bar.Top - handleSize/2, this.Width, handleSize);
        }

        graphics.DrawImage(thumbImg, imgRect);
    }

    Rectangle lastLabelRectangle = Rectangle.Empty;
    private void DrawLabel()
    {
        if (this.Parent == null) return; // wait for init
        
        if (lastLabelRectangle != Rectangle.Empty)
        {
            this.Parent.Invalidate(lastLabelRectangle);
            this.Parent.Update();
        }

        Graphics graphics = this.Parent.CreateGraphics();
        // Set the best settings possible (quality-wise)
        graphics.TextRenderingHint = _TextRenderingHint;        // System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        graphics.InterpolationMode = GraphicsInterpolationMode; // System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
        graphics.PixelOffsetMode = GraphicsPixelOffsetMode;     // System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
        graphics.SmoothingMode = GraphicsSmoothingMode;         // System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        SizeF textSizeF = graphics.MeasureString(_label, this.Font);
        int width = (int)Math.Ceiling(textSizeF.Width);
        int height = (int)Math.Ceiling(textSizeF.Height);
        Size textSize = new Size(width, height);

        Point position = new Point(this.Left - (textSize.Width - this.Width)/2, this.Bottom);

        Rectangle rectangle = new Rectangle(position, textSize);
        lastLabelRectangle = rectangle;
        graphics.DrawString(_label, this.Font, new SolidBrush(this.ForeColor), rectangle);
    }

    public delegate void SliderMovedEventHandler(object tag, int value);
    public event SliderMovedEventHandler SliderMoved = null;

    public delegate void ValueChangedEventHandler(object tag, int value);
    public event ValueChangedEventHandler ValueChanged = null;

    public Action<string> DebugMessageDelegate;

    private void DebugMessage(string message)
    {
        if (DebugMessageDelegate != null)
            DebugMessageDelegate(message);
    }

    private Orientation _orientation;
    private int _valueMax = 100;
    private int _valueMin = 0;
    private int _value;

    private int _valueSteps = 1;

    public int ValueSteps
    {
        get { return _valueSteps; }

        set { _valueSteps = value; }
    }

    public int ValueStepSize
    {
        get {
            if (Orientation == Orientation.Horizontal)
            {
                if (this.Width > ValueRange)
                    return this.Width / ValueRange;
                else
                    return ValueRange / this.Width;
            }
            else
            {
                if (this.Height > ValueRange)
                    return this.Height/ ValueRange;
                else
                    return ValueRange / this.Height;
            }
        }
    }

    public int ValueRange
    {
        get { return (_valueMax - _valueMin); }
    }

    [Description("The max value"), Category("Data")]
    public int ValueMax
    {
        get { return _valueMax; }
        set
        {
            _valueMax = value;
            Value = _value;
        }
    }
    [Description("the min value"), Category("Data")]
    public int ValueMin
    {
        get { return _valueMin; }
        set
        {
            _valueMin = value;
            Value = _value;
        }
    }

    [Description("Current value"), Category("Data")]
    public int Value
    {
        get { return _value; }
        set
        {
            _value = value;
            if (_value < _valueMin) _value = _valueMin;
            else if (_value > _valueMax) _value = _valueMax;
            SetBarFromValue();
            if (ValueChanged != null) ValueChanged(this.Tag, _value);
        }
    }
    private Color _sliderBarColor;

    [Description("color of slider"), Category("Data")]
    public Color SliderBarColor
    {
        get { return _sliderBarColor; }
        set { _sliderBarColor = value; }
    }

    public Orientation Orientation
    {
        get { return _orientation; }
        set
        {
            _orientation = value;
            SetBarFromValue();
        }
    }

    private void SetBarFromValue()
    {
        if (_orientation == Orientation.Vertical)
            SetBarByPosition(this.Size.Height - ((_value - _valueMin) * this.Size.Height) / ValueRange);
        else if (_orientation == Orientation.Horizontal)
            SetBarByPosition(this.Size.Width - ((_value - _valueMin) * this.Size.Width) / ValueRange);
    }

    private void SetBarByPosition(int rootPos)
    {
        
        if (_orientation == Orientation.Vertical)
        {
            bar.X = 0;
            if (rootPos < 0) rootPos = 0;
            else if (rootPos >= this.Height) rootPos = this.Height;

            bar.Height = this.Height - rootPos;
            bar.Y = rootPos;
            bar.Width = this.Width;
        }
        else if (_orientation == Orientation.Horizontal)
        {
            bar.X = 0;

            if (rootPos < 0) rootPos = 0;
            else if (rootPos >= this.Width) rootPos = this.Width;

            bar.Width = this.Width - rootPos;
            bar.Y = 0;
            bar.Height = this.Height;
        }
        Refresh();
    }


    private void SliderUserMove(int rootPos)
    {
        SetBarByPosition(rootPos);
        // calculate value from slider GUI position
        if (Orientation == Orientation.Vertical)
            _value = _valueMin + (ValueRange * (this.Height - bar.Y)) / this.Height;
        else
            _value = _valueMin + (ValueRange * (this.Width - bar.X)) / this.Width;

        if (setLabelToCurrentValue)
            this.Text = _value.ToString();

        if (ValueChanged != null) ValueChanged(this.Tag, _value);

        RoboRemoPC.Debug.AddLine(this.GetHashCode() + this.Parent.Name + "\n");

        SliderMoved(this.Tag, _value);
    }

    private void this_MouseDown(object sender, MouseEventArgs e)
    {
        if (SliderMoved == null) return; // if the event is not set then this slider is output only
        if (e.Button != MouseButtons.Left) return;
        if (Orientation == Orientation.Vertical)
            SliderUserMove(e.Y);
        else
            SliderUserMove(e.X);
    }

    private void this_MouseMove(object sender, MouseEventArgs e)
    {
        if (SliderMoved == null) return; // if the event is not set then this slider is output only
        if (e.Button != MouseButtons.Left) return;

        if (Orientation == Orientation.Vertical)
            SliderUserMove(e.Y);
        else
            SliderUserMove(e.X);
    }

    private void this_MouseWheel(object sender, MouseEventArgs e)
    {
        if (SliderMoved == null) return; // if the event is not set then this slider is output only
        if (e.Delta == 0) return;

        MouseScroll_ChangeValue(e.Delta);
        SliderMoved(this.Tag, Value);
    }

    private void this_Resize(object sender, EventArgs e)
    {
        SetBarFromValue();
    }

    private void MouseScroll_ChangeValue(int delta)
    {
        if (delta < 0) delta = -1;
        else delta = 1;
        Value = _value + delta * ValueStepSize;
    }

    private void this_KeyUp(object sender, KeyEventArgs e)
    {
        if (SliderMoved == null) return; // if the event is not set then this slider is output only

        if (e.KeyCode == Keys.Up)
            Value = _value + ValueStepSize;
        else if (e.KeyCode == Keys.Down)
            Value = _value - ValueStepSize;
    }

    private void this_MouseEnter(object sender, EventArgs e)
    {
        this.Focus();
    }
}