using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil
{
    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
    public class ClassWithProperty
    {
        private string _caption = "A Default caption";

        public string Caption
        {
            get { return _caption; }
            set { if (_caption != value) _caption = value; }
        }
    }


    class Program
    {


        static void Main(string[] args)
        {

            GetAccessor();

            // Test1();

           // Patameter();

            Console.ReadLine();
        }

        public static void GetAccessor()
        {
            ClassWithProperty test = new ClassWithProperty();
            Console.WriteLine("The Caption property: {0}", test.Caption);
            Console.WriteLine("----------");
            // Get the type and PropertyInfo.
            Type t = Type.GetType("CommonUtil.ClassWithProperty");
            PropertyInfo propInfo = t.GetProperty("Caption");

            // Get the public GetAccessors method.
            MethodInfo[] methInfos = propInfo.GetAccessors();
            Console.WriteLine("There are {0} accessors.",
                              methInfos.Length);
            for (int ctr = 0; ctr < methInfos.Length; ctr++)
            {
                MethodInfo m = methInfos[ctr];
                Console.WriteLine("Accessor #{0}:", ctr + 1);
                Console.WriteLine("   Name: {0}", m.Name);
                Console.WriteLine("   Visibility: {0}", GetVisibility(m));
                Console.Write("   Property Type: ");
                // Determine if this is the property getter or setter.
                if (m.ReturnType == typeof(void))
                {
                    Console.WriteLine("Setter");
                    Console.WriteLine("   Setting the property value.");
                    //  Set the value of the property.
                    m.Invoke(test, new object[] { "The Modified Caption" });
                }
                else
                {
                    Console.WriteLine("Getter");
                    // Get the value of the property.
                    Console.WriteLine("   Property Value: {0}",
                                      m.Invoke(test, new object[] { }));
                }
            }
            Console.WriteLine("----------");
            Console.WriteLine("The Caption property: {0}", test.Caption);
        }
        static string GetVisibility(MethodInfo m)
        {
            string visibility = "";
            if (m.IsPublic)
                return "Public";
            else if (m.IsPrivate)
                return "Private";
            else
               if (m.IsFamily)
                visibility = "Protected ";
            else if (m.IsAssembly)
                visibility += "Assembly";
            return visibility;
        }

        //Distinct 的自定义表达式
        public static void ProductDistinct()
        {
            List<Product> products = new List<Product>()
            {
                new Product(){ Id="1", Name="n1"},
                new Product(){ Id="1", Name="n2"},
                new Product(){ Id="2", Name="n1"},
                new Product(){ Id="2", Name="n2"},
            };

            // var distinctProduct = products.Distinct(new PropertyCompare<Product>("Id")).ToList();
            var distinctProduct = products.Distinct(new FasterPropertyComparer<Product>("Id")).ToList();
            distinctProduct.ForEach(o =>
            {
                Console.WriteLine($"Id:{o.Id}-Name:{o.Name}");

            });
        }

        public static void Test1()
        {
            Func<int> func1 = () => 1 + 1;
            int result1 = func1();

            ConstantExpression exp1 = Expression.Constant(1);
            ConstantExpression exp2 = Expression.Constant(1);
            BinaryExpression exp3 = Expression.Add(exp1, exp2);
            Expression<Func<int>> exp4 = Expression.Lambda<Func<int>>(exp3);
            Func<int> func = exp4.Compile();

            int result = func();

            Console.WriteLine("result1:" + result1);

        }

        public static void Patameter()
        {
            ParameterExpression param = Expression.Parameter(typeof(int));

            MethodCallExpression methodCall = Expression.Call(
                typeof(Console).GetMethod("WriteLine",new Type[] {typeof(int) }),
                param
                );

            Expression.Lambda<Action<int>>(
                methodCall,
                new ParameterExpression[] { param}
                ).Compile()(10);

        }

    }
}
