using System.Reflection;

namespace laborotorna4
{

    class Taverna {
        public string NameTaverna = "Default";
        protected int amountOfWorkers = 10;
        internal DateOnly date;
        private int BeerAmount = 10;
        private int AleAmount = 5;
        private int WineAmount = 3;

        public Taverna(string NameTaverna, int amountOfWorkers, DateOnly date) {
            this.NameTaverna = NameTaverna;
            this.amountOfWorkers = amountOfWorkers;
            this.date = date;
        }

        public void InfoAboutTaverna()
        {
            Console.WriteLine($"Taverna name - {NameTaverna}\n" +
                             $"Date open - {date}\nAmoint workeds - {amountOfWorkers}\n");
            InfoAmountItems();
        }

        private void InfoAmountItems()
        {
            Console.WriteLine("|--------------------|");
            Console.WriteLine($"Beear - {BeerAmount}");
            Console.WriteLine($"Ale - {AleAmount}");
            Console.WriteLine($"Wine - {WineAmount}");
            Console.WriteLine("|--------------------|");
        }

        protected internal void RestockDrink(string drinkType, int amount)
        {
            switch (drinkType.ToLower())
            {
                case "beer":
                    BeerAmount += amount;
                    Console.WriteLine($"{amount} +beers\n");
                    break;
                case "ale":
                    AleAmount += amount;
                    Console.WriteLine($"{amount} +ales\n");
                    break;
                case "wine":
                    WineAmount += amount;
                    Console.WriteLine($"Restocked {amount} +wines\n");
                    break;
                default:
                    Console.WriteLine("Unknown drink");
                    break;
            }
        }
    }

    class CompanyItems {
        public string testField;
        public string testProperty { get; set;}
        public void DeliveryItem(Taverna taverna)
        {
            taverna.RestockDrink("beer", 10);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Taverna myTaverna = new Taverna("Golden Mug", 5, new DateOnly(1995, 1, 1));
            myTaverna.InfoAboutTaverna();

            CompanyItems myCompanyItems = new CompanyItems();
            myCompanyItems.DeliveryItem(myTaverna);
            myTaverna.InfoAboutTaverna();

            Console.WriteLine("///////////////////////////////");

            Type type = myTaverna.GetType();
            Console.WriteLine("Name type - " + type.Name);
            Console.WriteLine("Fullname type - " + type.FullName);
            Console.WriteLine("Is class? - " + type.IsClass);
            Console.WriteLine("Is Abstract Class? - " + type.IsAbstract);

            Console.WriteLine("///////////////////////////////");
            Console.WriteLine("methods");

            TypeInfo typeInfo = typeof(Taverna).GetTypeInfo();
            IEnumerable<MethodInfo> methodList = typeInfo.DeclaredMethods;
            foreach (MethodInfo mi in methodList)
            {
                Console.WriteLine(mi.DeclaringType.Name + " " + mi.Name);
            }

            Console.WriteLine("Fields");
            IEnumerable<FieldInfo> fieldInfo= typeInfo.DeclaredFields;
            foreach (FieldInfo fi in fieldInfo)
            {

                Console.WriteLine(fi.Name + " - " + fi.FieldType);
            }
            Console.WriteLine("///////////////////////////////");

            MemberInfo[] memberInfo = typeof(CompanyItems).GetMembers();
            
            foreach (MemberInfo mi in memberInfo)
            {
                Console.WriteLine($"{mi.Name} - {mi.MemberType}");
            }
            Console.WriteLine("///////////////////////////////");

            FieldInfo[] fieldsInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            Console.WriteLine("Fields with FieldInfo");
            for (int i = 0; i < fieldsInfo.Length; i++)
            {
                Console.WriteLine("\nName          : {0}",fieldsInfo[i].Name);
                Console.WriteLine("Declaring Type  : {0}", fieldsInfo[i].DeclaringType);
                Console.WriteLine("IsPublic        : {0}", fieldsInfo[i].IsPublic);
                Console.WriteLine("MemberType      : {0}", fieldsInfo[i].MemberType);
                Console.WriteLine("FieldType       : {0}", fieldsInfo[i].FieldType);
                Console.WriteLine("IsFamily        : {0}", fieldsInfo[i].IsFamily);

            }
            Console.WriteLine("///////////////////////////////");

            MethodInfo methodsInfo = type.GetMethod("InfoAmountItems", BindingFlags.NonPublic | BindingFlags.Instance);

            methodsInfo.Invoke(myTaverna, null);


        }
    }
}
