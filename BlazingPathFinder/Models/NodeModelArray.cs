using BlazingPathFinder.Pages.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPathFinder.Models
{
	public class NodeModelArray
	{
		public NodeModelArray(List<Node> row)
		{
			Node_Row = row;
		}
		public List<Node> Node_Row { get; set; }
	}
}
