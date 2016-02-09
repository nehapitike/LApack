using System;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace testblas2
{
    public class BLAS : Hub
    {
        public double Blas1(String vec1, String vec2, String connID)
        {
            //Deserialize (JSON --> C# Object) using Newtonsoft.Json namespace  http://www.newtonsoft.com/json
            var A = JsonConvert.DeserializeObject<Matrix>(vec1);  // see below for MatObject class
            var B = JsonConvert.DeserializeObject<Matrix>(vec2);  // see below for MatObject class
            //dot product: C = C + A*B
            int N = A.size[1];// the number of elements in vector A 
            //int M = B.size[0];// the number of elements in vector B
            double C = 0; // dot product C
            
            for (int i = 0; i < N; i++)
            {
                C = C + A.data[0, i] * B.data[i, 0];
            }
            String strC = C.ToString();

            // Call the displayBlas1  method on the client side.
            Clients.Client(connID).DisplayBlas1(strC);

            return C;
        }

        public string[] Blas2_1(String vec, String mat, String connID)
        {
            //Deserialize (JSON --> C# Object) using Newtonsoft.Json namespace  http://www.newtonsoft.com/json
            var A = JsonConvert.DeserializeObject<Matrix>(vec);  // see below for MatObject class
            var B = JsonConvert.DeserializeObject<Matrix>(mat);  // see below for MatObject class
            //dot product: C = C + A*B
            int N = A.size[1]; // the number of elements in vector A 
            int M = B.size[1]; // number of columns in matrix B
            double C = 0;
            string[] D = new string[M]; 
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    C = C + A.data[0, j] * B.data[j, i];
                }

                string strC = C.ToString();
                D[i] = C.ToString();
                // Call the displayBlas2_1 method on the client side
                Clients.Client(connID).DisplayBlas2_1(strC);
                C = 0;
            }
            // Call the displayProduct_1 method on the client side
            Clients.Client(connID).displayProduct_1();
            return D;
        }
        public string[] Blas2_2(String mat, String vec, String connID)
        {
            //Deserialize (JSON --> C# Object) using Newtonsoft.Json namespace  http://www.newtonsoft.com/json
            var A = JsonConvert.DeserializeObject<Matrix>(mat);  // see below for MatObject class
            var B = JsonConvert.DeserializeObject<Matrix>(vec);  // see below for MatObject class
            //dot product: C = C + A*B
            int N = A.size[1]; // the number of columns in matrix A 
            int M = A.size[0]; // number of elements in column vector B
            double C = 0;
            string[] D = new string[M];// dot product D
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    C = C + A.data[i, j] * B.data[j, 0];
                    //C = C + 1;
                }
                string strC = C.ToString();
                D[i] = C.ToString();
                // Call the displayBlas2_2 method on the client side
                Clients.Client(connID).DisplayBlas2_2(strC);
                C = 0;
            }
            // Call the displayProduct_2 method on the client side
            Clients.Client(connID).displayProduct_2();
            return D;
        }
        public string[] Blas3(String mat1, String mat2, String connID)
        {
            //Deserialize (JSON --> C# Object) using Newtonsoft.Json namespace  http://www.newtonsoft.com/json
            var A = JsonConvert.DeserializeObject<Matrix>(mat1);  // see below for MatObject class
            var B = JsonConvert.DeserializeObject<Matrix>(mat2);  // see below for MatObject class
            //dot product: C = C + A*B
            int N = A.size[1]; // the number of columns in matrix A 
            int M = A.size[0]; // number of rows in matrix A
            int k = B.size[1]; // number of columns in matrix B
            double C = 0;
            string[] D = new string[M]; // dot product D
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    for (int z = 0; z < N; z++)
                    {
                        C = C + A.data[i, z] * B.data[z, j];
                    }
                    string strC = C.ToString();
                    D[i] = C.ToString();
                    // Call the displayBlas3 method on the client side
                    Clients.Client(connID).DisplayBlas3(strC);
                    C = 0;
                }
            }
            // Call the displayProduct_3 method on the client side
            Clients.Client(connID).displayProduct_3(M.ToString(),k.ToString());
            return D;
        }
        // This class can be generated using http://json2csharp.com/
        // Enter {"matrixType":"DenseMatrix", "data": [[1,2], [3,4], [5,6]], "size": [3, 2]}
        public class Matrix
        {
            public string matrixType { get; set; }
            public double[,] data { get; set; }
            public int[] size { get; set; }
        }
    }
}
