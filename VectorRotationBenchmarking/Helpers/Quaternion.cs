using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorRotationBenchmarking.Helpers
{
    public class Quaternion
    {
        public double Q0 { get; set; }
        public double Q1 { get; set; }
        public double Q2 { get; set; }
        public double Q3 { get; set; }

        public Quaternion(double q0, double q1, double q2, double q3)
        {
            Q0 = q0;
            Q1 = q1;
            Q2 = q2;
            Q3 = q3;
        }

        public static Quaternion operator +(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.Q0 + q2.Q0, q1.Q1 + q2.Q1, q1.Q2 + q2.Q2, q1.Q3 + q2.Q3);
        }

        public static Quaternion operator -(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.Q0 - q2.Q0, q1.Q1 - q2.Q1, q1.Q2 - q2.Q2, q1.Q3 - q2.Q3);
        }

        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            var n1 = (q1.Q0 * q2.Q0) - (q1.Q1 * q2.Q1) - (q1.Q2 * q2.Q2) - (q1.Q3 * q2.Q3);
            var n2 = (q1.Q0 * q2.Q1) + (q1.Q1 * q2.Q0) + (q1.Q2 * q2.Q3) - (q1.Q3 * q2.Q2);
            var n3 = (q1.Q0 * q2.Q2) - (q1.Q1 * q2.Q3) + (q1.Q2 * q2.Q0) + (q1.Q3 * q2.Q1);
            var n4 = (q1.Q0 * q2.Q3) + (q1.Q1 * q2.Q2) - (q1.Q2 * q2.Q1) + (q1.Q3 * q2.Q0);

            return new Quaternion(n1, n2, n3, n4);
        }

        public static Quaternion operator *(Quaternion q1, double scalar)
        {
            return new Quaternion(q1.Q0 * scalar, q1.Q1 * scalar, q1.Q2 * scalar, q1.Q3 * scalar);
        }
        
        public double Norm()
        {
            return Math.Sqrt(Math.Pow(Q0, 2) + Math.Pow(Q1, 2) + Math.Pow(Q2, 2) + Math.Pow(Q3, 2));
        }

        public Quaternion Invert()
        {
            var conjugate = new Quaternion(Q0, -Q1, -Q2, -Q3);
            return conjugate * (1.0 / this.Norm());
        }

        public override string ToString()
        {
            return $"({Q0}, {Q1}, {Q2}, {Q3})";
        }
    }
}
