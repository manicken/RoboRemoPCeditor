/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-08
 * Time: 18:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace RoboRemoPC
{
    /// <summary>
    /// Description of Test_Button_Region.
    /// </summary>
    public class Test_Button_Region
    {
        public Test_Button_Region()
        {
        }
        private bool Button1_MouseDown = false;
        void button1_Paint(object sender, PaintEventArgs e)
        {
            
            GraphicsPath myGraphicsPath = new GraphicsPath();

            Point[] myPointArray = {
                    new Point(5, 30), 
                    new Point(20, 40), 
                    new Point(50, 30)};
    
            FontFamily myFontFamily = new FontFamily("Courier New");
            
            PointF myPointF = new PointF(20, 20);
            StringFormat myStringFormat = new StringFormat();
            
            myGraphicsPath.AddArc(0, 0, 30, 20, -90, 180);
            myGraphicsPath.StartFigure();
            myGraphicsPath.AddCurve(myPointArray);
            myGraphicsPath.AddString("button", myFontFamily, (int)FontStyle.Bold, 32, myPointF, myStringFormat);
            myGraphicsPath.AddPie(button1.Width - 40, 10, 40, 40, 40, 110);
            Region newRegion = new System.Drawing.Region(myGraphicsPath);
            
            if (Button1_MouseDown)
                newRegion.Translate(2.0f, 2.0f);
            
            button1.Region = newRegion;
            
        }
        void button1_MouseDown(object sender, MouseEventArgs e)
        {
            Button1_MouseDown = true;
        }
        void button1_MouseUp(object sender, MouseEventArgs e)
        {
            Button1_MouseDown = false;
        }
    }
}
