using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex<T>
{

	T data;
	string vertexName;
	bool visited;
	List<Vertex<T>> adjacentVerticies;

	public Vertex(T data)
	{
		this.data = data;
		adjacentVerticies = new List<Vertex<T>>();
	}

	public Vertex(T data, string name)
	{
		this.data = data;
		vertexName = name;
		adjacentVerticies = new List<Vertex<T>>();
	}

	public void SetAdjacentVerticies(List<Vertex<T>> newVerticiesSet)
	{
		adjacentVerticies = newVerticiesSet;
	}

	public void SetVisited(bool visited)
	{
		this.visited = visited;
	}

	public void SetData(T data)
	{
		this.data = data;
	}

	public T GetData()
	{
		return data;
	}

	public bool IsVisited()
	{
		return visited;
	}

	public void AddEdge(Vertex<T> newAdjacentVertex)
	{
		adjacentVerticies.Add(newAdjacentVertex);
	}

	public string GetVertexName()
	{
		return vertexName;
	}

	public List<Vertex<T>> GetAdjacentList()
	{
		return adjacentVerticies;
	}

}
