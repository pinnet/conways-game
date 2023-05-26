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
        var myGrid = SetupTestGrid(3, 3);
        var owner = ScriptableObject.CreateInstance<OwnerSO>();
        myGrid[1, 1].Owner = owner;
        Assert.IsTrue(myGrid[1, 1].Alive);
    }

    /* Test if grid cell is dead after
     * owner is removed
     */
    [Test]
    public void CellDeadAfterOwnerNulledTest()
    {
        var myGrid = SetupTestGrid(3, 3);
        var owner = ScriptableObject.CreateInstance<OwnerSO>();
        myGrid[1, 1].Owner = owner;
        myGrid[1, 1].Owner = null;
        Assert.IsFalse(myGrid[1, 1].Alive);
        
    }

    /* Test if cell neighbor count is correct
     * for a cell with increasing neighbors
     */
    [Test]
    public void CountCellNeighborTest()
    {
        var myGrid = SetupTestGrid(5, 5);

        var owner = ScriptableObject.CreateInstance<OwnerSO>();
         
         /*
         *          0  1  2  3  4  
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

    /* Test if cell neighbor count is correct
     * for a cell with increasing neighbors
     * on the left boundary
     */
    [Test]
    public void CountCellNeighborLeftEdgeTest()
    {
        var myGrid = SetupTestGrid(5, 5);

        var owner = ScriptableObject.CreateInstance<OwnerSO>();

            /*
            *          0  1  2  3  4
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

    /* Test if cell neighbor count is correct
     * for a cell with increasing neighbors
     * on the right boundary
     */
    [Test]
    public void CountCellNeighborRightEdgeTest()
    {
        var myGrid = SetupTestGrid(5, 5);

        var owner = ScriptableObject.CreateInstance<OwnerSO>();

            /*
            *          0  1  2  3  4  
            *        0 x
            *        
            *        1
            *          
            *        2           x  x
            *          
            *        3           x  ?
            *          
            *        4           x  x
            *          
            *   
            */

            myGrid[0, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 4) == 0);
            myGrid[2, 3].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 4) == 1);
            myGrid[2, 4].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 4) == 2);
            myGrid[3, 3].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 4) == 3);
            myGrid[4, 3].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 4) == 4);
            myGrid[4, 4].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(3, 4) == 5);
    }

    /* Test if cell neighbor count is correct
     * for a cell with increasing neighbors
     * on the bottom boundary
     */
    [Test]
    public void CountCellNeighborBottomEdgeTest()
    {
        var myGrid = SetupTestGrid(5, 5);

        var owner = ScriptableObject.CreateInstance<OwnerSO>();

            /*
            *          0  1  2  3  4 
            *        0 x
            *        
            *        1
            *          
            *        2  
            *          
            *        3  x x x
            *          
            *        4  x ? x
            *          
           
            */

            myGrid[0, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(4, 1) == 0);
            myGrid[3, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(4, 1) == 1);
            myGrid[3, 1].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(4, 1) == 2);
            myGrid[3, 2].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(4, 1) == 3);
            myGrid[4, 0].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(4, 1) == 4);
            myGrid[4, 2].Owner = owner;
            Assert.IsTrue(myGrid.CountNeighbors(4, 1) == 5);
    }

    /* Test if cell neighbor count is correct
     * for a cell with increasing neighbors
     * on the top boundary
     */
    [Test]
    public void CountCellNeighborTopEdgeTest()
    {
        var myGrid = SetupTestGrid(5, 5);

        var owner = ScriptableObject.CreateInstance<OwnerSO>();

            /*
            *          0  1  2  3  4  
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


    /* Test Advance function if it generates
     * expected cells for no neighbor generation
     */
    [Test]
    public void AdvanceGenerationNoNeighborTest()
    {
        var myGrid = SetupTestGrid(5, 5);

        var owner = ScriptableObject.CreateInstance<OwnerSO>();

        /*
        *          0  1  2  3  4   
        *        0     
        *                           
        *        1   ?             
        *                        
        *        2                              
        *                         
        *        3                 
        *          
        *        4           
        *           
        */

        myGrid[1, 1].Owner = owner;
        Assert.IsTrue(myGrid[1, 1].Alive);

        myGrid.Advance(owner); // test function

        Assert.IsFalse(myGrid[2, 2].Alive); // Should be dead
    }

    /* Test Advance function if it generates
     * expected cells for one neighbor generation
     */
    [Test]
    public void AdvanceGenerationOneNeighborTest()
    {
        var myGrid = SetupTestGrid(5, 5);

        var owner = ScriptableObject.CreateInstance<OwnerSO>();

            /*
            *          0  1  2  3  4   
            *        0 x   
            *                           
            *        1   ?             
            *                        
            *        2                              
            *                         
            *        3                 
            *          
            *        4           
            *           
            */

            myGrid[0, 0].Owner = owner;
            myGrid[1, 1].Owner = owner;
            Assert.IsTrue(myGrid[1, 1].Alive);

            myGrid.Advance(owner); // test function

            Assert.IsFalse(myGrid[2, 2].Alive); // Should be dead
    }


    /* Test Advance function if it generates
     * expected cells for a two neighbor generation
     */
    [Test]
    public void AdvanceGenerationTwoNeighborTest()
    {
        var myGrid = SetupTestGrid(5, 5);

        var owner = ScriptableObject.CreateInstance<OwnerSO>();

            /*
            *          0  1  2  3  4  5
            *        0    
            *                           
            *        1       x  x     
            *                        
            *        2    .  ?                      
            *                         
            *        3    .            
            *          
            *        4           
            *          
            *        5   
            */
           

            // by 2 test
            myGrid[1,2].Owner = owner;
            myGrid[1,3].Owner = owner;
            myGrid[2,2].Owner = owner;
  
            Assert.IsTrue(myGrid[2, 2].Alive);

            myGrid.Advance(owner);

            Assert.IsFalse(myGrid[2, 2].Alive); // Should be dead

    }

    /* Test Advance function if it generates
     * expected cells for a Three neighbor generation
     */
    [Test]
    public void AdvanceGenerationThreeNeighborTest()
    {
        var myGrid = SetupTestGrid(5, 5);

        var owner = ScriptableObject.CreateInstance<OwnerSO>();

        /*
        *          0  1  2  3  4  5
        *        0    
        *                           
        *        1    x  x  x     
        *                        
        *        2       ?                      
        *                         
        *        3                 
        *          
        *        4           
        *          
        *        5   
        */


        // by 2 test
        myGrid[1, 1].Owner = owner;
        myGrid[1, 2].Owner = owner;
        myGrid[1, 3].Owner = owner;

        Assert.IsFalse(myGrid[2, 2].Alive);

        myGrid.Advance(owner);

        Assert.IsTrue(myGrid[2, 2].Alive); // Should be alive

    }

    /* Test Advance function if it generates
     * expected cells for a Four neighbor generation
     */
    [Test]
    public void AdvanceGenerationFourNeighborTest()
    {
        var myGrid = SetupTestGrid(5, 5);

        var owner = ScriptableObject.CreateInstance<OwnerSO>();

        /*
        *          0  1  2  3  4  5
        *        0    
        *                           
        *        1    x  x  x     
        *                        
        *        2       ?  x                    
        *                         
        *        3                 
        *          
        *        4           
        *          
        *        5   
        */


        // by 2 test
        myGrid[1, 1].Owner = owner;
        myGrid[1, 2].Owner = owner;
        myGrid[1, 3].Owner = owner;
        myGrid[2, 2].Owner = owner;
        myGrid[2, 3].Owner = owner;

        Assert.IsTrue(myGrid[2, 2].Alive);

        myGrid.Advance(owner);

        Assert.IsFalse(myGrid[2, 2].Alive); // Should be dead

    }

    
    private CellularAutomaton.Grid SetupTestGrid(int x,int y)
    {
        CellularAutomaton.Grid myGrid = null;

        try
        {
            myGrid = new CellularAutomaton.Grid(x, y);
        }
        catch (System.Exception)
        {
            Assert.Fail("Constructor Exception Error");
        }
        finally
        {
            if (myGrid == null) { Assert.Fail("Null Array Error"); }

        }
        return myGrid;
    }
}
