﻿using UnityEngine;
using System.Collections;

namespace Chronos
{
	public interface IComponentTimeline
	{
		Component component { get; }
		void Start();
		void Update();
		void FixedUpdate();
		void AdjustProperties();
	}

	public abstract class ComponentTimeline<T> : IComponentTimeline where T : Component
	{
		protected Timeline timeline { get; private set; }
		public T component { get; protected set; }
		public bool isDirty { get; protected set; }

		Component IComponentTimeline.component
		{
			get { return component; }
		}

		public ComponentTimeline(Timeline timeline)
		{
			this.timeline = timeline;
		}

		public virtual void Start() { }
		public virtual void Update() { }
		public virtual void FixedUpdate() { }
		public virtual void CopyProperties(T source) { }
		public virtual void AdjustProperties(float timeScale) { }

		public void AdjustProperties()
		{
			AdjustProperties(timeline.timeScale);
		}

		public bool Cache(T source)
		{
			bool shouldCopy = component == null && source != null;

			component = source;

			if (shouldCopy)
			{
				CopyProperties(source);
			}

			return source != null;
		}
	}
}
