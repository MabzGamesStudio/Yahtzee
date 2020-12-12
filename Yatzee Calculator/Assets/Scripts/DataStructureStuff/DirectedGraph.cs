using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DirectedGraph<T>
{

	/// <summary>
	/// This is the vertex which the graph flow starts from
	/// </summary>
	protected Vertex<T> startingVertex;

	/// <summary>
	/// This is a list of verticies in the graph
	/// </summary>
	protected List<Vertex<T>> verticies;

	/// <summary>
	/// This returns the starting vertex of the graph
	/// </summary>
	/// <returns>The starting vertex of the graph</returns>
	public Vertex<T> GetStart()
	{
		return startingVertex;
	}

	/// <summary>
	/// This initializes the graph with only the starting vertex which is given
	/// </summary>
	/// <param name="startingVertex">The starting vertex that the graph starts from</param>
	public DirectedGraph(Vertex<T> startingVertex)
	{
		if (startingVertex == null)
		{
			throw new System.Exception("The directed graph was initialized with a starting graph that is null.");
		}
		this.startingVertex = startingVertex;
		verticies = new List<Vertex<T>>();
		verticies.Add(startingVertex);
	}

	/// <summary>
	/// This initializes the graph with a list of verticies and the starting vertex is initialized with the first node in the list
	/// </summary>
	/// <param name="verticiesList">The list that is used to initilize the nodes in the graph</param>
	public DirectedGraph(List<Vertex<T>> verticiesList)
	{
		if (verticiesList == null)
		{
			throw new System.Exception("The directed graph was initialized with a verticiesList that is null.");
		}
		else if (verticiesList.Count == 0)
		{
			throw new System.Exception("The directed graph was initialized with a verticiesList that is empty.");
		}
		verticies = verticiesList;
		startingVertex = verticiesList[0];
	}

	/// <summary>
	/// This adds a directed connection from the vertex from to the vertex to
	/// </summary>
	/// <param name="from">The starting vertex of the connection</param>
	/// <param name="to">The vertex that the starting vertex is directed to</param>
	public void AddEdge(Vertex<T> from, Vertex<T> to)
	{
		if (from == null)
		{
			throw new System.Exception("The given from vertex is null.");
		}
		else if (to == null)
		{
			throw new System.Exception("The given to vertex is null.");
		}

		// This adds the edge between the verticies and adds the verticies to the verticies list is they do not already exist in the list
		from.AddEdge(to);
		if (!verticies.Contains(from))
		{
			verticies.Add(from);
		}
		if (!verticies.Contains(to))
		{
			verticies.Add(to);
		}
	}

	/// <summary>
	/// This adds the given vertex to the verticies list
	/// </summary>
	/// <param name="newVertex">The vertex to be added</param>
	public void AddVertex(Vertex<T> newVertex)
	{
		if (newVertex == null)
		{
			throw new System.Exception("The given vertex is null.");
		}
		verticies.Add(newVertex);
	}

	/// <summary>
	/// This sets the starting vertex to the given vertex
	/// </summary>
	/// <param name="vertex">The new starting vertex</param>
	public void SetStartingVertex(Vertex<T> vertex)
	{
		if (vertex == null)
		{
			throw new System.Exception("The given vertex is null.");
		}
		startingVertex = vertex;
		if (!verticies.Contains(vertex))
		{
			verticies.Add(vertex);
		}
	}

	/// <summary>
	/// This tells whether the given vertex exists in the graph
	/// </summary>
	/// <param name="vertex">The vertex to search if it exists in the graph</param>
	/// <returns></returns>
	bool VertexExists(Vertex<T> vertex)
	{
		if (vertex == null)
		{
			throw new System.Exception("The given vertex is null.");
		}
		return verticies.Contains(vertex);
	}

	/// <summary>
	/// This prints all of the paths from the vertex from to the vertex to
	/// </summary>
	/// <param name="from">The starting vertex</param>
	/// <param name="to">The ending vertex</param>
	public void PrintAllPaths(Vertex<T> from, Vertex<T> to)
	{
		if (!VertexExists(from) || !VertexExists(to))
		{
			Debug.Log("No path exists because the start or end vertex does not exist.");
		}
		PrintAllPathsHelper(from, to, "");
	}

	/// <summary>
	/// This is the helper method that prints all the paths from one point to another
	/// </summary>
	/// <param name="from">The starting vertex</param>
	/// <param name="to">The ending vertex</param>
	/// <param name="previousPath">The string of the path searched for so far</param>
	void PrintAllPathsHelper(Vertex<T> from, Vertex<T> to, string previousPath)
	{

		// If the starting vertex equals the ending vertex then the path has been found and prints the path
		if (from == to)
		{
			Debug.Log(previousPath + " " + to.GetVertexName());
			from.SetVisited(false);
		}

		// This vertex is set to visited to prevent infinite looping
		from.SetVisited(true);

		// This searches through each adjacent vertex in the from vertex that hasn't been visited yet
		foreach (Vertex<T> v in from.GetAdjacentList())
		{
			if (!v.IsVisited())
			{
				PrintAllPathsHelper(v, to, previousPath + " " + from.GetVertexName());
			}
		}

		from.SetVisited(false);
	}

}
