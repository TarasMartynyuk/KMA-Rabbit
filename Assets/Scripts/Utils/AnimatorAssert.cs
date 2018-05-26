using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Utils
{
    public static class AssertAnimator
    {
        public static void HasParameter(string paramName, Animator animator)
        {
            Assert.IsTrue(HasParameterHelper(paramName, animator), 
                $"The animator on a Game Object \"{animator.gameObject.name}\" does not have the parameter {paramName}");
        }

        public static void HasParameter(int paramId, Animator animator)
        {
            Assert.IsTrue(HasParameterHelper(paramId, animator), 
                $"The animator on a Game Object \"{animator.gameObject.name}\" does not have the parameter with id {paramId}");
        }

        /// <summary>
        /// pretty slow and should not be used in production code
        /// </summary>
        static bool HasParameterHelper(int paramHash, Animator animator)
        {
            return animator.parameters.Any(param => param.nameHash == paramHash);
        }

        /// <summary>
        /// pretty slow and should not be used in production code
        /// </summary>
        static bool HasParameterHelper(string paramName, Animator animator)
        {
            return animator.parameters.Any(param => param.name == paramName);
        }
    }
}
