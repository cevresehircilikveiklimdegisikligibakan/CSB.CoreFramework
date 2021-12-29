using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSB.Core.WebApp.Helpers
{
    public static class ListGenerator
    {
        public static List<TDestiny> WithLambda<TSourse, TDestiny>(List<TSourse> source, Func<TSourse, TDestiny> func) =>
            new List<TDestiny>(source.Select(s => func(s)));

        public static List<TDestiny> WithFor<TSourse, TDestiny>(List<TSourse> source, Func<TSourse, TDestiny> func)
        {
            var list = new List<TDestiny>(source.Count);

            for (int i = 0; i < source.Count; i++)
                list.Add(func(source[i]));

            return list;
        }

        public static List<TDestiny> WithForeach<TSourse, TDestiny>(List<TSourse> source, Func<TSourse, TDestiny> func)
        {
            var list = new List<TDestiny>(source.Count);

            foreach (var item in source)
                list.Add(func(item));

            return list;
        }

        public static List<TDestiny> WithParallelFor<TSourse, TDestiny>(List<TSourse> source, Func<TSourse, TDestiny> func)
        {
            BlockingCollection<TDestiny> list = new BlockingCollection<TDestiny>(source.Count);

            Parallel.For(0, source.Count, (i) => list.Add(func(source[i])));

            return list.ToList();
        }

        public static List<TDestiny> WithParallelForeach<TSourse, TDestiny>(List<TSourse> source, Func<TSourse, TDestiny> func)
        {
            BlockingCollection<TDestiny> list = new BlockingCollection<TDestiny>(source.Count);

            Parallel.ForEach(source, (item) => list.Add(func(item)));

            return list.ToList();
        }
    }
}