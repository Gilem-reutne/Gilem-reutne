// подпункты "Файл, Правка и Справка" создавались вручную ввиду бага среды rider, учти это
using System.ComponentModel;

namespace LibraryApp;

partial class EditForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        components = new System.ComponentModel.Container();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        groupBox1 = new System.Windows.Forms.GroupBox();
        cmbGenre = new System.Windows.Forms.ComboBox();
        chkIsAvailable = new System.Windows.Forms.CheckBox();
        txtPrice = new System.Windows.Forms.MaskedTextBox();
        numYear = new System.Windows.Forms.NumericUpDown();
        txtAuthor = new System.Windows.Forms.TextBox();
        txtTitle = new System.Windows.Forms.TextBox();
        errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
        btnSave = new System.Windows.Forms.Button();
        btnCancel = new System.Windows.Forms.Button();
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numYear).BeginInit();
        ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Location = new System.Drawing.Point(12, 9);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(100, 23);
        label1.TabIndex = 0;
        label1.Text = "Название";
        // 
        // label2
        // 
        label2.Location = new System.Drawing.Point(12, 34);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(100, 23);
        label2.TabIndex = 1;
        label2.Text = "Автор";
        // 
        // label3
        // 
        label3.Location = new System.Drawing.Point(12, 60);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(100, 23);
        label3.TabIndex = 2;
        label3.Text = "Год";
        // 
        // label4
        // 
        label4.Location = new System.Drawing.Point(12, 84);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(100, 23);
        label4.TabIndex = 3;
        label4.Text = "Цена";
        // 
        // label5
        // 
        label5.Location = new System.Drawing.Point(12, 110);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(100, 23);
        label5.TabIndex = 4;
        label5.Text = "Жанр";
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(cmbGenre);
        groupBox1.Controls.Add(chkIsAvailable);
        groupBox1.Controls.Add(txtPrice);
        groupBox1.Controls.Add(numYear);
        groupBox1.Controls.Add(txtAuthor);
        groupBox1.Controls.Add(txtTitle);
        groupBox1.Location = new System.Drawing.Point(118, 1);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(112, 168);
        groupBox1.TabIndex = 5;
        groupBox1.TabStop = false;
        groupBox1.Text = "groupBox1";
        // 
        // cmbGenre
        // 
        cmbGenre.FormattingEnabled = true;
        cmbGenre.Location = new System.Drawing.Point(6, 109);
        cmbGenre.Name = "cmbGenre";
        cmbGenre.Size = new System.Drawing.Size(100, 23);
        cmbGenre.TabIndex = 7;
        // 
        // chkIsAvailable
        // 
        chkIsAvailable.Location = new System.Drawing.Point(6, 138);
        chkIsAvailable.Name = "chkIsAvailable";
        chkIsAvailable.Size = new System.Drawing.Size(104, 24);
        chkIsAvailable.TabIndex = 6;
        chkIsAvailable.Text = "В наличии";
        chkIsAvailable.UseVisualStyleBackColor = true;
        // 
        // txtPrice
        // 
        txtPrice.Location = new System.Drawing.Point(6, 83);
        txtPrice.Mask = "00000";
        txtPrice.Name = "txtPrice";
        txtPrice.Size = new System.Drawing.Size(100, 23);
        txtPrice.TabIndex = 3;
        // 
        // numYear
        // 
        numYear.Location = new System.Drawing.Point(6, 58);
        numYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
        numYear.Name = "numYear";
        numYear.Size = new System.Drawing.Size(100, 23);
        numYear.TabIndex = 2;
        // 
        // txtAuthor
        // 
        txtAuthor.Location = new System.Drawing.Point(6, 33);
        txtAuthor.Name = "txtAuthor";
        txtAuthor.Size = new System.Drawing.Size(100, 23);
        txtAuthor.TabIndex = 1;
        // 
        // txtTitle
        // 
        txtTitle.Location = new System.Drawing.Point(6, 8);
        txtTitle.Name = "txtTitle";
        txtTitle.Size = new System.Drawing.Size(100, 23);
        txtTitle.TabIndex = 0;
        // 
        // errorProvider1
        // 
        errorProvider1.ContainerControl = this;
        // 
        // btnSave
        // 
        btnSave.Location = new System.Drawing.Point(12, 189);
        btnSave.Name = "btnSave";
        btnSave.Size = new System.Drawing.Size(75, 23);
        btnSave.TabIndex = 6;
        btnSave.Text = "Сохранить";
        btnSave.UseVisualStyleBackColor = true;
        // 
        // btnCancel
        // 
        btnCancel.Location = new System.Drawing.Point(149, 188);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(75, 23);
        btnCancel.TabIndex = 7;
        btnCancel.Text = "Отменить";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // EditForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(239, 224);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(groupBox1);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Text = "EditForm";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numYear).EndInit();
        ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;

    private System.Windows.Forms.ErrorProvider errorProvider1;

    private System.Windows.Forms.ComboBox cmbGenre;

    private System.Windows.Forms.CheckBox chkIsAvailable;

    private System.Windows.Forms.MaskedTextBox txtPrice;

    private System.Windows.Forms.NumericUpDown numYear;

    private System.Windows.Forms.TextBox txtTitle;
    private System.Windows.Forms.TextBox txtAuthor;

    private System.Windows.Forms.GroupBox groupBox1;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;

    #endregion
}
