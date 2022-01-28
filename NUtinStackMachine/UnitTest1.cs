using NUnit.Framework;
using StackMachine;
using System;
using System.Collections.Generic;

namespace NUtinStackMachine
{
    public class Tests
    {
        EngineExecutor engine;

        [SetUp]
        public void Setup()
        {
            engine = new EngineExecutor();
            engine.Variables["get100"] = new VariableGet
            {
                Func = () => 100L,
                VariableType = VariableType.Int64
            };
            engine.Variables["add"] = new VariableFunc
            {
                Func = vs => new Variable() { Value = ((long)vs[0].Value) + ((long)vs[1].Value), VariableType = VariableType.Int64 }
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
    }
}