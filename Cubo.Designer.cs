namespace Cubo3
{
    partial class Cubo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

       

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cubo));
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgbGeometria = new System.Windows.Forms.ToolStripProgressBar();
            this.imageTree = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.OpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.StatusStrip1.SuspendLayout();
           
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel1,
            this.pgbGeometria});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 244);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(292, 22);
            this.StatusStrip1.TabIndex = 100;
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
            this.pgbGeometria.Size = new System.Drawing.Size(100, 16);
            // 
            // imageTree
            // 
            this.imageTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageTree.ImageStream")));
            this.imageTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageTree.Images.SetKeyName(0, "");
            this.imageTree.Images.SetKeyName(1, "");
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            

        }

        #endregion

        internal System.Windows.Forms.StatusStrip StatusStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel1;
        internal System.Windows.Forms.ToolStripProgressBar pgbGeometria;
        private System.Windows.Forms.ImageList imageTree;
        private System.Windows.Forms.ImageList imageList1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDlg;
    }
}

