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
    /// Description of Ui.
    /// </summary>
    public class Ui : VBWin_JAVAsrc
    {
        static int defaultTextSize;
        public static int fontAscent;
        public static int fontH;
        public static double gridSize = 0.1d;
        public static Paint paint;
        public static int textOffsetY;
        public String bgColor;
        public int charDelay;
        public String charDelayString;
        public String commandEnding;
        public String connectAction;
        public Vector<UiItem> items;
        public String name;
        private Vector pointers;
        public RelativeLayout rell = new RelativeLayout(VBWin.mainActivity);
        private String version;
    
        public Ui(Vector items) {
            addView(this.rell);
            this.name = BuildConfig.VERSION_NAME;
            this.connectAction = BuildConfig.VERSION_NAME;
            this.charDelayString = BuildConfig.VERSION_NAME;
            this.bgColor = "FFFFFFFF";
            onBgColorChanged();
            this.version = VBWin.mainActivity.getVersionName();
            this.charDelay = MainActivity.charDelay;
            this.commandEnding = "\n";
            this.pointers = new Vector();
            paint = new Paint();
            paint.setColor(-16777216);
            paint.setTextAlign(Align.CENTER);
            paint.setStrokeCap(Cap.ROUND);
            paint.setAntiAlias(true);
            setDefaultTextSize();
            if (items == null) {
                this.items = new Vector();
                UiButton menuButton = new UiButton(0.02f, 0.02f, 0.2f, 0.15f, "menu");
                menuButton.local = true;
                menuButton.releaseAction = "menu";
                this.items.add(menuButton);
            } else {
                this.items = items;
            }
            setFocusable(true);
            setFocusableInTouchMode(true);
        }
    
        public static void setTextSize(float textSize) {
            paint.setTextSize(VBWin.scaledPixelDensity * textSize);
            textOffsetY = (-((int) (paint.getFontMetrics().ascent + paint.getFontMetrics().descent))) / 2;
            fontAscent = (int) paint.getFontMetrics().ascent;
            fontH = (int) (paint.descent() - paint.ascent());
        }
    
        public static float getDefaultTextSize() {
            return 15.0f;
        }
    
        public static void setDefaultTextSize() {
            setTextSize(getDefaultTextSize());
        }
    
        public void clear() {
            this.items = new Vector();
            UiButton menuButton = new UiButton(0.02f, 0.02f, 0.2f, 0.15f, "menu");
            menuButton.local = true;
            menuButton.releaseAction = "menu";
            this.items.add(menuButton);
            this.commandEnding = "\n";
            this.connectAction = BuildConfig.VERSION_NAME;
            this.name = BuildConfig.VERSION_NAME;
            this.bgColor = "FFFFFFFF";
            onBgColorChanged();
        }
    
        public void onBgColorChanged() {
            setBackgroundColor((int) (Long.parseLong(this.bgColor, 16) & -1));
        }
    
        public void paint(Canvas cnv) {
            int itemCount = this.items.size();
            if (MainActivity.win == this && itemCount == 1) {
                drawHintText(cnv, (((BuildConfig.VERSION_NAME + "Welcome to " + MainActivity.appName + "\n\n") + "To start, click\n") + "menu > edit ui, or\n") + "menu > interface > import", w / 2, h / 2, -8355712);
            }
            for (int i = 0; i < itemCount; i++) {
                ((UiItem) this.items.elementAt(i)).paint(cnv);
            }
            if (itemCount == 2 && (MainActivity.win instanceof UiEditor)) {
                ((UiItem) this.items.elementAt(1)).paintMoveResizeHint(cnv, -1);
            }
        }
    
        public void stop() {
            int itemCount = this.items.size();
            for (int i = 0; i < itemCount; i++) {
                ((UiItem) this.items.elementAt(i)).stop();
            }
            MainActivity mainActivity = VBWin.mainActivity;
            MainActivity.saveInterface();
            VBAcc.stop();
        }
    
        public void start() {
            int i;
            onCharDelayStringChanged();
            int itemCount = this.items.size();
            String currentVersion = VBWin.mainActivity.getVersionName();
            if (!this.version.equals(currentVersion)) {
                for (i = 0; i < itemCount; i++) {
                    ((UiItem) this.items.elementAt(i)).updateFromVersion(this.version);
                }
                this.version = currentVersion;
            }
            for (i = 0; i < itemCount; i++) {
                ((UiItem) this.items.elementAt(i)).start();
            }
            if (this.commandEnding.length() == 0) {
                VBWin.mainActivity.showAlertCommandEndingEmptyString();
            }
        }
    
        public void updateProcessingItems() {
            int itemCount = this.items.size();
            for (int i = 0; i < itemCount; i++) {
                UiItem item = (UiItem) this.items.elementAt(i);
                if (item instanceof UiProcessingItem) {
                    ((UiProcessingItem) item).updateAction();
                }
            }
        }
    
        public void backPressed() {
            if (!MainActivity.backDisabledWhenConnected || MainActivity.con == null) {
                VBWin.mainActivity.finish();
            } else {
                VBWin.mainActivity.makeToast("first disconnect");
            }
        }
    
        public static void paintFill(boolean fill) {
            if (fill) {
                paint.setStyle(Style.FILL);
            } else {
                paint.setStyle(Style.STROKE);
            }
        }
    
        public boolean onTouchEvent(MotionEvent event) {
            int pointerCount;
            float minDist;
            int i;
            float dist;
            int actionMasked = event.getActionMasked();
            if (actionMasked == 0) {
                float x = event.getX() / ((float) w);
                float y = event.getY() / ((float) h);
                VBPointer p = new VBPointer();
                p.x0 = x;
                p.x = x;
                p.y0 = y;
                p.y = y;
                this.pointers.add(p);
                onPointerDown(x, y);
            }
            if (actionMasked == 5) {
                int index = event.getActionIndex();
                x = event.getX(index) / ((float) w);
                y = event.getY(index) / ((float) h);
                p = new VBPointer();
                p.x0 = x;
                p.x = x;
                p.y0 = y;
                p.y = y;
                this.pointers.add(p);
                onPointerDown(x, y);
            }
            if (actionMasked == 1) {
                x = event.getX() / ((float) w);
                y = event.getY() / ((float) h);
                pointerCount = this.pointers.size();
                if (pointerCount != 1) {
                    VBWin.debug("err: ACTION_UP with pointerCount = " + pointerCount);
                }
                if (pointerCount > 0) {
                    p = (VBPointer) this.pointers.elementAt(0);
                    p.x = x;
                    p.y = y;
                    onPointerUp(p.x, p.y, p.x0, p.y0);
                    this.pointers.remove(p);
                }
            }
            if (actionMasked == 6) {
                index = event.getActionIndex();
                x = event.getX(index) / ((float) w);
                y = event.getY(index) / ((float) h);
                pointerCount = this.pointers.size();
                minDist = 1000000.0f;
                for (i = 0; i < pointerCount; i++) {
                    p = (VBPointer) this.pointers.elementAt(i);
                    dist = ((p.x - x) * (p.x - x)) + ((p.y - y) * (p.y - y));
                    if (dist < minDist) {
                        minDist = dist;
                    }
                }
                for (i = 0; i < pointerCount; i++) {
                    p = (VBPointer) this.pointers.elementAt(i);
                    if (((p.x - x) * (p.x - x)) + ((p.y - y) * (p.y - y)) == minDist) {
                        p.x = x;
                        p.y = y;
                        onPointerUp(p.x, p.y, p.x0, p.y0);
                        this.pointers.remove(p);
                        break;
                    }
                }
            }
            if (actionMasked == 2) {
                int eventPointerCount = event.getPointerCount();
                for (int j = 0; j < eventPointerCount; j++) {
                    x = event.getX(j) / ((float) w);
                    y = event.getY(j) / ((float) h);
                    pointerCount = this.pointers.size();
                    minDist = 1000000.0f;
                    for (i = 0; i < pointerCount; i++) {
                        p = (VBPointer) this.pointers.elementAt(i);
                        dist = ((p.x - x) * (p.x - x)) + ((p.y - y) * (p.y - y));
                        if (dist < minDist) {
                            minDist = dist;
                        }
                    }
                    for (i = 0; i < pointerCount; i++) {
                        p = (VBPointer) this.pointers.elementAt(i);
                        if (((p.x - x) * (p.x - x)) + ((p.y - y) * (p.y - y)) == minDist) {
                            p.x = x;
                            p.y = y;
                            onPointerMoved(p.x, p.y, p.x0, p.y0);
                            break;
                        }
                    }
                }
            }
            return true;
        }
    
        public void onPointerDown(float x, float y) {
            if (this.items != null) {
                UiItem item = getItem(x, y);
                if (item != null) {
                    item.onPointerDown(x, y);
                }
                repaint();
            }
        }
    
        public void onPointerMoved(float x, float y, float x0, float y0) {
            if (this.items != null) {
                UiItem item = getItem(x0, y0);
                if (item != null) {
                    item.onPointerMoved(x, y, x0, y0);
                }
                repaint();
            }
        }
    
        public void onPointerUp(float x, float y, float x0, float y0) {
            if (this.items != null) {
                UiItem item = getItem(x0, y0);
                if (item != null) {
                    item.onPointerUp(x, y, x0, y0);
                }
                repaint();
            }
        }
    
        private UiItem getItem(float x, float y) {
            if (this.items == null) {
                return null;
            }
            int itemCount = this.items.size();
            if (itemCount == 0) {
                return null;
            }
            int i;
            for (i = 0; i < itemCount; i++) {
                UiItem item = (UiItem) this.items.elementAt(i);
                if (item.isInteractive && item.containsPoint(x, y)) {
                    return item;
                }
            }
            if (!false) {
                float minDist = 10000.0f;
                for (i = 0; i < itemCount; i++) {
                    item = (UiItem) this.items.elementAt(i);
                    if (item.isInteractive) {
                        float dist = item.distTo(x, y);
                        if (dist < minDist) {
                            minDist = dist;
                        }
                    }
                }
                for (i = 0; i < itemCount; i++) {
                    item = (UiItem) this.items.elementAt(i);
                    if (item.isInteractive && item.distTo(x, y) == minDist) {
                        return item;
                    }
                }
            }
            return null;
        }
    
        public String toString() {
            VBDataStore store = new VBDataStore();
            int itemCount = this.items.size();
            store.putInt("itemCount", itemCount);
            store.putString("name", this.name);
            store.putString("commandEnding", this.commandEnding);
            store.putString("connectAction", this.connectAction);
            store.putString("charDelay", this.charDelayString);
            store.putString("bgColor", this.bgColor);
            store.putString("version", this.version);
            for (int i = 0; i < itemCount; i++) {
                store.putUiItem(BuildConfig.VERSION_NAME + i, (UiItem) this.items.elementAt(i));
            }
            return store.toString();
        }
    
        public static Ui fromString(String st) {
            Ui ui = new Ui(null);
            ui.items = new Vector();
            VBDataStore store = VBDataStore.fromString(st);
            int itemCount = store.getInt("itemCount", 0);
            for (int i = 0; i < itemCount; i++) {
                UiItem item = store.getUiItem(BuildConfig.VERSION_NAME + i, null);
                if (item != null) {
                    ui.addItem(item);
                } else {
                    Log.d("Ui fromString err", "item==null");
                }
            }
            ui.name = store.getString("name", BuildConfig.VERSION_NAME);
            ui.commandEnding = store.getString("commandEnding", "\n");
            ui.connectAction = store.getString("connectAction", BuildConfig.VERSION_NAME);
            ui.charDelayString = store.getString("charDelay", ui.charDelayString);
            ui.bgColor = store.getString("bgColor", ui.bgColor);
            ui.onBgColorChanged();
            ui.version = store.getString("version", BuildConfig.VERSION_NAME);
            return ui;
        }
    
        public boolean onCharDelayStringChanged() {
            if (this.charDelayString.length() == 0) {
                this.charDelay = MainActivity.charDelay;
                return true;
            }
            try {
                this.charDelay = Integer.parseInt(this.charDelayString);
                if (this.charDelay >= 0) {
                    return true;
                }
                VBWin.mainActivity.showError("charDelay error", "value can not be negative.");
                return false;
            } catch (Exception e) {
                VBWin.mainActivity.showError("charDelay parse error", e.toString());
                return false;
            }
        }
    
        public void addItem(UiItem item) {
            this.items.add(item);
            item.ui = this;
        }
    
        public void addItem(int index, UiItem item) {
            this.items.add(index, item);
            item.ui = this;
        }
    
        public boolean hasNoItems() {
            if (this.items == null || this.items.size() == 0) {
                return true;
            }
            return false;
        }
    
        public int getItemCount() {
            if (this.items == null) {
                return 0;
            }
            return this.items.size();
        }
    
        public UiItem getItem(int index) {
            return (UiItem) this.items.elementAt(index);
        }
    
        public void remove(UiItem item) {
            this.items.remove(item);
        }
    
        public void send(String st) {
            VBConnection con = MainActivity.con;
            if (con != null && con.ready) {
                con.charDelay = this.charDelay;
                con.send(st + this.commandEnding);
            }
        }
    
        public void send(String[] st) {
            VBConnection con = MainActivity.con;
            if (con != null && con.ready) {
                String s = BuildConfig.VERSION_NAME;
                for (int i = 0; i < st.length; i++) {
                    if (st[i].length() > 0) {
                        s = s + st[i] + this.commandEnding;
                    }
                }
                con.charDelay = this.charDelay;
                con.send(s);
            }
        }
    
        public void sendBytes(byte[] ba, int offset, int len) {
            VBConnection con = MainActivity.con;
            if (con != null && con.ready) {
                con.charDelay = this.charDelay;
                con.sendBytes(ba, offset, len);
            }
        }
    
        public void sendWithoutCmdEnding(String st) {
            VBConnection con = MainActivity.con;
            if (con != null && con.ready) {
                con.charDelay = this.charDelay;
                con.send(st);
            }
        }
    
        public Ui clone() {
            try {
                Class<?> c = getClass();
                String st = toString();
                return (Ui) c.getDeclaredMethod("fromString", new Class[]{String.class}).invoke(null, new Object[]{st});
            } catch (Exception e) {
                return null;
            }
        }
    
        public void unlockEditAll() {
            Iterator i$ = this.items.iterator();
            while (i$.hasNext()) {
                ((UiItem) i$.next()).isEditLocked = false;
            }
        }
    
        public static void drawHintText(Canvas cnv, String text, int x, int y, int color) {
            paint.setStyle(Style.STROKE);
            paint.setTextAlign(Align.CENTER);
            paint.setStrokeWidth(0.0f);
            paint.setColor(color);
            setTextSize(22.0f);
            String[] textRows = text.split("\n");
            int n = textRows.length;
            int x0 = w / 2;
            int y0 = (int) (((float) (h / 2)) - ((((float) (fontH * n)) + 0.5f) / 2.0f));
            for (int i = 0; i < n; i++) {
                cnv.drawText(textRows[i], (float) x0, (float) ((fontH * i) + y0), paint);
            }
            setDefaultTextSize();
        }
    }
}
