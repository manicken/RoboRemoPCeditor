/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-06
 * Time: 17:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace RoboRemoPC
{
    /// <summary>
    /// Description of UiItem.
    /// </summary>
    public abstract class UiItem_JAVAsrc
    {
        private float h;
        protected bool isEditLocked = false;
        protected bool isInteractive = true;
        //protected VBWin prevWin;
        protected Ui ui;
        private float w;
        private float x;
        private float y;
    
        public abstract String[] getEditOptions();
    
        public abstract void onEditOptionSelected(String str);
    
        public abstract void onPointerDown(float f, float f2);
    
        public abstract void onPointerMoved(float f, float f2, float f3, float f4);
    
        public abstract void onPointerUp(float f, float f2, float f3, float f4);
    
        public abstract void paint(Canvas canvas);
    
        public abstract void start();
    
        public abstract void stop();
    
        public void storeParams(VBDataStore store) {
            store.putFloat("x", getX());
            store.putFloat("y", getY());
            store.putFloat("w", getW());
            store.putFloat("h", getH());
            store.putBoolean("isEditLocked", this.isEditLocked);
        }
    
        public void loadParams(VBDataStore store) {
            setX(store.getFloat("x", 0.0f));
            setY(store.getFloat("y", 0.0f));
            setW(store.getFloat("w", 0.0f));
            setH(store.getFloat("h", 0.0f));
            this.isEditLocked = store.getBoolean("isEditLocked", false);
        }
    
        public float getX() {
            return this.x;
        }
    
        public float getY() {
            return this.y;
        }
    
        public float getW() {
            return this.w;
        }
    
        public float getH() {
            return this.h;
        }
    
        public void setX(float val) {
            this.x = snapToGrid(val);
        }
    
        public void setY(float val) {
            this.y = snapToGrid(val);
        }
    
        public void setW(float val) {
            this.w = snapToGrid(val);
        }
    
        public void setH(float val) {
            this.h = snapToGrid(val);
        }
    
        private float snapToGrid(float in) {
            float result;
            if (in > 0.0f) {
                result = ((float) ((int) (((double) (in / 0.02f)) + 0.5d))) * 0.02f;
            } else {
                result = ((float) ((int) (((double) (in / 0.02f)) - 0.5d))) * 0.02f;
            }
            result *= 1.0E7f;
            if (in > 0.0f) {
                result = (float) ((int) (((double) result) + 0.5d));
            } else {
                result = (float) ((int) (((double) result) - 0.5d));
            }
            return result / 1.0E7f;
        }
    
        public float distTo(float px, float py) {
            px -= this.x + (this.w / 2.0f);
            py -= this.y + (this.h / 2.0f);
            if ((-this.w) / 2.0f > px || px > this.w / 2.0f || (-this.h) / 2.0f > py || py > this.h / 2.0f) {
                if ((-this.w) / 2.0f <= px && px <= this.w / 2.0f) {
                    if (py < 0.0f) {
                        return ((-this.h) / 2.0f) - py;
                    }
                    if (py > 0.0f) {
                        return py - (this.h / 2.0f);
                    }
                }
                if ((-this.h) / 2.0f <= py && py <= this.h / 2.0f) {
                    if (px < 0.0f) {
                        return ((-this.w) / 2.0f) - px;
                    }
                    if (px > 0.0f) {
                        return px - (this.w / 2.0f);
                    }
                }
                return (float) Math.sqrt((double) min(min((((this.w / 2.0f) + px) * ((this.w / 2.0f) + px)) + (((this.h / 2.0f) + py) * ((this.h / 2.0f) + py)), ((px - (this.w / 2.0f)) * (px - (this.w / 2.0f))) + (((this.h / 2.0f) + py) * ((this.h / 2.0f) + py))), min(((px - (this.w / 2.0f)) * (px - (this.w / 2.0f))) + ((py - (this.h / 2.0f)) * (py - (this.h / 2.0f))), (((this.w / 2.0f) + px) * ((this.w / 2.0f) + px)) + ((py - (this.h / 2.0f)) * (py - (this.h / 2.0f))))));
            }
            return -min(min((this.w / 2.0f) - px, px + (this.w / 2.0f)), min((this.h / 2.0f) - py, py + (this.h / 2.0f)));
        }
    
        public bool containsPoint(float x, float y) {
            if (x >= this.x && y >= this.y && x <= this.x + this.w && y <= this.y + this.h) {
                return true;
            }
            return false;
        }
    
        private float min(float a, float b) {
            return a < b ? a : b;
        }
    
        public void openEditOptions() {
            this.prevWin = MainActivity.win;
            final UiItem item = this;
            String title = BuildConfig.VERSION_NAME;
            if (item instanceof UiButton) {
                title = "button";
            }
            if (item instanceof UiSlider) {
                title = "slider";
            }
            if (item instanceof UiLed) {
                title = "led";
            }
            if (item instanceof UiIndicator) {
                title = "level indicator";
            }
            if (item instanceof UiTextLog) {
                title = "text log";
            }
            if (item instanceof UiAcc) {
                title = "accelerometer";
            }
            if (item instanceof UiTextField) {
                title = "text field";
            }
            if (item instanceof UiPlot) {
                title = "plot";
            }
            if (item instanceof UiImage) {
                title = "image";
            }
            if (item instanceof UiTouchPad) {
                title = "touchpad";
            }
            if (item instanceof UiKbdConnector) {
                title = "kbd connector";
            }
            if (item instanceof UiHeartbeatSender) {
                title = "heartbeat sender";
            }
            if (item instanceof UiTouchStopper) {
                title = "touch stopper";
            }
            if (item instanceof UiVibrator) {
                title = "vibrator";
            }
            if (item instanceof UiFileReceiver) {
                title = "file receiver";
            }
            if (item instanceof UiFileSender) {
                title = "file sender";
            }
            if (item instanceof UiSpeaker) {
                title = "speaker";
            }
            if (item instanceof UiPrintf) {
                title = "printf()";
            }
            if (item instanceof UiInputParser) {
                title = "input parser";
            }
            new VBMenu(title, getItemEditOptions()) {
                public void onSelect(String st, int i) {
                    if (st.equals("remove")) {
                        UiItem.this.stop();
                        ((UiEditor) UiItem.this.prevWin).ui.remove(item);
                        UiItem.this.ui.updateProcessingItems();
                        UiItem.this.prevWin.show();
                    } else if (st.equals("copy")) {
                        ((UiEditor) UiItem.this.prevWin).itemToCopy = item;
                        UiItem.this.prevWin.show();
                    } else if (st.equals("lock edit")) {
                        item.isEditLocked = true;
                        UiItem.this.prevWin.show();
                    } else {
                        UiItem.this.onEditOptionSelected(st);
                        UiItem.this.prevWin.show();
                    }
                }
            }.show();
        }
    
        public void onChanged() {
            MainActivity.win.repaint();
        }
    
        private String[] getItemEditOptions() {
            int i;
            String[] st = getEditOptions();
            String[] common = new String[]{"copy", "remove", "lock edit"};
            String[] res = new String[(common.length + st.length)];
            for (i = 0; i < common.length; i++) {
                res[i] = common[i];
            }
            for (i = common.length; i < res.length; i++) {
                res[i] = st[i - common.length];
            }
            return res;
        }
    
        public void receiveData(byte[] data, int len, bool moreToCome) {
        }
    
        public UiItem clone() {
            try {
                Class<?> c = getClass();
                String st = toString();
                return (UiItem) c.getDeclaredMethod("fromString", new Class[]{String.class}).invoke(null, new Object[]{st});
            } catch (Exception e) {
                return null;
            }
        }
    
        public void paintMoveResizeHint(Canvas cnv, int color) {
            String moveText;
            String configText;
            String resizeText;
            int configX;
            Ui.paint.setStyle(Style.STROKE);
            Ui.paint.setStrokeWidth(0.0f);
            Ui.paint.setColor(color);
            Ui.setTextSize(20.0f);
            int x = (int) (getX() * ((float) VBWin.w));
            int y = (int) (getY() * ((float) VBWin.h));
            int w = (int) (getW() * ((float) VBWin.w));
            int h = (int) (getH() * ((float) VBWin.h));
            if (x < VBWin.w / 2) {
                Ui.paint.setTextAlign(Align.LEFT);
                moveText = "\u2199 Drag to move";
                configText = "\u2190 Click to config.";
                resizeText = "\u2196 Drag to resize";
                configX = x + w;
            } else {
                Ui.paint.setTextAlign(Align.RIGHT);
                moveText = "Drag to move \u2198";
                configText = "Click to config. \u2192";
                resizeText = "Drag to resize \u2197";
                configX = x;
            }
            cnv.drawText(moveText, (float) x, (float) ((Ui.fontAscent / 2) + y), Ui.paint);
            cnv.drawText(configText, (float) configX, (float) ((h / 2) + y), Ui.paint);
            cnv.drawText(resizeText, (float) (x + w), (float) ((y + h) - Ui.fontAscent), Ui.paint);
            Ui.setDefaultTextSize();
        }
    
        protected void send(String st) {
            if (this.ui != null) {
                this.ui.send(st);
            }
        }
    
        protected void send(String[] st) {
            if (this.ui != null) {
                this.ui.send(st);
            }
        }
    
        protected void sendWithoutCmdEnding(String st) {
            if (this.ui != null) {
                this.ui.sendWithoutCmdEnding(st);
            }
        }
    
        protected void sendBytes(byte[] ba, int offset, int len) {
            if (this.ui != null) {
                this.ui.sendBytes(ba, offset, len);
            }
        }
    
        protected void updateFromVersion(String oldVersion) {
        }
    }
}
