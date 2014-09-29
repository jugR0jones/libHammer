using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace libHammer.Assertions
{

    /// <summary>
    /// Allows for a wider range of assertion testing and improved readability in the code syntax.
    /// </summary>
    /// <author>Artur Mustafin</author>
    /// <see cref="http://codereview.stackexchange.com/users/7207/artur-mustafin"/>
    public static class AssertionContracts
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        private static class Exceptions<U> where U : Exception, new()
        {
            public static T ThrowOnTrue<T>(T obj, Func<T, bool> function, params object[] args)
            {
                if (function(obj) == true)
                {
                    Throw(obj, args);
                }
                return obj;
            }
            public static T ThrowOnFalse<T>(T obj, Func<T, bool> function, params object[] args)
            {
                if (function(obj) == false)
                {
                    Throw(obj, args);
                }
                return obj;
            }
            public static void Throw<T>(T obj, params object[] args)
            {
                throw CreateException(obj, args);
            }
            private static U CreateException<T>(T obj, params object[] args)
            {
                return (U)Activator.CreateInstance(typeof(U), args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class ContractException : Exception
        {
            public ContractException() : base() { }
            public ContractException(string message) : base(message) { }
            protected ContractException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public ContractException(string message, Exception innerException) : base(message, innerException) { }
        }

        public static T ThrowOnTrue<T, U>(this T obj, Func<T, bool> function, params object[] args) where U : Exception, new()
        {
            return AssertionContracts.Exceptions<ContractException>.ThrowOnTrue(obj, function);
        }
        public static T ThrowOnFalse<T, U>(this T obj, Func<T, bool> function, params object[] args) where U : Exception, new()
        {
            return AssertionContracts.Exceptions<ContractException>.ThrowOnFalse(obj, function);
        }
        public static void Throw<T, U>(this T obj, Func<T, bool> function, U ex) where U : Exception
        {
            AssertionContracts.Exceptions<ContractException>.Throw(obj, function);
        }
        public static T NoThrowContractException<T>(this T obj, Func<T, bool> function)
        {
            return AssertionContracts.Exceptions<ContractException>.ThrowOnTrue(obj, function);
        }
        public static T ThrowContractException<T>(this T obj, Func<T, bool> function)
        {
            return AssertionContracts.Exceptions<ContractException>.ThrowOnFalse(obj, function);
        }
        public static T AssertTrue<T>(this T obj, Func<T, bool> function)
        {
            Trace.Assert(function(obj) == true);
            return obj;
        }
        public static T AssertFalse<T>(this T obj, Func<T, bool> function)
        {
            Trace.Assert(function(obj) == false);
            return obj;
        }
        public static T AssertIsNullOrEmpty<T>(this T obj, Func<T, string> function)
        {
            Trace.Assert(string.IsNullOrEmpty(function(obj)));
            return obj;
        }
        public static T AssertIsNullOrWhitespace<T>(this T obj, Func<T, string> function)
        {
            Trace.Assert(string.IsNullOrWhiteSpace(function(obj)));
            return obj;
        }
        public static T AssertIsNotNullOrEmpty<T>(this T obj, Func<T, string> function)
        {
            Trace.Assert(!string.IsNullOrEmpty(function(obj)));
            return obj;
        }
        public static T AssertIsNotNullOrWhitespace<T>(this T obj, Func<T, string> function)
        {
            Trace.Assert(!string.IsNullOrWhiteSpace(function(obj)));
            return obj;
        }
        public static T AssertEquals<T, U>(this T obj, Func<T, U> function, U value)
        {
            Trace.Assert(object.Equals(function(obj), value));
            return obj;
        }
        public static T AssertNotEquals<T, U>(this T obj, Func<T, U> function, U value)
        {
            Trace.Assert(!object.Equals(function(obj), value));
            return obj;
        }
        public static T AssertDefault<T, U>(this T obj, Func<T, U> function)
        {
            Trace.Assert(object.Equals(function(obj), default(U)));
            return obj;
        }
        public static T AssertNonDefault<T, U>(this T obj, Func<T, U> function)
        {
            Trace.Assert(!object.Equals(function(obj), default(U)));
            return obj;
        }
        public static T AssertNotNull<T, U>(this T obj, Func<T, U> function)
        {
            Trace.Assert(!object.Equals(function(obj), default(U)));
            return obj;
        }
        public static T AssertNotNull<T>(this T obj)
        {
            Trace.Assert(!object.Equals(obj, default(T)));
            return obj;
        }
        public static T AssertNull<T, U>(this T obj, Func<T, U> function)
        {
            Trace.Assert(object.Equals(function(obj), default(U)));
            return obj;
        }
        public static T AssertNull<T>(this T obj)
        {
            Trace.Assert(object.Equals(obj, default(T)));
            return obj;
        }
        public static T AssertPositive<T>(this T obj, Func<T, int> function)
        {
            Trace.Assert(function(obj) > 0);
            return obj;
        }
        public static T AssertPositive<T>(this T obj, Func<T, long> function)
        {
            Trace.Assert(function(obj) > 0);
            return obj;
        }
        public static T AssertNegative<T>(this T obj, Func<T, int> function)
        {
            Trace.Assert(function(obj) < 0);
            return obj;
        }
        public static T AssertNegative<T>(this T obj, Func<T, long> function)
        {
            Trace.Assert(function(obj) < 0);
            return obj;
        }
        public static T AssertEqualsZero<T>(this T obj, Func<T, int> function)
        {
            Trace.Assert(function(obj) == 0);
            return obj;
        }
        public static T AssertEqualsZero<T>(this T obj, Func<T, long> function)
        {
            Trace.Assert(function(obj) == 0);
            return obj;
        }
        public static T AssertNotEqualsZero<T>(this T obj, Func<T, int> function)
        {
            Trace.Assert(function(obj) != 0);
            return obj;
        }
        public static T AssertNotEqualsZero<T>(this T obj, Func<T, long> function)
        {
            Trace.Assert(function(obj) != 0);
            return obj;
        }
        public static T AssertGreaterThan<T>(this T obj, Func<T, int> function, int value)
        {
            Trace.Assert(function(obj) > value);
            return obj;
        }
        public static T AssertGreaterThan<T>(this T obj, Func<T, long> function, long value)
        {
            Trace.Assert(function(obj) > value);
            return obj;
        }
        public static T AssertLessThan<T>(this T obj, Func<T, int> function, int value)
        {
            Trace.Assert(function(obj) < value);
            return obj;
        }
        public static T AssertLessThan<T>(this T obj, Func<T, long> function, long value)
        {
            Trace.Assert(function(obj) < value);
            return obj;
        }
        public static T AssertGreaterOrEqualsThan<T>(this T obj, Func<T, int> function, int value)
        {
            Trace.Assert(function(obj) >= value);
            return obj;
        }
        public static T AssertGreaterOrEqualsThan<T>(this T obj, Func<T, long> function, long value)
        {
            Trace.Assert(function(obj) >= value);
            return obj;
        }
        public static T AssertLessOrEqualsThan<T>(this T obj, Func<T, int> function, int value)
        {
            Trace.Assert(function(obj) <= value);
            return obj;
        }
        public static T AssertLessOrEqualsThan<T>(this T obj, Func<T, long> function, long value)
        {
            Trace.Assert(function(obj) <= value);
            return obj;
        }
        public static T AssertGreaterOrEqualsZero<T>(this T obj, Func<T, int> function)
        {
            Trace.Assert(function(obj) >= 0);
            return obj;
        }
        public static T AssertGreaterOrEqualsZero<T>(this T obj, Func<T, long> function, long value)
        {
            Trace.Assert(function(obj) >= 0);
            return obj;
        }
        public static T AssertLessOrEqualsZero<T>(this T obj, Func<T, int> function)
        {
            Trace.Assert(function(obj) <= 0);
            return obj;
        }
        public static T AssertLessOrEqualsZero<T>(this T obj, Func<T, long> function)
        {
            Trace.Assert(function(obj) <= 0);
            return obj;
        }
        public static T RequireTrue<T>(this T obj, Func<T, bool> function)
        {
            return obj.ThrowContractException((o) => function(obj) == true);
        }
        public static T RequireFalse<T>(this T obj, Func<T, bool> function)
        {
            return obj.ThrowContractException((o) => function(obj) == false);
        }
        public static T RequireIsNullOrEmpty<T>(this T obj, Func<T, string> function)
        {
            return obj.ThrowContractException((o) => string.IsNullOrEmpty(function(obj)));
        }
        public static T RequireIsNullOrWhitespace<T>(this T obj, Func<T, string> function)
        {
            return obj.ThrowContractException((o) => string.IsNullOrWhiteSpace(function(obj)));
        }
        public static T RequireIsNotNullOrEmpty<T>(this T obj, Func<T, string> function)
        {
            return obj.ThrowContractException((o) => !string.IsNullOrEmpty(function(obj)));
        }
        public static T RequireIsNotNullOrWhitespace<T>(this T obj, Func<T, string> function)
        {
            return obj.ThrowContractException((o) => !string.IsNullOrWhiteSpace(function(obj)));
        }
        public static T RequireEquals<T, U>(this T obj, Func<T, U> function, U value)
        {
            return obj.ThrowContractException((o) => object.Equals(function(obj), value));
        }
        public static T RequireEquals<T, U>(this T obj, T value)
        {
            return obj.ThrowContractException((o) => object.Equals(obj, value));
        }
        public static T RequireNotEquals<T, U>(this T obj, Func<T, U> function, U value)
        {
            return obj.ThrowContractException((o) => !object.Equals(function(obj), value));
        }
        public static T RequireNotEquals<T, U>(this T obj, T value)
        {
            return obj.ThrowContractException((o) => !object.Equals(obj, value));
        }
        public static T RequireDefault<T, U>(this T obj, Func<T, U> function)
        {
            return obj.ThrowContractException((o) => object.Equals(function(obj), default(U)));
        }
        public static T RequireDefault<T>(this T obj)
        {
            return obj.ThrowContractException((o) => object.Equals(obj, default(T)));
        }
        public static T RequireNonDefault<T, U>(this T obj, Func<T, U> function)
        {
            return obj.ThrowContractException((o) => !object.Equals(function(obj), default(U)));
        }
        public static T RequireNonDefault<T, U>(this T obj)
        {
            return obj.ThrowContractException((o) => !object.Equals(obj, default(T)));
        }
        public static T RequireNotNull<T, U>(this T obj, Func<T, U> function)
        {
            return obj.ThrowContractException((o) => !object.Equals(function(obj), default(U)));
        }
        public static T RequireNotNull<T>(this T obj)
        {
            return obj.ThrowContractException((o) => !object.Equals(obj, default(T)));
        }
        public static T RequireNull<T, U>(this T obj, Func<T, U> function)
        {
            return obj.ThrowContractException((o) => object.Equals(function(obj), default(U)));
        }
        public static T RequireNull<T>(this T obj)
        {
            return obj.ThrowContractException((o) => object.Equals(obj, default(T)));
        }
        public static T RequirePositive<T>(this T obj, Func<T, int> function)
        {
            return obj.ThrowContractException((o) => function(obj) > 0);
        }
        public static T RequirePositive<T>(this T obj, Func<T, long> function)
        {
            return obj.ThrowContractException((o) => function(obj) > 0);
        }
        public static T RequireNegative<T>(this T obj, Func<T, int> function)
        {
            return obj.ThrowContractException((o) => function(obj) < 0);
        }
        public static T RequireNegative<T>(this T obj, Func<T, long> function)
        {
            return obj.ThrowContractException((o) => function(obj) < 0);
        }
        public static T RequireEqualsZero<T>(this T obj, Func<T, int> function)
        {
            return obj.ThrowContractException((o) => function(obj) == 0);
        }
        public static T RequireEqualsZero<T>(this T obj, Func<T, long> function)
        {
            return obj.ThrowContractException((o) => function(obj) == 0);
        }
        public static T RequireNotEqualsZero<T>(this T obj, Func<T, int> function)
        {
            return obj.ThrowContractException((o) => function(obj) != 0);
        }
        public static T RequireNotEqualsZero<T>(this T obj, Func<T, long> function)
        {
            return obj.ThrowContractException((o) => function(obj) != 0);
        }
        public static T RequireGreaterThan<T>(this T obj, Func<T, int> function, int value)
        {
            return obj.ThrowContractException((o) => function(obj) > value);
        }
        public static T RequireGreaterThan<T>(this T obj, Func<T, long> function, long value)
        {
            return obj.ThrowContractException((o) => function(obj) > value);
        }
        public static T RequireLessThan<T>(this T obj, Func<T, int> function, int value)
        {
            return obj.ThrowContractException((o) => function(obj) < value);
        }
        public static T RequireLessThan<T>(this T obj, Func<T, long> function, long value)
        {
            return obj.ThrowContractException((o) => function(obj) < value);
        }
        public static T RequireGreaterOrEqualsThan<T>(this T obj, Func<T, int> function, int value)
        {
            return obj.ThrowContractException((o) => function(obj) >= value);
        }
        public static T RequireGreaterOrEqualsThan<T>(this T obj, Func<T, long> function, long value)
        {
            return obj.ThrowContractException((o) => function(obj) >= value);
        }
        public static T RequireLessOrEqualsThan<T>(this T obj, Func<T, int> function, int value)
        {
            return obj.ThrowContractException((o) => function(obj) <= value);
        }
        public static T RequireLessOrEqualsThan<T>(this T obj, Func<T, long> function, long value)
        {
            return obj.ThrowContractException((o) => function(obj) <= value);
        }
        public static T RequireGreaterOrEqualsZero<T>(this T obj, Func<T, int> function)
        {
            return obj.ThrowContractException((o) => function(obj) >= 0);
        }
        public static T RequireGreaterOrEqualsZero<T>(this T obj, Func<T, long> function, long value)
        {
            return obj.ThrowContractException((o) => function(obj) >= 0);
        }
        public static T RequireLessOrEqualsZero<T>(this T obj, Func<T, int> function)
        {
            return obj.ThrowContractException((o) => function(obj) <= 0);
        }
        public static T RequireLessOrEqualsZero<T>(this T obj, Func<T, long> function)
        {
            return obj.ThrowContractException((o) => function(obj) <= 0);
        }
        public static void ThrowOnFalse<U>(Func<bool> function, params object[] args) where U : Exception, new()
        {
            if (function() == false)
            {
                throw (U)Activator.CreateInstance(typeof(U), args);
            }
        }
        public static void ThrowOnTrue<U>(Func<bool> function, params object[] args) where U : Exception, new()
        {
            if (function() == true)
            {
                throw (U)Activator.CreateInstance(typeof(U), args);
            }
        }
        public static void Throw<U>(Func<bool> function, U ex) where U : Exception
        {
            if (function() == false)
            {
                throw ex;
            }
        }
        public static void AssertTrue(Func<bool> function)
        {
            Trace.Assert(function() == true);
        }
        public static void AssertFalse(Func<bool> function)
        {
            Trace.Assert(function() == false);
        }
        public static void AssertIsNullOrEmpty(Func<string> function)
        {
            Trace.Assert(string.IsNullOrEmpty(function()));
        }
        public static void AssertIsNullOrWhitespace(Func<string> function)
        {
            Trace.Assert(string.IsNullOrWhiteSpace(function()));
        }
        public static void AssertIsNotNullOrEmpty(Func<string> function)
        {
            Trace.Assert(!string.IsNullOrEmpty(function()));
        }
        public static void AssertIsNotNullOrWhitespace(Func<string> function)
        {
            Trace.Assert(!string.IsNullOrWhiteSpace(function()));
        }
        public static void AssertEquals<U>(Func<U> function, U value)
        {
            Trace.Assert(object.Equals(function(), value));
        }
        public static void AssertNotEquals<U>(Func<U> function, U value)
        {
            Trace.Assert(!object.Equals(function(), value));
        }
        public static void AssertDefault<U>(Func<U> function)
        {
            Trace.Assert(object.Equals(function(), default(U)));
        }
        public static void AssertNonDefault<U>(Func<U> function)
        {
            Trace.Assert(!object.Equals(function(), default(U)));
        }
        public static void AssertNotNull<U>(Func<U> function)
        {
            Trace.Assert(!object.Equals(function(), default(U)));
        }
        public static void AssertNull<U>(Func<U> function)
        {
            Trace.Assert(object.Equals(function(), default(U)));
        }
        public static void AssertPositive(Func<int> function)
        {
            Trace.Assert(function() > 0);
        }
        public static void AssertPositive(Func<long> function)
        {
            Trace.Assert(function() > 0);
        }
        public static void AssertNegative(Func<int> function)
        {
            Trace.Assert(function() < 0);
        }
        public static void AssertNegative(Func<long> function)
        {
            Trace.Assert(function() < 0);
        }
        public static void AssertEqualsZero(Func<int> function)
        {
            Trace.Assert(function() == 0);
        }
        public static void AssertEqualsZero(Func<long> function)
        {
            Trace.Assert(function() == 0);
        }
        public static void AssertNotEqualsZero(Func<int> function)
        {
            Trace.Assert(function() != 0);
        }
        public static void AssertNotEqualsZero(Func<long> function)
        {
            Trace.Assert(function() != 0);
        }
        public static void AssertGreaterThan(Func<int> function, int value)
        {
            Trace.Assert(function() > value);
        }
        public static void AssertGreaterThan(Func<long> function, long value)
        {
            Trace.Assert(function() > value);
        }
        public static void AssertLessThan(Func<int> function, int value)
        {
            Trace.Assert(function() < value);
        }
        public static void AssertLessThan(Func<long> function, long value)
        {
            Trace.Assert(function() < value);
        }
        public static void AssertGreaterOrEqualsThan(Func<int> function, int value)
        {
            Trace.Assert(function() >= value);
        }
        public static void AssertGreaterOrEqualsThan(Func<long> function, long value)
        {
            Trace.Assert(function() >= value);
        }
        public static void AssertLessOrEqualsThan(Func<int> function, int value)
        {
            Trace.Assert(function() <= value);
        }
        public static void AssertLessOrEqualsThan(Func<long> function, long value)
        {
            Trace.Assert(function() <= value);
        }
        public static void AssertGreaterOrEqualsZero(Func<int> function)
        {
            Trace.Assert(function() >= 0);
        }
        public static void AssertGreaterOrEqualsZero(Func<long> function, long value)
        {
            Trace.Assert(function() >= 0);
        }
        public static void AssertLessOrEqualsZero<T>(Func<int> function)
        {
            Trace.Assert(function() <= 0);
        }
        public static void AssertLessOrEqualsZero(Func<long> function)
        {
            Trace.Assert(function() <= 0);
        }
    }

}
