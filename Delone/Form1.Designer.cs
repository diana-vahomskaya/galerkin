
namespace Delone
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Points = new System.Windows.Forms.Button();
            this.Triangle = new System.Windows.Forms.Button();
            this.New_Points = new System.Windows.Forms.Button();
            this.N = new System.Windows.Forms.TextBox();
            this.Delta = new System.Windows.Forms.TextBox();
            this.Outside_R = new System.Windows.Forms.TextBox();
            this.Inside_R = new System.Windows.Forms.TextBox();
            this.Distance = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.yel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Triangulation = new System.Windows.Forms.Button();
            this.Draw = new System.Windows.Forms.Button();
            this.DRAW_POT = new System.Windows.Forms.RadioButton();
            this.DRAW_ISO = new System.Windows.Forms.RadioButton();
            this.DRAW_POWER = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(27, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(889, 736);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Points
            // 
            this.Points.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Points.Location = new System.Drawing.Point(10, 33);
            this.Points.Name = "Points";
            this.Points.Size = new System.Drawing.Size(141, 36);
            this.Points.TabIndex = 1;
            this.Points.Text = "Построение точек";
            this.Points.UseVisualStyleBackColor = true;
            this.Points.Click += new System.EventHandler(this.Points_Click);
            // 
            // Triangle
            // 
            this.Triangle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Triangle.Location = new System.Drawing.Point(157, 33);
            this.Triangle.Name = "Triangle";
            this.Triangle.Size = new System.Drawing.Size(153, 36);
            this.Triangle.TabIndex = 2;
            this.Triangle.Text = "Первая триангуляция";
            this.Triangle.UseVisualStyleBackColor = true;
            this.Triangle.Click += new System.EventHandler(this.Triangle_Click);
            // 
            // New_Points
            // 
            this.New_Points.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.New_Points.Location = new System.Drawing.Point(10, 75);
            this.New_Points.Name = "New_Points";
            this.New_Points.Size = new System.Drawing.Size(141, 34);
            this.New_Points.TabIndex = 3;
            this.New_Points.Text = "Делоне";
            this.New_Points.UseVisualStyleBackColor = true;
            this.New_Points.Click += new System.EventHandler(this.New_Points_Click);
            // 
            // N
            // 
            this.N.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.N.Location = new System.Drawing.Point(126, 28);
            this.N.Name = "N";
            this.N.Size = new System.Drawing.Size(100, 27);
            this.N.TabIndex = 4;
            this.N.Text = "14";
            // 
            // Delta
            // 
            this.Delta.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Delta.Location = new System.Drawing.Point(126, 69);
            this.Delta.Name = "Delta";
            this.Delta.Size = new System.Drawing.Size(100, 27);
            this.Delta.TabIndex = 5;
            this.Delta.Text = "0,05";
            // 
            // Outside_R
            // 
            this.Outside_R.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Outside_R.Location = new System.Drawing.Point(128, 36);
            this.Outside_R.Name = "Outside_R";
            this.Outside_R.Size = new System.Drawing.Size(100, 27);
            this.Outside_R.TabIndex = 6;
            this.Outside_R.Text = "100";
            // 
            // Inside_R
            // 
            this.Inside_R.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Inside_R.Location = new System.Drawing.Point(128, 69);
            this.Inside_R.Name = "Inside_R";
            this.Inside_R.Size = new System.Drawing.Size(100, 27);
            this.Inside_R.TabIndex = 7;
            this.Inside_R.Text = "60";
            // 
            // Distance
            // 
            this.Distance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Distance.Location = new System.Drawing.Point(128, 102);
            this.Distance.Name = "Distance";
            this.Distance.Size = new System.Drawing.Size(100, 27);
            this.Distance.TabIndex = 8;
            this.Distance.Text = "50";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(8, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Кол-во точек:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(8, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Погрешность:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(10, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Внешний R:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(10, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Внутенний R:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(10, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Расстояние:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Delta);
            this.groupBox1.Controls.Add(this.N);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(960, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 122);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры разбиения:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Distance);
            this.groupBox2.Controls.Add(this.Inside_R);
            this.groupBox2.Controls.Add(this.Outside_R);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(959, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 149);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры фигуры:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // yel
            // 
            this.yel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.yel.Location = new System.Drawing.Point(157, 75);
            this.yel.Name = "yel";
            this.yel.Size = new System.Drawing.Size(153, 34);
            this.yel.TabIndex = 16;
            this.yel.Text = "Выделение";
            this.yel.UseVisualStyleBackColor = true;
            this.yel.Click += new System.EventHandler(this.yel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.yel);
            this.groupBox3.Controls.Add(this.New_Points);
            this.groupBox3.Controls.Add(this.Triangle);
            this.groupBox3.Controls.Add(this.Points);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.Location = new System.Drawing.Point(922, 305);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(316, 126);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Чистое этапное Делоне";
            // 
            // Triangulation
            // 
            this.Triangulation.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Triangulation.Location = new System.Drawing.Point(1003, 448);
            this.Triangulation.Name = "Triangulation";
            this.Triangulation.Size = new System.Drawing.Size(142, 45);
            this.Triangulation.TabIndex = 18;
            this.Triangulation.Text = "Триангуляция";
            this.Triangulation.UseVisualStyleBackColor = true;
            this.Triangulation.Click += new System.EventHandler(this.Triangulation_Click);
            // 
            // Draw
            // 
            this.Draw.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Draw.Location = new System.Drawing.Point(1003, 692);
            this.Draw.Name = "Draw";
            this.Draw.Size = new System.Drawing.Size(142, 45);
            this.Draw.TabIndex = 20;
            this.Draw.Text = "Нарисовать";
            this.Draw.UseVisualStyleBackColor = true;
            this.Draw.Click += new System.EventHandler(this.Draw_Click);
            // 
            // DRAW_POT
            // 
            this.DRAW_POT.AutoSize = true;
            this.DRAW_POT.Font = new System.Drawing.Font("Segoe UI", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.DRAW_POT.Location = new System.Drawing.Point(20, 37);
            this.DRAW_POT.Name = "DRAW_POT";
            this.DRAW_POT.Size = new System.Drawing.Size(236, 24);
            this.DRAW_POT.TabIndex = 21;
            this.DRAW_POT.TabStop = true;
            this.DRAW_POT.Text = "Распределение потенциала";
            this.DRAW_POT.UseVisualStyleBackColor = true;
            // 
            // DRAW_ISO
            // 
            this.DRAW_ISO.AutoSize = true;
            this.DRAW_ISO.Font = new System.Drawing.Font("Segoe UI", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.DRAW_ISO.Location = new System.Drawing.Point(20, 80);
            this.DRAW_ISO.Name = "DRAW_ISO";
            this.DRAW_ISO.Size = new System.Drawing.Size(233, 24);
            this.DRAW_ISO.TabIndex = 22;
            this.DRAW_ISO.TabStop = true;
            this.DRAW_ISO.Text = "Эквипотенциальные линии";
            this.DRAW_ISO.UseVisualStyleBackColor = true;
            // 
            // DRAW_POWER
            // 
            this.DRAW_POWER.AutoSize = true;
            this.DRAW_POWER.Font = new System.Drawing.Font("Segoe UI", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.DRAW_POWER.Location = new System.Drawing.Point(20, 120);
            this.DRAW_POWER.Name = "DRAW_POWER";
            this.DRAW_POWER.Size = new System.Drawing.Size(141, 24);
            this.DRAW_POWER.TabIndex = 23;
            this.DRAW_POWER.TabStop = true;
            this.DRAW_POWER.Text = "Силовые линии";
            this.DRAW_POWER.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.DRAW_POWER);
            this.groupBox4.Controls.Add(this.DRAW_ISO);
            this.groupBox4.Controls.Add(this.DRAW_POT);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox4.Location = new System.Drawing.Point(940, 509);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(269, 167);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Отрисовка:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 770);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.Draw);
            this.Controls.Add(this.Triangulation);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Points;
        private System.Windows.Forms.Button Triangle;
        private System.Windows.Forms.Button New_Points;
        private System.Windows.Forms.TextBox N;
        private System.Windows.Forms.TextBox Delta;
        private System.Windows.Forms.TextBox Outside_R;
        private System.Windows.Forms.TextBox Inside_R;
        private System.Windows.Forms.TextBox Distance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button yel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button Triangulation;
        private System.Windows.Forms.Button Draw;
        private System.Windows.Forms.RadioButton DRAW_POT;
        private System.Windows.Forms.RadioButton DRAW_ISO;
        private System.Windows.Forms.RadioButton DRAW_POWER;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}

