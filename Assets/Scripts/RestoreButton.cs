// using UnityEngine;
// using UnityEngine.EventSystems;
//
// public class RestoreButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
// {
// 	private void Awake()
// 	{
// 		base.gameObject.SetActive(Application.platform == RuntimePlatform.IPhonePlayer && ResourceManager.AbleToRestore);
// 	}
//
// 	private void OnEnable()
// 	{
// 		ResourceManager.ProductRestored += ResourceManagerOnProductRestored;
// 		ResourceManager.ProductPurchased += ResourceManagerOnProductPurchased;
// 	}
//
// 	private void OnDisable()
// 	{
// 		ResourceManager.ProductRestored -= ResourceManagerOnProductRestored;
// 		ResourceManager.ProductPurchased -= ResourceManagerOnProductPurchased;
// 	}
//
// 	private void ResourceManagerOnProductRestored(bool b)
// 	{
// 		base.gameObject.SetActive(ResourceManager.AbleToRestore);
// 	}
//
// 	private void ResourceManagerOnProductPurchased(string s)
// 	{
// 		base.gameObject.SetActive(ResourceManager.AbleToRestore);
// 	}
//
// 	public void OnPointerClick(PointerEventData eventData)
// 	{
// 		ResourceManager.RestorePurchase();
// 	}
// }
