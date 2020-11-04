using BlazingPathFinder.Pages.Components;

namespace BlazingPathFinder.Models
{
	public class NodeModel
	{
		public NodeModel(int col, int row, bool isStart, bool isFinish)
		{
			Col = col;
			Row = row;
			IsStart = isStart;
			IsFinish = isFinish;
			Distance = double.PositiveInfinity;
			ShowVist = false;
			ShowPath = false;
			IsVisited = false;
			IsWall = false;
			PrevNode = null;
		}

		public int Col { get; set; }
		public int Row { get; set; }
		public bool IsStart { get; set; }
		public bool IsFinish { get; set; }
		public double Distance { get; set; }
		public bool ShowVist { get; set; }
		public bool ShowPath { get; set; }
		public bool IsVisited { get; set; }
		public bool IsWall { get; set; }
		public NodeModel PrevNode { get; set; }

		public (int, int) ReturnColAndRow() => (Col, Row);
	}
}
