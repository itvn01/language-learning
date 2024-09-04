using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UMExtensions;

namespace M1Game
{
    public static class TweenUtils
    {
        public static Tweener FadeIn(GameObject obj, float duration, Action complete = null, float delay = 0f)
        {
            var canvasGroup = obj.GetOrAddComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            return TweenUtils.FadeTo(canvasGroup, 1f, duration, complete, delay);
        }

        public static Tweener FadeOut(GameObject obj, float duration, Action complete = null, float delay = 0f)
        {
            return TweenUtils.FadeTo(obj, 0f, duration, complete, delay);
        }

        public static Tweener FadeTo(GameObject obj, float target, float duration, Action complete = null, float delay = 0f)
        {
             var canvasGroup = obj.GetOrAddComponent<CanvasGroup>();
            return TweenUtils.FadeTo(canvasGroup, target, duration, complete, delay);
        }

        public static Tweener FadeTo(CanvasGroup canvasGroup, float target, float duration, Action complete = null, float delay = 0f)
        {
            return canvasGroup.DOFade(target, duration).SetDelay(delay).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (complete != null)
                {
                    complete.Invoke();
                }
            });
        }

        public static void FadeTo(SpriteRenderer spr, float from, float target, float duration, System.Action complete = null, float delay = 0f)
        {
            var start_color = spr.color;
            start_color.a = from / 255f;
            spr.color = start_color;
            spr.DOColor(new Color(start_color.r, start_color.g, start_color.b, target / 255f), duration).SetDelay(delay).OnComplete(() => complete?.Invoke());
        }

        public static void LocalScaleTo(GameObject obj, Vector3 from, Vector3 to, float duration, Action complete = null, float delay = 0f)
        {
            obj.transform.localScale = from;
            obj.transform.DOScale(to, duration).SetDelay(delay).OnStepComplete(() => complete?.Invoke());
        }
    }
}

