﻿using System;
using System.Drawing;
using Aptacode.TaskPlex.Tasks;
using Aptacode.TaskPlex.Tasks.Transformation;
using Aptacode.TaskPlex.Tasks.Transformation.Interpolator.Easers;

namespace Aptacode.TaskPlex
{
    public static class TaskFactory
    {
        public static SequentialGroupTask Sequential(params BaseTask[] tasks)
        {
            return new SequentialGroupTask(tasks);
        }

        public static ParallelGroupTask Parallel(params BaseTask[] tasks)
        {
            return new ParallelGroupTask(tasks);
        }

        public static WaitTask Wait(TimeSpan duration)
        {
            return new WaitTask(duration);
        }

        public static IntTransformation<T> Create<T>(T target, string property, int endValue, TimeSpan duration,
            RefreshRate refreshRate = RefreshRate.Normal) where T : class
        {
            return IntTransformation<T>.Create(target, property, endValue, duration, refreshRate);
        }

        public static DoubleTransformation<T> Create<T>(T target, string property, double endValue, TimeSpan duration,
            RefreshRate refreshRate = RefreshRate.Normal) where T : class
        {
            return DoubleTransformation<T>.Create(target, property, endValue, duration, refreshRate);
        }

        public static IntTransformation<T> Create<T>(T target, string property, int endValue, TimeSpan duration,
            Easer easer,
            RefreshRate refreshRate = RefreshRate.Normal) where T : class
        {
            var transformation = IntTransformation<T>.Create(target, property, endValue, duration, refreshRate);
            transformation.Easer = easer;
            return transformation;
        }

        public static DoubleTransformation<T> Create<T>(T target, string property, double endValue, TimeSpan duration,
            Easer easer,
            RefreshRate refreshRate = RefreshRate.Normal) where T : class
        {
            var transformation = DoubleTransformation<T>.Create(target, property, endValue, duration, refreshRate);
            transformation.Easer = easer;
            return transformation;
        }


        public static StringTransformation<T> Create<T>(T target, string property, string endValue, TimeSpan duration,
            RefreshRate refreshRate = RefreshRate.Normal) where T : class
        {
            return StringTransformation<T>.Create(target, property, endValue, duration, refreshRate);
        }

        public static ColorTransformation<T> Create<T>(T target, string property, Color endValue, TimeSpan duration,
            RefreshRate refreshRate = RefreshRate.Normal) where T : class
        {
            return ColorTransformation<T>.Create(target, property, endValue, duration, refreshRate);
        }

        public static BoolTransformation<T> Create<T>(T target, string property, bool endValue, TimeSpan duration,
            RefreshRate refreshRate = RefreshRate.Normal) where T : class
        {
            return BoolTransformation<T>.Create(target, property, endValue, duration, refreshRate);
        }
    }
}