using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unlocks.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Unlocks.View
{
    public class IntroAnimation : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI consolePrefab;

        [SerializeField]
        private Transform holder;

        [SerializeField]
        private List<ConsoleCommand> commandList;

        [SerializeField]
        private UnityEvent onClose;

        public async UniTask Play()
        {
            foreach (var command in commandList)
            {
                var text = Instantiate(consolePrefab, holder);

                if (!string.IsNullOrEmpty(command.User))
                {
                    var speakerName = command.User;
                    speakerName = speakerName.Replace("{name}", GameManager.Instance.User.UserName);
                    await text.DOText($"$ {speakerName}: ", 0f);
                }

                if (command.Message == "[load]")
                {
                    await DOVirtual.Float(0, 100, 5f, value => text.text = $"> {value:0}%");
                    await UniTask.Delay(TimeSpan.FromSeconds(.5f));
                    text.text += "\n> complete.";
                }
                else if (command.Message == "[load2]")
                {
                    await DOVirtual.Float(0, 100, 1f, value => text.text = $"> Module setup... {value:0}%");

                    text = Instantiate(consolePrefab, holder);
                    await DOVirtual.Float(0, 100, 1.5f, value => text.text = $"> Connect client... {value:0}%");

                    text = Instantiate(consolePrefab, holder);
                    await DOVirtual.Float(0, 100, 1f, value => text.text = $"> Connect server... {value:0}%");

                    text = Instantiate(consolePrefab, holder);
                    await DOVirtual.Float(0, 100, 1f, value => text.text = $"> Cracking phase 1... {value:0}%");

                    text = Instantiate(consolePrefab, holder);
                    await DOVirtual.Float(0, 100, 0.8f, value => text.text = $"> Cracking phase 2... {value:0}%");

                    text = Instantiate(consolePrefab, holder);
                    await DOVirtual.Float(0, 100, 0.5f, value => text.text = $"> Scan public area... {value:0}%");

                    text = Instantiate(consolePrefab, holder);
                    await DOVirtual.Float(0, 100, 0.5f, value => text.text = $"> Crack public area... {value:0}%");

                    text = Instantiate(consolePrefab, holder);
                    await DOVirtual.Float(0, 100, 0.5f, value => text.text = $"> Create security hole... {value:0}%");

                    text = Instantiate(consolePrefab, holder);
                    await DOVirtual.Float(0, 100, 1f, value => text.text = $"> Join security hole... {value:0}%");

                    text.text += "\n> complete.";
                }
                else
                {
                    var message = command.Message.Replace("{name}", GameManager.Instance.User.UserName);
                    await text.DOText(text.text + message, command.duration);
                }

                if (command.afterInputWait)
                {
                    await WaitInput();
                }
            }
        }

        public async UniTask Close()
        {
            onClose.Invoke();

            var canvas = GetComponent<CanvasGroup>();
            await canvas.DOFade(0, .5f)
                .SetEase(Ease.Flash, 5f);
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }

        private async UniTask WaitInput()
        {
            List<UniTask> uniTasks = new List<UniTask>();
            uniTasks.Add(UniTask.WaitUntil(() => Input.GetMouseButtonDown(0)));
            await UniTask.WhenAny(uniTasks);
        }


        [Serializable]
        public class ConsoleCommand
        {
            public string User;

            public float duration = 2f;

            [TextArea]
            public string Message;

            public bool afterInputWait = true;
        }
    }
}