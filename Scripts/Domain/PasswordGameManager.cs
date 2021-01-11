using System;
using Unlocks.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unlocks.Domain
{
    public class PasswordGameManager : GameBaseManager
    {
        [SerializeField]
        private string password = "1234";

        [SerializeField]
        private PasswordGameHUD hud;

        private string inputNumberCache = null;

        protected void Start()
        {
            hud.ClearNumber();
            Setup();

            GameStart();
        }

        public void Setup()
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    Initialize((DateTime.Now.Month * 100 + DateTime.Now.Day).ToString("0000"), "TODAY ?");
                    break;
                case 1:
                    Initialize("1225", "Xmas");
                    break;
            }
        }

        public void Initialize(string password, string hintText)
        {
            this.password = password;
            hud.SetHintText(hintText);
        }

        public void InputNumber(int number)
        {
            if (IsSuccess) return;

            if (inputNumberCache == null)
            {
                inputNumberCache = "";
                hud.ClearNumber();
            }

            inputNumberCache += number;

            hud.UpdateInputView(inputNumberCache);
            if (inputNumberCache.Length >= 4)
            {
                // 判定
                if (Judge())
                {
                    Success();
                }
                else
                {
                    hud.ErrorFlash();
                    inputNumberCache = "";
                }
            }
        }

        public override void OnSuccess()
        {
            hud.SuccessFlash();

            base.OnSuccess();
        }

        public bool Judge()
        {
            return (password.Equals(inputNumberCache));
        }

        /// <summary>
        /// 失敗
        /// </summary>
        public void Failed()
        {
            hud.ErrorFlash();
        }
    }
}