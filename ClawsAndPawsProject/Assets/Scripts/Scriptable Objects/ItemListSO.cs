using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Items/Item List", fileName="New Item List")]
public class ItemListSO : ScriptableObject {

	public ItemSO[] items;
}
