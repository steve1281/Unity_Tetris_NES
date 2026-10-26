using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tetris
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GridManager gridManager;
        [SerializeField] private List<Tetro> tets = new List<Tetro>();
        [SerializeField] private TextMeshProUGUI ScoreText;

        private static GameManager instance;
        public static GameManager Instance { get => instance; private set => instance = value; }
        private bool rotateAvailable = false;
        private int positionAvailable = 0;

        public int score = 0;

        private void Awake()
        {
            if (GameManager.Instance != null) return;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            InitializeTetros(tets);
            StartCoroutine(Tests());
        }
        void Update()
        {
            ScoreText.text = score.ToString("0000");

            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                //Debug.Log("Move Right");
                positionAvailable = 1;
            }
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                //Debug.Log("Move Left");
                positionAvailable = -1;
            }
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                Debug.Log("Soft Drop");
            }
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                //Debug.Log("Rotate");
                rotateAvailable = true;
            }
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("Hard Drop");
            }
        }

        private void InitializeTetros(List<Tetro> tets)
        {
            int i = 0;
            foreach(Tetro tet in tets)
            {
                
                foreach(TetroShape shape in tet.shapes)
                {
                    i = 0;
                    if (shape.xshape.GetValue(0,0)) shape.shape[i++] = 0;
                    if (shape.xshape.GetValue(0,1)) shape.shape[i++] = 1;
                    if (shape.xshape.GetValue(0,2)) shape.shape[i++] = 2;
                    if (shape.xshape.GetValue(0,3)) shape.shape[i++] = 3;

                    if (shape.xshape.GetValue(1,0)) shape.shape[i++] = -10;
                    if (shape.xshape.GetValue(1,1)) shape.shape[i++] = -9;
                    if (shape.xshape.GetValue(1,2)) shape.shape[i++] = -8;
                    if (shape.xshape.GetValue(1,3)) shape.shape[i++] = -7;

                    if (shape.xshape.GetValue(2,0)) shape.shape[i++] = -20;
                    if (shape.xshape.GetValue(2,1)) shape.shape[i++] = -19;
                    if (shape.xshape.GetValue(2,2)) shape.shape[i++] = -18;
                    if (shape.xshape.GetValue(2,3)) shape.shape[i++] = -17;

                    if (shape.xshape.GetValue(3,0)) shape.shape[i++] = -30;
                    if (shape.xshape.GetValue(3,1)) shape.shape[i++] = -29;
                    if (shape.xshape.GetValue(3,2)) shape.shape[i++] = -28;
                    if (shape.xshape.GetValue(3,3)) shape.shape[i++] = -27;
                }
            }
        }
        

        private int UpdatePosition(int position)
        {
            position += positionAvailable;
            positionAvailable = 0;
            return position;
        }
        private int UpdateRotation(int currentRotation)
        {
            if (rotateAvailable)
            {
                rotateAvailable = false;
                currentRotation++;
            }
            return currentRotation;
        }

        private List<int> CreateDropOrder()
        {
            List<int> temp = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
            temp = temp.OrderBy(x => UnityEngine.Random.value).ToList();
            return temp;
        }
        private IEnumerator Tests()
        {
            int offset = 0;
            Tetro tet;
            List<int> dropOrder = new List<int>();

            while (true)
            {
                dropOrder = CreateDropOrder();
                foreach (int i in dropOrder)
                {
                    tet = tets[i];
                    yield return StartCoroutine(Test(tet, 210 + offset));
                }
            }
        }
        private IEnumerator Test(Tetro tet, int position)
        {
            int currentRotation = 0;
            int pastRotation = 0;


            TetroShape t = tet.shapes[0];// currentRotation%tet.shapes.Count];

            float localWait = 0;
            while (position >0) 
            {
                if (localWait > 0.5f)
                {
                    EraseTet(t, position);
                    currentRotation = UpdateRotation(currentRotation) % tet.shapes.Count;
                    //Debug.Log($"Current Rotation = {currentRotation}");
                    position = UpdatePosition(position);
                    t = tet.shapes[currentRotation];
                    position = position - 10;
                    if (!TestRender(t.shape, position))
                    {
                        DrawTet(t, position);
                        pastRotation = currentRotation;
                    }
                    else
                    { 
                        // collision occurred
                        t = tet.shapes[pastRotation];
                        position += 10;
                        DrawTet(t, position); 
                        Debug.Log("Fini-via Collision"); // <-- this will always be reached
                        gridManager.ResolveLines();
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
            Debug.Log("Fini"); // <-- this should never be reached.
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

        public void UpdateScore()
        {
            score++;  // for now just increase score
        }
    }

}
