using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get => instance; private set => instance = value; }
        
        private void Awake()
        {
            if (GameManager.Instance != null) return;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        [SerializeField] private GridManager gridManager;
        [SerializeField] private Tetro testTet;

        int position = 210;
        private float timeElapsed = 0f;
        private float totalTime = 5f;

        private void Start()
        {
            StartCoroutine(Test());
        }
        private IEnumerator Test()
        {
            float localWait = 0;
            while (position >0) //timeElapsed < totalTime)
            {
                if (localWait > 0.5f)
                {
                    EraseTet(testTet);
                    position = position - 10;
                    if (position < 0) position = 0;
                    DrawTet(testTet);
                    Physics2D.SyncTransforms();
                    Debug.Log("In Situ");
                    localWait = 0;
                } else
                {
                    localWait += Time.deltaTime;
                }
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            Debug.Log("Fini");
        }
        private void EraseTet(Tetro t)
        {
            RenderTet(t.shape, Palette.White);
        }
        private void DrawTet(Tetro t)
        {
            RenderTet(t.shape, t.ActiveColor);
        }
        private void RenderTet(List<int> x, Color c)
        {
            foreach (int i in x)
            {
                gridManager.UpdatePosition(position + i, c); 
            }
        }
    }

}
