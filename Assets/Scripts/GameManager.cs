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

        [SerializeField] private List<Tetro> tets = new List<Tetro>();

        private void Start()
        {
            StartCoroutine(Tests());
        }
        private IEnumerator Tests()
        {
            int offset = 0;
            foreach (Tetro t in tets)
            {
                yield return StartCoroutine(Test(t, 210+offset));
                offset += 2;
            }
        }
        private IEnumerator Test(Tetro t, int position)
        {
            float localWait = 0;
            while (position >0) 
            {
                if (localWait > 0.5f)
                {
                    EraseTet(t, position);
                    position = position - 10;
                    if (!TestRender(t.shape, position))
                    {
                        DrawTet(t, position);
                    }
                    else
                    {
                        position = position + 10;
                        DrawTet(t, position);
                        Debug.Log("Fini-via Collision");
                        yield break;
                    }
                    Physics2D.SyncTransforms();
                    Debug.Log("In Situ");
                    localWait = 0;
                } else
                {
                    localWait += Time.deltaTime;
                }
                yield return null;
            }
            Debug.Log("Fini");
        }
        private void EraseTet(Tetro t, int position)
        {
            RenderTet(t.shape, Palette.White, position);
        }
        private void DrawTet(Tetro t, int position)
        {
            RenderTet(t.shape, t.ActiveColor, position);
        }
        private bool TestRender(List<int> x, int position)
        {
            bool flag = false;
            for (int j= x.Count -1;j>=0; j--)
            {
                int i = x[j];
                if (gridManager.HasCollision(position + i))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private void RenderTet(List<int> x, Color c, int position)
        {
            foreach (int i in x)
            { 
                gridManager.UpdatePosition(position + i, c); 
            }
        }
    }

}
