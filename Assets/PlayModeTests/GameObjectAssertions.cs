using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using UnityEngine;

namespace PlayModeTests
{
    public class GameObjectAssertions : ReferenceTypeAssertions<GameObject, GameObjectAssertions>
    {
        protected override string Identifier => "id";

        public GameObjectAssertions(GameObject assertedGo) 
        {
            Subject = assertedGo;
        }

        public AndConstraint<object> BeDestroyed()
        {
            bool destroyed = Subject == null;
            Execute.Assertion.
                ForCondition(destroyed).
                FailWith($"Expected gameobject {(destroyed ? "" : $"\"{Subject.name}\"")} to be destroyed");
            return new AndConstraint<object>(this);
        }

        public AndConstraint<object> NotBeDestroyed()
        {
            bool destroyed = Subject == null;
            Execute.Assertion.
                ForCondition(! destroyed).
                FailWith($"Expected gameobject {(destroyed ? "" : $"\"{Subject.name}\"")} not to be destroyed");
            return new AndConstraint<object>(this);
        }
    }
}