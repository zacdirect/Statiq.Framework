﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyam.Core.Modules;

namespace Wyam.Core.Tests.Modules
{
    [TestFixture]
    public class SwitchFixture
    {
        [Test]
        public void SwitchResultsInCorrectCounts()
        {
            // Given
            Engine engine = new Engine();
            engine.Trace.AddListener(new TestTraceListener());
            CountModule a = new CountModule("A") { AdditionalOutputs = 2 };
            CountModule b = new CountModule("B");
            CountModule c = new CountModule("C");
            CountModule d = new CountModule("D");

            engine.Pipelines.Add(a, new Switch((x, y) => x.Content).Case("1", b).Case("2", c).Default(d));

            // When
            engine.Execute();

            // Then
            Assert.AreEqual(1, a.ExecuteCount);
            Assert.AreEqual(1, b.ExecuteCount);
            Assert.AreEqual(1, c.ExecuteCount);
            Assert.AreEqual(1, d.ExecuteCount);
        }

        [Test]
        public void SwitchNoCasesResultsInCorrectCounts()
        {
            // Given
            Engine engine = new Engine();
            engine.Trace.AddListener(new TestTraceListener());
            CountModule a = new CountModule("A") { AdditionalOutputs = 2 };
            CountModule b = new CountModule("B");
            CountModule c = new CountModule("C");

            engine.Pipelines.Add(a, new Switch((x, y) => x.Content).Default(b), c);

            // When
            engine.Execute();

            // Then
            Assert.AreEqual(1, a.ExecuteCount);
            Assert.AreEqual(1, b.ExecuteCount);
            Assert.AreEqual(3, b.InputCount);
            Assert.AreEqual(3, b.OutputCount);
            Assert.AreEqual(3, c.InputCount);
        }

        [Test]
        public void MissingDefaultResultsInCorrectCounts()
        {
            // Given
            Engine engine = new Engine();
            engine.Trace.AddListener(new TestTraceListener());
            CountModule a = new CountModule("A") { AdditionalOutputs = 2 };
            CountModule b = new CountModule("B");
            CountModule c = new CountModule("C");

            engine.Pipelines.Add(a, new Switch((x, y) => x.Content).Case("1", b), c);

            // When
            engine.Execute();

            // Then
            Assert.AreEqual(1, a.ExecuteCount);
            Assert.AreEqual(1, b.ExecuteCount);
            Assert.AreEqual(1, b.InputCount);
            Assert.AreEqual(1, b.OutputCount);
            Assert.AreEqual(3, c.InputCount);
        }

        [Test]
        public void ArrayInCaseResultsInCorrectCounts()
        {
            // Given
            Engine engine = new Engine();
            engine.Trace.AddListener(new TestTraceListener());
            CountModule a = new CountModule("A") { AdditionalOutputs = 2 };
            CountModule b = new CountModule("B");
            CountModule c = new CountModule("C");

            engine.Pipelines.Add(a, new Switch((x, y) => x.Content).Case(new string[] { "1", "2" }, b), c);

            // When
            engine.Execute();

            // Then
            Assert.AreEqual(1, a.ExecuteCount);
            Assert.AreEqual(1, b.ExecuteCount);
            Assert.AreEqual(2, b.InputCount);
            Assert.AreEqual(2, b.OutputCount);
            Assert.AreEqual(3, c.InputCount);
        }

        [Test]
        public void OmittingCasesAndDefaultResultsInCorrectCounts()
        {
            // Given
            Engine engine = new Engine();
            engine.Trace.AddListener(new TestTraceListener());
            CountModule a = new CountModule("A") { AdditionalOutputs = 2 };
            CountModule b = new CountModule("B");

            engine.Pipelines.Add(a, new Switch((x, y) => x.Content), b);

            // When
            engine.Execute();

            // Then
            Assert.AreEqual(3, b.InputCount);
        }
    }
}
