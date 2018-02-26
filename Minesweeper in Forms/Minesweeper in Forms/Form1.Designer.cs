namespace Minesweeper_in_Forms
{
  partial class Form1
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.level1Button = new System.Windows.Forms.Button();
      this.level2Button = new System.Windows.Forms.Button();
      this.level3Button = new System.Windows.Forms.Button();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.SuspendLayout();
      // 
      // level1Button
      // 
      this.level1Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.level1Button.Location = new System.Drawing.Point(237, 12);
      this.level1Button.Name = "level1Button";
      this.level1Button.Size = new System.Drawing.Size(75, 23);
      this.level1Button.TabIndex = 0;
      this.level1Button.Text = "Leicht";
      this.level1Button.UseVisualStyleBackColor = true;
      this.level1Button.Click += new System.EventHandler(this.level1Button_Click);
      // 
      // level2Button
      // 
      this.level2Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.level2Button.Location = new System.Drawing.Point(318, 12);
      this.level2Button.Name = "level2Button";
      this.level2Button.Size = new System.Drawing.Size(75, 23);
      this.level2Button.TabIndex = 1;
      this.level2Button.Text = "Mittel";
      this.level2Button.UseVisualStyleBackColor = true;
      this.level2Button.Click += new System.EventHandler(this.level2Button_Click);
      // 
      // level3Button
      // 
      this.level3Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.level3Button.Location = new System.Drawing.Point(399, 12);
      this.level3Button.Name = "level3Button";
      this.level3Button.Size = new System.Drawing.Size(75, 23);
      this.level3Button.TabIndex = 2;
      this.level3Button.Text = "Schwer";
      this.level3Button.UseVisualStyleBackColor = true;
      this.level3Button.Click += new System.EventHandler(this.level3Button_Click);
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(98, 41);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(505, 502);
      this.flowLayoutPanel1.TabIndex = 5;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(789, 689);
      this.Controls.Add(this.flowLayoutPanel1);
      this.Controls.Add(this.level3Button);
      this.Controls.Add(this.level2Button);
      this.Controls.Add(this.level1Button);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
      this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Button level1Button;
    private System.Windows.Forms.Button level2Button;
    private System.Windows.Forms.Button level3Button;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
  }
}

