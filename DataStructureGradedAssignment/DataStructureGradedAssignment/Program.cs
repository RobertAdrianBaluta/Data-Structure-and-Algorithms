using DataStructureGradedAssignment;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    static void Main()
    {
        //Opens the File, reads from it
        string filePath = "C:\\Users\\adrian-robert.baluta\\Desktop\\GraphNodes.txt";
        string[] lines = File.ReadAllLines(filePath);


        //Write down the file
        int totalRows = lines.Length;
        int totalCols = lines[0].Length;
        GraphXO graph = new GraphXO(totalRows, totalCols);

        foreach (string linetowrite in lines)
        {
            Console.WriteLine(linetowrite);
        }

        for (int row = 0; row < lines.Length; row++)
        {
            string line = lines[row];
            for (int col = 0; col < line.Length; col++)
            {
                // make node and insert into graph
                Node node = new Node(row, col);
                graph.nodesGrid[row, col] = node;

                // adjust node property based on character
                char character = line[col];
                if (character == 'X')
                {
                    node.isWall = true;
                }

                if (character == 'S')
                {
                    graph.startNode = node;
                }

                if (character == 'G')
                {
                    graph.goalNode = node;
                }
            }
        }

        //Writed down the nodes
        for (int row = 0; row < lines.Length; row++)
        {
            string line = lines[row];
            for (int col = 0; col < line.Length; col++)
            {
                Node node = graph.nodesGrid[row, col];
            }
        }
        Console.WriteLine($"start node: ({graph.startNode.row}, {graph.startNode.col})");
        Console.WriteLine($"goal node: ({graph.goalNode.row}, {graph.goalNode.col})");



        //Time for the mesurements
        Stopwatch GraphstopWatch = new Stopwatch();
        for (int v = 0; v < 2; v++)
        {
          //  graph.DFT(graph.startNode, graph.goalNode);
            graph.BFT(graph.startNode, graph.goalNode);
        }
      
            GraphstopWatch.Restart();
           // graph.DFT(graph.startNode, graph.goalNode);
           graph.BFT(graph.startNode, graph.goalNode);
            GraphstopWatch.Stop();

            TimeSpan graphts = GraphstopWatch.Elapsed;
            Console.WriteLine($"Time taken to Traverse: {graphts.TotalMilliseconds} milliseconds");
       

        Console.ReadLine();

        #region Sorting
        Algos algos = new Algos();
        Random rand = new Random();
        Stopwatch SortstopWatch = new Stopwatch();

        int arrayLength = 5000;
        int[] array = new int[arrayLength];       //Array that will be randomized and sorted

        int timeArrayLength = 5;
        double[] timeArray = new double[timeArrayLength];       //Array to store the time taken to sort

        for (int warmUp = 0; warmUp < 1; warmUp++)           //Warm up forloop to compensate for the initial compiler time 
        {
            for (int i = 0; i < arrayLength; i++)
                array[i] = rand.Next(1, 100);

           // algos.QuickSort(array);            //}
           // algos.MergeSort(array);               // }
            //algos.SelectionSort(array);          // } The sorting arrays
           // algos.BubbleSort(array);            // }
            algos.InsertionSort(array);        //}
        }

        for (int x = 0; x < timeArrayLength; x++)       // Performs the sorting algorithm 5 times in order to make work easier
        {
            for (int i = 0; i < arrayLength; i++)       // Randomizes the arrays values
            {
                array[i] = rand.Next(1, 100);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nOriginal State: " + "[{0}]", string.Join(", ", array));
            Console.ForegroundColor = ConsoleColor.White;


            SortstopWatch.Restart();
            // algos.QuickSort(array);
           // algos.MergeSort(array);
             //algos.SelectionSort(array);
           //  algos.BubbleSort(array);
              algos.InsertionSort(array);

            SortstopWatch.Stop();

            TimeSpan ts = SortstopWatch.Elapsed;           //Used to display the time variable later on
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sorted State: " + "[{0}]", string.Join(", ", array));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Time taken to sort: {ts.TotalMilliseconds} milliseconds");
            timeArray[x] = ts.TotalMilliseconds;        //Stores the Time into an array

        }

        double sum = 0;                       //Used to help with the calculations
        foreach (double x in timeArray)       // }
        {                                     //   }
            sum += x;                         //    } Adds up the time taken for each itteration and divides it with the ammoun of iterations to get the median
        }                                     //   }
        sum = sum / timeArrayLength;          // }

        Console.Write("\nThe median of all the times taken to calculate the algorithm's measurements: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(sum + " milliseconds");
        Console.ReadLine();
    }
}

