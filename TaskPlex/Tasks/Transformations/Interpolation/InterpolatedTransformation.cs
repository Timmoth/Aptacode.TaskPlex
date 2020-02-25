﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Aptacode.TaskPlex.Enums;
using Aptacode.TaskPlex.Interpolators;
using Aptacode.TaskPlex.Interpolators.Easers;

namespace Aptacode.TaskPlex.Tasks.Transformations.Interpolation
{
    public class InterpolatedTransformation<TClass, TProperty> : PropertyTransformation<TClass, TProperty>
        where TClass : class
    {
        private readonly Interpolator<TProperty> _interpolator;
        private IEnumerator<TProperty> _interpolationEnumerator;

        private Timer _timer;

        protected InterpolatedTransformation(TClass target,
            string property,
            Func<TProperty> endValue,
            TimeSpan duration,
            Interpolator<TProperty> interpolator,
            RefreshRate refreshRate = RefreshRate.Normal, EaserFunction easerFunction = null) : base(target,
            property,
            endValue,
            duration,
            refreshRate)
        {
            _interpolator = interpolator;
            Easer = easerFunction ?? Easers.Linear;
        }

        /// <summary>
        ///     Returns the easing function for this transformation
        /// </summary>
        public EaserFunction Easer { get; set; }

        protected override async Task InternalTask()
        {
            var startValue = GetValue();
            var endValue = GetEndValue();

            _interpolationEnumerator =
                _interpolator.Interpolate(startValue, endValue, StepCount, Easer).GetEnumerator();
            _timer = new Timer((int) RefreshRate);
            _timer.Elapsed += Timer_Elapsed;

            _timer?.Start();

            while (State != TaskState.Stopped && !CancellationTokenSource.IsCancellationRequested)
            {
                await Task.Delay(1).ConfigureAwait(false);
            }

            _timer?.Stop();
            _timer?.Dispose();
            _interpolationEnumerator?.Dispose();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!IsRunning())
            {
                return;
            }

            if (!_interpolationEnumerator.MoveNext())
            {
                State = TaskState.Stopped;
                return;
            }

            SetValue(_interpolationEnumerator.Current);
        }
    }
}