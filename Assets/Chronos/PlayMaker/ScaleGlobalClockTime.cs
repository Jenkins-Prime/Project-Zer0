#if CHRONOS_PLAYMAKER

using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace Chronos.PlayMaker
{
	[ActionCategory("Chronos")]
	[Tooltip("Sets the time scale on a global clock.")]
	[HelpUrl("http://ludiq.io/chronos/documentation#Clock.localTimeScale")]
	public class ScaleGlobalClockTime : FsmStateAction
	{
		[RequiredField]
		public FsmString globalClockKey;

		[RequiredField]
		public FsmFloat timeScale;

		public bool everyFrame;

		public override void Reset()
		{
			globalClockKey = null;
			timeScale = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoAction();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoAction();
		}

		void DoAction()
		{
			Timekeeper.instance.Clock(globalClockKey.Value).localTimeScale = timeScale.Value;
		}
	}
}

#endif