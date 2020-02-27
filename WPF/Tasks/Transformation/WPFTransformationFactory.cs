﻿using System;
using System.Windows;
using System.Windows.Media;
using Aptacode.TaskPlex.Enums;
using Aptacode.TaskPlex.Interpolators.Easers;

namespace Aptacode.TaskPlex.WPF.Tasks.Transformation
{
    public static class WPFTransformationFactory
    {
        public static PointTransformation<T> Create<T>(T target, string property, Point endValue, TimeSpan duration,
            EaserFunction easerFunction = null) where T : class
        {
            return PointTransformation<T>.Create(target, property, endValue,
                (int) (duration.TotalMilliseconds / (int) RefreshRate.High), easerFunction);
        }

        public static ColorTransformation<T> Create<T>(T target, string property, Color endValue, TimeSpan duration,
            EaserFunction easerFunction = null) where T : class
        {
            return ColorTransformation<T>.Create(target, property, endValue,
                (int) (duration.TotalMilliseconds / (int) RefreshRate.High), easerFunction);
        }

        public static ThicknessTransformation<T> Create<T>(T target, string property, Thickness endValue,
            TimeSpan duration, EaserFunction easerFunction = null) where T : class
        {
            return ThicknessTransformation<T>.Create(target, property, endValue,
                (int) (duration.TotalMilliseconds / (int) RefreshRate.High), easerFunction);
        }
    }
}