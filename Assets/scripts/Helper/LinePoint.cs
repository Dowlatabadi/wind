using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Diagnostics;

namespace LinePoint
{
    public class Line
    {
        public float m_slope { get; set; }

        public float b { get; set; }
    }

    public static class Helper
    {
        public static float LinePointGetDist(Vector2 point, Line line)
        {
            var b1 = point.y + (point.x / line.m_slope);

            //UnityEngine.Debug.Log(b1);
            var x_collisionPoint =
                (b1 - line.b) / (line.m_slope + (1 / line.m_slope));
            var y_colltionPoint = line.m_slope * x_collisionPoint + line.b;
            return Vector2
                .Distance(new Vector2(x_collisionPoint, y_colltionPoint),
                point);
        }

        public static Line GetLine(Vector2 point1, Vector2 point2)
        {
            return new Line {
                m_slope = (float)(point2.y - point1.y) / (point1.x - point2.x),
                b =
                    (float)(point1.x * point2.y - point2.x * point1.y) /
                    (point1.x - point2.x)
            };
        }

        public static Vector2
        find_next_point(List<Vector2> points, float min_dist)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            float distance = 0;
            var x = Random.Range(-2f, 2f);
            var y = Random.Range(-4f, 4f);
            var point = new Vector2(x, y);
            if (points.Count() <= 1) return point;
            bool is_ok;
            while (distance < min_dist)
            {
                if (stopWatch.ElapsedMilliseconds > 5000)
                {
                    UnityEngine.Debug.Log(distance + "failed");
                    break;
                }
                x = Random.Range(-1.5f, 1.5f);
                y = Random.Range(-3.5f, 3.5f);
                point = new Vector2(x, y);
                is_ok = true;
                for (int i = 0; i < points.Count(); i++)
                {
                    if (is_ok == false) break;
                    for (int j = 0; j < points.Count(); j++)
                    {
                        if (i == j) continue;

                        var point1 = points.ElementAt(i);
                        var point2 = points.ElementAt(j);
                        var line = Helper.GetLine(point1, point2);
                        distance = Helper.LinePointGetDist(point, line);
                        if (distance < min_dist)
                        {
                            is_ok = false;
                            break;
                        }
                    }
                }
            }
            return point;
        }
    }
}
