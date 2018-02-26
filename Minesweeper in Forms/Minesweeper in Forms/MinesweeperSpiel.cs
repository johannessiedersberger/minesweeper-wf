using System;
using System.Diagnostics;

namespace Minesweeper_in_Forms
{
  /// <summary>
  /// Enthält den Status des Feldes
  /// </summary>
  public enum Feld
  {
    /// <summary>
    /// Nicht Aufgedecktes Feld
    /// </summary>
    Unbekannt = -3,
    /// <summary>
    /// Feld das mit Fahne markiert ist
    /// </summary>
    Fahne = -2,
    /// <summary>
    /// Feld auf dem eine Mine liegt
    /// </summary>
    Mine = -1,
    /// <summary>
    /// Feld um das keine Minen liegen
    /// </summary>
    N0 = 0,
    /// <summary>
    /// Feld um das eine Mine liegt
    /// </summary>
    N1,
    /// <summary>
    /// Feld um das zwei Minen liegen
    /// </summary>
    N2,
    /// <summary>
    /// Feld um das drei Minen liegen
    /// </summary>
    N3,
    /// <summary>
    /// Feld um das vier Minen liegen
    /// </summary>
    N4,
    /// <summary>
    /// Feld um das fünf Minen liegen
    /// </summary>
    N5,
    /// <summary>
    /// Feld um das sechs Minen liegen
    /// </summary>
    N6,
    /// <summary>
    /// Feld um das sieben Minen liegen
    /// </summary>
    N7,
    /// <summary>
    /// Feld um das Acht Minen liegen
    /// </summary>
    N8,
  }

  /// <summary>
  /// Enthält die komplette Spiellogik
  /// </summary>
  public class MinesweeperSpiel
  {
    #region Properties

    /// <summary>
    /// Gibt die Zahl zurück die für den Inhalt des Feldes steht(-3 bis 8)
    /// </summary>
    /// <param name="reihe">Koordinate der Reihe</param>
    /// <param name="spalte">Koordiante der Spalte</param>
    /// <returns></returns>
    public Feld this[int reihe, int spalte]
    {
      get
      {
        if (_istMitFahneMarkiert[reihe, spalte])
          return Feld.Fahne;

        if (_istAufgedeckt[reihe, spalte])
          return (Feld)_inhaltFeld[reihe, spalte];
        else
          return Feld.Unbekannt;
      }
    }

    /// <summary>
    /// Gibt den Inhalt des Felder zurück
    /// </summary>
    /// <param name="reihe"></param>
    /// <param name="spalte"></param>
    /// <returns></returns>
    public Feld Inhalt(int reihe, int spalte)
    {
      return (Feld)_inhaltFeld[reihe, spalte];
    }

    /// <summary>
    /// Gibt true zurück wenn das Feld aufgedeckt wurde
    /// </summary>
    /// <param name="reihe"></param>
    /// <param name="spalte"></param>
    /// <returns></returns>
    public bool IstAufgedeckt(int reihe, int spalte)
    {
      return _istAufgedeckt[reihe, spalte];
    }

    /// <summary>
    /// Gibt die Hoehe des Felder zurück
    /// </summary>
    public int Hoehe
    {
      get { return _inhaltFeld.GetLength(0); }
    }

    /// <summary>
    /// Gibt die Breite des Feldes zurücl
    /// </summary>
    public int Breite
    {
      get { return _inhaltFeld.GetLength(1); }
    }

    #endregion Properties

    #region Member

    private bool[,] _istAufgedeckt;
    private int[,] _inhaltFeld;
    private bool[,] _istMitFahneMarkiert;

    #endregion Member

    /// <summary>
    /// Initialisirt ein Testbares Spielfeld
    /// </summary>
    /// <param name="inhaltFeld">Spielfeld das zum Testen verwendet wird</param>
    /// 
    internal MinesweeperSpiel(int[,] inhaltFeld)
    {
      int height = inhaltFeld.GetLength(0);
      int width = inhaltFeld.GetLength(1);

      _inhaltFeld = new int[height, width];
      _istMitFahneMarkiert = new bool[height, width];
      _istAufgedeckt = new bool[height, width];

      _inhaltFeld = inhaltFeld;
      SetzeZahlenUmMinen();
    }

