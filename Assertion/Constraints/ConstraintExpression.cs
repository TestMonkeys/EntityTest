// ****************************************************************
// License Info:
// Portions Copyright � 2002-2009 Charlie Poole or 
// Copyright � 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright � 2000-2002 Philip A. Craig 
// ****************************************************************

using System;
using System.Collections;
using TestMonkey.Assertion.Constraints.Operators;

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     ConstraintExpression represents a compound constraint in the
    ///     process of being constructed from a series of syntactic elements.
    ///     Individual elements are appended to the expression as they are
    ///     reognized. Once an internalActual Constraint is appended, the expression
    ///     returns a resolvable Constraint.
    /// </summary>
    public class ConstraintExpression : ConstraintExpressionBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:ConstraintExpression" /> class.
        /// </summary>
        public ConstraintExpression()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:ConstraintExpression" />
        ///     class passing in a ConstraintBuilder, which may be pre-populated.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public ConstraintExpression(ConstraintBuilder builder)
            : base(builder)
        {
        }

        #region Not

        /// <summary>
        ///     Returns a ConstraintExpression that negates any
        ///     following constraint.
        /// </summary>
        public ConstraintExpression Not
        {
            get { return Append(new NotOperator()); }
        }

        /// <summary>
        ///     Returns a ConstraintExpression that negates any
        ///     following constraint.
        /// </summary>
        public ConstraintExpression No
        {
            get { return Append(new NotOperator()); }
        }

        #endregion

        #region All

        /// <summary>
        ///     Returns a ConstraintExpression, which will apply
        ///     the following constraint to all members of a collection,
        ///     succeeding if all of them succeed.
        /// </summary>
        public ConstraintExpression All
        {
            get { return Append(new AllOperator()); }
        }

        #endregion

        #region Some

        /// <summary>
        ///     Returns a ConstraintExpression, which will apply
        ///     the following constraint to all members of a collection,
        ///     succeeding if at least one of them succeeds.
        /// </summary>
        public ConstraintExpression Some
        {
            get { return Append(new SomeOperator()); }
        }

        #endregion

        #region None

        /// <summary>
        ///     Returns a ConstraintExpression, which will apply
        ///     the following constraint to all members of a collection,
        ///     succeeding if all of them fail.
        /// </summary>
        public ConstraintExpression None
        {
            get { return Append(new NoneOperator()); }
        }

        #endregion

        #region Exactly(n)

        /// <summary>
        ///     Returns a ConstraintExpression, which will apply
        ///     the following constraint to all members of a collection,
        ///     succeeding only if a specified number of them succeed.
        /// </summary>
        public ConstraintExpression Exactly(int expectedCount)
        {
            return Append(new ExactCountOperator(expectedCount));
        }

        #endregion

        #region Property

        /// <summary>
        ///     Returns a new PropertyConstraintExpression, which will either
        ///     test for the existence of the named property on the object
        ///     being tested or apply any following constraint to that property.
        /// </summary>
        public ResolvableConstraintExpression Property(string name)
        {
            return Append(new PropOperator(name));
        }

        #endregion

        #region Length

        /// <summary>
        ///     Returns a new ConstraintExpression, which will apply the following
        ///     constraint to the Length property of the object being tested.
        /// </summary>
        public ResolvableConstraintExpression Length
        {
            get { return Property("Length"); }
        }

        #endregion

        #region Count

        /// <summary>
        ///     Returns a new ConstraintExpression, which will apply the following
        ///     constraint to the Count property of the object being tested.
        /// </summary>
        public ResolvableConstraintExpression Count
        {
            get { return Property("Count"); }
        }

        #endregion

        #region Message

        /// <summary>
        ///     Returns a new ConstraintExpression, which will apply the following
        ///     constraint to the Message property of the object being tested.
        /// </summary>
        public ResolvableConstraintExpression Message
        {
            get { return Property("Message"); }
        }

        #endregion

        #region InnerException

        /// <summary>
        ///     Returns a new ConstraintExpression, which will apply the following
        ///     constraint to the InnerException property of the object being tested.
        /// </summary>
        public ResolvableConstraintExpression InnerException
        {
            get { return Property("InnerException"); }
        }

        #endregion

        #region Attribute

        /// <summary>
        ///     Returns a new AttributeConstraint checking for the
        ///     presence of a particular attribute on an object.
        /// </summary>
        public ResolvableConstraintExpression Attribute(Type expectedType)
        {
            return Append(new AttributeOperator(expectedType));
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Returns a new AttributeConstraint checking for the
        ///     presence of a particular attribute on an object.
        /// </summary>
        public ResolvableConstraintExpression Attribute<T>()
        {
            return Attribute(typeof (T));
        }
#endif

        #endregion

        #region With

        /// <summary>
        ///     With is currently a NOP - reserved for future use.
        /// </summary>
        public ConstraintExpression With
        {
            get { return Append(new WithOperator()); }
        }

        #endregion

        #region Matches

        /// <summary>
        ///     Returns the constraint provided as an argument - used to allow custom
        ///     custom constraints to easily participate in the syntax.
        /// </summary>
        public Constraint Matches(Constraint constraint)
        {
            return Append(constraint);
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Returns the constraint provided as an argument - used to allow custom
        ///     custom constraints to easily participate in the syntax.
        /// </summary>
        public Constraint Matches<T>(Predicate<T> predicate)
        {
            return Append(new PredicateConstraint<T>(predicate));
        }
#endif

        #endregion

        #region Null

        /// <summary>
        ///     Returns a constraint that tests for null
        /// </summary>
        public NullConstraint Null
        {
            get { return (NullConstraint) Append(new NullConstraint()); }
        }

        #endregion

        #region True

        /// <summary>
        ///     Returns a constraint that tests for True
        /// </summary>
        public TrueConstraint True
        {
            get { return (TrueConstraint) Append(new TrueConstraint()); }
        }

        #endregion

        #region False

        /// <summary>
        ///     Returns a constraint that tests for False
        /// </summary>
        public FalseConstraint False
        {
            get { return (FalseConstraint) Append(new FalseConstraint()); }
        }

        #endregion

        #region Positive

        /// <summary>
        ///     Returns a constraint that tests for a positive value
        /// </summary>
        public GreaterThanConstraint Positive
        {
            get { return (GreaterThanConstraint) Append(new GreaterThanConstraint(0)); }
        }

        #endregion

        #region Negative

        /// <summary>
        ///     Returns a constraint that tests for a negative value
        /// </summary>
        public LessThanConstraint Negative
        {
            get { return (LessThanConstraint) Append(new LessThanConstraint(0)); }
        }

        #endregion

        #region NaN

        /// <summary>
        ///     Returns a constraint that tests for NaN
        /// </summary>
        public NaNConstraint NaN
        {
            get { return (NaNConstraint) Append(new NaNConstraint()); }
        }

        #endregion

        #region Empty

        /// <summary>
        ///     Returns a constraint that tests for empty
        /// </summary>
        public EmptyConstraint Empty
        {
            get { return (EmptyConstraint) Append(new EmptyConstraint()); }
        }

        #endregion

        #region Unique

        /// <summary>
        ///     Returns a constraint that tests whether a collection
        ///     contains all unique items.
        /// </summary>
        public UniqueItemsConstraint Unique
        {
            get { return (UniqueItemsConstraint) Append(new UniqueItemsConstraint()); }
        }

        #endregion

        #region BinarySerializable

        /// <summary>
        ///     Returns a constraint that tests whether an object graph is serializable in binary format.
        /// </summary>
        public BinarySerializableConstraint BinarySerializable
        {
            get { return (BinarySerializableConstraint) Append(new BinarySerializableConstraint()); }
        }

        #endregion

        #region XmlSerializable

        /// <summary>
        ///     Returns a constraint that tests whether an object graph is serializable in xml format.
        /// </summary>
        public XmlSerializableConstraint XmlSerializable
        {
            get { return (XmlSerializableConstraint) Append(new XmlSerializableConstraint()); }
        }

        #endregion

        #region EqualTo

        /// <summary>
        ///     Returns a constraint that tests two items for equality
        /// </summary>
        public EqualConstraint EqualTo(object expected)
        {
            return (EqualConstraint) Append(new EqualConstraint(expected));
        }

        #endregion

        #region SameAs

        /// <summary>
        ///     Returns a constraint that tests that two references are the same object
        /// </summary>
        public SameAsConstraint SameAs(object expected)
        {
            return (SameAsConstraint) Append(new SameAsConstraint(expected));
        }

        #endregion

        #region GreaterThan

        /// <summary>
        ///     Returns a constraint that tests whether the
        ///     internalActual value is greater than the suppled argument
        /// </summary>
        public GreaterThanConstraint GreaterThan(object expected)
        {
            return (GreaterThanConstraint) Append(new GreaterThanConstraint(expected));
        }

        #endregion

        #region GreaterThanOrEqualTo

        /// <summary>
        ///     Returns a constraint that tests whether the
        ///     internalActual value is greater than or equal to the suppled argument
        /// </summary>
        public GreaterThanOrEqualConstraint GreaterThanOrEqualTo(object expected)
        {
            return (GreaterThanOrEqualConstraint) Append(new GreaterThanOrEqualConstraint(expected));
        }

        /// <summary>
        ///     Returns a constraint that tests whether the
        ///     internalActual value is greater than or equal to the suppled argument
        /// </summary>
        public GreaterThanOrEqualConstraint AtLeast(object expected)
        {
            return (GreaterThanOrEqualConstraint) Append(new GreaterThanOrEqualConstraint(expected));
        }

        #endregion

        #region LessThan

        /// <summary>
        ///     Returns a constraint that tests whether the
        ///     internalActual value is less than the suppled argument
        /// </summary>
        public LessThanConstraint LessThan(object expected)
        {
            return (LessThanConstraint) Append(new LessThanConstraint(expected));
        }

        #endregion

        #region LessThanOrEqualTo

        /// <summary>
        ///     Returns a constraint that tests whether the
        ///     internalActual value is less than or equal to the suppled argument
        /// </summary>
        public LessThanOrEqualConstraint LessThanOrEqualTo(object expected)
        {
            return (LessThanOrEqualConstraint) Append(new LessThanOrEqualConstraint(expected));
        }

        /// <summary>
        ///     Returns a constraint that tests whether the
        ///     internalActual value is less than or equal to the suppled argument
        /// </summary>
        public LessThanOrEqualConstraint AtMost(object expected)
        {
            return (LessThanOrEqualConstraint) Append(new LessThanOrEqualConstraint(expected));
        }

        #endregion

        #region TypeOf

        /// <summary>
        ///     Returns a constraint that tests whether the internalActual
        ///     value is of the exact type supplied as an argument.
        /// </summary>
        public ExactTypeConstraint TypeOf(Type expectedType)
        {
            return (ExactTypeConstraint) Append(new ExactTypeConstraint(expectedType));
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Returns a constraint that tests whether the internalActual
        ///     value is of the exact type supplied as an argument.
        /// </summary>
        public ExactTypeConstraint TypeOf<T>()
        {
            return (ExactTypeConstraint) Append(new ExactTypeConstraint(typeof (T)));
        }
#endif

        #endregion

        #region InstanceOf

        /// <summary>
        ///     Returns a constraint that tests whether the internalActual value
        ///     is of the type supplied as an argument or a derived type.
        /// </summary>
        public InstanceOfTypeConstraint InstanceOf(Type expectedType)
        {
            return (InstanceOfTypeConstraint) Append(new InstanceOfTypeConstraint(expectedType));
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Returns a constraint that tests whether the internalActual value
        ///     is of the type supplied as an argument or a derived type.
        /// </summary>
        public InstanceOfTypeConstraint InstanceOf<T>()
        {
            return (InstanceOfTypeConstraint) Append(new InstanceOfTypeConstraint(typeof (T)));
        }
#endif

        /// <summary>
        ///     Returns a constraint that tests whether the internalActual value
        ///     is of the type supplied as an argument or a derived type.
        /// </summary>
        [Obsolete("Use InstanceOf(expectedType)")]
        public InstanceOfTypeConstraint InstanceOfType(Type expectedType)
        {
            return (InstanceOfTypeConstraint) Append(new InstanceOfTypeConstraint(expectedType));
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Returns a constraint that tests whether the internalActual value
        ///     is of the type supplied as an argument or a derived type.
        /// </summary>
        [Obsolete("Use InstanceOf<T>()")]
        public InstanceOfTypeConstraint InstanceOfType<T>()
        {
            return (InstanceOfTypeConstraint) Append(new InstanceOfTypeConstraint(typeof (T)));
        }
#endif

        #endregion

        #region AssignableFrom

        /// <summary>
        ///     Returns a constraint that tests whether the internalActual value
        ///     is assignable from the type supplied as an argument.
        /// </summary>
        public AssignableFromConstraint AssignableFrom(Type expectedType)
        {
            return (AssignableFromConstraint) Append(new AssignableFromConstraint(expectedType));
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Returns a constraint that tests whether the internalActual value
        ///     is assignable from the type supplied as an argument.
        /// </summary>
        public AssignableFromConstraint AssignableFrom<T>()
        {
            return (AssignableFromConstraint) Append(new AssignableFromConstraint(typeof (T)));
        }
#endif

        #endregion

        #region AssignableTo

        /// <summary>
        ///     Returns a constraint that tests whether the internalActual value
        ///     is assignable from the type supplied as an argument.
        /// </summary>
        public AssignableToConstraint AssignableTo(Type expectedType)
        {
            return (AssignableToConstraint) Append(new AssignableToConstraint(expectedType));
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Returns a constraint that tests whether the internalActual value
        ///     is assignable from the type supplied as an argument.
        /// </summary>
        public AssignableToConstraint AssignableTo<T>()
        {
            return (AssignableToConstraint) Append(new AssignableToConstraint(typeof (T)));
        }
#endif

        #endregion

        #region EquivalentTo

        /// <summary>
        ///     Returns a constraint that tests whether the internalActual value
        ///     is a collection containing the same elements as the
        ///     collection supplied as an argument.
        /// </summary>
        public CollectionEquivalentConstraint EquivalentTo(IEnumerable expected)
        {
            return (CollectionEquivalentConstraint) Append(new CollectionEquivalentConstraint(expected));
        }

        #endregion

        #region SubsetOf

        /// <summary>
        ///     Returns a constraint that tests whether the internalActual value
        ///     is a subset of the collection supplied as an argument.
        /// </summary>
        public CollectionSubsetConstraint SubsetOf(IEnumerable expected)
        {
            return (CollectionSubsetConstraint) Append(new CollectionSubsetConstraint(expected));
        }

        #endregion

        #region Ordered

        /// <summary>
        ///     Returns a constraint that tests whether a collection is ordered
        /// </summary>
        public CollectionOrderedConstraint Ordered
        {
            get { return (CollectionOrderedConstraint) Append(new CollectionOrderedConstraint()); }
        }

        #endregion

        #region Member

        /// <summary>
        ///     Returns a new CollectionContainsConstraint checking for the
        ///     presence of a particular object in the collection.
        /// </summary>
        public CollectionContainsConstraint Member(object expected)
        {
            return (CollectionContainsConstraint) Append(new CollectionContainsConstraint(expected));
        }

        /// <summary>
        ///     Returns a new CollectionContainsConstraint checking for the
        ///     presence of a particular object in the collection.
        /// </summary>
        public CollectionContainsConstraint Contains(object expected)
        {
            return (CollectionContainsConstraint) Append(new CollectionContainsConstraint(expected));
        }

        #endregion

        #region Contains

        /// <summary>
        ///     Returns a new ContainsConstraint. This constraint
        ///     will, in turn, make use of the appropriate second-level
        ///     constraint, depending on the type of the internalActual argument.
        ///     This overload is only used if the item sought is a string,
        ///     since any other type implies that we are looking for a
        ///     collection member.
        /// </summary>
        public ContainsConstraint Contains(string expected)
        {
            return (ContainsConstraint) Append(new ContainsConstraint(expected));
        }

        #endregion

        #region StringContaining

        /// <summary>
        ///     Returns a constraint that succeeds if the internalActual
        ///     value contains the substring supplied as an argument.
        /// </summary>
        public SubstringConstraint StringContaining(string expected)
        {
            return (SubstringConstraint) Append(new SubstringConstraint(expected));
        }

        /// <summary>
        ///     Returns a constraint that succeeds if the internalActual
        ///     value contains the substring supplied as an argument.
        /// </summary>
        public SubstringConstraint ContainsSubstring(string expected)
        {
            return (SubstringConstraint) Append(new SubstringConstraint(expected));
        }

        #endregion

        #region StartsWith

        /// <summary>
        ///     Returns a constraint that succeeds if the internalActual
        ///     value starts with the substring supplied as an argument.
        /// </summary>
        public StartsWithConstraint StartsWith(string expected)
        {
            return (StartsWithConstraint) Append(new StartsWithConstraint(expected));
        }

        /// <summary>
        ///     Returns a constraint that succeeds if the internalActual
        ///     value starts with the substring supplied as an argument.
        /// </summary>
        public StartsWithConstraint StringStarting(string expected)
        {
            return (StartsWithConstraint) Append(new StartsWithConstraint(expected));
        }

        #endregion

        #region EndsWith

        /// <summary>
        ///     Returns a constraint that succeeds if the internalActual
        ///     value ends with the substring supplied as an argument.
        /// </summary>
        public EndsWithConstraint EndsWith(string expected)
        {
            return (EndsWithConstraint) Append(new EndsWithConstraint(expected));
        }

        /// <summary>
        ///     Returns a constraint that succeeds if the internalActual
        ///     value ends with the substring supplied as an argument.
        /// </summary>
        public EndsWithConstraint StringEnding(string expected)
        {
            return (EndsWithConstraint) Append(new EndsWithConstraint(expected));
        }

        #endregion

        #region Matches

        /// <summary>
        ///     Returns a constraint that succeeds if the internalActual
        ///     value matches the regular expression supplied as an argument.
        /// </summary>
        public RegexConstraint Matches(string pattern)
        {
            return (RegexConstraint) Append(new RegexConstraint(pattern));
        }

        /// <summary>
        ///     Returns a constraint that succeeds if the internalActual
        ///     value matches the regular expression supplied as an argument.
        /// </summary>
        public RegexConstraint StringMatching(string pattern)
        {
            return (RegexConstraint) Append(new RegexConstraint(pattern));
        }

        #endregion

        #region SamePath

        /// <summary>
        ///     Returns a constraint that tests whether the path provided
        ///     is the same as an expected path after canonicalization.
        /// </summary>
        public SamePathConstraint SamePath(string expected)
        {
            return (SamePathConstraint) Append(new SamePathConstraint(expected));
        }

        #endregion

        #region SubPath

        /// <summary>
        ///     Returns a constraint that tests whether the path provided
        ///     is the same path or under an expected path after canonicalization.
        /// </summary>
        public SubPathConstraint SubPath(string expected)
        {
            return (SubPathConstraint) Append(new SubPathConstraint(expected));
        }

        #endregion

        #region SamePathOrUnder

        /// <summary>
        ///     Returns a constraint that tests whether the path provided
        ///     is the same path or under an expected path after canonicalization.
        /// </summary>
        public SamePathOrUnderConstraint SamePathOrUnder(string expected)
        {
            return (SamePathOrUnderConstraint) Append(new SamePathOrUnderConstraint(expected));
        }

        #endregion

        #region InRange

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Returns a constraint that tests whether the internalActual value falls
        ///     within a specified range.
        /// </summary>
        public RangeConstraint<T> InRange<T>(T from, T to) where T : IComparable<T>
        {
            return (RangeConstraint<T>) Append(new RangeConstraint<T>(from, to));
        }
#else
    /// <summary>
    /// Returns a constraint that tests whether the internalActual value falls 
    /// within a specified range.
    /// </summary>
        public RangeConstraint InRange(IComparable from, IComparable to)
        {
            return (RangeConstraint)this.Append(new RangeConstraint(from, to));
        }
#endif

        #endregion
    }
}