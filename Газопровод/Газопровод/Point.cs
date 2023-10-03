namespace Газопровод
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="X">координата X</param>
        /// <param name="Y">координата Y</param>
        public Point(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

    }
}
