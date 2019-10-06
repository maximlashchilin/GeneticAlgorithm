namespace GeneticEngine
{
  partial class FormGeneticEngine
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.textBoxResultX = new System.Windows.Forms.TextBox();
      this.textBoxResultY = new System.Windows.Forms.TextBox();
      this.buttonRun = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.textBoxResultX, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.textBoxResultY, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.buttonRun, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.55556F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.44444F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // textBoxResultX
      // 
      this.textBoxResultX.Location = new System.Drawing.Point(3, 3);
      this.textBoxResultX.Name = "textBoxResultX";
      this.textBoxResultX.Size = new System.Drawing.Size(155, 20);
      this.textBoxResultX.TabIndex = 0;
      // 
      // textBoxResultY
      // 
      this.textBoxResultY.Location = new System.Drawing.Point(403, 3);
      this.textBoxResultY.Name = "textBoxResultY";
      this.textBoxResultY.Size = new System.Drawing.Size(100, 20);
      this.textBoxResultY.TabIndex = 1;
      // 
      // buttonRun
      // 
      this.buttonRun.Location = new System.Drawing.Point(3, 73);
      this.buttonRun.Name = "buttonRun";
      this.buttonRun.Size = new System.Drawing.Size(75, 23);
      this.buttonRun.TabIndex = 2;
      this.buttonRun.Text = "button1";
      this.buttonRun.UseVisualStyleBackColor = true;
      this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
      // 
      // FormGeneticEngine
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "FormGeneticEngine";
      this.Text = "Genetic algorithm";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TextBox textBoxResultX;
    private System.Windows.Forms.TextBox textBoxResultY;
    private System.Windows.Forms.Button buttonRun;
  }
}

