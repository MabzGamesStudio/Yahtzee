using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AITesting : MonoBehaviour
{

	// Start is called before the first frame update
	void Start()
	{

		List<Vertex<string>> vertexList = new List<Vertex<string>>();

		vertexList.Add(new Vertex<string>("GGGGG", "Start"));

		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				string thing = "AAAAA11111";
				vertexList.Add(new Vertex<string>(thing.Substring(j, 5), (i + 1).ToString() + (j + 1).ToString()));
			}
		}

		// vertexList 19 - 23
		vertexList.Add(new Vertex<string>("HHHHH", "0"));
		vertexList.Add(new Vertex<string>("HHHHH", "1"));
		vertexList.Add(new Vertex<string>("HHHHH", "2"));
		vertexList.Add(new Vertex<string>("HHHHH", "3"));
		vertexList.Add(new Vertex<string>("HHHHH", "4"));

		// vertexList 24
		vertexList.Add(new Vertex<string>("HHHHH", "YAHTZEE"));

		DirectedGraph<string> onesGraph = new DirectedGraph<string>(vertexList);
		onesGraph.SetStartingVertex(vertexList[0]);



		// From start to 1,1-6
		onesGraph.AddEdge(vertexList[0], vertexList[1]);
		onesGraph.AddEdge(vertexList[0], vertexList[2]);
		onesGraph.AddEdge(vertexList[0], vertexList[3]);
		onesGraph.AddEdge(vertexList[0], vertexList[4]);
		onesGraph.AddEdge(vertexList[0], vertexList[5]);
		onesGraph.AddEdge(vertexList[0], vertexList[6]);


		// From 1,1 to 2,1-6
		onesGraph.AddEdge(vertexList[1], vertexList[7]);
		onesGraph.AddEdge(vertexList[1], vertexList[8]);
		onesGraph.AddEdge(vertexList[1], vertexList[9]);
		onesGraph.AddEdge(vertexList[1], vertexList[10]);
		onesGraph.AddEdge(vertexList[1], vertexList[11]);
		onesGraph.AddEdge(vertexList[1], vertexList[12]);

		// From 1,2 to 2,2-6
		onesGraph.AddEdge(vertexList[2], vertexList[8]);
		onesGraph.AddEdge(vertexList[2], vertexList[9]);
		onesGraph.AddEdge(vertexList[2], vertexList[10]);
		onesGraph.AddEdge(vertexList[2], vertexList[11]);
		onesGraph.AddEdge(vertexList[2], vertexList[12]);

		// From 1,3 to 2,3-6
		onesGraph.AddEdge(vertexList[3], vertexList[9]);
		onesGraph.AddEdge(vertexList[3], vertexList[10]);
		onesGraph.AddEdge(vertexList[3], vertexList[11]);
		onesGraph.AddEdge(vertexList[3], vertexList[12]);

		// From 1,4 to 2,4-6
		onesGraph.AddEdge(vertexList[4], vertexList[10]);
		onesGraph.AddEdge(vertexList[4], vertexList[11]);
		onesGraph.AddEdge(vertexList[4], vertexList[12]);

		// From 1,5 to 2,5-6
		onesGraph.AddEdge(vertexList[5], vertexList[11]);
		onesGraph.AddEdge(vertexList[5], vertexList[12]);



		// From 2,1 to 3,1 - 6
		onesGraph.AddEdge(vertexList[7], vertexList[13]);
		onesGraph.AddEdge(vertexList[7], vertexList[14]);
		onesGraph.AddEdge(vertexList[7], vertexList[15]);
		onesGraph.AddEdge(vertexList[7], vertexList[16]);
		onesGraph.AddEdge(vertexList[7], vertexList[17]);
		onesGraph.AddEdge(vertexList[7], vertexList[18]);

		// From 2,2 to 3,2-6
		onesGraph.AddEdge(vertexList[8], vertexList[14]);
		onesGraph.AddEdge(vertexList[8], vertexList[15]);
		onesGraph.AddEdge(vertexList[8], vertexList[16]);
		onesGraph.AddEdge(vertexList[8], vertexList[17]);
		onesGraph.AddEdge(vertexList[8], vertexList[18]);

		// From 2,3 to 3,3-6
		onesGraph.AddEdge(vertexList[9], vertexList[15]);
		onesGraph.AddEdge(vertexList[9], vertexList[16]);
		onesGraph.AddEdge(vertexList[9], vertexList[17]);
		onesGraph.AddEdge(vertexList[9], vertexList[18]);

		// From 2,4 to 3,4-6
		onesGraph.AddEdge(vertexList[10], vertexList[16]);
		onesGraph.AddEdge(vertexList[10], vertexList[17]);
		onesGraph.AddEdge(vertexList[10], vertexList[18]);

		// From 2,5 to 3,5-6
		onesGraph.AddEdge(vertexList[11], vertexList[17]);
		onesGraph.AddEdge(vertexList[11], vertexList[18]);

		// From 1-3,6 to YAHTZEE
		onesGraph.AddEdge(vertexList[6], vertexList[24]);
		onesGraph.AddEdge(vertexList[12], vertexList[24]);
		onesGraph.AddEdge(vertexList[18], vertexList[24]);

		// From 3,1 - 3,6 to point value vertex
		onesGraph.AddEdge(vertexList[13], vertexList[19]);
		onesGraph.AddEdge(vertexList[14], vertexList[20]);
		onesGraph.AddEdge(vertexList[15], vertexList[21]);
		onesGraph.AddEdge(vertexList[16], vertexList[22]);
		onesGraph.AddEdge(vertexList[17], vertexList[23]);

		Debug.Log(100f * onesGraph.OutcomeProbability(vertexList[0], vertexList[24]));



	}

	// Update is called once per frame
	void Update()
	{

	}

	/* This prints the chance of rolling a specific hand of dice
	 *
	 * A != 1
	 * B != 2
	 * C != 3
	 * D != 4
	 * E != 5
	 * F != 6
	 * 
	 * Example:
	 * Path.ChanceOfRollingHand("56611", "A1111")
	 * should be 2.315 chance because (5/6) * (1/6) * (1/6)
	 */
	void TestPathRollChance()
	{

		Debug.Log(Path.ChanceOfRollingHand("56611", "A1111"));
	}

	/* This prints all the paths from one vertex to another vertex
	*/
	void TestOnesGraph()
	{
		List<Vertex<string>> vertexList = new List<Vertex<string>>();

		vertexList.Add(new Vertex<string>("Start", "Start"));

		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				string thing = "0000011111";
				vertexList.Add(new Vertex<string>(i.ToString() + thing.Substring(j, 5), (i + 1).ToString() + (j + 1).ToString()));
			}
		}

		// vertexList 19 - 23
		vertexList.Add(new Vertex<string>("End", "0"));
		vertexList.Add(new Vertex<string>("End", "1"));
		vertexList.Add(new Vertex<string>("End", "2"));
		vertexList.Add(new Vertex<string>("End", "3"));
		vertexList.Add(new Vertex<string>("End", "4"));

		// vertexList 24
		vertexList.Add(new Vertex<string>("End", "YAHTZEE"));

		DirectedGraph<string> onesGraph = new DirectedGraph<string>(vertexList);
		onesGraph.SetStartingVertex(vertexList[0]);



		// From start to 1,1-6
		onesGraph.AddEdge(vertexList[0], vertexList[1]);
		onesGraph.AddEdge(vertexList[0], vertexList[2]);
		onesGraph.AddEdge(vertexList[0], vertexList[3]);
		onesGraph.AddEdge(vertexList[0], vertexList[4]);
		onesGraph.AddEdge(vertexList[0], vertexList[5]);
		onesGraph.AddEdge(vertexList[0], vertexList[6]);


		// From 1,1 to 2,1-6
		onesGraph.AddEdge(vertexList[1], vertexList[7]);
		onesGraph.AddEdge(vertexList[1], vertexList[8]);
		onesGraph.AddEdge(vertexList[1], vertexList[9]);
		onesGraph.AddEdge(vertexList[1], vertexList[10]);
		onesGraph.AddEdge(vertexList[1], vertexList[11]);
		onesGraph.AddEdge(vertexList[1], vertexList[12]);

		// From 1,2 to 2,2-6
		onesGraph.AddEdge(vertexList[2], vertexList[8]);
		onesGraph.AddEdge(vertexList[2], vertexList[9]);
		onesGraph.AddEdge(vertexList[2], vertexList[10]);
		onesGraph.AddEdge(vertexList[2], vertexList[11]);
		onesGraph.AddEdge(vertexList[2], vertexList[12]);

		// From 1,3 to 2,3-6
		onesGraph.AddEdge(vertexList[3], vertexList[9]);
		onesGraph.AddEdge(vertexList[3], vertexList[10]);
		onesGraph.AddEdge(vertexList[3], vertexList[11]);
		onesGraph.AddEdge(vertexList[3], vertexList[12]);

		// From 1,4 to 2,4-6
		onesGraph.AddEdge(vertexList[4], vertexList[10]);
		onesGraph.AddEdge(vertexList[4], vertexList[11]);
		onesGraph.AddEdge(vertexList[4], vertexList[12]);

		// From 1,5 to 2,5-6
		onesGraph.AddEdge(vertexList[5], vertexList[11]);
		onesGraph.AddEdge(vertexList[5], vertexList[12]);

		// From 1,6 to YAHTZEE
		onesGraph.AddEdge(vertexList[6], vertexList[19]);



		// From 2,1 to 3,1 - 6
		onesGraph.AddEdge(vertexList[7], vertexList[13]);
		onesGraph.AddEdge(vertexList[7], vertexList[14]);
		onesGraph.AddEdge(vertexList[7], vertexList[15]);
		onesGraph.AddEdge(vertexList[7], vertexList[16]);
		onesGraph.AddEdge(vertexList[7], vertexList[17]);
		onesGraph.AddEdge(vertexList[7], vertexList[18]);

		// From 2,2 to 3,2-6
		onesGraph.AddEdge(vertexList[8], vertexList[14]);
		onesGraph.AddEdge(vertexList[8], vertexList[15]);
		onesGraph.AddEdge(vertexList[8], vertexList[16]);
		onesGraph.AddEdge(vertexList[8], vertexList[17]);
		onesGraph.AddEdge(vertexList[8], vertexList[18]);

		// From 2,3 to 3,3-6
		onesGraph.AddEdge(vertexList[9], vertexList[15]);
		onesGraph.AddEdge(vertexList[9], vertexList[16]);
		onesGraph.AddEdge(vertexList[9], vertexList[17]);
		onesGraph.AddEdge(vertexList[9], vertexList[18]);

		// From 2,4 to 3,4-6
		onesGraph.AddEdge(vertexList[10], vertexList[16]);
		onesGraph.AddEdge(vertexList[10], vertexList[17]);
		onesGraph.AddEdge(vertexList[10], vertexList[18]);

		// From 2,5 to 3,5-6
		onesGraph.AddEdge(vertexList[11], vertexList[17]);
		onesGraph.AddEdge(vertexList[11], vertexList[18]);

		// From 1-3,6 to YAHTZEE
		onesGraph.AddEdge(vertexList[12], vertexList[19]);

		// From 3,1 - 3,6 to point value vertex
		onesGraph.AddEdge(vertexList[13], vertexList[19]);
		onesGraph.AddEdge(vertexList[14], vertexList[20]);
		onesGraph.AddEdge(vertexList[15], vertexList[21]);
		onesGraph.AddEdge(vertexList[16], vertexList[22]);
		onesGraph.AddEdge(vertexList[17], vertexList[23]);

		// From 1-3,6 to Yahtzee
		onesGraph.AddEdge(vertexList[6], vertexList[24]);
		onesGraph.AddEdge(vertexList[12], vertexList[24]);
		onesGraph.AddEdge(vertexList[18], vertexList[24]);



		onesGraph.PrintAllPaths(vertexList[0], vertexList[24]);
	}

}
