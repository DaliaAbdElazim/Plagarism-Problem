using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    public static class PROBLEM_CLASS
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        /// <summary>
        /// Given an UNDIRECTED Graph of matching pairs with their similarity scores (%), find the component with max average similarity score & return its average & corresponding IDs
        /// </summary>
        /// <param name="edges">array of matching pairs and their similarity score</param>
        /// <param name="maxAvgScore">return param1: max average score </param>
        /// <param name="IDs">return param2: IDs of the component with max average similarity score</param>
        public static void RequiredFunction(Tuple<string, string, float>[] edges, ref float maxAvgScore, ref List<string> IDs)
        {
            //REMOVE THIS LINE BEFORE START CODING

            Dictionary<string, List<Tuple<string, float>>> graph = new Dictionary<string, List<Tuple<string, float>>>();
            maxAvgScore = 0.0f;
            IDs = new List<string>();

            foreach (var edge in edges)
            {
                string id1 = edge.Item1;
                string id2 = edge.Item2;
                float score = edge.Item3;
                if (!graph.ContainsKey(id1))
                    graph[id1] = new List<Tuple<string, float>>();
                if (!graph.ContainsKey(id2))
                    graph[id2] = new List<Tuple<string, float>>();

                graph[id1].Add(new Tuple<string, float>(id2, score));
                graph[id2].Add(new Tuple<string, float>(id1, score));
            }


            Dictionary<string, bool> visit = new Dictionary<string, bool>();
            foreach (var edge in edges)
            {
                visit[edge.Item1] = false;
                visit[edge.Item2] = false;
            }

            foreach (var vertex in graph.Keys)
            {
                if (!visit[vertex])
                {
                    List<string> connectedGraph = new List<string>();

                    Queue<string> q = new Queue<string>();
                    q.Enqueue(vertex);
                    visit[vertex] = true;

                    float scores = 0.0f;
                    int num_edges = 0;

                    while (q.Count > 0)
                    {
                        string node = q.Dequeue();
                        connectedGraph.Add(node);

                        foreach (var neighbor in graph[node])
                        {
                            string neighbourNode = neighbor.Item1;

                            if (!visit[neighbourNode])
                            {
                                q.Enqueue(neighbourNode);
                                visit[neighbourNode] = true;
                            }
                            scores += neighbor.Item2;
                            num_edges++;
                        }
                    }

                    if (num_edges > 0)
                    {
                        float avgScore = scores / num_edges;
                        if (avgScore > maxAvgScore)
                        {
                            maxAvgScore = avgScore;
                            IDs = connectedGraph;
                        }
                    }
                }

            }
        }

        #endregion
    }
}