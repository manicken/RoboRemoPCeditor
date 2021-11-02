/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-06
 * Time: 17:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace RoboRemoPC
{
    /// <summary>
    /// Description of VBWin.
    /// </summary>
    public abstract class VBWin_JAVAsrc
    {
        public static int bgColor;
        public static VBColorTheme colorTheme;
        private static String debugString = BuildConfig.VERSION_NAME;
        public static UiTextLog debugTextLog = null;
        public static float density;
        public static int h;
        public static MainActivity mainActivity = null;
        public static int masterColor;
        protected static Paint p = null;
        public static float scaledPixelDensity;
        public static int w;
        protected Context ct;
    
        public abstract void backPressed();
    
        public abstract void paint(Canvas canvas);
    
        public VBWin() {
            super(mainActivity);
            LayoutParams params = new LayoutParams(-1, -1);
            params.weight = 0.0f;
            setLayoutParams(params);
            setBackgroundColor(colorTheme.bgColor);
            setOrientation(1);
            setWillNotDraw(false);
            postInvalidate();
            this.ct = mainActivity;
        }
    
        public void onTick() {
        }
    
        public void pointerPressed(int x, int y) {
        }
    
        public void pointerReleased(int x, int y) {
        }
    
        public void pointerDragged(int x, int y) {
        }
    
        public boolean onTouchEvent(MotionEvent event) {
            int action = event.getActionMasked();
            int x = (int) event.getX();
            int y = (int) event.getY();
            if (action == 0) {
                pointerPressed(x, y);
            }
            if (action == 2) {
                pointerDragged(x, y);
            }
            if (action == 1) {
                pointerReleased(x, y);
            }
            return true;
        }
    
        public void onDraw(Canvas cnv) {
            paint(cnv);
        }
    
        public void show() {
            MainActivity.win = this;
            repaint();
        }
    
        public void repaint() {
            mainActivity.repaint();
        }
    
        protected static int mcol(int br) {
            int col = masterColor;
            int red = (((col >> 16) & FT_4222_Defines.CHIPTOP_DEBUG_REQUEST) * br) / FT_4222_Defines.CHIPTOP_DEBUG_REQUEST;
            int green = (((col >> 8) & FT_4222_Defines.CHIPTOP_DEBUG_REQUEST) * br) / FT_4222_Defines.CHIPTOP_DEBUG_REQUEST;
            int blue = ((col & FT_4222_Defines.CHIPTOP_DEBUG_REQUEST) * br) / FT_4222_Defines.CHIPTOP_DEBUG_REQUEST;
            if (red > FT_4222_Defines.CHIPTOP_DEBUG_REQUEST) {
                red = FT_4222_Defines.CHIPTOP_DEBUG_REQUEST;
            }
            if (green > FT_4222_Defines.CHIPTOP_DEBUG_REQUEST) {
                green = FT_4222_Defines.CHIPTOP_DEBUG_REQUEST;
            }
            if (blue > FT_4222_Defines.CHIPTOP_DEBUG_REQUEST) {
                blue = FT_4222_Defines.CHIPTOP_DEBUG_REQUEST;
            }
            return (((((FT_4222_Defines.CHIPTOP_DEBUG_REQUEST << 8) + (red & FT_4222_Defines.CHIPTOP_DEBUG_REQUEST)) << 8) + (green & FT_4222_Defines.CHIPTOP_DEBUG_REQUEST)) << 8) + (blue & FT_4222_Defines.CHIPTOP_DEBUG_REQUEST);
        }
    
        public static int max(int a, int b) {
            return a > b ? a : b;
        }
    
        public static int min(int a, int b) {
            return a < b ? a : b;
        }
    
        public static int textW(String text) {
            Rect bounds = new Rect();
            p.getTextBounds(text, 0, text.length(), bounds);
            return bounds.width();
        }
    
        public static void debug(String st) {
            if (debugTextLog != null) {
                debugTextLog.append("\n" + st, true);
                debugTextLog.onChanged();
            }
        }
    
        public static String getDebugString() {
            return debugString;
        }
    }
}
