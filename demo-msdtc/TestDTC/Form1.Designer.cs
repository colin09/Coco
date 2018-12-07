namespace TestDTC
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
            this.txtBoxUrl1 = new System.Windows.Forms.TextBox();
            this.txtBoxUrl2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCall = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBoxUrl1
            // 
            this.txtBoxUrl1.Location = new System.Drawing.Point(134, 43);
            this.txtBoxUrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBoxUrl1.Name = "txtBoxUrl1";
            this.txtBoxUrl1.Size = new System.Drawing.Size(984, 26);
            this.txtBoxUrl1.TabIndex = 0;
            this.txtBoxUrl1.Text = "http://StoneT450/test1/api/test1/add?name=tao&value=8";
            // 
            // txtBoxUrl2
            // 
            this.txtBoxUrl2.Location = new System.Drawing.Point(134, 97);
            this.txtBoxUrl2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBoxUrl2.Name = "txtBoxUrl2";
            this.txtBoxUrl2.Size = new System.Drawing.Size(984, 26);
            this.txtBoxUrl2.TabIndex = 1;
            this.txtBoxUrl2.Text = "http://Stone-T460/test2/api/test2/add?ordername=moutai&payment=5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Url1:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 102);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Url2:";
            // 
            // btnCall
            // 
            this.btnCall.Location = new System.Drawing.Point(57, 160);
            this.btnCall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(112, 35);
            this.btnCall.TabIndex = 5;
            this.btnCall.Text = "Call";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(57, 212);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1060, 561);
            this.textBox1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 160);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 35);
            this.button1.TabIndex = 7;
            this.button1.Text = "Call with Exception";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 786);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxUrl2);
            this.Controls.Add(this.txtBoxUrl1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxUrl1;
        private System.Windows.Forms.TextBox txtBoxUrl2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

