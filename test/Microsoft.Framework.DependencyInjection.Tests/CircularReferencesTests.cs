// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.DependencyInjection.Tests.Fakes;
using Xunit;

namespace Microsoft.Framework.DependencyInjection.Tests
{
    public class CircularReferencesTests
    {
        [Fact]
        public void SelfCircularDependency()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<SelfCircularDependency>()
                .BuildServiceProvider();

            Assert.Throws<InvalidOperationException>(() => 
                serviceProvider.GetRequiredService<SelfCircularDependency>());
        }

        [Fact]
        public void SelfCircularDependencyWithInterface()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<ISelfCircularDependencyWithInterface, SelfCircularDependencyWithInterface>()
                .AddTransient<SelfCircularDependencyWithInterface>()
                .BuildServiceProvider();

            Assert.Throws<InvalidOperationException>(() => 
                serviceProvider.GetRequiredService<SelfCircularDependencyWithInterface>());
        }

        [Fact]
        public void DirectCircularDependecy()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<DirectCircularDependecyA>()
                .AddSingleton<DirectCircularDependecyB>()
                .BuildServiceProvider();

            Assert.Throws<InvalidOperationException>(() => 
                serviceProvider.GetRequiredService<DirectCircularDependecyA>());
        }

        [Fact]
        public void IndirectCircularDependecy()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IndirectCircularDependecyA>()
                .AddTransient<IndirectCircularDependecyB>()
                .AddTransient<IndirectCircularDependecyC>()
                .BuildServiceProvider();

            Assert.Throws<InvalidOperationException>(() => 
                serviceProvider.GetRequiredService<IndirectCircularDependecyA>());
        }
    }
}