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
		public static List<NodeModel> dijkstra(List<NodeModelArray> grid, NodeModel startNode, NodeModel finishNode)
		{
			List<NodeModel> visitedNodesInOrder = new List<NodeModel>();
			startNode.Distance = 0;
			List<NodeModel> unvisitedNodes = GetAllNodes(grid);
			while(unvisitedNodes.Count > 0)
			{
				SortNodesByDistance(ref unvisitedNodes);
				NodeModel closestNode = unvisitedNodes.RemoveAndReturnFirst();

				// If node is a wall, skip it.
				if (closestNode.IsWall) continue;
				
				// if the closest node distance is infinity,
				// then it must mean it is trapped and should end
				if (double.IsInfinity(closestNode.Distance))
				{
					visitedNodesInOrder.TrimExcess();
					return visitedNodesInOrder;
				}

				closestNode.IsVisited = true;
				visitedNodesInOrder.Add(closestNode);
				if (closestNode == finishNode)
				{
					visitedNodesInOrder.TrimExcess();
					return visitedNodesInOrder;
				}

				UpdateUnvisitedNeighbours(closestNode, ref grid);
			}
			visitedNodesInOrder.TrimExcess();
			return visitedNodesInOrder;
		}

		public static List<NodeModel> GetAllNodes(List<NodeModelArray> grid)
		{
			List<NodeModel> nodes = new List<NodeModel>();
			foreach(NodeModelArray row in grid)
				foreach (NodeModel col in row.Node_Row)
					nodes.Add(col);
			return nodes;
		}

		public static void SortNodesByDistance(ref List<NodeModel> unvisitedNodes )
		{
			unvisitedNodes = unvisitedNodes.OrderBy(x => x.Distance).ToList();
		}

		public static void UpdateUnvisitedNeighbours(NodeModel closestNode, ref List<NodeModelArray> grid)
		{
			List<NodeModel> unvisistedNeighbours = GetUnvisitedNeightbours(closestNode, ref grid);
			foreach(NodeModel neighbour in unvisistedNeighbours)
			{
				neighbour.Distance = closestNode.Distance + 1;
				neighbour.PrevNode = closestNode;
			}
		}

		public static List<NodeModel> GetUnvisitedNeightbours(NodeModel node, ref List<NodeModelArray> grid)
		{
			List<NodeModel> neighbours = new List<NodeModel>();
			(int col, int row) = node.ReturnColAndRow();
			if (row > 0) neighbours.Add(grid[row - 1].Node_Row[col]);
			if (row < grid.Count - 1) neighbours.Add(grid[row + 1].Node_Row[col]);
			if (col > 0) neighbours.Add(grid[row].Node_Row[col - 1]);
			if (col < grid[0].Node_Row.Count - 1) neighbours.Add(grid[row].Node_Row[col + 1]);
			return neighbours.Where(x => !x.IsVisited).ToList();
		}

		public static List<NodeModel> GetNodesInShortestPathOrder(NodeModel finishNode)
		{
			List<NodeModel> nodesInShortestPathOrder = new List<NodeModel>();
			NodeModel currentNode = finishNode;
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
