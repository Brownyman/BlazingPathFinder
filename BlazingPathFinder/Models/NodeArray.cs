using BlazingPathFinder.Pages.Components;
using System.Collections.Generic;

namespace BlazingPathFinder.Models
{
	public class NodeArray
	{
		public NodeArray(List<Node> row)
		{
			Node_Row = row;
		}

		public List<Node> Node_Row { get; set; }
	}
}
