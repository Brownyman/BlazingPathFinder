using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazingPathFinder.Models
{
	public class Node 
	{
		public Node(int col, int row, bool isStart, bool isFinish)
		{
			Col = col;
			Row = row;
			IsStart = isStart;
			IsFinish = isFinish;
			Distance = double.PositiveInfinity;
			ShowVisit = false;
			ShowPath = false;
			IsVisited = false;
			IsWall = false;
			PrevNode = null;
		}

		public Node() { }

		public int Col { get; set; }
		public int Row { get; set; }
		public bool IsStart { get; set; }
		public bool IsFinish { get; set; }

		public double Distance { get; set; }
		public bool ShowVisit { get; set; }
		public bool ShowPath { get; set; }
		public bool IsVisited { get; set; }
		public bool IsWall { get; set; }
		public Node PrevNode { get; set; }

		public (int, int) ReturnColAndRow() => (Col, Row);

		public void Reset()
		{
			IsVisited = false;
			ShowVisit = false;
			ShowPath = false;
			Distance = double.PositiveInfinity;
			IsWall = false;
			PrevNode = null;
		}

		internal string classValue
		{
			get
			{
				if (IsFinish)
					return "node-finish";
				else if (IsStart)
					return "node-start";
				else if (ShowVisit)
					return "node-visited";
				else if (ShowPath)
					return "node-shortest-path";
				else if (IsWall)
					return "node-wall";
				else
					return "";
			}
		}

		public async Task VisualisePath(bool isVisit, bool isPath)
		{
			ShowVisit = isVisit;
			ShowPath = isPath;
			//await InvokeAsync(StateHasChanged);
		}
	}
}
