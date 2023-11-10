using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gianni.Helper
{
	public enum TimeScale { unscaledDeltaTime, deltaTime }
	public static class CoRoutineHelper
	{

		public static Coroutine invokeCoRoutine(this MonoBehaviour Mono, IEnumerator Coroutine)
		{
			return Mono.StartCoroutine(Coroutine);
		}
		public static Coroutine invokeCoRoutine(this MonoBehaviour Mono, Func<IEnumerator> Coroutine)
		{
			return Mono.StartCoroutine(Coroutine());
		}
		public static Coroutine InvokeWait(this MonoBehaviour Mono, float time, Action AfterWait)
		{
			return Mono.StartCoroutine(waitFor(time, null, AfterWait, false));
		}
		public static Coroutine InvokeWait(this MonoBehaviour Mono, CustomYieldInstruction waitInstruction, Action AfterWait)
		{
			return Mono.StartCoroutine(waitForCustemYieldConstruction(waitInstruction, AfterWait));
		}
		public static Coroutine InvokeWait(this MonoBehaviour Mono, YieldInstruction waitInstruction, Action AfterWait)
		{
			return Mono.StartCoroutine(waitFor(waitInstruction, AfterWait, false));
		}
		public static Coroutine InvokeWaitCoRoAndTime(this MonoBehaviour Mono, Coroutine Coro, float time, Action AfterWait)
		{
			return Mono.StartCoroutine(waitFor(time, Coro, AfterWait, false));
		}
		public static Coroutine InvokeWaitLoop(this MonoBehaviour Mono, float time, Action AfterWait)
		{
			return Mono.StartCoroutine(waitFor(time, null, AfterWait, true));
		}
		public static Coroutine InvokeWaitLoop(this MonoBehaviour Mono, YieldInstruction waitInstruction, Action AfterWait)
		{
			return Mono.StartCoroutine(waitFor(waitInstruction, AfterWait, true));
		}
		public static Coroutine InvokeWaitLoopUntil(this MonoBehaviour Mono, float waitTime, Action thanDo, Func<bool> until, Action LastDo)
		{
			return Mono.StartCoroutine(waitLoopUntil(waitTime, thanDo, until, LastDo));
		}
		public static Coroutine InvokeWaitofThanDo(this MonoBehaviour Mono, Func<bool> Waitof, UnityAction thanDo)
		{
			return Mono.StartCoroutine(waitOf(Waitof, thanDo, false));
		}
		public static Coroutine InvokeDoAsLongAsTimer(this MonoBehaviour Mono, Action doUntilEndOfDuration, float duration)
		{
			return Mono.StartCoroutine(doActionCountdown(doUntilEndOfDuration, duration));
		}
		/// <summary>
		/// fürht eine Reihe von Coroutinen aus, nach einer Bedingung die auch null sein darf
		/// </summary>
		/// <param name="Mono"></param>
		/// <param name="Waitof">Bedinung, falls nicht notwendig dann null</param>
		/// <param name="Dos">Gestartete Corutinen</param>
		/// <returns></returns>
		public static Coroutine InvokeWaitandDos(this MonoBehaviour Mono, Func<bool> Waitof, params Func<Coroutine>[] Dos)
		{
			return Mono.StartCoroutine(WaitandDoos(Waitof, Dos));
		}

		public static Coroutine InvokeWaitofThanDoLoop(this MonoBehaviour Mono, Func<bool> Waitof, UnityAction thanDo)
		{
			return Mono.StartCoroutine(waitOf(Waitof, thanDo, true));
		}
		/// <summary>
		/// Eigent sich nicht wenn Euler Angel über null gehen
		/// </summary>
		/// <param name="Mono"></param>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <param name="duration"></param>
		/// <param name="worldPositionStay"></param>
		/// <returns></returns>
		public static Coroutine InvokeDrehungLerp(this MonoBehaviour Mono, Transform source, Quaternion target, float duration, Space worldPositionStay = Space.World)
		{
			return Mono.StartCoroutine(worldPositionStay == Space.World ? DrehObject(source, target, duration) : DrehObjectLocal(source, target, duration));
		}
		public static Coroutine InvokeDrehungLerp(this MonoBehaviour Mono, Transform source, float Angeltarget, float duration)
		{
			return Mono.StartCoroutine(DrehObject(source, Angeltarget, duration));
		}
		public static Coroutine InvokeMoveLerp(this MonoBehaviour Mono, Transform source, Vector3 target, float overTime, Space worldPositionStay = Space.World)
		{
			return Mono.StartCoroutine(worldPositionStay == Space.World ? MoveObject(source, target, overTime) : MoveObjectLocal(source, target, overTime));
		}
		public static Coroutine InvokeFollowLerp(this MonoBehaviour Mono, Vector3 Source, Transform target, float duration, Action OnDone)
		{
			return Mono.StartCoroutine(FollowObject(Mono.transform, Source, target, duration, OnDone));
		}
		public static Coroutine InvokeFollowLerp(this MonoBehaviour Mono, Transform Source, Transform target, float duration, Action OnDone)
		{
			return Mono.StartCoroutine(FollowObject(Source.transform, Source.position, target, duration, OnDone));
		}
		public static Coroutine InvokeColorLerp(this MonoBehaviour Mono, SpriteRenderer start, Color EndColor, float inTime, Action CallBack)
		{
			return Mono.StartCoroutine(Colore(start, EndColor, inTime, CallBack));
		}
		public static Coroutine LerpPointToPoint(this MonoBehaviour Mono, Transform transObject, List<Vector3> Points, float speed)
		{
			return Mono.StartCoroutine(LerpPointToPointRegelmaessig(transObject, Points, speed));
		}
		public static Coroutine InvokeScaleLerp(this MonoBehaviour Mono, Transform obj, float inTime, float ScaleToAdd)
		{
			return Mono.StartCoroutine(LerpScale(obj, inTime, ScaleToAdd));
		}
		private static IEnumerator doActionCountdown(Action doUntilEndOfDuration, float duration)
		{
			var t = duration;
			while (t > 0)
			{
				doUntilEndOfDuration();
				t -= Time.deltaTime;
				yield return null;
			}
		}
		private static IEnumerator LerpScale(Transform obj, float inTime, float to)
		{
			var startTime = Time.time;
			var from = obj.localScale;
			var toV3 = new Vector3(from.x + to, from.y + to, from.z + to);
			while (Time.time < startTime + inTime)
			{
				obj.localScale = Vector3.Slerp(from, toV3, (Time.time - startTime) / inTime);
				yield return null;
			}
			obj.localScale = toV3;
		}

		static IEnumerator Colore(SpriteRenderer from, Color to, float inTime, Action callBack)
		{
			var startColor = from.color;
			var startTime = Time.time;

			while (Time.time < startTime + inTime)
			{
				try
				{
					from.color = Color.Lerp(startColor, to, (Time.time - startTime) / inTime);
				}
				catch (Exception)
				{
					yield break;
				}
				yield return null;
			}

			var temp = from.color;
			from.color = to;
			to = temp;

			callBack();
		}

		static IEnumerator DrehObject(Transform source, Quaternion target, float overTime)
		{
			var t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / overTime;
				source.rotation = Quaternion.Lerp(source.rotation, target, t);
				yield return null;
			}
			source.rotation = target;
		}
		static IEnumerator DrehObject(Transform source, float targetAngel, float duration)
		{
			var t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / duration;
				float startAngel = source.eulerAngles.z;
				float angle = Mathf.LerpAngle(startAngel, targetAngel, t);
				source.eulerAngles = new Vector3(0, 0, angle);
				yield return null;
			}
			source.eulerAngles = new Vector3(0, 0, targetAngel);
		}
		static IEnumerator DrehObjectLocal(Transform source, Quaternion target, float overTime)
		{
			var t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / overTime;
				source.localRotation = Quaternion.Lerp(source.localRotation, target, t);
				yield return null;
			}
			source.localRotation = target;

		}
		static IEnumerator FollowObject(Transform ObjectToMove, Vector3 source, Transform target, float overTime, Action OnDone)
		{
			var t = 0f;
			while (t < 1f)
			{
				if (ObjectToMove == null)
					break;
				//t += Mathf.Pow(Time.deltaTime / overTime, 2f); // Hoch 2 damit Extra langsam Anwächst und zum Ende Sehr schnell
				t += Time.unscaledDeltaTime / overTime;
				ObjectToMove.position = Vector3.Lerp(source, target.position, t);
				yield return null;
			}
			// Null Error bei Killig Object after Movements 
			// Falls das Object gekilled wird nach Movments, dann braucht man auch keine Positions mehr ändern, 
			if (ObjectToMove)
				ObjectToMove.position = target.position;
			OnDone();
		}
		static IEnumerator FollowObjectSmooth(Transform source, Transform target, float overTime, Action OnDone)
		{
			var t = 0f;
			Vector3 vel = Vector3.zero;
			while (t < 1f)
			{
				t += Time.deltaTime / overTime;
				source.position = Vector3.SmoothDamp(source.position, target.position, ref vel, 0.125f);
				yield return null;
			}
			source.position = target.position;
			OnDone();
		}
		static IEnumerator MoveObject(Transform source, Vector3 target, float overTime)
		{
			var t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / overTime;
				source.position = Vector3.LerpUnclamped(source.position, target, t);
				yield return null;
			}
			source.position = target;
		}

		static IEnumerator MoveObjectLocal(Transform source, Vector3 target, float overTime)
		{
			//var sw = new System.Diagnostics.Stopwatch();
			//sw.Start();
			var t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / overTime;
				source.localPosition = Vector3.LerpUnclamped(source.localPosition, target, t);
				yield return null;
			}
			source.localPosition = target;
			//sw.Stop();
			//Debug.Log("Lerp Ende in Millisec: "+ sw.ElapsedMilliseconds);
		}

		private static IEnumerator waitLoopUntil(float waitOf, Action thanDo, Func<bool> until, Action LastDo)
		{
			do
			{
				yield return new WaitForSeconds(waitOf);
				thanDo();
			} while (until());
			LastDo();
		}

		static IEnumerator WaitandDoos(Func<bool> waitof, params Func<Coroutine>[] dos)
		{
			var wait = new WaitUntil(waitof);
			yield return wait;
			for (var i = 0; i < dos.Length; i++)
			{
				yield return dos[i]();
			}
		}

		static IEnumerator waitOf(Func<bool> Waitof, UnityAction thanDo, bool Loop)
		{
			var wait = new WaitUntil(Waitof);
			do
			{
				yield return wait;
				thanDo.Invoke();
			}
			while (Loop);
		}
		static IEnumerator waitForCustemYieldConstruction(CustomYieldInstruction Coro, Action AfterWait)
		{
				yield return Coro;
				AfterWait();
		}
		static IEnumerator waitFor(float time, Coroutine Coro, Action AfterWait, bool Loop)
		{
			do
			{
				yield return Coro;
				yield return new WaitForSeconds(time);
				AfterWait();

			}
			while (Loop);
		}
		static IEnumerator waitFor(YieldInstruction waitOf, Action AfterWait, bool Loop)
		{
			do
			{
				yield return waitOf;
				AfterWait();
			}
			while (Loop);
		}
		static IEnumerator waitFor(Func<bool> waitof, Action AfterWait, bool Loop)
		{
			do
			{
				yield return new WaitUntil(waitof);
				AfterWait();
			}
			while (Loop);
		}
		static IEnumerator LerpPointToPointRegelmaessig(Transform transObject, List<Vector3> Points, float speed)
		{
			var PointsCount = Points.Count;
			if (PointsCount < 3)
			{
				Debug.Log("List Points brauchen mindestens 3 Vektoren");
				yield break;
			}
			var NextStreckenPunkt = 0f;
			var lerpStartZeit = Time.time;
			var altPoint = Points[0];
			var nextPoint = Points[1];
			var distanz = Vector2.Distance(altPoint, nextPoint);
			//transform.rotation = nextPoint.getRotat();



			for (var i = 2; i < PointsCount; i++)
			{

				var timeSinceLerpStarted = Time.time - lerpStartZeit;
				NextStreckenPunkt += timeSinceLerpStarted * speed;
				if (NextStreckenPunkt > distanz)
				{
					NextStreckenPunkt -= distanz;
					distanz = 0;
					do
					{
						altPoint = nextPoint;
						nextPoint = Points[i];
						distanz += Vector2.Distance(altPoint, nextPoint);
					}
					while (NextStreckenPunkt > distanz);
				}
				var lerpPunkt = 1 / distanz * NextStreckenPunkt;
				transObject.position = Vector2.Lerp(altPoint, nextPoint, lerpPunkt);
				//transObject.rotation = nextPoint.getRotat();

				lerpStartZeit = Time.time;
			}
			transObject.position = Points[PointsCount - 1];
		}
	}
}

