namespace Task_1
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.gMapControlMain = new GMap.NET.WindowsForms.GMapControl();
            this.SuspendLayout();
            // 
            // gMapControlMain
            // 
            this.gMapControlMain.Bearing = 0F;
            this.gMapControlMain.CanDragMap = true;
            this.gMapControlMain.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControlMain.GrayScaleMode = false;
            this.gMapControlMain.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControlMain.LevelsKeepInMemory = 5;
            this.gMapControlMain.Location = new System.Drawing.Point(0, 0);
            this.gMapControlMain.Margin = new System.Windows.Forms.Padding(0);
            this.gMapControlMain.MarkersEnabled = true;
            this.gMapControlMain.MaxZoom = 16;
            this.gMapControlMain.MinZoom = 2;
            this.gMapControlMain.MouseWheelZoomEnabled = true;
            this.gMapControlMain.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControlMain.Name = "gMapControlMain";
            this.gMapControlMain.NegativeMode = false;
            this.gMapControlMain.PolygonsEnabled = true;
            this.gMapControlMain.RetryLoadTile = 0;
            this.gMapControlMain.RoutesEnabled = true;
            this.gMapControlMain.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControlMain.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControlMain.ShowTileGridLines = false;
            this.gMapControlMain.Size = new System.Drawing.Size(800, 450);
            this.gMapControlMain.TabIndex = 0;
            this.gMapControlMain.Zoom = 0D;
            this.gMapControlMain.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gMapControlMain_OnMarkerEnter);
            this.gMapControlMain.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.gMapControlMain_OnMarkerLeave);
            this.gMapControlMain.Load += new System.EventHandler(this.gMapControlMain_Load);
            this.gMapControlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gMapControlMain_MouseDown);
            this.gMapControlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gMapControlMain_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gMapControlMain);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapControlMain;
    }
}

