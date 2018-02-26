using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper_in_Forms
{
  /// <summary>
  /// Enthält die Funktionalitäten der Form
  /// </summary>
  public partial class Form1 : Form
  {
    #region Member
    
    /// <summary>
    /// Enthält das Spiel das durch das Auswählen des Schwierigkeitsgrades erstellt wurde
    /// </summary>
    public MinesweeperSpiel MinesweeperSpiel;

    private Button[,] _buttons;

    private const int MINBUTTONSIZE = 24;
    private const int BUTTONSPACING = 6;

    #endregion Member

    /// <summary>
    /// Initialisiert alle Komponenten
    /// </summary>
    public Form1()
    {
      InitializeComponent();
      ZentriereLevelButton();
    }

    private void ZentriereLevelButton()
    {
      level2Button.Location = new Point(ClientSize.Width / 2 - level2Button.Width / 2, level2Button.Location.Y);
      level1Button.Location = new Point(level2Button.Location.X - level1Button.Width - 10, level2Button.Location.Y);
      level3Button.Location = new Point(level2Button.Location.X + level1Button.Width + 10, level2Button.Location.Y);
    }

    private void level1Button_Click(object sender, EventArgs e)
    {
      NeuesSpiel(10, 10, 10);
    }

    private void level2Button_Click(object sender, EventArgs e)
    {
      NeuesSpiel(20, 20, 20);
    }

    private void level3Button_Click(object sender, EventArgs e)
    {
      NeuesSpiel(30, 20, 25);
    }

    private void NeuesSpiel(int breite, int hoehe, int minen)
    {
      MinesweeperSpiel = new MinesweeperSpiel(hoehe, breite, minen);
      _buttons = new Button[hoehe, breite];

      FuegeButtonsNeuEin();
      ZentriereFlowPanelLayout();
    }

    private void FuegeButtonsNeuEin()
    {
      if (MinesweeperSpiel == null)
        return;

      flowLayoutPanel1.Controls.Clear();
      MinimumSize = MinSize();

      for (int reihe = 0; reihe < MinesweeperSpiel.Hoehe; reihe++)
      {
        for (int spalte = 0; spalte < MinesweeperSpiel.Breite; spalte++)
        {
          Button button = new Button();
          button.Size = ButtonSize();
          button.Text = GibZeichenZurück(MinesweeperSpiel[reihe, spalte]);
          button.Click += new EventHandler(ButtonClicked);
          button.MouseDown += new MouseEventHandler(SelectFlag);
          button.Tag = new int[2] { reihe, spalte };
          button.BackColor = GetFieldColor(MinesweeperSpiel[reihe, spalte]);
          flowLayoutPanel1.Controls.Add(button);
          _buttons[reihe, spalte] = button;
        }
      }
      flowLayoutPanel1.Size = FlowLayoutPanelSize();
    }

    private void ZentriereFlowPanelLayout()
    {
      flowLayoutPanel1.Location = new Point(level2Button.Location.X + level2Button.Width / 2 - flowLayoutPanel1.Width / 2, flowLayoutPanel1.Location.Y);
    }

    private Size FlowLayoutPanelSize()
    {
      int buttonHeight = ButtonSize().Height;
      return new Size(MinesweeperSpiel.Breite * (buttonHeight + BUTTONSPACING), MinesweeperSpiel.Hoehe * (buttonHeight + BUTTONSPACING));
    }

    private Size MinSize()
    {
      return new Size(MinesweeperSpiel.Breite * (MINBUTTONSIZE + BUTTONSPACING), MinesweeperSpiel.Hoehe * (MINBUTTONSIZE + BUTTONSPACING) + flowLayoutPanel1.Location.Y);
    }

    private Size ButtonSize()
    {
      int buttonSize = 0;
      int buttonSizeH = (ClientSize.Height - flowLayoutPanel1.Location.Y) / MinesweeperSpiel.Hoehe - BUTTONSPACING;
      int buttonSizeW = (ClientSize.Width) / MinesweeperSpiel.Breite - BUTTONSPACING;

      if (buttonSizeH < buttonSizeW)
        buttonSize = buttonSizeH;
      else
        buttonSize = buttonSizeW;
      return new Size(buttonSize, buttonSize);
    }

    private void ButtonClicked(Object sender, EventArgs e)
    {
      Button button = (Button)sender;
      int[] data = (int[])button.Tag;
      try
      {
        MinesweeperSpiel.DeckeAuf(data[0], data[1]);
      }
      catch (InvalidOperationException) { }

      ZeichneSpielfeldNeu();
      if (MinesweeperSpiel.SpielGewonnen())
        MessageBox.Show("Gewonnen");
    }

    private void SelectFlag(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      Button button = (Button)sender;
      int[] data = (int[])button.Tag;
      MinesweeperSpiel.SetzeFahnenMarkierung(data[0], data[1]);
      ZeichneSpielfeldNeu();
    }

    private void ZeichneSpielfeldNeu()
    {
      for (int reihe = 0; reihe < MinesweeperSpiel.Hoehe; reihe++)
      {
        for (int spalte = 0; spalte < MinesweeperSpiel.Breite; spalte++)
        {
          _buttons[reihe, spalte].Text = GibZeichenZurück(MinesweeperSpiel[reihe, spalte]);
          _buttons[reihe, spalte].BackColor = GetFieldColor(MinesweeperSpiel[reihe, spalte]);
        }
      }
    }

    private Color GetFieldColor(Feld feld)
    {
      switch (feld)
      {
        case Feld.Unbekannt:
          return Color.Goldenrod;
        case Feld.Fahne:
          return Color.IndianRed;
        case Feld.Mine:
          return Color.Indigo;
        case Feld.N0:
          return Color.Silver;
        default:
          {
            return Color.Snow;
          }
      }
    }

    private string GibZeichenZurück(Feld feld)
    {
      switch (feld)
      {
        case Feld.Unbekannt:
          return " ";
        case Feld.Fahne:
          return "P";
        case Feld.Mine:
          return " ";
        default:
          {
            int minenInNachbarschaft = (int)feld;
            return minenInNachbarschaft.ToString();
          }
      }
    }

    private void Form1_ResizeEnd(object sender, EventArgs e)
    {
      FuegeButtonsNeuEin();
      ZentriereFlowPanelLayout();
    }
  }
}