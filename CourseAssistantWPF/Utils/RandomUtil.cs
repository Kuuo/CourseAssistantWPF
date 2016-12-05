using System;

namespace CourseAssistantWPF.Utils {
    public static class RandomUtil {

        private static Random random;
        public static int Seed { get; set; }
        

        static RandomUtil() {
            Seed = DateTime.Now.Millisecond;
            random = new Random(Seed);
        }

        public static double Uniform() => random.NextDouble();

        public static int Uniform(int n) {
            if (n <= 0) throw new ArgumentException("Parameter N must be positive");
            return random.Next(n);
        }
        
        public static void Shuffle<T>(T[] a) {
            int N = a.Length;
            for (int i = N - 1; i > 0; i--) {
                int r = Uniform(i + 1);
                Swap(ref a[i], ref a[r]);
            }
        }

        public static void Swap<T>(ref T a, ref T b) {
            T temp = a; a = b; b = temp;
        }
    }
}
