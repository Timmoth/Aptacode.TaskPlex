using Aptacode.Core.Tasks.Transformations;
using Aptacode.TaskPlex.Core_Tests.Utilites;
using Aptacode_TaskCoordinator.Tests.Utilites;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aptacode.TaskPlex.Core_Tests
{
    public class Transformation_Tests
    {
        TestRectangle testRectangle;

        [SetUp]
        public void Setup()
        {
            testRectangle = new TestRectangle();
        }

        [Test]
        public void StartAndFinishEvents()
        {
            PropertyTransformation transformation = PropertyTransformation_Helpers.GetIntTransformation(testRectangle, "Width", 0, 100, 10, 1);

            bool startedCalled = false;
            bool finishedCalled = false;

            transformation.OnStarted += (s, e) =>
            {
                startedCalled = true;
            };

            transformation.OnFinished += (s, e) =>
            {
                finishedCalled = true;
            };

            transformation.StartAsync().Wait();

            Assert.That(startedCalled == true && finishedCalled == true);
        }

        [Test]
        public void ZeroTransformationDuration()
        {
            PropertyTransformation transformation = PropertyTransformation_Helpers.GetDoubleTransformation(testRectangle, "Opacity", 0, 1, 0, 1);

            List<double> changeLog = new List<double>();
            testRectangle.OnOpacityChanged += (s, e) =>
            {
                changeLog.Add(e.NewValue);
            };

            transformation.StartAsync().Wait();

            List<double> expectedChangeLog = new List<double>() { 1.0 };
            Assert.That(changeLog.SequenceEqual(expectedChangeLog, new DoubleComparer()));
        }


        [Test]
        public void ZeroIntervalDuration()
        {
            PropertyTransformation transformation = PropertyTransformation_Helpers.GetDoubleTransformation(testRectangle, "Opacity", 0, 1, 1, 0);

            List<double> changeLog = new List<double>();
            testRectangle.OnOpacityChanged += (s, e) =>
            {
                changeLog.Add(e.NewValue);
            };

            transformation.StartAsync().Wait();

            Assert.That(new DoubleComparer().Equals(testRectangle.Opacity, 1.0));
        }
    }
}