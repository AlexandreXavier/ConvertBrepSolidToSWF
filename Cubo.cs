using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Cubo3
{
    /// Cubo Form.
    
    public class frmCubo : System.Windows.Forms.Form
    {
        # region variaveis
        public string sFile;
        public string sName;
        public Boolean ficheirosEstaticos = false;
        public Boolean ficheirosBase = false;
        public static List<Thread> thread = new List<Thread>();
        public Inventor.SurfaceBody oSurfaceBody;
        internal System.Windows.Forms.OpenFileDialog OpenFileDlg;
        private System.ComponentModel.IContainer components;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TabPage tabPage8;
        private System.Windows.Forms.ImageList imageTree;
        internal TabControl TabControl;
        internal TabPage Home;
        internal GroupBox GroupBox4;
        internal Button bt2;
        internal Button bt1;
        internal GroupBox GroupBox3;
        internal Button btOpen;
        internal TabPage Insert;
        internal TabPage Publish;
        internal GroupBox GroupBox6;
        internal Button btPublishSWF;
        internal StatusStrip StatusStrip1;
        internal ToolStripStatusLabel ToolStripStatusLabel1;
        internal ToolStripProgressBar pgbGeometria;
        internal SaveFileDialog SaveFileDialog1;
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash;
        private GroupBox groupBox1;
        private TreeView treList;
        internal Button cmdRefreshTree;
        internal Button cmdClose;

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;

        public CurrencyManager cm;
       
        //Define the inventor object
        public Inventor.ApprenticeServerComponent objapprenticeServerApp = new Inventor.ApprenticeServerComponentClass();

        //Define the status of the 3D view
        private bool appReady = false;
        private bool swfReady = false;
        private Flash.External.ExternalInterfaceProxy proxy;
        public static Cubo3.frmCubo formulario;
        //Contentor Raiz
        private WMAssembly Contentor;
        #endregion
        //Iniciate the layout
        public frmCubo()
        {
            InitializeComponent();
            frmCubo.formulario = this;
            
            
        }

        // Clean up any resources being used.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        //Design the layout
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCubo));
            this.OpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.imageTree = new System.Windows.Forms.ImageList(this.components);
            this.TabControl = new System.Windows.Forms.TabControl();
            this.Home = new System.Windows.Forms.TabPage();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.bt2 = new System.Windows.Forms.Button();
            this.bt1 = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.btOpen = new System.Windows.Forms.Button();
            this.Insert = new System.Windows.Forms.TabPage();
            this.Publish = new System.Windows.Forms.TabPage();
            this.GroupBox6 = new System.Windows.Forms.GroupBox();
            this.btPublishSWF = new System.Windows.Forms.Button();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgbGeometria = new System.Windows.Forms.ToolStripProgressBar();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.axShockwaveFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdRefreshTree = new System.Windows.Forms.Button();
            this.treList = new System.Windows.Forms.TreeView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.pg = new System.Windows.Forms.PropertyGrid();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TabControl.SuspendLayout();
            this.Home.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.Publish.SuspendLayout();
            this.GroupBox6.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageTree
            // 
            this.imageTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageTree.ImageStream")));
            this.imageTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageTree.Images.SetKeyName(0, "");
            this.imageTree.Images.SetKeyName(1, "");
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.Home);
            this.TabControl.Controls.Add(this.Insert);
            this.TabControl.Controls.Add(this.Publish);
            this.TabControl.Location = new System.Drawing.Point(16, 8);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(962, 100);
            this.TabControl.TabIndex = 98;
            // 
            // Home
            // 
            this.Home.Controls.Add(this.GroupBox4);
            this.Home.Controls.Add(this.GroupBox3);
            this.Home.Location = new System.Drawing.Point(4, 22);
            this.Home.Name = "Home";
            this.Home.Padding = new System.Windows.Forms.Padding(3);
            this.Home.Size = new System.Drawing.Size(954, 74);
            this.Home.TabIndex = 0;
            this.Home.Text = "Home";
            this.Home.ToolTipText = "Start ";
            this.Home.UseVisualStyleBackColor = true;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.button2);
            this.GroupBox4.Controls.Add(this.button1);
            this.GroupBox4.Controls.Add(this.bt2);
            this.GroupBox4.Controls.Add(this.bt1);
            this.GroupBox4.Location = new System.Drawing.Point(98, 6);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(301, 62);
            this.GroupBox4.TabIndex = 1;
            this.GroupBox4.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(231, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 47);
            this.button2.TabIndex = 3;
            this.button2.Text = "Guardar Vista";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(156, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 47);
            this.button1.TabIndex = 2;
            this.button1.Text = "Mostrar Tudo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bt2
            // 
            this.bt2.Location = new System.Drawing.Point(81, 9);
            this.bt2.Name = "bt2";
            this.bt2.Size = new System.Drawing.Size(69, 47);
            this.bt2.TabIndex = 1;
            this.bt2.Text = "Esconder Tudo";
            this.bt2.UseVisualStyleBackColor = true;
            this.bt2.Click += new System.EventHandler(this.esconderTudo);
            // 
            // bt1
            // 
            this.bt1.Location = new System.Drawing.Point(6, 9);
            this.bt1.Name = "bt1";
            this.bt1.Size = new System.Drawing.Size(69, 47);
            this.bt1.TabIndex = 0;
            this.bt1.Text = "Criar SWF";
            this.bt1.UseVisualStyleBackColor = true;
            this.bt1.Click += new System.EventHandler(this.bt1_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.GroupBox3.Controls.Add(this.btOpen);
            this.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GroupBox3.Location = new System.Drawing.Point(6, 6);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(86, 62);
            this.GroupBox3.TabIndex = 0;
            this.GroupBox3.TabStop = false;
            // 
            // btOpen
            // 
            this.btOpen.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btOpen.Location = new System.Drawing.Point(6, 9);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(75, 47);
            this.btOpen.TabIndex = 0;
            this.btOpen.Text = "Open";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // Insert
            // 
            this.Insert.Location = new System.Drawing.Point(4, 22);
            this.Insert.Name = "Insert";
            this.Insert.Padding = new System.Windows.Forms.Padding(3);
            this.Insert.Size = new System.Drawing.Size(954, 74);
            this.Insert.TabIndex = 1;
            this.Insert.Text = "Insert";
            this.Insert.UseVisualStyleBackColor = true;
            // 
            // Publish
            // 
            this.Publish.Controls.Add(this.GroupBox6);
            this.Publish.Location = new System.Drawing.Point(4, 22);
            this.Publish.Name = "Publish";
            this.Publish.Size = new System.Drawing.Size(954, 74);
            this.Publish.TabIndex = 2;
            this.Publish.Text = "Publish";
            this.Publish.UseVisualStyleBackColor = true;
            // 
            // GroupBox6
            // 
            this.GroupBox6.Controls.Add(this.btPublishSWF);
            this.GroupBox6.Location = new System.Drawing.Point(10, 0);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Size = new System.Drawing.Size(102, 73);
            this.GroupBox6.TabIndex = 0;
            this.GroupBox6.TabStop = false;
            // 
            // btPublishSWF
            // 
            this.btPublishSWF.Location = new System.Drawing.Point(6, 9);
            this.btPublishSWF.Name = "btPublishSWF";
            this.btPublishSWF.Size = new System.Drawing.Size(85, 62);
            this.btPublishSWF.TabIndex = 0;
            this.btPublishSWF.Text = "SWF";
            this.btPublishSWF.UseVisualStyleBackColor = true;
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel1,
            this.pgbGeometria});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 739);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(990, 22);
            this.StatusStrip1.TabIndex = 99;
            this.StatusStrip1.Text = "StatusStrip1";
            // 
            // ToolStripStatusLabel1
            // 
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Size = new System.Drawing.Size(49, 17);
            this.ToolStripStatusLabel1.Text = "Progress";
            // 
            // pgbGeometria
            // 
            this.pgbGeometria.Name = "pgbGeometria";
            this.pgbGeometria.Size = new System.Drawing.Size(800, 16);
            // 
            // axShockwaveFlash
            // 
            this.axShockwaveFlash.Enabled = true;
            this.axShockwaveFlash.Location = new System.Drawing.Point(257, 110);
            this.axShockwaveFlash.Name = "axShockwaveFlash";
            this.axShockwaveFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash.OcxState")));
            this.axShockwaveFlash.Size = new System.Drawing.Size(717, 370);
            this.axShockwaveFlash.TabIndex = 0;
            this.axShockwaveFlash.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(this.axFlashGui_ExtCall);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdClose);
            this.groupBox1.Controls.Add(this.cmdRefreshTree);
            this.groupBox1.Controls.Add(this.treList);
            this.groupBox1.Location = new System.Drawing.Point(26, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 384);
            this.groupBox1.TabIndex = 101;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Browser";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(117, 347);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(88, 27);
            this.cmdClose.TabIndex = 26;
            this.cmdClose.Text = "Close";
            // 
            // cmdRefreshTree
            // 
            this.cmdRefreshTree.Location = new System.Drawing.Point(16, 347);
            this.cmdRefreshTree.Name = "cmdRefreshTree";
            this.cmdRefreshTree.Size = new System.Drawing.Size(95, 27);
            this.cmdRefreshTree.TabIndex = 25;
            this.cmdRefreshTree.Text = "Refresh Tree";
            this.cmdRefreshTree.Click += new System.EventHandler(this.cmdRefreshTree_Click_1);
            // 
            // treList
            // 
            this.treList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treList.CheckBoxes = true;
            this.treList.ImageIndex = 0;
            this.treList.ImageList = this.imageTree;
            this.treList.Location = new System.Drawing.Point(16, 20);
            this.treList.Name = "treList";
            this.treList.SelectedImageIndex = 0;
            this.treList.Size = new System.Drawing.Size(189, 321);
            this.treList.TabIndex = 24;
            this.treList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treList_AfterCheck);
            this.treList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treList_AfterSelect);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(392, 211);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "tabPage1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(192, 71);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "tabPage2";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(192, 71);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage4.ImageIndex = 0;
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(192, 71);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(0, 0);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(200, 100);
            this.tabPage5.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(0, 0);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(200, 100);
            this.tabPage6.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(0, 0);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(200, 100);
            this.tabPage7.TabIndex = 0;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(0, 0);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(200, 100);
            this.tabPage8.TabIndex = 0;
            // 
            // pg
            // 
            this.pg.HelpVisible = false;
            this.pg.Location = new System.Drawing.Point(26, 500);
            this.pg.Name = "pg";
            this.pg.Size = new System.Drawing.Size(217, 114);
            this.pg.TabIndex = 102;
            this.pg.ToolbarVisible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Malha",
            "Cor",
            "Imagem"});
            this.comboBox1.Location = new System.Drawing.Point(93, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tipo de textura";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(139, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Escolher";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 87);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(199, 42);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Alpha";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(127, 20);
            this.textBox1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new System.Drawing.Point(26, 620);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 116);
            this.panel1.TabIndex = 104;
            // 
            // listBox1
            // 
            this.listBox1.DataSource = new string[0];
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(257, 511);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(172, 225);
            this.listBox1.TabIndex = 105;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            //this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 492);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 106;
            this.label3.Text = "Lista de Vistas";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(436, 511);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(324, 20);
            this.textBox2.TabIndex = 107;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(436, 492);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 108;
            this.label4.Text = "Descrição da Vista Actual";
            // 
            // frmCubo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(990, 761);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.axShockwaveFlash);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.pg);
            this.Name = "frmCubo";
            this.Text = "Cubo";
            this.Load += new System.EventHandler(this.frmCubo_Load);
            this.TabControl.ResumeLayout(false);
            this.Home.ResumeLayout(false);
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.Publish.ResumeLayout(false);
            this.GroupBox6.ResumeLayout(false);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Main Aplication

        /// Start the application
        /// The main entry point for the application.
        [STAThread]
        static void Main()
        {
            
            Application.Run(new frmCubo());
        }
         #endregion
        #region Flash
        /// Called by the proxy when an ActionScript ExternalInterface call
        /// is made by the SWF
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments associated with the event</param>
        /// <returns>The response to the function call.</returns>
        private object proxy_ExternalInterfaceCall(object sender, Flash.External.ExternalInterfaceCallEventArgs e)
        {
            int teste = 0;
            switch (e.FunctionCall.FunctionName)
            {
                case "initReady":
                   return initReady();
                   break;
                case "selectInTree":
                   selectInTree((String)e.FunctionCall.Arguments[0]);
                   return null; 
                   break;
                default:
                    return "nada";
            }
        }
        public Boolean initReady()
        {
            swfReady = true;
            return true;
        }
        public void selectInTree(String caminho)
        {
            MyTreeNode temp2 = getItemFromTree(caminho);
            treList.SelectedNode = temp2;
            treList.Focus();
        }
        #endregion
        public MyTreeNode getItemFromTree(String caminho)
        {
            List<int> temp = WMServicosConver.converterStringEmArray(caminho);
            MyTreeNode temp2 = (MyTreeNode)treList.Nodes[0];
            for (int i = 0; i < temp.Count; i++)
            {
                temp2 = (MyTreeNode)temp2.Nodes[temp[i]];
            }
            return temp2;
        }
        public void changeMaterialColor(String caminho,int cor)
        {
            proxy.Call("changeMaterial", caminho, cor);
        }

        #region Events from the layout

        // Called when the "Send" button is clicked by the user.
        // </summary>
        // <param name="sender"></param>
        // <param name="e"></param>
        private void treList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int teste = 9;
           /* try
            {
               MyTreeNode temp = (MyTreeNode)e.Node;
                if (!temp.item.caminho.Equals(""))
                {
                    proxy.Call("selectObject", temp.item.caminho);
                }

            }
            catch (Exception eerro)
            {
                int teste = 9;
            }
			*/
           // MyTreeNode temp = (MyTreeNode)e.Node;
            //pg.SelectedObject = temp.item;
            MyTreeNode temp = (MyTreeNode)e.Node;
            //pg.Refresh();
            if(!temp.item.caminho.Equals("")){

                try
                {

                    if (pg.SelectedObject == null || !pg.SelectedObject.Equals(temp.item))
                    {
                        //pg.Focus();
                        //pg.SelectedObject = temp.item;
                        selectedItem = temp.item;
                        //pg.Focus();

                    }
                }
                catch (Exception edssdrro)
                {
                    int erro = 9;
                }  
                  //proxy.Call("selectObject", temp.item.caminho);
                //WmServicos.proxy.Call("changeMaterialColor", temp.item.caminho,0x000000);
                    
                
            }
        }
        private void treList_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                
                MyTreeNode temp = (MyTreeNode)e.Node;
                temp.item.compilar = temp.Checked;
                if (temp.SelectedImageIndex != 0)
                {
                 proxy.Call("esconderObject", temp.item.caminho, temp.Checked.ToString());
                //    WmServicos.proxy.Call("changeMaterialColor", temp.item.caminho,0x000000 );
                }
                if (!temp.supLock)
                {
                    if (temp.Level != 0)
                    {
                        if (temp.Checked)
                        {
                            //se foi selecionado ele verifica se o pai o esta
                            MyTreeNode pt = ((MyTreeNode)temp.Parent);
                            pt.childSelect++;
                            Console.WriteLine("Objecto check add" + temp.Text + " pai childs select " + pt.childSelect);
                            if (!temp.Parent.Checked)
                            {
                                //se o pai nao esta seleccionado ele impedi-o de actualizar o seus filhos troca-lhe o estado
                                pt.infLock = true;
                                temp.Parent.Checked = temp.Checked;
                                //proxy.Call("esconderObject", temp.item.caminho);
                                pt.infLock = false;
                            }
                        }
                        else
                        {
                            MyTreeNode pt = ((MyTreeNode)temp.Parent);
                            pt.childSelect--;
                            Console.WriteLine("Objecto decheck sub" + temp.Text+ " pai childs select " + pt.childSelect);
                            //verifica se o pai ainda tem filho activos
                            if (pt.childSelect == 0)
                            {
                                pt.infLock = true;
                                temp.Parent.Checked = temp.Checked;
                                //proxy.Call("esconderObject", temp.item.caminho);
                                
                                pt.infLock = false;
                            }

                        }
                    }
                }
                if (temp.SelectedImageIndex == 0 && !temp.infLock)
                {
                    
                        Console.WriteLine("Objecto " + temp.Text + " checked " + temp.Checked);
                        //assembly
                        MyTreeNode t;
                        temp.childSelect = temp.Checked ? temp.Nodes.Count : 0;
                        for (int i = 0; i < temp.Nodes.Count; i++)
                        {

                            t = (MyTreeNode)temp.Nodes[i];
                            t.supLock = true;
                            t.Checked = temp.Checked;
                            t.supLock = false;
                            
                        }
                        
                    }
              //  proxy.Call("esconderObject", temp.item.caminho);                
            }
            catch (Exception erro)
            {
                int teste = 9;
            }
	

        }

        public void SWFSelectPeca(string caminhoPeca)
        {
            string res = (string)proxy.Call("selectPeca", caminhoPeca);
        }


        //Close the aplication
        private void cmdClose_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
        //Load the form
        private void frmCubo_Load(object sender, System.EventArgs e)
        {

        }

        //Open the file to convert
        private void btOpen_Click(object sender, EventArgs e)
        {
            //Parametros da caixa de dialogo para abrir ficheiro
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.InitialDirectory = objapprenticeServerApp.FileLocations.Workspace;
            openFileDlg.Title = "Select file";
            openFileDlg.DefaultExt = ".ipt|.iam";
            openFileDlg.Filter = "Inventor files (*.ipt; *.iam)|*.ipt;*.iam";
            openFileDlg.ShowDialog();

            //Patch from the file
            sFile = openFileDlg.FileName;
            string str = sFile;
            int idx1;
            int idx2;
            idx1 = str.LastIndexOf('\\') + 1;
            idx2 = str.IndexOf('.');

            //Nome do ficheiro
            Config.projectNome = (str.Substring(idx1, idx2 - idx1)).Replace(" ", "_");


            //Verificar se não está vazio
            if (sFile != "")
            {
                //Criar o contetor com os dados do inventor
                WmServicos.verificarDirectorio();
                BuildContentor();
                //WMVertice.calcularCentroMassa();
               // WMVertice.ficheiro.Close();
                //criarSwfPreview();
                //                carregarSwf();
                int i = 0;
            }
        }
        public void carregarSwf()
        {
           // this.axShockwaveFlash.Movie=Config.caminhoSwf;
            this.axShockwaveFlash.LoadMovie(0, Config.caminhoSwf);
            //this.axShockwaveFlash.LoadMovie(0, "c:\\temp\\temp2\\assembly3\\bin\\assembly3.swf");
            // Create the proxy and register this app to receive notification when the proxy receives
            proxy = new Flash.External.ExternalInterfaceProxy(axShockwaveFlash);
            WmServicos.proxy = proxy;
            // a call from ActionScript
            appReady = true;
            WmServicos.axShockwaveFlash = this.axShockwaveFlash;
            axShockwaveFlash.Select();
            criarTree();
           
           
            
            //proxy.Call("setAppReady", true);
        }
        private void axFlashGui_ExtCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
            int teste = 0;

        }
        private void cmdBrowse_Click(object sender, System.EventArgs e)
        {

        }

        #endregion

        #region Methods called by Flash Player

        
        private void setSWFIsReady()
        {
            // record that the SWF has registered it's functions (i.e. that C#
            // can safely call the ActionScript functions)
            swfReady = true;

 
        }

        #endregion

        #region Private Methods

        //Criar num vector com toda a informação do inventor
        private void BuildContentor()
        {
            
            //
            Inventor.ApprenticeServerDocument objapprenticeServerDocument;
            WMServicosMatrix.transGeo = objapprenticeServerApp.TransientGeometry;
            objapprenticeServerDocument = objapprenticeServerApp.Open(sFile);
            
            //objapprenticeServerDocument.ComponentDefinition.RangeBox.GetBoxData(ref  WmServicos.MinPoint, ref  WmServicos.MaxPoint);
            //Double x = (((double[])(Cubo3.WmServicos.MaxPoint))[0] + ((double[])(Cubo3.WmServicos.MinPoint))[0]) / 2;
            //Double y = (((double[])(Cubo3.WmServicos.MaxPoint))[1] + ((double[])(Cubo3.WmServicos.MinPoint))[1]) / 2;
            //Double z = (((double[])(Cubo3.WmServicos.MaxPoint))[2] + ((double[])(Cubo3.WmServicos.MinPoint))[2]) / 2;
            
            try
            {
                
             //   string te = objapprenticeServerDocument.ActiveRenderStyle.ToString();
                // Filtrar conforme o tipo de documento aberto
                if (objapprenticeServerDocument.DocumentType == Inventor.DocumentTypeEnum.kPartDocumentObject)
                {
                    Contentor = new WMAssembly(objapprenticeServerDocument.ComponentDefinition.SurfaceBodies[1], objapprenticeServerDocument.FullFileName, objapprenticeServerDocument.DisplayName);

                }
                else
                {
                    Contentor = new WMAssembly(objapprenticeServerDocument.ComponentDefinition.Occurrences, objapprenticeServerDocument.FullFileName, WmServicos.getNomeClassePrincipal(objapprenticeServerDocument.DisplayName), objapprenticeServerDocument.ComponentDefinition);

                }
                WmServicos.cleanThreads();
                int testeuu = 9;
            }
            catch (Exception e)
            {
                int teste = WMPeca.teste;
            }
        }

        private void criarSwfPreview()
        {
            fichMainPreview.criarFicheiro(Contentor.nomeClassAs, Contentor.nome);
            criarFicheiroEstaticos();
            criarFicheirosBase();
            Contentor.criarActionScript();
            WmServicos.criarTemplate();
            //Thread temp = new Thread(new ThreadStart(WmServicos.criarTemplate));
            //temp.Start();
            //thread.Add(temp);
            //WmServicos.cleanThreads();
            WmServicos.compilar();
            carregarSwf();
        }
        #endregion
        private void criarFicheiroEstaticos()
        {
            if (ficheirosEstaticos == false)
            {
                ficheirosEstaticos = true;
                fichPeca t1 = new fichPeca();
                fichAssembly t2 = new fichAssembly();
                fichWmservicos t3 = new fichWmservicos();
                fichVista t4 = new fichVista();
                t1.run();
                t2.run();
                t3.run();
                t4.run();
                //Thread temp = new Thread(new ThreadStart(t1.run));
                //frmCubo.thread.Add(temp);
                //temp.Start();
                //temp = new Thread(new ThreadStart(t2.run));
                //frmCubo.thread.Add(temp);
                //temp.Start();
                //temp = new Thread(new ThreadStart(t3.run));
                //frmCubo.thread.Add(temp);
                //temp.Start();
            }
        }
        private void criarFicheirosBase()
        {
            if (ficheirosBase == false)
            {
                WmTipo.gerarAS3();
            }
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            criarSwfPreview();
        }
        public void criarTree()
        {
            //Suppress re-painting the tree view until all nodes have been created
            try
            {
                treList.BeginUpdate();

                //Clear the tree contents
                treList.Nodes.Clear();
                treList.Nodes.Add(Contentor.criarNo());
                treList.ExpandAll();

                treList.EndUpdate();
                
            }
            catch (Exception e)
            {
                int teste=9;
            }
        }

        private void cmdRefreshTree_Click_1(object sender, EventArgs e)
        {
            criarTree();
            //pg.SelectedObject = ((MyTreeNode)treList.Nodes[0].Nodes[2].Nodes[0]).item;
            //pg.SelectedObject=Contentor;
            int teste=9;
        }

        private void esconderTudo(object sender, EventArgs e)
        {
            try
            {
                //proxy.Call("esconderTudo");
                string t = axShockwaveFlash.CallFunction("<invoke" + " name=\"esconderTudo\" returntype=\"xml\"><arguments><false/></arguments></invoke>");
                int i = 0;
            }
            catch (Exception er)
            {
                int t = 0;
            }
        }

        internal Button button1;

        private void button1_Click(object sender, EventArgs e)
        {
//            proxy.Call("esconderTudo", "true");
           
        }

        private Button button2;

        private void button2_Click(object sender, EventArgs e)
        {
            Contentor.guardarVista();
            proxy.Call("guardarVista");
        }

        private PropertyGrid pg;
        public PropertyGrid getPg()
        {
            return pg;
        }
        public TreeView getTreeList()
        {
            return treList;
        }

        public Flash.External.ExternalInterfaceProxy getFlashProxy()
        {
            return proxy;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            MyTreeNode temp = (MyTreeNode)treList.SelectedNode;
            temp.item.TipoCompilacao = (String)(((ComboBox)sender).SelectedItem);
            if (((ComboBox)sender).SelectedItem.Equals("Malha"))
            {
                button3.Enabled = false;
            }
            else
            {
                button3.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyTreeNode temp = (MyTreeNode)treList.SelectedNode;
            if (temp.item.TipoCompilacao.Equals("Imagem"))
            {
                OpenFileDialog file = new OpenFileDialog();
                file.ShowDialog();
                temp.item.FicheiroTextura = file.FileName;
            } else if(temp.item.TipoCompilacao.Equals("Cor")){
                ColorDialog cor = new ColorDialog();
                
                cor.AllowFullOpen=true;
                cor.AnyColor=true;
                cor.ShowDialog();
                temp.item.Cor=cor.Color;
            }
        }
        private WMTipoElemento _selectedItem;
        private WMTipoElemento selectedItem
        {
            get { return _selectedItem; }
            set {
                _selectedItem = value;
                pg.SelectedObject = value;
                
                //comboBox1.SelectedText = value.TipoCompilacao;
                this.comboBox1.SelectedValueChanged -= new System.EventHandler(this.comboBox1_SelectedValueChanged);
                comboBox1.SelectedItem = value.TipoCompilacao;
                this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
                //comboBox1.ResumeLayout();
                this.trackBar1.ValueChanged -= new System.EventHandler(this.trackBar1_ValueChanged);
                trackBar1.Value =(int)value.Alpha;
                this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
                if (value.TipoCompilacao.Equals("Malha"))
                {
                    textBox1.Text = "";
                }
                else if (value.TipoCompilacao.Equals("Imagem"))
                {
                    textBox1.Text = value.vistaActual.localizacaoTextura.path;
                }
                else
                {
                    textBox1.Text =  value.colorRGB().ToString("X");
                    textBox1.ForeColor = value.Cor;
                    textBox1.BackColor = value.Cor;
                }
            }

        }
        private ComboBox comboBox1;
        private Label label1;
        private Button button3;
        private TrackBar trackBar1;
        private Label label2;
        private TextBox textBox1;
        private Panel panel1;

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            selectedItem.Alpha = ((TrackBar)sender).Value;
        }

        public ListBox listBox1;
        private Label label3;
        public TextBox textBox2;
        private Label label4;


        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int index = ((ListBox)sender).SelectedIndex;
            if (index != -1)
            {
                proxy.Call("setVista", index);
            }
        }


    }
}