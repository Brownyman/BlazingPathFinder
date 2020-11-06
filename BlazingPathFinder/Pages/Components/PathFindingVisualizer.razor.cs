using BlazingPathFinder.Algorithms;
using BlazingPathFinder.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPathFinder.Pages.Components
{
	public partial class PathFindingVisualizer : ComponentBase
	{

		static int START_NODE_ROW = 8;
		static int START_NODE_COL = 15;
		static int FINISH_NODE_ROW = 12;
		static int FINISH_NODE_COL = 35;

		private bool loading = false;
		private Node[,] Grid { get; set; }
		private Node StartNode { get; set; }
		private Node FinishNode { get; set; }

		private bool isAlgoRunning = false;

		protected override async Task OnInitializedAsync()
		{
			CreateGridAsync();
			loading = true;
		}

		#region GridMethods


		private async Task LoadGrid()
		{
			CreateGridAsync();
		}

		private async Task CreateRandomGridAsync()
		{
			Random r = new Random();

			await ResetGridAsync();
			Grid[START_NODE_ROW, START_NODE_COL].IsStart = false;
			Grid[FINISH_NODE_ROW, FINISH_NODE_COL].IsFinish = false;

			START_NODE_ROW = r.Next(21);
			START_NODE_COL = r.Next(51);
			
			FINISH_NODE_ROW = r.Next(21);
			FINISH_NODE_COL = r.Next(51);

			while(FINISH_NODE_ROW == START_NODE_ROW 
					&& FINISH_NODE_COL == START_NODE_COL)
			{
				FINISH_NODE_ROW = r.Next(21);
				FINISH_NODE_COL = r.Next(51);
			}
			Grid[START_NODE_ROW, START_NODE_COL].IsStart = true;
			Grid[FINISH_NODE_ROW, FINISH_NODE_COL].IsFinish = true;

			await InvokeAsync(StateHasChanged);
		}

		private async Task ResetGridAsync()
		{
			foreach (Node node in Grid)
				if (node.IsVisited || node.IsWall) node.Reset();

			await InvokeAsync(StateHasChanged);
		}

		private void CreateGridAsync()
		{
			try
			{
				Grid = new Node[20, 50];
				for (int row = 0; row < 20; row++)
				{
					for (int col = 0; col < 50; col++)
					{
						Grid[row, col] = new Node(
							col,
							row,
							(row == START_NODE_ROW && col == START_NODE_COL),
							(row == FINISH_NODE_ROW && col == FINISH_NODE_COL));
					}
				}
			}
			catch (Exception ex)
			{
				return;
			}
		}

		#endregion

		#region Mouse Event Handlers

		private bool mouseIsPressed = false;

		public async Task OnMouseDown(int row, int col)
		{
			if (isAlgoRunning) return;
			Grid[row, col].IsWall = !Grid[row, col].IsWall;
			await InvokeAsync(StateHasChanged);
			mouseIsPressed = true;
		}

		public async Task OnMouseUp(int row, int col)
		{
			mouseIsPressed = false;
		}

		public async Task OnMouseEnter(int row, int col)
		{
			if (!mouseIsPressed || isAlgoRunning) return;
			Grid[row, col].IsWall = !Grid[row, col].IsWall;
			await InvokeAsync(StateHasChanged);
		}

		#endregion

		#region Execute Algorithm

		private async Task VisualizeDijkstra()
		{
			StartNode = Grid[START_NODE_ROW, START_NODE_COL];
			FinishNode = Grid[FINISH_NODE_ROW, FINISH_NODE_COL];
			(List<Node> visitedNodesInOrder, List < Node > nodesInShortestPathOrder) = Dijkstra.dijkstra(Grid, StartNode, FinishNode);
			if(nodesInShortestPathOrder != null)
				await AnimateDijkstra(visitedNodesInOrder, nodesInShortestPathOrder);
		}

		private async Task AnimateDijkstra(List<Node> visitedNodesInOrder, List<Node> nodesInShortestPathOrder)
		{
			for (int i = 0; i <= visitedNodesInOrder.Count; i++)
			{
				if (i == visitedNodesInOrder.Count)
				{
					await AnimateShortestPath(nodesInShortestPathOrder);
					return;
				}

				await Grid[visitedNodesInOrder[i].Row, visitedNodesInOrder[i].Col].VisualisePath(true, false);
				await InvokeAsync(StateHasChanged);
				await Task.Delay(10);
			}
		}

		private async Task AnimateShortestPath(List<Node> nodesInShortestPathOrder)
		{
			for (int i = 0; i < nodesInShortestPathOrder.Count; i++)
			{
				await Grid[nodesInShortestPathOrder[i].Row, nodesInShortestPathOrder[i].Col].VisualisePath(false, true);
				await InvokeAsync(StateHasChanged);
				await Task.Delay(50);
			}
		}

		#endregion

	}
}
