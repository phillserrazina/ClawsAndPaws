using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class InputCaretFix : MonoBehaviour {
 
    private InputField inputField = null;
 
    private IEnumerator Start ()
    {
        yield return null;
        inputField = GetComponent<InputField>();
 
       if (inputField != null)
       {
           // Find the child by name. This usually isnt good but is the easiest way for the time being.
           Transform caretGO = inputField.transform.Find(inputField.transform.name + " Input Caret");
           caretGO.GetComponent<CanvasRenderer>().SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
       }
    }

    private void Update() {
        if (inputField == null)
            inputField = GetComponent<InputField>();
    }
}
