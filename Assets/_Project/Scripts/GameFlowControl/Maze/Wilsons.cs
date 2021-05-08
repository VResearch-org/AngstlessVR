using System.Collections.Generic;
using UnityEngine;

namespace VResearch.GameFlowControl.Maze
{
    public class Wilsons : Maze
    {
        List<MapLocation> notUsed = new List<MapLocation>();

        public override void Generate()
        {
            // create and starting cell
            int x = Random.Range(2, width - 1);
            int z = Random.Range(2, depth - 1);
            map[x, z] = 2;
            int runwalkAttempts = width * depth / 3;
            while (GetAvailableCells() > 1 && runwalkAttempts > 0)
            {
                RandomWalk();
                runwalkAttempts--;
            }
        }

        private int CountSquareMazeNeighbours(int x, int z)
        {
            int count = 0;
            if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
            for (int d = 0; d < directions.Count; d++)
            {
                int nx = x + directions[d].x;
                int nz = z + directions[d].z;
                if (map[nx, nz] == 2)
                {
                    count++;
                }
            }
            return count;
        }

        private int GetAvailableCells()
        {
            notUsed.Clear();
            for (int z = 1; z < depth; z++)
                for (int x = 1; x < width; x++)
                {
                    if (CountSquareMazeNeighbours(x, z) == 0)
                    {
                        notUsed.Add(new MapLocation(x, z));
                    }
                }
            return notUsed.Count;
        }

        private void RandomWalk()
        {
            List<MapLocation> inWalk = new List<MapLocation>();

            int rstartIndex = Random.Range(0, notUsed.Count);
            int cx = notUsed[rstartIndex].x;
            int cz = notUsed[rstartIndex].z;

            inWalk.Add(new MapLocation(cx, cz));

            int countloops = 0;

            bool validPath = false;

            while (cx > 0 && cx < width - 1 && cz > 0 && cz < depth - 1 && countloops < (width * depth) && !validPath)
            {
                map[cx, cz] = 0;
                if (CountSquareMazeNeighbours(cx, cz) > 1)
                    break;
                int rd = Random.Range(0, directions.Count);
                int nx = cx + directions[rd].x;
                int nz = cz + directions[rd].z;
                if (CounSquareNeighbours(nx, nz) < 2)
                {
                    cx = nx;
                    cz = nz;

                    inWalk.Add(new MapLocation(nx, nz));
                }
                validPath = CountSquareMazeNeighbours(cx, cz) == 1;
                countloops++;
            }

            if (validPath)
            {
                map[cx, cz] = 0;

                foreach (MapLocation m in inWalk) map[m.x, m.z] = 2;
                inWalk.Clear();
            }
            else
            {
                foreach (MapLocation m in inWalk) map[m.x, m.z] = 1;
                inWalk.Clear();
            }
        }
    }
}
