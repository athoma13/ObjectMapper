﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OMap
{
    public class BuilderNode<TSource, TTarget>
    {
        private readonly IInternalBuilder _internalBuilder;

        internal BuilderNode(IInternalBuilder internalBuilder)
        {
            _internalBuilder = internalBuilder;
        }

        public BuilderNode<TSource, TTarget> MapProperty<TProperty>(Expression<Func<TSource, TProperty>> from, Expression<Func<TTarget, TProperty>> to)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationSourceTargetExpressionEntry(from, to, MapType.MapProperty));
            return this;
        }

        public BuilderNode<TSource, TTarget> MapObject<TProperty1, TProperty2>(Expression<Func<TSource, TProperty1>> from, Expression<Func<TTarget, TProperty2>> to)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationSourceTargetExpressionEntry(from, to, MapType.MapObject));
            return this;
        }

        public BuilderNode<TSource, TTarget> MapCollection<TProperty1, TProperty2>(Expression<Func<TSource, IEnumerable<TProperty1>>> from, Expression<Func<TTarget, IList<TProperty2>>> to)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationSourceTargetExpressionEntry(from, to, MapType.MapCollection));
            return this;
        }

        public BuilderNode<TSource, TTarget> MapFunction(Action<TSource, TTarget> action)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationFunctionMapEntry(action, typeof(TSource), typeof(TTarget), null));
            return this;
        }




    }

    public class BuilderNode<TSource, TTarget, TDependencies>
    {
        private readonly IInternalBuilder _internalBuilder;

        internal BuilderNode(IInternalBuilder internalBuilder)
        {
            _internalBuilder = internalBuilder;
        }

        public BuilderNode<TSource, TTarget, TDependencies> MapProperty<TProperty>(Expression<Func<TSource, TProperty>> from, Expression<Func<TTarget, TProperty>> to)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationSourceTargetExpressionEntry(from, to, MapType.MapProperty));
            return this;
        }
        public BuilderNode<TSource, TTarget, TDependencies> MapProperty<TProperty>(Expression<Func<TSource, TDependencies, TProperty>> from, Expression<Func<TTarget, TProperty>> to)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationSourceTargetExpressionEntry(from, to, MapType.MapProperty));
            return this;
        }

        public BuilderNode<TSource, TTarget, TDependencies> MapObject<TProperty1, TProperty2>(Expression<Func<TSource, TProperty1>> from, Expression<Func<TTarget, TProperty2>> to)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationSourceTargetExpressionEntry(from, to, MapType.MapObject));
            return this;
        }
        public BuilderNode<TSource, TTarget, TDependencies> MapCollection<TProperty1, TProperty2>(Expression<Func<TSource, IEnumerable<TProperty1>>> from, Expression<Func<TTarget, IList<TProperty2>>> to)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationSourceTargetExpressionEntry(from, to, MapType.MapCollection));
            return this;
        }
        public BuilderNode<TSource, TTarget, TDependencies> MapFunction(Action<TSource, TTarget, TDependencies> action)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationFunctionMapEntry(action, typeof(TSource), typeof(TTarget), typeof(TDependencies)));
            return this;
        }
        public BuilderNode<TSource, TTarget, TDependencies> MapFunction(Action<TSource, TTarget> action)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationFunctionMapEntry(action, typeof(TSource), typeof(TTarget), null));
            return this;
        }
    }

    public class BuilderNodeWithDependencies<TSource, TTarget>
    {
        private readonly IInternalBuilder _internalBuilder;

        internal BuilderNodeWithDependencies(IInternalBuilder internalBuilder)
        {
            _internalBuilder = internalBuilder;
        }
        public BuilderNode<TSource, TTarget> MapProperty<TProperty>(Expression<Func<TSource, TProperty>> from, Expression<Func<TTarget, TProperty>> to)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationSourceTargetExpressionEntry(from, to, MapType.MapProperty));
            return new BuilderNode<TSource, TTarget>(_internalBuilder);
        }
        public BuilderNode<TSource, TTarget> MapObject<TProperty1, TProperty2>(Expression<Func<TSource, TProperty1>> from, Expression<Func<TTarget, TProperty2>> to)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationSourceTargetExpressionEntry(from, to, MapType.MapObject));
            return new BuilderNode<TSource, TTarget>(_internalBuilder);
        }
        public BuilderNode<TSource, TTarget> MapCollection<TProperty1, TProperty2>(Expression<Func<TSource, IEnumerable<TProperty1>>> from, Expression<Func<TTarget, IList<TProperty2>>> to)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationSourceTargetExpressionEntry(from, to, MapType.MapCollection));
            return new BuilderNode<TSource, TTarget>(_internalBuilder);
        }
        public BuilderNode<TSource, TTarget> MapFunction(Action<TSource, TTarget> action)
        {
            _internalBuilder.AddEntry(new BuilderConfigurationFunctionMapEntry(action, typeof(TSource), typeof(TTarget), null));
            return new BuilderNode<TSource, TTarget>(_internalBuilder);
        }

        public BuilderNodeWithDependencies<TSource, TTarget> MapAll(params Expression<Func<TTarget, object>>[] exceptions)
        {
            MapAllHelper.MapAll(_internalBuilder, typeof(TSource), typeof(TTarget), exceptions);
            return this;
        }


        public BuilderNode<TSource, TTarget, Tuple<T>> WithDependencies<T>(string name = null)
        {
            _internalBuilder.SetNamedResolutions(new Dictionary<Type, string>() { { typeof(T), name } });
            return new BuilderNode<TSource, TTarget, Tuple<T>>(_internalBuilder);
        }
        public BuilderNode<TSource, TTarget, Tuple<T1, T2>> WithDependencies<T1, T2>(string name1 = null, string name2 = null)
        {
            _internalBuilder.SetNamedResolutions(new Dictionary<Type, string>() { { typeof(T1), name1 }, { typeof(T2), name2 } });
            return new BuilderNode<TSource, TTarget, Tuple<T1, T2>>(_internalBuilder);
        }
        public BuilderNode<TSource, TTarget, Tuple<T1, T2, T3>> WithDependencies<T1, T2, T3>(string name1 = null, string name2 = null, string name3 = null)
        {
            _internalBuilder.SetNamedResolutions(new Dictionary<Type, string>() { { typeof(T1), name1 }, { typeof(T2), name2 }, { typeof(T3), name3 } });
            return new BuilderNode<TSource, TTarget, Tuple<T1, T2, T3>>(_internalBuilder);
        }
        public BuilderNode<TSource, TTarget, Tuple<T1, T2, T3, T4>> WithDependencies<T1, T2, T3, T4>(string name1 = null, string name2 = null, string name3 = null, string name4 = null)
        {
            _internalBuilder.SetNamedResolutions(new Dictionary<Type, string>() { { typeof(T1), name1 }, { typeof(T2), name2 }, { typeof(T3), name3 }, { typeof(T4), name4 } });
            return new BuilderNode<TSource, TTarget, Tuple<T1, T2, T3, T4>>(_internalBuilder);
        }
        public BuilderNode<TSource, TTarget, Tuple<T1, T2, T3, T4, T5>> WithDependencies<T1, T2, T3, T4, T5>(string name1 = null, string name2 = null, string name3 = null, string name4 = null, string name5 = null)
        {
            _internalBuilder.SetNamedResolutions(new Dictionary<Type, string>() { { typeof(T1), name1 }, { typeof(T2), name2 }, { typeof(T3), name3 }, { typeof(T4), name4 }, { typeof(T5), name5 } });
            return new BuilderNode<TSource, TTarget, Tuple<T1, T2, T3, T4, T5>>(_internalBuilder);
        }
        public BuilderNode<TSource, TTarget, Tuple<T1, T2, T3, T4, T5, T6>> WithDependencies<T1, T2, T3, T4, T5, T6>(string name1 = null, string name2 = null, string name3 = null, string name4 = null, string name5 = null, string name6 = null)
        {
            _internalBuilder.SetNamedResolutions(new Dictionary<Type, string>() { { typeof(T1), name1 }, { typeof(T2), name2 }, { typeof(T3), name3 }, { typeof(T4), name4 }, { typeof(T5), name5 }, { typeof(T6), name6 } });
            return new BuilderNode<TSource, TTarget, Tuple<T1, T2, T3, T4, T5, T6>>(_internalBuilder);
        }
        public BuilderNode<TSource, TTarget, Tuple<T1, T2, T3, T4, T5, T6, T7>> WithDependencies<T1, T2, T3, T4, T5, T6, T7>(string name1 = null, string name2 = null, string name3 = null, string name4 = null, string name5 = null, string name6 = null, string name7 = null)
        {
            _internalBuilder.SetNamedResolutions(new Dictionary<Type, string>() { { typeof(T1), name1 }, { typeof(T2), name2 }, { typeof(T3), name3 }, { typeof(T4), name4 }, { typeof(T5), name5 }, { typeof(T6), name6 }, { typeof(T7), name7 } });
            return new BuilderNode<TSource, TTarget, Tuple<T1, T2, T3, T4, T5, T6, T7>>(_internalBuilder);
        }
        public BuilderNode<TSource, TTarget, Tuple<T1, T2, T3, T4, T5, T6, T7, T8>> WithDependencies<T1, T2, T3, T4, T5, T6, T7, T8>(string name1 = null, string name2 = null, string name3 = null, string name4 = null, string name5 = null, string name6 = null, string name7 = null, string name8 = null)
        {
            _internalBuilder.SetNamedResolutions(new Dictionary<Type, string>() { { typeof(T1), name1 }, { typeof(T2), name2 }, { typeof(T3), name3 }, { typeof(T4), name4 }, { typeof(T5), name5 }, { typeof(T6), name6 }, { typeof(T7), name7 }, { typeof(T8), name8 } });
            return new BuilderNode<TSource, TTarget, Tuple<T1, T2, T3, T4, T5, T6, T7, T8>>(_internalBuilder);
        }
    }

    internal interface IInternalBuilder
    {
        void AddEntry(BuilderConfigurationEntry entry);
        void SetNamedResolutions(IDictionary<Type, string> namedResolutions);
        BuilderConfigurationEntry[] GetEntries();
    }

    public enum BuildeConfigurationEntryType
    {
        SourceTargetWithDependency,
        SourceTargetNoDependency,
    }

    internal abstract class BuilderConfigurationEntry
    {
        public Dictionary<Type, string> NamedResolutions { get; set; }
        public MapType MapType { get; private set; }
        public Type SourceType { get; private set; }
        public Type TargetType { get; private set; }
        public bool HasDependencyArgument { get; private set; }

        protected BuilderConfigurationEntry(MapType mapType, Type sourceType, Type targetType, bool hasDependencyArgument)
        {
            HasDependencyArgument = hasDependencyArgument;
            MapType = mapType;
            SourceType = sourceType;
            TargetType = targetType;
            NamedResolutions = new Dictionary<Type, string>();
        }
    }

    internal class BuilderConfigurationSourceTargetExpressionEntry : BuilderConfigurationEntry
    {
        public LambdaExpression SourceExpression { get; private set; }
        public LambdaExpression TargetExpression { get; private set; }

        public BuilderConfigurationSourceTargetExpressionEntry(LambdaExpression sourceExpression, LambdaExpression targetExpression, MapType mapType)
            : base(mapType, sourceExpression.Parameters[0].Type, targetExpression.Parameters[0].Type, sourceExpression.Parameters.Count > 1)
        {
            SourceExpression = sourceExpression;
            TargetExpression = targetExpression;
        }
    }

    internal class BuilderConfigurationFunctionMapEntry : BuilderConfigurationEntry
    {
        public Delegate Action { get; private set; }
        public Type DependencyTupleType { get; private set; }

        public BuilderConfigurationFunctionMapEntry(Delegate action, Type sourceType, Type targetType, Type dependencyTupleType)
            : base(MapType.MapFunction, sourceType, targetType, dependencyTupleType != null)
        {
            Action = action;
            DependencyTupleType = dependencyTupleType;
        }
    }


    public class ConfigurationBuilder : IInternalBuilder
    {
        private readonly List<BuilderConfigurationEntry> _builderEntries = new List<BuilderConfigurationEntry>();
        private IDictionary<Type, string> _namedResolutions = new Dictionary<Type, string>();

        public BuilderNodeWithDependencies<TSource, TTarget> CreateMap<TSource, TTarget>()
        {
            return new BuilderNodeWithDependencies<TSource, TTarget>(this);
        }
        void IInternalBuilder.AddEntry(BuilderConfigurationEntry entry)
        {
            //Copy NamedResolutions to the entry
            entry.NamedResolutions = new Dictionary<Type, string>(_namedResolutions);
            _builderEntries.Add(entry);
        }
        void IInternalBuilder.SetNamedResolutions(IDictionary<Type, string> names)
        {
            //NOTE: This is temporary - may be overwritten multiple times during configuration of the Mapper.
            _namedResolutions = names;
        }

        BuilderConfigurationEntry[] IInternalBuilder.GetEntries()
        {
            return _builderEntries.ToArray();
        }

        public MappingConfiguration Build()
        {
            var configEntries = new List<MappingConfigurationEntry>();
            foreach (var entry in _builderEntries)
            {
                var sourceType = entry.SourceType;
                var targetType = entry.TargetType;
                var entryDescription = CreateEntryDescription(entry, sourceType, targetType);
                var hasDependency = entry.HasDependencyArgument;

                if (entry.MapType == MapType.MapProperty)
                {
                    var sourceTargetEntry = (BuilderConfigurationSourceTargetExpressionEntry)entry;
                    var action = CreateMappingAction(sourceTargetEntry.SourceExpression, sourceTargetEntry.TargetExpression);
                    var configEntry = new MappingConfigurationPropertyEntry(
                        sourceType,
                        targetType,
                        entryDescription,
                        action,
                        hasDependency ? sourceTargetEntry.SourceExpression.Parameters[1].Type : null,
                        sourceTargetEntry.NamedResolutions);

                    configEntries.Add(configEntry);
                }
                else if (entry.MapType == MapType.MapObject)
                {
                    var sourceTargetEntry = (BuilderConfigurationSourceTargetExpressionEntry)entry;
                    var targetMember = MemberExpessionVisitor.GetMember(sourceTargetEntry.TargetExpression);
                    if (targetMember == null) throw new MappingException("Could not find object type on target");

                    var configEntry = new MappingConfigurationObjectEntry(
                        sourceType,
                        targetType,
                        entryDescription,
                        CreateGetterFunction(sourceTargetEntry.SourceExpression),
                        CreateGetterFunction(sourceTargetEntry.TargetExpression),
                        CreateObjectSetterMappingAction(sourceTargetEntry.TargetExpression),
                        sourceTargetEntry.TargetExpression.ReturnType);

                    configEntries.Add(configEntry);
                }
                else if (entry.MapType == MapType.MapCollection)
                {
                    var sourceTargetEntry = (BuilderConfigurationSourceTargetExpressionEntry)entry;
                    var targetMember = MemberExpessionVisitor.GetMember(sourceTargetEntry.TargetExpression);
                    if (targetMember == null) throw new MappingException("Could not find collection type on target");

                    var configEntry = new MappingConfigurationCollectionEntry(
                        sourceType,
                        targetType,
                        entryDescription,
                        CreateGetterFunction(sourceTargetEntry.SourceExpression),
                        CreateGetterFunction(sourceTargetEntry.TargetExpression),
                        CreateObjectSetterMappingAction(sourceTargetEntry.TargetExpression),
                        targetMember.Type);

                    configEntries.Add(configEntry);
                }
                else if (entry.MapType == MapType.MapFunction)
                {
                    var functionEntry = (BuilderConfigurationFunctionMapEntry)entry;

                    var configEntry = new MappingConfigurationFunctionEntry(
                        sourceType,
                        targetType,
                        entryDescription,
                        functionEntry.Action,
                        functionEntry.DependencyTupleType,
                        functionEntry.NamedResolutions
                        );

                    configEntries.Add(configEntry);
                }


            }
            return new MappingConfiguration(configEntries);
        }

        private static string CreateEntryDescription(BuilderConfigurationEntry entry, Type sourceType, Type targetType)
        {
            var sourceTypeName = sourceType.Name;
            var targetTypeName = targetType.Name;

            if (entry.MapType == MapType.MapFunction) return string.Format("MappingFunction({0}, {1})", sourceTypeName, targetTypeName);

            //Entry MUST be BuilderConfigurationSourceTargetExpressionEntry for any other MapTypes...
            var sourceTargetEntry = (BuilderConfigurationSourceTargetExpressionEntry)entry;
            var sourceMember = MemberExpessionVisitor.GetMember(sourceTargetEntry.SourceExpression);
            var targetMember = MemberExpessionVisitor.GetMember(sourceTargetEntry.TargetExpression);

            var result = string.Format("{0}.{1} -> {2}.{3}",
                sourceTypeName,
                sourceMember == null ? "?" : sourceMember.Member.Name,
                targetTypeName,
                targetMember == null ? "?" : targetMember.Member.Name
                );

            return result;
        }

        private static Delegate CreateObjectSetterMappingAction(LambdaExpression target)
        {
            var pTarget = Expression.Parameter(target.Parameters[0].Type, "target");
            var pObject = Expression.Parameter(((MemberExpression)target.Body).Type, "newObject");

            var targetProperty = target.Body;
            targetProperty = Replace(targetProperty, target.Parameters[0], pTarget);

            var setter = Expression.Assign(targetProperty, pObject);
            var lamda = Expression.Lambda(setter, pTarget, pObject);
            return lamda.Compile();
        }

        private static Delegate CreateGetterFunction(LambdaExpression expression)
        {
            return expression.Compile();
        }


        private static Delegate CreateMappingAction(LambdaExpression source, LambdaExpression target)
        {
            var hasDependency = source.Parameters.Count > 1;

            var pSource = Expression.Parameter(source.Parameters[0].Type, "source");
            var pDependencies = hasDependency ? Expression.Parameter(source.Parameters[1].Type, "dependencies") : null;
            var body = source.Body;
            body = Replace(body, source.Parameters[0], pSource);
            if (hasDependency) body = Replace(body, source.Parameters[1], pDependencies);

            var pTarget = Expression.Parameter(target.Parameters[0].Type, "target");
            var targetProperty = target.Body;
            targetProperty = Replace(targetProperty, target.Parameters[0], pTarget);

            var setter = Expression.Assign(targetProperty, body);
            var lamda = hasDependency ? Expression.Lambda(setter, pSource, pTarget, pDependencies) : Expression.Lambda(setter, pSource, pTarget);
            return lamda.Compile();
        }


        private class MemberExpessionVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _parameter;
            private MemberExpression Result { get; set; }

            private MemberExpessionVisitor(ParameterExpression parameter)
            {
                _parameter = parameter;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                if (Result == null && _parameter == node.Expression) Result = node;
                return base.VisitMember(node);
            }

            public static MemberExpression GetMember(LambdaExpression expression)
            {
                var visitor = new MemberExpessionVisitor(expression.Parameters[0]);
                visitor.Visit(expression);
                return visitor.Result;
            }
        }

        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _node;
            private readonly Expression _withNode;

            public ReplaceExpressionVisitor(Expression node, Expression withNode)
            {
                _node = node;
                _withNode = withNode;
            }

            public override Expression Visit(Expression node)
            {
                return node == _node ? _withNode : base.Visit(node);
            }
        }

        private static Expression Replace(Expression expression, Expression node, Expression withNode)
        {
            return new ReplaceExpressionVisitor(node, withNode).Visit(expression);
        }
    }
}