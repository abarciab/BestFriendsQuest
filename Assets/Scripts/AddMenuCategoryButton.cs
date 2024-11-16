using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMenuCategoryButton : MonoBehaviour
{
    public FeatureSubType Type;

    public void Select()
    {
        GetComponentInParent<AddMenuController>().ChangeCategory(Type);
    }
}
