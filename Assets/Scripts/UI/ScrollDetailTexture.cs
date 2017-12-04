using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScrollDetailTexture : MonoBehaviour
{
    public bool uniqueMaterial = false;
    public Vector2 scrollPerSecond = Vector2.zero;

    Matrix4x4 m_Matrix;
    Material mCopy;
    Material mOriginal;
    Image mSprite;
    Material m_Mat;

    private void OnEnable ()
    {
        mSprite = GetComponent<Image>();
        mOriginal = mSprite.material;

        if (!uniqueMaterial || mSprite.material == null) return;
        mCopy = new Material(mOriginal);
        mCopy.name = "Copy of " + mOriginal.name;
        mCopy.hideFlags = HideFlags.DontSave;
        mSprite.material = mCopy;
    }

    private void OnDisable ()
    {
        if (mCopy != null)
        {
            mSprite.material = mOriginal;
            if (Application.isEditor)
                DestroyImmediate(mCopy);
            else
                Destroy(mCopy);
            mCopy = null;
        }
        mOriginal = null;
    }

    private void Update ()
    {
        Material mat = (mCopy != null) ? mCopy : mOriginal;

        if (mat == null) return;
        Texture tex = mat.GetTexture("_DetailTex");

        if (tex != null)
        {
            mat.SetTextureOffset("_DetailTex", scrollPerSecond * Time.time);

            // TODO: It would be better to add support for MaterialBlocks on UIRenderer,
            // because currently only one Update() function's matrix can be active at a time.
            // With material block properties, the batching would be correctly broken up instead,
            // and would work with multiple widgets using this detail shader.
        }
    }
}