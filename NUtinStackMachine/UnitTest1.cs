using NUnit.Framework;
using StackMachine;
using StackMachine.Symbol;
using System;
using System.Collections.Generic;

namespace NUtinStackMachine
{
    public class Tests
    {
        StackMachineEngine engine;

        [SetUp]
        public void Setup()
        {
            engine = new StackMachineEngine();
            engine.Variables["get100"] = new VariableGet
            {
                Func = () => 100L,
                VariableType = VariableType.Int64
            };
            engine.Variables["add"] = new VariableFunc
            {
                Func = input => new Variable() { Value = ((long)input[0].Value) + ((long)input[1].Value), VariableType = VariableType.Int64 }
            };
        }
        

        [Test]
        public void Test1()
        {
            var ret = engine.Execute("2 + 2");
            Assert.AreEqual((long)ret.Value, 4L);
        }

        [Test]
        public void Test2()
        {
            var ret = engine.Execute("2 + 2 * 4");
            Assert.AreEqual((long)ret.Value, 10L);
        }

        [Test]
        public void Test3()
        {
            var ret = engine.Execute("(2 + 2) * 4");
            Assert.AreEqual((long)ret.Value, 16L);
        }

        [Test]
        public void Test4()
        {
            var ret = engine.Execute("add(3,5)");
            Assert.AreEqual((long)ret.Value, 8L);
        }

        [Test]
        public void Test5()
        {
            var ret = engine.Execute("add(3+2,5*2)");
            Assert.AreEqual((long)ret.Value, 15L);
        }

        [Test]
        public void Test6()
        {
            var ret = engine.Execute("add(1,(2+3)*100)");
            Assert.AreEqual((long)ret.Value, 501L);
        }

        [Test]
        public void Test7()
        {
            var ret = engine.Execute("get100");
            Assert.AreEqual((long)ret.Value, 100L);
        }

        [Test]
        public void Test8()
        {
            var ret = engine.Execute("add(1,(2+3)*get100)");
            Assert.AreEqual((long)ret.Value, 501L);
        }

        [Test]
        public void Test9()
        {
            var ret = engine.Execute("add(1,(2+3)*(get100))");
            Assert.AreEqual((long)ret.Value, 501L);
        }

        [Test]
        public void Test10()
        {
            var ret = engine.Execute("add(1,(2+3)*(get100+1))");
            Assert.AreEqual((long)ret.Value, 506L);
        }

        [Test]
        public void Test11()
        {
            var ret = engine.Execute("\"Hello\" + ' ' + \"world\"");
            Assert.AreEqual((string)ret.Value, "Hello world");
        }

        [Test]
        public void Test12()
        {
            var ret = engine.Execute("1 + \"Hello\" + ' ' + \"world\" + (2 * 3)");
            Assert.AreEqual((string)ret.Value, "1Hello world6");
        }

        [Test]
        public void Test13()
        {
            var ret = engine.Execute("to(\"50\", \"long\") + 50");
            Assert.AreEqual((long)ret.Value, 100);
        }

        [Test]
        public void Test14()
        {
            engine.Execute("hello = \"Hello \" + \"world\"");
            var ret = engine.Execute("hello + 123");
            Assert.AreEqual((string)ret.Value, "Hello world123");
        }
    }
}