namespace WinFormsApp1
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
            buttonAumenta = new Button();
            labelContatore = new Label();
            buttonDiminuisci = new Button();
            labelCont = new Label();
            buttonReset = new Button();
            numerouno = new TextBox();
            numerodue = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            labelrisultato = new Label();
            SuspendLayout();
            // 
            // buttonAumenta
            // 
            buttonAumenta.Location = new Point(12, 12);
            buttonAumenta.Name = "buttonAumenta";
            buttonAumenta.Size = new Size(127, 38);
            buttonAumenta.TabIndex = 0;
            buttonAumenta.Text = "Aumenta";
            buttonAumenta.UseVisualStyleBackColor = true;
            buttonAumenta.Click += buttonAumenta_Click;
            // 
            // labelContatore
            // 
            labelContatore.AutoSize = true;
            labelContatore.Location = new Point(115, 65);
            labelContatore.Name = "labelContatore";
            labelContatore.Size = new Size(13, 15);
            labelContatore.TabIndex = 1;
            labelContatore.Text = "0";
            labelContatore.Click += labelContatore_Click;
            // 
            // buttonDiminuisci
            // 
            buttonDiminuisci.Location = new Point(160, 14);
            buttonDiminuisci.Name = "buttonDiminuisci";
            buttonDiminuisci.Size = new Size(132, 36);
            buttonDiminuisci.TabIndex = 2;
            buttonDiminuisci.Text = "Diminuisci";
            buttonDiminuisci.UseVisualStyleBackColor = true;
            buttonDiminuisci.Click += buttonDiminuisci_Click;
            // 
            // labelCont
            // 
            labelCont.AutoSize = true;
            labelCont.Location = new Point(18, 65);
            labelCont.Name = "labelCont";
            labelCont.Size = new Size(60, 15);
            labelCont.TabIndex = 3;
            labelCont.Text = "Contatore";
            labelCont.Click += labelCont_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(317, 17);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(137, 34);
            buttonReset.TabIndex = 4;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // numerouno
            // 
            numerouno.Location = new Point(20, 126);
            numerouno.Name = "numerouno";
            numerouno.Size = new Size(108, 23);
            numerouno.TabIndex = 5;
            numerouno.TextChanged += numerouno_TextChanged;
            // 
            // numerodue
            // 
            numerodue.Location = new Point(160, 126);
            numerodue.Name = "numerodue";
            numerodue.Size = new Size(132, 23);
            numerodue.TabIndex = 6;
            numerodue.TextChanged += numerodue_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(319, 122);
            button1.Name = "button1";
            button1.Size = new Size(51, 28);
            button1.TabIndex = 7;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(403, 122);
            button2.Name = "button2";
            button2.Size = new Size(60, 31);
            button2.TabIndex = 8;
            button2.Text = "-";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(492, 120);
            button3.Name = "button3";
            button3.Size = new Size(59, 35);
            button3.TabIndex = 9;
            button3.Text = "*";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(586, 122);
            button4.Name = "button4";
            button4.Size = new Size(78, 35);
            button4.TabIndex = 10;
            button4.Text = "/";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // labelrisultato
            // 
            labelrisultato.AutoSize = true;
            labelrisultato.Location = new Point(416, 212);
            labelrisultato.Name = "labelrisultato";
            labelrisultato.Size = new Size(13, 15);
            labelrisultato.TabIndex = 11;
            labelrisultato.Text = "0";
            labelrisultato.Click += labelrisultato_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelrisultato);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(numerodue);
            Controls.Add(numerouno);
            Controls.Add(buttonReset);
            Controls.Add(labelCont);
            Controls.Add(buttonDiminuisci);
            Controls.Add(labelContatore);
            Controls.Add(buttonAumenta);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAumenta;
        private Label labelContatore;
        private Button buttonDiminuisci;
        private Label labelCont;
        private Button buttonReset;
        private TextBox numerouno;
        private TextBox numerodue;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Label labelrisultato;
    }
}