using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unlocks.View.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Unlocks.View;
using Random = UnityEngine.Random;

namespace Unlocks.Domain
{
    public interface IGesturedPointHandler
    {
        void OnDragStart(GesturedPoint point);

        void OnDragEnter(GesturedPoint point);
    }

    public class GestureGameManager : GameBaseManager, IGesturedPointHandler
    {
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private JudgeTextAnimation judgeTextAnimation;

        [Header("Line Renderer")]
        [SerializeField]
        private LineRenderer lineRenderer;

        [SerializeField]
        private Color failedColor;

        [SerializeField]
        private Color successColor;

        [SerializeField]
        private List<GameObject> pointList;

        [SerializeField]
        private UnityEvent onEnter;

        private List<int> numList = new List<int>();

        private List<Vector2Int> indexList = new List<Vector2Int>();

        private Camera camera;

        private Stack<GesturedPoint> stack = new Stack<GesturedPoint>();

        private void Awake()
        {
            camera = Camera.main;
        }

        protected void Start()
        {
            Initialize();
            GameStart();
        }

        private void Initialize()
        {
            int[,] array = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    array[i, j] = 3 * i + j;
                }
            }


            List<Vector2Int> canToPos = new List<Vector2Int>();
            Vector2Int[] firstCan = new Vector2Int[]
            {
                new Vector2Int(0, 0), new Vector2Int(0, 2),
                new Vector2Int(2, 0), new Vector2Int(2, 2)
            };
            var first = firstCan[Random.Range(0, firstCan.Length)];
            indexList.Add(first); // 左上
            for (int i = 0; i < 9; i++)
            {
                var now = indexList.Last();
                // 候補算出
                canToPos.Clear();
                // 右方向
                if (now.x + 1 < 3)
                {
                    var pos = new Vector2Int(now.x + 1, now.y);
                    if (!indexList.Contains(pos)) canToPos.Add(pos);
                }

                // 左方向
                if (now.x - 1 > 0)
                {
                    var pos = new Vector2Int(now.x - 1, now.y);
                    if (!indexList.Contains(pos)) canToPos.Add(pos);
                }

                // 上方向
                if (now.y - 1 > 0)
                {
                    var pos = new Vector2Int(now.x, now.y - 1);
                    if (!indexList.Contains(pos)) canToPos.Add(pos);
                }

                // 下方向
                if (now.y + 1 < 3)
                {
                    var pos = new Vector2Int(now.x, now.y + 1);
                    if (!indexList.Contains(pos)) canToPos.Add(pos);
                }

                if (canToPos.Any())
                {
                    indexList.Add(canToPos[Random.Range(0, canToPos.Count)]);
                }
                else
                {
                    // 候補がない場合は抜ける.
                    break;
                }
            }

            numList.Clear();
            foreach (var pos in indexList)
            {
                numList.Add(array[pos.x, pos.y]);
            }

            pointList[first.x * 3 + first.y].GetComponent<Image>().color = new Color(0.2753055f, 0.754717f, 0.4016958f);

            foreach (var point in pointList)
            {
                point.GetComponent<GesturedPoint>().Initialize(this);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && !IsSuccess)
            {
                if (stack.Count == 0) return;

                var result = Judge();
                if (result)
                {
                    Success();
                    return;
                }
                else
                {
                    judgeTextAnimation.ErrorFlash();
                }

                foreach (var gesturedPoint in stack)
                {
                    gesturedPoint.SetSelected(false);
                }

                UpdateLine();

                stack.Clear();
            }
        }

        public void OnDragStart(GesturedPoint point)
        {
            if (IsSuccess) return;

            if (numList[0] != point.Index)
            {
                judgeTextAnimation.ErrorFlash();
                UpdateLine();
                stack.Clear();
                return;
            }

            onEnter.Invoke();

            stack.Push(point);
            point.SetSelected(true);
            UpdateLine();
        }

        public void OnDragEnter(GesturedPoint point)
        {
            if (IsSuccess) return;

            // ジェスチャーを開始しているか
            if (stack.Count > 0)
            {
                // 重複チェック
                if (stack.Contains(point))
                {
                    return;
                }

                onEnter.Invoke();

                stack.Push(point);
                point.SetSelected(true);
                
                var points = stack.Reverse().Select(p => p.Index).ToArray();
                for (var i = 0; i < stack.Count; i++)
                {
                    if (points[i] != numList[i])
                    {
                        judgeTextAnimation.ErrorFlash();
                        UpdateLine();
                        stack.Clear();
                        return;
                    }
                }

                if (Judge())
                {
                    Success();
                }

                UpdateLine();
            }
        }

        private void UpdateLine()
        {
            var pointArray = stack
                .Select(gesturedPoint => GetWorldPositionFromRectPosition(gesturedPoint.RectTransform)).ToArray();
            lineRenderer.positionCount = pointArray.Length;
            foreach (var p in pointArray)
            {
                // Debug.Log(p);
            }

            lineRenderer.SetPositions(pointArray);
        }

        private bool Judge()
        {
            if (numList.Count != stack.Count) return false;

            var points = stack.Reverse().Select(point => point.Index).ToArray();
            for (var i = 0; i < stack.Count; i++)
            {
                if (points[i] != numList[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override async void OnSuccess()
        {
            await lineRenderer.DOColor(
                new Color2(lineRenderer.startColor, lineRenderer.endColor),
                new Color2(successColor, successColor), 1f);

            judgeTextAnimation.SuccessFlash();

            base.OnSuccess();
        }

        private Vector3 GetWorldPositionFromRectPosition(RectTransform rect)
        {
            // UI座標からスクリーン座標に変換
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(camera, rect.position);
            // return screenPos;

            // ワールド座標
            Vector3 result = Vector3.zero;

            // スクリーン座標→ワールド座標に変換
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPos, canvas.worldCamera, out result);

            return result;
        }
    }
}