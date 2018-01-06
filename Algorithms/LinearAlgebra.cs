namespace Grapher.Algorithms
{

    static class LinearAlgebra
    {

        public static bool Intersection(Point ts, Point tt, Point os, Point ot)
        {

            // https://en.wikipedia.org/wiki/line%E2%80%93line_intersection

            double denominator = (ts.X - tt.X) * (os.Y - ot.Y) - (ts.Y - tt.Y) * (os.X - ot.X);

            if (-0.001 < denominator && 0.001 > denominator)
            {   // "When the two lines are parallel or coincident the denominator is zero"

                // |      
                // |  |   ||
                // |  ||  ||
                // |   |  

                if (ts == os && tt == ot)
                {   // They are the same segment in the same different directions
                    return true;
                }
                else if (ts == ot && tt == os)
                {   // They are the same segment in different directions
                    return true;
                }
                else if (Contains(ts, tt, os))
                {
                    return true;
                }
                else if (Contains(ts, tt, ot))
                {
                    return true;
                }
                else if (Contains(os, ot, ts))
                {
                    return true;
                }
                else if (Contains(os, ot, tt))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {   // " Note that the intersection point is for the infinitely long lines defined by the points, 
                //   rather than the line segments between the points, 
                //   and can produce an intersection point beyond the lengths of the line segments. "

                if (ts == os || ts == ot || tt == os || tt == ot)
                {
                    return false;
                }
                else
                {

                    var point = new Point(
                        (ts.X * tt.Y - ts.Y * tt.X) * (os.X - ot.X) - (ts.X - tt.X) * (os.X * ot.Y - os.Y * ot.X) / denominator,
                        (ts.X * tt.Y - ts.Y * tt.X) * (os.Y - ot.Y) - (ts.Y - tt.Y) * (os.X * ot.Y - os.Y * ot.X) / denominator
                    );

                    return Contains(ts, tt, point) && Contains(os, ot, point);

                }
            }

        }

        private static bool Contains(Point s, Point t, Point p)
        {

            var cross_product = (p.Y - s.Y) * (t.X - s.X) - (p.X - s.X) * (t.Y - s.Y);

            if (s == p)
            {   // if p is source
                return false;
            }
            else if (t == p)
            {   // if p is target
                return false;
            }
            else if (!(0.1 > cross_product && -0.1 < cross_product))
            {   // if p is not on the line
                return false;
            }
            else
            {   // else p is betw<een source and target

                // http://stackoverflow.com/a/328122

                var dot_product = (p.X - s.X) * (t.X - s.X) + (p.Y - s.Y) * (t.Y - s.Y);
                var squared_length = (t.X - s.X) * (t.X - s.X) + (t.Y - s.Y) * (t.Y - s.Y);

                if (0.0 <= dot_product && dot_product <= squared_length)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

    }

}
