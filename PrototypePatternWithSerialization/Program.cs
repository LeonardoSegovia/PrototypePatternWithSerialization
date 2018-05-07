using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePatternWithSerialization
{

    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(stream,self);
            stream.Seek(0, SeekOrigin.Begin);

            var objCopy = binaryFormatter.Deserialize(stream);

            stream.Close();

            return (T) objCopy;
        }
    }

    [Serializable]
    public class Person
    {
        public string[] Names { get; set; }
        public Address Address { get; set; }

        public Person(string[] name, Address address)
        {
            Names = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {Names.First()} {Names.Last()} , {nameof(Address)}: {Address}";
        }
    }
    [Serializable]
    public class Address
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var persona = new Person(new[] {"Leonardo", "Segovia"}, new Address("Covadonga", 3014));

            var otraPersona = persona.DeepCopy();

            otraPersona.Address= new Address("Covadonga", 3030);

            Console.WriteLine(persona);

            Console.WriteLine(otraPersona);
            
            Console.ReadKey();
        }
    }
}
