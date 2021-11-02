/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-06
 * Time: 13:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;

using System.Reflection;
using System.Resources;

using System.ComponentModel.Design;
using System.ComponentModel;
using System.Collections;

namespace RoboRemoPC
{
    
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private VBDataStore vbdsUi;

        private VBDataStore vbdsUiItemsRef;

        public List<UiItem> uiItems;
        
        private OpenFileDialog ofd;
        private SaveFileDialog sfd;

        private string currentFilePath = "";
        //private int selectedUiItemIndex = 0;
        
        private UiItem currentSelectedUiItem;
        private Control[] currentSelection = new Control[0];

        private Control[] currentSelectionCopyRef = new Control[0];

        //private Bitmap bm;

        public UiItem uiItemCopyRef = null;

        public UiItem[] uiItemsCopyRef = new UiItem[0];

        //private Point panelMousePoint;

        private DesignSurface ds;
        private IDesignerHost idh;
        private ISelectionService selectionService;
        private UserControl designRoot;
        private Point lastCmsLocation;

        public MainForm()
        {
            InitializeComponent();

            ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath + "\\gui_files";
            ofd.Filter = "All files|*.*";
            sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath + "\\gui_files";
            sfd.Filter = "All files|*.*";

            ResourceManager resources = new ResourceManager("RoboRemoPC.Resources", Assembly.GetExecutingAssembly());
            
            UiItem.Img_UiTouchStopper_Horizontal = (Bitmap) resources.GetObject("stoptape"); //image without extension
            UiItem.Img_UiTouchStopper_Vertical = (Bitmap) resources.GetObject("stoptape"); //image without extension
            UiItem.Img_UiTouchStopper_Vertical.RotateFlip(RotateFlipType.Rotate90FlipNone);

            uiItems = new List<UiItem>();

            Init_DesignSurface();
            //customSlider22.Init();
            customSlider22.SliderMoved += CustomSlider22_SliderMoved;
            customSlider22.ValueChanged += CustomSlider22_ValueChanged;
        }

        private void CustomSlider22_ValueChanged(object tag, int value)
        {
            //Debug.AddLine("CustomSlider_ValueChanged: " + value);
        }

        private void CustomSlider22_SliderMoved(object tag, int value)
        {
            //Debug.AddLine("CustomSlider_SliderMoved: " + value);
        }

        /* TODO: fix keyboard commands
private void PanelUiView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
   if (e.Control && e.KeyCode == Keys.V)
       PasteUiItem();
   else if (e.KeyCode == Keys.Delete)
       RemoveCurrentUiItem();
}*/

        private void Show_DesignSurface_ContextMenu(int xPos, int yPos)
        {
            lastCmsLocation = designRoot.PointToClient(new Point(xPos, yPos));

            cmsEditor.Show(designRoot, lastCmsLocation);
        }

        private void Init_DesignSurface()
        {
            panelUiView.Controls.Clear(); // clear development items

            ServiceContainer serviceContainer = new ServiceContainer();
            serviceContainer.AddService(typeof(IMenuCommandService), new MenuService(ds, Show_DesignSurface_ContextMenu));

            ds = new DesignSurface(serviceContainer);
            
            ds.BeginLoad(typeof(UserControl)); // Get the View of the DesignSurface, host it in a form, and show it 

            Control c = ds.View as Control;
            c.Parent = panelUiView;
            c.Dock = DockStyle.Fill;

            idh = (IDesignerHost)ds.GetService(typeof(IDesignerHost));
            selectionService = (ISelectionService)ds.GetService(typeof(ISelectionService));
            if (selectionService != null)
            {
                // Add an event handler for the SelectionChanging 
                // and SelectionChanged events.
                //selectionService.SelectionChanging += new EventHandler(OnSelectionChanging);
                selectionService.SelectionChanged += new EventHandler(OnSelectionChanged);
            }
            designRoot = (UserControl)idh.RootComponent;
            designRoot.SizeChanged += designRoot_SizeChanged;
            
            designRoot.Height = panelUiView.Height - 32;
            designRoot.Width = panelUiView.Width - 32;

            // Use ComponentChangeService to announce changing of the 
            // Form's Controls collection */ 
            IComponentChangeService icc = (IComponentChangeService)idh.GetService(typeof(IComponentChangeService));
            icc.OnComponentChanging(idh.RootComponent, TypeDescriptor.GetProperties(idh.RootComponent)["Controls"]);
        }

