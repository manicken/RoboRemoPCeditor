/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-08
 * Time: 00:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RoboRemoPC
{
    /// <summary>
    /// Description of CustomDraw.
    /// </summary>
    public class CustomDraw
    {
        public CustomDraw()
        {
        }
        
        public static void DrawRectangle(Graphics g, Rectangle rect, float penWidth, Color penColor, DashStyle penDashStyle)
        {
            using (Pen pen = new Pen(penColor, penWidth))
            {
                pen.DashStyle = penDashStyle;
                float shrinkAmount = pen.Width / 2;
                g.DrawRectangle(
                    pen,
                    rect.X + shrinkAmount,   // move half a pen-width to the right
                    rect.Y + shrinkAmount,   // move half a pen-width to the down
                    rect.Width - penWidth,   // shrink width with one pen-width
                    rect.Height - penWidth); // shrink height with one pen-width
            }
        }
        
        public static Bitmap GetRoundedCornerRectangleGradianted(Color start, Color middle, Color end)
        {
            GraphicsPath gp = new GraphicsPath();
            
            //
            gp.AddArc(0,0, 20, 20, 180, 90);
            //gp.AddLine(10, 0, 90, 0);
            gp.AddArc(89, 0, 20, 20, 270, 90);
            //gp.AddLine(109, 10, 109, 90);
            gp.AddArc(89, 89, 20, 20, 0, 90);
            gp.AddArc(0, 89, 20, 20, 90, 90);
            //gp.AddLine(90, 110, 10, 110);
            //gp.AddLine(10, 110, 10, 10);
            
            Bitmap bm = new Bitmap(110, 110);
            
            LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 0), new Point(100, 0), start, middle);
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.FillPath(brush, gp);
                g.DrawPath(new Pen(Color.Black, 1), gp);
                g.Save();
            }
            /*
            brush = new LinearGradientBrush(new Point(91, 0), new Point(100, 0), middle, end);
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.FillPath(brush, gp);
                //g.DrawPath(new Pen(Color.Black, 1), gp);
                g.Save();
            }*/
            bm.MakeTransparent(bm.GetPixel(0,0));
            return bm;
            //bm.Save(@"c:\bitmap.bmp");
        }
    }
}
