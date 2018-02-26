using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper_in_Forms;

namespace MinseweeperTest
{
  /// <summary>
  /// Testet die Funktionsweise des Spiels
  /// </summary>
  [TestFixture]
  class MinesweeperTester
  {
    /// <summary>
    /// check if the creation of the parameters is 
    /// </summary>
    [Test]
    public void InvalidCreation()
    {
      // given /when /then
      Assert.That(() => new MinesweeperSpiel(9, 16, 5), Throws.ArgumentException); // check invalid row excepton

      Assert.That(() => new MinesweeperSpiel(10, 5, 5), Throws.ArgumentException); // check invalid column exception

      Assert.That(() => new MinesweeperSpiel(10, 10, 0), Throws.ArgumentException); // check invalid mines exception
    }


    /// <summary>
    /// Chekck if the construction of the field is working
    /// </summary>
    [Test]
    public void Construction()
    {
      //given 
      int width = 15;
      int height = 15;
      int mines = 7;

      //when
      MinesweeperSpiel minesweeperSpiel = new MinesweeperSpiel(width, height, mines);

      //Than
      Assert.That(minesweeperSpiel.Hoehe, Is.EqualTo(height));
      Assert.That(minesweeperSpiel.Breite, Is.EqualTo(width));

      for (int reihe = 0; reihe < minesweeperSpiel.Hoehe; reihe++)
      {
        for (int spalte = 0; spalte < minesweeperSpiel.Breite; spalte++)
        {
          Assert.That(minesweeperSpiel[reihe, spalte], Is.EqualTo(Feld.Unbekannt));
        }
      }
    }

    /// <summary>
    /// Check if the mines have been set 
    /// </summary>
    [Test]
    public void SetRandomMines()
    {
      //given
      int width = 15;
      int height = 15;
      int mines = 30;

      //when
      MinesweeperSpiel minesweeperSpiel = new MinesweeperSpiel(width, height, mines);

      //Than
      Assert.That(mines, Is.EqualTo(minesweeperSpiel.AnzahlMinen()));
    }

    private int[,] testField;
    private void InitializeTestField()
    {
      int height = 5;
      int width = 6;

      testField = new int[height, width];

      testField[2, 2] = -1;
      testField[2, 3] = -1;
    }

    /// <summary>
    /// Check if the fields Around an 0 Field will be opended
    /// </summary>
    [Test]
    public void CheckOpenFields()
    {
      //Given
      int rowCoordiante = 0;
      int columCoodinate = 0;

      InitializeTestField();
      int height = testField.GetLength(0);
      int width = testField.GetLength(1);

      //When
      MinesweeperSpiel minesweeperSpiel = new MinesweeperSpiel(testField);
      minesweeperSpiel.DeckeAuf(rowCoordiante, columCoodinate);

      //Than
      for (int row = 0; row < height; row++)
      {
        for (int colum = 0; colum < width; colum++)
        {
          if (minesweeperSpiel.Inhalt(row, colum) != Feld.Mine)
            Assert.That(minesweeperSpiel.IstAufgedeckt(row, colum), Is.EqualTo(true));
        }
      }
    }


    [Test]
    public void OpelAllFields()
    {
      //given
      int height = 15;
      int width = 15;
      int mines = 1;

      //When
      MinesweeperSpiel minesweeperSpiel = new MinesweeperSpiel(height, width, mines);
      minesweeperSpiel.DeckeAlleFelderAuf();

      //Than
      for (int row = 0; row < height; row++)
      {
        for (int colum = 0; colum < width; colum++)
        {
          Assert.That(minesweeperSpiel.IstAufgedeckt(row, colum), Is.EqualTo(true));
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    [Test]
    public void SetFlag()
    {
      // Given
      int rowCoordiante = 0;
      int columnCoodinate = 0;
      InitializeTestField();

      // When
      MinesweeperSpiel minesweeperSpiel = new MinesweeperSpiel(testField);

      // Set Flag /Than Field is Market with Flag
      minesweeperSpiel.SetzeFahnenMarkierung(rowCoordiante, columnCoodinate);
      Assert.That(minesweeperSpiel[rowCoordiante, columnCoodinate], Is.EqualTo(Feld.Fahne));
      // Set Flag at Flag that Market Field /Than Flag is Removed from Field
      minesweeperSpiel.SetzeFahnenMarkierung(rowCoordiante, columnCoodinate);
      Assert.That(minesweeperSpiel[rowCoordiante, columnCoodinate], Is.EqualTo(Feld.Unbekannt));

      // open Field that is marked with Flag
      minesweeperSpiel.DeckeAuf(0, 4);
      Feld zustand = minesweeperSpiel[0, 4];
      minesweeperSpiel.SetzeFahnenMarkierung(4, 0);

      // Than wont be marked with flag 
      Assert.That(minesweeperSpiel[0, 4], Is.EqualTo(zustand));
    }

    [Test]
    public void OpenField()
    {
      //Given
      InitializeTestField();
      MinesweeperSpiel minesweeperSpiel = new MinesweeperSpiel(testField);

      //When /Than 
      Assert.That(() => minesweeperSpiel.DeckeAuf(2, 2), Throws.InvalidOperationException);
      //When  /Than 
      Assert.That(() => minesweeperSpiel.DeckeAuf(20, 20), Throws.ArgumentException);

      //When 
      minesweeperSpiel.DeckeAuf(0, 0);
      Feld zustand = minesweeperSpiel[0,0];
      minesweeperSpiel.DeckeAuf(0, 0);
      //Than 
      Assert.That(minesweeperSpiel[0, 4], Is.EqualTo(zustand));
    }

    [Test]
    public void FieldIndexer()
    {
      //Given
      InitializeTestField();
      MinesweeperSpiel minesweeperSpiel = new MinesweeperSpiel(testField);

      //When /Than
      minesweeperSpiel.SetzeFahnenMarkierung(0, 0);
      Assert.That(minesweeperSpiel[0, 0], Is.EqualTo(Feld.Fahne));

      //When /Than
      minesweeperSpiel.DeckeAuf(1, 1);
      Assert.That(minesweeperSpiel[1, 1], Is.EqualTo(Feld.N1));
    }

    [Test]
    public void TestIfCoodinateIsInRowAndColumn()
    {
      //Given
      int height = 15;
      int width = 15;
      int mines = 1;
      //When
      MinesweeperSpiel minesweeperSpiel = new MinesweeperSpiel(height, width, mines);
      //Than
      Assert.That(minesweeperSpiel.IstKoordianteInReihe(16), Is.EqualTo(false));
      Assert.That(minesweeperSpiel.IstKoordianteInSpalte(16), Is.EqualTo(false));
    }

    /// <summary>
    /// Check if the Winning condition is working
    /// </summary>
    [Test]
    public void Won()
    {
      //given
      int width = 20;
      int height = 20;
      int rowCoordinate = 6;
      int columCoordinate = 7;

      int[,] spielfeld = new int[height, width];
      spielfeld[rowCoordinate, columCoordinate] = -1;
      //when /Than
      MinesweeperSpiel minesweeperSpiel = new MinesweeperSpiel(spielfeld);
      minesweeperSpiel.DeckeAuf(0, 0);
      Assert.That(minesweeperSpiel.SpielGewonnen(), Is.EqualTo(true));
    }
  }
}
