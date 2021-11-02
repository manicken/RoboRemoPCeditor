using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsan;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace RoboRemoPC
{
    public class UiItem
    {
        public string Type = "";

        public VBDataStore vbds;

        public static Bitmap Img_UiTouchStopper_Vertical;
        public static Bitmap Img_UiTouchStopper_Horizontal;

        public Control control = null;

        public UiItem(TypeValuePair tvp)
        {
            Type = tvp.Type;
            vbds = new VBDataStore(tvp.Value);
        }
        public UiItem(string type, string str)
        {
            Type = type;
            vbds = new VBDataStore(str);
        }

        public Control CreateNewUiControl(IDesignerHost idh)//, Control container)
        {
            if (Type == "UiButton")
                control = (Button)idh.CreateComponent(typeof(Button));// new Button();
            else if (Type == "UiSlider" || Type == "UiIndicator")
            {
                control = (CustomSlider2)idh.CreateComponent(typeof(CustomSlider2));//new CustomSlider();
                string ori = vbds.getString("ori", "v");
                if (ori == "h")
                    ((CustomSlider2)control).Orientation = Orientation.Horizontal;
                else
                    ((CustomSlider2)control).Orientation = Orientation.Vertical;
                ((CustomSlider2)control).isHandleVisible = (Type == "UiSlider");
            }
            else if (Type == "UiTextField")
            {
                control = (TransparentLabel)idh.CreateComponent(typeof(TransparentLabel));//new TextBox();

                ((TransparentLabel)control).TextPosition = new Point(6, 3);

            }
            else if (Type == "UiTextLog")
            {
                control = (TextBox)idh.CreateComponent(typeof(TextBox));//new TextBox();
                ((TextBox)control).Multiline = true;
                ((TextBox)control).ReadOnly = true;
                ((TextBox)control).BackColor = System.Drawing.Color.FromName("LightSkyBlue");
            }
            else if (Type == "UiTouchStopper")
            {
                control = (Panel)idh.CreateComponent(typeof(Panel));//new Panel();
                ((Panel)control).BackgroundImageLayout = ImageLayout.Tile;
                ((Panel)control).BorderStyle = BorderStyle.None;
                ((Panel)control).Paint += touchStopper_Paint;
            }
            else
            {
                control = (Button)idh.CreateComponent(typeof(Button));//new Button();
                ((Button)control).FlatStyle = FlatStyle.Flat;

            }
            control.Margin = new Padding(0);
            UserControl uc = (UserControl)idh.RootComponent;
            UpdateUiControl(uc);
            uc.Controls.Add(control);
            control.Tag = this;
            return control;
        }

        //TODO: seperate UpdateOrGetNew_Control into two functions 
        //GetNew and Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container">the container of the ui item</param>
        public void UpdateUiControl(Control container)
        {
            if (control != null)
            {
                control.Left = (int)(vbds.getFloat("x", 0.0f) * ((float)container.Width));
                control.Top = (int)(vbds.getFloat("y", 0.0f) * ((float)container.Height));
                control.Width = (int)(vbds.getFloat("w", 0.0f) * ((float)container.Width));
                control.Height = (int)(vbds.getFloat("h", 0.0f) * ((float)container.Height));
            }
            if (Type == "UiButton")
            {
                Button btn = (Button)control;

                btn.BackColor = System.Drawing.Color.LightGray;
                btn.Text = vbds.getString("text", "button");
            }
            else if (Type == "UiSlider" || Type == "UiIndicator")
            {
                CustomSlider2 slider = (CustomSlider2)control;

                if (Type == "UiSlider")
                {
                    slider.ValueMin = vbds.getInt("minVal", 0);
                    slider.ValueMax = vbds.getInt("maxVal", 0);
                    slider.Value = vbds.getInt("val", 0);
                    slider.Text = vbds.getString("label", "");
                }
                else if (Type == "UiIndicator")
                {
                    slider.ValueMin = Convert.ToInt32(vbds.getFloat("minVal", 0.0f));
                    slider.ValueMax = Convert.ToInt32(vbds.getFloat("maxVal", 0.0f));
                    slider.Value = Convert.ToInt32(vbds.getFloat("val", 0.0f));
                }

                /*
                    */
                if (slider.Height > slider.Width)
                {
                    slider.Orientation = Orientation.Vertical;
                    vbds.putString("ori", "v");
                }
                else
                {
                    slider.Orientation = Orientation.Horizontal;
                    vbds.putString("ori", "h");
                }

                slider.BackColor = System.Drawing.Color.Gray;
                string color = vbds.getString("color", "y");
                if (color == "r")
                    slider.SliderBarColor = System.Drawing.Color.Red;
                else if (color == "g")
                    slider.SliderBarColor = System.Drawing.Color.Green;
                else if (color == "b")
                    slider.SliderBarColor = System.Drawing.Color.Blue;
                else if (color == "y")
                    slider.SliderBarColor = System.Drawing.Color.FromName("Gold");
            }
            else if (Type == "UiTextField")
            {
                TransparentLabel txt = (TransparentLabel)control;

                txt.TransparentText = vbds.getString("text", "TextField");
                int bgColor = int.Parse(vbds.getString("bgColor", "FFFFFFFF"), System.Globalization.NumberStyles.HexNumber);
                txt.BackColor = System.Drawing.Color.FromArgb(bgColor);
                int textColor = int.Parse(vbds.getString("textColor", "FF000000"), System.Globalization.NumberStyles.HexNumber);
                txt.ForeColor = System.Drawing.Color.FromArgb(textColor);
            }
            else if (Type == "UiTextLog")
            {
                ((TextBox)control).Text = vbds.getString("text", "TextLog");
            }
            else if (Type == "UiTouchStopper")
            {
                if (control.Height > control.Width)
                    control.BackgroundImage = Img_UiTouchStopper_Vertical;
                else
                    control.BackgroundImage = Img_UiTouchStopper_Horizontal;
            }
            else
            {
                Button btn = (Button)control;
                
                if (Type == "UiLed")
                {
                    string color = vbds.getString("color", "y");
                    bool onState = vbds.getBoolean("on", false);
                    if (color == "r")
                    {
                        if (onState) btn.BackColor = System.Drawing.Color.FromArgb(255, 89, 89);
                        else btn.BackColor = System.Drawing.Color.FromArgb(106, 0, 0);
                    }
                    else if (color == "g")
                    {
                        if (onState) btn.BackColor = System.Drawing.Color.FromArgb(18, 255, 18);
                        else btn.BackColor = System.Drawing.Color.FromArgb(0, 105, 0);
                    }
                    else if (color == "b")
                    {
                        if (onState) btn.BackColor = System.Drawing.Color.FromArgb(89, 89, 255);
                        else btn.BackColor = System.Drawing.Color.FromArgb(0, 0, 106);
                    }
                    else if (color == "y")
                    {
                        if (onState) btn.BackColor = System.Drawing.Color.FromArgb(255, 252, 19);
                        else btn.BackColor = System.Drawing.Color.FromArgb(106, 105, 0);
                    }
                }
                else
                    btn.BackColor = System.Drawing.Color.LightGray;

                btn.Text = vbds.getString("text", Type);
            }
            
        }

        void touchStopper_Paint(object s, PaintEventArgs e)
        {
            CustomDraw.DrawRectangle(e.Graphics, ((Panel)s).ClientRectangle, 2.0f, Color.Red, DashStyle.Solid);
            /*Panel panel = (Panel)s;
            Rectangle rect = panel.ClientRectangle;
            //rect.Inflate(-1, -1);
            Pen pen = new Pen(Color.Red, 2);
            pen.DashStyle = DashStyle.Dot;
            pen.Alignment = PenAlignment.Inset; //<-- this
            e.Graphics.DrawRectangle(pen, rect);*/
        }

        public void setX(float val)
        {
            vbds.putFloat("x", snapToGrid(val, 0.02f));
        }

        public void setY(float val)
        {
            vbds.putFloat("y", snapToGrid(val, 0.02f));
        }

        public void setW(float val)
        {
            val = snapToGrid(val, 0.01f);
            if (val == 0.0f) val = 0.01f;
            vbds.putFloat("w", val);
        }

        public void setH(float val)
        {
            val = snapToGrid(val, 0.02f);
            if (val == 0.0f) val = 0.02f;
            vbds.putFloat("h", val);
        }

        private float snapToGrid(float val, float gridSize)
        {
            float result;
            if (val > 0.0f)
            {
                result = ((float)((int)(((double)(val / gridSize)) + 0.5d))) * gridSize;
            }
            else
            {
                result = ((float)((int)(((double)(val / gridSize)) - 0.5d))) * gridSize;
            }
            result *= 1.0E7f;
            if (val > 0.0f)
            {
                result = (float)((int)(((double)result) + 0.5d));
            }
            else
            {
                result = (float)((int)(((double)result) - 0.5d));
            }
            return result / 1.0E7f;
        }
    }
}
