using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CellularAutomaton;
using NUnit.Framework.Interfaces;

public class CellularAutomatonTests
{

    /* Test if the grid throws an exception when
     * instantiated within minimum size constraints
     */
    [Test]
    public void InvalidGridSizeExceptionTest()
    {
        bool exceptionThrown = false;
        try
        {
            var myGrid0 = new CellularAutomaton.Grid(0, 0);
            var myGrid1 = new CellularAutomaton.Grid(1, 1);
            var myGrid2 = new CellularAutomaton.Grid(2, 1);
            var myGrid3 = new CellularAutomaton.Grid(1, 2);
            var myGrid4 = new CellularAutomaton.Grid(2, 2);
        }
        catch (System.ArgumentException e)
        {
            exceptionThrown = true;
            if(e.Message == "Grid must be at least 3x3")
            {
                Assert.Pass();  
            }
            else
            {
                Assert.Fail();
            }
        }
        finally
        { 
            if (!exceptionThrown)
            {
                Assert.Fail();
            }
        }
        
    }

    /* Test if the grid throws an exception when
     * instantiated within maximum size constraints
     */
    [Test]
    public void MaximumGridSizeExceptionTest()
    {
        bool exceptionThrown = false;
        try
        {
            var myGrid0 = new CellularAutomaton.Grid(100, 100);
        }
        catch (System.ArgumentException e)
        {
            exceptionThrown = true;
            if (e.Message == "Grid must be less than 10000 cells")
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        finally
        {
            if (!exceptionThrown)
            {
                Assert.Fail();
            }
        }

    }
    
    /* Test if the grid is initialized correctly
     * within minimum size constraints
     */
    [Test]
    public void MinimumGridSizeInitializationTest()
    {
        CellularAutomaton.Grid myGrid = null;
        try
        {
            myGrid = new CellularAutomaton.Grid(3, 3);
        }
        catch (System.Exception)
        {
            Assert.Fail("Constructor Exception Error");
        }
        finally
        {

            if(myGrid == null) { Assert.Fail("Null Array Error");}

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++){
                    var cell = myGrid[i, j];

                    if (cell == null) { Assert.Fail("Null Cell Error"); }
                    if (cell.Alive) { Assert.Fail("Cell Alive Error"); }
                }
            }
        }
    
    }

    /* Test if the grid generates exception on
     * Out of bounds condition
     */
    [Test]
    public void GridReferenceOutOfBoundsTest()
    {
        CellularAutomaton.Grid myGrid = null;
        try
        {
            myGrid = new CellularAutomaton.Grid(3, 3);
        }
        catch (System.Exception)
        {
            Assert.Fail("Constructor Exception Error");
        }
        finally
        {

            if (myGrid == null) { Assert.Fail("Null Array Error"); }

            try
            {
                var cell = myGrid[4, 3];
            }
            catch (System.IndexOutOfRangeException)
            {
                Assert.Pass();
            }
            catch (System.Exception)
            {
                Assert.Fail("Wrong Exception Thrown");
            }
           
        }

    }

    /* Test if grid cell is alive after
     * owner is added
     */
    [Test]
    public void CellAliveAfterOwnerAddTest()
    {
        CellularAutomaton.Grid myGrid = null;
        try
        {
            myGrid = new CellularAutomaton.Grid(3, 3);
        }
        catch (System.Exception)
        {
            Assert.Fail("Constructor Exception Error");
        }
        finally
        {
            if (myGrid == null) { Assert.Fail("Null Array Error"); }
            var owner = ScriptableObject.CreateInstance<OwnerSO>();
            myGrid[1, 1].Owner = owner;
            Assert.IsTrue(myGrid[1, 1].Alive);
        }
    }

    /* Test if grid cell is dead after
     * owner is removed
     */
    [Test]
    public void CellDeadAfterOwnerNulledTest()
    {
        CellularAutomaton.Grid myGrid = null;
        try
        {
            myGrid = new CellularAutomaton.Grid(3, 3);
        }
        catch (System.Exception)
        {
            Assert.Fail("Constructor Exception Error");
        }
        finally
        {
            if (myGrid == null) { Assert.Fail("Null Array Error"); }
            var owner = ScriptableObject.CreateInstance<OwnerSO>();
            myGrid[1, 1].Owner = owner;
            myGrid[1, 1].Owner = null;
            Assert.IsFalse(myGrid[1, 1].Alive);
        }
    }

    /* Test if cell neighbor count is correct
     * for a cell with increasing neighbors
     */
    [Test]
    public void CountCellNeighborTest()
    {
        CellularAutomaton.Grid myGrid = null;
        try
        {
            myGrid = new CellularAutomaton.Grid(5, 5);
        }
        catch (System.Exception)
        {
            Assert.Fail("Constructor Exception Error");
        }
        finally
        {
            if (myGrid == null) { Assert.Fail("Null Array Error"); }

            var owner = ScriptableObject.CreateInstance<OwnerSO>();
         
         /*
         *          0  1  2  3  4  5
         *        0 x
         *        
         *        1
         *          
         *        2       x  x  x
         *          
         *        3       x  ?  x
         *          
         *        4       x  x  x
         *          
         *        5
         */

            myGrid[0, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 3) == 0);
            myGrid[2, 2].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 3) == 1); 
            myGrid[2, 3].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 3) == 2);
            myGrid[2, 4].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 3) == 3);
            myGrid[3, 2].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 3) == 4);
            myGrid[3, 4].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 3) == 5);
            myGrid[4, 2].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 3) == 6);
            myGrid[4, 3].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 3) == 7);
            myGrid[4, 4].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 3) == 8);

        }
        
    }

    /* Test if cell neighbor count is correct
     * for a cell with increasing neighbors
     * on the left boundary
     */
    [Test]
    public void CountCellNeighborLeftEdgeTest()
    {
        CellularAutomaton.Grid myGrid = null;
        try
        {
            myGrid = new CellularAutomaton.Grid(6, 6);
        }
        catch (System.Exception)
        {
            Assert.Fail("Constructor Exception Error");
        }
        finally
        {
            if (myGrid == null) { Assert.Fail("Null Array Error"); }

            var owner = ScriptableObject.CreateInstance<OwnerSO>();

            /*
            *          0  1  2  3  4  5
            *        0 x
            *        
            *        1
            *          
            *        2  x x
            *          
            *        3  ? x
            *          
            *        4  x x
            *          
            *        5
            */

            myGrid[0, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 0) == 0);
            myGrid[2, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 0) == 1);
            myGrid[2, 1].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 0) == 2);
            myGrid[3, 1].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 0) == 3);
            myGrid[4, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 0) == 4);
            myGrid[4, 1].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 0) == 5);
        }

    }

    /* Test if cell neighbor count is correct
     * for a cell with increasing neighbors
     * on the right boundary
     */
    [Test]
    public void CountCellNeighborRightEdgeTest()
    {
        CellularAutomaton.Grid myGrid = null;
        try
        {
            myGrid = new CellularAutomaton.Grid(6, 6);
        }
        catch (System.Exception)
        {
            Assert.Fail("Constructor Exception Error");
        }
        finally
        {
            if (myGrid == null) { Assert.Fail("Null Array Error"); }

            var owner = ScriptableObject.CreateInstance<OwnerSO>();

            /*
            *          0  1  2  3  4  5
            *        0 x
            *        
            *        1
            *          
            *        2              x x
            *          
            *        3              x ?
            *          
            *        4              x x
            *          
            *        5
            */

    myGrid[0, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 5) == 0);
            myGrid[2, 4].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 5) == 1);
            myGrid[2, 5].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 5) == 2);
            myGrid[3, 4].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 5) == 3);
            myGrid[4, 4].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 5) == 4);
            myGrid[4, 5].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 5) == 5);
        }

    }

    /* Test if cell neighbor count is correct
     * for a cell with increasing neighbors
     * on the bottom boundary
     */
    [Test]
    public void CountCellNeighborBottomEdgeTest()
    {
        CellularAutomaton.Grid myGrid = null;
        try
        {
            myGrid = new CellularAutomaton.Grid(6, 6);
        }
        catch (System.Exception)
        {
            Assert.Fail("Constructor Exception Error");
        }
        finally
        {
            if (myGrid == null) { Assert.Fail("Null Array Error"); }

            var owner = ScriptableObject.CreateInstance<OwnerSO>();

            /*
            *          0  1  2  3  4  5
            *        0 x
            *        
            *        1
            *          
            *        2  
            *          
            *        3  
            *          
            *        4  x x x
            *          
            *        5  x ? x
            */

            myGrid[0, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(5, 1) == 0);
            myGrid[4, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(5, 1) == 1);
            myGrid[4, 1].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(5, 1) == 2);
            myGrid[4, 2].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(5, 1) == 3);
            myGrid[5, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(5, 1) == 4);
            myGrid[5, 2].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(5, 1) == 5);
        }

    }

    /* Test if cell neighbor count is correct
     * for a cell with increasing neighbors
     * on the top boundary
     */
    [Test]
    public void CountCellNeighborTopEdgeTest()
    {
        CellularAutomaton.Grid myGrid = null;
        try
        {
            myGrid = new CellularAutomaton.Grid(6, 6);
        }
        catch (System.Exception)
        {
            Assert.Fail("Constructor Exception Error");
        }
        finally
        {
            if (myGrid == null) { Assert.Fail("Null Array Error"); }

            var owner = ScriptableObject.CreateInstance<OwnerSO>();

            /*
            *          0  1  2  3  4  5
            *        0 x     x  ?  x
            *                           
            *        1       x  x  x
            *          
            *        2               
            *          
            *        3               
            *          
            *        4               
            *          
            *        5
            */

            myGrid[0, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(0, 3) == 0);
            myGrid[0, 2].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(0, 3) == 1);
            myGrid[0, 4].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(0, 3) == 2);
            myGrid[1, 2].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(0, 3) == 3);
            myGrid[1, 3].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(0, 3) == 4);
            myGrid[1, 4].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(0, 3) == 5);
        }

    }









    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator NewTestScriptWithEnumeratorPasses()
    //{
    // Use the Assert class to test conditions.
    // Use yield to skip a frame.
    //   yield return null;
    //}
}
