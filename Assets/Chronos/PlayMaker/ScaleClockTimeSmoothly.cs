#if CHRONOS_PLAYMAKER

using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace Chronos.PlayMaker
{
	[ActionCategory("Chronos")]
	[Tooltip("Sets the time scale on a clock smoothly over time.")]
	[HelpUrl("http://ludiq.io/chronos/documentation#Clock.LerpTimeScale")]
	public class ScaleClockTimeSmoothly : ChronosComponentAction<Clock>
	{
		[RequiredField]
		[CheckForComponent(typeof(Clock))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		public FsmFloat timeScale;

		[RequiredField]
		public FsmFloat duration;

		public bool steady;

		public override void Reset()
		{
			gameObject = null;
			timeScale = null;
			duration = null;
			steady = false;
		}

		public override void OnEnter()
		{
			DoAction();
		}

		void DoAction()
		{
			if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject))) return;

			clock.LerpTimeScale(timeScale.Value, duration.Value, steady);
		}
	}
}

#endif