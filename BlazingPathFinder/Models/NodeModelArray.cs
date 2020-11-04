using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPathFinder.Models
{
	public class NodeModelArray
	{
		public NodeModelArray(List<NodeModel> row)
		{
			Node_Row = row;
		}
		public List<NodeModel> Node_Row { get; set; }
	}
}