    /// <summary>
    /// Initialisiert das Spielfeld
    /// </summary>
    /// <param name="hoehe">Hoehe des Spielfeldes </param>
    /// <param name="breite">Breite des Spielfeldes</param>
    /// <param name="minen">Anzal der Minen</param>
    public MinesweeperSpiel(int hoehe, int breite, int minen)
    {
      if (hoehe < 10 || hoehe > 30 || breite < 10 || breite > 30)
        throw new ArgumentException("Breite/Höhe sind nicht im erlaubten Bereich");

      if (minen <= 0 || minen >= hoehe * breite)
        throw new ArgumentException("Die Anzahl der Minen ist nicht im erlaubten Breich");

      InitialisiereSpielfeld(hoehe, breite, minen);
    }

    private void InitialisiereSpielfeld(int reihe, int spalte, int minen)
    {
      _istMitFahneMarkiert = new bool[reihe, spalte];
      _istAufgedeckt = new bool[reihe, spalte];
      _inhaltFeld = new int[reihe, spalte];

      SetzeZufälligeMinen(minen);
      SetzeZahlenUmMinen();
    }

    private int gesetzteMinen;
    private void SetzeZufälligeMinen(int minenAnzahl)
    {
      Random random = new Random(DateTime.Now.Ticks.GetHashCode());
      while (gesetzteMinen < minenAnzahl)
      {
        int reihe = random.Next(0, Hoehe - 1);
        int spalte = random.Next(0, Breite - 1);
        if (_inhaltFeld[reihe, spalte] != -1)
        {
          _inhaltFeld[reihe, spalte] = -1;
          gesetzteMinen++;
        }        
      }
    }

    private void SetzeZahlenUmMinen()
    {
      for (int reihe = 0; reihe < Hoehe; reihe++)
      {
        for (int spalte = 0; spalte < Breite; spalte++)
        {
          if (!IstMine(reihe, spalte))
          {
            _inhaltFeld[reihe, spalte] = MinenInDerNachbarschaftVon(reihe, spalte);
          }
        }
      }
    }

    /// <summary>
    /// Gibt die Anzahl der Minen in der Umgebung eines Felder zurück
    /// </summary>
    /// <param name="reihe"></param>
    /// <param name="spalte"></param>
    /// <returns></returns>
    public int MinenInDerNachbarschaftVon(int reihe, int spalte)
    {
      Debug.Assert(!IstMine(reihe, spalte));

      int anzahlMinenNachbarschaft = 0;
      for (int kernelY = reihe - 1; kernelY <= reihe + 1; kernelY++)
      {
        for (int kernelX = spalte - 1; kernelX <= spalte + 1; kernelX++)
        {
          if (IstMine(kernelY, kernelX))
            anzahlMinenNachbarschaft++;
        }
      }
      return anzahlMinenNachbarschaft;
    }

    private bool IstMine(int reihe, int spalte)
    {
      return IstInnerhalbDesFeldes(reihe, spalte) && _inhaltFeld[reihe, spalte] < 0;
    }

    /// <summary>
    /// Checkt ob die Koordinate für die Reihe verfügbar ist
    /// </summary>
    /// <param name="koordinate"></param>
    /// <returns>Gibt true zurück wenn der Wert im Parameter in die reihe passt</returns>
    internal bool IstKoordianteInReihe(int koordinate)
    {
      return (IstInnerhalbDesFeldes(koordinate, 0));
    }

    /// <summary>
    /// Checkt ob die Koordinate für die Spalte verfügbar ist
    /// </summary>
    /// <param name="koordinate"></param>
    /// <returns>Gibt true zurück wenn der Wert im Parameter in die Spalte passt</returns>
    internal bool IstKoordianteInSpalte(int koordinate)
    {
      return IstInnerhalbDesFeldes(0, koordinate);
    }

