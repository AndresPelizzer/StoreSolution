namespace WinFormsApp1
{
    partial class FormDatabase
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            listBox1 = new ListBox();
            button6 = new Button();
            grid = new DataGridView();
            groupBox1 = new GroupBox();
            btnAnnulla = new Button();
            btnElimina = new Button();
            btnSalva = new Button();
            btnNuovo = new Button();
            txtId = new TextBox();
            label1 = new Label();
            txtDescrizione = new TextBox();
            label2 = new Label();
            txtNome = new TextBox();
            Nome = new Label();
            groupBox2 = new GroupBox();
            label3 = new Label();
            textBox1 = new TextBox();
            label4 = new Label();
            textEncrypted = new TextBox();
            buttonEncrypt = new Button();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(1010, 647);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Chiudi";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(77, 29);
            button2.Name = "button2";
            button2.Size = new Size(122, 23);
            button2.TabIndex = 1;
            button2.Text = "TEST INSERT";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(81, 73);
            button3.Name = "button3";
            button3.Size = new Size(118, 23);
            button3.TabIndex = 2;
            button3.Text = "TEST UPDATE";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(84, 126);
            button4.Name = "button4";
            button4.Size = new Size(115, 23);
            button4.TabIndex = 3;
            button4.Text = "TEST DELETE";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(27, 494);
            button5.Name = "button5";
            button5.Size = new Size(172, 23);
            button5.TabIndex = 4;
            button5.Text = "TEST SELECT (connected)";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(27, 523);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(176, 64);
            listBox1.TabIndex = 5;
            // 
            // button6
            // 
            button6.Location = new Point(27, 230);
            button6.Name = "button6";
            button6.Size = new Size(176, 23);
            button6.TabIndex = 6;
            button6.Text = "TEST SELECT (disconnected)";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Location = new Point(245, 29);
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.Size = new Size(501, 542);
            grid.TabIndex = 7;
            grid.RowEnter += grid_RowEnter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnAnnulla);
            groupBox1.Controls.Add(btnElimina);
            groupBox1.Controls.Add(btnSalva);
            groupBox1.Controls.Add(btnNuovo);
            groupBox1.Controls.Add(txtId);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtDescrizione);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtNome);
            groupBox1.Controls.Add(Nome);
            groupBox1.Location = new Point(773, 31);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(304, 337);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Dettaglio";
            // 
            // btnAnnulla
            // 
            btnAnnulla.Location = new Point(195, 283);
            btnAnnulla.Name = "btnAnnulla";
            btnAnnulla.Size = new Size(102, 36);
            btnAnnulla.TabIndex = 9;
            btnAnnulla.Text = "Annulla";
            btnAnnulla.UseVisualStyleBackColor = true;
            btnAnnulla.Click += btnAnnulla_Click;
            // 
            // btnElimina
            // 
            btnElimina.Location = new Point(194, 227);
            btnElimina.Name = "btnElimina";
            btnElimina.Size = new Size(103, 41);
            btnElimina.TabIndex = 8;
            btnElimina.Text = "Elimina";
            btnElimina.UseVisualStyleBackColor = true;
            btnElimina.Click += btnElimina_Click;
            // 
            // btnSalva
            // 
            btnSalva.Location = new Point(34, 279);
            btnSalva.Name = "btnSalva";
            btnSalva.Size = new Size(134, 32);
            btnSalva.TabIndex = 7;
            btnSalva.Text = "Salva";
            btnSalva.UseVisualStyleBackColor = true;
            btnSalva.Click += btnSalva_Click;
            // 
            // btnNuovo
            // 
            btnNuovo.Location = new Point(32, 223);
            btnNuovo.Name = "btnNuovo";
            btnNuovo.Size = new Size(135, 35);
            btnNuovo.TabIndex = 6;
            btnNuovo.Text = "Nuovo";
            btnNuovo.UseVisualStyleBackColor = true;
            btnNuovo.Click += btnNuovo_Click;
            // 
            // txtId
            // 
            txtId.Location = new Point(26, 46);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(100, 23);
            txtId.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 26);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 4;
            label1.Text = "ID";
            // 
            // txtDescrizione
            // 
            txtDescrizione.Location = new Point(36, 171);
            txtDescrizione.Name = "txtDescrizione";
            txtDescrizione.Size = new Size(233, 23);
            txtDescrizione.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 147);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 2;
            label2.Text = "Descrizione";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(30, 109);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(240, 23);
            txtNome.TabIndex = 1;
            // 
            // Nome
            // 
            Nome.AutoSize = true;
            Nome.Location = new Point(26, 86);
            Nome.Name = "Nome";
            Nome.Size = new Size(40, 15);
            Nome.TabIndex = 0;
            Nome.Text = "Nome";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(buttonEncrypt);
            groupBox2.Controls.Add(textEncrypted);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(773, 396);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(311, 174);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Encrypt";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 19);
            label3.Name = "label3";
            label3.Size = new Size(83, 15);
            label3.TabIndex = 0;
            label3.Text = "Stringa iniziale";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(14, 43);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(283, 23);
            textBox1.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 86);
            label4.Name = "label4";
            label4.Size = new Size(83, 15);
            label4.TabIndex = 2;
            label4.Text = "Valore criptato";
            // 
            // textEncrypted
            // 
            textEncrypted.Location = new Point(12, 108);
            textEncrypted.Name = "textEncrypted";
            textEncrypted.Size = new Size(285, 23);
            textEncrypted.TabIndex = 3;
            // 
            // buttonEncrypt
            // 
            buttonEncrypt.Location = new Point(12, 137);
            buttonEncrypt.Name = "buttonEncrypt";
            buttonEncrypt.Size = new Size(75, 23);
            buttonEncrypt.TabIndex = 4;
            buttonEncrypt.Text = "Encrypt";
            buttonEncrypt.UseVisualStyleBackColor = true;
            buttonEncrypt.Click += buttonEncrypt_Click;
            // 
            // FormDatabase
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1110, 682);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(grid);
            Controls.Add(button6);
            Controls.Add(listBox1);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "FormDatabase";
            Text = "FormDatabase";
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private ListBox listBox1;
        private Button button6;
        private DataGridView grid;
        private GroupBox groupBox1;
        private TextBox txtDescrizione;
        private Label label2;
        private TextBox txtNome;
        private Label Nome;
        private TextBox txtId;
        private Label label1;
        private Button btnNuovo;
        private Button btnSalva;
        private Button btnElimina;
        private Button btnAnnulla;
        private GroupBox groupBox2;
        private Button buttonEncrypt;
        private TextBox textEncrypted;
        private Label label4;
        private TextBox textBox1;
        private Label label3;
    }
}