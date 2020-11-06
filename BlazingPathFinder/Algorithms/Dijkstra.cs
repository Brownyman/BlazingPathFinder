using BlazingPathFinder.Common.ExtensionMethods;
using BlazingPathFinder.Models;
using BlazingPathFinder.Pages.Components;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BlazingPathFinder.Algorithms
{
	public static class Dijkstra
	{
		public static (List<Node>, List<Node>) dijkstra(Node[,] grid, Node startNode, Node finishNode)
		{
			List<Node> visitedNodesInOrder = new List<Node>();
			startNode.Distance = 0;
			List<Node> unvisitedNodes = GetAllNodes(grid);
			while(unvisitedNodes.Count > 0)
			{
				SortNodesByDistance(ref unvisitedNodes);
				Node closestNode = unvisitedNodes.RemoveAndReturnFirst();

				// If node is a wall, skip it.
				if (closestNode.IsWall) continue;
				
				// if the closest node distance is infinity,
				// then it must mean it is trapped and should end
				if (double.IsInfinity(closestNode.Distance))
				{
					visitedNodesInOrder.TrimExcess();
					return (visitedNodesInOrder, null);
				}

				closestNode.IsVisited = true;
				visitedNodesInOrder.Add(closestNode);
				if (closestNode == finishNode)
				{
					visitedNodesInOrder.TrimExcess();
					return (visitedNodesInOrder, GetNodesInShortestPathOrder(finishNode));
				}

				UpdateUnvisitedNeighbours(closestNode, ref grid);
			}
			visitedNodesInOrder.TrimExcess();
			return (visitedNodesInOrder, GetNodesInShortestPathOrder(finishNode));
		}

		public static List<Node> GetAllNodes(Node[,] grid)
		{
			List<Node> nodes = new List<Node>();
			foreach(Node Node in grid)
					nodes.Add(Node);
			return nodes;
		}

		public static void SortNodesByDistance(ref List<Node> unvisitedNodes )
		{
			unvisitedNodes = unvisitedNodes.OrderBy(x => x.Distance).ToList();
		}

		public static void UpdateUnvisitedNeighbours(Node closestNode, ref Node[,] grid)
		{
			List<Node> unvisistedNeighbours = GetUnvisitedNeightbours(closestNode, ref grid);
			foreach(Node neighbour in unvisistedNeighbours)
			{
				neighbour.Distance = closestNode.Distance + 1;
				neighbour.PrevNode = closestNode;
			}
		}

		public static List<Node> GetUnvisitedNeightbours(Node node, ref Node[,] grid)
		{
			List<Node> neighbours = new List<Node>();
			(int col, int row) = node.ReturnColAndRow();
			if (row > 0) neighbours.Add(grid[row - 1, col]);
			if (row < grid.GetLength(0) - 1) neighbours.Add(grid[row + 1, col]);
			if (col > 0) neighbours.Add(grid[row, col - 1]);
			if (col < grid.GetLength(1) - 1) neighbours.Add(grid[row, col + 1]);
			return neighbours.Where(x => !x.IsVisited).ToList();
		}

		public static List<Node> GetNodesInShortestPathOrder(Node finishNode)
		{
			List<Node> nodesInShortestPathOrder = new List<Node>();
			Node currentNode = finishNode;
			while(currentNode != null)
			{
				nodesInShortestPathOrder.Insert(0, currentNode);
				currentNode = currentNode.PrevNode;
			}
			nodesInShortestPathOrder.TrimExcess();
			return nodesInShortestPathOrder;
		}

	}
}
