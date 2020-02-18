namespace FacialRecognitionOldEmgu
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStream = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.imgCamUser = new Emgu.CV.UI.ImageBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDetect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgCamUser)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStream
            // 
            this.btnStream.Location = new System.Drawing.Point(151, 516);
            this.btnStream.Name = "btnStream";
            this.btnStream.Size = new System.Drawing.Size(207, 32);
            this.btnStream.TabIndex = 3;
            this.btnStream.Text = "Start Streaming";
            this.btnStream.UseVisualStyleBackColor = true;
            this.btnStream.Click += new System.EventHandler(this.btnStream_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(395, 515);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 33);
            this.button2.TabIndex = 4;
            this.button2.Text = "Save Face";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnSaveFace_Click);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(643, 525);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(220, 22);
            this.nameBox.TabIndex = 5;
            // 
            // imgCamUser
            // 
            this.imgCamUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgCamUser.Location = new System.Drawing.Point(68, 33);
            this.imgCamUser.Name = "imgCamUser";
            this.imgCamUser.Size = new System.Drawing.Size(795, 446);
            this.imgCamUser.TabIndex = 2;
            this.imgCamUser.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(537, 528);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name of face: ";
            // 
            // btnDetect
            // 
            this.btnDetect.Location = new System.Drawing.Point(151, 565);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(207, 42);
            this.btnDetect.TabIndex = 7;
            this.btnDetect.Text = "Detect Auxiliary Features(Mouth, Nose, Eyes)";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 616);
            this.Controls.Add(this.btnDetect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgCamUser);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnStream);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgCamUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStream;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox nameBox;
        private Emgu.CV.UI.ImageBox imgCamUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDetect;
    }
}

