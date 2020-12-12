using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex<T>
{

	/// <summary>
	/// The data to be stored in the vertex.
	/// </summary>
	T data;

	/// <summary>
	/// The name of the vertex.
	/// </summary>
	string vertexName;

	/// <summary>
	/// Whether this vertex has been visited.
	/// </summary>
	bool visited;

	/// <summary>
	/// The list of adjacent verticies.
	/// </summary>
	List<Vertex<T>> adjacentVerticies;

	/// <summary>
	/// This creates a new vertex with the given data.
	/// </summary>
	/// <param name="data">The data to be stored in the vertex</param>
	public Vertex(T data)
	{

		// This handles improper input
		if (data == null)
		{
			throw new System.Exception("A: The given data is null.");
		}
		this.data = data;
		adjacentVerticies = new List<Vertex<T>>();
	}

	/// <summary>
	/// This creates a new vertex with the given data and name.
	/// </summary>
	/// <param name="data">The data to be stored in the vertex</param>
	/// <param name="name">The name of the vertex</param>
	public Vertex(T data, string name)
	{

		// This handles improper input
		if (data == null)
		{
			throw new System.Exception("A: The given data is null.");
		}
		else if (name == null)
		{
			throw new System.Exception("A: The given name is null.");
		}
		this.data = data;
		vertexName = name;
		adjacentVerticies = new List<Vertex<T>>();
	}

	/// <summary>
	/// This sets the adjacent vertex list for this vertex.
	/// </summary>
	/// <param name="newVerticiesSet">The new list of adjacent verticies</param>
	public void SetAdjacentVerticies(List<Vertex<T>> newVerticiesSet)
	{

		// This handles improper input
		if (newVerticiesSet == null)
		{
			throw new System.Exception("A: The given newVerticiesSet is null.");
		}
		adjacentVerticies = newVerticiesSet;
	}

	/// <summary>
	/// This updates whether the vertex has been visited.
	/// </summary>
	/// <param name="visited">Whether the vertex has been visited</param>
	public void SetVisited(bool visited)
	{
		this.visited = visited;
	}

	/// <summary>
	/// This updates the data of the given vertex
	/// </summary>
	/// <param name="data">The new data for the vertex</param>
	public void SetData(T data)
	{

		// This handles improper input
		if (data == null)
		{
			throw new System.Exception("A: The given data is null.");
		}
		this.data = data;
	}

	/// <summary>
	/// This returns the data of the vertex
	/// </summary>
	/// <returns>The data for this vertex</returns>
	public T GetData()
	{
		return data;
	}

	/// <summary>
	/// This returns whether this vertex has been visited
	/// </summary>
	/// <returns>Whether this vertex has been visited</returns>
	public bool IsVisited()
	{
		return visited;
	}

	/// <summary>
	/// This adds the given vertex to this vertex's adjacent verticies list
	/// </summary>
	/// <param name="newAdjacentVertex">The vertex to be added to the adjacent list of this vertex</param>
	public void AddEdge(Vertex<T> newAdjacentVertex)
	{

		// This handles improper input
		if (newAdjacentVertex == null)
		{
			throw new System.Exception("A: The given newAdjacentVertex is null.");
		}
		adjacentVerticies.Add(newAdjacentVertex);
	}

	/// <summary>
	/// This returns the name of the vertex
	/// </summary>
	/// <returns>The name of this vertex</returns>
	public string GetVertexName()
	{
		return vertexName;
	}

	/// <summary>
	/// This sets this vertex's name to the given newName
	/// </summary>
	/// <param name="newName">The new name for this vertex</param>
	public void SetVertexName(string newName)
	{

		// This handles improper input
		if (newName == null)
		{
			throw new System.Exception("A: The given newName is null.");
		}
		vertexName = newName;
	}

	/// <summary>
	/// This prints a string vertex with this vertex's information
	/// </summary>
	/// <returns>A string of this vertex's information</returns>
	public override string ToString()
	{
		return "Name: " + vertexName + "\n" + data.ToString();
	}

	/// <summary>
	/// This gets the list of verticies adjacent to this vertex
	/// </summary>
	/// <returns>The list of verticies adjacent to this vertex</returns>
	public List<Vertex<T>> GetAdjacentList()
	{
		return adjacentVerticies;
	}

}
