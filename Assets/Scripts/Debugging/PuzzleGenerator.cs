using UnityEngine;

public class PuzzleGenerator : MonoBehaviour {
    [SerializeField] private int puzzleSize;
    [SerializeField] private bool isAvailableFlat = true;

    private PieceGenerator pieceGenerator;
    private int[,] horizontalSide;
    private int[,] verticalSide;
    private Mesh[,] puzzleMeshs;

    private void Awake() {
        gameObject.AddComponent<PieceGenerator>();
        pieceGenerator = GetComponent<PieceGenerator>();

        horizontalSide = new int[puzzleSize + 1, puzzleSize];
        verticalSide = new int[puzzleSize, puzzleSize + 1];
        puzzleMeshs = new Mesh[puzzleSize, puzzleSize];
    }

    private void Start() {
        GeneratePuzzles();
    }

    public void GeneratePuzzles() {
        for (int i = 0; i <= puzzleSize; i++) {
            for (int j = 0; j < puzzleSize; j++) {
                if(i == 0 || i == puzzleSize) {
                    horizontalSide[i, j] = 0;
                }else {
                    horizontalSide[i, j] = Random.Range(isAvailableFlat ? 0 : 1, 3);
                }
            }
        }

        for(int i = 0; i < puzzleSize; i++) {
            for(int j = 0; j <= puzzleSize; j++) {
                if(j == 0 || j == puzzleSize) {
                    verticalSide[i, j] = 0;
                } else {
                    verticalSide[i, j] = Random.Range(isAvailableFlat ? 0 : 1, 3);
                }
            }
        }

        for (int i = 0; i < puzzleSize; i++) {
            for (int j = 0; j < puzzleSize; j++) {
                //int bottomType, int leftType, int topType, int rightType
                if (i == 0 && j == 0){
                    puzzleMeshs[i, j] = pieceGenerator.GeneratePieceMesh(verticalSide[i, j + 1], horizontalSide[i, j], verticalSide[i, j], horizontalSide[i + 1, j]);
                } else if(i != 0 && j == 0) {
                    puzzleMeshs[i, j] = pieceGenerator.GeneratePieceMesh(verticalSide[i, j + 1], (horizontalSide[i, j] == 0) ? 0 : 3 - horizontalSide[i, j], verticalSide[i, j], horizontalSide[i + 1, j]);
                } else if(i == 0 && j != 0) {
                     puzzleMeshs[i, j] = pieceGenerator.GeneratePieceMesh(verticalSide[i, j + 1], horizontalSide[i, j], (verticalSide[i, j] == 0) ? 0 : 3 - verticalSide[i, j], horizontalSide[i + 1, j]);
                } else {
                    puzzleMeshs[i, j] = pieceGenerator.GeneratePieceMesh(verticalSide[i, j + 1], (horizontalSide[i, j] == 0) ? 0 : 3 - horizontalSide[i, j], (verticalSide[i, j] == 0) ? 0 : 3 - verticalSide[i, j], horizontalSide[i + 1, j]);
                }
            }
        }

        GeneratePuzzleObj();
    }

    private void GeneratePuzzleObj() {
        for (int i = 0; i < puzzleSize; i++) {
            for (int j = 0; j < puzzleSize; j++) {
                GameObject obj = new GameObject("piece" + i + ", " + j);
                obj.transform.position = new Vector3(i, -j, 0);
                obj.transform.localScale = new Vector3(0.9f, 0.9f, 0);
                MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
                MeshFilter meshFilter = obj.AddComponent<MeshFilter>();

                meshFilter.mesh = puzzleMeshs[i, j];
            }
        }
    }
}
