using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedGraph<T>
{
	Vertex<T> startingVertex;
	List<Vertex<T>> verticies;

	public DirectedGraph()
	{
		verticies = new List<Vertex<T>>();
	}

	public DirectedGraph(List<Vertex<T>> verticiesList)
	{
		verticies = verticiesList;
	}

	public void AddEdge(Vertex<T> from, Vertex<T> to)
	{
		foreach (Vertex<T> vertex in verticies)
		{
			if (vertex == from)
			{
				vertex.AddEdge(to);
				return;
			}
		}
	}

	public void AddVertex(Vertex<T> newVertex)
	{
		verticies.Add(newVertex);
	}

	public void SetStartingVertex(Vertex<T> vertex)
	{
		foreach (Vertex<T> v in verticies)
		{
			if (v.Equals(vertex))
			{
				startingVertex = v;
				return;
			}
		}
	}

	bool VertexExists(Vertex<T> vertex)
	{
		return verticies.Contains(vertex);
	}

	public void PrintAllPaths(Vertex<T> from, Vertex<T> to)
	{
		if (!VertexExists(from) || !VertexExists(to))
		{
			Debug.Log("No path exists because the start or end vertex does not exist.");
		}
		PrintAllPathsHelper(from, to, "");
	}

	void PrintAllPathsHelper(Vertex<T> from, Vertex<T> to, string previousPath)
	{
		if (from == to)
		{
			Debug.Log(previousPath + " " + to.GetVertexName());
		}

		foreach (Vertex<T> v in from.GetAdjacentList())
		{
			PrintAllPathsHelper(v, to, previousPath + " " + from.GetVertexName());
		}
	}

	public float OutcomeProbability(Vertex<string> from, Vertex<string> to)
	{

		List<float> individualPaths = new List<float>();

		individualPaths = PathProbability(from, to, 1f, individualPaths);

		float probability = 0;
		for (int i = 0; i < individualPaths.Count; i++)
		{
			probability += individualPaths[i];
		}

		return probability;
	}

	List<float> PathProbability(Vertex<string> from, Vertex<string> to, float currentProbability, List<float> pathList)
	{
		if (from == to)
		{
			pathList.Add(currentProbability);
			Debug.Log(currentProbability + " has been added");
			return pathList;
		}

		foreach (Vertex<string> v in from.GetAdjacentList())
		{
			float nextProbability = currentProbability * Path.ChanceOfRollingHand(from.GetData(), v.GetData());
			//Debug.Log(nextProbability);
			PathProbability(v, to, nextProbability, pathList);
		}

		return pathList;

	}

}
