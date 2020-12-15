using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AOC.Tools;

namespace AOC.Day12
{
    public static class Part02
    {
        static double ToRad(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        static Point2D Rotate(double angle, Point2D p)
        {
            var rad = ToRad(angle);
            double s = Math.Sin(rad);
            double c = Math.Cos(rad);

            double xnew = p.X * c - p.Y * s;
            double ynew = p.X * s + p.Y * c;

            p.X = (int)Math.Round(xnew);
            p.Y = (int)Math.Round(ynew);

            return p;
        }

        static void Shift(ref Point2D ship, ref Point2D way, int multiple)
        {
            ship.X += way.X * multiple;
            ship.Y += way.Y * multiple;
        }

        static void Move(ref Point2D ship, ref Point2D way, string action)
        {
            var size = Convert.ToInt32(action.Substring(1));

            switch (action[0])
            {
                case 'N': way.Y += size; break;
                case 'S': way.Y -= size; break;
                case 'E': way.X += size; break;
                case 'W': way.X -= size; break;

                case 'L': way = Rotate(size, way); break;
                case 'R': way = Rotate(-size, way); break;

                case 'F': Shift(ref ship, ref way, size); break;
            }
        }

        public static void Run()
        {
            var reader = FileIO.CreateProjFilePath("./day12/input.txt");
            var list = reader.ReadAll();

            var ship = new Point2D() { X = 0, Y = 0 };
            var waypoint = new Point2D() { X = 10, Y = 1 };

            foreach (var it in list)
            {
                Move(ref ship, ref waypoint, it);
            }

            Console.WriteLine(Math.Abs(ship.X) + Math.Abs(ship.Y));
        }
    }
}