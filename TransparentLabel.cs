using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;

/// <summary>
/// Use this for drawing custom graphics and text with transparency.
/// Inherit from DrawingArea and override the OnDraw method.
/// </summary>
public class TransparentLabel : Control
{
    private string _text = "TransparentLabel";
    private Point _textPosition = new Point(0, 0);
    private System.Drawing.Text.TextRenderingHint _TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

    [Description("Text to show"), Category("Data")]
    public string TransparentText
    {
        get { return _text; }
        set { _text = value; Refresh(); }
    }

    [Description("Text position"), Category("Data")]
    public Point TextPosition
    {
        get { return _textPosition; }
        set { _textPosition = value; Refresh(); }
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

    public TransparentLabel()
    {
        base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        pevent.Graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, this.Width, this.Height);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        // Update the private member so we can use it in the OnDraw method
        Graphics graphics = e.Graphics;

        // Set the best settings possible (quality-wise)
        graphics.TextRenderingHint = _TextRenderingHint;        // System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        graphics.InterpolationMode = GraphicsInterpolationMode; // System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
        graphics.PixelOffsetMode = GraphicsPixelOffsetMode;     // System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
        graphics.SmoothingMode = GraphicsSmoothingMode;         // System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        
        TransparentLabel.DrawText(graphics, _text, this.Font, new SolidBrush(this.ForeColor), _textPosition);
    }

    static public void DrawText(Graphics graphics, string text, Font font, Brush color, Point position)
    {
        SizeF textSizeF = graphics.MeasureString(text, font);
        int width = (int)Math.Ceiling(textSizeF.Width);
        int height = (int)Math.Ceiling(textSizeF.Height);
        Size textSize = new Size(width, height);
        Rectangle rectangle = new Rectangle(position, textSize);

        graphics.DrawString(text, font, color, rectangle);
    }
}