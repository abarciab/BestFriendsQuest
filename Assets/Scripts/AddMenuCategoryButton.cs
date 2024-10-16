using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMenuCategoryButton : MonoBehaviour
{
    public FeatureType Type;

    public void Select()
    {
        GetComponentInParent<AddMenuController>().ChangeCategory(Type);
    }
}