    /// <summary>
    /// Gibt true zurück wenn der Wert im Parameter sich innerhalb des Feldes befindet
    /// </summary>
    /// <param name="reihe"></param>
    /// <param name="spalte"></param>
    /// <returns></returns>
    public bool IstInnerhalbDesFeldes(int reihe, int spalte)
    {
      return IstInBereich(reihe, min: 0, max: Hoehe - 1)
        && IstInBereich(spalte, min: 0, max: Breite - 1);

    }

    private static bool IstInBereich(int test, int min, int max)
    {
      return test >= min && test <= max;
    }

    /// <summary>
    /// Deckt die Felder auf und Setzt die Anzahl der Fahnen
    /// </summary>
    /// <param name="reihe">Koordinate der Reihe</param>
    /// <param name="spalte">Koordinate der Spalte</param>
    public void DeckeAuf(int reihe, int spalte)
    {
      if (!IstInnerhalbDesFeldes(reihe, spalte))
        throw new ArgumentException("Koordinate außerhalb des Spiefelds");

      if (_istAufgedeckt[reihe, spalte] || _istMitFahneMarkiert[reihe, spalte])
        return;

      if (IstMine(reihe, spalte))
      {
        DeckeAlleFelderAuf();
        throw new InvalidOperationException("Du hast eine Mine getroffen :(");
      }
      DeckeFlaecheAuf(reihe, spalte);
    }

    internal void DeckeAlleFelderAuf()
    {
      for (int reihe = 0; reihe < Hoehe; reihe++)
      {
        for (int spalte = 0; spalte < Breite; spalte++)
        {
          _istAufgedeckt[reihe, spalte] = true;
          _istMitFahneMarkiert[reihe, spalte] = false;
        }
      }
    }

    private void DeckeFlaecheAuf(int reihe, int spalte)
    {
      if (!IstInnerhalbDesFeldes(reihe, spalte) || _istAufgedeckt[reihe, spalte])
        return;

      _istAufgedeckt[reihe, spalte] = true;

      if (_inhaltFeld[reihe, spalte] != 0)
        return;

      for (int linksRechts = -1; linksRechts <= 1; linksRechts++)
      {
        for (int obenUnten = -1; obenUnten <= 1; obenUnten++)
        {
          DeckeFlaecheAuf(reihe + linksRechts, spalte + obenUnten);
        }
      }
    }

    /// <summary>
    /// Setzt oder Entfernt eine Fahne
    /// </summary>
    /// <param name="reihe">Koordinate der Reihe</param>
    /// <param name="spalte">Koordiante der Spalte</param>
    public void SetzeFahnenMarkierung(int reihe, int spalte)
    {
      if (_istAufgedeckt[reihe, spalte] && !_istMitFahneMarkiert[reihe, spalte])
        return;

      _istMitFahneMarkiert[reihe, spalte] = !_istMitFahneMarkiert[reihe, spalte];
    }

    /// <summary>
    /// Fragt ab ob das Spiel gewonnen wurde
    /// </summary>
    /// <returns>Gibt true zurück wenn die Anzahl der Ungeöffneten Felder Ohne Minen 0 ist</returns>
    public bool SpielGewonnen()
    {
      return AnzahlUngeöffneteFelderOhneMinen() == 0;
    }

    /// <summary>
    /// Gibt die Anzahl der Ungeöffneten Felder ohne Minen zurück
    /// </summary>
    /// <returns></returns>
    public int AnzahlUngeöffneteFelderOhneMinen()
    {
      int genöffneteFelder = 0;
      for (int reihe = 0; reihe < Hoehe; reihe++)
      {
        for (int spalte = 0; spalte < Breite; spalte++)
        {
          if (_istAufgedeckt[reihe, spalte])
            genöffneteFelder++;
        }
      }
      return (Hoehe * Breite) - (AnzahlMinen() + genöffneteFelder);
    }

    /// <summary>
    /// Gibt die Anzahl der Minen im Spielfeld zurück
    /// </summary>
    /// <returns></returns>
    public int AnzahlMinen()
    {
      int anzahlMinen = 0;
      for (int reihe = 0; reihe < Hoehe; reihe++)
      {
        for (int spalte = 0; spalte < Breite; spalte++)
        {
          if (IstMine(reihe, spalte))
            anzahlMinen++;
        }
      }
      return anzahlMinen;
    }
  }
}