        private void DeselectAllRows(DataGridView dgv)
        {
            for (int i = 0; i < dgv.RowCount; i++)
                dgv.Rows[i].Cells[0].Selected = false;
        }

        private void OnSelectionChanged(object sender, EventArgs args)
        {
            currentSelection = new Control[((ICollection)selectionService.GetSelectedComponents()).Count];
            ((ICollection)selectionService.GetSelectedComponents()).CopyTo(currentSelection, 0);

            DeselectAllRows(dgv);
            if (currentSelection.Length == 1)
            {
                Control ctrl = currentSelection[0];
                if (ctrl.GetType() == typeof(UserControl))
                {
                    dgvUiItem.DataSource = null;
                    return;
                }
                //Debug.AddLine(DateTime.Now.ToLongTimeString() + " OnSelectionChanged type:"+ctrl.GetType());

                currentSelectedUiItem = (UiItem)ctrl.Tag;
                dgvUiItem.DataSource = currentSelectedUiItem.vbds.dt;
                int uiItemIndex = uiItems.IndexOf(currentSelectedUiItem);
                int ownerIndex = vbdsUi.keys.IndexOf(uiItemIndex.ToString());
                dgv.Rows[ownerIndex].Cells[0].Selected = true;
                return;
            }
            //Debug.AddLine(DateTime.Now.ToLongTimeString() + " OnSelectionChanged");
            dgvUiItem.DataSource = null;

            for (int i = 0; i < currentSelection.Length; i++)
            {
                UiItem uiItem = (UiItem)currentSelection[i].Tag;
                int uiItemIndex = uiItems.IndexOf(uiItem);
                int ownerIndex = vbdsUi.keys.IndexOf(uiItemIndex.ToString());
                dgv.Rows[ownerIndex].Cells[0].Selected = true;
            }
        }

        private void this_Shown(object sender, EventArgs e)
        {
            Init_DesignSurface();

            try { OpenUiItemsRefFile(ofd.InitialDirectory + "\\master.txt"); }
            catch { }

            CreateNewUi();

            Color colorStart = Color.FromArgb(255, 255, 0, 0);
            Color colorMiddle = Color.FromArgb(255, 255, 64, 64);
            Color colorEnd = Color.FromArgb(255, 255, 16, 16);
        }
        
        
        private void tsmiCreateNewUi_Click(object sender, EventArgs e)
        {
            CreateNewUi();
        }

        public void CreateNewUi()
        {
            uiItems.Clear();
            vbdsUi = new VBDataStore(vbdsUiItemsRef.ToString());
            int indexOf = vbdsUi.keys.IndexOf("1"); // don't remove item zero because it's menu
            int count = vbdsUi.keys.Count - indexOf;

            vbdsUi.RemoveItems(indexOf, count);
            vbdsUi.putString("itemCount", "1");
            vbdsUi.putBoolean("touchProcessingExact", true);
            vbdsUi.GenerateDataTable();

            UiItem newUiItem = new UiItem(vbdsUi.getUiItem("0"));
            uiItems.Add(newUiItem);

            newUiItem.vbds.putString("text", "menu");

            designRoot.Controls.Clear();
           
            AddNewUiItemToPanel(newUiItem);

            int bgColor = int.Parse(vbdsUi.getString("bgColor", "FFFFFFFF"), System.Globalization.NumberStyles.HexNumber);
            designRoot.BackColor = Color.FromArgb(bgColor);
            dgv.DataSource = vbdsUi.dt;
        }

