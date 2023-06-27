using UnityEngine;

namespace TopDownRogueLike.Commons
{
    /// <summary>
    /// Provides utility methods for managing animation states of enemies and players.
    /// </summary>
    public static class AnimationStateManagement
    {
        /// <summary>
        /// Sets the running animation state for an enemy.
        /// </summary>
        /// <param name="animator">The animator component of the enemy.</param>
        /// <param name="canRunning">Whether the enemy is running or not.</param>
        public static void EnemyAnimationStateRunning(Animator animator, bool canRunning)
        {
            animator.SetBool(ConstantVariable.ENEMY_RUNNING, canRunning);
        }

        /// <summary>
        /// Sets the running animation state for a player.
        /// </summary>
        /// <param name="animator">The animator component of the player.</param>
        /// <param name="canRun">Whether the player is running or not.</param>
        public static void PlayerAnimationStateRunning(Animator animator, bool canRun)
        {
            animator.SetBool(ConstantVariable.PLAYER_RUNNING, canRun);
        }

        /// <summary>
        /// Sets the taken damage animation state for a player.
        /// </summary>
        /// <param name="animator">The animator component of the player.</param>
        /// <param name="canTakeDamage">Whether the player is taking damage or not.</param>
        public static void PlayerTakenDamage(Animator animator, bool canTakeDamage)
        {
            animator.SetBool(ConstantVariable.PLAYER_TAKEN_DAMAGE, canTakeDamage);
        }

        /// <summary>
        /// Sets the attack animation state for a player.
        /// </summary>
        /// <param name="animator">The animator component of the player.</param>
        public static void PlayerAttack(Animator animator)
        {
            animator.SetTrigger(ConstantVariable.PLAYER_ATTACK);
        }

        /// <summary>
        /// Sets the feddback animation state for a player when player got damage by enemy.
        /// </summary>
        /// <param name="animator">The animator component of the player.</param>
        public static void PlayerGotDamage(Animator animator)
        {
            animator.SetTrigger(ConstantVariable.PLAYER_HIT);
        }

        /// <summary>
        /// Sets the open animation state of the chest when triggered by the player.
        /// </summary>
        /// <param name="animator">The animator component of the player.</param>
        /// <param name="isEmpty">Specifies whether the chest is empty or not.</param>
        public static void TiggerChestOpen(Animator animator, bool isEmpty)
        {
            animator.SetTrigger(!isEmpty ? ConstantVariable.CHEST_OPEN_WITH_FULL_ITEMS : ConstantVariable.CHEST_OPEN_WITH_EMPTY_ITEMS);
        }
    }
}
