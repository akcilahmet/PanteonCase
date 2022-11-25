using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utils
{
    public static class UtilsMethod 
    {
        public const int sortingOrderDefault = 5000;
        private static Quaternion[] cachedQuaternionEulerArr;
        
        #region GetMouseWorldPos

            public static Vector3 GetMouseWorldPosition() {
                Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
                vec.z = 0f;
                return vec;
            }
            public static Vector3 GetMouseWorldPositionWithZ() {
                return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            }
            public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) {
                return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
            }
            public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
                Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
                return worldPosition;
            }

        #endregion

        #region Color
        // Get Color from Hex string FF00FFAA
        public static Color GetColorFromString(string color) {
            float red = Hex_to_Dec01(color.Substring(0,2));
            float green = Hex_to_Dec01(color.Substring(2,2));
            float blue = Hex_to_Dec01(color.Substring(4,2));
            float alpha = 1f;
            if (color.Length >= 8) {
                // Color string contains alpha
                alpha = Hex_to_Dec01(color.Substring(6,2));
            }
            return new Color(red, green, blue, alpha);
        }
        // Returns a float between 0->1
        public static float Hex_to_Dec01(string hex) {
            return Hex_to_Dec(hex)/255f;
        }
        public static int Hex_to_Dec(string hex) {
            return Convert.ToInt32(hex, 16);
        }
        

        #endregion

        #region TextCreate
        // Create Text in the World
            public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft,
                TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault) {
                if (color == null) color = Color.white;
                return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
            }
            // Create Text in the World
            public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) {
                GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
                Transform transform = gameObject.transform;
                transform.SetParent(parent, false);
                transform.localPosition = localPosition;
                TextMesh textMesh = gameObject.GetComponent<TextMesh>();
                textMesh.anchor = textAnchor;
                textMesh.alignment = textAlignment;
                textMesh.text = text;
                textMesh.fontSize = fontSize;
                textMesh.color = color;
                textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
                return textMesh;
            }
            

        #endregion
        
        
        public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles) {
            vertices = new Vector3[4 * quadCount];
            uvs = new Vector2[4 * quadCount];
            triangles = new int[6 * quadCount];
        }
        
        public static void AddToMeshArrays(Vector3[] vertices, Vector2[] uvs, int[] triangles, int index, Vector3 pos, float rot, Vector3 baseSize, Vector2 uv00, Vector2 uv11) {
            //Relocate vertices
            int vIndex = index*4;
            int vIndex0 = vIndex;
            int vIndex1 = vIndex+1;
            int vIndex2 = vIndex+2;
            int vIndex3 = vIndex+3;

            baseSize *= .5f;

            bool skewed = baseSize.x != baseSize.y;
            if (skewed) {
                vertices[vIndex0] = pos+GetQuaternionEuler(rot)*new Vector3(-baseSize.x,  baseSize.y);
                vertices[vIndex1] = pos+GetQuaternionEuler(rot)*new Vector3(-baseSize.x, -baseSize.y);
                vertices[vIndex2] = pos+GetQuaternionEuler(rot)*new Vector3( baseSize.x, -baseSize.y);
                vertices[vIndex3] = pos+GetQuaternionEuler(rot)*baseSize;
            } else {
                vertices[vIndex0] = pos+GetQuaternionEuler(rot-270)*baseSize;
                vertices[vIndex1] = pos+GetQuaternionEuler(rot-180)*baseSize;
                vertices[vIndex2] = pos+GetQuaternionEuler(rot- 90)*baseSize;
                vertices[vIndex3] = pos+GetQuaternionEuler(rot-  0)*baseSize;
            }
		
            //Relocate UVs
            uvs[vIndex0] = new Vector2(uv00.x, uv11.y);
            uvs[vIndex1] = new Vector2(uv00.x, uv00.y);
            uvs[vIndex2] = new Vector2(uv11.x, uv00.y);
            uvs[vIndex3] = new Vector2(uv11.x, uv11.y);
		
            //Create triangles
            int tIndex = index*6;
		
            triangles[tIndex+0] = vIndex0;
            triangles[tIndex+1] = vIndex3;
            triangles[tIndex+2] = vIndex1;
		
            triangles[tIndex+3] = vIndex1;
            triangles[tIndex+4] = vIndex3;
            triangles[tIndex+5] = vIndex2;
        }
        
        private static Quaternion GetQuaternionEuler(float rotFloat) {
            int rot = Mathf.RoundToInt(rotFloat);
            rot = rot % 360;
            if (rot < 0) rot += 360;
            //if (rot >= 360) rot -= 360;
            if (cachedQuaternionEulerArr == null) CacheQuaternionEuler();
            return cachedQuaternionEulerArr[rot];
        }
        
       
        private static void CacheQuaternionEuler() {
            if (cachedQuaternionEulerArr != null) return;
            cachedQuaternionEulerArr = new Quaternion[360];
            for (int i=0; i<360; i++) {
                cachedQuaternionEulerArr[i] = Quaternion.Euler(0,0,i);
            }
        }
       
    }
}