#endregion

#region Graph
public class Node
{
    public int row;
    public int col;

    public bool isWall;
    public bool isVisited;
    public Node(int y, int x)
    {
        row = y; col = x;
        isWall = false;
    }
}

public class GraphXO
{
    public Node[,] nodesGrid;
    int totalRows;
    int totalCols;
    public Node startNode;
    public Node goalNode;
    public GraphXO(int _rows, int _cols)
    {
        totalRows = _rows; totalCols = _cols;
        nodesGrid = new Node[totalRows, totalCols];
        startNode = null;
        goalNode = null;

    }

    public List<Node> GetNeighbors(Node node) //List<node> function returns a list of nodes it is used to 
    {
        int row = node.row; int col = node.col;
        List<Node> neighbors = new List<Node>();

        // check up
        if (row > 0 && !nodesGrid[row - 1, col].isWall) { neighbors.Add(nodesGrid[row - 1, col]); }

        // check down
        if (row < totalRows - 1 && !nodesGrid[row + 1, col].isWall) { neighbors.Add(nodesGrid[row + 1, col]); }

        // check left
        if (col > 0 && !nodesGrid[row, col - 1].isWall) { neighbors.Add(nodesGrid[row, col - 1]); }

        // check right
        if (col < totalCols - 1 && !nodesGrid[row, col + 1].isWall) { neighbors.Add(nodesGrid[row, col + 1]); }

        return neighbors;
    }

    public void DFT(Node startNode, Node goalNode)
    {
        if (startNode.isVisited)         // Check if the node has already been visited
        {
            return;
        }

        startNode.isVisited = true;       // Mark the current node as visitedv

        Console.WriteLine($"Visiting ({startNode.row}, {startNode.col}) ");

        List<Node> neighbors = GetNeighbors(startNode);

        foreach (var neighborsofNode in neighbors) // var neighbor is a stand in variable for neigbhours of the current Node
        {
            if (!goalNode.isVisited)
            {
                DFT(neighborsofNode, goalNode); //Recursion, but this time for the neighbor of the node
            }
            if (startNode == goalNode)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Traverse Complete");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
        }
    }
    public void BFT(Node startNode, Node goalNode)
    {
        Queue<Node> queue = new Queue<Node>();              // make a queue, insert start node
        queue.Enqueue(startNode);

        while (queue.Count > 0)                //while the queue is not empty, pop out the front and insert the unvisited neighbors of this front node into the queue
        {
            Node frontNode = queue.Dequeue();
           Console.WriteLine($"Visiting ({frontNode.row}, {frontNode.col}) ");
            if (frontNode == goalNode)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Traverse Complete");
                Console.ForegroundColor = ConsoleColor.White;
                return;   //return when we reach goal node.
            }
            List<Node> neighbors = GetNeighbors(frontNode);
            foreach (var neighborsofNode in neighbors)
            {
                if (!neighborsofNode.isVisited)
                {
                    Console.WriteLine($"Neighbor of front node ({frontNode.row}, {frontNode.col}) : ({neighborsofNode.row}, {neighborsofNode.col}) ");
                    queue.Enqueue(neighborsofNode);
                    neighborsofNode.isVisited = true;
                }
            }
        }
    }
    #endregion

}


