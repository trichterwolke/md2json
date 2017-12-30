/// <summary>
///   Mini-Biblothek zum Vergleichen von zwei Objekten im Rahmen eines Unittest.
/// </summary>
/// <author>Daniel Vogelsang NeosIT</author>
/// <modified>07.04.2017</modified>
/// <version>2.0.0.0</version>
namespace Markdown2Json.Test.Util
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Xunit.Sdk;

    /// <summary>
    ///   Klasse zum Vergleichen von zwei Objekten im Rahmen eines Unittest.
    ///   Wenn Abweichungen gefunden werden, wird eine Exception geworfen.
    /// </summary>
    /// <typeparam name="T">Typ der Objektes welches verglichen werden soll</typeparam>
    public class ObjectComparer<T> : IEqualityComparer<T>
    {
        private bool isSealed = false;
        private readonly Dictionary<string, IEqualityComparer<T>> assertions;
        internal Dictionary<string, IEqualityComparer<T>> Assertions { get { return assertions; } }

        public ObjectComparer()
            : this(new Dictionary<string, IEqualityComparer<T>>())
        { }

        private ObjectComparer(Dictionary<string, IEqualityComparer<T>> assertions)
        {
            this.assertions = assertions;
        }

        /// <summary>
        ///   Fügt eine neue Eigenschaft hinzu die verglichen wird.
        /// </summary>
        /// <param name="propertySelector">Eigenschaft die verglichen wird</param>
        public ObjectComparer<T> Add(Expression<Func<T, object>> propertySelector)
        {
            var name = GetName(propertySelector);
            var selector = propertySelector.Compile();
            return Add(selector, name);
        }

        /// <summary>
        ///   Fügt eine neue Eigenschaft hinzu die verglichen wird.
        /// </summary>
        /// <param name="name">Name der Eigenschaft der im Falle einer Abweichung in der Exception genannt wird</param>
        /// <param name="propertySelector">Eigenschaft die verglichen wird</param>
        public ObjectComparer<T> Add(Func<T, object> propertySelector, string name)
        {
            var result = isSealed ? Clone() : this;
            result.Assertions.Add(name, new AssertWithEquals<T>(propertySelector, name));
            return result;
        }

        /// <summary>
        ///   Fügt eine neue Eigenschaft hinzu die verglichen wird.
        /// </summary>
        /// <param name="name">Name der Eigenschaft der im Falle einer Abweichung in der Exception genannt wird </param>       
        /// <param name="propertySelector">Eigenschaft die verglichen wird</param>
        [Obsolete("Use Add(Func<T, object> propertySelector, string name)")]
        public ObjectComparer<T> Add(string name, Func<T, object> propertySelector)
        {
            return Add(propertySelector, name);
        }

        /// <summary>
        ///   Fügt eine neue Eigenschaft hinzu die mit Hilfe des übergeben IEqualityCompares verglichen wird.
        /// </summary>
        /// <typeparam name="TProperty">Typ der Eigenschaft die verglichen werden soll</typeparam>
        /// <param name="propertySelector">Eigenschaft die verglichen wird. </param>
        /// <param name="propertyComparer">IEqualitiyComperer der die Eigenschaften vergleicht.</param>
        public ObjectComparer<T> Add<TProperty>(Expression<Func<T, TProperty>> propertySelector, IEqualityComparer<TProperty> propertyComparer)
        {
            var name = GetName(propertySelector);
            var selector = propertySelector.Compile();
            return Add(selector, propertyComparer, name);
        }

        /// <summary>
        ///   Fügt eine neue Eigenschaft hinzu die mit Hilfe des übergeben IEqualityCompares verglichen wird.
        /// </summary>
        /// <typeparam name="TProperty">Typ der Eigenschaft die verglichen werden soll</typeparam>
        /// <param name="propertySelector">Eigenschaft die verglichen wird.</param>
        /// <param name="propertyComparer">IEqualitiyComperer der die Eigenschaften vergleicht. </param>
        public ObjectComparer<T> Add<TProperty>(Func<T, TProperty> propertySelector, IEqualityComparer<TProperty> propertyComparer, string name)
        {
            var result = isSealed ? Clone() : this;
            result.Assertions.Add(name, new AssertWithComparer<T, TProperty>(propertySelector, propertyComparer, name));
            return result;
        }

        public ObjectComparer<T> Remove(Expression<Func<T, object>> propertySelector)
        {
            return Remove(GetName(propertySelector));
        }

        public ObjectComparer<T> Remove(string name)
        {
            var result = isSealed ? Clone() : this;
            result.Assertions.Remove(name);
            return result;
        }

        public ObjectComparer<T> Seal()
        {
            this.isSealed = true;
            return this;
        }

        /// <summary>
        ///   Prüft ob die Eigenschaften von zwei Objekten gleich sind.
        ///   Wenn einen Eigenschaft nicht gleich ist, wird eine Exception geworfen.
        /// </summary>
        /// <param name="expected">Der Erwartungswert</param>
        /// <param name="actual">Der Prüfling </param>
        /// <exception cref="PropertyEqualException">Falls eine Eigenschaft nicht gleich ist, wird diese Exception geworfen.</exception>
        /// <returns> true wenn die Objekte gleich sind, wenn nicht wird eine Exception geworfen </returns>
        public bool Equals(T expected, T actual)
        {
            // Wenn beide Objekte null sind oder die gleiche Referenz haben, dann sind sie gleich
            if (ReferenceEquals(expected, actual)) return true;

            // Wenn nur eines der beiden Objekte null ist, dann sind sie ungleich
            if (ReferenceEquals(expected, null) || ReferenceEquals(actual, null)) return false;


            foreach (var comparer in this.assertions)
            {
                comparer.Value.Equals(expected, actual);
            }

            return true;
        }

        /// <summary>
        ///   Gibt einen Hashcode für das angegebene Objekt zurück.
        /// </summary>
        /// <returns> Ein Hashcode für das angegebene Objekt. </returns>
        /// <param name="obj"> Das <see cref="T:System.Object" /> , für das ein Hashcode zurückgegeben werden soll. </param>
        /// <exception cref="T:System.ArgumentNullException">Der Typ von
        ///   <paramref name="obj" />
        ///   ist ein Verweistyp, und
        ///   <paramref name="obj" />
        ///   ist null.</exception>
        public int GetHashCode(T obj)
        {
            return obj == null ? 0 : obj.GetHashCode();
        }

        private ObjectComparer<T> Clone()
        {
            return new ObjectComparer<T>(new Dictionary<string, IEqualityComparer<T>>(assertions));
        }

        private static string GetName<TResult>(Expression<Func<T, TResult>> expression)
        {
            string s = expression.ToString();
            MemberExpression memberExpression = null;

            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression.Body as MemberExpression;
            }

            if (memberExpression == null)
            {
                return expression.ToString();
            }
            else
            {
                return ((PropertyInfo)memberExpression.Member).Name;
            }
        }
    }

    internal abstract class AssertBase<T> : IEqualityComparer<T>
    {
        private readonly string name;

        protected AssertBase(string name)
        {
            this.name = name;
        }

        public string Name { get { return name; } }

        public abstract bool Equals(T x, T y);

        public int GetHashCode(T obj)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Klasse zum Vergleichen zweier Objekte anhand des übergeben EqualityComparers
    /// Löst eine Exception aus, wenn die Objekte nicht gleich sind.
    /// </summary>
    /// <typeparam name="T">Typ der Objekte die verglichen werden</typeparam>
    internal class AssertWithComparer<T, TProperty> : AssertBase<T>
    {
        private readonly Func<T, TProperty> selector;
        private readonly IEqualityComparer<TProperty> comparer;


        public AssertWithComparer(Func<T, TProperty> selector, IEqualityComparer<TProperty> comparer, string name)
            : base(name)
        {
            this.selector = selector;
            this.comparer = comparer;

        }

        public override bool Equals(T expected, T actual)
        {
            TProperty expectedValue = selector(expected);
            TProperty actualValue = selector(actual);

            // Wenn beide Objekte null sind oder die gleiche Referenz haben, dann sind sie gleich           
            if (!(ReferenceEquals(expectedValue, actualValue)
                || expectedValue != null && comparer.Equals(expectedValue, actualValue)))
            {
                throw new PropertyEqualException(Name, expectedValue, actualValue);
            }
            return true;
        }
    }

    /// <summary>
    /// Klasse zum Vergleichen zweier Objekte anhand ihrer Equals-Methode.
    /// Löst eine Exception aus, wenn die Objekte nicht gleich sind.
    /// </summary>
    /// <typeparam name="T">Typ der Objekte die verglichen werden</typeparam>
    internal class AssertWithEquals<T> : AssertBase<T>
    {
        private readonly Func<T, object> selector;

        public AssertWithEquals(Func<T, object> selector, string name)
            : base(name)
        {
            this.selector = selector;
        }

        public override bool Equals(T expected, T actual)
        {
            object expectedValue = selector(expected);
            object actualValue = selector(actual);

            // Wenn beide Objekte null sind oder die gleiche Referenz haben, dann sind sie gleich           
            if (!(ReferenceEquals(expectedValue, actualValue)
                || expectedValue != null && expectedValue.Equals(actualValue)))
            {
                throw new PropertyEqualException(Name, expectedValue, actualValue);
            }
            return true;
        }
    }

    public class PropertyEqualException : ApplicationException
    {
        public PropertyEqualException(string property, object expected, object actual)
            : base("Difference at property " + property, new EqualException(expected, actual))
        {
        }

        public override string Message
        {
            get { return base.Message + ": " + InnerException.Message; }
        }
    }
}