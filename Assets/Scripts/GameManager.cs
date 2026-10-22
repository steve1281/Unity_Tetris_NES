using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Tetris
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GridManager gridManager;
        [SerializeField] private List<Tetro> tets = new List<Tetro>();
        private static GameManager instance;
        public static GameManager Instance { get => instance; private set => instance = value; }

        private void Awake()
        {
            if (GameManager.Instance != null) return;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            StartCoroutine(Tests());
        }
        void Update()
        {
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                Debug.Log("Move Right");
            }
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                Debug.Log("Move Left");
            }
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                Debug.Log("Soft Drop");
            }
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                Debug.Log("Rotate");
            }
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("Hard Drop");
            }
        }

        private IEnumerator Tests()
        {
            int offset = 0;
            foreach (Tetro tet in tets)
            {
                
                yield return StartCoroutine(Test(tet, 210+offset));
                //offset += 1;
            }
        }
        private IEnumerator Test(Tetro tet, int position)
        {
            int currentRotation = 0;
            int pastRotation = 0;

            TetroShape t = tet.shapes[currentRotation%tet.shapes.Count];

            float localWait = 0;
            while (position >0) 
            {
                if (localWait > 0.5f)
                {
                    EraseTet(t, position);
                    // right rotation
                    if (currentRotation > tet.shapes.Count-1 )
                    {
                        currentRotation = 0;
                    }
                    t = tet.shapes[currentRotation];
                    position = position - 10;
                    if (!TestRender(t.shape, position))
                    {
                        DrawTet(t, position);
                        pastRotation = currentRotation;
                        //currentRotation++;
                    }
                    else
                    { 
                        // collision occurred
                        t = tet.shapes[pastRotation];
                        position += 10;
                        DrawTet(t, position); // + 10);
                        Debug.Log("Fini-via Collision");
                        yield break;
                    }
                    Physics2D.SyncTransforms();
                    //Debug.Log("In Situ");
                    localWait = 0;
                } else
                {
                    localWait += Time.deltaTime;
                }
                yield return null;
            }
            Debug.Log("Fini");
        }
        private void EraseTet(TetroShape t, int position)
        {
            RenderTet(t.shape, Palette.White, position);
        }
        private void DrawTet(TetroShape t, int position)
        {
            RenderTet(t.shape, t.ActiveColor, position);
        }
        private bool TestRender(List<int> x, int position)
        {
            bool flag = false;
            //for (int j= x.Count -1;j>=0; j--)
            for (int j = 0; j < x.Count; j++)
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
