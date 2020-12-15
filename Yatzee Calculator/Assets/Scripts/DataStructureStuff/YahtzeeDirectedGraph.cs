using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class YahtzeeDirectedGraph : DirectedGraph<VertexInformation>
{

	/// <summary>
	/// The size of all possible roll combinations
	/// </summary>
	readonly static int ROLL_LIST_SIZE = 252;

	readonly static string dataFileName = "YahtzeeTableNormalFloat";

	/// <summary>
	/// This is an array of verticies with each roll combination
	/// </summary>
	static Vertex<VertexInformation>[] completeRollArray = InitializeCompleteRollArray();

	/// <summary>
	/// This initializes the graph with the given vertex as the starting vertex
	/// </summary>
	/// <param name="vertex">The starting vertex of the graph</param>
	public YahtzeeDirectedGraph(Vertex<VertexInformation> vertex) : base(vertex)
	{

	}

	/// <summary>
	/// This initializes the graph with the given verticiesList and assigns the staring vertex to the first element in the list. The list can not be null or empty.
	/// </summary>
	/// <param name="verteciesList">The list of verticies for the graph</param>
	public YahtzeeDirectedGraph(List<Vertex<VertexInformation>> verteciesList) : base(verteciesList)
	{

	}

	/// <summary>
	/// This initializes the completeRollArray with the 252 verticies of the combinations of dice
	/// </summary>
	/// <returns>The array of verticies with each roll combination</returns>
	public static Vertex<VertexInformation>[] InitializeCompleteRollArray()
	{

		// This intitilizes the graph with verticies of length roll list size
		completeRollArray = new Vertex<VertexInformation>[ROLL_LIST_SIZE];

		// This counts the index to add the next element in the array
		int count = 0;

		// This loops through each dice combination not counting repeats
		for (int i = 0; i < 7776; i++)
		{
			// This creates the next possible dice combination that is cycled through based on i. From 11111 to 66666
			int[] thing = new int[5];
			for (int j = 0; j < 5; j++)
			{
				thing[5 - j - 1] = 1 + ((i / (int)Mathf.Pow(6, j)) % 6);
			}

			// The dice combination is only added if the order of the dice is ascending, which results in no repeats of dice combinations
			if (thing[0] <= thing[1] && thing[1] <= thing[2] && thing[2] <= thing[3] && thing[3] <= thing[4])
			{

				// This converts the int[] of dice numbers to a string
				string stringThing = "";
				for (int j = 0; j < 5; j++)
				{
					stringThing += thing[j];
				}

				// The default name of a vertex is Bob
				string name = "Bob";

				// This creates a new vertex, assigns its averagePointValue and rollNumber to -1, and the name to Bob
				VertexInformation vertexInformation = new VertexInformation(-1, -1, stringThing);

				// This adds the vertex to the next open spot in the array
				completeRollArray[count] = new Vertex<VertexInformation>(vertexInformation, name);

				// The count is incremented to the next open spot in the array
				count++;
			}
		}

		// The array is complete and is returned
		return completeRollArray;
	}

	/// <summary>
	/// This creates the yahtzee graph based on the currentGameState with the third set of roll vertexes initialized with average point values
	/// </summary>
	/// <param name="currentGameState">The current GameState that will decide the generation of the graph point values</param>
	/// <returns>The YahtzeeDirectedGraph with point values</returns>
	public static YahtzeeDirectedGraph CompleteRollGraph(GameState currentGameState)
	{

		// This creates the starting vertex with generic information
		VertexInformation vertexInformation = new VertexInformation(-1, 0, "GGGGG");
		Vertex<VertexInformation> start = new Vertex<VertexInformation>(vertexInformation, "Start");

		// THis creates a new YahtzeeDirectedGraph based on the stringing vertex that was created
		YahtzeeDirectedGraph yahtzeeGraph = new YahtzeeDirectedGraph(start);

		// This initializes the list of the 252 dice combinations
		List<Vertex<VertexInformation>> rollLinkedList = completeRollArray.ToList();

		// This initializes the first set of dice rolls and sets the roll number to 1 for the first column
		List<Vertex<VertexInformation>> verticies1 = DuplicateAdjacencyList(rollLinkedList);
		foreach (Vertex<VertexInformation> v in verticies1)
		{
			v.GetData().SetRollNumber(1);
		}

		// This initializes the second set of dice rolls and sets the roll number to 2 for the second column
		List<Vertex<VertexInformation>> verticies2 = DuplicateAdjacencyList(rollLinkedList);
		foreach (Vertex<VertexInformation> v in verticies2)
		{
			v.GetData().SetRollNumber(2);
		}

		// This initializes the third set of dice rolls and sets the roll number to 3 for the third column
		List<Vertex<VertexInformation>> verticies3 = DuplicateAdjacencyList(rollLinkedList);
		foreach (Vertex<VertexInformation> v in verticies3)
		{
			v.GetData().SetRollNumber(3);
		}

		// This adds edges from the start to the first column
		for (int i = 0; i < ROLL_LIST_SIZE; i++)
		{
			start.AddEdge(verticies1[i]);
		}

		// This adds edges from the each vertex in the first column to the each vertex in the second column
		for (int i = 0; i < ROLL_LIST_SIZE; i++)
		{
			for (int j = 0; j < ROLL_LIST_SIZE; j++)
			{
				verticies1[i].AddEdge(verticies2[j]);
			}
		}

		// This adds edges from the each vertex in the second column to the each vertex in the third column and initializes the point values for the third column
		for (int i = 0; i < ROLL_LIST_SIZE; i++)
		{
			for (int j = 0; j < ROLL_LIST_SIZE; j++)
			{
				verticies2[i].AddEdge(verticies3[j]);
			}

			// This sets the average point values for the third set of vertex rolls
			AddEndPointValues(verticies3[i], currentGameState);
		}

		// This initializes the path dice roll combinations used to help calculate vertex average point values
		Path.InitializeDiceRollCombinations();

		// This calculates all of the dice average point values in the second column
		foreach (Vertex<VertexInformation> v in start.GetAdjacentList()[0].GetAdjacentList())
		{
			CalculateAveragePointValue(v);
		}

		// Tha path roll combinations are reset for the new column
		Path.InitializeDiceRollCombinations();

		// This calculates all of the dice average point values in the first column
		foreach (Vertex<VertexInformation> v in start.GetAdjacentList())
		{
			CalculateAveragePointValue(v);
		}

		// The path roll combinations are reset for the new column
		Path.InitializeDiceRollCombinations();

		// This calculates the average point value of the start
		CalculateAveragePointValue(start);

		// The complete yahtzee graph is returned
		return yahtzeeGraph;
	}

	/// <summary>
	/// This updates the given vertex to its averagePointValue based on the currentGameState. This assumes that the vertex is on its last roll.
	/// </summary>
	/// <param name="vertex">The vertex whose averagePointValue will be updated</param>
	/// <param name="currentGameState">The gameState that will be used to calculate the vertex averagePointValue</param>
	static void AddEndPointValues(Vertex<VertexInformation> vertex, GameState currentGameState)
	{

		// This handles improper input values
		if (vertex == null)
		{
			throw new System.Exception("A: The given vertex was null.");
		}
		else if (currentGameState == null)
		{
			throw new System.Exception("B: The given currentGameState was null.");
		}

		// This lists the average point values for each choice of box
		float[] boxChoicePointValues = new float[13];

		// This is the string version of the 5 dice
		string diceHeld = vertex.GetData().GetDiceFormat();

		// This calculate the round values based on box choices
		for (int i = 0; i < boxChoicePointValues.Length; i++)
		{

			// This tries to calculate the outcome for box i
			try
			{
				boxChoicePointValues[i] = YahtzeeScoring.CalculateBoxOutcome(currentGameState, diceHeld, i);
			}

			// If the box is already filled in then the boxChoicePointValue for i is set to -1 and any other error is displayed
			catch (System.Exception e)
			{
				if (e.Message.Substring(0, 1).Equals("E"))
				{
					boxChoicePointValues[i] = -1;
				}
				else
				{
					Debug.LogError(e);
				}
			}
		}

		// This adds the average point value of the next GameState to each box choice
		for (int i = 0; i < boxChoicePointValues.Length; i++)
		{

			// The choice is only updated if the box has not been filled in yet
			if (boxChoicePointValues[i] != -1)
			{

				// This updates the boxesFilledIn to the new GameState
				bool[] newBoxesFilledIn = new bool[13];
				currentGameState.GetBoxesFilledIn().CopyTo(newBoxesFilledIn, 0);

				// This updates the new bonusAvailable based on the new GameState
				bool newBonusAvailable = YahtzeeScoring.CanEarnYahtzeeBonus(currentGameState, diceHeld, i);

				// This updates the new top total based on the new GameState
				int newTopTotal = currentGameState.GetTopTotal();
				if (i < 6)
				{
					newTopTotal = YahtzeeScoring.NewTopTotal(currentGameState, diceHeld, i);
				}

				// This updates the box to be filled in
				newBoxesFilledIn[i] = true;

				// This creates the new GameState based on the choice
				GameState newGameState = new GameState(newBoxesFilledIn, newBonusAvailable, newTopTotal);

				// This gets the ID of the newGameState
				string newGameStateID = YahtzeeDataIO.ConvertGameStateToID(newGameState);

				// This adds the averagePointValue of the newGameState by accessing the yahtzee data
				try
				{
					boxChoicePointValues[i] += float.Parse(YahtzeeDataIO.GetFromDictionary(newGameStateID, dataFileName));
				}

				// If the newGameStateID does not exist, then the GameState is not possible so the boxChoicePointValues[i] is not increased
				catch (System.Exception e)
				{
					// Ignore
				}
				
			}
		}

		// This is the max score based on the possible box choices
		float maxChoice = 0;

		// This sets maxChoice to the maximum float in the boxChoicePointValues array
		foreach (float choice in boxChoicePointValues)
		{
			if (choice > maxChoice)
			{
				maxChoice = choice;
			}
		}

		// The vertex is updated to the maxChoice because that will result in this point's average point value
		vertex.GetData().SetAveragePointValue(maxChoice);
	}

	/// <summary>
	/// This calculates the average point value for the given vertex
	/// </summary>
	/// <param name="vertex">The vertex to calculate the average point value of</param>
	/// <returns>The average point value of the given vertex</returns>
	public static float CalculateAveragePointValue(Vertex<VertexInformation> vertex)
	{

		// If the vertex's average point value is already initializes then it is returned
		if (vertex.GetData().GetAveragePointValue() != -1)
		{
			return vertex.GetData().GetAveragePointValue();
		}

		// This creates an array of the poissible choices of roll combinations of the dice in this veretex
		string[] diceRollingOptions = GetDiceRollOptions(vertex.GetData().GetDiceFormat());

		// This will store the average point value for each choice of dice roll
		float[] optionsAveragePointValues = new float[diceRollingOptions.Length];

		// This makes each diceRollingOptions format to be numbers 1-6 and Gs filling in any option that has less than 5 characters to format them properly
		for (int i = 0; i < diceRollingOptions.Length; i++)
		{

			diceRollingOptions[i] += "GGGGG".Substring(0, 5 - diceRollingOptions[i].Length);

		}

		// This is the max average point value for a choice of dice to roll
		float max = 0;

		// This is the list of adjacent verticies of the vertex
		List<Vertex<VertexInformation>> vertexList = vertex.GetAdjacentList();

		// This calculates the average point value of each path of dice rolling possibilities and sets max to the best average point value option
		for (int i = 0; i < diceRollingOptions.Length; i++)
		{

			// If the given dice rolling option has already been calculated in this column then the value is retrieved and compared to max
			try
			{
				max = Mathf.Max(max, Path.GetFloatFromStartingCombination(diceRollingOptions[i]));
			}

			// This calculates the dice roll combination when it hasn't been calculated yet
			catch (System.Exception e)
			{
				// This calculates the average point value for this dice rolling option and adds this option and value to path
				for (int j = 0; j < vertexList.Count; j++)
				{

					// This gets the jth element in the vertex list
					Vertex<VertexInformation> v = vertexList[j];

					// This is the probability of reaching an adjacent from this vertex
					float pathChance = Path.ChanceOfRollingHand(diceRollingOptions[i], v.GetData().GetDiceFormat());

					// If the possible chance of reaching this vertex is not 0, then the average point value of this rolling possibility is equal to the path probability times the next vertex's average point value
					if (pathChance != 0)
					{
						optionsAveragePointValues[i] += pathChance * CalculateAveragePointValue(v);
					}
				}

				// This adds the dice combination to the already calculated averagePointValue array
				Path.AddDiceCombination(diceRollingOptions[i], optionsAveragePointValues[i]);

				// If this option has a greater average point value then the max is updated
				max = Mathf.Max(max, optionsAveragePointValues[i]);
			}
		}

		// This vertex is assigned its averagePointValue as the maximum average point value of its choices
		vertex.GetData().SetAveragePointValue(max);

		// The maximum average point value is returned
		return max;
	}

	/// <summary>
	/// This returns the possible dice to roll options given a currentHand of 5 dice 1-6 or GGGGG representing the starting roll
	/// </summary>
	/// <param name="currentHand">S string of 5 dice numbered from 1-6 or GGGGG representing the starting roll</param>
	/// <returns>An array of unique dice roll options given a hand of dice</returns>
	static string[] GetDiceRollOptions(string currentHand)
	{

		// If the hand is GGGGG then it is the stringing point and so all dice are rolled
		if (currentHand.Equals("GGGGG"))
		{
			return new string[] { "GGGGG" };
		}

		// This is the list of unique roll options
		List<string> hands = new List<string>();

		// There are 5 dice to roll which means 2^5 or 32 dice rolling combinations not counting repeats so this cycles through each combination and adds it to the hands list if it is unique
		for (int i = 0; i < 32; i++)
		{

			// The hand is the string of 0-5 dice to roll numbered 1-6
			string hand = "";

			// 32 hands are generated with each of the 32 combinations taking or leaving each of the 5 dice
			if ((i / 16) % 2 == 0)
			{
				hand += currentHand.Substring(0, 1);
			}
			if ((i / 8) % 2 == 0)
			{
				hand += currentHand.Substring(1, 1);
			}
			if ((i / 4) % 2 == 0)
			{
				hand += currentHand.Substring(2, 1);
			}
			if ((i / 2) % 2 == 0)
			{
				hand += currentHand.Substring(3, 1);
			}
			if (i % 2 == 0)
			{
				hand += currentHand.Substring(4, 1);
			}

			// This bool tells whether the generated hand already exists in the hands list
			bool repeat = false;

			// This checks each hand in hands and if the element is an anagram of the current hand, then it is a repeat hand
			foreach (string str in hands)
			{
				if (Anagram.IsAnagram(str, hand))
				{
					repeat = true;
				}
			}

			// The current hand is added to the list if it is new
			if (!repeat)
			{
				hands.Add(hand);
			}
		}

		// The fully generated hands list is returned as an array
		return hands.ToArray();
	}

	/// <summary>
	/// This duplicates the adjacency list of a vertex so that the primitive variables are duplicated without references being duplicated
	/// </summary>
	/// <param name="vertexList">The adjacency list to be duplicated</param>
	/// <returns>The duplicated given adjacency list with new primitive data and no duplicated references</returns>
	static List<Vertex<VertexInformation>> DuplicateAdjacencyList(List<Vertex<VertexInformation>> vertexList)
	{

		// This creates the new list
		List<Vertex<VertexInformation>> newList = new List<Vertex<VertexInformation>>();

		// For each old vertex a new vertex with the same data and no referenecs is generated then added back to the new list
		foreach (Vertex<VertexInformation> oldVertex in vertexList)
		{
			VertexInformation newData = oldVertex.GetData().DuplicateDataInformation();
			Vertex<VertexInformation> newVertex = new Vertex<VertexInformation>(newData, oldVertex.GetVertexName());
			newList.Add(newVertex);
		}

		// The duplicated list is returned
		return newList;
	}

}