        private void tsmiFileOpen_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;
            OpenAndShow(ofd.FileName);
            currentFilePath = ofd.FileName;
            tsmiSaveFile.Enabled = true;
        }
        private void tsmiFileSaveAs_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.Cancel)
                return;
            currentFilePath = sfd.FileName;
            SaveCurrentFile();
            tsmiSaveFile.Enabled = true;
        }
        private void tsmiSaveFile_Click(object sender, EventArgs e)
        {
            SaveCurrentFile();
        }

        private void SaveCurrentFile()
        {
            vbdsUi.putInt("itemCount", uiItems.Count);

            for (int i = 0; i < uiItems.Count; i++)
            {
                vbdsUi.putUIitem(i.ToString(), uiItems[i].Type, uiItems[i].vbds.ToString());
            }

            File.WriteAllText(currentFilePath, vbdsUi.ToString());
        }

        private void tsmiRefUiItemsFileOpen_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;
            OpenUiItemsRefFile(ofd.FileName);
        }

        private void OpenUiItemsRefFile(string filePath)
        {
            vbdsUiItemsRef = new VBDataStore(File.ReadAllText(filePath));
            int itemCount = vbdsUiItemsRef.getInt("itemCount", 0);
            if (itemCount == 0)
            {
                MessageBox.Show("This Ref. File don't contain any UiItems");
                return;
            }
            tsmiAdd.DropDownItems.Clear();
            for (int i = 1;i < itemCount; i++)
            {
                string uiItemType = vbdsUiItemsRef.getType(i.ToString());
                if (uiItemType == "") continue; // failsafe
                ToolStripMenuItem tsmi = new ToolStripMenuItem(uiItemType);
                tsmi.Click += tsmiAddItem_Click;
                tsmi.Tag = i.ToString();
                tsmiAdd.DropDownItems.Add(tsmi);
            }
        }

        private void PasteUiItem()
        {
            /*for (int i = 0; i < uiItemsCopyRef.Length; i++)
            {
                AddNewUiItem(uiItemsCopyRef[i].Type, uiItemsCopyRef[i].vbds.ToString(), lastCmsLocation);
            }*/
            if (currentSelectionCopyRef == null) return;
            if (currentSelectionCopyRef.Length == 0) return;

            Point refPoint = currentSelectionCopyRef[0].Location;
            for (int i = 1; i < currentSelectionCopyRef.Length; i++)
            {
                if (refPoint.X > currentSelectionCopyRef[i].Location.X ||
                    refPoint.Y > currentSelectionCopyRef[i].Location.Y)
                {
                    refPoint = currentSelectionCopyRef[i].Location;
                }
            }
            for (int i = 0; i < currentSelectionCopyRef.Length; i++)
            {
                UiItem uiItem = (UiItem)currentSelectionCopyRef[i].Tag;
                Point newPoint = new Point((currentSelectionCopyRef[i].Location.X - refPoint.X) + lastCmsLocation.X,
                                           (currentSelectionCopyRef[i].Location.Y - refPoint.Y) + lastCmsLocation.Y);
                AddNewUiItem(uiItem.Type, uiItem.vbds.ToString(), newPoint);
            }
        }

        public void AddNewUiItem(string type, string value, Point location)
        {
            UiItem newUiItem = new UiItem(type, value);
            uiItems.Add(newUiItem);
            
            newUiItem.vbds.putString("text", type);

            vbdsUi.putUIitem((uiItems.Count - 1).ToString(), type, newUiItem.vbds.ToString());
            vbdsUi.putInt("itemCount", uiItems.Count);

            newUiItem.setX((float)location.X / designRoot.Width);
            newUiItem.setY((float)location.Y / designRoot.Height);

            if (newUiItem.Type == "UiTextField")
            {
                newUiItem.vbds.putString("bgColor", "00FFFFFF"); // transparent background as default
                newUiItem.vbds.putString("w", "0.22");
                newUiItem.vbds.putString("h", "0.04");
            }
            AddNewUiItemToPanel(newUiItem);

            //TODO: proper select of item
            //currentSelectedUiItem = newUiItem;
        }

        public void AddNewUiItemToPanel(UiItem newUiItem)
        {
            Control ctrl = newUiItem.CreateNewUiControl(idh);
            ctrl.MouseUp += ctrl_MouseUp;
            ctrl.Click += Ctrl_Click;
            ctrl.SizeChanged += (sender, e) => UiItem_ControlMovedOrResized(ctrl);
            ctrl.LocationChanged += (sender, e) => UiItem_ControlMovedOrResized(ctrl);
            ctrl.BringToFront();
        }

        private void Ctrl_Click(object sender, EventArgs e)
        {
            Debug.AddLine("ctrl clkci");
        }

        public void RemoveCurrentSelectedUiItems()
        {
            if (currentSelection == null) return;
            if (currentSelection.Length == 0) return;

            for (int i = 0; i < currentSelection.Length; i++)
            {
                if (currentSelection[i].GetType() == typeof(UserControl)) continue;

                RemoveUiItem((UiItem)currentSelection[i].Tag);
            }
            currentSelection = new Control[0];
        }

        public bool IsMenuButton(UiItem uiItem)
        {
            return (uiItem.vbds.getString("pressAction", "") == "menu") ||
                   (uiItem.vbds.getString("releaseAction", "") == "menu");
        }

        public void RemoveUiItem(UiItem uiItem)
        {
            if (uiItem == null) return;

            if (uiItem.Type == "UiButton")
            {
                if (IsMenuButton(uiItem))
                {
                    MessageBox.Show("Can't remove menu button!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            int index = uiItems.IndexOf(uiItem);
            designRoot.Controls.Remove(uiItem.control);
            uiItems.RemoveAt(index);
            vbdsUi.RemoveUiItem(index.ToString());
            vbdsUi.putInt("itemCount", uiItems.Count);
            vbdsUi.GenerateDataTable();
            dgv.DataSource = vbdsUi.dt;
        }

        private void tsmiAddItem_Click(object sender, EventArgs e)
        {
            TypeValuePair tvp = vbdsUiItemsRef.getUiItem((string)((ToolStripMenuItem)sender).Tag);
            AddNewUiItem(tvp.Type, tvp.Value, lastCmsLocation);
        }

        private void OpenAndShow(string filePath)
        {
            vbdsUi = new VBDataStore(File.ReadAllText(filePath));

            dgv.DataSource = vbdsUi.dt;

            int itemCount = vbdsUi.getInt("itemCount", 0);
            if (itemCount == 0)
                return;
            uiItems = new List<UiItem>(itemCount);
            designRoot.Controls.Clear();
            tsslblItemCount.Text = itemCount.ToString();
            
            for (int i = 0; i < itemCount; i++) 
            {
                UiItem newUiItem = new UiItem(vbdsUi.getUiItem(i.ToString()));
                uiItems.Add(newUiItem);
                AddNewUiItemToPanel(newUiItem);
                currentSelectedUiItem = null;
            }
            int bgColor = int.Parse(vbdsUi.getString("bgColor", "FFFFFFFF"), System.Globalization.NumberStyles.HexNumber);
            designRoot.BackColor = Color.FromArgb(bgColor);
        }
        
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (((string)vbdsUi.dt.Rows[e.RowIndex][1]).StartsWith("Ui"))
            {
                int selectedUiItemIndex = int.Parse((string)vbdsUi.dt.Rows[e.RowIndex][0]);
                currentSelectedUiItem = uiItems[selectedUiItemIndex];
                dgvUiItem.DataSource = currentSelectedUiItem.vbds.dt;
            }
        }
        
        private void dgv_DataSourceChanged(object sender, EventArgs e)
        {
            dgv.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgv.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgv.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
        }
        
        private void dgvUiItem_DataSourceChanged(object sender, EventArgs e)
        {
            dgvUiItem.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgvUiItem.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgvUiItem.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgvUiItem.Columns[0].ReadOnly = true;
            dgvUiItem.Columns[1].ReadOnly = true;
            dgvUiItem.Columns[2].ReadOnly = false;
        }

        private void ctrl_MouseUp(object s, MouseEventArgs e)
        {
            Debug.AddLine("ctrl mouseup");
            Control sender = (Control)s;
            if (e.Button == MouseButtons.Left)
            {
                currentSelectedUiItem = (UiItem)sender.Tag;

                dgvUiItem.DataSource = currentSelectedUiItem.vbds.dt;

                int uiItemIndex = uiItems.IndexOf(currentSelectedUiItem);
                int ownerIndex = vbdsUi.keys.IndexOf(uiItemIndex.ToString());
                dgv.Rows[ownerIndex].Cells[0].Selected = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                cmsEditor.Show(sender, e.Location);
            }
        }
        private bool UpdatingDesignRoot_Size = false;
        private void UiItem_ControlMovedOrResized(Control ctrl)
        {
            if (UpdatingDesignRoot_Size) return;
            //Debug.AddLine("UiItem_ControlMovedOrResized");
            UiItem uiItem = (UiItem)ctrl.Tag;
            uiItem.setX( (float)ctrl.Left / (float)designRoot.Width);
            uiItem.setY( (float)ctrl.Top / (float)designRoot.Height);
            uiItem.setW( (float)ctrl.Width / (float)designRoot.Width);
            uiItem.setH( (float)ctrl.Height / (float)designRoot.Height);

            uiItem.UpdateUiControl(designRoot);
        }
        
        private void designRoot_SizeChanged(object sender, EventArgs e)
        {
            UpdatingDesignRoot_Size = true;
            int itemCount = designRoot.Controls.Count;
            for (int i= 0;  i < itemCount; i++)
            {
                Control ctrl = (Control)designRoot.Controls[i];
                UiItem uiItem = (UiItem)ctrl.Tag;
                uiItem.UpdateUiControl(designRoot);
            }
            UpdatingDesignRoot_Size = false;
        }
        
        private void dgvUiItem_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 2)
                e.Cancel = true;
        }
        
        private void dgvUiItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (currentSelectedUiItem == null) return;

            currentSelectedUiItem.vbds.UpdateValueFromDataTableToStore(e.RowIndex);
            currentSelectedUiItem.UpdateUiControl(designRoot);
            
            dgvUiItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
        }

        private void tsmiRemoveUiItems_Click(object sender, EventArgs e)
        {
            RemoveCurrentSelectedUiItems();
        }

        private void tsmiCopyUiItem_Click(object sender, EventArgs e)
        {
            currentSelectionCopyRef = currentSelection;
            /*uiItemsCopyRef = new UiItem[currentSelection.Length];

            for (int i = 0; i < currentSelection.Length; i++)
            {
                uiItemsCopyRef[i] = (UiItem)currentSelection[i].Tag;
            }*/
            //uiItemCopyRef = currentSelectedUiItem; // just store ref to avoid memory usage
        }

        private void tsmiPasteUiItem_Click(object sender, EventArgs e)
        {
            PasteUiItem();
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 2 || e.RowIndex == 0)
                e.Cancel = true;

            string type = (string)dgv.Rows[e.RowIndex].Cells[1].Value;

            if (type.StartsWith("Ui"))
                e.Cancel = true;
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            vbdsUi.UpdateValueFromDataTableToStore(e.RowIndex);

            string key = (string)dgv.Rows[e.RowIndex].Cells[0].Value;
            if (key == "bgColor")
            {
                int bgColor = int.Parse(vbdsUi.getString("bgColor", "FFFFFFFF"), System.Globalization.NumberStyles.HexNumber);
                designRoot.BackColor = Color.FromArgb(bgColor);
            }
        }

        GraphicsPath GrPath
        {
            get
            {
                GraphicsPath grPath = new GraphicsPath();
                grPath.AddArc(0, 0, 40, 40, 180, 90);
                grPath.AddArc(this.Width - 40, 0, 40, 40, 270, 90);
                grPath.AddArc(this.Width - 40, this.Height - 40, 40, 40, 0, 90);
                grPath.AddArc(0, this.Height - 40, 40, 40, 90, 90);
                //grPath.AddEllipse(this.ClientRectangle);
                return grPath;
            }
        }

        private void dgvUiItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvUiItem.BeginEdit(false);
        }
    }
}
