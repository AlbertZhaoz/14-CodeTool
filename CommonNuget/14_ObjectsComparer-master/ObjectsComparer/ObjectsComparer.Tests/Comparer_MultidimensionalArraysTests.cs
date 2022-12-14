using System.Linq;
using NUnit.Framework;
using ObjectsComparer.Tests.TestClasses;

namespace ObjectsComparer.Tests
{
    [TestFixture]
    public class ComparerMultidimensionalArraysTests
    {
        [Test]
        public void IntOfIntInequality1()
        {
            var a1 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 1, 2 } } };
            var a2 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 1, 3 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntOfInt[0][1]", differences[0].MemberPath);
            Assert.AreEqual("2", differences[0].Value1);
            Assert.AreEqual("3", differences[0].Value2);
        }

        [Test]
        public void IntOfIntInequality2()
        {
            var a1 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 1, 2 }, new[] { 3, 4 } } };
            var a2 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 2, 2 }, new[] { 3, 5 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(2, differences.Count);
            Assert.AreEqual("IntOfInt[0][0]", differences[0].MemberPath);
            Assert.AreEqual("1", differences[0].Value1);
            Assert.AreEqual("2", differences[0].Value2);
            Assert.AreEqual("IntOfInt[1][1]", differences[1].MemberPath);
            Assert.AreEqual("4", differences[1].Value1);
            Assert.AreEqual("5", differences[1].Value2);
        }

        [Test]
        public void IntOfIntInequality3()
        {
            var a1 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 1, 2 } } };
            var a2 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 2, 2 }, new[] { 3, 5 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntOfInt.Length", differences[0].MemberPath);
            Assert.AreEqual("1", differences[0].Value1);
            Assert.AreEqual("2", differences[0].Value2);
        }

        [Test]
        public void IntOfIntInequality4()
        {
            var a1 = new MultidimensionalArrays { IntOfInt = new int[0][] };
            var a2 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 2, 2 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntOfInt.Length", differences[0].MemberPath);
            Assert.AreEqual("0", differences[0].Value1);
            Assert.AreEqual("1", differences[0].Value2);
        }

        [Test]
        public void IntOfIntInequality5()
        {
            var a1 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 1, 2 }, new[] { 3, 5 } } };
            var a2 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 1, 2 }, new[] { 3, 5, 6 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntOfInt[1].Length", differences[0].MemberPath);
            Assert.AreEqual("2", differences[0].Value1);
            Assert.AreEqual("3", differences[0].Value2);
        }

        [Test]
        public void IntOfIntInequality6()
        {
            var a1 = new MultidimensionalArrays { IntOfInt = new int[0][] };
            var a2 = new MultidimensionalArrays();
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntOfInt", differences[0].MemberPath);
            Assert.AreEqual(typeof(int[][]).FullName, differences[0].Value1);
            Assert.AreEqual(string.Empty, differences[0].Value2);
        }

        [Test]
        public void IntOfIntInequality7()
        {
            var a1 = new MultidimensionalArrays();
            var a2 = new MultidimensionalArrays { IntOfInt = new int[0][] };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntOfInt", differences[0].MemberPath);
            Assert.AreEqual(string.Empty, differences[0].Value1);
            Assert.AreEqual(typeof(int[][]).FullName, differences[0].Value2);
        }

        [Test]
        public void IntOfIntEquality1()
        {
            var a1 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 1, 2 }, new[] { 3, 5 } } };
            var a2 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 1, 2 }, new[] { 3, 5 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2);

            Assert.IsTrue(isEqual);
        }

        [Test]
        public void IntOfIntEquality2()
        {
            var a1 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 1, 2 }, new int[0] } };
            var a2 = new MultidimensionalArrays { IntOfInt = new[] { new[] { 1, 2 }, new int[0] } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2);

            Assert.IsTrue(isEqual);
        }

        [Test]
        public void IntOfIntEquality3()
        {
            var a1 = new MultidimensionalArrays { IntOfInt = new int[0][] };
            var a2 = new MultidimensionalArrays { IntOfInt = new int[0][] };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2);

            Assert.IsTrue(isEqual);
        }

        [Test]
        public void IntOfIntEquality4()
        {
            var a1 = new MultidimensionalArrays { IntOfInt = new int[0][] };
            var a2 = new MultidimensionalArrays();
            var comparer = new Comparer<MultidimensionalArrays>(new ComparisonSettings { EmptyAndNullEnumerablesEqual = true });

            var isEqual = comparer.Compare(a1, a2);

            Assert.IsTrue(isEqual);
        }

        [Test]
        public void IntOfIntEquality5()
        {
            var a1 = new MultidimensionalArrays();
            var a2 = new MultidimensionalArrays { IntOfInt = new int[0][] };
            var comparer = new Comparer<MultidimensionalArrays>(new ComparisonSettings { EmptyAndNullEnumerablesEqual = true });

            var isEqual = comparer.Compare(a1, a2);

            Assert.IsTrue(isEqual);
        }

        [Test]
        public void IntIntInequality1()
        {
            var a1 = new MultidimensionalArrays { IntInt = new[,] { { 1, 2 } } };
            var a2 = new MultidimensionalArrays { IntInt = new[,] { { 1, 3 }, { 1, 3 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntInt.Dimension0", differences[0].MemberPath);
            Assert.AreEqual("1", differences[0].Value1);
            Assert.AreEqual("2", differences[0].Value2);
        }

        [Test]
        public void IntIntInequality2()
        {
            var a1 = new MultidimensionalArrays { IntInt = new[,] { { 1, 2 }, { 1, 3 } } };
            var a2 = new MultidimensionalArrays { IntInt = new[,] { { 1, 3, 4 }, { 1, 3, 8 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntInt.Dimension1", differences[0].MemberPath);
            Assert.AreEqual("2", differences[0].Value1);
            Assert.AreEqual("3", differences[0].Value2);
        }

        [Test]
        public void IntIntInequality3()
        {
            var a1 = new MultidimensionalArrays { IntInt = new[,] { { 1, 2 } } };
            var a2 = new MultidimensionalArrays { IntInt = new[,] { { 1, 3 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntInt[0,1]", differences[0].MemberPath);
            Assert.AreEqual("2", differences[0].Value1);
            Assert.AreEqual("3", differences[0].Value2);
        }

        [Test]
        public void IntIntInequality4()
        {
            var a1 = new MultidimensionalArrays { IntInt = new[,] { { 1, 2 }, { 3, 4 } } };
            var a2 = new MultidimensionalArrays { IntInt = new[,] { { 0, 2 }, { 3, 4 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntInt[0,0]", differences[0].MemberPath);
            Assert.AreEqual("1", differences[0].Value1);
            Assert.AreEqual("0", differences[0].Value2);
        }

        [Test]
        public void IntIntInequality5()
        {
            var a1 = new MultidimensionalArrays { IntInt = new[,] { { 1, 2 }, { 3, 0 } } };
            var a2 = new MultidimensionalArrays { IntInt = new[,] { { 1, 2 }, { 3, 4 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntInt[1,1]", differences[0].MemberPath);
            Assert.AreEqual("0", differences[0].Value1);
            Assert.AreEqual("4", differences[0].Value2);
        }

        [Test]
        public void IntIntInequality6()
        {
            var a1 = new MultidimensionalArrays { IntInt = null };
            var a2 = new MultidimensionalArrays { IntInt = new int[0, 0] };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntInt", differences[0].MemberPath);
            Assert.AreEqual(string.Empty, differences[0].Value1);
            Assert.AreEqual(typeof(int[,]).FullName, differences[0].Value2);
        }

        [Test]
        public void IntIntInequality7()
        {
            var a1 = new MultidimensionalArrays { IntInt = new int[0, 0] };
            var a2 = new MultidimensionalArrays { IntInt = null };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2, out var differencesEnum);
            var differences = differencesEnum.ToList();

            Assert.IsFalse(isEqual);
            CollectionAssert.IsNotEmpty(differences);
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual("IntInt", differences[0].MemberPath);
            Assert.AreEqual(typeof(int[,]).FullName, differences[0].Value1);
            Assert.AreEqual(string.Empty, differences[0].Value2);
        }

        [Test]
        public void IntIntEquality1()
        {
            var a1 = new MultidimensionalArrays { IntInt = new[,] { { 1, 2 }, { 3, 4 } } };
            var a2 = new MultidimensionalArrays { IntInt = new[,] { { 1, 2 }, { 3, 4 } } };
            var comparer = new Comparer<MultidimensionalArrays>();

            var isEqual = comparer.Compare(a1, a2);

            Assert.IsTrue(isEqual);
        }

        [Test]
        public void IntIntEquality2()
        {
            var a1 = new MultidimensionalArrays { IntInt = null };
            var a2 = new MultidimensionalArrays { IntInt = new int[0, 0] };
            var comparer = new Comparer<MultidimensionalArrays>(new ComparisonSettings { EmptyAndNullEnumerablesEqual = true });

            var isEqual = comparer.Compare(a1, a2);

            Assert.IsTrue(isEqual);
        }

        [Test]
        public void IntIntEquality3()
        {
            var a1 = new MultidimensionalArrays { IntInt = new int[0, 0] };
            var a2 = new MultidimensionalArrays { IntInt = null };
            var comparer = new Comparer<MultidimensionalArrays>(new ComparisonSettings { EmptyAndNullEnumerablesEqual = true });

            var isEqual = comparer.Compare(a1, a2);

            Assert.IsTrue(isEqual);
        }
    }
}
