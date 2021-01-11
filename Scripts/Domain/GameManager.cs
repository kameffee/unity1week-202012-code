using Unlocks.View;
using UnityEngine;

namespace Unlocks.Domain
{
    public class GameManager
    {
        #region Singleton

        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }

                return instance;
            }
        }

        #endregion

        public StageManager StageManager { get; private set; }

        public ITransition Transition { get; private set; }

        public StatusCanvas StatusCanvas { get; private set; }

        public GameStatus Status { get; private set; }

        public UserModel User { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            instance.Initialize();
        }

        private async void Initialize()
        {
            StageManager = new StageManager();
            StageManager.Initialize();

            User = new UserModel();
            User.Initialize();

            // 画面遷移
            var transitionCanvas = await TransitionCanvas.Create();
            Object.DontDestroyOnLoad(transitionCanvas.gameObject);
            Transition = transitionCanvas;

            // 状態
            Status = new GameStatus();

            var statusCanvas = await StatusCanvas.Create();
            statusCanvas.Initialize(Status);
            Object.DontDestroyOnLoad(statusCanvas.gameObject);
            StatusCanvas = statusCanvas;
        }
    }
}